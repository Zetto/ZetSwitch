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

    public partial class MainForm : Form
    {
        private DataModel model;

		#region private

		private void ReloadList()
		{
			ListBoxItems.Items.Clear();
			foreach (Profile profile in ProfileManager.GetInstance().Profiles)
			{
				ListBoxItems.Items.Add(profile.Name);
			}
		}

		private void DeleteActualProfile()
		{
			if (ListBoxItems.SelectedIndex < 0)
				return;
			if (MessageBox.Show(Language.GetText("DeleteQuestion") + "'" + ListBoxItems.Items[ListBoxItems.SelectedIndex].ToString() + "'?", Language.GetText("DeleteProfile"), MessageBoxButtons.YesNo) == DialogResult.No)
				return;
			ProfileManager.GetInstance().Delete(ListBoxItems.Items[ListBoxItems.SelectedIndex].ToString());
			ReloadList();
		}

		private void NewProfile()
		{
			Profile profile = ProfileManager.GetInstance().GetNewProfile();
			ItemConfig dlg = new ItemConfig(true, profile);
			AddOwnedForm(dlg);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				ProfileManager.GetInstance().Add(profile);
				ReloadList();
				SetSelectByName(profile.Name);
			}
			dlg.Dispose();
			return;
		}

		private void ChangeProfile()
		{
			if (ListBoxItems.SelectedIndex < 0)
				return;
			string Name = ListBoxItems.Items[ListBoxItems.SelectedIndex].ToString();
			Profile profile = ProfileManager.GetInstance().GetCloneProfile(Name);
			ItemConfig dlg = new ItemConfig(false, profile);
			AddOwnedForm(dlg);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				ProfileManager.GetInstance().Change(Name, profile);
				ReloadList();
				SetSelectByName(profile.Name);
			}
			dlg.Dispose();
		}

		private void ApplyActualProfile()
		{
			if (ListBoxItems.SelectedIndex < 0)
				return;

			Apply(ListBoxItems.SelectedItem.ToString());
		}

		private void Apply(string Name)
		{
			if (Name.Length != 0)
			{
				try
				{
					ProfileManager.GetInstance().ApplyProfile(Name);
					// TODO: aplikace profilu Item.Apply();
				}
				catch (Exception E)
				{
					MessageBox.Show(E.Message, Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			MessageBox.Show(Language.GetText("ProfileApplied"), Language.GetText("Succes"), MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private bool ApplyQuestion(string ProfileName)
		{
			return MessageBox.Show(Language.GetText("UseProfile") + "'" + ProfileName + "'?", Language.GetText("UseProfile"), MessageBoxButtons.YesNo, 
									MessageBoxIcon.Question) == DialogResult.Yes ? true : false;
		}

		private void SetSelectByName(string Name)
		{
			int i = ListBoxItems.FindStringExact(Name);
			if (i < 0)
			{
				if (ListBoxItems.SelectedIndex < 0 && ListBoxItems.Items.Count > 0)
					ListBoxItems.SetSelected(0, true);
				return;
			}
			else
			{
				if (!(ListBoxItems.SelectedIndex < 0))
					ListBoxItems.SetSelected(ListBoxItems.SelectedIndex, false);
				ListBoxItems.SetSelected(i, true);
			}
		}

		private bool CreateMenuList()
		{
			contextMenuTray.Items.Clear();
			contextMenuTray.Items.Add(Language.GetText("ProfileNew"));

			if (ListBoxItems.Items.Count != 0)
				contextMenuTray.Items.Add("-");

			foreach (string Text in ListBoxItems.Items)
			{
				contextMenuTray.Items.Add(Text);
			}

			if (ListBoxItems.Items.Count != 0)
				contextMenuTray.Items.Add("-");
			contextMenuTray.Items.Add(Language.GetText("Exit"));

			return true;
		}

		private void ResetLanguage()
		{

			this.fileToolStripMenuItem1.Text = Language.GetText("MenuFile");
			this.newProfileToolStripMenuItem1.Text = Language.GetText("ProfileNew");
			this.deleteProfileToolStripMenuItem.Text = Language.GetText("ProfileDelete");
			this.exitToolStripMenuItem1.Text = Language.GetText("Exit");
			this.actionToolStripMenuItem1.Text = Language.GetText("MenuAction");
			this.applyProfileToolStripMenuItem1.Text = Language.GetText("ProfileApply");
			this.changeProfileToolStripMenuItem1.Text = Language.GetText("ProfileChange");
			this.helpToolStripMenuItem1.Text = Language.GetText("MenuHelp");
			this.aaboutToolStripMenuItem.Text = Language.GetText("MenuAbout");
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

		#endregion

		#region public 
		
		public DataModel Model
		{
			set { model = value; }
		}

        public MainForm()
        {
            InitializeComponent();
            ResetLanguage();

            ReloadList();
         
            if (ListBoxItems.Items.Count > 0)
                ListBoxItems.SetSelected(0, true);

			ImgCollection.Instance.PathToImages = Application.StartupPath + Properties.Settings.Default.ImagesPath;
        }

		public void GoToTray()
		{
			this.NotifyIcon.Visible = true;
			Hide();
		}

		#endregion

		#region handlers

		private void MainForm_SizeChange(object sender, EventArgs e)
		{
			ReloadList();
		}

		private void New_Click(object sender, EventArgs e)
		{
			NewProfile();
		}

		private void Delete_Click(object sender, EventArgs e)
		{
			DeleteActualProfile();
		}

		//apply
		private void Apply_Click(object sender, EventArgs e)
		{
			if (ListBoxItems.SelectedIndex < 0)
				return;
			if (ApplyQuestion(ListBoxItems.SelectedItem.ToString()))
			{
				ApplyActualProfile();
			}
		}

		private void Change_Click(object sender, EventArgs e)
		{
			ChangeProfile();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			ProfileManager.GetInstance().SaveSettings();
			Application.Exit();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.NotifyIcon.Dispose();
			Properties.Settings.Default.Save();
		}

		private void ListBoxItems_DoubleClick(object sender, EventArgs e)
		{
			if (ApplyQuestion(ListBoxItems.SelectedItem.ToString()))
			{
				ApplyActualProfile();
			}
		}

		private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Close();
		}


		private void ListBoxItems_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Point Loc = ListBoxItems.PointToScreen(new Point(e.X, e.Y));
				for (int i = 0; i < ListBoxItems.Items.Count; i++)
				{
					Rectangle Rec = ListBoxItems.GetItemRectangle(i);
					if (Rec.X <= e.X && (Rec.X + Rec.Width) >= e.X && Rec.Y <= e.Y && (Rec.Y + Rec.Height) >= e.Y)
					{
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

		private void renameProfileToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox About = new AboutBox();
			About.ShowDialog();

		}

		private void nastaveniToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Setting Dlg = new Setting();
			if (Dlg.ShowDialog() == DialogResult.OK)
			{
				ResetLanguage();
			}
		}

		void NotifyIcon_DoubleClick(object sender, System.EventArgs e)
		{
			Show();
			WindowState = FormWindowState.Normal;
			this.NotifyIcon.Visible = false;
		}

		void NotifyIcon_Click(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (CreateMenuList())
				{
					contextMenuTray.Visible = true;
				}
			}
		}

		void MainForm_Resize(object sender, System.EventArgs e)
		{
			if (FormWindowState.Minimized == WindowState)
			{
				GoToTray();
			}
		}

		void contextMenuTray_ItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
		{
			contextMenuTray.Hide();

			if (e.ClickedItem.Text == "Nový Profil" || e.ClickedItem.Text == "New Profile")
			{
				New_Click(null, null);
			}
			else if (e.ClickedItem.Text == "Konec" || e.ClickedItem.Text == "Exit")
			{
				Close();
			}
			else
				Apply(e.ClickedItem.Text);


		}

		void contextMenuTray_LostFocus(object sender, System.EventArgs e)
		{
			contextMenuTray.Hide();
		}

		private void ListBoxItems_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{

			Rectangle rc = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height - 5);

			if (e.Index < 0)
				return;

			ListBox List = (ListBox)sender;
			StringFormat sf = new StringFormat();
			sf.LineAlignment = StringAlignment.Center;
			sf.Alignment = StringAlignment.Center;
			Brush Br = new SolidBrush(Color.LightGray);

			string str = (string)List.Items[e.Index];


			if (e.State != (DrawItemState.Selected | DrawItemState.Focus))
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.White), rc);
				e.Graphics.DrawString(str, new Font("Ariel", 12), new SolidBrush(Color.Black), rc, sf);

			}
			else
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), rc);
				e.Graphics.DrawString(str, new Font("Ariel", 12), new SolidBrush(Color.Black), rc, sf);
				Br = new SolidBrush(Color.Gray);
			}


			//draw ip address

			Profile profile = ProfileManager.GetInstance().GetProfile(str);
			NetworkInterfaceSettings settings;

			if (profile != null && profile.Connections.Count != 0)
			{
				settings = profile.Connections[0].Settings;
				sf.Alignment = StringAlignment.Far;
				if (settings.IsDHCP)
				{
					e.Graphics.DrawString("DHCP", new Font("Ariel", 8), Br, rc, sf);
				}
				else
				{
					e.Graphics.DrawString(settings.IP.ToString(), new Font("Ariel", 8), Br, rc, sf);
				}

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

    }
}
