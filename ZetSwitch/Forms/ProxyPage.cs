using System;
using System.Globalization;
using System.Windows.Forms;
using ZetSwitchData;
using System.Linq;

namespace ZetSwitch.Forms {
	public partial class ProxyPage : UserControl, ISettingPanel {
		private Profile actProfile;
		public ProxyPage() {
			InitializeComponent();
		}

		private int GetInt(string txt) {
			int o;
			Int32.TryParse(txt, out o);
			return o;
		}

		public void UpdateData() {
			var settings = new ProxySettings(
					true,
					HTTP.Text,
					GetInt(HTTPPort.Text),
					FTP.Text,
					GetInt(FTPPort.Text),
					Socks.Text,
					GetInt(SocksPort.Text),
					SSL.Text,
					GetInt(SSLPort.Text)
				);

			actProfile.BrowserSettings.Proxy = settings;
			actProfile.BrowserSettings.HomePage = HomePage.Text;
			foreach (string name in lbBrowsers.Items) {
				var set = actProfile.BrowserNames.First(x => x.Name == name);
				set.Used = lbBrowsers.GetItemChecked(lbBrowsers.Items.IndexOf(name));
			}

		}

		public void SetData(Profile profile) {
			ProxySettings settings = profile.BrowserSettings.Proxy;
			HomePage.Text = profile.BrowserSettings.HomePage;
			lbBrowsers.Items.Clear();
			foreach(var setting in profile.BrowserNames) {
				lbBrowsers.Items.Insert(lbBrowsers.Items.Count, setting.Name);
				lbBrowsers.SetItemChecked(lbBrowsers.Items.Count - 1, setting.Used);
			}
		
			HTTP.Text = settings.HTTP;
			FTP.Text = settings.FTP;
			Socks.Text = settings.Socks;
			SSL.Text = settings.SSL;
			HTTPPort.Text = settings.HTTPPort > 0 ? settings.HTTPPort.ToString(CultureInfo.InvariantCulture) : "";
			FTPPort.Text = settings.FTPPort  > 0 ? settings.FTPPort.ToString(CultureInfo.InvariantCulture) : "";
			SocksPort.Text = settings.SocksPort  > 0 ? settings.SocksPort.ToString(CultureInfo.InvariantCulture) : "";
			SSLPort.Text = settings.SSLPort > 0 ? settings.SSLPort.ToString(CultureInfo.InvariantCulture) : "";

			cbUseAll.Checked = (settings.HTTP == settings.FTP && settings.HTTP == settings.SSL && settings.HTTP == settings.Socks &&
				settings.HTTPPort == settings.FTPPort && settings.HTTPPort == settings.SocksPort && settings.HTTPPort == settings.SSLPort);
			actProfile = profile;
		}

		public void ResetLanguage() {
		}

		private void CopyHTTPInfo() {
			if (cbUseAll.Checked) {
				SSL.Text = HTTP.Text;
				SSLPort.Text = HTTPPort.Text;
				FTP.Text = HTTP.Text;
				FTPPort.Text = HTTPPort.Text;
				Socks.Text = HTTP.Text;
				SocksPort.Text = HTTPPort.Text;
			}
		}

		private void SetUseForAll(bool forAll) {
			bool enable = !forAll;
			SSL.Enabled = enable;
			SSLPort.Enabled = enable;
			FTP.Enabled = enable;
			FTPPort.Enabled = enable;
			Socks.Enabled = enable;
			SocksPort.Enabled = enable;
			CopyHTTPInfo();
		}

		private void CbUseAllCheckedChanged(object sender, EventArgs e) {
			SetUseForAll(cbUseAll.Checked);
		}

		private void HTTPTextChanged(object sender, EventArgs e) {
			CopyHTTPInfo();
		}

		private void HTTPPortTextChanged(object sender, EventArgs e) {
			CopyHTTPInfo();
		}
	}
}
