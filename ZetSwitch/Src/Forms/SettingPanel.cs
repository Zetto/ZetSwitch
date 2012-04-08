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
using System.Globalization;
using System.Windows.Forms;
using IPAddressControlLib;
using ZetSwitch.Browsers;
using ZetSwitch.Network;

namespace ZetSwitch {
	internal abstract class SettingPanel : TabPage {

		public Profile ActualProfile { protected get; set; }
		public string InterfaceName { protected get; set; }

		public abstract bool DataValidation(out string error); // todo: validaci dat dat nejspis nekam uplne jinam
		public abstract bool SavePanel();
		public abstract bool LoadPanel();
		public abstract void SetControls();
	}

	internal class IPPage : SettingPanel {

		#region DOCASNE

		//private CheckBox CbUse;
		private IPAddressControl ipGW;
		private IPAddressControl ipMask;
		private IPAddressControl ipIpAddress;
		private RadioButton ipdhcpManual;
		private RadioButton ipdhcpAuto;
		private RadioButton dnsdhcpManual;
		private RadioButton dnsdhcpAuto;
		private IPAddressControl ipDNS2;
		private IPAddressControl ipDNS1;

		#endregion

		public override void SetControls() {
			#region DOCASNE

			//CbUse = (CheckBox)Controls["cbUseNetwork"];
			ipGW = (IPAddressControl) Controls["groupBox1"].Controls["IpGW"];
			ipMask = (IPAddressControl) Controls["groupBox1"].Controls["IpMask"];
			ipIpAddress = (IPAddressControl) Controls["groupBox1"].Controls["IpIpAddress"];
			ipdhcpManual = (RadioButton) Controls["groupBox1"].Controls["IPDHCPManual"];
			ipdhcpAuto = (RadioButton) Controls["groupBox1"].Controls["IPDHCPAuto"];
			dnsdhcpManual = (RadioButton) Controls["groupBox2"].Controls["DNSDHCPManual"];
			dnsdhcpAuto = (RadioButton) Controls["groupBox2"].Controls["DNSDHCPAuto"];
			ipDNS2 = (IPAddressControl) Controls["groupBox2"].Controls["IpDNS2"];
			ipDNS1 = (IPAddressControl) Controls["groupBox2"].Controls["IpDNS1"];

			ipGW.TextChanged += OnDataChanged;
			ipMask.TextChanged += OnDataChanged;
			ipIpAddress.TextChanged += OnDataChanged;
			ipDNS2.TextChanged += OnDataChanged;
			ipDNS1.TextChanged += OnDataChanged;

			ipdhcpManual.CheckedChanged += IpdhcpManualCheckedChanged;
			ipdhcpManual.CheckedChanged += OnDataChanged;
			ipdhcpAuto.CheckedChanged += IpdhcpAutoCheckedChanged;
			ipdhcpAuto.CheckedChanged += OnDataChanged;
			dnsdhcpAuto.CheckedChanged += DNSDHCPAutoCheckedChanged;
			dnsdhcpAuto.CheckedChanged += OnDataChanged;
			dnsdhcpManual.CheckedChanged += DNSDHCPManualCheckedChanged;
			dnsdhcpManual.CheckedChanged += OnDataChanged;


			#endregion
		}

		public event EventHandler DataChanged;

		public void SetDisableControl(bool dhcpip, bool dhcpdns) {
			dnsdhcpAuto.Enabled = dhcpip; //disable DNS from DHCP if IP address is set manualy
			if (dhcpip == false)
				dhcpdns = false;

			ipdhcpAuto.Checked = dhcpip;
			ipdhcpManual.Checked = !dhcpip;
			ipMask.Enabled = !dhcpip;
			ipIpAddress.Enabled = !dhcpip;
			ipGW.Enabled = !dhcpip;

			dnsdhcpAuto.Checked = dhcpdns;
			dnsdhcpManual.Checked = !dhcpdns;
			ipDNS1.Enabled = !dhcpdns;
			ipDNS2.Enabled = !dhcpdns;
		}

		public void OnDataChanged(object sender, EventArgs e) {
			if (DataChanged != null)
				DataChanged(this, null);
		}

