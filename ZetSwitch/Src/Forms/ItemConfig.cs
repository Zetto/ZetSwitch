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
using ZetSwitch.Network;


namespace ZetSwitch
{
    public partial class ItemConfig : Form
	{

		#region variables

		Profile profile;
		bool isNew;
		int oldSelIndex = -1;
		string oldName;
		List<SettingPanel> panels = new List<SettingPanel>();

		#endregion

		#region private

		private void PopulateListBox() {
			List<string> names = profile.GetNetworkInterfaceNames();
			List<NetworkInterfaceSettings> ifs = ProfileManager.GetInstance().Model.GetNetworkInterfaceSettings();
			foreach (NetworkInterfaceSettings setting in ifs) {
				if (!names.Contains(setting.Name)) {
					names.Add(setting.Name);
					profile.AddNetworkInterface(setting);
				}
			}

			foreach (string name in names) {
				ListBoxInterfaces.Items.Add(name);
				ListBoxInterfaces.SetItemChecked(ListBoxInterfaces.Items.Count - 1, profile.IsNetworkInterfaceInProfile(name));
			}
			if (ListBoxInterfaces.Items.Count > 0)
				ListBoxInterfaces.SetSelected(0, true);
		}

		private void LoadData() {
			TextBoxName.Text = profile.Name;
			LoadItemIcon(profile.IconFile);
		}

		private void LoadItemIcon(string file)
		{
			try
			{
				Picture.Image = (Image)ImgCollection.Instance.GetImage(file);
			}
			catch (Exception)
			{
				MessageBox.Show(Language.GetText("CanLoadIconFile") + file, Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				profile.IconFile = "default";
				Picture.Image = Properties.Resources._default;
			}
		}

		private bool CanChange(int index)
		{
			string error;
			if (panels.Count <= index || index < 0)
				return true;
			if (panels[index].DataValidation(out error))
				return true;
			else
			{
				MessageBox.Show(error, Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		private bool SaveTabPage(int index)
		{
			if (panels.Count <= index || index < 0)
				return true;
			return panels[index].SavePanel();
		}

		private bool LoadTabPage(int index)
		{
			if (panels.Count <= index || index < 0)
				return true;
			return panels[index].LoadPanel();
		}

		private bool DataValidation()
		{
			StringBuilder StrMessage = new StringBuilder();
			if (isNew || oldName != profile.Name)
			{
				if (ProfileManager.GetInstance().GetProfile(TextBoxName.Text) != null)
				{
					StrMessage.Append("Profil '" + TextBoxName.Text + "' již existuje.\n");
				}
			}
			if (TextBoxName.Text.Length == 0)
			{
				StrMessage.Append(Language.GetText("ProfileNameIsEmpty") + "\n");
			}
			if (ListBoxInterfaces.SelectedIndex >= 0 && ListBoxInterfaces.GetItemChecked(ListBoxInterfaces.SelectedIndex))
			{
				if (!CanChange(TabControl.SelectedIndex))
					return false;
			}
			if (StrMessage.Length > 0)
			{
				MessageBox.Show(StrMessage.ToString(), Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		

		private void SelectProfileIcon()
		{
			using (OpenFileDialog Dialog = new OpenFileDialog()) {
				Dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Data\\Images\\";
				Dialog.Filter = "Obrázky (*.jpg) (*.bmp) (*.png) (*.gif) (*.ico) | *.ico ;*.jpg;*.bmp; *.png; *.gif";

				if (Dialog.ShowDialog() == DialogResult.OK) {
					string fileName = Dialog.FileName;
					fileName = ImgCollection.Instance.InitImage(fileName);
					LoadItemIcon(fileName);
					profile.IconFile = fileName;
				}
			}
		}

		private bool SaveData()
		{
			profile.Name = TextBoxName.Text;
			if (!DataValidation())
				return false;

			SaveTabPage(TabControl.SelectedIndex);
			
			foreach (string name in ListBoxInterfaces.Items) {
				profile.UseNetworkInterface(name, ListBoxInterfaces.GetItemChecked(ListBoxInterfaces.Items.IndexOf(name)));
			}
			profile.RemoveUnusedItenrfaces();
			return true;
		}

		private void ResetLanguage()
		{
			this.label6.Text = Language.GetText("Name");
			this.ButtonPictureChange.Text = Language.GetText("Change");
			this.IPDHCPManual.Text = Language.GetText("TextUseThisSetting");
			this.DNSDHCPManual.Text = Language.GetText("TextUseThisSetting");
			this.label3.Text = Language.GetText("TextDefGate");
			this.IPDHCPAuto.Text = Language.GetText("TextOtainDNS");
			this.label2.Text = Language.GetText("TextMask");
			this.DNSDHCPAuto.Text = Language.GetText("TextOtainDNS");
			this.CButton.Text = Language.GetText("Cancel");
			this.OkButton.Text = Language.GetText("Ok");
			this.label7.Text = Language.GetText("TextConSelect");
			return;
		}

		#endregion

		#region handlers

		private void ListBoxInterfaces_SelectedIndexChanged(object sender, EventArgs e)
		{
			// todo: projit z jistit jestli to neni uplny nesmysl
			int index = ListBoxInterfaces.SelectedIndex;
			if (index < 0 || oldSelIndex == index)
				return;
			if (oldSelIndex >= 0 && ListBoxInterfaces.GetItemChecked(oldSelIndex) && !CanChange(TabControl.SelectedIndex))
			{
				ListBoxInterfaces.SetSelected(oldSelIndex, true);
				return;
			}
			if (oldSelIndex >= 0 && !SaveTabPage(TabControl.SelectedIndex))
				return;
			foreach (SettingPanel panel in panels)
				panel.InterfaceName = (string)ListBoxInterfaces.Items[index];
			LoadTabPage(TabControl.SelectedIndex);
			oldSelIndex = index;
		}

		private void TabControl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadTabPage(TabControl.SelectedIndex);
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			if (!SaveData())
				return;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void CButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ButtonPictureChange_Click(object sender, EventArgs e)
		{
			SelectProfileIcon();
		}

		private void radioButtonBrowser_CheckedChanged(object sender, EventArgs e)
		{
			//tabPageProxy.ChangeBrowser();
		}

		#endregion


		#region public

		public ItemConfig(bool newProfile, Profile profile)
        {
			this.profile = profile;
			isNew = newProfile;
			InitializeComponent();
			oldName = profile.Name;
			tabPageIP.DataChanged += new EventHandler(OnDataChange);
			
			panels.Add(tabPageIP);
		/*	panels.Add(tabPageProxy);
			panels.Add(tabPageMAC);*/

			foreach (SettingPanel panel in panels)
			{
				panel.SetControls();
				panel.ActualProfile = profile;
			}

			LoadData();

			ProfileManager.GetInstance().Model.LoadData();
			if (ProfileManager.GetInstance().Model.IsIFLoaded())
				PopulateListBox();	
			else
				ProfileManager.GetInstance().Model.DataLoaded += new EventHandler(model_DataLoaded);
			ResetLanguage();
		}

		private void model_DataLoaded(object o, EventArgs e) {
			ProfileManager.GetInstance().Model.DataLoaded -= new EventHandler(model_DataLoaded);
			PopulateListBox();
		}

		private void OnDataChange(object o, EventArgs e) {
			SetUseActualSelection();
		}

		private void SetUseActualSelection() {
			int index = ListBoxInterfaces.SelectedIndex;
			if (index < 0 )
				return;
			ListBoxInterfaces.SetItemChecked(index,true);
		}

		#endregion
	}
}
