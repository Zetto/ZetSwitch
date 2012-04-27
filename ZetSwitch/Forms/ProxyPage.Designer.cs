namespace ZetSwitch.Forms {
	partial class ProxyPage {
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbUseAll = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.SocksPort = new System.Windows.Forms.TextBox();
			this.Socks = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.FTPPort = new System.Windows.Forms.TextBox();
			this.FTP = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SSLPort = new System.Windows.Forms.TextBox();
			this.SSL = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.HTTPPort = new System.Windows.Forms.TextBox();
			this.HTTP = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.HomePage = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.lbBrowsers = new System.Windows.Forms.CheckedListBox();
			this.label10 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cbUseAll);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.SocksPort);
			this.groupBox1.Controls.Add(this.Socks);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.FTPPort);
			this.groupBox1.Controls.Add(this.FTP);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.SSLPort);
			this.groupBox1.Controls.Add(this.SSL);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.HTTPPort);
			this.groupBox1.Controls.Add(this.HTTP);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(192, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(233, 151);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Servers";
			// 
			// cbUseAll
			// 
			this.cbUseAll.AutoSize = true;
			this.cbUseAll.Location = new System.Drawing.Point(11, 45);
			this.cbUseAll.Name = "cbUseAll";
			this.cbUseAll.Size = new System.Drawing.Size(204, 17);
			this.cbUseAll.TabIndex = 17;
			this.cbUseAll.Text = "Use the same settings for all protocols";
			this.cbUseAll.UseVisualStyleBackColor = true;
			this.cbUseAll.CheckedChanged += new System.EventHandler(this.CbUseAllCheckedChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(175, 126);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(10, 13);
			this.label7.TabIndex = 16;
			this.label7.Text = ":";
			// 
			// SocksPort
			// 
			this.SocksPort.Location = new System.Drawing.Point(186, 122);
			this.SocksPort.Name = "SocksPort";
			this.SocksPort.Size = new System.Drawing.Size(38, 20);
			this.SocksPort.TabIndex = 15;
			// 
			// Socks
			// 
			this.Socks.Location = new System.Drawing.Point(49, 122);
			this.Socks.Name = "Socks";
			this.Socks.Size = new System.Drawing.Size(124, 20);
			this.Socks.TabIndex = 14;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(8, 126);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(40, 13);
			this.label8.TabIndex = 13;
			this.label8.Text = "Socks:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(175, 100);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(10, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = ":";
			// 
			// FTPPort
			// 
			this.FTPPort.Location = new System.Drawing.Point(186, 96);
			this.FTPPort.Name = "FTPPort";
			this.FTPPort.Size = new System.Drawing.Size(38, 20);
			this.FTPPort.TabIndex = 11;
			// 
			// FTP
			// 
			this.FTP.Location = new System.Drawing.Point(49, 96);
			this.FTP.Name = "FTP";
			this.FTP.Size = new System.Drawing.Size(124, 20);
			this.FTP.TabIndex = 10;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 100);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(30, 13);
			this.label6.TabIndex = 9;
			this.label6.Text = "FTP:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(175, 74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(10, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = ":";
			// 
			// SSLPort
			// 
			this.SSLPort.Location = new System.Drawing.Point(186, 70);
			this.SSLPort.Name = "SSLPort";
			this.SSLPort.Size = new System.Drawing.Size(38, 20);
			this.SSLPort.TabIndex = 7;
			// 
			// SSL
			// 
			this.SSL.Location = new System.Drawing.Point(49, 70);
			this.SSL.Name = "SSL";
			this.SSL.Size = new System.Drawing.Size(124, 20);
			this.SSL.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(30, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "SSL:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(175, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(10, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = ":";
			// 
			// HTTPPort
			// 
			this.HTTPPort.Location = new System.Drawing.Point(186, 19);
			this.HTTPPort.Name = "HTTPPort";
			this.HTTPPort.Size = new System.Drawing.Size(38, 20);
			this.HTTPPort.TabIndex = 3;
			this.HTTPPort.TextChanged += new System.EventHandler(this.HTTPPortTextChanged);
			// 
			// HTTP
			// 
			this.HTTP.Location = new System.Drawing.Point(49, 19);
			this.HTTP.Name = "HTTP";
			this.HTTP.Size = new System.Drawing.Size(124, 20);
			this.HTTP.TabIndex = 2;
			this.HTTP.TextChanged += new System.EventHandler(this.HTTPTextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "HTTP:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.HomePage);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Location = new System.Drawing.Point(191, 160);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(236, 65);
			this.groupBox2.TabIndex = 18;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Home page";
			// 
			// HomePage
			// 
			this.HomePage.Location = new System.Drawing.Point(50, 25);
			this.HomePage.Name = "HomePage";
			this.HomePage.Size = new System.Drawing.Size(175, 20);
			this.HomePage.TabIndex = 18;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(9, 28);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(32, 13);
			this.label9.TabIndex = 0;
			this.label9.Text = "URL:";
			// 
			// lbBrowsers
			// 
			this.lbBrowsers.FormattingEnabled = true;
			this.lbBrowsers.Location = new System.Drawing.Point(3, 26);
			this.lbBrowsers.Name = "lbBrowsers";
			this.lbBrowsers.Size = new System.Drawing.Size(183, 184);
			this.lbBrowsers.TabIndex = 19;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(3, 10);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(53, 13);
			this.label10.TabIndex = 44;
			this.label10.Text = "Browsers:";
			// 
			// ProxyPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label10);
			this.Controls.Add(this.lbBrowsers);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "ProxyPage";
			this.Size = new System.Drawing.Size(535, 235);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox HTTP;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox HTTPPort;
		private System.Windows.Forms.CheckBox cbUseAll;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox SocksPort;
		private System.Windows.Forms.TextBox Socks;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox FTPPort;
		private System.Windows.Forms.TextBox FTP;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox SSLPort;
		private System.Windows.Forms.TextBox SSL;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox HomePage;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckedListBox lbBrowsers;
		private System.Windows.Forms.Label label10;
	}
}
