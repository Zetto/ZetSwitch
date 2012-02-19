namespace ZetSwitch
{
    partial class ItemConfig
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemConfig));
			this.label6 = new System.Windows.Forms.Label();
			this.TextBoxName = new System.Windows.Forms.TextBox();
			this.Picture = new System.Windows.Forms.PictureBox();
			this.ButtonPictureChange = new System.Windows.Forms.Button();
			this.CButton = new System.Windows.Forms.Button();
			this.OkButton = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ListBoxInterfaces = new ZetSwitch.InterfaceListBox();
			this.TabControl = new System.Windows.Forms.TabControl();
			this.tabPageIP = new ZetSwitch.IPPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.DNSDHCPManual = new System.Windows.Forms.RadioButton();
			this.DNSDHCPAuto = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.IpDNS2 = new IPAddressControlLib.IPAddressControl();
			this.label4 = new System.Windows.Forms.Label();
			this.IpDNS1 = new IPAddressControlLib.IPAddressControl();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.IPDHCPManual = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.IPDHCPAuto = new System.Windows.Forms.RadioButton();
			this.IpGW = new IPAddressControlLib.IPAddressControl();
			this.label2 = new System.Windows.Forms.Label();
			this.IpMask = new IPAddressControlLib.IPAddressControl();
			this.label1 = new System.Windows.Forms.Label();
			this.IpIpAddress = new IPAddressControlLib.IPAddressControl();
			((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.TabControl.SuspendLayout();
			this.tabPageIP.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(38, 13);
			this.label6.TabIndex = 29;
			this.label6.Text = "Name:";
			// 
			// TextBoxName
			// 
			this.TextBoxName.Location = new System.Drawing.Point(50, 16);
			this.TextBoxName.Name = "TextBoxName";
			this.TextBoxName.Size = new System.Drawing.Size(145, 20);
			this.TextBoxName.TabIndex = 30;
			// 
			// Picture
			// 
			this.Picture.Location = new System.Drawing.Point(27, 256);
			this.Picture.Name = "Picture";
			this.Picture.Size = new System.Drawing.Size(53, 44);
			this.Picture.TabIndex = 31;
			this.Picture.TabStop = false;
			// 
			// ButtonPictureChange
			// 
			this.ButtonPictureChange.Location = new System.Drawing.Point(110, 267);
			this.ButtonPictureChange.Name = "ButtonPictureChange";
			this.ButtonPictureChange.Size = new System.Drawing.Size(75, 23);
			this.ButtonPictureChange.TabIndex = 32;
			this.ButtonPictureChange.Text = "Change";
			this.ButtonPictureChange.UseVisualStyleBackColor = true;
			this.ButtonPictureChange.Click += new System.EventHandler(this.ButtonPictureChange_Click);
			// 
			// CButton
			// 
			this.CButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CButton.Location = new System.Drawing.Point(248, 341);
			this.CButton.Name = "CButton";
			this.CButton.Size = new System.Drawing.Size(75, 23);
			this.CButton.TabIndex = 40;
			this.CButton.Text = "Cancel";
			this.CButton.UseVisualStyleBackColor = true;
			this.CButton.Click += new System.EventHandler(this.CButton_Click);
			// 
			// OkButton
			// 
			this.OkButton.Location = new System.Drawing.Point(163, 341);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 23);
			this.OkButton.TabIndex = 39;
			this.OkButton.Text = "Ok";
			this.OkButton.UseVisualStyleBackColor = true;
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(9, 50);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(107, 13);
			this.label7.TabIndex = 41;
			this.label7.Text = "Choose connections:";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.TextBoxName);
			this.groupBox3.Controls.Add(this.ListBoxInterfaces);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.Picture);
			this.groupBox3.Controls.Add(this.ButtonPictureChange);
			this.groupBox3.Location = new System.Drawing.Point(4, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(212, 318);
			this.groupBox3.TabIndex = 43;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Profile";
			// 
			// ListBoxInterfaces
			// 
			this.ListBoxInterfaces.FormattingEnabled = true;
			this.ListBoxInterfaces.Location = new System.Drawing.Point(12, 66);
			this.ListBoxInterfaces.Name = "ListBoxInterfaces";
			this.ListBoxInterfaces.Size = new System.Drawing.Size(183, 184);
			this.ListBoxInterfaces.TabIndex = 28;
			this.ListBoxInterfaces.SelectedIndexChanged += new System.EventHandler(this.ListBoxInterfaces_SelectedIndexChanged);
			// 
			// TabControl
			// 
			this.TabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.TabControl.Controls.Add(this.tabPageIP);
			this.TabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.TabControl.Location = new System.Drawing.Point(222, 12);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(257, 318);
			this.TabControl.TabIndex = 42;
			this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
			// 
			// tabPageIP
			// 
			this.tabPageIP.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageIP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.tabPageIP.Controls.Add(this.groupBox2);
			this.tabPageIP.Controls.Add(this.groupBox1);
			this.tabPageIP.Location = new System.Drawing.Point(4, 25);
			this.tabPageIP.Name = "tabPageIP";
			this.tabPageIP.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageIP.Size = new System.Drawing.Size(249, 289);
			this.tabPageIP.TabIndex = 0;
			this.tabPageIP.Text = "IP";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.DNSDHCPManual);
			this.groupBox2.Controls.Add(this.DNSDHCPAuto);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.IpDNS2);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.IpDNS1);
			this.groupBox2.Location = new System.Drawing.Point(6, 179);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(231, 107);
			this.groupBox2.TabIndex = 35;
			this.groupBox2.TabStop = false;
			// 
			// DNSDHCPManual
			// 
			this.DNSDHCPManual.AutoSize = true;
			this.DNSDHCPManual.Location = new System.Drawing.Point(6, 23);
			this.DNSDHCPManual.Name = "DNSDHCPManual";
			this.DNSDHCPManual.Size = new System.Drawing.Size(215, 17);
			this.DNSDHCPManual.TabIndex = 40;
			this.DNSDHCPManual.TabStop = true;
			this.DNSDHCPManual.Text = "Use the following DNS server addresses";
			this.DNSDHCPManual.UseVisualStyleBackColor = true;
			// 
			// DNSDHCPAuto
			// 
			this.DNSDHCPAuto.AutoSize = true;
			this.DNSDHCPAuto.Location = new System.Drawing.Point(6, 0);
			this.DNSDHCPAuto.Name = "DNSDHCPAuto";
			this.DNSDHCPAuto.Size = new System.Drawing.Size(213, 17);
			this.DNSDHCPAuto.TabIndex = 39;
			this.DNSDHCPAuto.TabStop = true;
			this.DNSDHCPAuto.Text = "Obtain DNS server addres automatically";
			this.DNSDHCPAuto.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(4, 75);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(39, 13);
			this.label5.TabIndex = 38;
			this.label5.Text = "DNS2:";
			// 
			// IpDNS2
			// 
			this.IpDNS2.AllowInternalTab = false;
			this.IpDNS2.AutoHeight = true;
			this.IpDNS2.BackColor = System.Drawing.SystemColors.Window;
			this.IpDNS2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.IpDNS2.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.IpDNS2.Location = new System.Drawing.Point(87, 72);
			this.IpDNS2.MinimumSize = new System.Drawing.Size(87, 20);
			this.IpDNS2.Name = "IpDNS2";
			this.IpDNS2.ReadOnly = false;
			this.IpDNS2.Size = new System.Drawing.Size(132, 20);
			this.IpDNS2.TabIndex = 37;
			this.IpDNS2.Text = "...";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(4, 49);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 36;
			this.label4.Text = "DNS1:";
			// 
			// IpDNS1
			// 
			this.IpDNS1.AllowInternalTab = false;
			this.IpDNS1.AutoHeight = true;
			this.IpDNS1.BackColor = System.Drawing.SystemColors.Window;
			this.IpDNS1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.IpDNS1.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.IpDNS1.Location = new System.Drawing.Point(87, 46);
			this.IpDNS1.MinimumSize = new System.Drawing.Size(87, 20);
			this.IpDNS1.Name = "IpDNS1";
			this.IpDNS1.ReadOnly = false;
			this.IpDNS1.Size = new System.Drawing.Size(132, 20);
			this.IpDNS1.TabIndex = 35;
			this.IpDNS1.Text = "...";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.IPDHCPManual);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.IPDHCPAuto);
			this.groupBox1.Controls.Add(this.IpGW);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.IpMask);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.IpIpAddress);
			this.groupBox1.Location = new System.Drawing.Point(6, 29);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(231, 128);
			this.groupBox1.TabIndex = 34;
			this.groupBox1.TabStop = false;
			// 
			// IPDHCPManual
			// 
			this.IPDHCPManual.AutoSize = true;
			this.IPDHCPManual.Location = new System.Drawing.Point(6, 23);
			this.IPDHCPManual.Name = "IPDHCPManual";
			this.IPDHCPManual.Size = new System.Drawing.Size(170, 17);
			this.IPDHCPManual.TabIndex = 42;
			this.IPDHCPManual.TabStop = true;
			this.IPDHCPManual.Text = "Use the following IP addresses";
			this.IPDHCPManual.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 101);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 13);
			this.label3.TabIndex = 32;
			this.label3.Text = "Default gateway";
			// 
			// IPDHCPAuto
			// 
			this.IPDHCPAuto.AutoSize = true;
			this.IPDHCPAuto.Location = new System.Drawing.Point(6, 0);
			this.IPDHCPAuto.Name = "IPDHCPAuto";
			this.IPDHCPAuto.Size = new System.Drawing.Size(183, 17);
			this.IPDHCPAuto.TabIndex = 41;
			this.IPDHCPAuto.TabStop = true;
			this.IPDHCPAuto.Text = "Obtain an IP addres automatically";
			this.IPDHCPAuto.UseVisualStyleBackColor = true;
			// 
			// IpGW
			// 
			this.IpGW.AllowInternalTab = false;
			this.IpGW.AutoHeight = true;
			this.IpGW.BackColor = System.Drawing.SystemColors.Window;
			this.IpGW.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.IpGW.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.IpGW.Location = new System.Drawing.Point(90, 98);
			this.IpGW.MinimumSize = new System.Drawing.Size(87, 20);
			this.IpGW.Name = "IpGW";
			this.IpGW.ReadOnly = false;
			this.IpGW.Size = new System.Drawing.Size(129, 20);
			this.IpGW.TabIndex = 31;
			this.IpGW.Text = "...";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 13);
			this.label2.TabIndex = 30;
			this.label2.Text = "Subnet mask:";
			// 
			// IpMask
			// 
			this.IpMask.AllowInternalTab = false;
			this.IpMask.AutoHeight = true;
			this.IpMask.BackColor = System.Drawing.SystemColors.Window;
			this.IpMask.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.IpMask.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.IpMask.Location = new System.Drawing.Point(90, 72);
			this.IpMask.MinimumSize = new System.Drawing.Size(87, 20);
			this.IpMask.Name = "IpMask";
			this.IpMask.ReadOnly = false;
			this.IpMask.Size = new System.Drawing.Size(129, 20);
			this.IpMask.TabIndex = 29;
			this.IpMask.Text = "...";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 28;
			this.label1.Text = "IP address:";
			// 
			// IpIpAddress
			// 
			this.IpIpAddress.AllowInternalTab = false;
			this.IpIpAddress.AutoHeight = true;
			this.IpIpAddress.BackColor = System.Drawing.SystemColors.Window;
			this.IpIpAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.IpIpAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.IpIpAddress.Location = new System.Drawing.Point(90, 46);
			this.IpIpAddress.MinimumSize = new System.Drawing.Size(87, 20);
			this.IpIpAddress.Name = "IpIpAddress";
			this.IpIpAddress.ReadOnly = false;
			this.IpIpAddress.Size = new System.Drawing.Size(129, 20);
			this.IpIpAddress.TabIndex = 27;
			this.IpIpAddress.Text = "...";
			// 
			// ItemConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CButton;
			this.ClientSize = new System.Drawing.Size(471, 369);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.TabControl);
			this.Controls.Add(this.CButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ItemConfig";
			this.Text = "Zet Switch";
			((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.TabControl.ResumeLayout(false);
			this.tabPageIP.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        
        #endregion

		private InterfaceListBox ListBoxInterfaces;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBoxName;
        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.Button ButtonPictureChange;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private IPAddressControlLib.IPAddressControl IpGW;
        private System.Windows.Forms.Label label2;
        private IPAddressControlLib.IPAddressControl IpMask;
        private System.Windows.Forms.Label label1;
        private IPAddressControlLib.IPAddressControl IpIpAddress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton IPDHCPManual;
        private System.Windows.Forms.RadioButton IPDHCPAuto;
        private System.Windows.Forms.RadioButton DNSDHCPManual;
        private System.Windows.Forms.RadioButton DNSDHCPAuto;
        private System.Windows.Forms.Label label5;
        private IPAddressControlLib.IPAddressControl IpDNS2;
        private System.Windows.Forms.Label label4;
        private IPAddressControlLib.IPAddressControl IpDNS1;
        private System.Windows.Forms.Button CButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl TabControl;
		private System.Windows.Forms.GroupBox groupBox3;
		private IPPage tabPageIP;
    }
}