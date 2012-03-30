﻿/////////////////////////////////////////////////////////////////////////////
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
using Microsoft.Win32;
using System.Windows.Forms;

namespace ZetSwitch {
	class UserConfiguration : ZetSwitch.IUserConfiguration {
		LanguagesStore store = new LanguagesStore();

		private void SaveAutoRun(bool Run) {
			using (RegistryKey Key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run")) {
				if (Key != null) {
					if (Run) {
						Key.SetValue("Zet Switch", "\"" + Application.ExecutablePath + "\" -autorun", RegistryValueKind.String);
					}
					else if (Key.GetValue("Zet Switch") != null) {
						Key.DeleteValue("Zet Switch");
					}

				}
			}
		}

		private bool LoadAutorun() {
			using (RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run")) {
				return Key != null && Key.GetValue("Zet Switch") != null;
			}
		}

		public ConfigurationState LoadConfiguration() {
			ConfigurationState state = new ConfigurationState();
			state.AvailableLanguages = store.GetAvailableLanguages();
			state.Autorun = LoadAutorun();
			state.LanguageShort = Properties.Settings.Default.ActLanguage;
			state.ShowWelcome = Properties.Settings.Default.ShowWelcomeDialog;
			return state;
		}

		public void SaveConfigurate(ConfigurationState state) {
			if (Properties.Settings.Default.ActLanguage != state.LanguageShort && state.LanguageShort.Length != 0) {
				Properties.Settings.Default.ActLanguage = state.LanguageShort;
				Language.LoadWords(Properties.Settings.Default.ActLanguage, store);
			}
			Properties.Settings.Default.ShowWelcomeDialog = state.ShowWelcome;
			SaveAutoRun(state.Autorun);
		}
	}
}
