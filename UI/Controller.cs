using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;
using RigCAT.NET;

namespace UI
{
    internal sealed class Controller
    {
        public event EventHandler ContactStoreChanged;
        public event EventHandler CivServerChanged;

        public ContactStore ContactStore {get;private set;}
        public IRadio Radio { get;private set;}
        public WinKey WinKey { get; private set; }
        public CWMacro CWMacro { get; private set; }

        public Controller()
        {
            WinKey = new WinKey("COM17");
            CWMacro = new CWMacro(null);
            CWMacro.WinKey = WinKey;
        }

        public void OpenLog()
        {
            // Get the login details
            using (LogonForm lf = new LogonForm())
            {
                DialogResult dr = lf.ShowDialog();
                if (dr != DialogResult.OK)
                    return;

                ContactStore = new ContactStore(lf.Server, lf.Database, lf.Username, lf.Password);
                if (ContactStoreChanged != null)
                    ContactStoreChanged(this, new EventArgs());

                if (!string.IsNullOrEmpty(lf.CivSerialPort))
                {
                    Radio = new RadioFactory().GetRadio(RadioModel.IcomGeneric, new RadioConnectionSettings { BaudRate = 19200, FlowControl = FlowControl.None, Port = "COM2", UseDTR = true, UseRTS = true });
                    if (CivServerChanged != null)
                        CivServerChanged(this, new EventArgs());
                }
            }
        }
    }
}
