namespace ISR.Application.Routes
{
    public sealed record OptimizedRouteResult(
        double TotalDistance,
        IReadOnlyList<RouteStopResult> Stops);
}
