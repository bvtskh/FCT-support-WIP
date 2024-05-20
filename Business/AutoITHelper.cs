using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NichiconJP_FCT_Support_WIP.Entity;

namespace NichiconJP_FCT_Support_WIP.Business
{
    public class AutoITHelper
    {
        static string mainTitle = "HAM14W Ver.2.511";
        public static string GetBoardNo()
        {
            var winHandle = AutoIt.AutoItX.WinGetHandle(mainTitle);
            if (winHandle != IntPtr.Zero)
            {
                var controlHandle = AutoIt.AutoItX.ControlGetHandle(winHandle, "[CLASS:ThunderRT6TextBox; INSTANCE:1]");
                if (controlHandle != IntPtr.Zero)
                {
                    string boardNo = AutoIt.AutoItX.ControlGetText(winHandle, controlHandle);
                    return boardNo;
                }
            }

            return null;
        }
        public static bool IsScanLabel()
        {
            string title = "LabelScan";
            var winHwnd = AutoIt.AutoItX.WinGetHandle(title);
            //var winHwnd = KAutoHelper.AutoControl.FindWindowHandle("", title);
            return winHwnd != IntPtr.Zero;
        }
    }
}
