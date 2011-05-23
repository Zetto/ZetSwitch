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
using System.Text;
using Microsoft.Win32;

namespace ZetSwitch.Browsers
{
	[Serializable]
    public class IE : Browser
	{

		#region private

		private void EnableProxy()
		{
			RegistryKey Key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
			Key.SetValue("ProxyEnable", 1);
		}

		private void DisableProxy()
		{
			RegistryKey Key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
			Key.SetValue("ProxyEnable", 0);
		}

		#endregion

		#region Browser
		protected override bool LoadProxySettings()
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
            if (Key != null)
            {

                Proxy.Enabled = Convert.ToBoolean((int)Key.GetValue("ProxyEnable"));
                if (Proxy.Enabled)
                {
                    string[] Servers = Key.GetValue("ProxyServer").ToString().Split(';');
                    if (Servers.Length == 1)
                    {
                        string[] Item = Servers[0].Split(':');
                        if (Item.Length < 2)
                            return false;
                        Proxy.HTTP = Item[0];
                        Proxy.HPort = Convert.ToInt32(Item[1]);
                        Proxy.UseAdrForAll = true;
                    }
                    else if (Servers.Length > 1)
                    {
                        Proxy.UseAdrForAll = false;
                        foreach (string Adr in Servers)
                        {
                            string[] Item = Adr.Split('=', ':');
                            if (Item.Length < 3)
                                continue;
                            switch (Item[0])
                            {
                                case "http":
                                    Proxy.HTTP = Item[1];
                                    Proxy.HPort = Convert.ToInt32(Item[2]);
                                    break;
                                case "ftp":
                                    Proxy.FTP = Item[1];
                                    Proxy.FTPPort = Convert.ToInt32(Item[2]);
                                    break;
                                case "socks":
                                    Proxy.Socks = Item[1];
                                    Proxy.SocksPort = Convert.ToInt32(Item[2]);
                                    break;
                                case "https":
                                    Proxy.SSL = Item[1];
                                    Proxy.SSLPort = Convert.ToInt32(Item[2]);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                    else
                        return false;
                }

            }
            else
                throw new Exception(Language.GetText("CannotOpenRegistry"));
            return true;
        }

		protected override bool SaveProxySettings()
        {
            RegistryKey Key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");

            if (Key == null)
                throw new Exception(Language.GetText("CannotOpenRegistry"));

            if (Proxy.HTTP.Length == 0 && Proxy.FTP.Length == 0 && Proxy.Socks.Length == 0 && Proxy.SSL.Length == 0)
                return true;
            StringBuilder Servers = new StringBuilder();
            if (Proxy.Enabled)
            {
                if (Proxy.UseAdrForAll)
                {
                    Servers.Append(Proxy.HTTP + ":" + Proxy.HPort.ToString());
                }
                else
                {
                    if (Proxy.HTTP.Length != 0)
                        Servers.Append("http=" + Proxy.HTTP + ":" + Proxy.HPort.ToString() + ";");
                    if (Proxy.FTP.Length != 0)
                        Servers.Append("ftpp=" + Proxy.FTP + ":" + Proxy.FTPPort.ToString() + ";");
                    if (Proxy.Socks.Length != 0)
                        Servers.Append("socks=" + Proxy.Socks + ":" + Proxy.SocksPort.ToString() + ";");
                    if (Proxy.HTTP.Length != 0)
                        Servers.Append("https=" + Proxy.SSL + ":" + Proxy.SSLPort.ToString() + ";");
                }
                if (Servers.Length != 0)
                {
                    Key.SetValue("ProxyServer", Servers.ToString());
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

       

        protected override bool LoadHomePage()
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main");
            if (Key != null)
            {
                HomePage = (string)Key.GetValue("Start Page");
                return true;
            }
			else
				throw new Exception(Language.GetText("CannotOpenRegistry"));
        }

		protected override bool SaveHomePage()
        {
            RegistryKey Key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Internet Explorer\\Main");
            if (Key != null)
            {
                Key.SetValue("Start Page", HomePage);
                return true;
            }
			else
				throw new Exception(Language.GetText("CannotOpenRegistry"));
        }

		protected override bool Find()
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer");
            return Key != null;
        }

		#endregion

	}
    
}
