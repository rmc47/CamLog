using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WsjtxImport
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string station = null;
            string logFile = null;
            for (int i = 0; i< args.Length; i++)
            {
                if (args[i] == "--station")
                {
                    station = args[i + 1];
                }
                else if (args[i] == "--log")
                {
                    logFile = args[i + 1];
                }
                else if (args[i] == "--help")
                {
                    MessageBox.Show("To set initial station name and log file: WsjtxImport.exe --station \"Station name\" --log \"c:\\path\\to\\log.adi\"");
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WsjtxImportForm(initialStationName: station, initialAdifLogFile: logFile));
        }
    }
}
