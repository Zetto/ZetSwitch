using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ZetSwitchData;
using ZetSwitchData.Browsers.FF;

namespace Tests.Model {
	[TestFixture]
	internal class FirefoxFileTests {
		private const string HomePage = @"http://www.seznam.cz/";

		private readonly ProxySettings testProxy = new ProxySettings(
			true,
			"1.1.1.1",
			80,
			"3.3.3.3",
			82,
			"2.2.2.2",
			81,
			"4.4.4.4",
			83

	);

	[Test]
		public void ShouldParseConfigLine() {
			var config = new FirefoxConfigReader();
			// comments
			ConfigLine line = config.ReadLine("");
			Assert.AreEqual(true,line.Comment);
			line = config.ReadLine("/*");
			Assert.AreEqual(true, line.Comment);
			line = config.ReadLine("# Mo");
			Assert.AreEqual(true, line.Comment);

			// values
			line = config.ReadLine("user_pref(\"accessibility.typeaheadfind.flashBar\", 0);");
			Assert.AreEqual(true, line.Value.Valid);
			Assert.AreEqual(false, line.Comment);
			Assert.AreEqual(false, line.Value.Comma);
			Assert.AreEqual("accessibility.typeaheadfind.flashBar", line.Key);
			Assert.AreEqual("0", line.Value.Value);

			line = config.ReadLine("user_pref(\"app.update.lastUpdateTime.addon-background-update-timer\", 1334434686);");
			Assert.AreEqual(true, line.Value.Valid);
			Assert.AreEqual(false, line.Comment);
			Assert.AreEqual(false, line.Value.Comma);
			Assert.AreEqual("app.update.lastUpdateTime.addon-background-update-timer", line.Key);
			Assert.AreEqual("1334434686", line.Value.Value);

			line = config.ReadLine("user_pref(\"app.update\", true);");
			Assert.AreEqual(true, line.Value.Valid);
			Assert.AreEqual(false, line.Comment);
			Assert.AreEqual(false, line.Value.Comma);
			Assert.AreEqual("app.update", line.Key);
			Assert.AreEqual("true", line.Value.Value);

			line = config.ReadLine("user_pref(\"app.update\", \"value\");");
			Assert.AreEqual(true, line.Value.Valid);
			Assert.AreEqual(false, line.Comment);
			Assert.AreEqual(true, line.Value.Comma);
			Assert.AreEqual("app.update", line.Key);
			Assert.AreEqual("value", line.Value.Value);

			line = config.ReadLine("user_pref(\"app.update\", \"\");");
			Assert.AreEqual(true, line.Value.Valid);
			Assert.AreEqual(false, line.Comment);
			Assert.AreEqual(true, line.Value.Comma);
			Assert.AreEqual("app.update", line.Key);
			Assert.AreEqual("", line.Value.Value);

			line = config.ReadLine("user_pref(, false);");
			Assert.AreEqual(false, line.Value.Valid);

			line = config.ReadLine("user_pref(\"app.update\", );");
			Assert.AreEqual(false, line.Value.Valid);
		}

		[Test]
		public void ShouldGenerateLine() {
			var config = new FirefoxConfigReader();
			var value = new ConfigFileValue();

			string result = config.GetLine("key", value);
			Assert.AreEqual("",result);

			value.Value = "0";
			result = config.GetLine("key", value);
			Assert.AreEqual("user_pref(\"key\", 0);",result);

			value.Value = "false";
			result = config.GetLine("key", value);
			Assert.AreEqual("user_pref(\"key\", false);", result);

			value.Value = "value";
			value.Comma = true;
			result = config.GetLine("key", value);
			Assert.AreEqual("user_pref(\"key\", \"value\");", result);

			value.Value = "\"\"";
			value.Comma = true;
			result = config.GetLine("key", value);
			Assert.AreEqual("user_pref(\"key\", \"\");", result);
		}

		[Test]
		public void ShouldReadProfile() {
			using (var reader = new StringReader(Properties.Resources.FFProfiles)) {
				var config = new FirefoxConfigReader();
				string name = config.GetProfileName(reader);
				Assert.AreEqual("Profiles\\c6socz9d.default", name);
			}
		}

