using System;
using System.Collections.Generic;
using System.Threading;

//using System.Linq;
using System.Windows.Forms;

namespace NichiconJP_FCT_Support_WIP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex singleton = new Mutex(true, "NichiconJP_FCT_Support_WIP");
            if (!singleton.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("NichiconJP_FCT_Support_WIP đang mở!","Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Stop);
                Environment.Exit(0);
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
