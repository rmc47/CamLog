using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public sealed class Locator
    {
        public static readonly Locator Unknown = new Locator(0, 0);

        private readonly string m_Maidenhead;
        private double? m_Lat;
        private double? m_Long;

        public Locator(string maidenhead)
        {
            if (maidenhead == null)
            {
                m_Lat = 0.0;
                m_Long = 0.0;
            }
            else
            {
                m_Maidenhead = maidenhead.Trim();
                if (m_Maidenhead.Length == 0)
                {
                    m_Lat = 0.0;
                    m_Long = 0.0;
                }
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
            get {
                if (!m_Lat.HasValue)
                    PopulateLatLong();
                return m_Lat.Value; 
            }
        }

        public double Longitude
        {
            get 
            {
                if (!m_Long.HasValue)
                    PopulateLatLong();
                return m_Long.Value; 
            }
        }

        private void PopulateLatLong()
        {
            double lat, lon;
            try
            {
                Geographics.LatLongFromMaidenhead(m_Maidenhead, out lat, out lon);
            }
            catch
            {
                lat = lon = 0.0;
            }
            m_Lat = lat;
            m_Long = lon;
        }

        public override string ToString()
        {
            return m_Maidenhead;
        }
    }
}
