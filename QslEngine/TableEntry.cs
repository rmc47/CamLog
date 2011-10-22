using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QslEngine
{
    internal sealed class TableEntry
    {
        public string Callsign { get; set; }
        public string MyCall { get; set; }
        public DateTime UtcTime { get; set; }
        public string Band { get; set; }
        public string Mode { get; set; }
        public string Rst { get; set; }
    }
}
