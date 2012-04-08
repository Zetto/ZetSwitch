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
using System.Windows.Forms;
using ZetSwitch.Properties;


namespace ZetSwitch {
	public partial class ProfileForm : Form, IProfileView {
		private readonly List<SettingPanel> panels = new List<SettingPanel>();
		private int oldSelIndex = -1;
		private Profile actProfile;


		public ProfileForm() {
			InitializeComponent();
			ResetLanguage();

			tabPageIP.DataChanged += OnDataChange;
			panels.Add(tabPageIP);

			/*	panels.Add(tabPageProxy);
				panels.Add(tabPageMAC);*/
		}

		public void SetProfile(Profile profile) {
			actProfile = profile;
			TextBoxName.Text = actProfile.Name;
			UpdateIcon();

			foreach (SettingPanel panel in panels) {
				panel.SetControls();
				panel.ActualProfile = profile;
			}
		}

		public bool ShowView() {
			return ShowDialog() == DialogResult.OK;
		}

		public void UpdateInterfaceList() {
			if (IsHandleCreated && InvokeRequired) {
				BeginInvoke(new MethodInvoker(UpdateInterfaceList),null);
				return;
			}

			ListBoxInterfaces.IsLoaded = true;
			var names = actProfile.GetNetworkInterfaceNames();
			foreach (var name in names) {
				ListBoxInterfaces.Items.Add(name);
				ListBoxInterfaces.SetItemChecked(ListBoxInterfaces.Items.Count - 1, actProfile.IsNetworkInterfaceInProfile(name));
			}

			if (ListBoxInterfaces.Items.Count > 0)
				ListBoxInterfaces.SetSelected(0, true);
		}

		public void UpdateIcon() {
			Picture.Image = actProfile.GetIcon();
		}

		public string AskToSelectNewIcon(string path, string filter) {
			using (var dialog = new OpenFileDialog()) {
				dialog.InitialDirectory = path;
				dialog.Filter = filter;
				if (dialog.ShowDialog() == DialogResult.OK)
					return dialog.FileName;
			}
			return null;
		}

		private void ButtonPictureChangeClick(object sender, EventArgs e) {
			if (SelectProfileIcon != null)
				SelectProfileIcon(this, null);
		}


		private bool DataValidation() {
		/*	var message = new StringBuilder();
			if (isNew || oldName != actProfile.Name) {
				if (datas.GetProfile(TextBoxName.Text) != null) {
					message.Append("Profil '" + TextBoxName.Text + "' již existuje.\n");
				}
			}
			if (TextBoxName.Text.Length == 0) {
				message.Append(Language.GetText("ProfileNameIsEmpty") + "\n");
			}
			if (ListBoxInterfaces.SelectedIndex >= 0 && ListBoxInterfaces.GetItemChecked(ListBoxInterfaces.SelectedIndex)) {
				if (!CanChange(TabControl.SelectedIndex))
					return false;
			}
			if (message.Length > 0) {
				MessageBox.Show(message.ToString(), Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}*/
			return true;
		}

		private bool SaveData() {
			actProfile.Name = TextBoxName.Text;
			if (!DataValidation())
				return false;

			SaveTabPage(TabControl.SelectedIndex);

			foreach (string name in ListBoxInterfaces.Items) {
				actProfile.UseNetworkInterface(name, ListBoxInterfaces.GetItemChecked(ListBoxInterfaces.Items.IndexOf(name)));
			}
			actProfile.RemoveUnusedInterfaces();
			return true;
		}

		private bool SaveTabPage(int index) {
			if (panels.Count <= index || index < 0)
				return true;
			return panels[index].SavePanel();
		}

		private void LoadTabPage(int index) {
			if (panels.Count <= index || index < 0)
				return;
			panels[index].LoadPanel();
		}

		private bool CanChange(int index) {
			string error;
			if (panels.Count <= index || index < 0)
				return true;
			if (panels[index].DataValidation(out error))
				return true;
			MessageBox.Show(error, Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}

		private void ListBoxInterfacesSelectedIndexChanged(object sender, EventArgs e) {
			int index = ListBoxInterfaces.SelectedIndex;
			if (index < 0 || oldSelIndex == index)
				return;
			if (oldSelIndex >= 0 && ListBoxInterfaces.GetItemChecked(oldSelIndex) && !CanChange(TabControl.SelectedIndex)) {
				ListBoxInterfaces.SetSelected(oldSelIndex, true);
				return;
			}
			if (oldSelIndex >= 0 && !SaveTabPage(TabControl.SelectedIndex))
				return;
			foreach (SettingPanel panel in panels)
				panel.InterfaceName = (string) ListBoxInterfaces.Items[index];
			LoadTabPage(TabControl.SelectedIndex);
			oldSelIndex = index;
		}

		private void TabControlSelectedIndexChanged(object sender, EventArgs e) {
			LoadTabPage(TabControl.SelectedIndex);
		}

		private void OkButtonClick(object sender, EventArgs e) {
			if (!SaveData())
				return;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void CButtonClick(object sender, EventArgs e) {
			Close();
		}

		private void OnDataChange(object o, EventArgs e) {
			SetUseActualSelection();
		}

		private void SetUseActualSelection() {
			int index = ListBoxInterfaces.SelectedIndex;
			if (index < 0)
				return;
			ListBoxInterfaces.SetItemChecked(index, true);
		}


		private void ResetLanguage() {
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

		public event EventHandler SelectProfileIcon;
	}
}
