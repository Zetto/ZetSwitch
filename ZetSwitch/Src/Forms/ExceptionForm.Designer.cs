namespace ZetSwitch.Forms
{
	partial class ExceptionForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionForm));
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnMail = new System.Windows.Forms.LinkLabel();
			this.btnOk = new System.Windows.Forms.Button();
			this.textTrace = new System.Windows.Forms.RichTextBox();
			this.lblAttach = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new System.Drawing.Point(13, 13);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(310, 13);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "Ooops something is very wrong. Do not panic and contact us at:";
			// 
			// btnMail
			// 
			this.btnMail.AutoSize = true;
			this.btnMail.Location = new System.Drawing.Point(319, 13);
			this.btnMail.Name = "btnMail";
			this.btnMail.Size = new System.Drawing.Size(139, 13);
			this.btnMail.TabIndex = 1;
			this.btnMail.TabStop = true;
			this.btnMail.Text = "tomas.skarecky@gmail.com";
			this.btnMail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MailLinkClicked);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(231, 252);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.OkClick);
			// 
			// textTrace
			// 
			this.textTrace.Location = new System.Drawing.Point(20, 42);
			this.textTrace.Name = "textTrace";
			this.textTrace.ReadOnly = true;
			this.textTrace.Size = new System.Drawing.Size(494, 204);
			this.textTrace.TabIndex = 3;
			this.textTrace.Text = "";
			// 
			// lblAttach
			// 
			this.lblAttach.AutoSize = true;
			this.lblAttach.Location = new System.Drawing.Point(13, 26);
			this.lblAttach.Name = "lblAttach";
			this.lblAttach.Size = new System.Drawing.Size(191, 13);
			this.lblAttach.TabIndex = 4;
			this.lblAttach.Text = "Please attach the following information:";
			// 
			// ExceptionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(526, 280);
			this.Controls.Add(this.lblAttach);
			this.Controls.Add(this.textTrace);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnMail);
			this.Controls.Add(this.lblMessage);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(542, 318);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(542, 318);
			this.Name = "ExceptionForm";
			this.ShowInTaskbar = false;
			this.Text = "Error";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.LinkLabel btnMail;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.RichTextBox textTrace;
		private System.Windows.Forms.Label lblAttach;
	}
}