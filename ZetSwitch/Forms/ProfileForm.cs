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
using ZetSwitchData;

namespace ZetSwitch {
	public partial class ProfileForm : Form, IProfileView {
		private readonly List<ISettingPanel> panels = new List<ISettingPanel>();

		public ProfileForm() {
			InitializeComponent();
			ResetLanguage();
			panels.Add(ipPageView);
			panels.Add(profilePageView);
			panels.Add(proxyPageView);
			profilePageView.SelectProfileIcon += OnSelectIcon;
		}

		private void OnSelectIcon(object sender, EventArgs e) {
			if (SelectProfileIcon != null)
				SelectProfileIcon(this, null);
		}

		public void SetProfile(Profile profile) {
			foreach (var panel in panels) 
				panel.SetData(profile);
		}

		public void UpdateInterfaceList() {
			if (IsHandleCreated && InvokeRequired) {
				BeginInvoke(new MethodInvoker(UpdateInterfaceList), null);
				return;
			}
			ipPageView.UpdateInterfaceList();
		}

		public void UpdateIcon() {
			profilePageView.UpdateIcon();
		}

		public bool ShowView() {
			return ShowDialog() == DialogResult.OK;
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
				message.Append(ClientServiceLocator.GetService<ILanguage>().GetText("ProfileError"));
				message.Append(String.Join("\n -", messages.ToArray()));
				MessageBox.Show(message.ToString(), ClientServiceLocator.GetService<ILanguage>().GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			
		}

		public void Accept() {
			DialogResult = DialogResult.OK;
			Close();
		}

		private void UpdateData() {
			foreach (var settingPanel in panels) 
				settingPanel.UpdateData();
		}

		private void OkButtonClick(object sender, EventArgs e) {
			UpdateData();
			if (Confirm != null)
				Confirm(this, null);
		}

		private void CButtonClick(object sender, EventArgs e) {
			Close();
		}

		private void ResetLanguage() {
			CButton.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Cancel");
			OkButton.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Ok");
			foreach (var panel in panels) {
				panel.ResetLanguage();
			}
		}

		public event EventHandler SelectProfileIcon;
		public event EventHandler Confirm;
	}

	public interface ISettingPanel {
		void UpdateData();
		void SetData(Profile profile);
		void ResetLanguage();
	}
}
