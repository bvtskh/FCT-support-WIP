using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace NichiconJP_FCT_Support_WIP.Entity
{
    public static class SystemSetting
    {
        public static string stationNo { get; set; }
        public static string fileExtension { get; set; } = ".dat";
        public static string outputLog { get; set; }
        public static string processName { get; set; }
        public static string Source { get; set; }
        public static bool includeSubdirectories { get; set; }
    }
}
