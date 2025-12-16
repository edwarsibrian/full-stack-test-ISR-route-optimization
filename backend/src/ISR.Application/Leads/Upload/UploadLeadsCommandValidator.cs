using FluentValidation;
using ISR.Domain.Enums;

namespace ISR.Application.Leads.Upload
{
    public sealed class UploadLeadsCommandValidator
    : AbstractValidator<UploadLeadsCommand>
    {
        public UploadLeadsCommandValidator()
        {
            RuleFor(x => x.ManagerFile)
            .NotNull()
            .WithMessage("Manager CSV file is required");

            RuleFor(x => x.ManagerFile.FileName)
                .Matches(@"\.csv$")
                .WithMessage("Manager file must be a CSV");

            When(x => x.IsrFile is not null, () =>
            {
                RuleFor(x => x.IsrFile!.FileName)
                    .Matches(@"\.csv$")
                    .WithMessage("ISR file must be a CSV");
            });
        }
    }
}
