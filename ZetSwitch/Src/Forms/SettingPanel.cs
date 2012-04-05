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
using System.Text;
using System.Windows.Forms;
using IPAddressControlLib;
using ZetSwitch.Browsers;
using ZetSwitch.Network;

namespace ZetSwitch
{
    abstract class SettingPanel : TabPage
    {
		protected string interfaceName;
		protected Profile profile;

		public Profile ActualProfile
		{
			set { profile = value; }
		}

		public string InterfaceName
		{
			set { interfaceName = value; }
		}

        public SettingPanel()
        {
        }

        public abstract bool DataValidation(out string error); // todo: validaci dat dat nejspis nekam uplne jinam
		public abstract bool SavePanel();
		public abstract bool LoadPanel();
		public abstract void SetControls();
    }

    class IPPage : SettingPanel
    {
		
        #region DOCASNE
		//private System.Windows.Forms.CheckBox CbUse;
        private IPAddressControlLib.IPAddressControl IpGW;
        private IPAddressControlLib.IPAddressControl IpMask;
        private IPAddressControlLib.IPAddressControl IpIpAddress;
        private System.Windows.Forms.RadioButton IPDHCPManual;
        private System.Windows.Forms.RadioButton IPDHCPAuto;
        private System.Windows.Forms.RadioButton DNSDHCPManual;
        private System.Windows.Forms.RadioButton DNSDHCPAuto;
        private IPAddressControlLib.IPAddressControl IpDNS2;
        private IPAddressControlLib.IPAddressControl IpDNS1;
        #endregion
		
		public IPPage() { }

		public override void SetControls()
        {
            #region DOCASNE
			//CbUse = (CheckBox)Controls["cbUseNetwork"];
            IpGW = (IPAddressControl)Controls["groupBox1"].Controls["IpGW"];
            IpMask = (IPAddressControl)Controls["groupBox1"].Controls["IpMask"];
            IpIpAddress = (IPAddressControl)Controls["groupBox1"].Controls["IpIpAddress"];
            IPDHCPManual = (RadioButton)Controls["groupBox1"].Controls["IPDHCPManual"];
            IPDHCPAuto = (RadioButton)Controls["groupBox1"].Controls["IPDHCPAuto"];
            DNSDHCPManual = (RadioButton)Controls["groupBox2"].Controls["DNSDHCPManual"];
            DNSDHCPAuto = (RadioButton)Controls["groupBox2"].Controls["DNSDHCPAuto"];
            IpDNS2 = (IPAddressControl)Controls["groupBox2"].Controls["IpDNS2"];
            IpDNS1 = (IPAddressControl)Controls["groupBox2"].Controls["IpDNS1"];

			this.IpGW.TextChanged += new System.EventHandler(this.OnDataChanged);
			this.IpMask.TextChanged += new System.EventHandler(this.OnDataChanged);
			this.IpIpAddress.TextChanged += new System.EventHandler(this.OnDataChanged);
			this.IpDNS2.TextChanged += new System.EventHandler(this.OnDataChanged);
			this.IpDNS1.TextChanged += new System.EventHandler(this.OnDataChanged);

			this.IPDHCPManual.CheckedChanged += new System.EventHandler(this.IPDHCPManual_CheckedChanged);
			this.IPDHCPManual.CheckedChanged += new System.EventHandler(this.OnDataChanged);
			this.IPDHCPAuto.CheckedChanged += new System.EventHandler(this.IPDHCPAuto_CheckedChanged);
			this.IPDHCPAuto.CheckedChanged += new System.EventHandler(this.OnDataChanged);
			this.DNSDHCPAuto.CheckedChanged += new System.EventHandler(this.DNSDHCPAuto_CheckedChanged);
			this.DNSDHCPAuto.CheckedChanged += new System.EventHandler(this.OnDataChanged);
			this.DNSDHCPManual.CheckedChanged += new System.EventHandler(this.DNSDHCPManual_CheckedChanged);
			this.DNSDHCPManual.CheckedChanged += new System.EventHandler(this.OnDataChanged);
			

            #endregion		
        }

		public event EventHandler DataChanged;

        public void SetDisableControl(bool DHCPIP, bool DHCPDNS)
        {
            DNSDHCPAuto.Enabled = DHCPIP;  //disable DNS from DHCP if IP address is set manualy
            if (DHCPIP == false)
                DHCPDNS = false;

            IPDHCPAuto.Checked = DHCPIP;
            IPDHCPManual.Checked = !DHCPIP;
            IpMask.Enabled = !DHCPIP;
            IpIpAddress.Enabled = !DHCPIP;
            IpGW.Enabled = !DHCPIP;

            DNSDHCPAuto.Checked = DHCPDNS;
            DNSDHCPManual.Checked = !DHCPDNS;
            IpDNS1.Enabled = !DHCPDNS;
            IpDNS2.Enabled = !DHCPDNS;
        }

