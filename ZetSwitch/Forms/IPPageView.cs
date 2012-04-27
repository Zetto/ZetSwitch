using System;
using System.Windows.Forms;
using ZetSwitchData;
using ZetSwitchData.Network;

namespace ZetSwitch {
	public partial class IPPageView : UserControl, ISettingPanel {
		private bool changingIf;
		private Profile actProfile;
		private NetworkInterfaceSettings actualSettings = new NetworkInterfaceSettings();


		public IPPageView() {
			InitializeComponent();
		//	ResetLanguage();
		}

		public void ResetLanguage() {
			IPDHCPManual.Text = ClientServiceLocator.GetService<ILanguage>().GetText("TextUseThisSetting");
			DNSDHCPManual.Text = ClientServiceLocator.GetService<ILanguage>().GetText("TextUseThisSetting");
			label3.Text = ClientServiceLocator.GetService<ILanguage>().GetText("TextDefGate");
			IPDHCPAuto.Text = ClientServiceLocator.GetService<ILanguage>().GetText("TextOtainDNS");
			label2.Text = ClientServiceLocator.GetService<ILanguage>().GetText("TextMask");
			DNSDHCPAuto.Text = ClientServiceLocator.GetService<ILanguage>().GetText("TextOtainDNS");
		}

		public void UpdatePanel() {
			IpMask.SetAddressBytes(actualSettings.Mask);
			IpIpAddress.SetAddressBytes(actualSettings.IP);
			IpGW.SetAddressBytes(actualSettings.GateWay);
			if (actualSettings.DNS1 != null && !actualSettings.DNS1.IsZero()) 
				IpDNS1.SetAddressBytes(actualSettings.DNS1);
				if (actualSettings.DNS2 != null && !actualSettings.DNS2.IsZero())
					IpDNS2.SetAddressBytes(actualSettings.DNS2);
			SetDisableControl(actualSettings.IsDHCP, actualSettings.IsDNSDHCP);
		}

		public void UpdateData() {
			foreach (string name in ListBoxInterfaces.Items)
				actProfile.UseNetworkInterface(name, ListBoxInterfaces.GetItemChecked(ListBoxInterfaces.Items.IndexOf(name)));

			actualSettings.IsDHCP = IPDHCPAuto.Checked;
			actualSettings.GateWay = IpGW.Text;
			actualSettings.IP = IpIpAddress.Text;
			actualSettings.Mask = IpMask.Text;
			if (DNSDHCPAuto.Checked) {
				actualSettings.DNS1 = null;
				actualSettings.DNS2 = null;
				actualSettings.IsDNSDHCP = true;
			}
			else {
				actualSettings.DNS1 = IpDNS1.Text;
				actualSettings.DNS2 = IpDNS2.Text;
				actualSettings.IsDNSDHCP = false;
			}

		}

		private void SetUseActualSelection() {
			int index = ListBoxInterfaces.SelectedIndex;
			if (index < 0 || changingIf)
				return;
			ListBoxInterfaces.SetItemChecked(index, true);
		}

		public void UpdateInterfaceList() {
			ListBoxInterfaces.Items.Clear();
			ListBoxInterfaces.IsLoaded = true;
			var names = actProfile.GetNetworkInterfaceNames();
			foreach (var name in names) {
				ListBoxInterfaces.Items.Add(name);
				ListBoxInterfaces.SetItemChecked(ListBoxInterfaces.Items.Count - 1, actProfile.IsNetworkInterfaceInProfile(name));
			}

			if (ListBoxInterfaces.Items.Count > 0)
				ListBoxInterfaces.SetSelected(0, true);
		}

		public void SetData(Profile profile) {
			actProfile = profile;
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
			SetUseActualSelection();
		}

		private void OnDataChanged(object sender, EventArgs e) {
			SetUseActualSelection();
		}

		private void ListBoxInterfacesSelectedIndexChanged(object sender, EventArgs e) {
			if (ListBoxInterfaces.SelectedIndex < 0)
				return;
			changingIf = true;
			UpdateData();
			actualSettings = actProfile.Connections.GetProfileNetworkSettings((string)ListBoxInterfaces.Items[ListBoxInterfaces.SelectedIndex]).Settings;
			UpdatePanel();
			changingIf = false;
		}
	}

}
