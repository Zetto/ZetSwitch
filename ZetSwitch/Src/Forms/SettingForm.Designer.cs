namespace ZetSwitch
{
    partial class SettingForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
			this.checkBoxRunAuto = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.comboBoxLang = new System.Windows.Forms.ComboBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// checkBoxRunAuto
			// 
			this.checkBoxRunAuto.AutoSize = true;
			this.checkBoxRunAuto.Location = new System.Drawing.Point(13, 13);
			this.checkBoxRunAuto.Name = "checkBoxRunAuto";
			this.checkBoxRunAuto.Size = new System.Drawing.Size(183, 17);
			this.checkBoxRunAuto.TabIndex = 0;
			this.checkBoxRunAuto.Text = "Run automatically on system start";
			this.checkBoxRunAuto.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 58);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(58, 13);
			this.label6.TabIndex = 36;
			this.label6.Text = "Language:";
			// 
			// comboBoxLang
			// 
			this.comboBoxLang.FormattingEnabled = true;
			this.comboBoxLang.Items.AddRange(new object[] {
            "Česky",
            "English"});
			this.comboBoxLang.Location = new System.Drawing.Point(68, 55);
			this.comboBoxLang.Name = "comboBoxLang";
			this.comboBoxLang.Size = new System.Drawing.Size(121, 21);
			this.comboBoxLang.TabIndex = 35;
			this.comboBoxLang.Text = "Česky";
			this.comboBoxLang.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLangSelectedIndexChanged);
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(15, 94);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 37;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.ButtonOkClick);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(133, 94);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 38;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// Setting
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(235, 129);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.comboBoxLang);
			this.Controls.Add(this.checkBoxRunAuto);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Setting";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Settings";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxRunAuto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxLang;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}