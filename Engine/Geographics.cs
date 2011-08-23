using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public static class Geographics
    {
        private const double c_Wgs84_A = 6378137;
        private const double c_Wgs84_B = 6356752.3142;
        private const double c_Wgs84_F = 1 / 298.257223563;

        public static double GeodesicDistance(Locator start, Locator end)
        {
            double L = DegreesToRadians(end.Longitude - start.Longitude);

            //var U1 = Math.atan((1-f) * Math.tan(lat1.toRad()));
            //var U2 = Math.atan((1-f) * Math.tan(lat2.toRad()));
            //var sinU1 = Math.sin(U1), cosU1 = Math.cos(U1);
            //var sinU2 = Math.sin(U2), cosU2 = Math.cos(U2);
            double U1 = Math.Atan((1 - c_Wgs84_F) * Math.Tan(DegreesToRadians(start.Latitude)));
            double U2 = Math.Atan((1 - c_Wgs84_F) * Math.Tan(DegreesToRadians(end.Latitude)));
            double sinU1 = Math.Sin(U1);
            double cosU1 = Math.Cos(U1);
            double sinU2 = Math.Sin(U2);
            double cosU2 = Math.Cos(U2);

            //var lambda = L, lambdaP, iterLimit = 100;
            //  do {
            //    var sinLambda = Math.sin(lambda), cosLambda = Math.cos(lambda);
            //    var sinSigma = Math.sqrt((cosU2*sinLambda) * (cosU2*sinLambda) + 
            //      (cosU1*sinU2-sinU1*cosU2*cosLambda) * (cosU1*sinU2-sinU1*cosU2*cosLambda));
            //    if (sinSigma==0) return 0;  // co-incident points
            //    var cosSigma = sinU1*sinU2 + cosU1*cosU2*cosLambda;
            //    var sigma = Math.atan2(sinSigma, cosSigma);
            //    var sinAlpha = cosU1 * cosU2 * sinLambda / sinSigma;
            //    var cosSqAlpha = 1 - sinAlpha*sinAlpha;
            //    var cos2SigmaM = cosSigma - 2*sinU1*sinU2/cosSqAlpha;
            //    if (isNaN(cos2SigmaM)) cos2SigmaM = 0;  // equatorial line: cosSqAlpha=0 (§6)
            //    var C = f/16*cosSqAlpha*(4+f*(4-3*cosSqAlpha));
            //    lambdaP = lambda;
            //    lambda = L + (1-C) * f * sinAlpha *
            //      (sigma + C*sinSigma*(cos2SigmaM+C*cosSigma*(-1+2*cos2SigmaM*cos2SigmaM)));
            //  } while (Math.abs(lambda-lambdaP) > 1e-12 && --iterLimit>0);
            double lambda = L;
            double lambdaP;
            double cosSqAlpha, sinSigma, cos2SigmaM, cosSigma, sigma;
            int iterLimit = 100;
            do
            {
                double sinLambda = Math.Sin(lambda);
                double cosLambda = Math.Cos(lambda);
                sinSigma = Math.Sqrt((cosU2 * sinLambda) * (cosU2 * sinLambda) +
                    (cosU1 * sinU2 - sinU1 * cosU2 * cosLambda) * (cosU1 * sinU2 - sinU1 * cosU2 * cosLambda));
                if (sinSigma == 0)
                    return 0; // co-incident points
                cosSigma = sinU1 * sinU2 + cosU1 * cosU2 * cosLambda;
                sigma = Math.Atan2(sinSigma, cosSigma);
                double sinAlpha = cosU1 * cosU2 * sinLambda / sinSigma;
                cosSqAlpha = 1 - sinAlpha * sinAlpha;
                cos2SigmaM = cosSigma - 2 * sinU1 * sinU2 / cosSqAlpha;
                if (double.IsNaN(cos2SigmaM))
                    cos2SigmaM = 0; // equatorial line: cosSqAlpha=0 (§6)
                double C = c_Wgs84_F / 16 * cosSqAlpha * (4 + c_Wgs84_F * (4 - 3 * cosSqAlpha));
                lambdaP = lambda;
                lambda = L + (1 - C) * c_Wgs84_F * sinAlpha *
                    (sigma + C * sinSigma * (cos2SigmaM + C * cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM)));
            }
            while (Math.Abs(lambda - lambdaP) > 1e-12 && --iterLimit > 0);

            if (iterLimit == 0) return double.NaN;  // formula failed to converge

            double uSq = cosSqAlpha * (c_Wgs84_A * c_Wgs84_A - c_Wgs84_B * c_Wgs84_B) / (c_Wgs84_B * c_Wgs84_B);
            double A = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)));
            double B = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)));
            double deltaSigma = B * sinSigma * (cos2SigmaM + B / 4 * (cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM) -
              B / 6 * cos2SigmaM * (-3 + 4 * sinSigma * sinSigma) * (-3 + 4 * cos2SigmaM * cos2SigmaM)));
            double s = c_Wgs84_B * A * (sigma - deltaSigma);

            s = Math.Round(s, 3); // round to 1mm precision
            return s;
        }


        public static int BeamHeading(Locator start, Locator end)
        {
            ////double R = 6371; // km
            //double dLat = DegreesToRadians(end.Latitude - start.Latitude);
            //double dLon = DegreesToRadians(end.Longitude - start.Longitude);
            ////double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            ////        Math.Cos(DegreesToRadians(end.Latitude)) * Math.Cos(DegreesToRadians(end.Latitude)) *
            ////        Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            ////double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            ////double d = R * c;
            //double y = Math.Sin(dLon) * Math.Cos(end.Latitude);
            //double x = Math.Cos(start.Latitude) * Math.Sin(end.Latitude) -
            //        Math.Sin(start.Latitude) * Math.Cos(end.Latitude) * Math.Cos(dLon);
            //double brng = Math.Atan2(y, x);
            //return ((int)RadiansToDegrees(brng) + 360) % 360;

            double lat1 = DegreesToRadians(start.Latitude);
            double lat2 = DegreesToRadians(end.Latitude);
            double dLon = DegreesToRadians(end.Longitude - start.Longitude);

            double y = Math.Sin(dLon) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) -
                    Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            int degrees = (int)RadiansToDegrees(Math.Atan2(y, x));
            if (degrees < 0)
                return 360 + degrees;
            else
                return degrees;
        }

        public static string MaidenheadFromLatLong(double latitude, double longitude)
        {
            // Correct for false easting / northing
            latitude = latitude + 90;
            longitude = longitude + 180;

            // Get field digits - each digit = 10 degrees lat, 20 degrees long
            int fieldLat = (int)Math.Floor(latitude / 10);
            int fieldLong = (int)Math.Floor(longitude / 20);

            // Get the square digits - each one = 1 degrees lat, 2 degrees long
            int squareLat = (int)Math.Floor(latitude) % 10;
            int squareLong = (int)Math.Floor(longitude / 2) % 10;

            // Get the subsquare digits - each one = 2.5 minutes of lat, 5 minutes of long
            int subLat = (int)Math.Floor((latitude - Math.Floor(latitude)) * 24);
            int subLong = (int)Math.Floor((longitude /2 - Math.Floor(longitude /2)) * 24);

            return string.Format("{0}{1}{2}{3}{4}{5}", (char)('A' + fieldLong), (char)('A' + fieldLat), squareLong, squareLat, (char)('a' + subLong), (char)('a' + subLat));
        }

        public static void LatLongFromMaidenhead(string maidenhead, out double latitude, out double longitude)
        {
            if (maidenhead == null || maidenhead.Length != 6)
                throw new ArgumentException("Locator must be 6 characters long", "maidenhead");

            // Get everything into nice workable ints :-)
            maidenhead = maidenhead.Trim().ToUpperInvariant();
            int fieldLong = maidenhead[0] - 'A';
            int fieldLat = maidenhead[1] - 'A';
            if (fieldLong > 18 || fieldLong < 0 || fieldLat > 18 || fieldLat < 0)
                throw new ArgumentException("Field is outside range A-R", "maidenhead");
            int squareLong = maidenhead[2] - '0';
            int squareLat = maidenhead[3] - '0';
            if (squareLong > 9 || squareLong < 0 || squareLat > 9 || squareLat < 0)
                throw new ArgumentException("Square must be 0-9", "maidenhead");
            int subLong = maidenhead[4] - 'A';
            int subLat = maidenhead[5] - 'A';
            if (subLong > 24 || subLong < 0 || subLat > 24 || subLat < 0)
                throw new ArgumentException("Subsquare must be a-x", "maidenhead");

            latitude = ((double)fieldLat * 10) + (double)squareLat + ((double)subLat / 24) - 90;
            longitude = ((double)fieldLong * 20) + (double)squareLong * 2 + ((double)subLong / 12) - 180;
        }

        private static double RadiansToDegrees(double radians)
        {
            return (radians / (2 * Math.PI)) * 360;
        }
        private static double DegreesToRadians(double degrees)
        {
            return (degrees / 360) * 2 * Math.PI;
        }
    }
}
