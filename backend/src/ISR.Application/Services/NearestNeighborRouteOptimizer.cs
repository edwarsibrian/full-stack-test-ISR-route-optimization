using ISR.Application.Common.Interfaces;
using ISR.Application.Routes;
using ISR.Domain.Entities;

namespace ISR.Application.Services
{
    public sealed class NearestNeighborRouteOptimizer : IRouteOptimizer
    {
        public OptimizedRouteResult Optimize(
            double startLat,
            double startLng,
            IReadOnlyList<Lead> leads)
        {
            var remaining = leads.ToList();
            var stops = new List<RouteStopResult>();

            var currentLat = startLat;
            var currentLng = startLng;
            double totalDistance = 0;

            while (remaining.Any())
            {
                var next = remaining
                    .Select(l => new
                    {
                        Lead = l,
                        Distance = Distance(currentLat, currentLng, l.Latitude, l.Longitude)
                    })
                    .OrderBy(x => x.Distance)
                    .First();

                totalDistance += next.Distance;

                stops.Add(new RouteStopResult(
                    next.Lead.Id,
                    next.Lead.Name,
                    next.Lead.Address,
                    next.Lead.Latitude,
                    next.Lead.Longitude,
                    next.Distance));

                currentLat = next.Lead.Latitude;
                currentLng = next.Lead.Longitude;
                remaining.Remove(next.Lead);
            }

            return new OptimizedRouteResult(totalDistance, stops);
        }

        private static double Distance(
            double lat1, double lng1,
            double lat2, double lng2)
        {
            const double R = 6371; // km

            var dLat = DegreesToRadians(lat2 - lat1);
            var dLng = DegreesToRadians(lng2 - lng1);

            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreesToRadians(lat1)) *
                Math.Cos(DegreesToRadians(lat2)) *
                Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private static double DegreesToRadians(double deg) => deg * (Math.PI / 180);
    }
}
