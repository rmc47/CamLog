using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QslEngine
{
    public sealed class LabelEntry
    {
        public string Callsign { get; set; }
        public string MyCall { get; set; }
        public Location Location { get; set; }
        public TableEntry[] QSOs { get; set; }
    }
}
