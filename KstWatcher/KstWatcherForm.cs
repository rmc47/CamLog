using CamLog.KstWatcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KstWatcher
{
    public partial class KstWatcherForm : Form
    {
        private KstConnection m_KstConnection;
        private List<ChatMessage> m_ChatMessages = new List<ChatMessage>();
        private Dictionary<string, DateTime> m_ActiveCallsigns = new Dictionary<string, DateTime>();

        private Engine.ContactStore m_ContactStore;

        public KstWatcherForm()
        {
            InitializeComponent();
            m_KstConnection = new KstConnection();
            m_KstConnection.ChatMessageReceived += ChatMessageReceived;
            m_KstConnection.Connect();
            m_ContactStore = new Engine.ContactStore("flossie01", "2017_08_01_144MHz_UKAC", "g3pye", "g3pye");
        }

        private void ChatMessageReceived(object sender, ChatMessage e)
        {
            lock (m_ChatMessages)
            {
                m_ChatMessages.Add(e);
                m_ActiveCallsigns[e.SenderCall] = e.Timestamp;
            }
            UpdateActiveCallsignsList();
        }

        private void UpdateActiveCallsignsList()
        {
            if (Disposing || IsDisposed || !IsHandleCreated)
                return;
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateActiveCallsignsList));
                return;
            }

            lock (m_ChatMessages)
            {
                object selectedCall = m_ActiveCallsignsList.SelectedItem;

                m_ActiveCallsignsList.BeginUpdate();
                m_ActiveCallsignsList.Items.Clear();
                var recentCalls = m_ActiveCallsigns.Where(callsign => callsign.Value > DateTime.UtcNow.AddMinutes(-10)).OrderBy(callsign => callsign.Key).Select(callsign => callsign.Key);
                foreach (string call in recentCalls.Where(callsign => CallsignFilter(callsign)))
                    m_ActiveCallsignsList.Items.Add(call);
                m_ActiveCallsignsList.EndUpdate();
            }
        }

        private bool CallsignFilter(string callsign)
        {
            string[] allowedPrefixes = new string[] { "G", "2", "M", "P", "D", "ON", "OV", "OZ", "F", "SM", "EI", "LX", };
            bool allowedPrefix = allowedPrefixes.Any(prefix => callsign.StartsWith(prefix));
            if (!allowedPrefix)
                return false;

            if (m_ContactStore.GetPreviousContacts(callsign).Count > 0)
                return false;

            return true;
        }

        private void CallsignClick(object sender, EventArgs e)
        {
            string selectedCallsign = m_ActiveCallsignsList.SelectedItem as string;
            if (selectedCallsign == null)
                return;

            m_LastMessagesList.BeginUpdate();
            m_LastMessagesList.Items.Clear();
            var messages = m_ChatMessages.Where(message => message.SenderCall == selectedCallsign).OrderByDescending(message => message.Timestamp).Take(20);
            foreach (ChatMessage message in messages)
                m_LastMessagesList.Items.Add(string.Format("{0} | {1} | {2}", message.Timestamp.ToShortTimeString(), message.RecipientCall, message.Message));
            m_LastMessagesList.EndUpdate();
        }
    }
}
