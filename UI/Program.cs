using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UI
{
    static class Program
    {
        public static Controller Controller { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Controller = new UI.Controller();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ContestForm());
        }
    }
}