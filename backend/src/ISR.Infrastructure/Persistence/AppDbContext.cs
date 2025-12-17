using ISR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISR.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lead> Leads => Set<Lead>();
        public DbSet<HomeAddress> HomeAddresses => Set<HomeAddress>();
    }
}
