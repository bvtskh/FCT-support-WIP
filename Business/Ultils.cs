using Microsoft.Win32;
using NichiconJP_FCT_Support_WIP.RunTime;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NichiconJP_FCT_Support_WIP.Business
{
    public class Ultils
    {

        public static void RegisterInStartup(bool isChecked, string executablePath)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (isChecked)
            {
                registryKey.SetValue("ApplicationName", executablePath);
            }
            else
            {
                registryKey.DeleteValue("ApplicationName");
            }
        }

        /// <summary>
        /// create log
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="productionId"></param>
        /// <param name="status"></param>
        /// <param name="process"></param>
        //public static void CreateFileLog(string model, string boardNo, string status, string process, DateTime dateCheck)
        //{
        //    status = "P";
        //    process = "FCT_MUR";
        //    string dateTime = dateCheck.ToString("yyMMddHHmmss");
        //    string fileName = $"{dateTime}_{model}_{boardNo}.txt";
        //    //string fileName1 = String.Format("{0}_{1}_{2}.txt", dateTime, model, productId);
        //    string folderRoot = $@"{GetValueRegistryKey("OutputLog")}\";

        //    bool exists = Directory.Exists(folderRoot);
        //    if (!exists)
        //        Directory.CreateDirectory(folderRoot);

        //    string path = folderRoot + fileName;
        //    if (!File.Exists(path))
        //    {
        //        File.Create(path).Dispose();
        //        using (TextWriter tw = new StreamWriter(path))
        //        {
        //            tw.WriteLine($"{model}|{boardNo}|{dateTime}|{status}|{process}");
        //            tw.Close();
        //        }
        //    }
        //    else if (File.Exists(path))
        //    {
        //        using (TextWriter tw = new StreamWriter(path))
        //        {
        //            tw.WriteLine($"{model}|{boardNo}|{dateTime}|{status}|{process}");
        //            tw.Close();
        //        }
        //    }
        //}

        public static bool IsCreateFileLog(string model, string productId, string status, string process, DateTime dateCheck)
        {
            string dateTime = dateCheck.ToString("yyMMddHHmmss");
            string fileName = $"{dateTime}_{model}_{productId}.txt";
            string folderRoot = $@"{GetValueRegistryKey("OutputLog")}";

            bool exists = Directory.Exists(folderRoot);
            if (!exists)
                Directory.CreateDirectory(folderRoot);

            string path = folderRoot +"\\"+fileName;
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    using (StreamWriter tw = new StreamWriter(path))
                    {
                        tw.WriteLine($"{model}|{productId}|{dateTime}|{status}|{process}");
                        return true;
                    }
                }
                else if (File.Exists(path))
                {
                    using (StreamWriter tw = new StreamWriter(path))
                    {
                        tw.WriteLine($"{model}|{productId}|{dateTime}|{status}|{process}");
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <param name="newline"></param>
        /// <returns></returns>
        public static string ReadLastLine(string path, Encoding encoding, string newline)
        {
            string line = null;
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (reader.Peek() == -1)
                    {
                        return line;
                    }
                }
                reader.Close();
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int CountLine(string path)
        {
            int count = 0;
            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(file))
            {
                while (reader.ReadLine() != null)
                {
                    count++;
                }
                reader.Close();
                return count;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string GetValueInLine(string path, int line)
        {
            string value;
            using (var sr = new StreamReader(path))
            {
                for (int i = 1; i < line; i++)
                {
                    sr.ReadLine();
                }
                value = sr.ReadLine();
                sr.Dispose();
                return value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string GetLine(string path, int line)
        {
            string value;
            using (var sr = new StreamReader(path))
            {
                for (int i = 1; i < line; i++)
                {
                    sr.ReadLine();
                }
                value = sr.ReadLine();
                sr.Dispose();
                return value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="content"></param>
        public static void WriteRegistry(string keyName, string content)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FCT_NICHICONJP_SUPPORT_WIP\Configs");
            if (!string.IsNullOrEmpty(keyName) && !string.IsNullOrEmpty(content))
            {
                key.SetValue(keyName, content);
                key.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="content"></param>
        public static void WriteRegistryArray(string keyName, string content)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FCT_NICHICONJP_SUPPORT_WIP\Configs");
            if (!string.IsNullOrEmpty(keyName) && !string.IsNullOrEmpty(content))
            {
                string exitsValue = GetValueRegistryKey(keyName);
                if (exitsValue != null)
                {
                    exitsValue += content + ";";
                    key.SetValue(keyName, exitsValue);
                }
                else
                {
                    key.SetValue(keyName, content + ";");
                }
                key.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static string GetValueRegistryKey(string keyName)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FCT_NICHICONJP_SUPPORT_WIP\Configs");
                if (!key.GetSubKeyNames().Contains(keyName))
                {
                    Registry.CurrentUser.CreateSubKey($"{key}\\{keyName}");
                }
                var value = key.GetValue(keyName);
                if (keyName == "Version")
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                    var runingVersion = ConvertVersion(fvi.FileVersion);
                    var oldversion = ConvertVersion(key.GetValue("Version"));
                    if (oldversion == null)
                    {
                        key.SetValue(keyName, fvi.FileVersion);
                        value = key.GetValue(keyName);
                    }
                    else
                    {
                        if (oldversion < runingVersion)
                        {
                            key.SetValue(keyName, fvi.FileVersion);
                            value = key.GetValue(keyName);
                        }
                    }
                }
                key.Close();
                return value == null ? null : value.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static int? ConvertVersion(object version)
        {
            try
            {
                if (version != null)
                {
                    string ver = version.ToString().Replace(".", string.Empty);
                    int result;
                    if (int.TryParse(ver, out result)) return result;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileProcessName(string filePath)
        {
            Process[] procs = Process.GetProcesses();
            string fileName = Path.GetFileName(filePath);

            foreach (Process proc in procs)
            {
                if (proc.MainWindowHandle != new IntPtr(0) && !proc.HasExited)
                {
                    ProcessModule[] arr = new ProcessModule[proc.Modules.Count];

                    foreach (ProcessModule pm in proc.Modules)
                    {
                        if (pm.ModuleName == fileName)
                            return proc.ProcessName;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsFileLocked(string filePath)
        {
            try
            {
                using (File.Open(filePath, FileMode.Open)) { }
                return false;
            }
            catch (IOException e)
            {
                var errorCode = Marshal.GetHRForException(e) & ((1 << 16) - 1);

                return errorCode == 32 || errorCode == 33;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }


        const int ERROR_SHARING_VIOLATION = 32;
        const int ERROR_LOCK_VIOLATION = 33;
        private static bool IsFileLocked(Exception exception)
        {
            int errorCode = Marshal.GetHRForException(exception) & ((1 << 16) - 1);
            return errorCode == ERROR_SHARING_VIOLATION || errorCode == ERROR_LOCK_VIOLATION;
        }

        public static bool CanReadFile(string filePath)
        {
            //Try-Catch so we dont crash the program and can check the exception
            try
            {
                //The "using" is important because FileStream implements IDisposable and
                //"using" will avoid a heap exhaustion situation when too many handles  
                //are left undisposed.
                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    if (fileStream != null)
                        fileStream.Close();  //This line is me being overly cautious, fileStream will never be null unless an exception occurs... and I know the "using" does it but its helpful to be explicit - especially when we encounter errors - at least for me anyway!
                }
            }
            catch (IOException ex)
            {
                //THE FUNKY MAGIC - TO SEE IF THIS FILE REALLY IS LOCKED!!!
                if (IsFileLocked(ex))
                {
                    // do something, eg File.Copy or present the user with a MsgBox - I do not recommend Killing the process that is locking the file
                    return false;
                }
            }
            finally
            {
            }
            return true;
        }
        public static void ActiveWindows(string title)
        {
            int windowHandle = NativeWin32.FindWindow(null, title);
            NativeWin32.SetForegroundWindow(windowHandle);
        }
    }
}

