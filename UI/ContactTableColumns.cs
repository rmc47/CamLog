using System;
using System.Collections.Generic;
using System.Text;

namespace UI
{
    internal enum ContactTableColumns : int
    {
        Band= 0,
        Time = 1,
        Callsign=2,
        RstSent = 3,
        SerialSent = 4,
        RstReceived = 5,
        SerialReceived = 6,
        LocatorReceived = 7,
        Distance = 8,
        Beam = 9,
        Comments = 10
    }
}
