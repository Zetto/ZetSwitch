using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace ZetSwitchData.Browsers.FF {
	public class ConfigFileValue {
		private string v = "";

		private bool valid;
		public bool Valid { get { return valid; } }

		public string Value {
			get { return v; }
			set {
				Comma = value.Length > 0 && value[0] == '\"';
				v = Comma ? value.Substring(1, value.Length - 2) : value; // remove '\"' from string value
				valid = v.Length > 0 || Comma;
			}
		}

		public bool Comma { get; set; }

		public override bool Equals(object obj) {
			var other = obj as ConfigFileValue;
			if (other == null)
				return false;
			return Value == other.Value && Comma == other.Comma;
		}

		public override int GetHashCode() {
			return (Comma.ToString(CultureInfo.InvariantCulture) + Value).GetHashCode();
		}
	}

	public class ConfigLine {
		public bool Comment { get; set; }
		public string Key { get; set; }
		public ConfigFileValue Value { get; set; }

		public ConfigLine() {
			Key = "";
			Value = new ConfigFileValue();
		}
	}

	public class FirefoxConfigReader {
		private readonly StringBuilder comment = new StringBuilder();
		private readonly Dictionary<string, ConfigFileValue> config = new Dictionary<string, ConfigFileValue>();

		public ConfigLine ReadLine(string line) {
			if (line.IndexOf("user_pref", 0, StringComparison.Ordinal) != 0) 
				return new ConfigLine {Comment = true};
			var data = line.Substring(10, line.Length - 12);
			int del = data.IndexOf(',');
			if (del < 0 || del - 2 <= 0) 
				return new ConfigLine();
			
			string key = data.Substring(1, del - 2);
			if (key.Length == 0) 
				return new ConfigLine();
			var value = new ConfigFileValue { Value = data.Substring(del + 2, data.Length - del - 2) };
			return new ConfigLine {Key = key, Value = value};
		}

		public void LoadConfig(TextReader reader) {
			config.Clear();
			string line;
			while ((line = reader.ReadLine()) != null) {
				ConfigLine l = ReadLine(line);
				if (l.Comment) {
					comment.Append(line);
					comment.Append("\n");
				}
				else if (l.Value.Valid)
					config[l.Key] = l.Value;
			}
		}

		public string GetProfileName(TextReader reader) {
			string name = "";
			string line;
			while ((line = reader.ReadLine()) != null) {
				var conf = line.Split('='); //TODO: nalezeni spravneho profilu
				if (conf.Length != 2)
					continue;
				if (conf[0] != "Path") continue;

				name = conf[1].Replace('/', '\\');
			}
			return name;
		}

		public string GetLine(string key, ConfigFileValue value) {
			if (!value.Valid)
				return "";
			string v = value.Comma ? "\"" + value.Value + "\"" : value.Value;
			return String.Format("user_pref(\"{0}\", {1});", key, v);
		}

		public void SaveConfig(TextWriter writer) {
			writer.Write(comment.ToString());
			foreach (var value in config) 
				writer.WriteLine(GetLine(value.Key,value.Value));
		}

		private string GetConfig(string item) {
			return config.ContainsKey(item) ? config[item].Value : "";
		}

		private int GetConfigInt(string item) {
			var i = config.ContainsKey(item) ? config[item].Value : "";
			return i.Length == 0 ? 0 : Convert.ToInt32(i);
		}


		public ProxySettings ProxySettings() {
			var settings = new ProxySettings(GetConfigInt("network.proxy.type") == 1,
			                                 GetConfig("network.proxy.http"),
			                                 GetConfigInt("network.proxy.http_port"),
			                                 GetConfig("network.proxy.ftp"),
			                                 GetConfigInt("network.proxy.ftp_port"),
											 GetConfig("network.proxy.ssl"),
			                                 GetConfigInt("network.proxy.ssl_port"),
			                                 GetConfig("network.proxy.socks"),
			                                 GetConfigInt("network.proxy.socks_port")
				);
			return settings;
		}

		public void SetProxySettings(ProxySettings settings) {
			config["network.proxy.http"] = new ConfigFileValue {Value = settings.HTTP, Comma = true};
			config["network.proxy.http_port"] = new ConfigFileValue { Value = settings.HTTPPort.ToString(CultureInfo.InvariantCulture) };
			config["network.proxy.ftp"] = new ConfigFileValue { Value = settings.FTP, Comma = true };
			config["network.proxy.ftp_port"] = new ConfigFileValue { Value =settings.FTPPort.ToString(CultureInfo.InvariantCulture) };
			config["network.proxy.socks"] = new ConfigFileValue { Value =settings.Socks, Comma = true };
			config["network.proxy.socks_port"] = new ConfigFileValue { Value =settings.SocksPort.ToString(CultureInfo.InvariantCulture) };
			config["network.proxy.ssl"] = new ConfigFileValue { Value =settings.SSL, Comma = true };
			config["network.proxy.ssl_port"] = new ConfigFileValue { Value = settings.SSLPort.ToString(CultureInfo.InvariantCulture) };
			config["network.proxy.type"] = new ConfigFileValue { Value = settings.Enabled ? "1" : "0" };
		}

		public string Homepage() {
			return GetConfig("browser.startup.homepage");
		}

		public void SetHomePage(string page) {
			config["browser.startup.homepage"] = new ConfigFileValue { Value = page, Comma = true };
		}
	}

	public class FirefoxConfigFile : IDisposable {
		private readonly string configDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
		                                    @"\Mozilla\Firefox";

		private const string ProfileFileName = @"\profiles.ini";
		private const string ConfigFileName = @"\prefs.js";
		private string profilePath = "";
		private bool disposed;

		private TextReader configReader;
		private TextReader profileReader;
		private TextWriter configWriter;

		~FirefoxConfigFile() {
			Dispose(false);
		}

		public bool Exists() {
			return File.Exists(configDir + ProfileFileName);
		}

		public void SetConfigFileName(string name) {
			profilePath = "\\" + name + ConfigFileName;
		}

		public TextReader GetProfileFileReader() {
			if (profileReader != null)
				profileReader.Dispose();
			profileReader = new StreamReader(configDir + ProfileFileName);
			return profileReader;
		}

		public TextReader GetConfigFileReader() {
			if (configReader != null)
				configReader.Dispose();
			configReader = new StreamReader(configDir + profilePath);
			return configReader;
		}

		public TextWriter GetConfigFileWriter() {
			if (configWriter != null)
				configReader.Dispose();
			configWriter = new StreamWriter(configDir + profilePath);
			return configWriter;
		}

		private void Dispose(bool disposing) {
			if (disposed) return;
			if (disposing) {
				if (profileReader != null) {
					profileReader.Dispose();
					profileReader = null;
				}
				if (configReader != null) {
					configReader.Dispose();
					configReader = null;
				}
				if (configWriter != null) {
					configWriter.Dispose();
					configWriter = null;
				}
				disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		public void Dispose() {
			Dispose(true);
			
		}
	}
}
