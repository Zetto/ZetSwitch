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
using System.Globalization;
using System.Text;
using Microsoft.Win32;

namespace ZetSwitchData.Browsers {
	[Serializable]
	public class IE : Browser {
		
		private bool EnableProxy() {
			try {
				var key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
				if (key != null) {
					key.SetValue("ProxyEnable", 1);
					return true;
				}
			}
			catch(Exception) {}
			return false;
		}

		private bool DisableProxy() {
			try {
				var key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
				if (key != null) {
					key.SetValue("ProxyEnable", 0);
					return true;
				}
			}
			catch(Exception) {
			}
			return false;
		}

		protected bool SaveProxySettings(ProxySettings proxy) {
			var key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
			if (key == null)
				throw new Exception(ClientServiceLocator.GetService<ILanguage>().GetText("CannotOpenRegistry"));

			if (proxy.HTTP.Length == 0 && proxy.FTP.Length == 0 && proxy.Socks.Length == 0 && proxy.SSL.Length == 0) 
				return DisableProxy();
			var servers = new StringBuilder();
			if (proxy.UseAdrForAll) {
				servers.Append(proxy.HTTP + ":" + proxy.HTTPPort.ToString(CultureInfo.InvariantCulture));
			}
			else {
				if (proxy.HTTP.Length != 0)
					servers.Append("http=" + proxy.HTTP + ":" + proxy.HTTPPort.ToString(CultureInfo.InvariantCulture) + ";");
				if (proxy.FTP.Length != 0)
					servers.Append("ftp=" + proxy.FTP + ":" + proxy.FTPPort.ToString(CultureInfo.InvariantCulture) + ";");
				if (proxy.Socks.Length != 0)
					servers.Append("socks=" + proxy.Socks + ":" + proxy.SocksPort.ToString(CultureInfo.InvariantCulture) + ";");
				if (proxy.HTTP.Length != 0)
					servers.Append("https=" + proxy.SSL + ":" + proxy.SSLPort.ToString(CultureInfo.InvariantCulture) + ";");
			}
			if (servers.Length != 0) {
				key.SetValue("ProxyServer", servers.ToString());
				return EnableProxy();
			}
			return true;
		}

		private void SaveHomePage(string homePage) {
			try {
				var key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Internet Explorer\\Main");
				if (key == null)
					throw new Exception(ClientServiceLocator.GetService<ILanguage>().GetText("CannotOpenRegistry"));
				key.SetValue("Start Page", homePage);
			}
			catch(Exception) {
			}
		}

		public override string Name() {
			return @"Internet Explorer";
		}

		public override void SetBrowserSettings(BrowserSettings settings) {
			SaveHomePage(settings.HomePage);
			SaveProxySettings(settings.Proxy);
		}

		public override bool IsDetected() {
			try {
				var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer");
				return key != null;
			}
			catch(Exception) {
			}
			return false;
		}
	}
}