		public void OnDataChanged(object sender, EventArgs e) {
			if (DataChanged != null)
				DataChanged(this, null);
		}

		public override bool SavePanel()
		{
			ProfileNetworkSettings profileSet = profile.Connections.GetProfileNetworkSettings(interfaceName);
			if (profileSet == null)
				return true;
			NetworkInterfaceSettings settings = profileSet.Settings;
			if (settings == null)
				return true;
			try
			{
				//profileSet.UseNetwork = CbUse.Checked;
				settings.IsDHCP = IPDHCPAuto.Checked;
				settings.GateWay = IpGW.Text;
				settings.IP = IpIpAddress.Text;
				settings.Mask = IpMask.Text;
				if (DNSDHCPAuto.Checked)
				{
					settings.DNS1 = null;
					settings.DNS2 = null;
					settings.IsDNSDHCP = true;
				}
				else
				{
					settings.DNS1 = IpDNS1.Text;
					settings.DNS2 = IpDNS2.Text;
					settings.IsDNSDHCP = false;
				}
			}
			catch { }
			return true;
		}

		public override bool LoadPanel()
		{
			ProfileNetworkSettings profileSet = profile.Connections.GetProfileNetworkSettings(interfaceName);
			if (profileSet == null)
				return true;
			NetworkInterfaceSettings settings = profile.Connections.GetNetworkSettings(interfaceName);
			if (settings == null )
				return true;
			try
			{
				//CbUse.Checked= profileSet.UseNetwork;
				IpMask.SetAddressBytes(settings.Mask);
				IpIpAddress.SetAddressBytes(settings.IP);
				IpGW.SetAddressBytes(settings.GateWay);
				if (settings.DNS1 != null && !settings.DNS1.IsZero())
				{
					IpDNS1.SetAddressBytes(settings.DNS1);
					if (settings.DNS2 != null && !settings.DNS2.IsZero())
						IpDNS2.SetAddressBytes(settings.DNS2);
					SetDisableControl(settings.IsDHCP, settings.IsDNSDHCP);
				}
				else
					SetDisableControl(settings.IsDHCP, settings.IsDNSDHCP);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Program.UseTrace(e);
				return false;
			}
			return true;
		}

		public override bool DataValidation(out string error)
		{
			error = "";
			NetworkInterfaceSettings settings = profile.Connections.GetNetworkSettings(interfaceName);
			return settings == null ? true : settings.Validate(out error);
		}

		private void IPDHCPManual_CheckedChanged(object sender, EventArgs e) {
			SetDisableControl(IPDHCPAuto.Checked, DNSDHCPAuto.Checked);
		}

		private void IPDHCPAuto_CheckedChanged(object sender, EventArgs e) {
			SetDisableControl(IPDHCPAuto.Checked, DNSDHCPAuto.Checked);
		}

		private void DNSDHCPAuto_CheckedChanged(object sender, EventArgs e) {
			SetDisableControl(IPDHCPAuto.Checked, DNSDHCPAuto.Checked);
		}

		private void DNSDHCPManual_CheckedChanged(object sender, EventArgs e) {
			SetDisableControl(IPDHCPAuto.Checked, DNSDHCPAuto.Checked);
		}

    }

    class ProxyPage : SettingPanel
    {
        #region DOCASNE
        private System.Windows.Forms.TextBox textBoxHPort;
        private System.Windows.Forms.TextBox textBoxHTTP;
        private System.Windows.Forms.TextBox textBoxSOPort;
        private System.Windows.Forms.TextBox textBoxSOCK;
        private System.Windows.Forms.TextBox textBoxSSPort;
        private System.Windows.Forms.TextBox textBoxSSL;
        private System.Windows.Forms.TextBox textBoxFPort;
        private System.Windows.Forms.TextBox textBoxFTP;
        private System.Windows.Forms.CheckBox checkBoxUseForAll;
        private System.Windows.Forms.TextBox textBoxHomePage;
		private System.Windows.Forms.CheckBox cbUse;
        private System.Windows.Forms.CheckBox checkBoxIE;
        private System.Windows.Forms.CheckBox checkBoxOp;
        private System.Windows.Forms.CheckBox checkBoxFF;
        private System.Windows.Forms.RadioButton radioButtonIE;
        private System.Windows.Forms.RadioButton radioButtonFF;
        private System.Windows.Forms.RadioButton radioButtonOP;
        #endregion

		BROWSERS actualBrowser = BROWSERS.Ie;

		#region private 

