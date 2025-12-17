using ISR.Application.Common.Interfaces;
using ISR.Domain.Entities;
using MediatR;

namespace ISR.Application.Home
{
    public class SetHomeAddressCommandHandler
        : IRequestHandler<SetHomeAddressCommand, HomeAddressResult>
    {
        private readonly IHomeAddressRepository _repository;

        public SetHomeAddressCommandHandler(IHomeAddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<HomeAddressResult> Handle(
        SetHomeAddressCommand request,
        CancellationToken ct)
        {
            var homeAddress = new HomeAddress(
            request.Latitude,
            request.Longitude
        );

            await _repository.SaveAsync(homeAddress, ct);

            return new HomeAddressResult(
                homeAddress.Latitude,
                homeAddress.Longitude
            );
        }
    }
}
