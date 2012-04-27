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
			
			return manager.ContainsProfile(actProfile.Name) ? "Profile se zadaným jménem již existuje" : null;
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
