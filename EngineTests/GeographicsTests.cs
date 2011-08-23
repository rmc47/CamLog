using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Engine;

namespace EngineTests
{
    [TestFixture]
    public class GeographicsTests
    {
        [Test]
        public void CheckDistance()
        {
            Locator start = new Locator(53 + (double)9 / 60 + (double)2 / (60 * 60), -(1 + (double)50 / 60 + (double)40 / (60 * 60)));
            Locator end = new Locator(52 + (double)12 / 60 + (double)19 / (60 * 60), -(0 + (double)8 / 60 + (double)33 / (60 * 60)));
            double distance = Geographics.GeodesicDistance(start, end);
            Assert.AreEqual(155927.727, distance);
        }

        [Test]
        public void CheckBearing()
        {
            Locator start = new Locator(53 + (double)9 / 60 + (double)2 / (60 * 60), -(1 + (double)50 / 60 + (double)40 / (60 * 60)));
            Locator end = new Locator(52 + (double)12 / 60 + (double)19 / (60 * 60), (0 + (double)8 / 60 + (double)33 / (60 * 60)));
            int heading = Geographics.BeamHeading(start, end);
            Assert.AreEqual(127, heading);
        }

        [Test]
        public void CheckMaidenheadFromLatLong()
        {
            Assert.AreEqual("AB12cd", Geographics.MaidenheadFromLatLong(-77.854, -177.791));
            Assert.AreEqual("JO02ee", Geographics.MaidenheadFromLatLong(52.188, 0.375));
            Assert.AreEqual("JR78aa", Geographics.MaidenheadFromLatLong(88, 14));
            Assert.AreEqual("GH77dp", Geographics.MaidenheadFromLatLong(-12.345, -45.678));
        }

        [Test]
        public void CheckLatLongFromMaidenhead()
        {
            double latitude;
            double longitude;

            Geographics.LatLongFromMaidenhead("AB12cd", out latitude, out longitude);
            AssertWithinDistance(latitude, longitude, -77.854, -177.891);
        }

        [Test]
        public void CheckMaidenheadRoundtrip_Random()
        {
            double latitude;
            double longitude;

            Random r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                latitude = (r.NextDouble() - 0.5) * 90;
                longitude = (r.NextDouble() - 0.5) * 180;
                string maidenhead = Geographics.MaidenheadFromLatLong(latitude, longitude);
                double lat2;
                double long2;
                Geographics.LatLongFromMaidenhead(maidenhead, out lat2, out long2);
                Console.WriteLine("Maidenhead {0} within {1}km", maidenhead, AssertWithinDistance(latitude, longitude, lat2, long2));
            }
        }

        [Test]
        public void MyLoc()
        {
            Console.WriteLine(Geographics.MaidenheadFromLatLong(51.96465, 0.29951));
        }

        private int AssertWithinDistance(double lat1, double long1, double lat2, double long2)
        {
            // Two points in same subsquare < 12KM from each other
            double dist = Geographics.GeodesicDistance(new Locator(lat1, long1), new Locator(lat2, long2));
            Assert.Greater(12000, dist);
            return (int)Math.Round(dist / 1000);
        }
    }
}
