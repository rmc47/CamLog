using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamLog.KstWatcher
{
    class ChatMessage
    {
        public DateTime Timestamp { get; set; }
        public string SenderCall { get; set; }
        public string RecipientCall { get; set; }
        public string Message { get; set; }
    }
}
