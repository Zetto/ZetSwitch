/////////////////////////////////////////////////////////////////////////////
//
// ZetSwitch: Network manager
// Copyright (C) 2011 Tomas Skarecky
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
//
///////////////////////////////////////////////////////////////////////////// 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Collections;
using ZetSwitch.Network;

namespace ZetSwitch
{

    public partial class MainForm : Form, IMainView {
        
        ProfileManager profiles;

        public MainForm(ProfileManager profiles) {
            this.profiles = profiles;

            InitializeComponent();
            ResetLanguage();

            ReloadList();
         
            if (ListBoxItems.Items.Count > 0)
                ListBoxItems.SetSelected(0, true);
			ImgCollection.Instance.PathToImages = Application.StartupPath + Properties.Settings.Default.ImagesPath;
        }

		void onNotifyApplyClick(object sender, EventArgs e) {
			ToolStripItem item = sender as ToolStripItem;
			if (item == null || ApplyProfile == null)
				return;
			ApplyProfile(this, new ProfileEventArgs(item.Text));
		}
       
		private bool CreateMenuList() {
			contextMenuTray.Items.Clear();
			contextMenuTray.Items.Add(Language.GetText("ProfileNew"),null,New_Click);

			if (ListBoxItems.Items.Count != 0)
				contextMenuTray.Items.Add("-");

			foreach (string Text in ListBoxItems.Items) {
				contextMenuTray.Items.Add(Text,null,onNotifyApplyClick);
			}

			if (ListBoxItems.Items.Count != 0)
				contextMenuTray.Items.Add("-");
			contextMenuTray.Items.Add(Language.GetText("Exit"), null, exitToolStripMenuItem1_Click);

			return true;
		}

		public void ResetLanguage() {
			this.fileToolStripMenuItem1.Text = Language.GetText("MenuFile");
			this.newProfileToolStripMenuItem1.Text = Language.GetText("ProfileNew");
			this.deleteProfileToolStripMenuItem.Text = Language.GetText("ProfileDelete");
			this.exitToolStripMenuItem1.Text = Language.GetText("Exit");
			this.actionToolStripMenuItem1.Text = Language.GetText("MenuAction");
			this.applyProfileToolStripMenuItem1.Text = Language.GetText("ProfileApply");
			this.changeProfileToolStripMenuItem1.Text = Language.GetText("ProfileChange");
			this.helpToolStripMenuItem1.Text = Language.GetText("MenuHelp");
			this.aaboutToolStripMenuItem.Text = Language.GetText("MenuAbout");
			this.desktopShortcut.Text = Language.GetText("DesktopShortcut");
			this.toolStripButton1.Text = Language.GetText("Apply");
			this.toolStripButton2.Text = Language.GetText("New");
			this.toolStripButton3.Text = Language.GetText("Change");
			this.toolStripButton4.Text = Language.GetText("Delete");
			this.labelVersion.Text = Language.GetText("StatusBarVer") + Properties.Resources.Version;
			this.newProfileToolStripMenuItem.Text = Language.GetText("ProfileApply");
			this.createProfileToolStripMenuItem.Text = Language.GetText("ProfileNew");
			this.changeProfileToolStripMenuItem.Text = Language.GetText("ProfileChange");
			this.deleteProfileToolStripMenuItem1.Text = Language.GetText("ProfileDelete");
			this.NotifyIcon.Text = Language.GetText("TrayOpen");
			this.nastaveniToolStripMenuItem.Text = Language.GetText("Settings");

			return;
		}

        public void ReloadList() {
            ListBoxItems.Items.Clear();
            foreach (Profile profile in profiles.Profiles) {
                ListBoxItems.Items.Add(profile.Name);
            }
        }

