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
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

using ZetSwitch.Browsers;
using ZetSwitch.Network;


namespace ZetSwitch
{
	[Serializable]
    public class Profile 
    {
        string name;
        string iconFile;
		bool useBrowser;

		protected ProfileNetworkSettingsList connections;
		protected Dictionary<BROWSERS, Browser> browsers;

		public Profile()
        {
			name = "New";
			iconFile = "default";
			useBrowser = false;
			connections = new ProfileNetworkSettingsList();
			browsers = new Dictionary<BROWSERS, Browser>();
        }

		public Profile(Profile other)
		{
			name = other.name;
			iconFile = other.iconFile;
			useBrowser = other.useBrowser;
			connections = new ProfileNetworkSettingsList(other.connections);
			browsers = new Dictionary<BROWSERS, Browser>(other.browsers);
		}

		public Profile cloneProfile() 
		{
			Profile result = null;

			using (MemoryStream ms = new MemoryStream())
			{
				var formatter = new BinaryFormatter();
				formatter.Serialize(ms, this);
				ms.Position = 0;
				result = (Profile)formatter.Deserialize(ms);
			}

			return result;
		}

		public ProfileNetworkSettingsList Connections
		{
			get { return connections; }
			set { connections = value; }
		}

		public Dictionary<BROWSERS, Browser> Browsers
		{
			set { browsers = value; }
		}

		public bool UseBrowser
		{
			get { return useBrowser; }
			set { useBrowser = value; }
		}
		
		public Browser GetBrowser(BROWSERS browser) 
		{
			return browsers[browser];
		}

		public List<string> GetNetworkInterfaceNames()
		{
			return connections.GetNetworkInterfaceNames();
		}

		public void SetSelectedInterfaces(List<string> names)
		{
		}

		public void MergeNetworkInterfaces(List<NetworkInterfaceSettings> other)
		{
			connections.MergeNetworkInterfaces(other);
		}

		public void PrepareSave()
		{
			connections.PrepareSave();
		}

		public bool IsNetworkInterfaceInProfile(string name)
		{
			return connections.IsUsed(name);
		}

		public void UseNetworkInterface(string name, bool use)
		{
			connections.UseNetworkInterface(name, use);
		}

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string IconFile
        {
            get { return iconFile; }
            set { iconFile = value; }
        }

		public override int GetHashCode()
		{
			return name.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Profile other = (Profile)obj;
			if (other == null)
				return false;
			return other.Name == name;
		}
    }
}
