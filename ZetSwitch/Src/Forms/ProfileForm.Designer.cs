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
			this.tabPageIP = new System.Windows.Forms.TabPage();
			this.ipPageView = new ZetSwitch.IPPageView();
			((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.TabControl.SuspendLayout();
			this.tabPageIP.SuspendLayout();
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
			this.ButtonPictureChange.Click += new System.EventHandler(this.ButtonPictureChangeClick);
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
			this.CButton.Click += new System.EventHandler(this.CButtonClick);
			// 
			// OkButton
			// 
			this.OkButton.Location = new System.Drawing.Point(163, 341);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 23);
			this.OkButton.TabIndex = 39;
			this.OkButton.Text = "Ok";
			this.OkButton.UseVisualStyleBackColor = true;
			this.OkButton.Click += new System.EventHandler(this.OkButtonClick);
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
			this.ListBoxInterfaces.SelectedIndexChanged += new System.EventHandler(this.ListBoxInterfacesSelectedIndexChanged);
			// 
			// TabControl
			// 
			this.TabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.TabControl.Controls.Add(this.tabPageIP);
			this.TabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.TabControl.Location = new System.Drawing.Point(222, 17);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(257, 318);
			this.TabControl.TabIndex = 42;
			// 
			// tabPageIP
			// 
			this.tabPageIP.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageIP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.tabPageIP.Controls.Add(this.ipPageView);
			this.tabPageIP.Location = new System.Drawing.Point(4, 25);
			this.tabPageIP.Name = "tabPageIP";
			this.tabPageIP.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageIP.Size = new System.Drawing.Size(249, 289);
			this.tabPageIP.TabIndex = 0;
			this.tabPageIP.Text = "IP";
			// 
			// ipPageView
			// 
			this.ipPageView.Location = new System.Drawing.Point(7, 7);
			this.ipPageView.Name = "ipPageView";
			this.ipPageView.Size = new System.Drawing.Size(237, 264);
			this.ipPageView.TabIndex = 0;
			// 
			// ProfileForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CButton;
			this.ClientSize = new System.Drawing.Size(482, 368);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.CButton);
			this.Controls.Add(this.TabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProfileForm";
			this.Text = "Zet Switch";
			((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.TabControl.ResumeLayout(false);
			this.tabPageIP.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        
        #endregion

		private InterfaceListBox ListBoxInterfaces;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBoxName;
        private System.Windows.Forms.PictureBox Picture;
		private System.Windows.Forms.Button ButtonPictureChange;
        private System.Windows.Forms.Button CButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl TabControl;
		private System.Windows.Forms.GroupBox groupBox3;
		private TabPage tabPageIP;
		private IPPageView ipPageView;
    }
}