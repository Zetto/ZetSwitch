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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using ZetSwitch.Browsers;
using ZetSwitch.Network;


namespace ZetSwitch {
	[Serializable]
	public class Profile {
		private string iconFile;
		private bool useBrowser;

		protected ProfileNetworkSettingsList connections;
		protected Dictionary<BROWSERS, Browser> browsers;

		public Profile() {
			Name = "New";
			iconFile = "default";
			useBrowser = false;
			connections = new ProfileNetworkSettingsList();
			browsers = new Dictionary<BROWSERS, Browser>();
		}

		public Profile(Profile other) {
			Name = other.Name;
			iconFile = other.iconFile;
			useBrowser = other.useBrowser;
			connections = new ProfileNetworkSettingsList(other.connections);
			browsers = new Dictionary<BROWSERS, Browser>(other.browsers);
		}

		public Profile CloneProfile() {
			Profile result;

			using (var ms = new MemoryStream()) {
				var formatter = new BinaryFormatter();
				formatter.Serialize(ms, this);
				ms.Position = 0;
				result = (Profile) formatter.Deserialize(ms);
			}

			return result;
		}

		public ProfileNetworkSettingsList Connections {
			get { return connections; }
			set { connections = value; }
		}

		public bool ContainsIF(string name) {
			return connections.Contains(name);
		}

		public Dictionary<BROWSERS, Browser> Browsers {
			set { browsers = value; }
		}

		public bool UseBrowser {
			get { return useBrowser; }
			set { useBrowser = value; }
		}

		public Browser GetBrowser(BROWSERS browser) {
			return browsers[browser];
		}

		public List<string> GetNetworkInterfaceNames() {
			return connections.GetNetworkInterfaceNames();
		}

		public void SetSelectedInterfaces(List<string> names) {
		}

		public void PrepareSave() {
			connections.PrepareSave();
		}

		public bool IsNetworkInterfaceInProfile(string name) {
			return connections.IsUsed(name);
		}

		public void UseNetworkInterface(string name, bool use) {
			connections.UseNetworkInterface(name, use);
		}

		public void RemoveUnusedItenrfaces() {
			connections.RemoveUnused();
		}

		public string Name { get; set; }

		public string IconFile {
			get { return iconFile; }
			set { iconFile = value; }
		}

		public override int GetHashCode() {
			return Name.GetHashCode();
		}

		public override bool Equals(object obj) {
			var other = obj as Profile;
			if (other == null)
				return false;
			return other.Name == Name;
		}

		internal void AddNetworkInterface(NetworkInterfaceSettings setting) {
			connections.Add(new ProfileNetworkSettings(setting));
		}
	}
}
