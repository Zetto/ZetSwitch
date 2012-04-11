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
using System.Xml;
using ZetSwitchData.Network;

namespace ZetSwitchData.Configuration
{
	class LoaderXmlv03 : ILoader
	{
		XmlDocument document;
		string fileName;

		const string IdRoot = "config";
		const string DefVersion = "0.3.0";
		const string IdVersion = "version";
		const string IdProfile = "profile";
		const string IdName = "name";
		const string IdIconFile = "iconFile";
		const string IdNetwork = "network";
		const string IdInterface = "interface";
		const string IdIp = "ip";
		const string IdMask = "mask";
		const string IdGW = "gw";
		const string IdDNS1 = "dns1";
		const string IdDNS2 = "dns2";
		const string IdAdapter = "adapter";
		const string IdDNSDHCP = "DNSDHCP";
		const string IdDHCP = "DHCP";
		
		#region private

		private ProfileNetworkSettingsList GetNetworkSettingsList(XmlNode node)
		{
			var list = new ProfileNetworkSettingsList();
			XmlNode actNode = node.FirstChild;
			while (actNode != null) {
				if (actNode.Name == IdInterface) {
					var settings = new ProfileNetworkSettings {Use = true};

					var elmnt = (XmlElement)actNode;
					settings.Settings.Name = elmnt.GetAttribute(IdName);
					settings.Settings.SettingId = elmnt.GetAttribute(IdAdapter);
					settings.Settings.IsDHCP = Boolean.Parse(elmnt.GetAttribute(IdDHCP));
					settings.Settings.IsDNSDHCP = Boolean.Parse(elmnt.GetAttribute(IdDNSDHCP));
					
					settings.Settings.IP = new IPAddress(elmnt.GetAttribute(IdIp));
					settings.Settings.Mask = new IPAddress(elmnt.GetAttribute(IdMask));
					settings.Settings.GateWay = new IPAddress(elmnt.GetAttribute(IdGW));
					settings.Settings.DNS1 = new IPAddress(elmnt.GetAttribute(IdDNS1));
					settings.Settings.DNS2 = new IPAddress(elmnt.GetAttribute(IdDNS2));
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
			var profile = new Profile {Name = profileNode.GetAttribute(IdName), 
				IconFile = profileNode.GetAttribute(IdIconFile)};

			XmlNode actNode = profileNode.FirstChild;
			while (actNode != null) {
				switch (actNode.Name) {
					case IdNetwork:
						profile.Connections = GetNetworkSettingsList(actNode);
						break;
				}
				actNode = actNode.NextSibling;
			}

			return profile;
		}

		#endregion

		#region public

		public void SetDocument(XmlDocument document, string fileName) {
			this.document = document;
			this.fileName = fileName;
		}

		public List<Profile> LoadProfiles()
		{
			if (document == null || document.DocumentElement == null)
				return new List<Profile>();
			var profiles = new List<Profile>();
			XmlNodeList list = document.DocumentElement.GetElementsByTagName(IdProfile);

			foreach (XmlNode node in list) {
				var profile = LoadProfile((XmlElement)node);
				if (profile != null)
					profiles.Add(profile);
			}
			return profiles;
		}

		private void SaveNetworkSettingsList(IEnumerable<ProfileNetworkSettings> list, XmlDocument doc,XmlElement parent) {
			XmlElement network = doc.CreateElement(IdNetwork);
			foreach (ProfileNetworkSettings settings in list) {
				XmlElement interf = doc.CreateElement(IdInterface);
				interf.SetAttribute(IdName, settings.Settings.Name);
				interf.SetAttribute(IdAdapter, settings.Settings.SettingId);
				interf.SetAttribute(IdDHCP, settings.Settings.IsDHCP.ToString(CultureInfo.InvariantCulture));
				interf.SetAttribute(IdDNSDHCP, settings.Settings.IsDNSDHCP.ToString(CultureInfo.InvariantCulture));
				if (!settings.Settings.IsDHCP) {
					interf.SetAttribute(IdIp, settings.Settings.IP.ToString());
					interf.SetAttribute(IdMask, settings.Settings.Mask.ToString());
					interf.SetAttribute(IdGW, settings.Settings.GateWay.ToString());
				}
				if (!settings.Settings.IsDNSDHCP)
				{
					interf.SetAttribute(IdDNS1, settings.Settings.DNS1.ToString());
					if (settings.Settings.DNS2 != null)
						interf.SetAttribute(IdDNS2, settings.Settings.DNS2.ToString());
				}
				network.AppendChild(interf);
			}
			parent.AppendChild(network);
		}

		private void SaveProfile(Profile profile, XmlDocument doc, XmlElement parent)
		{
			XmlElement elmnt = doc.CreateElement(IdProfile);
			elmnt.SetAttribute(IdName, profile.Name);
			elmnt.SetAttribute(IdIconFile, profile.IconFile);
			parent.AppendChild(elmnt);
			SaveNetworkSettingsList(profile.Connections,doc,elmnt);
		}

		public bool SaveProfiles(List<Profile> list)
		{
			if (string.IsNullOrEmpty(fileName))
				return false;

			var doc = new XmlDocument();
			XmlElement root = doc.CreateElement(IdRoot);
			root.SetAttribute(IdVersion, DefVersion);
			doc.AppendChild(root);

			foreach (var profile in list) {
				SaveProfile(profile, doc,root);				
			}
			doc.Save(fileName);
			return true;
		}

		#endregion
	}
}
