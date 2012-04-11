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
using System.Drawing;
using System.Windows.Forms;
using ZetSwitchData;
using ZetSwitchData.Network;

namespace ZetSwitch
{

    public partial class MainForm : Form, IMainView {
    	readonly DataManager manager;

        public MainForm(DataManager manager) {
            this.manager = manager;

            InitializeComponent();
            ResetLanguage();

            ReloadList();
         
            if (ListBoxItems.Items.Count > 0)
                ListBoxItems.SetSelected(0, true);
        }

		void OnNotifyApplyClick(object sender, EventArgs e) {
			var item = sender as ToolStripItem;
			if (item == null || ApplyProfile == null)
				return;
			ApplyProfile(this, new ProfileEventArgs(item.Text));
		}
       
		private bool CreateMenuList() {
			contextMenuTray.Items.Clear();
			contextMenuTray.Items.Add(ClientServiceLocator.GetService<ILanguage>().GetText("ProfileNew"),null,NewClick);

			if (ListBoxItems.Items.Count != 0)
				contextMenuTray.Items.Add("-");

			foreach (string text in ListBoxItems.Items) {
				contextMenuTray.Items.Add(text,null,OnNotifyApplyClick);
			}

			if (ListBoxItems.Items.Count != 0)
				contextMenuTray.Items.Add("-");
			contextMenuTray.Items.Add(ClientServiceLocator.GetService<ILanguage>().GetText("Exit"), null, ExitToolStripMenuItem1Click);

			return true;
		}

		public void ResetLanguage() {
			fileToolStripMenuItem1.Text = ClientServiceLocator.GetService<ILanguage>().GetText("MenuFile");
			newProfileToolStripMenuItem1.Text = ClientServiceLocator.GetService<ILanguage>().GetText("ProfileNew");
			deleteProfileToolStripMenuItem.Text = ClientServiceLocator.GetService<ILanguage>().GetText("ProfileDelete");
			exitToolStripMenuItem1.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Exit");
			actionToolStripMenuItem1.Text = ClientServiceLocator.GetService<ILanguage>().GetText("MenuAction");
			applyProfileToolStripMenuItem1.Text = ClientServiceLocator.GetService<ILanguage>().GetText("ProfileApply");
			changeProfileToolStripMenuItem1.Text = ClientServiceLocator.GetService<ILanguage>().GetText("ProfileChange");
			helpToolStripMenuItem1.Text = ClientServiceLocator.GetService<ILanguage>().GetText("MenuHelp");
			aaboutToolStripMenuItem.Text = ClientServiceLocator.GetService<ILanguage>().GetText("MenuAbout");
			desktopShortcut.Text = ClientServiceLocator.GetService<ILanguage>().GetText("DesktopShortcut");
			toolStripButton1.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Apply");
			toolStripButton2.Text = ClientServiceLocator.GetService<ILanguage>().GetText("New");
			toolStripButton3.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Change");
			toolStripButton4.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Delete");
			labelVersion.Text = ClientServiceLocator.GetService<ILanguage>().GetText("StatusBarVer") + Properties.Resources.Version;
			newProfileToolStripMenuItem.Text = ClientServiceLocator.GetService<ILanguage>().GetText("ProfileApply");
			createProfileToolStripMenuItem.Text = ClientServiceLocator.GetService<ILanguage>().GetText("ProfileNew");
			changeProfileToolStripMenuItem.Text = ClientServiceLocator.GetService<ILanguage>().GetText("ProfileChange");
			deleteProfileToolStripMenuItem1.Text = ClientServiceLocator.GetService<ILanguage>().GetText("ProfileDelete");
			NotifyIcon.Text = ClientServiceLocator.GetService<ILanguage>().GetText("TrayOpen");
			nastaveniToolStripMenuItem.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Settings");
		}

        public void ReloadList() {
            ListBoxItems.Items.Clear();
            foreach (var profile in manager.Profiles) {
                ListBoxItems.Items.Add(profile.Name);
            }
        }

        public void SetSelectByName(string name) {
            var i = ListBoxItems.FindStringExact(name);
            if (i < 0) {
                if (ListBoxItems.SelectedIndex < 0 && ListBoxItems.Items.Count > 0)
                    ListBoxItems.SetSelected(0, true);
            }
            else {
                if (!(ListBoxItems.SelectedIndex < 0))
                    ListBoxItems.SetSelected(ListBoxItems.SelectedIndex, false);
                ListBoxItems.SetSelected(i, true);
            }
        }

		public void GoToTray() {
			NotifyIcon.Visible = true;
			Hide();
		}