		public void ClearControls()
		{
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

		private void DisableAll()
		{
			textBoxHTTP.Enabled = false;
			textBoxHPort.Enabled = false;
			textBoxHomePage.Enabled = false;
			DisableOtherControl();
		}

		private void EnableAll()
		{
			textBoxHTTP.Enabled = true;
			textBoxHPort.Enabled = true;
			textBoxHomePage.Enabled = true;
			EnableOtherControl();
		}

		private void DisableOtherControl()
		{
			textBoxFTP.Enabled = false;
			textBoxSOCK.Enabled = false;
			textBoxSSL.Enabled = false;

			textBoxFPort.Enabled = false;
			textBoxSOPort.Enabled = false;
			textBoxSSPort.Enabled = false;
		}

		private void EnableOtherControl()
		{
			textBoxFTP.Enabled = true;
			textBoxSOCK.Enabled = true;
			textBoxSSL.Enabled = true;

			textBoxFPort.Enabled = true;
			textBoxSOPort.Enabled = true;
			textBoxSSPort.Enabled = true;
		}

		private void ReloadData()
		{
			Browser browser = profile.GetBrowser(actualBrowser);
			ProxySettings proxy = browser.Proxy;
			ClearControls();
			textBoxHTTP.Text = proxy.HTTP;
			textBoxHPort.Text = proxy.HPort.ToString();
			checkBoxUseForAll.Checked = proxy.UseAdrForAll;

			if (proxy.UseAdrForAll == false)
			{
				textBoxFTP.Text = proxy.FTP;
				textBoxSOCK.Text = proxy.Socks;
				textBoxSSL.Text = proxy.SSL;

				textBoxFPort.Text = proxy.FTPPort.ToString();
				textBoxSOPort.Text = proxy.SocksPort.ToString();
				textBoxSSPort.Text = proxy.SSLPort.ToString();
			}
			else
				DisableOtherControl();

			textBoxHomePage.Text = browser.HomePage;
		}

		#endregion

		public ProxyPage() { }

		public override void SetControls()
        {
            #region DOCASNE
            textBoxHPort = (TextBox)Controls["groupBox4"].Controls["textBoxHPort"];
            textBoxHTTP = (TextBox)Controls["groupBox4"].Controls["textBoxHTTP"];
            textBoxSOPort = (TextBox)Controls["groupBox4"].Controls["textBoxSOPort"];
            textBoxSOCK = (TextBox)Controls["groupBox4"].Controls["textBoxSOCK"];
            textBoxSSPort = (TextBox)Controls["groupBox4"].Controls["textBoxSSPort"];
            textBoxSSL = (TextBox)Controls["groupBox4"].Controls["textBoxSSL"];
            textBoxFPort = (TextBox)Controls["groupBox4"].Controls["textBoxFPort"];
            textBoxFTP = (TextBox)Controls["groupBox4"].Controls["textBoxFTP"];
            checkBoxUseForAll = (CheckBox)Controls["groupBox4"].Controls["checkBoxUseForAll"];
			cbUse = (CheckBox)Controls["groupBox4"].Controls["cbUseBrowser"];
            checkBoxIE = (CheckBox)Controls["groupBox4"].Controls["checkBoxIE"];
            checkBoxOp = (CheckBox)Controls["groupBox4"].Controls["checkBoxOp"];
            checkBoxFF = (CheckBox)Controls["groupBox4"].Controls["checkBoxFF"];
            radioButtonIE = (RadioButton)Controls["groupBox4"].Controls["radioButtonIE"];
            radioButtonFF = (RadioButton)Controls["groupBox4"].Controls["radioButtonFF"];
            radioButtonOP = (RadioButton)Controls["groupBox4"].Controls["radioButtonOP"];
            textBoxHomePage = (TextBox)Controls["groupBox5"].Controls["textBoxHomePage"];
            #endregion
        }

		public override bool SavePanel()
        {
			profile.UseBrowser = cbUse.Checked;
			return true; 
        }

		public bool ChangeBrowser()
        {
			if (radioButtonIE.Checked)
				actualBrowser = BROWSERS.Ie;
			else if (radioButtonFF.Checked)
				actualBrowser = BROWSERS.Firefox;
			ReloadData();
			return true;
        }

		public override bool LoadPanel()
        {
			cbUse.Checked = profile.UseBrowser;
			if (!profile.GetBrowser(Browsers.BROWSERS.Ie).IsDetected) 
			{
				radioButtonIE.Enabled = false;
				checkBoxIE.Enabled = false;
			}

			if (!profile.GetBrowser(Browsers.BROWSERS.Firefox).IsDetected) 
			{
				radioButtonFF.Enabled = false;
                checkBoxFF.Enabled = false;
			}
            radioButtonOP.Enabled = false;
            checkBoxOp.Enabled = false;

			ReloadData();
            return true;
        }

        public override bool DataValidation(out string error)
        {
			error = "";
            return true;
        }
    }

    class MACPage : SettingPanel
    {
		public MACPage() { }

		public override void SetControls()
		{

		}

		public override bool SavePanel()
        {
			return true;
        }

        public override bool LoadPanel()
        {
            return true;
        }

        public override bool DataValidation(out string error)
        {
            throw new NotImplementedException();
        }
    }
}
