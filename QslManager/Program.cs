using QslEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QslManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //DymoEngine de = new DymoEngine();
            //de.PrintAddress(new Address { Callsign = "M0VFC", AddressLines = new string[] { "Foo" }, Name = "Robert Chipperfield" });
            Application.Run(new MainForm());
        }
    }
}
