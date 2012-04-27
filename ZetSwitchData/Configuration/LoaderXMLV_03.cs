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
using System.Xml;
using ZetSwitchData.Browsers;
using ZetSwitchData.Network;

namespace ZetSwitchData.Configuration {
	internal class LoaderXmlv03 : ILoader {
		private XmlDocument document;
		private string saveName = "";

		private const string IdRoot = "config";
		private const string DefVersion = "0.3.0";
		private const string IdVersion = "version";
		private const string IdProfile = "profile";
		private const string IdName = "name";
		private const string IdIconFile = "iconFile";
		private const string IdNetwork = "network";
		private const string IdInterface = "interface";
		private const string IdIp = "ip";
		private const string IdMask = "mask";
		private const string IdGW = "gw";
		private const string IdDNS1 = "dns1";
		private const string IdDNS2 = "dns2";
		private const string IdAdapter = "adapter";
		private const string IdDNSDHCP = "DNSDHCP";
		private const string IdDHCP = "DHCP";

		private const string IDBrowsers = "browsers";
		private const string IDBrowser = "browser";
		private const string IDProxy = "proxy";
		private const string IDHTTP = "HTTP";
		private const string IDSSL = "SSL";
		private const string IDFTP = "FTP";
		private const string IDSocks = "Socks";
		private const string IDPort = "port";
		private const string IDHomePage = "homePage";

