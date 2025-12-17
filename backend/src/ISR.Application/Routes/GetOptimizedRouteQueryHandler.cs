using ISR.Application.Common.Interfaces;
using MediatR;

namespace ISR.Application.Routes
{
    public sealed class GetOptimizedRouteQueryHandler
    : IRequestHandler<GetOptimizedRouteQuery, OptimizedRouteResult>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IHomeAddressRepository _homeRepository;
        private readonly IRouteOptimizer _optimizer;

        public GetOptimizedRouteQueryHandler(
            ILeadRepository leadRepository,
            IHomeAddressRepository homeRepository,
            IRouteOptimizer optimizer)
        {
            _leadRepository = leadRepository;
            _homeRepository = homeRepository;
            _optimizer = optimizer;
        }

        public async Task<OptimizedRouteResult> Handle(
            GetOptimizedRouteQuery request,
            CancellationToken ct)
        {
            var home = await _homeRepository.GetAsync(ct)
                ?? throw new InvalidOperationException("Home address not set.");

            var leads = await _leadRepository.GetAllAsync(ct);

            return _optimizer.Optimize(
                home.Latitude,
                home.Longitude,
                leads.ToList());
        }
    }
}
