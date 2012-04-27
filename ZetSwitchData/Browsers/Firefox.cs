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
		private bool init;
		private bool detected;

		private void Init() {
			var config = new FirefoxConfigReader();
			init = true;
			using (var file = new FirefoxConfigFile()) {
				TextReader reader = file.GetProfileFileReader();
				if (reader == null)
					return;
				profileName = config.GetProfileName(reader);
				file.SetConfigFileName(profileName);
				reader = file.GetConfigFileReader();
				if (reader == null)
					return;
				config.LoadConfig(reader);
			}
			detected = true;
		}

		public override string Name() {
			return @"Firefox";
		}

		public override void SetBrowserSettings(BrowserSettings settings) {
			if (!init)
				Init();
			var config = new FirefoxConfigReader();
			using (var file = new FirefoxConfigFile()) {
				file.SetConfigFileName(profileName);
				var reader = file.GetConfigFileReader();
				if (reader == null)
					return;
				config.LoadConfig(reader);
				reader.Dispose();
				config.SetHomePage(settings.HomePage);
				config.SetProxySettings(settings.Proxy);
				config.SaveConfig(file.GetConfigFileWriter());
			}
		}

		public override bool IsDetected() {
			if (!init)
				Init();
			return detected;
		}
	}
}
