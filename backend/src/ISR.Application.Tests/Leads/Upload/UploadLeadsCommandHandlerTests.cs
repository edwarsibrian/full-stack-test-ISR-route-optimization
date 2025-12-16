using ISR.Application.Common.Interfaces;
using ISR.Application.Leads.Upload;
using ISR.Domain.Entities;
using Moq;

namespace ISR.Application.Tests.Leads.Upload
{
    [TestFixture]
    public class UploadLeadsCommandHandlerTests
    {
        private Mock<ILeadRepository> _leadRepositoryMock = null!;
        private UploadLeadsCommandHandler _handler = null!;

        [SetUp]
        public void Setup()
        {
            _leadRepositoryMock = new Mock<ILeadRepository>();

            _handler = new UploadLeadsCommandHandler(
                _leadRepositoryMock.Object
            );
        }

        [Test]
        public async Task Handle_ShouldImportOnlyManagerFile_WhenIsrFileIsNull()
        {
            // Arrange
            var managerFile = TestFormFileFactory.CreateValidCsv();

            var command = new UploadLeadsCommand(
                managerFile,
                null
            );

            _leadRepositoryMock
                .Setup(r => r.AddRangeAsync(
                    It.IsAny<IEnumerable<Lead>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.TotalRows, Is.EqualTo(2));
            Assert.That(result.ImportedLeads, Is.EqualTo(2));
            Assert.That(result.FailedLeads, Is.EqualTo(0));

            _leadRepositoryMock.Verify(r =>
                r.AddRangeAsync(
                    It.Is<IEnumerable<Lead>>(l => l.Count() == 2),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public async Task Handle_ShouldImportBothFiles_WhenManagerAndIsrAreProvided()
        {
            // Arrange
            var managerFile = TestFormFileFactory.CreateValidCsv();
            var isrFile = TestFormFileFactory.CreateValidCsv();

            var command = new UploadLeadsCommand(
                managerFile,
                isrFile
            );

            _leadRepositoryMock
                .Setup(r => r.AddRangeAsync(
                    It.IsAny<IEnumerable<Lead>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert (resultado agregado)
            Assert.That(result.TotalRows, Is.EqualTo(4));
            Assert.That(result.ImportedLeads, Is.EqualTo(4));
            Assert.That(result.FailedLeads, Is.EqualTo(0));

            // Assert (persistencia por archivo)
            _leadRepositoryMock.Verify(r =>
                r.AddRangeAsync(
                    It.Is<IEnumerable<Lead>>(l => l.Count() == 2),
                    It.IsAny<CancellationToken>()),
                Times.Exactly(2));
        }

        [Test]
        public async Task Handle_ShouldIgnoreEmptyCsv()
        {
            // Arrange
            var managerFile = TestFormFileFactory.CreateEmptyCsv();

            var command = new UploadLeadsCommand(
                managerFile,
                null
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.TotalRows, Is.EqualTo(0));
            Assert.That(result.ImportedLeads, Is.EqualTo(0));
            Assert.That(result.FailedLeads, Is.EqualTo(0));

            _leadRepositoryMock.Verify(r =>
                r.AddRangeAsync(It.IsAny<IEnumerable<Lead>>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
