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
using ZetSwitchData.Browsers;
using ZetSwitchData.Network;


namespace ZetSwitchData {
	[Serializable]
	public class UsedBrowser {
		public bool Used;
		public string Name = "";
		
		public UsedBrowser() {
		}

		public  UsedBrowser(string name, bool used) {
			Used = used;
			Name = name;
		}
	}

	public class Profile {

		public List<UsedBrowser> BrowserNames { get; set; }
		public BrowserSettings BrowserSettings { get; set; }

		public ProfileNetworkSettingsList Connections { get; set; }		
		public Profile() {
			BrowserSettings = new BrowserSettings();
			BrowserNames = new List<UsedBrowser>();
			Name = "New";
			IconFile = "default";
			Connections = new ProfileNetworkSettingsList();
		}

		public Profile(Profile other) {
			BrowserSettings = new BrowserSettings(other.BrowserSettings);
			Name = other.Name;
			BrowserNames = new List<UsedBrowser>(other.BrowserNames);
			IconFile = other.IconFile;
			Connections = new ProfileNetworkSettingsList(other.Connections);
		}

		public Profile CloneProfile() {
			return new Profile(this);
		}

		public bool ContainsIF(string name) {
			return Connections.Contains(name);
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

		public void AddNetworkInterface(NetworkInterfaceSettings setting) {
			Connections.Add(new ProfileNetworkSettings(setting));
		}

		public void AddBrowsersNames(List<string> names ) {
			foreach (var name in names) {
				if (!BrowserNames.Exists(x => x.Name == name)) {
					BrowserNames.Add(new UsedBrowser(name, false));
				}
			}
		}

		public bool Validation(List<string> errors) {
			if (Name.Length == 0)
				errors.Add("Jméno profilu nesmí být prázdné.");
			if (!BrowserSettings.Proxy.IsValid())
				errors.Add("Chyba v nastavení proxy.");
			return errors.Count == 0;
		}
	}
}
