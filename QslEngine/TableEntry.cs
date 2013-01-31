using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QslEngine
{
    public sealed class TableEntry
    {
        public TableEntry()
        {
        }

        public TableEntry(Contact c)
        {
            Band = BandHelper.ToMHzString(c.Band);
            Mode = ModeHelper.ToString(c.Mode);
            Rst = c.ReportSent;
            UtcTime = c.StartTime;
        }

        public DateTime UtcTime { get; set; }
        public string Band { get; set; }
        public string Mode { get; set; }
        public string Rst { get; set; }
    }
}
