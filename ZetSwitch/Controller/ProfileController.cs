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
using ZetSwitchData;
using ZetSwitchData.Network;
using ZetSwitch.Properties;

namespace ZetSwitch {
	public class ProfileController : IProfileController {
		private IProfileView actView;
		private IDataManager manager;
		private Profile actProfile;
		private string oldProfileName = "";

		public void SetProfile(Profile profile, bool isNew) {
			actProfile = profile;
			oldProfileName = isNew ? "" : actProfile.Name ;
			actProfile.AddBrowsersNames(manager.GetBrowsersNames());
			actView.SetProfile(actProfile);
			if (manager != null && manager.IsIFLoaded())
				LoadData();
		}

		public bool Show() {
			bool result = actView.ShowView();
			actProfile.RemoveUnusedInterfaces();
			return result;
		}

		public void SetView(IProfileView view) {
			actView = view;
			actView.SelectProfileIcon += OnSelectProfileIcon;
			actView.Confirm += OnActViewConfirm;
		}
		
		private string ValidateProfileName() {
			if (String.IsNullOrEmpty(actProfile.Name))
				return null;

			if (!String.IsNullOrEmpty(oldProfileName) && actProfile.Name == oldProfileName)
				return null;

			return manager.ContainsProfile(actProfile.Name) ? ClientServiceLocator.GetService<ILanguage>().GetText("ProfileExists") : null;
		}

		void OnActViewConfirm(object sender, EventArgs e) {
			var errors = new List<string>();
			string er = ValidateProfileName();
			
			if (!String.IsNullOrEmpty(er))
				errors.Add(er);
			actProfile.Validation(errors);
			
			if (errors.Count > 0) {
				actView.ShowError(errors);
				return;
			}
			actView.Accept();
		}

		public void SetManager(IDataManager man) {
			if (manager != null) 
				manager.DataLoaded -= OnDataLoaded;
			manager = man;
			if (!manager.IsIFLoaded())
				manager.DataLoaded += OnDataLoaded;
		}

		private void LoadData() {
			IList<string> names = actProfile.GetNetworkInterfaceNames();
			IList<NetworkInterfaceSettings> ifs = manager.GetNetworkInterfaceSettings();
			foreach (NetworkInterfaceSettings setting in ifs.Where(setting => !names.Contains(setting.Name)))
				actProfile.AddNetworkInterface(setting);
			actView.UpdateInterfaceList();
		}

		private void OnDataLoaded(object o, EventArgs e) {
			LoadData();
		}

		private void OnSelectProfileIcon(object o, EventArgs e) {
			var images = ClientServiceLocator.GetService<IImageRepository>();
			string path = images.GetDirectory();
			string filter = Resources.imagesDialogString;
			string fileName = actView.AskToSelectNewIcon(path, filter);
			if (!String.IsNullOrEmpty(fileName)) {
				
				actProfile.IconFile = images.InitImage(fileName);
				actView.UpdateIcon();
			}
		}
	}
}
