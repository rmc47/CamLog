using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Engine
{
    public class Contact
    {
        private int m_Id;
        private int m_SourceId;
        private DateTime m_LastModified;
        private DateTime m_StartTime;
        private DateTime m_EndTime;
        private string m_Callsign;
        private string m_Station;
        private string m_Operator;
        private Band m_Band;
        private long m_Frequency;
        private Mode m_Mode;
        private string m_ReportSent;
        private string m_ReportReceived;
        private Locator m_LocatorReceived;
        private string m_Notes;

        private int m_SerialSent;
        private int m_SerialReceived;
        private string m_IotaRef;        
        private int m_Points;

        private DateTime? m_QslRxDate;
        private DateTime? m_QslTxDate;
        private string m_QslMethod;
        private int m_LocationID;

        public Contact()
        {
            m_Id = -1;
            m_LastModified = DateTime.UtcNow;
        }

        public int Id
        {
            get { return m_Id; }
            internal set { m_Id = value; }
        }

        public int SourceId
        {
            get { return m_SourceId; }
            set { m_SourceId = value; }
        }

        public DateTime LastModified
        {
            get { return m_LastModified; }
            internal set { m_LastModified = value; }
        }

        public DateTime StartTime
        {
            get { return m_StartTime;}
            set { m_StartTime = value;}
        }

        public DateTime EndTime
        {
            get { return m_EndTime; }
            set { m_EndTime = value; }
        }

        public string Callsign
        {
            get { return m_Callsign; }
            set { m_Callsign = value; }
        }

        public string ReportSent
        {
            get { return m_ReportSent; }
            set { m_ReportSent = value; }
        }

        public string ReportReceived
        {
            get { return m_ReportReceived; }
            set { m_ReportReceived = value; }
        }

        public int SerialSent
        {
            get { return m_SerialSent; }
            set { m_SerialSent = value; }
        }

        public int SerialReceived
        {
            get { return m_SerialReceived; }
            set { m_SerialReceived = value; }
        }

        public Locator LocatorReceived
        {
            get { return m_LocatorReceived; }
            set { m_LocatorReceived = value; }
        }

        public string LocatorReceivedString
        {
            get { return LocatorReceived == null ? string.Empty : m_LocatorReceived.ToString().ToUpperInvariant(); }
        }

        public string IotaRef
        {
            get { return m_IotaRef; }
            set { m_IotaRef = value; }
        }

        public string Operator
        {
            get { return m_Operator; }
            set { m_Operator = value; }
        }

        public Band Band
        {
            get { return m_Band; }
            set { m_Band = value; }
        }

        public long Frequency
        {
            get { return m_Frequency; }
            set { m_Frequency = value; }
        }

        public Mode Mode
        {
            get { return m_Mode; }
            set { m_Mode = value; }
        }

        public string Notes
        {
            get { return m_Notes; }
            set { m_Notes = value; }
        }

        public int Points
        {
            get { return m_Points; }
            set { m_Points = value; }
        }

        public string Station
        {
            get { return m_Station; }
            set { m_Station = value; }
        }

        public DateTime? QslRxDate
        {
            get { return m_QslRxDate; }
            set { m_QslRxDate = value; }
        }

        public DateTime? QslTxDate
        {
            get { return m_QslTxDate; }
            set { m_QslTxDate = value; }
        }

        public string QslMethod
        {
            get { return m_QslMethod;}
            set { m_QslMethod = value; }
        }

        public int LocationID
        {
            get { return m_LocationID; }
            set { m_LocationID = value; }
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", Id, StartTime, Callsign, ReportSent, ReportReceived, SerialSent, SerialReceived, LocatorReceivedString, Band, Mode, Operator);
        }

        public static int QsoMatchCompare(Contact c1, Contact c2)
        {
            if (c1 == null && c2 == null)
                return 0;
            if (c1 == null)
                return -1;
            if (c2 == null)
                return 1;

            int val;

            if (c1.SourceId > 0 && c2.SourceId > 0)
            {
                val = c1.SourceId.CompareTo(c2.SourceId);
                if (val != 0)
                    return val;
            }

            val = c1.Callsign.CompareTo(c2.Callsign);
            if (val != 0)
                return val;

            val = c1.Band.CompareTo(c2.Band);
            if (val != 0)
                return val;

            val = c1.Mode.CompareTo(c2.Mode);
            if (val != 0)
                return val;

            if (Math.Abs(c1.StartTime.Subtract(c2.StartTime).TotalMinutes) > 5)
                return c1.StartTime.CompareTo(c2.StartTime);

            // If callsign, band, mode and time (within 5 mins) match, call it the same
            return 0;
        }

        public static bool QsoMatchEquals(Contact c1, Contact c2)
        {
            return QsoMatchCompare(c1, c2) == 0;
        }

        public class QsoMatchComparer : IComparer<Contact>
        {
            public int Compare(Contact x, Contact y)
            {
                return QsoMatchCompare(x, y);
            }
        }
    }
}
