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
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using ZetSwitch.Browsers;
using ZetSwitch.Network;


namespace ZetSwitch {
	[Serializable]
	public class Profile {
		private bool useBrowser;

		public ProfileNetworkSettingsList Connections { get; set; }
		public Dictionary<BROWSERS, Browser> Browsers { protected get; set; }

		public Profile() {
			Name = "New";
			IconFile = "default";
			useBrowser = false;
			Connections = new ProfileNetworkSettingsList();
			Browsers = new Dictionary<BROWSERS, Browser>();
		}

		public Profile(Profile other) {
			Name = other.Name;
			IconFile = other.IconFile;
			useBrowser = other.useBrowser;
			Connections = new ProfileNetworkSettingsList(other.Connections);
			Browsers = new Dictionary<BROWSERS, Browser>(other.Browsers);
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

		public bool ContainsIF(string name) {
			return Connections.Contains(name);
		}

		public bool UseBrowser {
			get { return useBrowser; }
			set { useBrowser = value; }
		}

		public Browser GetBrowser(BROWSERS browser) {
			return Browsers[browser];
		}

		public List<string> GetNetworkInterfaceNames() {
			return Connections.GetNetworkInterfaceNames();
		}

		public void SetSelectedInterfaces(List<string> names) {
		}

		public void PrepareSave() {
			Connections.PrepareSave();
		}

		public bool IsNetworkInterfaceInProfile(string name) {
			return Connections.IsUsed(name);
		}

		public void UseNetworkInterface(string name, bool use) {
			Connections.UseNetworkInterface(name, use);
		}

		public void RemoveUnusedInterfaces() {
			Connections.RemoveUnused();
		}

		public string Name { get; set; }
		public string IconFile { get; set; }

		public Image GetIcon() {
			var images = ClientServiceLocator.GetService<IImageRepository>();
			Image image = Properties.Resources._default;
			try {
				image = images.GetImage(IconFile);
			}
			catch(Exception) {}
			return image;
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
			Connections.Add(new ProfileNetworkSettings(setting));
		}

		public bool Validation(List<string> errors) {
			if (Name.Length == 0)
				errors.Add("Jméno profilu nesmí být prázdné");
			return errors.Count == 0;
		}
	}
}
