using MediatR;

namespace ISR.Application.Home
{
    public sealed record SetHomeAddressCommand(
        double Latitude,
        double Longitude
    ) : IRequest<HomeAddressResult>;
}
