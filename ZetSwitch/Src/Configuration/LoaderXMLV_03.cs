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
using System.Text;
using System.Xml;

namespace ZetSwitch
{
	class LoaderXMLV_03 : ILoader
	{
		XmlDocument document;
		string fileName;

		const string idRoot = "config";
		const string defVersion = "0.3.0";
		const string idVersion = "version";
		const string idProfile = "profile";
		const string idName = "name";
		const string idIconFile = "iconFile";
		const string idNetwork = "network";
		const string idInterface = "interface";
		const string idIp = "ip";
		const string idMask = "mask";
		const string idGW = "gw";
		const string idDNS1 = "dns1";
		const string idDNS2 = "dns2";
		const string idAdapter = "adapter";
		const string idDNSDHCP = "DNSDHCP";
		const string idDHCP = "DHCP";
		
		#region private

		private ProfileNetworkSettingsList GetNetworkSettingsList(XmlNode node)
		{
			ProfileNetworkSettingsList list = new ProfileNetworkSettingsList();
			XmlNode actNode = node.FirstChild;
			while (actNode != null)
			{
				if (actNode.Name == idInterface)
				{
					ProfileNetworkSettings settings = new ProfileNetworkSettings();
					settings.Use = true;

					XmlElement elmnt = (XmlElement)actNode;
					settings.Settings.Name = elmnt.GetAttribute(idName);
					settings.Settings.SettingId = elmnt.GetAttribute(idAdapter);
					settings.Settings.IsDHCP = Boolean.Parse(elmnt.GetAttribute(idDHCP));
					settings.Settings.IsDNSDHCP = Boolean.Parse(elmnt.GetAttribute(idDNSDHCP));
					
					settings.Settings.IP = new IPAddress(elmnt.GetAttribute(idIp));
					settings.Settings.Mask = new IPAddress(elmnt.GetAttribute(idMask));
					settings.Settings.GateWay = new IPAddress(elmnt.GetAttribute(idGW));
					settings.Settings.DNS1 = new IPAddress(elmnt.GetAttribute(idDNS1));
					settings.Settings.DNS2 = new IPAddress(elmnt.GetAttribute(idDNS2));
					list.Add(settings);
				}
				actNode = actNode.NextSibling;
			}
			return list;
		}

		private Profile LoadProfile(XmlElement profileNode)
		{
			if (profileNode == null)
				return null;
			Profile profile = new Profile();
			profile.Name = profileNode.GetAttribute(idName);
			profile.IconFile = profileNode.GetAttribute(idIconFile);
			
			XmlNode actNode = profileNode.FirstChild;
			while (actNode != null)
			{
				switch (actNode.Name)
				{
					case idNetwork:
						profile.Connections = GetNetworkSettingsList(actNode);
						break;
				}
				actNode = actNode.NextSibling;
			}

			return profile;
		}

		#endregion

		#region public

		public void SetDocument(XmlDocument document, string fileName)
		{
			this.document = document;
			this.fileName = fileName;
		}

		public List<Profile> LoadProfiles()
		{
			if (document == null || document.DocumentElement == null)
				return new List<Profile>();
			List<Profile> profiles = new List<Profile>();
			XmlNodeList list = document.DocumentElement.GetElementsByTagName(idProfile);

			foreach (XmlNode node in list)
			{
				Profile profile = LoadProfile((XmlElement)node);
				if (profile != null)
					profiles.Add(profile);
			}
			return profiles;
		}

		private void SaveNetworkSettingsList(ProfileNetworkSettingsList list, XmlDocument document,XmlElement parent)
		{
			XmlElement network = document.CreateElement(idNetwork);
			foreach (ProfileNetworkSettings settings in list)
			{
				XmlElement interf = document.CreateElement(idInterface);
				interf.SetAttribute(idName, settings.Settings.Name);
				interf.SetAttribute(idAdapter, settings.Settings.SettingId);
				interf.SetAttribute(idDHCP, settings.Settings.IsDHCP.ToString());
				interf.SetAttribute(idDNSDHCP, settings.Settings.IsDNSDHCP.ToString());
				if (!settings.Settings.IsDHCP)
				{
					interf.SetAttribute(idIp, settings.Settings.IP.ToString());
					interf.SetAttribute(idMask, settings.Settings.Mask.ToString());
					interf.SetAttribute(idGW, settings.Settings.GateWay.ToString());
				}
				if (!settings.Settings.IsDNSDHCP)
				{
					interf.SetAttribute(idDNS1, settings.Settings.DNS1.ToString());
					if (settings.Settings.DNS2 != null)
						interf.SetAttribute(idDNS2, settings.Settings.DNS2.ToString());
				}
				network.AppendChild(interf);
			}
			parent.AppendChild(network);
		}

		private void SaveProfile(Profile profile, XmlDocument document, XmlElement parent)
		{
			XmlElement elmnt = document.CreateElement(idProfile);
			elmnt.SetAttribute(idName, profile.Name);
			elmnt.SetAttribute(idIconFile, profile.IconFile);
			parent.AppendChild(elmnt);
			SaveNetworkSettingsList(profile.Connections,document,elmnt);
		}

		public bool SaveProfiles(List<Profile> list)
		{
			if (fileName == null || fileName.Length == 0)
				return false;

			XmlDocument document = new XmlDocument();
			XmlElement root = document.CreateElement(idRoot);
			root.SetAttribute(idVersion, defVersion);
			document.AppendChild(root);

			foreach (Profile profile in list)
			{
				SaveProfile(profile, document,root);				
			}
			document.Save(fileName);
			return true;
		}

		#endregion
	}
}
