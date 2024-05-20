using NichiconJP_FCT_Support_WIP.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NichiconJP_FCT_Support_WIP
{
    public partial class frmConfig : Form
    {
        private void GetTaskWindows()
        {
            // Get the desktopwindow handle
            int nDeshWndHandle = NativeWin32.GetDesktopWindow();
            // Get the first child window
            int nChildHandle = NativeWin32.GetWindow(nDeshWndHandle, NativeWin32.GW_CHILD);
            while (nChildHandle != 0)
            {
                //If the child window is this (SendKeys) application then ignore it.
                if (nChildHandle == this.Handle.ToInt32())
                {
                    nChildHandle = NativeWin32.GetWindow(nChildHandle, NativeWin32.GW_HWNDNEXT);
                }

                // Get only visible windows
                if (NativeWin32.IsWindowVisible(nChildHandle) != 0)
                {
                    StringBuilder sbTitle = new StringBuilder(1024);
                    // Read the Title bar text on the windows to put in combobox
                    NativeWin32.GetWindowText(nChildHandle, sbTitle, sbTitle.Capacity);
                    string sWinTitle = sbTitle.ToString();
                    {
                        if (sWinTitle.Length > 0)
                        {
                            cboWindows.Items.Add(sWinTitle);
                        }
                    }
                }
                // Look for the next child.
                nChildHandle = NativeWin32.GetWindow(nChildHandle, NativeWin32.GW_HWNDNEXT);
            }

            BinDataToControls();
        }
        private void BinDataToControls()
        {
            if (Ultils.GetValueRegistryKey("Process") != null)
            {
                cboWindows.Text = Ultils.GetValueRegistryKey("Process").ToString();
            }

            if (Ultils.GetValueRegistryKey("CurrentStation") != null)
            {
                txtCurrentStation.Text = Ultils.GetValueRegistryKey("CurrentStation").ToString();
            }

            if (Ultils.GetValueRegistryKey("OutputLog") != null)
            {
                txtOutputLog.Text = Ultils.GetValueRegistryKey("OutputLog").ToString();
            }
            if (Ultils.GetValueRegistryKey("PathLogFCT") != null)
            {
                txtPathLogFCT.Text = Ultils.GetValueRegistryKey("PathLogFCT").ToString();
            }                             
        }

        public frmConfig()
        {
            InitializeComponent();
            GetTaskWindows();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            BinDataToControls();
        }

        private void btnSaveChanged_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(cboWindows.Text))
            //{
            //    MessageBox.Show("Vui lòng chọn Process!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    cboWindows.Focus();
            //    return;
            //}
            if (!Directory.Exists(txtPathLogFCT.Text))
            {
                MessageBox.Show("Sai đường dẫn lưu Log FCT!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPathLogFCT.Focus();
                return;
            }
            if (!Directory.Exists(txtOutputLog.Text))
            {
                MessageBox.Show("Sai đường dẫn file FCT!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblInputLog.Focus();
                return;
            }
            //if (string.IsNullOrEmpty(txtBarcodeLength.Text))
            //{
            //    MessageBox.Show("Vui lòng nhập vào độ dài của barcode!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtBarcodeLength.Focus();
            //    return;
            //}

           // Ultils.WriteRegistry("Process", cboWindows.Text);
            Ultils.WriteRegistry("PathLogFCT", txtPathLogFCT.Text);
            Ultils.WriteRegistry("CurrentStation", txtCurrentStation.Text);
            Ultils.WriteRegistry("OutputLog", txtOutputLog.Text);
            MessageBox.Show("Save success!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void lblRefresh_Click(object sender, EventArgs e)
        {
            GetTaskWindows();
        }

        private void lblBrowseFCTLog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog outputLog = new FolderBrowserDialog();
            DialogResult open = outputLog.ShowDialog();
            if (open == DialogResult.OK)
            {
                txtPathLogFCT.Text = outputLog.SelectedPath;
                if (string.IsNullOrEmpty(txtPathLogFCT.Text))
                {
                    btnSaveChanged.Focus();
                }
                else
                {
                    txtPathLogFCT.Focus();
                }
            }
        }

        private void lblInputLog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog outputLog = new FolderBrowserDialog();
            DialogResult open = outputLog.ShowDialog();
            if (open == DialogResult.OK)
            {
                txtOutputLog.Text = outputLog.SelectedPath;
                if (string.IsNullOrEmpty(txtOutputLog.Text))
                {
                    btnSaveChanged.Focus();
                }
                else
                {
                    lblInputLog.Focus();
                }
            }
        }

    }
}
