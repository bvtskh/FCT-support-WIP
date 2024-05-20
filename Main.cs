using NichiconJP_FCT_Support_WIP.Business;
using NichiconJP_FCT_Support_WIP.Entity;
using NichiconJP_FCT_Support_WIP.PVSServiceReference;
using NichiconJP_FCT_Support_WIP.RunTime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NichiconJP_FCT_Support_WIP
{
    public enum ProgramType
    {
        None, ScanBarcode, Start
    }

    public partial class Main : Form
    {
        FileSystemWatcher fileWatcher;
        int pass = 0, ng = 0, total = 0;
        private bool changeFile;
        private string boardNo;
        private readonly object obj = new object();
        private bool run = false;
        string mainTitle = "HAM14W Ver.2.511";
        ProgramType prgType = ProgramType.None;
        private bool isFileBusy = false;
        private ErpService.ERPWebServiceSoapClient _erp_service = new ErpService.ERPWebServiceSoapClient();

        private PVSServiceReference.PVSWebServiceSoapClient _pvs_service = new PVSServiceReference.PVSWebServiceSoapClient();
        private void ShowMessage(string key, string str_status, string str_message)
        {         
            switch (key)
            {
                case "PASS":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = str_status; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.DarkGreen; }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.DarkGreen; }));

                    break;
                case "FAIL":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = str_status; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.DarkRed; }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.DarkRed; }));
                    break;
                case "WAIT":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = str_status; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.FromArgb(255, 128, 0); }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.FromArgb(255, 128, 0); }));

                    break;
                case "Default":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = @"[N\A]"; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.FromArgb(255, 128, 0); }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = "no results"; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.FromArgb(255, 128, 0); }));

                    break;
            }
        }
        private void ActiveProcess(string title)
        {
            Process[] processes = Process.GetProcesses();
            int windowHandle = 0;
            foreach (Process p in processes)
            {
                if (p.MainWindowTitle.Contains(title))
                {
                    windowHandle = (int)p.MainWindowHandle;
                    break;
                }
            }
            NativeWin32.SetForegroundWindow(windowHandle);
        }
        private void BinDataToControls()
        {
            if (Ultils.GetValueRegistryKey("Process") != null)
            {
                SystemSetting.processName = Ultils.GetValueRegistryKey("Process").ToString();
            }

            if (Ultils.GetValueRegistryKey("PathLogFCT") != null)
            {
                SystemSetting.Source = Ultils.GetValueRegistryKey("PathLogFCT").ToString();
            }

            if (Ultils.GetValueRegistryKey("CurrentStation") != null)
            {
                SystemSetting.stationNo = Ultils.GetValueRegistryKey("CurrentStation").ToString();
            }

            if (Ultils.GetValueRegistryKey("OutputLog") != null)
            {
                SystemSetting.outputLog = Ultils.GetValueRegistryKey("OutputLog").ToString();
            }

            if (Ultils.GetValueRegistryKey("FileExtension") != null)
            {
                SystemSetting.fileExtension = Ultils.GetValueRegistryKey("FileExtension").ToString();
            }

            if (Ultils.GetValueRegistryKey("Version") != null)
            {
                SystemSetting.fileExtension = Ultils.GetValueRegistryKey("Version").ToString();
            }
        }

        void EnabledButton(bool enable)
        {
            btnStart.Enabled = enable;
            btnStop.Enabled = !enable;
        }
        
        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                using (StreamReader reader = new StreamReader(e.FullPath))
                {
                    List<string> lines = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                    for (int i = lines.Count() - 1; i > 1; i--)
                    {
                        if (lines[i].ToString().Trim() == "999")
                        {
                            if (!IsFirstTimeCreateFile(lines)) // nếu ko phải lần đầu sinh ra file
                            {
                                if (i == lines.Count - 1) continue; // gặp 999 đầu thì bỏ qua                          
                                CreateFile(lines[i + 1]);
                                return;
                            }
                            else // nếu là lần đầu
                            {
                                CreateFile(lines[0]);
                                return;
                            }
                        }
                    }
                }                
            }
            catch (UnauthorizedAccessException)
            {
                return;
            }
            catch (DirectoryNotFoundException)
            {
                return;
            }

            catch (IOException)
            {
                return;
            }
            catch (Exception)
            {
                return;
            }                    
        }

        private void CreateFile(string line)
        {
            List<string> contentLine = line.Split(',').ToList();
            var model = contentLine.First();
            var productId = contentLine.Last();
            if (Ultils.IsCreateFileLog(model, productId, "P", "FCT_MUR", DateTime.Now))
            {
                this.BeginInvoke(new Action(() =>
                {
                    pass++;
                    this.lblPASS.Text = pass.ToString();
                    this.lblNG.Text = ng.ToString();
                    this.lblTOTAL.Text = (pass + ng).ToString();
                    var status = $"TEST OK: {productId}";
                    ShowMessage("PASS", "PASS", status);
                }));
                _erp_service.SaveTestLog(new ErpService.TestLogEntity()
                {
                    BOARD_NO = productId,
                    PRODUCT_ID = model,
                    BOARD_STATE = 1,
                    STATION_NO = "FCT_MUR",
                    CLIENT_NAME = Environment.MachineName,
                    TEST_TIME = _pvs_service.GetDateTime(),
                    HOST_NAME = GetPhysicalMac()
                }) ;
                return;
            }
        }

        private string GetPhysicalMac()
        {
            try
            {
                // Get all network interfaces
                NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                // Iterate through each network interface
                foreach (NetworkInterface networkInterface in networkInterfaces)
                {
                    // Check if the interface is operational and not a loopback or tunnel
                    if (networkInterface.OperationalStatus == OperationalStatus.Up &&
                        networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        networkInterface.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                    {
                        // Get the physical address (MAC address) of the network interface
                        byte[] physicalAddressBytes = networkInterface.GetPhysicalAddress().GetAddressBytes();
                        string macAddress = BitConverter.ToString(physicalAddressBytes);

                        return macAddress;
                    }
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        private bool IsFirstTimeCreateFile(List<string> lines)
        {
            return lines.Where(w => w.ToString().Trim() == "999").Count() == 1;
        }

        private string GetFileName(string linetxt)
        {
            try
            {
                var list = linetxt.Split(',').ToList();
                var path = (list[1] + " " + list[4] + " " + list[5]).Replace('/','.').Replace(':','.');
                return path;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void ViewPassrate()
        {
            this.BeginInvoke(new Action(() =>
            {
                this.lblPASS.Text = pass.ToString();
                this.lblNG.Text = ng.ToString();
                this.lblTOTAL.Text = (pass + ng).ToString();
            }));
        }

        public Main()
        {
            InitializeComponent();
            BinDataToControls();
            lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();//RunTimeManager.GetRunningVersion();
        }

        private void lblConfigs_Click(object sender, EventArgs e)
        {
            new frmConfig().ShowDialog();
            BinDataToControls();
        }

        private void lblConfigs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {          
            try
            {
                BinDataToControls();
                var runingVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                var olversion = Ultils.GetValueRegistryKey("Version");
                if (runingVersion != olversion)
                {
                    MessageBox.Show("Vui lòng sử dụng phiên bản mới\nLiên hệ DX - 2266 nếu gặp khó khăn!");
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ShowMessage("Default", @"[N\A]", "no result");
            lblConfigs.Enabled = true;
            pass = 0;
            ng = 0;
            total = 0;
            this.BeginInvoke(new Action(() => { lblNG.Text = ng.ToString(); }));
            this.BeginInvoke(new Action(() => { lblPASS.Text = pass.ToString(); }));
            this.BeginInvoke(new Action(() => { lblTOTAL.Text = total.ToString(); }));
            EnabledButton(true);
            if (fileWatcher.EnableRaisingEvents == true)
            {
                fileWatcher.EnableRaisingEvents = false;
                fileWatcher.Dispose();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Ultils.GetValueRegistryKey("PathLogFCT");
                if (path == null)
                {
                    MessageBox.Show("Vui lòng cài đặt đường dẫn"); return;
                }
                EnabledButton(false);
                lblConfigs.Enabled = false;
                fileWatcher = new FileSystemWatcher();
                fileWatcher.IncludeSubdirectories = true;
                fileWatcher.Filter = "*.dat";
                fileWatcher.Path = path;
                tSource.Text = path;
                fileWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.CreationTime
                                     | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                //fileWatcher.Changed += new FileSystemEventHandler(OnFileChanged);
                fileWatcher.Changed += FileWatcher_Event;
                fileWatcher.EnableRaisingEvents = true;
                ShowMessage("WAIT", "Wait", "Waitting log ...");
            }
            catch (Exception)
            {
                throw;
            }          
        }

        int createcount = 0;
        private void FileWatcher_Event(object sender, FileSystemEventArgs e)
        {
            if (isFileBusy) 
            {
                Thread.Sleep(1000);
                isFileBusy = false;
            }
            if (!isFileBusy)
            {
                if (e.ChangeType == WatcherChangeTypes.Changed)
                {
                    isFileBusy = true;
                    try
                    {
                        using (StreamReader reader = new StreamReader(e.FullPath))
                        {
                            List<string> lines = new List<string>();
                            while (!reader.EndOfStream)
                            {
                                lines.Add(reader.ReadLine());
                            }
                            for (int i = lines.Count() - 1; i > 1; i--)
                            {
                                if (lines[i].ToString().Trim() == "999")
                                {
                                    if (!IsFirstTimeCreateFile(lines)) // nếu ko phải lần đầu sinh ra file
                                    {
                                        if (i == lines.Count - 1) continue; // gặp 999 đầu thì bỏ qua                          
                                        CreateFile(lines[i + 1]);
                                        isFileBusy = false;
                                        return;
                                    }
                                    else // nếu là lần đầu
                                    {
                                        CreateFile(lines[0]);
                                        isFileBusy = false;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        return;
                    }
                    catch (DirectoryNotFoundException)
                    {
                        return;
                    }

                    catch (IOException)
                    {
                        return;
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
            }              
        }
    }
}
