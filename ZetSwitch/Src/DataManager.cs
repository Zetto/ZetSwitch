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
using System.Globalization;
using System.Linq;
using ZetSwitch.Network;
using ZetSwitch.Browsers;

namespace ZetSwitch {
	public class DataManager : IDataManager {
		private bool disposed;

		private List<Profile> profiles = new List<Profile>();

		private readonly NetworkManager interfaceManager = new NetworkManager();
		private readonly Dictionary<BROWSERS, Browser> browsers = new Dictionary<BROWSERS, Browser>();

		public List<Profile> Profiles {
			get { return profiles; }
		}

		public DataManager() {
			browsers[BROWSERS.Ie] = BrowserFactory.CreateBrowser(BROWSERS.Ie);
			browsers[BROWSERS.Firefox] = BrowserFactory.CreateBrowser(BROWSERS.Firefox);
			interfaceManager.DataLoaded += InterfaceManagerDataLoaded;

		}

		~DataManager() {
			Dispose(false);
		}

		public bool IsIFLoaded() {
			return interfaceManager.IsLoaded();
		}

		private void InterfaceManagerDataLoaded(object o, EventArgs e) {
			if (DataLoaded != null)
				DataLoaded(this, null);
		}

		public event EventHandler DataLoaded;

		private void LoadData() {
			interfaceManager.StartLoad();
			foreach (KeyValuePair<BROWSERS, Browser> pair in browsers)
				pair.Value.LoadData();
		}

		public List<NetworkInterfaceSettings> GetNetworkInterfaceSettings() {
			return new List<NetworkInterfaceSettings>(interfaceManager.GetNetworkInterfaceSettings());
		}

		public Dictionary<BROWSERS, Browser> GetBrowsers() {
			return new Dictionary<BROWSERS, Browser>(browsers);
		}

		private bool ApplyProfile(Profile profile) {
			// TODO: MAC address
			// TODO: browsers
			foreach (ProfileNetworkSettings settings in profile.Connections.Where(settings => settings.Use)) {
				interfaceManager.Save(settings.Settings);
			}
			return true;
		}

		public void LoadProfiles() {
			var data = new DataStore();
			ILoaderFactory factory = data.GetLoaderFactory(LOADERS.XML);
			factory.InitString(".\\Data\\profiles.xml");
			profiles = factory.GetLoader().LoadProfiles();
		}

		public void StartDelayedLoading() {
			LoadData();
		}

		public void SaveSettings() {
			foreach (Profile profile in profiles)
				profile.PrepareSave();
			var data = new DataStore();
			ILoaderFactory factory = data.GetLoaderFactory(LOADERS.XML);
			factory.InitString(".\\Data\\profiles.xml");
			factory.GetLoader().SaveProfiles(profiles);
		}

		public bool Apply(string name) {
			var profile = GetProfile(name);
			if (profile == null)
				return false;
			return ApplyProfile(profile);
		}

		public Profile GetProfile(string name) {
			return profiles.Find(o => o.Name == name);
		}

		public Profile New() {
			var profile = new Profile {Name = GetNewProfileName(), Browsers = GetBrowsers()};
			return profile;
		}

		public Profile Clone(string name) {
			Profile old;
			if ((old = profiles.Find(o => o.Name == name)) == null)
				return New();
			return old.CloneProfile();
		}

		public void Add(Profile profile) {
			if (profiles.Contains(profile))
				return;
			profiles.Add(profile);
		}

		public void Delete(string name) {
			Profile profile;
			if ((profile = profiles.Find(o => o.Name == name)) != null)
				profiles.Remove(profile);
		}

		public void Change(string oldName, Profile profile) {
			var old = profiles.Find(item => item.Name == oldName);
			if (old != null) {
				int index = profiles.IndexOf(old);
				profiles.Remove(old);
				profiles.Insert(index, profile);
			}
			else
				profiles.Add(profile);
		}

		public string GetNewProfileName() {
			string newNameBase = Language.GetText("Profile");
			string newName = newNameBase;
			int offset = 1;
			while (profiles.Find(o => o.Name == newName) != null) {
				newName = newNameBase + " " + offset.ToString(CultureInfo.InvariantCulture);
				offset++;
			}
			return newName;
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing) {
			if (!disposed) {
				if (disposing) {
					interfaceManager.Dispose();
				}
			}
			disposed = true;
		}
	}
}
