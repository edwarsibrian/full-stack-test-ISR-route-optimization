using ISR.Application.Common.Interfaces;
using ISR.Domain.Entities;
using ISR.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ISR.Infrastructure.Persistence.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private readonly AppDbContext _context;

        public LeadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> ImportFromCsvAsync(
        IFormFile file,
        LeadSource source,
        CancellationToken cancellationToken)
        {
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);

            var leads = new List<Lead>();

            // Skip header
            await reader.ReadLineAsync(cancellationToken);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync(cancellationToken);
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var columns = line.Split(',');

                try
                {
                    var lead = new Lead(
                        name: columns[0],
                        address: columns[1],
                        latitude: Convert.ToDouble(columns[2]),
                        longitude: Convert.ToDouble(columns[3]),
                        source: source
                    );

                    leads.Add(lead);
                }
                catch
                {
                    // some errors
                }
            }

            _context.Leads.AddRange(leads);
            await _context.SaveChangesAsync(cancellationToken);

            return leads.Count;
        }

        public async Task AddRangeAsync(
            IEnumerable<Lead> leads,
            CancellationToken ct)
        {
            _context.Leads.AddRange(leads);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyList<Lead>> GetAllAsync(CancellationToken cancellationToken)
        {
            var leads = await _context.Leads
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return leads.AsReadOnly();
        }
    }
}
