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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZetSwitch.Network;
using ZetSwitch.Properties;


namespace ZetSwitch
{
    public partial class ProfileForm : Form
	{

		#region variables

    	readonly ProfileManager _profiles;
    	readonly Profile _profile;
    	readonly bool _isNew;
    	readonly string _oldName;
		int _oldSelIndex = -1;
    	readonly List<SettingPanel> _panels = new List<SettingPanel>();

		#endregion

        public ProfileForm(bool newProfile, Profile profile, ProfileManager profiles) {
			InitializeComponent();
            _profile = profile;
            _profiles = profiles;
            _isNew = newProfile;
            _oldName = profile.Name;
            
			tabPageIP.DataChanged += OnDataChange;
            _panels.Add(tabPageIP);
            /*	panels.Add(tabPageProxy);
                panels.Add(tabPageMAC);*/

            foreach (SettingPanel panel in _panels)
            {
                panel.SetControls();
                panel.ActualProfile = profile;
            }

            LoadData();

            if (profiles.Model.IsIFLoaded())
                PopulateListBox();
            else
                profiles.Model.DataLoaded += ModelDataLoaded;
            ResetLanguage();
        }

		#region private

		private void PopulateListBox() {
			ListBoxInterfaces.IsLoaded = true;
			var names = _profile.GetNetworkInterfaceNames();
            var ifs = _profiles.Model.GetNetworkInterfaceSettings();
			foreach (NetworkInterfaceSettings setting in ifs.Where(setting => !names.Contains(setting.Name))) {
				names.Add(setting.Name);
				_profile.AddNetworkInterface(setting);
			}

			foreach (string name in names) {
				ListBoxInterfaces.Items.Add(name);
				ListBoxInterfaces.SetItemChecked(ListBoxInterfaces.Items.Count - 1, _profile.IsNetworkInterfaceInProfile(name));
			}
			if (ListBoxInterfaces.Items.Count > 0)
				ListBoxInterfaces.SetSelected(0, true);
		}

		private void LoadData() {
			TextBoxName.Text = _profile.Name;
			LoadItemIcon(_profile.IconFile);
		}

		private void LoadItemIcon(string file)
		{
			try
			{
				Picture.Image = ImgCollection.Instance.GetImage(file);
			}
			catch (Exception)
			{
				MessageBox.Show(Language.GetText("CanLoadIconFile") + file, Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				_profile.IconFile = "default";
				Picture.Image = Resources._default;
			}
		}

		private bool CanChange(int index)
		{
			string error;
			if (_panels.Count <= index || index < 0)
				return true;
			if (_panels[index].DataValidation(out error))
				return true;
			MessageBox.Show(error, Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}

		private bool SaveTabPage(int index)
		{
			if (_panels.Count <= index || index < 0)
				return true;
			return _panels[index].SavePanel();
		}

		private void LoadTabPage(int index)
		{
			if (_panels.Count <= index || index < 0)
				return;
			_panels[index].LoadPanel();
		}

		private bool DataValidation()
		{
			var message = new StringBuilder();
			if (_isNew || _oldName != _profile.Name) {
                if (_profiles.GetProfile(TextBoxName.Text) != null)
				{
					message.Append("Profil '" + TextBoxName.Text + "' již existuje.\n");
				}
			}
			if (TextBoxName.Text.Length == 0) {
				message.Append(Language.GetText("ProfileNameIsEmpty") + "\n");
			}
			if (ListBoxInterfaces.SelectedIndex >= 0 && ListBoxInterfaces.GetItemChecked(ListBoxInterfaces.SelectedIndex))
			{
				if (!CanChange(TabControl.SelectedIndex))
					return false;
			}
			if (message.Length > 0)
			{
				MessageBox.Show(message.ToString(), Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		

		private void SelectProfileIcon()
		{
			using (var dialog = new OpenFileDialog()) {
				dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Data\\Images\\";
				dialog.Filter = Resources.imagesDialogString;

				if (dialog.ShowDialog() == DialogResult.OK) {
					string fileName = dialog.FileName;
					fileName = ImgCollection.Instance.InitImage(fileName);
					LoadItemIcon(fileName);
					_profile.IconFile = fileName;
				}
			}
		}

		private bool SaveData()
		{
			_profile.Name = TextBoxName.Text;
			if (!DataValidation())
				return false;

			SaveTabPage(TabControl.SelectedIndex);
			
			foreach (string name in ListBoxInterfaces.Items) {
				_profile.UseNetworkInterface(name, ListBoxInterfaces.GetItemChecked(ListBoxInterfaces.Items.IndexOf(name)));
			}
			_profile.RemoveUnusedItenrfaces();
			return true;
		}

		private void ResetLanguage()
		{
			label6.Text = Language.GetText("Name");
			ButtonPictureChange.Text = Language.GetText("Change");
			IPDHCPManual.Text = Language.GetText("TextUseThisSetting");
			DNSDHCPManual.Text = Language.GetText("TextUseThisSetting");
			label3.Text = Language.GetText("TextDefGate");
			IPDHCPAuto.Text = Language.GetText("TextOtainDNS");
			label2.Text = Language.GetText("TextMask");
			DNSDHCPAuto.Text = Language.GetText("TextOtainDNS");
			CButton.Text = Language.GetText("Cancel");
			OkButton.Text = Language.GetText("Ok");
			label7.Text = Language.GetText("TextConSelect");
		}

		#endregion

		#region handlers

		private void ListBoxInterfacesSelectedIndexChanged(object sender, EventArgs e)
		{
			// todo: projit z jistit jestli to neni uplny nesmysl
			int index = ListBoxInterfaces.SelectedIndex;
			if (index < 0 || _oldSelIndex == index)
				return;
			if (_oldSelIndex >= 0 && ListBoxInterfaces.GetItemChecked(_oldSelIndex) && !CanChange(TabControl.SelectedIndex))
			{
				ListBoxInterfaces.SetSelected(_oldSelIndex, true);
				return;
			}
			if (_oldSelIndex >= 0 && !SaveTabPage(TabControl.SelectedIndex))
				return;
			foreach (SettingPanel panel in _panels)
				panel.InterfaceName = (string)ListBoxInterfaces.Items[index];
			LoadTabPage(TabControl.SelectedIndex);
			_oldSelIndex = index;
		}

		private void TabControlSelectedIndexChanged(object sender, EventArgs e)
		{
			LoadTabPage(TabControl.SelectedIndex);
		}

		private void OkButtonClick(object sender, EventArgs e)
		{
			if (!SaveData())
				return;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void CButtonClick(object sender, EventArgs e)
		{
			Close();
		}

		private void ButtonPictureChangeClick(object sender, EventArgs e)
		{
			SelectProfileIcon();
		}

		#endregion


		#region public

		private void ModelDataLoaded(object o, EventArgs e) {
			_profiles.Model.DataLoaded -= ModelDataLoaded;
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