		[Test]
		public void ShouldReadProxySettingsEnabled() {
			using (var reader = new StringReader(Properties.Resources.FFPrefs)) {
				var config = new FirefoxConfigReader();
				config.LoadConfig(reader);
				var settings = config.ProxySettings();
				Assert.AreEqual("1.1.1.1",settings.HTTP);
				Assert.AreEqual("2.2.2.2", settings.SSL);
				Assert.AreEqual("3.3.3.3", settings.FTP);
				Assert.AreEqual("4.4.4.4", settings.Socks);

				Assert.AreEqual(80, settings.HTTPPort);
				Assert.AreEqual(81, settings.SSLPort);
				Assert.AreEqual(82, settings.FTPPort);
				Assert.AreEqual(83, settings.SocksPort);

				Assert.AreEqual(false,settings.UseAdrForAll);
				Assert.AreEqual(true, settings.Enabled);
			}
		}

		[Test]
		public void ShouldReadProxySettingsDisabled() {
			using (var reader = new StringReader(Properties.Resources.FFPrefsProxyDisabled)) {
				var config = new FirefoxConfigReader();
				config.LoadConfig(reader);
				var settings = config.ProxySettings();
				Assert.AreEqual(false, settings.Enabled);
			}
		}

		[Test]
		public void ShouldSetProxySettings() {
			var config = new FirefoxConfigReader();
			config.SetProxySettings(testProxy);
			var tmp = config.ProxySettings();
			Assert.AreEqual(tmp, testProxy);
		}

		[Test]
		public void ShouldReadHomePage() {
			using (var reader = new StringReader(Properties.Resources.FFPrefs)) {
				var config = new FirefoxConfigReader();
				config.LoadConfig(reader);
				string home = config.Homepage();
				Assert.AreEqual(HomePage, home);

			}
		}

		[Test]
		public void ShouldSetHomePage() {
			using (var reader = new StringReader(Properties.Resources.FFPrefs)) {
				var config = new FirefoxConfigReader();
				config.LoadConfig(reader);
				config.SetHomePage(HomePage);
				Assert.AreEqual(HomePage, config.Homepage());
			}
		}

		[Test]
		public void ShouldSaveHomePage() {
			using (var savedData = new MemoryStream()) {
				var config = new FirefoxConfigReader();
				using (var reader = new StringReader(Properties.Resources.FFPrefs)) {
					config.LoadConfig(reader);
					config.SetHomePage(HomePage);
				}

				using (var writer = new StreamWriter(savedData)) {
					writer.AutoFlush = true;
					config.SaveConfig(writer);
					savedData.Flush();
					savedData.Seek(0, SeekOrigin.Begin);

					using (var reader = new StreamReader(savedData)) {
						var config2 = new FirefoxConfigReader();
						config2.LoadConfig(reader);
						Assert.AreEqual(HomePage, config2.Homepage());
					}
				}
			}
		}

		[Test]
		public void ShouldSaveSettingsFile() {
			using (var savedData = new MemoryStream()) {
				var config = new FirefoxConfigReader();
				using (var reader = new StringReader(Properties.Resources.FFPrefs)) {
					config.LoadConfig(reader);
				}
				using (var writer = new StreamWriter(savedData)) {
					writer.AutoFlush = true;
					config.SaveConfig(writer);
					savedData.Flush();
					savedData.Seek(0, SeekOrigin.Begin);
					var rows = new HashSet<string>();
					using (var reader = new StreamReader(savedData)) {
						string line;
						while ((line = reader.ReadLine()) != null) {
							rows.Add(line);
						}
					}
					using (var reader = new StringReader(Properties.Resources.FFPrefs)) {
						string line;
						while ((line = reader.ReadLine()) != null) {
							Assert.AreEqual(true, rows.Contains(line));
						}
					}
				}
			}
		}

		[Test]
		public void ShouldSaveProxySettingsToFile() {
			using (var savedData = new MemoryStream()) {
				var config = new FirefoxConfigReader();
				using (var reader = new StringReader(Properties.Resources.FFPrefsProxyDisabled)) {
					config.LoadConfig(reader);
				}
				config.SetProxySettings(testProxy);
				
				using (var writer = new StreamWriter(savedData)) {
					writer.AutoFlush = true;
					config.SaveConfig(writer);
					
					savedData.Flush();
					savedData.Seek(0, SeekOrigin.Begin);
					var rows = new HashSet<string>();
					using (var reader = new StreamReader(savedData)) {
						string line;
						while ((line = reader.ReadLine()) != null) {
							rows.Add(line);
						}
					}
					using (var reader = new StringReader(Properties.Resources.FFPrefs)) {
						string line;
						while ((line = reader.ReadLine()) != null) {
							Assert.AreEqual(true, rows.Contains(line));
						}
					}
				}
			}
		}
	}
}
