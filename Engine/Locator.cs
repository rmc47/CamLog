using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public sealed class Locator
    {
        public static readonly Locator Unknown = new Locator(0, 0);

        private readonly string m_Maidenhead;
        private readonly double m_Lat;
        private readonly double m_Long;

        public Locator(string maidenhead)
        {
            m_Maidenhead = maidenhead;
            try
            {
                Geographics.LatLongFromMaidenhead(maidenhead, out m_Lat, out m_Long);
            }
            catch
            {
#warning Mmm, tasty hack...
                m_Lat = m_Long = 0;
            }
        }

        public Locator(double latitude, double longitude)
        {
            m_Lat = latitude;
            m_Long = longitude;
            m_Maidenhead = Geographics.MaidenheadFromLatLong(latitude, longitude);
        }

        public double Latitude
        {
            get { return m_Lat; }
        }

        public double Longitude
        {
            get { return m_Long; }
        }

        public override string ToString()
        {
            return m_Maidenhead;
        }
    }
}
