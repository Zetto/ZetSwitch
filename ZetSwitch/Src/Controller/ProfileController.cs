using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZetSwitch.Network;
using ZetSwitch.Properties;

namespace ZetSwitch {
	class ProfileController : IProfileController {
		private IProfileView actView;
		private IDataManager manager;
		private Profile actProfile;
		private bool newProfile;

		public bool Show(Profile profile, bool isNew) {
			actProfile = profile;
			newProfile = isNew;

			actView.SetProfile(actProfile);
			if (!manager.IsIFLoaded()) 
				manager.DataLoaded += OnDataLoaded;
			else
				LoadData();
			return actView.ShowView();
		}

		public void SetView(IProfileView view) {
			actView = view;
			actView.SelectProfileIcon += OnSelectProfileIcon;
		}

		public void SetManager(IDataManager man) {
			if (manager != null) 
				manager.DataLoaded -= OnDataLoaded;
			manager = man;
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
			string path = AppDomain.CurrentDomain.BaseDirectory + "Data\\Images\\";
			string filter = Resources.imagesDialogString;
			string fileName = actView.AskToSelectNewIcon(path, filter);
			if (!String.IsNullOrEmpty(fileName)) {
				var images = ClientServiceLocator.GetService<IImageRepository>();
				fileName = images.InitImage(fileName);
				LoadItemIcon(fileName);
				actView.UpdateIcon();
			}
		}

		private void LoadItemIcon(string fileName) {
			actProfile.IconFile = fileName;
		}
	}
}
