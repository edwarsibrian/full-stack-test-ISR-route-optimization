using ISR.Application.Common.Interfaces;
using ISR.Domain.Enums;
using MediatR;

namespace ISR.Application.Leads.Upload
{
    public sealed class UploadLeadsCommandHandler
    : IRequestHandler<UploadLeadsCommand, UploadLeadsResult>
    {
        private readonly ILeadRepository _leadRepository;

        public UploadLeadsCommandHandler(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public async Task<UploadLeadsResult> Handle(
        UploadLeadsCommand request,
        CancellationToken cancellationToken)
        {
            var imported = 0;
            var failed = 0;

            // Manager leads (required)
            imported += await _leadRepository.ImportFromCsvAsync(
                request.ManagerFile,
                LeadSource.Manager,
                cancellationToken);

            // ISR leads (optional)
            if (request.IsrFile is not null)
            {
                imported += await _leadRepository.ImportFromCsvAsync(
                    request.IsrFile,
                    LeadSource.ISR,
                    cancellationToken);
            }

            return new UploadLeadsResult(
                TotalRows: imported + failed,
                ImportedLeads: imported,
                FailedLeads: failed
            );
        }
    }
}
