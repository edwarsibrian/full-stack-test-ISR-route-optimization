using ISR.Application.Routes;
using ISR.Domain.Entities;

namespace ISR.Application.Common.Interfaces
{
    public interface IRouteOptimizer
    {
        OptimizedRouteResult Optimize(
            double startLat,
            double startLng,
            IReadOnlyList<Lead> leads);
    }
}
