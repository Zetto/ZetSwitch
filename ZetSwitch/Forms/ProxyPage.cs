using System.Windows.Forms;
using ZetSwitchData;
using ZetSwitchData.Browsers;
using ZetSwitchData.Network;

namespace ZetSwitch.Forms {
	public partial class ProxyPage : UserControl, ISettingPanel {
		public ProxyPage() {
			InitializeComponent();
		}

		public void UpdateData() {
			
		}

		public void SetData(Profile profile, NetworkInterfaceSettings actualInterface) {
			
;		}

		private void SetUseForAll(bool forAll) {
			bool enable = !forAll;
			SSL.Enabled = enable;
			SSLPort.Enabled = enable;
			FTP.Enabled = enable;
			FTPPort.Enabled = enable;
			Socks.Enabled = enable;
			SocksPort.Enabled = enable;
			if (!enable) {
				SSL.Text = HTTP.Text;
				SSLPort.Text = HTTTPort.Text;
				FTP.Text = HTTP.Text;
				FTPPort.Text = HTTTPort.Text;
				Socks.Text = HTTP.Text;
				SocksPort.Text = HTTTPort.Text;
			}
		}

		private void CbUseAllCheckedChanged(object sender, System.EventArgs e) {
			SetUseForAll(cbUseAll.Checked);
		}
	}
}
