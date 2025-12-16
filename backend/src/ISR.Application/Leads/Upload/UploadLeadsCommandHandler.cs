using ISR.Application.Common.Interfaces;
using ISR.Domain.Entities;
using ISR.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Globalization;

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
            int imported = 0;
            int failed = 0;

            if (request.ManagerFile != null)
            {
                var (i, f) = await ImportFileAsync(
                    request.ManagerFile,
                    LeadSource.Manager,
                    cancellationToken);

                imported += i;
                failed += f;
            }

            if (request.IsrFile != null)
            {
                var (i, f) = await ImportFileAsync(
                    request.IsrFile,
                    LeadSource.ISR,
                    cancellationToken);

                imported += i;
                failed += f;
            }

            return new UploadLeadsResult(
                TotalRows: imported + failed,
                ImportedLeads: imported,
                FailedLeads: failed
            );
        }

        private async Task<(int imported, int failed)> ImportFileAsync(
        IFormFile file,
        LeadSource source,
        CancellationToken ct)
        {
            var leads = new List<Lead>();
            int failed = 0;

            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);

            string? line;
            bool isHeader = true;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                try
                {
                    var parts = line.Split(',');

                    var lead = new Lead(
                        parts[0],
                        parts[1],
                        double.Parse(parts[2], CultureInfo.InvariantCulture),
                        double.Parse(parts[3], CultureInfo.InvariantCulture),
                        source
                    );

                    leads.Add(lead);
                }
                catch
                {
                    failed++;
                }
            }

            if (leads.Any())
            {
                await _leadRepository.AddRangeAsync(leads, ct);
            }

            return (leads.Count, failed);
        }
    }
}
