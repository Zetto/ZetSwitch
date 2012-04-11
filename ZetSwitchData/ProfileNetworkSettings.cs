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
using ZetSwitchData.Network;

namespace ZetSwitchData {
	[Serializable]
	public class ProfileNetworkSettings {
		private bool use;
		private bool useNetwork;
		private bool useMac;

		private NetworkInterfaceSettings settings;

		public bool Use {
			get { return use; }
			set { use = value; }
		}

		public bool UseNetwork {
			get { return useNetwork; }
			set { useNetwork = value; }
		}

		public bool UseMac {
			get { return useMac; }
			set { useMac = value; }
		}

		public NetworkInterfaceSettings Settings {
			get { return settings; }
			set { settings = value; }
		}

		public ProfileNetworkSettings() {
			settings = new NetworkInterfaceSettings();
		}

		public ProfileNetworkSettings(NetworkInterfaceSettings settings) {
			this.settings = settings;
		}
	}

	[Serializable]
	public class ProfileNetworkSettingsList : List<ProfileNetworkSettings> {
		public ProfileNetworkSettingsList() {
		}

		public ProfileNetworkSettingsList(IEnumerable<ProfileNetworkSettings> other) : base(other) {
		}

		public ProfileNetworkSettingsList(IEnumerable<NetworkInterfaceSettings> other) {
			foreach (NetworkInterfaceSettings setting in other) {
				Add(new ProfileNetworkSettings(setting));
			}
		}

		public ProfileNetworkSettings GetProfileNetworkSettings(string name) {
			return Find(item => item.Settings.Name == name);
		}

		public NetworkInterfaceSettings GetNetworkSettings(string name) {
			ProfileNetworkSettings set = GetProfileNetworkSettings(name);
			return set == null ? null : set.Settings;
		}

		public List<string> GetNetworkInterfaceNames() {
			return this.Select(setting => setting.Settings.Name).ToList();
		}

		public ProfileNetworkSettings GetSetting(string name) {
			return Find(item => item.Settings.Name == name);
		}

		public bool Contains(string name) {
			return GetSetting(name) != null;
		}

		public void PrepareSave() {
			var lst = new ProfileNetworkSettingsList();
			lst.AddRange(this.Where(setting => setting.Use));
			Clear();
			AddRange(lst);
		}

		public bool IsUsed(string name) {
			return Find(item => item.Settings.Name == name && item.Use) != null;
		}

		public void UseNetworkInterface(string name, bool use) {
			ProfileNetworkSettings set = GetProfileNetworkSettings(name);
			if (set == null)
				return;
			set.Use = use;
		}

		internal void RemoveUnused() {
			var items = FindAll(item => item.Use == false);
			foreach (var item in items)
				Remove(item);
		}
	}
}