		public void ShowErrorMessage(string message) {
			MessageBox.Show(message, ClientServiceLocator.GetService<ILanguage>().GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		public void CloseView() {
			Close();
		}

		public bool AskToApplyProfile(string profile) {
			return MessageBox.Show(ClientServiceLocator.GetService<ILanguage>().GetText("UseProfile") + "'" + profile + "'?", ClientServiceLocator.GetService<ILanguage>().GetText("UseProfile"), MessageBoxButtons.YesNo,
									MessageBoxIcon.Question) == DialogResult.Yes;
		}

		#region handlers

		private void MainFormSizeChange(object sender, EventArgs e)
		{
			ReloadList();
		}

		private void NewClick(object sender, EventArgs e) {
			NewProfileFunc();
		}

		private void DeleteClick(object sender, EventArgs e) {
			DeleteActualProfile();
		}

		//apply
		private void ApplyClick(object sender, EventArgs e) {
			ApplyProfileFunc();
		}

		private void ChangeClick(object sender, EventArgs e) {
			ChangeProfileFunc();
		}

		private void AboutToolStripMenuItemClick(object sender, EventArgs e) {
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
		}

		private void ChangeProfileFunc() {
			if (ChangeProfile != null)
				ChangeProfile(this, null);
		}

		private void ApplyProfileFunc() {
			if (ApplyProfile != null)
				ApplyProfile(this, null);
		}

		private void DesktopShortcutClick(object sender, EventArgs e) {
			if (CreateShortcut != null)
				CreateShortcut(this, null);
		}

		private void ExitFunc() {
			if (Exit != null)
				Exit(this, null);
			
		}

		private void MainFormFormClosed(object sender, FormClosedEventArgs e) {
			this.NotifyIcon.Dispose(); //todo: dat nekam do dza
			ExitFunc();
		}

		private void ListBoxItemsDoubleClick(object sender, EventArgs e) {
			ApplyProfileFunc();
		}

		private void ExitToolStripMenuItem1Click(object sender, EventArgs e) {
			Close();
		}


		private void ListBoxItemsMouseClick(object sender, MouseEventArgs e) {
			if (e.Button != MouseButtons.Right) return;
			var loc = ListBoxItems.PointToScreen(new Point(e.X, e.Y));
			for (int i = 0; i < ListBoxItems.Items.Count; i++) {
				var rec = ListBoxItems.GetItemRectangle(i);
				if (rec.X <= e.X && (rec.X + rec.Width) >= e.X && rec.Y <= e.Y && (rec.Y + rec.Height) >= e.Y) {
					ItemContextMenu.Items[0].Enabled = true;
					ItemContextMenu.Items[2].Enabled = true;
					ItemContextMenu.Items[3].Enabled = true;
					ItemContextMenu.Items[4].Enabled = true;
					ListBoxItems.ClearSelected();
					ListBoxItems.SetSelected(i, true);
					ItemContextMenu.Show(loc);
					return;
				}
			}
			ItemContextMenu.Items[0].Enabled = false;
			ItemContextMenu.Items[3].Enabled = false;
			ItemContextMenu.Items[4].Enabled = false;
			ItemContextMenu.Show(loc);
		}


		private void SettingsToolStripMenuItemClick(object sender, EventArgs e) {
			if (OpenSettings != null)
				OpenSettings(this, null);
		}
		
		void NotifyIconDoubleClick(object sender, EventArgs e) {
			Show();
			WindowState = FormWindowState.Normal;
			NotifyIcon.Visible = false;
		}

		void NotifyIconClick(object sender, MouseEventArgs e) {
			if (e.Button != MouseButtons.Right) return;
			if (CreateMenuList())
			{
				contextMenuTray.Visible = true;
			}
		}

    	void MainFormResize(object sender, EventArgs e) {
			if (FormWindowState.Minimized == WindowState)
			{
				GoToTray();
			}
		}

    	private void ListBoxItemsDrawItem(object sender, DrawItemEventArgs e) {
			var rc = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height - 5);

			if (e.Index < 0)
				return;

			var list = (ListBox)sender;
			var str = (string)list.Items[e.Index];
			var profile = manager.GetProfile(str);
			if (profile == null)
				return;

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

				if (profile.Connections.Count != 0) {
					NetworkInterfaceSettings settings = profile.Connections[0].Settings;
					sf.Alignment = StringAlignment.Far;
					e.Graphics.DrawString(settings.IsDHCP ? "DHCP" : settings.IP.ToString(), ipFont, br, rc, sf);
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
			e.Graphics.DrawImage(profile.GetIcon(), new Point(1, e.Bounds.Y));
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