		public override bool SavePanel() {
			ProfileNetworkSettings profileSet = ActualProfile.Connections.GetProfileNetworkSettings(InterfaceName);
			if (profileSet == null)
				return true;
			NetworkInterfaceSettings settings = profileSet.Settings;
			if (settings == null)
				return true;
			//profileSet.UseNetwork = CbUse.Checked;
			settings.IsDHCP = ipdhcpAuto.Checked;
			settings.GateWay = ipGW.Text;
			settings.IP = ipIpAddress.Text;
			settings.Mask = ipMask.Text;
			if (dnsdhcpAuto.Checked) {
				settings.DNS1 = null;
				settings.DNS2 = null;
				settings.IsDNSDHCP = true;
			}
			else {
				settings.DNS1 = ipDNS1.Text;
				settings.DNS2 = ipDNS2.Text;
				settings.IsDNSDHCP = false;
			}
			return true;
		}

		public override bool LoadPanel() {
			ProfileNetworkSettings profileSet = ActualProfile.Connections.GetProfileNetworkSettings(InterfaceName);
			if (profileSet == null)
				return true;
			NetworkInterfaceSettings settings = ActualProfile.Connections.GetNetworkSettings(InterfaceName);
			if (settings == null)
				return true;
			ipMask.SetAddressBytes(settings.Mask);
			ipIpAddress.SetAddressBytes(settings.IP);
			ipGW.SetAddressBytes(settings.GateWay);
			if (settings.DNS1 != null && !settings.DNS1.IsZero()) {
				ipDNS1.SetAddressBytes(settings.DNS1);
				if (settings.DNS2 != null && !settings.DNS2.IsZero())
					ipDNS2.SetAddressBytes(settings.DNS2);
				SetDisableControl(settings.IsDHCP, settings.IsDNSDHCP);
			}
			else
				SetDisableControl(settings.IsDHCP, settings.IsDNSDHCP);
			return true;
		}

		public override bool DataValidation(out string error) {
			error = "";
			NetworkInterfaceSettings settings = ActualProfile.Connections.GetNetworkSettings(InterfaceName);
			return settings == null || settings.Validate(out error);
		}

		private void IpdhcpManualCheckedChanged(object sender, EventArgs e) {
			SetDisableControl(ipdhcpAuto.Checked, dnsdhcpAuto.Checked);
		}

		private void IpdhcpAutoCheckedChanged(object sender, EventArgs e) {
			SetDisableControl(ipdhcpAuto.Checked, dnsdhcpAuto.Checked);
		}

		private void DNSDHCPAutoCheckedChanged(object sender, EventArgs e) {
			SetDisableControl(ipdhcpAuto.Checked, dnsdhcpAuto.Checked);
		}

		private void DNSDHCPManualCheckedChanged(object sender, EventArgs e) {
			SetDisableControl(ipdhcpAuto.Checked, dnsdhcpAuto.Checked);
		}

	}

	internal class ProxyPage : SettingPanel {
		#region DOCASNE

		private TextBox textBoxHPort;
		private TextBox textBoxHTTP;
		private TextBox textBoxSOPort;
		private TextBox textBoxSOCK;
		private TextBox textBoxSSPort;
		private TextBox textBoxSSL;
		private TextBox textBoxFPort;
		private TextBox textBoxFTP;
		private CheckBox checkBoxUseForAll;
		private TextBox textBoxHomePage;
		private CheckBox cbUse;
		private CheckBox checkBoxIE;
		private CheckBox checkBoxOp;
		private CheckBox checkBoxFF;
		private RadioButton radioButtonIE;
		private RadioButton radioButtonFF;
		private RadioButton radioButtonOP;

		#endregion

		private BROWSERS actualBrowser = BROWSERS.Ie;

		#region private

		public void ClearControls() {
			EnableOtherControl();
			textBoxHTTP.Text = "";
			textBoxHPort.Text = "";
			checkBoxUseForAll.Checked = false;


			textBoxFTP.Text = "";
			textBoxSOCK.Text = "";
			textBoxSSL.Text = "";

			textBoxFPort.Text = "";
			textBoxSOPort.Text = "";
			textBoxSSPort.Text = "";
		}

		private void DisableOtherControl() {
			textBoxFTP.Enabled = false;
			textBoxSOCK.Enabled = false;
			textBoxSSL.Enabled = false;

			textBoxFPort.Enabled = false;
			textBoxSOPort.Enabled = false;
			textBoxSSPort.Enabled = false;
		}

		private void EnableOtherControl() {
			textBoxFTP.Enabled = true;
			textBoxSOCK.Enabled = true;
			textBoxSSL.Enabled = true;

			textBoxFPort.Enabled = true;
			textBoxSOPort.Enabled = true;
			textBoxSSPort.Enabled = true;
		}

