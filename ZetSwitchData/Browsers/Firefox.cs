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

using System.IO;

namespace ZetSwitchData.Browsers.FF {
	public class Firefox : Browser {
		private string profileName = "";
		private ProxySettings proxy = new ProxySettings();
		private string homePage = "";

		public Firefox() {
		}

		public Firefox(Firefox other) {
			IsDetected = other.IsDetected;
			proxy = new ProxySettings(other.proxy);
			homePage = other.homePage;
			profileName = other.profileName;
		}

		public override  bool Init() {
			var config = new FirefoxConfigReader();
			using (var file = new FirefoxConfigFile()) {
				TextReader reader = file.GetProfileFileReader();
				if (reader == null)
					return false;
				profileName = config.GetProfileName(reader);
				file.SetConfigFileName(profileName);
				reader = file.GetConfigFileReader();
				if (reader == null)
					return false;
				config.LoadConfig(reader);
				proxy = config.ProxySettings();
				homePage = config.Homepage();
			}
			IsDetected = true;
			return true;
		}

		public override void Save() {
			var config = new FirefoxConfigReader();
			using (var file = new FirefoxConfigFile()) {
				file.SetConfigFileName(profileName);
				var reader = file.GetConfigFileReader();
				if (reader == null)
					return;
				config.LoadConfig(reader);
				config.SetHomePage(homePage);
				config.SetProxySettings(proxy);
				config.SaveConfig(file.GetConfigFileWriter());
			}
		}

		public override ProxySettings ProxySettings() {
			return proxy;
		}

		public override void SetProxySettings(ProxySettings settings) {
			proxy = settings;
		}

		public override  string HomePage() {
			return homePage;
		}

		public override void SetHomePage(string page) {
			homePage = page;
		}
	}
}
