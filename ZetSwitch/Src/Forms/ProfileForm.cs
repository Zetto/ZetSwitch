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
using ZetSwitch.Properties;


namespace ZetSwitch {
	public partial class ProfileForm : Form, IProfileView {
		private readonly List<ISettingPanel> panels = new List<ISettingPanel>();
		private Profile actProfile;
		private bool changingIf = false;
		
		public ProfileForm() {
			InitializeComponent();
			ResetLanguage();

			ipPageView.DataChanged += OnDataChange;

			panels.Add(ipPageView);
		}

		public void SetProfile(Profile profile) {
			actProfile = profile;
			TextBoxName.Text = actProfile.Name;
			UpdateIcon();
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

		public void ShowError(IList<string> messages) {
			if (messages.Count!= 0) {
				var message = new StringBuilder();
				message.Append(Language.GetText("ProfileError"));
				message.Append(String.Join("\n -", messages.ToArray()));
				MessageBox.Show(message.ToString(), Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			
		}

		public void Accept() {
			DialogResult = DialogResult.OK;
			Close();
		}

		private void ButtonPictureChangeClick(object sender, EventArgs e) {
			if (SelectProfileIcon != null)
				SelectProfileIcon(this, null);
		}

		private void UpdateData() {
			actProfile.Name = TextBoxName.Text;

			foreach (string name in ListBoxInterfaces.Items) 
				actProfile.UseNetworkInterface(name, ListBoxInterfaces.GetItemChecked(ListBoxInterfaces.Items.IndexOf(name)));

			foreach (var settingPanel in panels) 
				settingPanel.UpdateData();
		}

		private void ListBoxInterfacesSelectedIndexChanged(object sender, EventArgs e) {
			if (ListBoxInterfaces.SelectedIndex < 0 )
				return;
			changingIf = true;
			foreach (var settingPanel in panels)
				settingPanel.SetData(actProfile, actProfile.Connections.GetProfileNetworkSettings((string)ListBoxInterfaces.Items[ListBoxInterfaces.SelectedIndex]).Settings);
			changingIf = false;
		}

		private void OkButtonClick(object sender, EventArgs e) {
			UpdateData();
			if (Confirm != null)
				Confirm(this, null);
		}

		private void CButtonClick(object sender, EventArgs e) {
			Close();
		}

		private void OnDataChange(object o, EventArgs e) {
			if (!changingIf)
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
			CButton.Text = Language.GetText("Cancel");
			OkButton.Text = Language.GetText("Ok");
			label7.Text = Language.GetText("TextConSelect");
		}

		public event EventHandler SelectProfileIcon;
		public event EventHandler Confirm;
	}

	public interface ISettingPanel {
		void UpdateData();
		void SetData(Profile profile, Network.NetworkInterfaceSettings actualInterface);
	}
}
