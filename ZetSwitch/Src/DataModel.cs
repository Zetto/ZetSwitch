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
using ZetSwitch.Browsers;
using ZetSwitch.Network;

namespace ZetSwitch {
	public class DataModel : IDisposable {
		private readonly NetworkManager interfaceManager = new NetworkManager();
		private readonly Dictionary<BROWSERS, Browser> browsers = new Dictionary<BROWSERS, Browser>();

		private bool disposed;

		public DataModel() {
			browsers[BROWSERS.Ie] = BrowserFactory.CreateBrowser(BROWSERS.Ie);
			browsers[BROWSERS.Firefox] = BrowserFactory.CreateBrowser(BROWSERS.Firefox);
			interfaceManager.DataLoaded += InterfaceManagerDataLoaded;
		}

		~DataModel() {
			Dispose(false);
		}

		public bool IsIFLoaded() {
			return interfaceManager.IsLoaded();
		}

		private void InterfaceManagerDataLoaded(object o, EventArgs e) {
			if (DataLoaded != null)
				DataLoaded(this, null);
		}

		public event EventHandler DataLoaded;

		public bool LoadData() {
			interfaceManager.StartLoad();
			foreach (KeyValuePair<BROWSERS, Browser> pair in browsers)
				pair.Value.LoadData();
			return true;
		}

		public List<NetworkInterfaceSettings> GetNetworkInterfaceSettings() {
			return new List<NetworkInterfaceSettings>(interfaceManager.GetNetworkInterfaceSettings());
		}

		public Dictionary<BROWSERS, Browser> GetBrowsers() {
			return new Dictionary<BROWSERS, Browser>(browsers);
		}

		public bool ApplyProfile(Profile profile) {
			// TODO: MAC address
			// TODO: browsers
			foreach (ProfileNetworkSettings settings in profile.Connections.Where(settings => settings.Use)) {
				interfaceManager.Save(settings.Settings);
			}
			return true;
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing) {
			if (!disposed) {
				if (disposing) {
					interfaceManager.Dispose();
				}
			}
			disposed = true;
		}
	}
}
