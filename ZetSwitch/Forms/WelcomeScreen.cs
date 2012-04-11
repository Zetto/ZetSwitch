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
	public partial class WelcomeScreen : Form, IWelcomeView {
		private ConfigurationState state;

		public WelcomeScreen() {
			InitializeComponent();
			ResetLanguage();
		}

		public bool ShowView() {
			return ShowDialog() == DialogResult.OK;
		}

		private void OkClick(object sender, EventArgs e) {
			state.Language = comboBoxLang.Text;
			state.ShowWelcome = !checkBoxShowAgain.Checked;
			DialogResult = DialogResult.OK;
			Close();
		}

		public void ResetLanguage() {
			lblName.Text = ClientServiceLocator.GetService<ILanguage>().GetText("ZetSwitch") + " " + Properties.Resources.Version;
			btnOk.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Ok");
			checkBoxShowAgain.Text = ClientServiceLocator.GetService<ILanguage>().GetText("DontShowAgain");
			lblLanguage.Text  = ClientServiceLocator.GetService<ILanguage>().GetText("Language");
			lblEmail.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Email");
			lblAuthor.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Author");
			Text = ClientServiceLocator.GetService<ILanguage>().GetText("Welcome");
		}


		public void SetState(ConfigurationState configurationState) {
			state = configurationState;
			checkBoxShowAgain.Checked = !configurationState.ShowWelcome;
			comboBoxLang.Items.Clear();
			foreach (string name in configurationState.GetLanguages())
				comboBoxLang.Items.Add(name);
			comboBoxLang.Text = configurationState.Language;
		}

		private void ComboBoxLangSelectedIndexChanged(object sender, EventArgs e) {
			state.Language = comboBoxLang.Text;
			if (LanguageChanged != null)
				LanguageChanged(this, null);

		}

		public event EventHandler LanguageChanged;
	}
}
