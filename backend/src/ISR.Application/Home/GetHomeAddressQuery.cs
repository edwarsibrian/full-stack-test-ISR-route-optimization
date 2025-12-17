using MediatR;

namespace ISR.Application.Home
{
    public sealed record GetHomeAddressQuery() : IRequest<HomeAddressResult>;
}
