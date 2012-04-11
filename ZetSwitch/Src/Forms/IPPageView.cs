using System;
using System.Windows.Forms;
using ZetSwitch.Network;

namespace ZetSwitch {
	public partial class IPPageView : UserControl, ISettingPanel {
		private NetworkInterfaceSettings settings;

		public IPPageView() {
			InitializeComponent();
			ResetLanguage();
		}

		private void ResetLanguage() {
			IPDHCPManual.Text = Language.GetText("TextUseThisSetting");
			DNSDHCPManual.Text = Language.GetText("TextUseThisSetting");
			label3.Text = Language.GetText("TextDefGate");
			IPDHCPAuto.Text = Language.GetText("TextOtainDNS");
			label2.Text = Language.GetText("TextMask");
			DNSDHCPAuto.Text = Language.GetText("TextOtainDNS");
		}

		public void UpdatePanel() {
			IpMask.SetAddressBytes(settings.Mask);
			IpIpAddress.SetAddressBytes(settings.IP);
			IpGW.SetAddressBytes(settings.GateWay);
			if (settings.DNS1 != null && !settings.DNS1.IsZero()) 
				IpDNS1.SetAddressBytes(settings.DNS1);
				if (settings.DNS2 != null && !settings.DNS2.IsZero())
					IpDNS2.SetAddressBytes(settings.DNS2);
			SetDisableControl(settings.IsDHCP, settings.IsDNSDHCP);
		}

		public void UpdateData() {
			settings.IsDHCP = IPDHCPAuto.Checked;
			settings.GateWay = IpGW.Text;
			settings.IP = IpIpAddress.Text;
			settings.Mask = IpMask.Text;
			if (DNSDHCPAuto.Checked) {
				settings.DNS1 = null;
				settings.DNS2 = null;
				settings.IsDNSDHCP = true;
			}
			else {
				settings.DNS1 = IpDNS1.Text;
				settings.DNS2 = IpDNS2.Text;
				settings.IsDNSDHCP = false;
			}
		}

		public void SetData(Profile profile, NetworkInterfaceSettings actualInterface) {
			settings = actualInterface;
			UpdatePanel();
		}

		private void SetDisableControl(bool dhcpip, bool dhcpdns) {
			DNSDHCPAuto.Enabled = dhcpip; //disable DNS from DHCP if IP address is set manualy
			if (dhcpip == false)
				dhcpdns = false;

			IPDHCPAuto.Checked = dhcpip;
			IPDHCPManual.Checked = !dhcpip;
			IpMask.Enabled = !dhcpip;
			IpIpAddress.Enabled = !dhcpip;
			IpGW.Enabled = !dhcpip;

			DNSDHCPAuto.Checked = dhcpdns;
			DNSDHCPManual.Checked = !dhcpdns;
			IpDNS1.Enabled = !dhcpdns;
			IpDNS2.Enabled = !dhcpdns;
		}

		private void OnSelectionChanged(object sender, EventArgs e) {
			SetDisableControl(IPDHCPAuto.Checked, DNSDHCPAuto.Checked);
			if (DataChanged != null)
				DataChanged(this, null);
		}

		private void OnDataChanged(object sender, EventArgs e) {
			if (DataChanged != null)
				DataChanged(this, null);
		}

		public event EventHandler DataChanged;
	}

}
