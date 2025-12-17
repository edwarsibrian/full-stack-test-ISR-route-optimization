using MediatR;

namespace ISR.Application.Routes
{
    public sealed record GetOptimizedRouteQuery 
        : IRequest<OptimizedRouteResult>;
}