		private void ReloadData() {
			Browser browser = ActualProfile.GetBrowser(actualBrowser);
			ProxySettings proxy = browser.Proxy;
			ClearControls();
			textBoxHTTP.Text = proxy.HTTP;
			textBoxHPort.Text = proxy.HPort.ToString(CultureInfo.InvariantCulture);
			checkBoxUseForAll.Checked = proxy.UseAdrForAll;

			if (proxy.UseAdrForAll == false) {
				textBoxFTP.Text = proxy.FTP;
				textBoxSOCK.Text = proxy.Socks;
				textBoxSSL.Text = proxy.SSL;

				textBoxFPort.Text = proxy.FTPPort.ToString(CultureInfo.InvariantCulture);
				textBoxSOPort.Text = proxy.SocksPort.ToString(CultureInfo.InvariantCulture);
				textBoxSSPort.Text = proxy.SSLPort.ToString(CultureInfo.InvariantCulture);
			}
			else
				DisableOtherControl();

			textBoxHomePage.Text = browser.HomePage;
		}

		#endregion

		public override void SetControls() {
			#region DOCASNE

			textBoxHPort = (TextBox) Controls["groupBox4"].Controls["textBoxHPort"];
			textBoxHTTP = (TextBox) Controls["groupBox4"].Controls["textBoxHTTP"];
			textBoxSOPort = (TextBox) Controls["groupBox4"].Controls["textBoxSOPort"];
			textBoxSOCK = (TextBox) Controls["groupBox4"].Controls["textBoxSOCK"];
			textBoxSSPort = (TextBox) Controls["groupBox4"].Controls["textBoxSSPort"];
			textBoxSSL = (TextBox) Controls["groupBox4"].Controls["textBoxSSL"];
			textBoxFPort = (TextBox) Controls["groupBox4"].Controls["textBoxFPort"];
			textBoxFTP = (TextBox) Controls["groupBox4"].Controls["textBoxFTP"];
			checkBoxUseForAll = (CheckBox) Controls["groupBox4"].Controls["checkBoxUseForAll"];
			cbUse = (CheckBox) Controls["groupBox4"].Controls["cbUseBrowser"];
			checkBoxIE = (CheckBox) Controls["groupBox4"].Controls["checkBoxIE"];
			checkBoxOp = (CheckBox) Controls["groupBox4"].Controls["checkBoxOp"];
			checkBoxFF = (CheckBox) Controls["groupBox4"].Controls["checkBoxFF"];
			radioButtonIE = (RadioButton) Controls["groupBox4"].Controls["radioButtonIE"];
			radioButtonFF = (RadioButton) Controls["groupBox4"].Controls["radioButtonFF"];
			radioButtonOP = (RadioButton) Controls["groupBox4"].Controls["radioButtonOP"];
			textBoxHomePage = (TextBox) Controls["groupBox5"].Controls["textBoxHomePage"];

			#endregion
		}

		public override bool SavePanel() {
			ActualProfile.UseBrowser = cbUse.Checked;
			return true;
		}

		public bool ChangeBrowser() {
			if (radioButtonIE.Checked)
				actualBrowser = BROWSERS.Ie;
			else if (radioButtonFF.Checked)
				actualBrowser = BROWSERS.Firefox;
			ReloadData();
			return true;
		}

		public override bool LoadPanel() {
			cbUse.Checked = ActualProfile.UseBrowser;
			if (!ActualProfile.GetBrowser(BROWSERS.Ie).IsDetected) {
				radioButtonIE.Enabled = false;
				checkBoxIE.Enabled = false;
			}

			if (!ActualProfile.GetBrowser(BROWSERS.Firefox).IsDetected) {
				radioButtonFF.Enabled = false;
				checkBoxFF.Enabled = false;
			}
			radioButtonOP.Enabled = false;
			checkBoxOp.Enabled = false;

			ReloadData();
			return true;
		}

		public override bool DataValidation(out string error) {
			error = "";
			return true;
		}
	}

	internal class MACPage : SettingPanel {
		public override void SetControls() {

		}

		public override bool SavePanel() {
			return true;
		}

		public override bool LoadPanel() {
			return true;
		}

		public override bool DataValidation(out string error) {
			throw new NotImplementedException();
		}
	}
}