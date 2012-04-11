﻿/////////////////////////////////////////////////////////////////////////////
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
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace ZetSwitchData.Browsers
{
	[Serializable]
	public class Firefox : Browser
	{
		[NonSerialized] 
		readonly Hashtable config;
		string pathToConfig;

		public Firefox()
		{
			config = new Hashtable();
			pathToConfig = "";
		}

		#region private

		private string GetConfig(string item)
		{
			return config.ContainsKey(item) ? config[item].ToString() : "";
		}

		private int GetConfigInt(string item) {
			var i = config.ContainsKey(item) ? (string)config[item] : "";
			return i.Length == 0 ? 0 : Convert.ToInt32(i);
		}

		private bool FindConfigFileAddres() {
			
			if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox"))
				throw new FileNotFoundException(ClientServiceLocator.GetService<ILanguage>().GetText("FirefoxSettingsFileNotFound"));
			using (var f = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\profiles.ini")) {
				pathToConfig = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\";
				string line;
				while ((line = f.ReadLine()) != null) {
					var conf = line.Split('=');  //TODO: nalezeni spravneho profilu
					if (conf.Length != 2)
						continue;
					if (conf[0] != "Path") continue;
					
					pathToConfig += conf[1].Replace('/', '\\');
					pathToConfig += "\\prefs.js";
					if (!File.Exists(pathToConfig))
						throw new Exception(ClientServiceLocator.GetService<ILanguage>().GetText("FirefoxSettingsFileNotFound"));
					return true;
				}
			}
			throw new Exception(ClientServiceLocator.GetService<ILanguage>().GetText("FirefoxSettingsFileNotFound"));
		}

		#endregion

		#region Browser

		protected override bool LoadProxySettings()
		{
			try
			{
				HomePage = GetConfig("\"browser.startup.homepage\"");
				Proxy.HTTP = GetConfig("\"network.proxy.http\"");
				Proxy.HPort = GetConfigInt("\"network.proxy.http_port\"");
				Proxy.FTP = GetConfig("\"network.proxy.ftp\"");
				Proxy.FTPPort = GetConfigInt("\"network.proxy.ftp_port\"");
				Proxy.Socks = GetConfig("\"network.proxy.socks\"");
				Proxy.SocksPort = GetConfigInt("\"network.proxy.socks_port\"");
				Proxy.SSL = GetConfig("\"network.proxy.ssl\"");
				Proxy.SSLPort = GetConfigInt("\"network.proxy.ssl_port\"");

				Proxy.Enabled = GetConfig("\"network.proxy.type\"") == "1";
			}
			catch (Exception e) {
				Trace.WriteLine(e.StackTrace);
				Trace.WriteLine(e.Message);
				return false;
			}
			return true;
		}

		protected override bool SaveProxySettings()
		{

			return true;
		}


		protected override bool LoadHomePage()
		{
			return true;
		}

		protected override bool SaveHomePage()
		{
			return true;
		}

		protected override bool Find()
		{
			return FindConfigFileAddres();
		}

		#endregion

		#region public

		public override bool LoadData()
		{
			if (pathToConfig.Length == 0)
			{
				try { FindConfigFileAddres(); }
				catch (Exception) { return false; }
			}

			using (var f = new StreamReader(pathToConfig)) {
				string line;
				while ((line = f.ReadLine()) != null) {
					if (line.IndexOf("user_pref", 0, StringComparison.Ordinal) != 0)  //non config lines
						continue;
					var data = line.Substring(10, line.Length - 12);
					var items = data.Split(',');
					if (items.Length != 2)
						continue;
					try {
						config[items[0]] = items[1][1] == '\"' ? items[1].Substring(2, items[1].Length - 3) : items[1];
					} catch (Exception)
					{ }
				}
			}

			base.LoadData();
			return true;
		}

		#endregion
	}
}