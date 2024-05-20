namespace NichiconJP_FCT_Support_WIP
{
    partial class frmConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSaveChanged = new System.Windows.Forms.Button();
            this.lblRefresh = new System.Windows.Forms.Label();
            this.lblBrowseFCTLog = new System.Windows.Forms.Label();
            this.lblInputLog = new System.Windows.Forms.Label();
            this.txtOutputLog = new System.Windows.Forms.TextBox();
            this.txtCurrentStation = new System.Windows.Forms.TextBox();
            this.txtPathLogFCT = new System.Windows.Forms.TextBox();
            this.cboWindows = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comControl = new System.IO.Ports.SerialPort(this.components);
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSaveChanged);
            this.groupBox2.Controls.Add(this.lblRefresh);
            this.groupBox2.Controls.Add(this.lblBrowseFCTLog);
            this.groupBox2.Controls.Add(this.lblInputLog);
            this.groupBox2.Controls.Add(this.txtOutputLog);
            this.groupBox2.Controls.Add(this.txtCurrentStation);
            this.groupBox2.Controls.Add(this.txtPathLogFCT);
            this.groupBox2.Controls.Add(this.cboWindows);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 205);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            // 
            // btnSaveChanged
            // 
            this.btnSaveChanged.BackColor = System.Drawing.Color.Green;
            this.btnSaveChanged.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveChanged.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveChanged.ForeColor = System.Drawing.Color.White;
            this.btnSaveChanged.Image = global::NichiconJP_FCT_Support_WIP.Properties.Resources.Save;
            this.btnSaveChanged.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveChanged.Location = new System.Drawing.Point(210, 138);
            this.btnSaveChanged.Name = "btnSaveChanged";
            this.btnSaveChanged.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSaveChanged.Size = new System.Drawing.Size(118, 30);
            this.btnSaveChanged.TabIndex = 36;
            this.btnSaveChanged.Text = "Save change";
            this.btnSaveChanged.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveChanged.UseVisualStyleBackColor = false;
            this.btnSaveChanged.Click += new System.EventHandler(this.btnSaveChanged_Click);
            // 
            // lblRefresh
            // 
            this.lblRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRefresh.Image = global::NichiconJP_FCT_Support_WIP.Properties.Resources.refesh_16;
            this.lblRefresh.Location = new System.Drawing.Point(321, 20);
            this.lblRefresh.Name = "lblRefresh";
            this.lblRefresh.Size = new System.Drawing.Size(17, 21);
            this.lblRefresh.TabIndex = 27;
            this.lblRefresh.Click += new System.EventHandler(this.lblRefresh_Click);
            // 
            // lblBrowseFCTLog
            // 
            this.lblBrowseFCTLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBrowseFCTLog.Image = global::NichiconJP_FCT_Support_WIP.Properties.Resources.folder_saved_search_16;
            this.lblBrowseFCTLog.Location = new System.Drawing.Point(320, 47);
            this.lblBrowseFCTLog.Name = "lblBrowseFCTLog";
            this.lblBrowseFCTLog.Size = new System.Drawing.Size(18, 23);
            this.lblBrowseFCTLog.TabIndex = 25;
            this.lblBrowseFCTLog.Click += new System.EventHandler(this.lblBrowseFCTLog_Click);
            // 
            // lblInputLog
            // 
            this.lblInputLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblInputLog.Image = global::NichiconJP_FCT_Support_WIP.Properties.Resources.folder_saved_search_16;
            this.lblInputLog.Location = new System.Drawing.Point(320, 76);
            this.lblInputLog.Name = "lblInputLog";
            this.lblInputLog.Size = new System.Drawing.Size(18, 23);
            this.lblInputLog.TabIndex = 25;
            this.lblInputLog.Click += new System.EventHandler(this.lblInputLog_Click);
            // 
            // txtOutputLog
            // 
            this.txtOutputLog.Location = new System.Drawing.Point(98, 76);
            this.txtOutputLog.Name = "txtOutputLog";
            this.txtOutputLog.ReadOnly = true;
            this.txtOutputLog.Size = new System.Drawing.Size(216, 20);
            this.txtOutputLog.TabIndex = 24;
            // 
            // txtCurrentStation
            // 
            this.txtCurrentStation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCurrentStation.Enabled = false;
            this.txtCurrentStation.Location = new System.Drawing.Point(98, 104);
            this.txtCurrentStation.Name = "txtCurrentStation";
            this.txtCurrentStation.Size = new System.Drawing.Size(100, 20);
            this.txtCurrentStation.TabIndex = 23;
            this.txtCurrentStation.Text = "FCT_MUR";
            // 
            // txtPathLogFCT
            // 
            this.txtPathLogFCT.Location = new System.Drawing.Point(98, 50);
            this.txtPathLogFCT.Name = "txtPathLogFCT";
            this.txtPathLogFCT.ReadOnly = true;
            this.txtPathLogFCT.Size = new System.Drawing.Size(216, 20);
            this.txtPathLogFCT.TabIndex = 23;
            // 
            // cboWindows
            // 
            this.cboWindows.Enabled = false;
            this.cboWindows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWindows.FormattingEnabled = true;
            this.cboWindows.Location = new System.Drawing.Point(98, 19);
            this.cboWindows.Name = "cboWindows";
            this.cboWindows.Size = new System.Drawing.Size(216, 24);
            this.cboWindows.TabIndex = 22;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(49, 107);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(43, 13);
            this.label18.TabIndex = 21;
            this.label18.Text = "Station:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Target:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Source:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Process:";
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 205);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Config";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSaveChanged;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblRefresh;
        private System.Windows.Forms.Label lblBrowseFCTLog;
        private System.Windows.Forms.Label lblInputLog;
        private System.Windows.Forms.TextBox txtOutputLog;
        private System.Windows.Forms.TextBox txtCurrentStation;
        private System.Windows.Forms.TextBox txtPathLogFCT;
        private System.Windows.Forms.ComboBox cboWindows;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.IO.Ports.SerialPort comControl;
    }
}