        public void SetSelectByName(string Name) {
            int i = ListBoxItems.FindStringExact(Name);
            if (i < 0) {
                if (ListBoxItems.SelectedIndex < 0 && ListBoxItems.Items.Count > 0)
                    ListBoxItems.SetSelected(0, true);
                return;
            }
            else {
                if (!(ListBoxItems.SelectedIndex < 0))
                    ListBoxItems.SetSelected(ListBoxItems.SelectedIndex, false);
                ListBoxItems.SetSelected(i, true);
            }
        }

		public void GoToTray() {
			this.NotifyIcon.Visible = true;
			Hide();
		}

		public void ShowErrorMessage(string message) {
			MessageBox.Show(message, Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public void ShowInfoMessage(string message, string caption) {
			MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public bool AskQuestion(string message, string caption) {
			return (MessageBox.Show(message, caption, MessageBoxButtons.YesNo) == DialogResult.Yes);
		}

		public string GetSelectedProfile() {
			if (ListBoxItems.SelectedIndex < 0)
				return null;
			return ListBoxItems.SelectedItem.ToString();
		}

		public void closeView() {
			this.Close();
		}

		public bool AskToApplyProfile(string profile) {
			return MessageBox.Show(Language.GetText("UseProfile") + "'" + profile + "'?", Language.GetText("UseProfile"), MessageBoxButtons.YesNo,
									MessageBoxIcon.Question) == DialogResult.Yes;
		}

		#region handlers

		private void MainForm_SizeChange(object sender, EventArgs e)
		{
			ReloadList();
		}

		private void New_Click(object sender, EventArgs e) {
			NewProfileFunc();
		}

		private void Delete_Click(object sender, EventArgs e) {
			DeleteActualProfile();
		}

		//apply
		private void Apply_Click(object sender, EventArgs e) {
			ApplyProfileFunc();
		}

		private void Change_Click(object sender, EventArgs e) {
			ChangeProfileFunc();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			if (OpenAbout != null) {
				OpenAbout(this, null);
			}
		}

		private void DeleteActualProfile() {
			if (RemoveProfile != null)
				RemoveProfile(this, null);
		}

		private void NewProfileFunc() {
			if (NewProfile != null)
				NewProfile(this, null);
			return;
		}

		private void ChangeProfileFunc() {
			if (ChangeProfile != null)
				ChangeProfile(this, null);
		}

		private void ApplyProfileFunc() {
			if (ApplyProfile != null)
				ApplyProfile(this, null);
		}

		private void desktopShortcut_Click(object sender, EventArgs e) {
			if (CreateShortcut != null)
				CreateShortcut(this, null);
		}

		private void ExitFunc() {
			if (Exit != null)
				Exit(this, null);
			
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
			this.NotifyIcon.Dispose(); //todo: dat nekam do dza
			ExitFunc();
		}

		private void ListBoxItems_DoubleClick(object sender, EventArgs e) {
			ApplyProfileFunc();
		}

		private void exitToolStripMenuItem1_Click(object sender, EventArgs e) {
			Close();
		}


		private void ListBoxItems_MouseClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Right) {
				Point Loc = ListBoxItems.PointToScreen(new Point(e.X, e.Y));
				for (int i = 0; i < ListBoxItems.Items.Count; i++) {
					Rectangle Rec = ListBoxItems.GetItemRectangle(i);
					if (Rec.X <= e.X && (Rec.X + Rec.Width) >= e.X && Rec.Y <= e.Y && (Rec.Y + Rec.Height) >= e.Y) {
						ItemContextMenu.Items[0].Enabled = true;
						ItemContextMenu.Items[2].Enabled = true;
						ItemContextMenu.Items[3].Enabled = true;
						ItemContextMenu.Items[4].Enabled = true;
						ListBoxItems.ClearSelected();
						ListBoxItems.SetSelected(i, true);
						ItemContextMenu.Show(Loc);
						return;
					}
				}
				ItemContextMenu.Items[0].Enabled = false;
				ItemContextMenu.Items[3].Enabled = false;
				ItemContextMenu.Items[4].Enabled = false;
				ItemContextMenu.Show(Loc);
			}
			return;
		}


		private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
			if (OpenSettings != null)
				OpenSettings(this, null);
		}
		
