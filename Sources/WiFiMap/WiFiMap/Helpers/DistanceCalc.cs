using System;

namespace WiFiMap.Helpers
{
    internal static class DistanceCalc
    {
        private const double Radius = 6371;

        private static double Radians(double x)
        {
            return x * Math.PI / 180;
        }

        public static double DistanceBetweenPlaces(double lat1, double lon1, double lat2, double lon2)
        {
            var dlon = Radians(lon2 - lon1);
            var dlat = Radians(lat2 - lat1);

            var a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(Radians(lat1)) * Math.Cos(Radians(lat2)) *
                       (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
            var angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return angle * Radius * 1000;
        }
    }
}