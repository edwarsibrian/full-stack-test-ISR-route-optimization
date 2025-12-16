using ISR.Domain.Enums;

namespace ISR.Application.Leads.Upload
{
    public sealed record UploadLeadsResult(
    int TotalRows,
    int ImportedLeads,
    int FailedLeads);
}
