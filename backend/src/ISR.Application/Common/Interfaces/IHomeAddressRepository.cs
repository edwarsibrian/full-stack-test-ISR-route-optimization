using ISR.Domain.Entities;

namespace ISR.Application.Common.Interfaces
{
    public interface IHomeAddressRepository
    {
        Task<HomeAddress?> GetAsync(CancellationToken ct = default);
        Task SaveAsync(HomeAddress homeAddress, CancellationToken ct = default);
    }
}
