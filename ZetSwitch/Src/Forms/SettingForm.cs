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
using System.Configuration;
using Microsoft.Win32;

namespace ZetSwitch
{
	public partial class SettingForm : Form, ISettingsView {
		ConfigurationState state;
        
		public SettingForm() {
           InitializeComponent();
		   ResetLanguage();
        }

		public void SetState(ConfigurationState state) {
			this.state = state;
			checkBoxRunAuto.Checked = state.Autorun;
			comboBoxLang.Items.Clear();
			foreach (string name in state.GetLanguages())
				comboBoxLang.Items.Add(name);
			comboBoxLang.Text = state.Language;
		}

        private void comboBoxLang_SelectedIndexChanged(object sender, EventArgs e) {
			state.Language = comboBoxLang.Text;
        }

		public bool ShowView() {
			return ShowDialog() == DialogResult.OK;
		}

        public void ResetLanguage() {
            this.Text = Language.GetText("Settings");
            this.checkBoxRunAuto.Text = Language.GetText("RunAuto");
            this.label6.Text = Language.GetText("Language");
            this.buttonOk.Text = Language.GetText("Ok");
            this.buttonCancel.Text = Language.GetText("Cancel");
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
			Close();
        }

		private void buttonOk_Click(object sender, EventArgs e) {
			state.Autorun = checkBoxRunAuto.Checked;
			DialogResult = DialogResult.OK;
			Close();
		}
    }
}
