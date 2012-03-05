using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class SourceCallsign
    {
        public int SourceID { get; private set; }
        public string Callsign { get; private set; }

        public SourceCallsign(int sourceId, string callsign)
        {
            SourceID = sourceId;
            Callsign = callsign;
        }

        public override string ToString()
        {
            return Callsign;
        }

        public override bool Equals(object obj)
        {
            SourceCallsign sc = obj as SourceCallsign;
            if (sc == null)
                return false;
            if (sc.SourceID != SourceID)
                return false;
            return string.Equals(sc.Callsign, Callsign, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return SourceID.GetHashCode() ^ Callsign.GetHashCode();
        }
    }
}
