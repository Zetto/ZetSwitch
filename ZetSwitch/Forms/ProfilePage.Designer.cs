namespace ZetSwitch.Forms {
	partial class ProfilePage {
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
			this.TextBoxName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.Picture = new System.Windows.Forms.PictureBox();
			this.ButtonPictureChange = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
			this.SuspendLayout();
			// 
			// TextBoxName
			// 
			this.TextBoxName.Location = new System.Drawing.Point(48, 12);
			this.TextBoxName.Name = "TextBoxName";
			this.TextBoxName.Size = new System.Drawing.Size(145, 20);
			this.TextBoxName.TabIndex = 34;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 12);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(38, 13);
			this.label6.TabIndex = 33;
			this.label6.Text = "Name:";
			// 
			// Picture
			// 
			this.Picture.Location = new System.Drawing.Point(70, 38);
			this.Picture.Name = "Picture";
			this.Picture.Size = new System.Drawing.Size(53, 44);
			this.Picture.TabIndex = 35;
			this.Picture.TabStop = false;
			// 
			// ButtonPictureChange
			// 
			this.ButtonPictureChange.Location = new System.Drawing.Point(60, 88);
			this.ButtonPictureChange.Name = "ButtonPictureChange";
			this.ButtonPictureChange.Size = new System.Drawing.Size(75, 23);
			this.ButtonPictureChange.TabIndex = 36;
			this.ButtonPictureChange.Text = "Change";
			this.ButtonPictureChange.UseVisualStyleBackColor = true;
			this.ButtonPictureChange.Click += new System.EventHandler(this.ButtonPictureChangeClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 13);
			this.label1.TabIndex = 37;
			this.label1.Text = "Icon:";
			// 
			// ProfilePage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TextBoxName);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.Picture);
			this.Controls.Add(this.ButtonPictureChange);
			this.Name = "ProfilePage";
			this.Size = new System.Drawing.Size(286, 212);
			((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TextBoxName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.PictureBox Picture;
		private System.Windows.Forms.Button ButtonPictureChange;
		private System.Windows.Forms.Label label1;
	}
}
