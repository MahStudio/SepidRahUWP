using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;

namespace MappingUtilities
{
    public static class GeopointExtensions
    {
        private const double circle = 2 * Math.PI;
        private const double degreesToRadian = Math.PI / 180.0;
        private const double radianToDegrees = 180.0 / Math.PI;
        private const double earthRadius = 6378137.0;

        public static IList<Geopoint> GetCirclePoints(this Geopoint center,
                                       double radius, int nrOfPoints = 50)
        {
            var locations = new List<Geopoint>();
            double latA = center.Position.Latitude * degreesToRadian;
            double lonA = center.Position.Longitude * degreesToRadian;
            double angularDistance = radius / earthRadius;

            double sinLatA = Math.Sin(latA);
            double cosLatA = Math.Cos(latA);
            double sinDistance = Math.Sin(angularDistance);
            double cosDistance = Math.Cos(angularDistance);
            double sinLatAtimeCosDistance = sinLatA * cosDistance;
            double cosLatAtimeSinDistance = cosLatA * sinDistance;

            double step = circle / nrOfPoints;
            for (double angle = 0; angle < circle; angle += step)
            {
                var lat = Math.Asin(sinLatAtimeCosDistance + cosLatAtimeSinDistance *
                                    Math.Cos(angle));
                var dlon = Math.Atan2(Math.Sin(angle) * cosLatAtimeSinDistance,
                                      cosDistance - sinLatA * Math.Sin(lat));
                var lon = ((lonA + dlon + Math.PI) % circle) - Math.PI;

                locations.Add(new Geopoint(new BasicGeoposition
                {
                    Latitude = lat * radianToDegrees,
                    Longitude = lon * radianToDegrees
                }));
            }
            return locations;
        }
        public static IList<Geopoint> GetCirclePoints(this BasicGeoposition center,
                            double radius, int nrOfPoints = 50)
        {
            return new Geopoint(center).GetCirclePoints(radius, nrOfPoints);
        }
    }

}