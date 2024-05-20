using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NichiconJP_FCT_Support_WIP.RunTime
{
    public static class RunTimeManager
    {
        public static bool isRunning { get; set; }
        public static bool isBussy { get; set; }
        public static string GetRunningVersion()
        {
            //try
            //{
            //   // return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            //}
            //catch
            //{
            //    return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //}
            return null;
        }
    }
}
