using System.Windows.Forms;

namespace ZetSwitch
{
    partial class ProfileForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileForm));
			this.CButton = new System.Windows.Forms.Button();
			this.OkButton = new System.Windows.Forms.Button();
			this.tabPageIP = new System.Windows.Forms.TabPage();
			this.ipPageView = new ZetSwitch.IPPageView();
			this.pageProxy = new System.Windows.Forms.TabPage();
			this.proxyPageView = new ZetSwitch.Forms.ProxyPage();
			this.profilePage = new System.Windows.Forms.TabPage();
			this.profilePageView = new ZetSwitch.Forms.ProfilePage();
			this.TabControl = new System.Windows.Forms.TabControl();
			this.tabPageIP.SuspendLayout();
			this.pageProxy.SuspendLayout();
			this.profilePage.SuspendLayout();
			this.TabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// CButton
			// 
			this.CButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CButton.Location = new System.Drawing.Point(349, 313);
			this.CButton.Name = "CButton";
			this.CButton.Size = new System.Drawing.Size(75, 23);
			this.CButton.TabIndex = 40;
			this.CButton.Text = "Cancel";
			this.CButton.UseVisualStyleBackColor = true;
			this.CButton.Click += new System.EventHandler(this.CButtonClick);
			// 
			// OkButton
			// 
			this.OkButton.Location = new System.Drawing.Point(268, 313);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 23);
			this.OkButton.TabIndex = 39;
			this.OkButton.Text = "Ok";
			this.OkButton.UseVisualStyleBackColor = true;
			this.OkButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// tabPageIP
			// 
			this.tabPageIP.Controls.Add(this.ipPageView);
			this.tabPageIP.Location = new System.Drawing.Point(4, 22);
			this.tabPageIP.Name = "tabPageIP";
			this.tabPageIP.Size = new System.Drawing.Size(433, 279);
			this.tabPageIP.TabIndex = 3;
			this.tabPageIP.Text = "IP";
			this.tabPageIP.UseVisualStyleBackColor = true;
			// 
			// ipPageView
			// 
			this.ipPageView.Location = new System.Drawing.Point(3, 3);
			this.ipPageView.Name = "ipPageView";
			this.ipPageView.Size = new System.Drawing.Size(427, 270);
			this.ipPageView.TabIndex = 1;
			// 
			// pageProxy
			// 
			this.pageProxy.Controls.Add(this.proxyPageView);
			this.pageProxy.Location = new System.Drawing.Point(4, 22);
			this.pageProxy.Name = "pageProxy";
			this.pageProxy.Padding = new System.Windows.Forms.Padding(3);
			this.pageProxy.Size = new System.Drawing.Size(433, 279);
			this.pageProxy.TabIndex = 1;
			this.pageProxy.Text = "Proxy";
			this.pageProxy.UseVisualStyleBackColor = true;
			// 
			// proxyPageView
			// 
			this.proxyPageView.Location = new System.Drawing.Point(6, 6);
			this.proxyPageView.Name = "proxyPageView";
			this.proxyPageView.Size = new System.Drawing.Size(427, 232);
			this.proxyPageView.TabIndex = 0;
			// 
			// profilePage
			// 
			this.profilePage.Controls.Add(this.profilePageView);
			this.profilePage.Location = new System.Drawing.Point(4, 22);
			this.profilePage.Name = "profilePage";
			this.profilePage.Size = new System.Drawing.Size(433, 279);
			this.profilePage.TabIndex = 2;
			this.profilePage.Text = "Profile";
			this.profilePage.UseVisualStyleBackColor = true;
			// 
			// profilePageView
			// 
			this.profilePageView.Location = new System.Drawing.Point(4, 4);
			this.profilePageView.Name = "profilePageView";
			this.profilePageView.Size = new System.Drawing.Size(286, 212);
			this.profilePageView.TabIndex = 0;
			// 
			// TabControl
			// 
			this.TabControl.Controls.Add(this.profilePage);
			this.TabControl.Controls.Add(this.tabPageIP);
			this.TabControl.Controls.Add(this.pageProxy);
			this.TabControl.Location = new System.Drawing.Point(5, 2);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(441, 305);
			this.TabControl.TabIndex = 42;
			// 
			// ProfileForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CButton;
			this.ClientSize = new System.Drawing.Size(455, 338);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.CButton);
			this.Controls.Add(this.TabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProfileForm";
			this.Text = "Profile settings";
			this.tabPageIP.ResumeLayout(false);
			this.pageProxy.ResumeLayout(false);
			this.profilePage.ResumeLayout(false);
			this.TabControl.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        
        #endregion

		private System.Windows.Forms.Button CButton;
		private System.Windows.Forms.Button OkButton;
		private TabPage tabPageIP;
		private IPPageView ipPageView;
		private TabPage pageProxy;
		private Forms.ProxyPage proxyPageView;
		private TabPage profilePage;
		private Forms.ProfilePage profilePageView;
		private TabControl TabControl;
    }
}