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

namespace ZetSwitch {
	public class MainController {
		readonly IMainView view;
		readonly IDataManager manager;

		public MainController(IMainView view, IDataManager manager) {
			this.view = view;
			this.manager = manager;

			view.ApplyProfile += OnApplyProfile;
			view.RemoveProfile += OnRemoveProfile;
            view.ChangeProfile += OnChangeProfile;
			view.NewProfile += OnNewProfile;
			view.Exit += OnExit;
			view.OpenAbout += OnOpenAbout;
			view.OpenSettings += OnOpenSettings;
			view.CreateShortcut += OnCreateShortcut;
		}

		void OnCreateShortcut(object sender, EventArgs e) {
			var profile = view.GetSelectedProfile();
			if (profile == null)
				return;
			var p = manager.GetProfile(profile);
			var shortcut = ClientServiceLocator.GetService<IShortcutCreator>();
			shortcut.CreateProfileLnk(p);
		}

		private void OnApplyProfile(object sender, ProfileEventArgs e) {
			var profile = e == null ? view.GetSelectedProfile() : e.Name;
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
			using (IProfileView profileView = ClientServiceLocator.GetService<IViewFactory>().CreateProfileView()) {
				var controller = ClientServiceLocator.GetService<IProfileController>();
				controller.SetView(profileView);
				controller.SetManager(manager);
				if (controller.Show(profile,false)) {
					manager.Change(name, profile);
					view.ReloadList();
					view.SetSelectByName(profile.Name);
				}

			}
		}

		private void OnNewProfile(object sender, EventArgs e) {
			var profile = manager.New();
			using (IProfileView profileView = ClientServiceLocator.GetService<IViewFactory>().CreateProfileView()) {
				var controller = ClientServiceLocator.GetService<IProfileController>();
				controller.SetView(profileView);
				controller.SetManager(manager);
				if (controller.Show(profile, true)) {
					manager.Add(profile);
					view.ReloadList();
					view.SetSelectByName(profile.Name);
				}

			}
		}

		private void OnExit(object sender, EventArgs e) {
			Application.Exit();
		}

		private void OnApplicationStarted(object sender, EventArgs e) {
			
		}

		private void OnOpenSettings(object sender, EventArgs e) {
			var controler = ClientServiceLocator.GetService<ISettingsController>();
			if (controler.Show())
				view.ResetLanguage();
		}

		private void OnOpenAbout(object sender, EventArgs e) {
			var controler = ClientServiceLocator.GetService<IAboutController>();
			controler.Show();
		}
	}
}