		void NotifyIcon_DoubleClick(object sender, System.EventArgs e) {
			Show();
			WindowState = FormWindowState.Normal;
			this.NotifyIcon.Visible = false;
		}

		void NotifyIcon_Click(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Right)
			{
				if (CreateMenuList())
				{
					contextMenuTray.Visible = true;
				}
			}
		}

		void MainForm_Resize(object sender, System.EventArgs e) {
			if (FormWindowState.Minimized == WindowState)
			{
				GoToTray();
			}
		}
		
		void contextMenuTray_LostFocus(object sender, System.EventArgs e) {
			contextMenuTray.Hide();
		}

		private void ListBoxItems_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e) {
			Rectangle rc = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height - 5);

			if (e.Index < 0)
				return;

			ListBox List = (ListBox)sender;
			string str = (string)List.Items[e.Index];
			Profile profile = profiles.GetProfile(str);
			NetworkInterfaceSettings settings;

			StringFormat sf = null;
			Brush br = null, solid = null, selected = null, nonselected = null;
			Font titleFont = null, ipFont = null;

			try {
				sf = new StringFormat();
				solid = new SolidBrush(Color.Black);
				titleFont = new Font("Ariel", 12);
				ipFont = new Font("Ariel", 8);
				selected = new SolidBrush(Color.LightBlue);
				nonselected = new SolidBrush(Color.White);

				sf.LineAlignment = StringAlignment.Center;
				sf.Alignment = StringAlignment.Center;
		
				if (e.State == (DrawItemState.Selected | DrawItemState.Focus)) {
					e.Graphics.FillRectangle(selected, rc);
					e.Graphics.DrawString(str, titleFont, solid, rc, sf);
					br = new SolidBrush(Color.Gray);

				}
				else {
					e.Graphics.FillRectangle(nonselected, rc);
					e.Graphics.DrawString(str, titleFont, solid, rc, sf);
					br = new SolidBrush(Color.LightGray);

				}

				//draw ip address

				if (profile != null && profile.Connections.Count != 0) {
					settings = profile.Connections[0].Settings;
					sf.Alignment = StringAlignment.Far;
					if (settings.IsDHCP) {
						e.Graphics.DrawString("DHCP", ipFont, br, rc, sf);
					}
					else {
						e.Graphics.DrawString(settings.IP.ToString(), ipFont, br, rc, sf);
					}

				}
			}
			finally {
				if (sf!=null) sf.Dispose();
				if (br != null) br.Dispose();
				if (solid != null) solid.Dispose();
				if (nonselected != null) nonselected.Dispose();
				if (selected != null) selected.Dispose();
				if (titleFont != null) titleFont.Dispose();
				if (ipFont != null) ipFont.Dispose();
			}

			//draw icon
			if (profile.IconFile == null || profile.IconFile == "default" || profile.IconFile.Length == 0)
			{
				e.Graphics.DrawImage(Properties.Resources._default, new Point(1, e.Bounds.Y));
			}
			else
			{
				string fileName = profile.IconFile;
				try
				{
					Bitmap Bit = ImgCollection.Instance.GetImage(fileName);
					e.Graphics.DrawImage(Bit, new Point(1, e.Bounds.Y));
				}
				catch (Exception)
				{
					e.Graphics.DrawImage(Properties.Resources._default, new Point(1, e.Bounds.Y));
				}
			}
			return;
		}


		#endregion


		public event EventHandler<ProfileEventArgs> ApplyProfile;
		public event EventHandler RemoveProfile;
		public event EventHandler ChangeProfile;
		public event EventHandler NewProfile;
		public event EventHandler Exit;
		public event EventHandler OpenSettings;
		public event EventHandler OpenAbout;
		public event EventHandler CreateShortcut;
	}
}
