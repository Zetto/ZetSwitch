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

namespace ZetSwitch {
	class MainController {
		IMainView view;
		IProfileManager manager;

		public MainController(IMainView view, IProfileManager manager) {
			this.view = view;
			this.manager = manager;

			view.ApplyProfile += new EventHandler<ProfileEventArgs>(OnApplyProfile);
			view.RemoveProfile += new EventHandler(OnRemoveProfile);
            view.ChangeProfile += new EventHandler(OnChangeProfile);
			view.NewProfile += new EventHandler(OnNewProfile);
			view.Exit += new EventHandler(OnExit);
			view.OpenAbout += new EventHandler(OnOpenAbout);
			view.OpenSettings += new EventHandler(OnOpenSettings);
			view.CreateShortcut += new EventHandler(OnCreateShortcut);
		}

		void OnCreateShortcut(object sender, EventArgs e) {
			string profile = view.GetSelectedProfile();
			if (profile == null)
				return;
			Profile p = manager.GetProfile(profile);
			ShorcutCreator shortcut = new ShorcutCreator();
			shortcut.CreateProfileLnk(p);
		}

		private void OnApplyProfile(object sender, ProfileEventArgs e) {
			string profile = e == null ? view.GetSelectedProfile() : e.Name;
			if (profile == null)
				return;
			if (!view.AskToApplyProfile(profile))
				return;

			try {
				manager.Apply(profile);
			} catch (Exception E) {
				view.ShowErrorMessage(E.Message);
				return;
			}
			view.ShowInfoMessage(Language.GetText("ProfileApplied"), Language.GetText("Succes"));
		}

		private void OnRemoveProfile(object sender, EventArgs e) {
			string name = view.GetSelectedProfile();
			if (name == null)
				return;
			if (!view.AskQuestion(Language.GetText("DeleteQuestion") + "'" + name + "'?", Language.GetText("DeleteProfile")))
				return;
			manager.Delete(name);
			view.ReloadList();
		}

		private void OnChangeProfile(object sender, EventArgs e) {
			string name = view.GetSelectedProfile();
			if (name == null)
				return;
			Profile profile = manager.Clone(name);
            using (ItemConfig dlg = new ItemConfig(false, profile, (ProfileManager)manager))
            {
				if (dlg.ShowDialog() == DialogResult.OK) {
					manager.Change(name, profile);
					view.ReloadList();
					view.SetSelectByName(profile.Name);
				}
			}
		}

		private void OnNewProfile(object sender, EventArgs e) {
			Profile profile = manager.New();
            using (ItemConfig dlg = new ItemConfig(true, profile, (ProfileManager)manager)) {
				if (dlg.ShowDialog() == DialogResult.OK) {
					manager.Add(profile);
					view.ReloadList();
					view.SetSelectByName(profile.Name);
				}
			}
		}

		private void OnExit(object sender, EventArgs e) {
			Application.Exit();
		}

		private void OnOpenSettings(object sender, EventArgs e) {
			ISettingsController controler = ClientServiceLocator.GetService<ISettingsController>();
			if (controler.Show())
				view.ResetLanguage();
		}

		private void OnOpenAbout(object sender, EventArgs e) {
			IAboutController controler = ClientServiceLocator.GetService<IAboutController>();
			controler.Show();
		}
	}
}
