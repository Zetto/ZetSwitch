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
using System.Text;
using Microsoft.Win32;

namespace ZetSwitchData.Browsers
{
	[Serializable]
    public class IE : Browser
	{

		#region private

		private void EnableProxy()
		{
			var key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
			if (key != null) 
				key.SetValue("ProxyEnable", 1);
		}

		private void DisableProxy()
		{
			var key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
			if (key != null) 
				key.SetValue("ProxyEnable", 0);
		}

		#endregion

		#region Browser
		protected override bool LoadProxySettings()
        {
            var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
			if (key == null)
				throw new Exception(ClientServiceLocator.GetService<ILanguage>().GetText("CannotOpenRegistry"));
			
			Proxy.Enabled = Convert.ToBoolean((int) key.GetValue("ProxyEnable"));
			if (Proxy.Enabled) {
				var servers = key.GetValue("ProxyServer").ToString().Split(';');
				if (servers.Length == 1)
				{
					var item = servers[0].Split(':');
					if (item.Length < 2)
						return false;
					Proxy.HTTP = item[0];
					Proxy.HPort = Convert.ToInt32(item[1]);
					Proxy.UseAdrForAll = true;
				}
				else if (servers.Length > 1)
				{
					Proxy.UseAdrForAll = false;
					foreach (var item in servers.Select(adr => adr.Split('=', ':')).Where(item => item.Length >= 3)) {
						switch (item[0]) {
							case "http":
								Proxy.HTTP = item[1];
								Proxy.HPort = Convert.ToInt32(item[2]);
								break;
							case "ftp":
								Proxy.FTP = item[1];
								Proxy.FTPPort = Convert.ToInt32(item[2]);
								break;
							case "socks":
								Proxy.Socks = item[1];
								Proxy.SocksPort = Convert.ToInt32(item[2]);
								break;
							case "https":
								Proxy.SSL = item[1];
								Proxy.SSLPort = Convert.ToInt32(item[2]);
								break;
							default:
								continue;
						}
					}
				}
				else
					return false;
			}
			return true;
        }

		protected override bool SaveProxySettings() {
            var key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
            if (key == null)
                throw new Exception(ClientServiceLocator.GetService<ILanguage>().GetText("CannotOpenRegistry"));

            if (Proxy.HTTP.Length == 0 && Proxy.FTP.Length == 0 && Proxy.Socks.Length == 0 && Proxy.SSL.Length == 0)
                return true;
            var servers = new StringBuilder();
            if (Proxy.Enabled) {
                if (Proxy.UseAdrForAll) {
                    servers.Append(Proxy.HTTP + ":" + Proxy.HPort.ToString(CultureInfo.InvariantCulture));
                }
                else {
                    if (Proxy.HTTP.Length != 0)
                        servers.Append("http=" + Proxy.HTTP + ":" + Proxy.HPort.ToString(CultureInfo.InvariantCulture) + ";");
                    if (Proxy.FTP.Length != 0)
                        servers.Append("ftpp=" + Proxy.FTP + ":" + Proxy.FTPPort.ToString(CultureInfo.InvariantCulture) + ";");
                    if (Proxy.Socks.Length != 0)
                        servers.Append("socks=" + Proxy.Socks + ":" + Proxy.SocksPort.ToString(CultureInfo.InvariantCulture) + ";");
                    if (Proxy.HTTP.Length != 0)
                        servers.Append("https=" + Proxy.SSL + ":" + Proxy.SSLPort.ToString(CultureInfo.InvariantCulture) + ";");
                }
                if (servers.Length != 0) {
                    key.SetValue("ProxyServer", servers.ToString());
                    EnableProxy();
                }
                else
                    return false;
            }
            else
            {
                DisableProxy();
            }
            return true;
        }

       

        protected override bool LoadHomePage() {
        	var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main");
        	if (key == null)
        		throw new Exception(ClientServiceLocator.GetService<ILanguage>().GetText("CannotOpenRegistry"));
        	HomePage = (string) key.GetValue("Start Page");
        	return true;
        }

		protected override bool SaveHomePage() {
			var key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Internet Explorer\\Main");
			if (key == null)
				throw new Exception(ClientServiceLocator.GetService<ILanguage>().GetText("CannotOpenRegistry"));
			key.SetValue("Start Page", HomePage);
			return true;
		}

		protected override bool Find() {
            var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer");
            return key != null;
        }

		#endregion

	}
    
}
