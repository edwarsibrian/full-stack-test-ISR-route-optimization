using ISR.Domain.Entities;
using ISR.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace ISR.Application.Common.Interfaces
{
    public interface ILeadRepository
    {
        Task<int> ImportFromCsvAsync(
        IFormFile file,
        LeadSource source,
        CancellationToken cancellationToken);

        Task AddRangeAsync(
            IEnumerable<Lead> leads,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<Lead>> GetAllAsync(
            CancellationToken cancellationToken);
    }
}
