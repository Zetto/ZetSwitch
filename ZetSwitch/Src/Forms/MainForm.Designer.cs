using System.Reflection;

namespace ZetSwitch
{
    partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.ListBoxItems = new System.Windows.Forms.ListBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.newProfileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.actionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.applyProfileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.changeProfileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.nastaveniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.aaboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.labelVersion = new System.Windows.Forms.ToolStripStatusLabel();
			this.ItemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.newProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteProfileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.desktopShortcut = new System.Windows.Forms.ToolStripMenuItem();
			this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.ItemContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// ListBoxItems
			// 
			this.ListBoxItems.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.ListBoxItems.FormattingEnabled = true;
			this.ListBoxItems.ItemHeight = 45;
			this.ListBoxItems.Location = new System.Drawing.Point(0, 66);
			this.ListBoxItems.MinimumSize = new System.Drawing.Size(228, 139);
			this.ListBoxItems.Name = "ListBoxItems";
			this.ListBoxItems.Size = new System.Drawing.Size(340, 193);
			this.ListBoxItems.TabIndex = 15;
			this.ListBoxItems.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBoxItems_DrawItem);
			this.ListBoxItems.DoubleClick += new System.EventHandler(this.ListBoxItems_DoubleClick);
			this.ListBoxItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListBoxItems_MouseClick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.actionToolStripMenuItem1,
            this.helpToolStripMenuItem1});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(340, 24);
			this.menuStrip1.TabIndex = 18;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem1
			// 
			this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProfileToolStripMenuItem1,
            this.deleteProfileToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem1});
			this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
			this.fileToolStripMenuItem1.Size = new System.Drawing.Size(57, 20);
			this.fileToolStripMenuItem1.Text = "Soubor";
			// 
			// newProfileToolStripMenuItem1
			// 
			this.newProfileToolStripMenuItem1.Image = global::ZetSwitch.Properties.Resources.add;
			this.newProfileToolStripMenuItem1.Name = "newProfileToolStripMenuItem1";
			this.newProfileToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newProfileToolStripMenuItem1.Size = new System.Drawing.Size(185, 22);
			this.newProfileToolStripMenuItem1.Text = "New profile";
			this.newProfileToolStripMenuItem1.Click += new System.EventHandler(this.New_Click);
			// 
			// deleteProfileToolStripMenuItem
			// 
			this.deleteProfileToolStripMenuItem.Image = global::ZetSwitch.Properties.Resources.trash;
			this.deleteProfileToolStripMenuItem.Name = "deleteProfileToolStripMenuItem";
			this.deleteProfileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.deleteProfileToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.deleteProfileToolStripMenuItem.Text = "Delete profile";
			this.deleteProfileToolStripMenuItem.Click += new System.EventHandler(this.Delete_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(182, 6);
			// 
			// exitToolStripMenuItem1
			// 
			this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
			this.exitToolStripMenuItem1.Size = new System.Drawing.Size(185, 22);
			this.exitToolStripMenuItem1.Text = "Exit";
			this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
			// 
			// actionToolStripMenuItem1
			// 
			this.actionToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applyProfileToolStripMenuItem1,
            this.changeProfileToolStripMenuItem1,
            this.toolStripSeparator6,
            this.nastaveniToolStripMenuItem});
			this.actionToolStripMenuItem1.Name = "actionToolStripMenuItem1";
			this.actionToolStripMenuItem1.Size = new System.Drawing.Size(59, 20);
			this.actionToolStripMenuItem1.Text = "Actions";
			// 
			// applyProfileToolStripMenuItem1
			// 
			this.applyProfileToolStripMenuItem1.Image = global::ZetSwitch.Properties.Resources.accept;
			this.applyProfileToolStripMenuItem1.Name = "applyProfileToolStripMenuItem1";
			this.applyProfileToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.applyProfileToolStripMenuItem1.Text = "Use profile";
			this.applyProfileToolStripMenuItem1.Click += new System.EventHandler(this.Apply_Click);
			// 
			// changeProfileToolStripMenuItem1
			// 
			this.changeProfileToolStripMenuItem1.Image = global::ZetSwitch.Properties.Resources.change;
			this.changeProfileToolStripMenuItem1.Name = "changeProfileToolStripMenuItem1";
			this.changeProfileToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.changeProfileToolStripMenuItem1.Text = "Change profile";
			this.changeProfileToolStripMenuItem1.Click += new System.EventHandler(this.Change_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
			// 
			// nastaveniToolStripMenuItem
			// 
			this.nastaveniToolStripMenuItem.Name = "nastaveniToolStripMenuItem";
			this.nastaveniToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.nastaveniToolStripMenuItem.Text = "Settings";
			this.nastaveniToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem1
			// 
			this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aaboutToolStripMenuItem});
			this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
			this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem1.Text = "Help";
			// 
			// aaboutToolStripMenuItem
			// 
			this.aaboutToolStripMenuItem.Name = "aaboutToolStripMenuItem";
			this.aaboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.aaboutToolStripMenuItem.Text = "About";
			this.aaboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripButton3,
            this.toolStripSeparator4,
            this.toolStripButton4});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(340, 39);
			this.toolStrip1.TabIndex = 19;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = global::ZetSwitch.Properties.Resources.accept;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(76, 36);
			this.toolStripButton1.Text = "Použít";
			this.toolStripButton1.Click += new System.EventHandler(this.Apply_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.Image = global::ZetSwitch.Properties.Resources.add;
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(74, 36);
			this.toolStripButton2.Text = "Přidat";
			this.toolStripButton2.Click += new System.EventHandler(this.New_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.Image = global::ZetSwitch.Properties.Resources.change;
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(81, 36);
			this.toolStripButton3.Text = "Změnit";
			this.toolStripButton3.Click += new System.EventHandler(this.Change_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
			// 
			// toolStripButton4
			// 
			this.toolStripButton4.Image = global::ZetSwitch.Properties.Resources.trash;
			this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new System.Drawing.Size(81, 36);
			this.toolStripButton4.Text = "Smazat";
			this.toolStripButton4.Click += new System.EventHandler(this.Delete_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.AllowMerge = false;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelVersion});
			this.statusStrip1.Location = new System.Drawing.Point(0, 255);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.statusStrip1.Size = new System.Drawing.Size(340, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 20;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// labelVersion
			// 
			this.labelVersion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.labelVersion.Size = new System.Drawing.Size(121, 17);
			this.labelVersion.Text = "Zet Switch version 0.3";
			// 
			// ItemContextMenu
			// 
			this.ItemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProfileToolStripMenuItem,
            this.toolStripSeparator7,
            this.createProfileToolStripMenuItem,
            this.changeProfileToolStripMenuItem,
            this.deleteProfileToolStripMenuItem1,
            this.toolStripSeparator5,
            this.desktopShortcut});
			this.ItemContextMenu.Name = "ItemContextMenu";
			this.ItemContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.ItemContextMenu.Size = new System.Drawing.Size(170, 148);
			// 
			// newProfileToolStripMenuItem
			// 
			this.newProfileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.newProfileToolStripMenuItem.Image = global::ZetSwitch.Properties.Resources.accept;
			this.newProfileToolStripMenuItem.Name = "newProfileToolStripMenuItem";
			this.newProfileToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.newProfileToolStripMenuItem.Text = "Použij Profil";
			this.newProfileToolStripMenuItem.Click += new System.EventHandler(this.Apply_Click);
			// 
			// createProfileToolStripMenuItem
			// 
			this.createProfileToolStripMenuItem.Image = global::ZetSwitch.Properties.Resources.add;
			this.createProfileToolStripMenuItem.Name = "createProfileToolStripMenuItem";
			this.createProfileToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.createProfileToolStripMenuItem.Text = "Přidat Profil";
			this.createProfileToolStripMenuItem.Click += new System.EventHandler(this.New_Click);
			// 
			// changeProfileToolStripMenuItem
			// 
			this.changeProfileToolStripMenuItem.Image = global::ZetSwitch.Properties.Resources.change;
			this.changeProfileToolStripMenuItem.Name = "changeProfileToolStripMenuItem";
			this.changeProfileToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
			this.changeProfileToolStripMenuItem.Text = "Změnit Profil";
			this.changeProfileToolStripMenuItem.Click += new System.EventHandler(this.Change_Click);
			// 
			// deleteProfileToolStripMenuItem1
			// 
			this.deleteProfileToolStripMenuItem1.Image = global::ZetSwitch.Properties.Resources.trash;
			this.deleteProfileToolStripMenuItem1.Name = "deleteProfileToolStripMenuItem1";
			this.deleteProfileToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
			this.deleteProfileToolStripMenuItem1.Text = "Smazat Profil";
			this.deleteProfileToolStripMenuItem1.Click += new System.EventHandler(this.Delete_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(166, 6);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(166, 6);
			// 
			// desktopShortcut
			// 
			this.desktopShortcut.Name = "desktopShortcut";
			this.desktopShortcut.Size = new System.Drawing.Size(169, 22);
			this.desktopShortcut.Text = "Zástupce na ploše";
			this.desktopShortcut.Click += new System.EventHandler(this.desktopShortcut_Click);
			// 
			// NotifyIcon
			// 
			this.NotifyIcon.ContextMenuStrip = this.contextMenuTray;
			this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
			this.NotifyIcon.Text = "Open Zet Switch";
			this.NotifyIcon.DoubleClick += new System.EventHandler(this.NotifyIcon_DoubleClick);
			this.NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_Click);
			// 
			// contextMenuTray
			// 
			this.contextMenuTray.Name = "contextMenuTray";
			this.contextMenuTray.Size = new System.Drawing.Size(61, 4);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(340, 277);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.ListBoxItems);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(345, 195);
			this.Name = "MainForm";
			this.Text = "Zet Switch";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.SizeChanged += new System.EventHandler(this.MainForm_SizeChange);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ItemContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }
       
        #endregion

        private System.Windows.Forms.ListBox ListBoxItems;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newProfileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem applyProfileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem changeProfileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aaboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelVersion;
        private System.Windows.Forms.ContextMenuStrip ItemContextMenu;
        private System.Windows.Forms.ToolStripMenuItem newProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem createProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteProfileToolStripMenuItem1;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuTray;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem nastaveniToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem desktopShortcut;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    }
}

