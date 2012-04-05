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
using System.Management;

namespace ZetSwitch {
	public class AdapterDataHelper {
		private readonly ManagementObject obj;


		public AdapterDataHelper(ManagementObject ob) {
			obj = ob;
		}

		public AdapterDataHelper() {
		}


		public string ID {
			get { return (string) obj["SettingID"]; }
		}

		public IPAddress IP {
			get {
				var buff = (string[]) obj["IPAddress"];
				return buff != null ? new IPAddress(buff[0]) : new IPAddress();
			}
		}

		public IPAddress Mask {
			get {
				var buff = obj["IPSubnet"] as string[];
				return buff != null ? new IPAddress(buff[0]) : new IPAddress();
			}
		}

		public IPAddress GW {
			get {
				var buff = obj["DefaultIPGateway"] as string[];
				return buff != null ? new IPAddress(buff[0]) : new IPAddress();
			}
		}

		public bool DHCP {
			get { return Convert.ToBoolean(obj["DHCPEnabled"].ToString()); }
		}

		public string Name {
			get {
				ManagementObjectCollection name;
				using (var searchAdapt = new ManagementObjectSearcher()) {
					searchAdapt.Query =
						new ObjectQuery("Select * from Win32_NetworkAdapter Where MACAddress ='" + obj["MACAddress"] + "'");
					name = searchAdapt.Get();
				}

				foreach (ManagementObject o in name) {
					try {
						return o["NetConnectionID"].ToString();
					}
					catch (Exception e) {
						Program.UseTrace(e);
					}
				}
				return "";
			}
		}

		public IPAddress[] DNS {
			get {
				var mip = new IPAddress[2];
				var ip = (string[]) obj["DNSServerSearchOrder"];
				if (ip == null)
					return mip;
				for (int i = 0; i < ip.Length && i < mip.Length; i++) {
					mip[i] = ip[i];
				}
				return mip;
			}
		}

	}
}
