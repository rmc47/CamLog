using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QslEngine
{
    public sealed class Address
    {
        public string Callsign { get; set; }
        public string Name { get; set; }
        public string[] AddressLines { get; set; }
        public int FirstContactID { get; set; }
    }
}
