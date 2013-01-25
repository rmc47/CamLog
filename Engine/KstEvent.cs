using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public sealed class KstMessageEventArgs : EventArgs
    {
        public string Time { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
    }
}
