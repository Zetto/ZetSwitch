namespace ZetSwitch {
	partial class IPPageView {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.IPDHCPAuto = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.DNSDHCPManual = new System.Windows.Forms.RadioButton();
			this.DNSDHCPAuto = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.IpDNS2 = new IPAddressControlLib.IPAddressControl();
			this.label4 = new System.Windows.Forms.Label();
			this.IpDNS1 = new IPAddressControlLib.IPAddressControl();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.IPDHCPManual = new System.Windows.Forms.RadioButton();
			this.IpGW = new IPAddressControlLib.IPAddressControl();
			this.label2 = new System.Windows.Forms.Label();
			this.IpMask = new IPAddressControlLib.IPAddressControl();
			this.label1 = new System.Windows.Forms.Label();
			this.IpIpAddress = new IPAddressControlLib.IPAddressControl();
			this.label3 = new System.Windows.Forms.Label();
			this.ListBoxInterfaces = new ZetSwitch.InterfaceListBox();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
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
			this.IPDHCPAuto.CheckedChanged += new System.EventHandler(this.OnSelectionChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.DNSDHCPManual);
			this.groupBox2.Controls.Add(this.DNSDHCPAuto);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.IpDNS2);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.IpDNS1);
			this.groupBox2.Location = new System.Drawing.Point(192, 154);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(231, 107);
			this.groupBox2.TabIndex = 37;
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
			this.DNSDHCPManual.CheckedChanged += new System.EventHandler(this.OnSelectionChanged);
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
			this.DNSDHCPAuto.CheckedChanged += new System.EventHandler(this.OnSelectionChanged);
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
			this.IpDNS2.TextChanged += new System.EventHandler(this.OnDataChanged);
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
			this.IpDNS1.TextChanged += new System.EventHandler(this.OnDataChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.IPDHCPManual);
			this.groupBox1.Controls.Add(this.IPDHCPAuto);
			this.groupBox1.Controls.Add(this.IpGW);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.IpMask);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.IpIpAddress);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(192, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(231, 128);
			this.groupBox1.TabIndex = 36;
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
			this.IPDHCPManual.CheckedChanged += new System.EventHandler(this.OnSelectionChanged);
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
			this.IpGW.TextChanged += new System.EventHandler(this.OnDataChanged);
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
			this.IpMask.TextChanged += new System.EventHandler(this.OnDataChanged);
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
			this.IpIpAddress.TextChanged += new System.EventHandler(this.OnDataChanged);
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
			// ListBoxInterfaces
			// 
			this.ListBoxInterfaces.FormattingEnabled = true;
			this.ListBoxInterfaces.Location = new System.Drawing.Point(3, 26);
			this.ListBoxInterfaces.Name = "ListBoxInterfaces";
			this.ListBoxInterfaces.Size = new System.Drawing.Size(183, 184);
			this.ListBoxInterfaces.TabIndex = 42;
			this.ListBoxInterfaces.SelectedIndexChanged += new System.EventHandler(this.ListBoxInterfacesSelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(0, 10);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(107, 13);
			this.label7.TabIndex = 43;
			this.label7.Text = "Choose connections:";
			// 
			// IPPageView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ListBoxInterfaces);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "IPPageView";
			this.Size = new System.Drawing.Size(432, 264);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton IPDHCPAuto;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton DNSDHCPManual;
		private System.Windows.Forms.RadioButton DNSDHCPAuto;
		private System.Windows.Forms.Label label5;
		private IPAddressControlLib.IPAddressControl IpDNS2;
		private System.Windows.Forms.Label label4;
		private IPAddressControlLib.IPAddressControl IpDNS1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton IPDHCPManual;
		private System.Windows.Forms.Label label3;
		private IPAddressControlLib.IPAddressControl IpGW;
		private System.Windows.Forms.Label label2;
		private IPAddressControlLib.IPAddressControl IpMask;
		private System.Windows.Forms.Label label1;
		private IPAddressControlLib.IPAddressControl IpIpAddress;
		private InterfaceListBox ListBoxInterfaces;
		private System.Windows.Forms.Label label7;
	}
}
