namespace ISR.Application.Routes
{
    public sealed record RouteStopResult(
        Guid LeadId,
        string Name,
        string Address,
        double Latitude,
        double Longitude,
        double DistanceFromPrevious);
}