		private ProfileNetworkSettingsList GetNetworkSettingsList(XmlNode node) {
			var list = new ProfileNetworkSettingsList();
			XmlNode actNode = node.FirstChild;
			while (actNode != null) {
				if (actNode.Name == IdInterface) {
					var settings = new ProfileNetworkSettings {Use = true};

					var elmnt = (XmlElement) actNode;
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

		private Profile LoadProfile(XmlElement profileNode) {
			if (profileNode == null)
				return null;
			var profile = new Profile {
			                          	Name = profileNode.GetAttribute(IdName),
			                          	IconFile = profileNode.GetAttribute(IdIconFile)
			                          };

			XmlNode actNode = profileNode.FirstChild;
			while (actNode != null) {
				switch (actNode.Name) {
					case IdNetwork:
						profile.Connections = GetNetworkSettingsList(actNode);
						break;
					case IDBrowsers:
						profile.BrowserSettings.Proxy = GetProxySettings((XmlElement)actNode);
						profile.BrowserSettings.HomePage = GetHomePage((XmlElement)actNode);
						profile.BrowserNames = GetBrowsersList((XmlElement)actNode);
						break;

				}
				actNode = actNode.NextSibling;
			}

			return profile;
		}

		private List<UsedBrowser> GetBrowsersList(XmlElement node) {
			XmlNodeList lst = node.GetElementsByTagName(IDBrowser);
			var browsers = new List<UsedBrowser>();
			foreach (XmlElement elmnt in lst) 
				browsers.Add(new UsedBrowser(elmnt.GetAttribute(IdName), true));

			return browsers;
		}

		private string GetHomePage(XmlElement node) {
			return node.GetAttribute(IDHomePage);
		}

		private int GetInt(string v) {
			int o;
			Int32.TryParse(v, out o);
			return o;
		} 

		private ProxySettings GetProxySettings(XmlElement node) {
			XmlElement proxy = node.ChildNodes.OfType<XmlElement>().First(x => x.Name == IDProxy);
			string HTTP = "";
			int HTTPPort = 0;
			string FTP = "";
			int FTPPort = 0;
			string Socks = "";
			int SocksPort = 0;
			string SSL = "";
			int SSLPort = 0;
			XmlElement elmnt = proxy.ChildNodes.OfType<XmlElement>().First(x => x.Name == IDHTTP);
			if (elmnt != null) {
				HTTP = elmnt.GetAttribute(IdIp);
				HTTPPort = GetInt(elmnt.GetAttribute(IDPort));
			}

			elmnt = proxy.ChildNodes.OfType<XmlElement>().First(x => x.Name == IDFTP);
			if (elmnt != null) {
				FTP = elmnt.GetAttribute(IdIp);
				FTPPort = GetInt(elmnt.GetAttribute(IDPort));
			}

			elmnt = proxy.ChildNodes.OfType<XmlElement>().First(x => x.Name == IDSSL);
			if (elmnt != null) {
				SSL = elmnt.GetAttribute(IdIp);
				SSLPort = GetInt(elmnt.GetAttribute(IDPort));
			}

			elmnt = proxy.ChildNodes.OfType<XmlElement>().First(x => x.Name == IDSocks);
			if (elmnt != null) {
				Socks = elmnt.GetAttribute(IdIp);
				SocksPort = GetInt(elmnt.GetAttribute(IDPort));
			}
			return new ProxySettings(true,HTTP,HTTPPort,FTP,FTPPort,SSL,SSLPort,Socks,SocksPort);
		}

		#region public

		public void SetDocument(XmlDocument doc,string save) {
			document = doc;
			saveName = save;
		}

		public List<Profile> LoadProfiles() {
			if (document == null || document.DocumentElement == null)
				return new List<Profile>();
			var profiles = new List<Profile>();
			XmlNodeList list = document.DocumentElement.GetElementsByTagName(IdProfile);

			foreach (XmlNode node in list) {
				var profile = LoadProfile((XmlElement) node);
				if (profile != null)
					profiles.Add(profile);
			}
			return profiles;
		}

		private void SaveBrowsers(BrowserSettings browserSettings, List<string> names, XmlDocument doc, XmlElement parent) {
			XmlElement browsers = doc.CreateElement(IDBrowsers);

			browsers.SetAttribute(IDHomePage, browserSettings.HomePage);

			foreach (var name in names) {
				XmlElement browser = doc.CreateElement(IDBrowser);
				browser.SetAttribute(IdName, name);
				browsers.AppendChild(browser);
			}
			
		
			XmlElement proxy = doc.CreateElement(IDProxy);

			ProxySettings settings = browserSettings.Proxy;

			XmlElement elmnt = doc.CreateElement(IDHTTP);
			elmnt.SetAttribute(IdIp, settings.HTTP);
			elmnt.SetAttribute(IDPort, settings.HTTPPort.ToString(CultureInfo.InvariantCulture));
			proxy.AppendChild(elmnt);

			elmnt = doc.CreateElement(IDFTP);
			elmnt.SetAttribute(IdIp, settings.FTP);
			elmnt.SetAttribute(IDPort, settings.FTPPort.ToString(CultureInfo.InvariantCulture));
			proxy.AppendChild(elmnt);

			elmnt = doc.CreateElement(IDSSL);
			elmnt.SetAttribute(IdIp, settings.SSL);
			elmnt.SetAttribute(IDPort, settings.SSLPort.ToString(CultureInfo.InvariantCulture));
			proxy.AppendChild(elmnt);

			elmnt = doc.CreateElement(IDSocks);
			elmnt.SetAttribute(IdIp, settings.Socks);
			elmnt.SetAttribute(IDPort, settings.SocksPort.ToString(CultureInfo.InvariantCulture));
			proxy.AppendChild(elmnt);

			browsers.AppendChild(proxy);
			parent.AppendChild(browsers);

		}

		private void SaveNetworkSettingsList(IEnumerable<ProfileNetworkSettings> list, XmlDocument doc, XmlElement parent) {
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
				if (!settings.Settings.IsDNSDHCP) {
					interf.SetAttribute(IdDNS1, settings.Settings.DNS1.ToString());
					if (settings.Settings.DNS2 != null)
						interf.SetAttribute(IdDNS2, settings.Settings.DNS2.ToString());
				}
				network.AppendChild(interf);
			}
			parent.AppendChild(network);
		}

		private void SaveProfile(Profile profile, XmlDocument doc, XmlElement parent) {
			XmlElement elmnt = doc.CreateElement(IdProfile);
			elmnt.SetAttribute(IdName, profile.Name);
			elmnt.SetAttribute(IdIconFile, profile.IconFile);
			parent.AppendChild(elmnt);
			SaveNetworkSettingsList(profile.Connections, doc, elmnt);
			SaveBrowsers(profile.BrowserSettings, profile.BrowserNames.Where(x=>x.Used).Select(x => x.Name).ToList(), 
				doc, elmnt);
		}

		public bool SaveProfiles(List<Profile> list) {
			if (string.IsNullOrEmpty(saveName))
				return false;
			
			var doc = new XmlDocument();
			XmlElement root = doc.CreateElement(IdRoot);
			root.SetAttribute(IdVersion, DefVersion);
			doc.AppendChild(root);

			foreach (var profile in list) {
				SaveProfile(profile, doc, root);
			}
			doc.Save(saveName);
			return true;
		}

		#endregion
	}
}
