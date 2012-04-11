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
using System.Windows.Forms;
using ZetSwitchData;

namespace ZetSwitch {
	public partial class SettingForm : Form, ISettingsView {
		private ConfigurationState state;

		public SettingForm() {
			InitializeComponent();
			ResetLanguage();
		}

		public void SetState(ConfigurationState configurationState) {
			state = configurationState;
			checkBoxRunAuto.Checked = state.Autorun;
			comboBoxLang.Items.Clear();
			foreach (string name in state.GetLanguages())
				comboBoxLang.Items.Add(name);
			comboBoxLang.Text = state.Language;
		}

		private void ComboBoxLangSelectedIndexChanged(object sender, EventArgs e) {
			state.Language = comboBoxLang.Text;
		}

		public bool ShowView() {
			return ShowDialog() == DialogResult.OK;
		}

		public void ResetLanguage() {
			Text = ClientServiceLocator.GetService<ILanguage>().GetText("Settings");
			checkBoxRunAuto.Text = ClientServiceLocator.GetService<ILanguage>().GetText("RunAuto");
			label6.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Language");
			buttonOk.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Ok");
			buttonCancel.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Cancel");
		}

		private void ButtonCancelClick(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void ButtonOkClick(object sender, EventArgs e) {
			state.Autorun = checkBoxRunAuto.Checked;
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
