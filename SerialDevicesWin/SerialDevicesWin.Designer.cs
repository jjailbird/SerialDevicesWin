namespace SerialDevicesWin
{
    partial class SerialDevicesWinMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerialDevicesWinMain));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.dtpPortChecker = new System.Windows.Forms.Button();
            this.dtpPortList = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.aisPortChecker = new System.Windows.Forms.Button();
            this.aisPortList = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.msrPortChecker = new System.Windows.Forms.Button();
            this.msrPortList = new System.Windows.Forms.ComboBox();
            this.lblClose = new System.Windows.Forms.Label();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblBtnX = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtLogBox = new System.Windows.Forms.TextBox();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.contextMenuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnTest);
            this.groupBox4.Controls.Add(this.dtpPortChecker);
            this.groupBox4.Controls.Add(this.dtpPortList);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(17, 51);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(172, 76);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Thermal Printer Port";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(28, 47);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(135, 23);
            this.btnTest.TabIndex = 10;
            this.btnTest.Text = "Print Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // dtpPortChecker
            // 
            this.dtpPortChecker.BackColor = System.Drawing.Color.DimGray;
            this.dtpPortChecker.Location = new System.Drawing.Point(5, 19);
            this.dtpPortChecker.Name = "dtpPortChecker";
            this.dtpPortChecker.Size = new System.Drawing.Size(22, 22);
            this.dtpPortChecker.TabIndex = 9;
            this.dtpPortChecker.UseVisualStyleBackColor = false;
            this.dtpPortChecker.Click += new System.EventHandler(this.dtpPortChecker_Click);
            // 
            // dtpPortList
            // 
            this.dtpPortList.FormattingEnabled = true;
            this.dtpPortList.Location = new System.Drawing.Point(28, 20);
            this.dtpPortList.Name = "dtpPortList";
            this.dtpPortList.Size = new System.Drawing.Size(135, 20);
            this.dtpPortList.TabIndex = 8;
            this.dtpPortList.SelectedIndexChanged += new System.EventHandler(this.dtpPortList_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.aisPortChecker);
            this.groupBox3.Controls.Add(this.aisPortList);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(373, 51);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(172, 52);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Barcode Scanner Port";
            // 
            // aisPortChecker
            // 
            this.aisPortChecker.BackColor = System.Drawing.Color.DimGray;
            this.aisPortChecker.Enabled = false;
            this.aisPortChecker.Location = new System.Drawing.Point(4, 19);
            this.aisPortChecker.Name = "aisPortChecker";
            this.aisPortChecker.Size = new System.Drawing.Size(22, 22);
            this.aisPortChecker.TabIndex = 10;
            this.aisPortChecker.UseVisualStyleBackColor = false;
            // 
            // aisPortList
            // 
            this.aisPortList.Enabled = false;
            this.aisPortList.FormattingEnabled = true;
            this.aisPortList.Location = new System.Drawing.Point(28, 20);
            this.aisPortList.Name = "aisPortList";
            this.aisPortList.Size = new System.Drawing.Size(135, 20);
            this.aisPortList.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.msrPortChecker);
            this.groupBox1.Controls.Add(this.msrPortList);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(195, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 52);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MSR Port";
            // 
            // msrPortChecker
            // 
            this.msrPortChecker.BackColor = System.Drawing.Color.DimGray;
            this.msrPortChecker.Enabled = false;
            this.msrPortChecker.Location = new System.Drawing.Point(5, 19);
            this.msrPortChecker.Name = "msrPortChecker";
            this.msrPortChecker.Size = new System.Drawing.Size(22, 22);
            this.msrPortChecker.TabIndex = 9;
            this.msrPortChecker.UseVisualStyleBackColor = false;
            // 
            // msrPortList
            // 
            this.msrPortList.Enabled = false;
            this.msrPortList.FormattingEnabled = true;
            this.msrPortList.Location = new System.Drawing.Point(28, 20);
            this.msrPortList.Name = "msrPortList";
            this.msrPortList.Size = new System.Drawing.Size(135, 20);
            this.msrPortList.TabIndex = 8;
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClose.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.lblClose.Location = new System.Drawing.Point(534, 6);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(19, 22);
            this.lblClose.TabIndex = 14;
            this.lblClose.Text = "x";
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.White;
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Controls.Add(this.lblBtnX);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(564, 28);
            this.headerPanel.TabIndex = 15;
            this.headerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.headerPanel_MouseDown);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.lblTitle.Location = new System.Drawing.Point(13, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(206, 22);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Serial Devices Control";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            // 
            // lblBtnX
            // 
            this.lblBtnX.AutoSize = true;
            this.lblBtnX.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBtnX.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBtnX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.lblBtnX.Location = new System.Drawing.Point(534, 0);
            this.lblBtnX.Name = "lblBtnX";
            this.lblBtnX.Size = new System.Drawing.Size(19, 22);
            this.lblBtnX.TabIndex = 1;
            this.lblBtnX.Text = "x";
            this.lblBtnX.Click += new System.EventHandler(this.lblBtnX_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.titleLabel.Location = new System.Drawing.Point(15, 13);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(0, 22);
            this.titleLabel.TabIndex = 0;
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.ContextMenuStrip = this.contextMenuStripMain;
            this.notifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMain.Icon")));
            this.notifyIconMain.Text = "notifyIcon1";
            this.notifyIconMain.Visible = true;
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.hideToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(105, 70);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // txtLogBox
            // 
            this.txtLogBox.Location = new System.Drawing.Point(22, 133);
            this.txtLogBox.Multiline = true;
            this.txtLogBox.Name = "txtLogBox";
            this.txtLogBox.Size = new System.Drawing.Size(514, 72);
            this.txtLogBox.TabIndex = 16;
            // 
            // SerialDevicesWinMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(564, 273);
            this.Controls.Add(this.txtLogBox);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SerialDevicesWinMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.SerialDevicesWinMain_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contextMenuStripMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button dtpPortChecker;
        private System.Windows.Forms.ComboBox dtpPortList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button aisPortChecker;
        private System.Windows.Forms.ComboBox aisPortList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button msrPortChecker;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBtnX;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ComboBox msrPortList;
        private System.Windows.Forms.TextBox txtLogBox;
        private System.Windows.Forms.Button btnTest;
    }
}

