using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public sealed class QrzEntry
    {
        public string Callsign { get; internal set; }
        public string Name { get; internal set; }
        public Locator Locator { get; internal set; }
    }
}
