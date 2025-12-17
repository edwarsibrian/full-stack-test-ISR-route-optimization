using ISR.Application.Common.Interfaces;
using MediatR;

namespace ISR.Application.Home
{
    public class GetHomeAddressQueryHandler
        : IRequestHandler<GetHomeAddressQuery, HomeAddressResult?>
    {
        private readonly IHomeAddressRepository _repository;

        public GetHomeAddressQueryHandler(IHomeAddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<HomeAddressResult?> Handle(GetHomeAddressQuery request, CancellationToken ct)
        {
            var homeAddress = await _repository.GetAsync(ct);

            if (homeAddress is null)
                return null;

            return new HomeAddressResult(
                Latitude: homeAddress.Latitude,
                Longitude: homeAddress.Longitude
            );
        }
    }
}
