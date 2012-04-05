namespace ZetSwitch
{
    partial class WelcomeScreen
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeScreen));
			this.btnOk = new System.Windows.Forms.Button();
			this.checkBoxShowAgain = new System.Windows.Forms.CheckBox();
			this.comboBoxLang = new System.Windows.Forms.ComboBox();
			this.lblLanguage = new System.Windows.Forms.Label();
			this.lblEmail = new System.Windows.Forms.Label();
			this.lbEmail = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.lblAuthor = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOk.Location = new System.Drawing.Point(119, 121);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 12;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.OkClick);
			// 
			// checkBoxShowAgain
			// 
			this.checkBoxShowAgain.AutoSize = true;
			this.checkBoxShowAgain.Location = new System.Drawing.Point(16, 85);
			this.checkBoxShowAgain.Name = "checkBoxShowAgain";
			this.checkBoxShowAgain.Size = new System.Drawing.Size(131, 17);
			this.checkBoxShowAgain.TabIndex = 13;
			this.checkBoxShowAgain.Text = "Do not show next time";
			this.checkBoxShowAgain.UseVisualStyleBackColor = true;
			// 
			// comboBoxLang
			// 
			this.comboBoxLang.FormattingEnabled = true;
			this.comboBoxLang.Items.AddRange(new object[] {
            "Česky",
            "English"});
			this.comboBoxLang.Location = new System.Drawing.Point(223, 83);
			this.comboBoxLang.Name = "comboBoxLang";
			this.comboBoxLang.Size = new System.Drawing.Size(98, 21);
			this.comboBoxLang.TabIndex = 33;
			this.comboBoxLang.Text = "English";
			this.comboBoxLang.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLangSelectedIndexChanged);
			// 
			// lblLanguage
			// 
			this.lblLanguage.AutoSize = true;
			this.lblLanguage.Location = new System.Drawing.Point(162, 87);
			this.lblLanguage.Name = "lblLanguage";
			this.lblLanguage.Size = new System.Drawing.Size(58, 13);
			this.lblLanguage.TabIndex = 34;
			this.lblLanguage.Text = "Language:";
			this.lblLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblEmail
			// 
			this.lblEmail.AutoSize = true;
			this.lblEmail.Location = new System.Drawing.Point(13, 50);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(35, 13);
			this.lblEmail.TabIndex = 42;
			this.lblEmail.Text = "Email:";
			// 
			// lbEmail
			// 
			this.lbEmail.AutoSize = true;
			this.lbEmail.Location = new System.Drawing.Point(55, 49);
			this.lbEmail.Name = "lbEmail";
			this.lbEmail.Size = new System.Drawing.Size(139, 13);
			this.lbEmail.TabIndex = 41;
			this.lbEmail.TabStop = true;
			this.lbEmail.Text = "tomas.skarecky@gmail.com";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(54, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(87, 13);
			this.label1.TabIndex = 40;
			this.label1.Text = "Tomáš Škarecký";
			// 
			// lblAuthor
			// 
			this.lblAuthor.AutoSize = true;
			this.lblAuthor.Location = new System.Drawing.Point(13, 29);
			this.lblAuthor.Name = "lblAuthor";
			this.lblAuthor.Size = new System.Drawing.Size(41, 13);
			this.lblAuthor.TabIndex = 39;
			this.lblAuthor.Text = "Author:";
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(12, 9);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(82, 13);
			this.lblName.TabIndex = 38;
			this.lblName.Text = "Zet Switch v0.3";
			// 
			// pictureBox1
			// 
			this.pictureBox1.InitialImage = global::ZetSwitch.Properties.Resources.about;
			this.pictureBox1.Location = new System.Drawing.Point(254, 9);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(64, 64);
			this.pictureBox1.TabIndex = 37;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.WaitOnLoad = true;
			// 
			// WelcomeScreen
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(330, 148);
			this.Controls.Add(this.lblEmail);
			this.Controls.Add(this.lbEmail);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblAuthor);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.lblLanguage);
			this.Controls.Add(this.comboBoxLang);
			this.Controls.Add(this.checkBoxShowAgain);
			this.Controls.Add(this.btnOk);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WelcomeScreen";
			this.Text = "Vítejte";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        

        #endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.CheckBox checkBoxShowAgain;
        private System.Windows.Forms.ComboBox comboBoxLang;
		private System.Windows.Forms.Label lblLanguage;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.LinkLabel lbEmail;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblAuthor;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.PictureBox pictureBox1;

    }
}