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

namespace ZetSwitch
{
	public partial class WelcomeScreen : Form, IWelcomeView {
		ConfigurationState state;
		public WelcomeScreen() {
            InitializeComponent();
			ResetLanguage();
        }

		public bool ShowView() {
			return ShowDialog() == DialogResult.OK;
		}

		private void btnOk_Click(object sender, EventArgs e) {
			state.Language = comboBoxLang.Text;
			state.ShowWelcome = !checkBoxShowAgain.Checked;
			DialogResult = DialogResult.OK;
			Close();
		}

        public void ResetLanguage() {
			this.lblName.Text = Language.GetText("ZetSwitch") + " " + Properties.Resources.Version;
			this.btnOk.Text = Language.GetText("Ok");
            this.checkBoxShowAgain.Text = Language.GetText("DontShowAgain");
            this.lblLanguage.Text = Language.GetText("Language");
            this.lblEmail.Text = Language.GetText("Email");
			this.lblAuthor.Text = Language.GetText("Author");
			this.Text = Language.GetText("Welcome");
        }


		public void SetState(ConfigurationState state) {
			this.state = state;
			checkBoxShowAgain.Checked = !state.ShowWelcome;
			comboBoxLang.Items.Clear();
			foreach (string name in state.GetLanguages())
				comboBoxLang.Items.Add(name);
			comboBoxLang.Text = state.Language;
		}

		private void comboBoxLang_SelectedIndexChanged(object sender, EventArgs e) {
			state.Language = comboBoxLang.Text;
			if (LanguageChanged != null)
				LanguageChanged(this, null);

		}

		public event EventHandler LanguageChanged;
	}
}
