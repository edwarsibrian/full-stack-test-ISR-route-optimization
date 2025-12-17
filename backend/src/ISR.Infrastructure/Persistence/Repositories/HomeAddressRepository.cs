using ISR.Application.Common.Interfaces;
using ISR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISR.Infrastructure.Persistence.Repositories
{
    public class HomeAddressRepository : IHomeAddressRepository
    {
        private readonly AppDbContext _context;

        public HomeAddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HomeAddress?> GetAsync(CancellationToken ct = default)
        {
            return await _context.HomeAddresses
                .AsNoTracking()
                .FirstOrDefaultAsync(ct);
        }

        public async Task SaveAsync(HomeAddress homeAddress, CancellationToken ct = default)
        {
            var existing = await _context.HomeAddresses.FirstOrDefaultAsync(ct);
            if (existing is null)
            {
                _context.HomeAddresses.Add(homeAddress);
            }
            else
            {
                existing.Update(homeAddress.Latitude, homeAddress.Longitude);
            }

            await _context.SaveChangesAsync(ct);
        }
    }
}
