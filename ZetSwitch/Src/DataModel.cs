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

using ZetSwitch.Browsers;
using ZetSwitch.Network;

namespace ZetSwitch
{
	public class DataModel
	{
		NetworkManager interfaceManager = new NetworkManager();
		Dictionary<BROWSERS, Browser> browsers = new Dictionary<BROWSERS, Browser>();
		
		public DataModel()
		{
			browsers[BROWSERS.IE] = BrowserFactory.createBrowser(BROWSERS.IE);
			browsers[BROWSERS.FIREFOX] = BrowserFactory.createBrowser(BROWSERS.FIREFOX);
		}

		public bool LoadData()
		{
			interfaceManager.LoadData();
			foreach (KeyValuePair<BROWSERS, Browser> pair in browsers)
				pair.Value.LoadData();
			return true;
		}

		public List<NetworkInterfaceSettings> GetNetworkInterfaceSettings() 
		{
			return new List<NetworkInterfaceSettings>(interfaceManager.GetNetworkInterfaceSettings());
		}

		public Dictionary<BROWSERS, Browser> GetBrowsers()
		{
			return new Dictionary<BROWSERS, Browser>(browsers);
		}

		public bool ApplyProfile(Profile profile) 
		{
			// TODO: MAC address
			// TODO: browsers
			foreach (ProfileNetworkSettings settings in profile.Connections)
			{
				if (settings.Use /*&& settings.UseNetwork*/)
					interfaceManager.Save(settings.Settings);
			}
			return true;
		}
	}
}
