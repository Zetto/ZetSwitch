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
using System.Text;

namespace ZetSwitchData.Network {
	public class NetworkInterfaceSettings {
		#region variables

		#endregion

		#region access

		public string SettingId { get; set; }
		public string Name { get; set; }
		public bool IsDNSDHCP { get; set; }
		public bool IsDHCP { get; set; }
		public IPAddress IP { get; set; }
		public IPAddress Mask { get; set; }
		public IPAddress GateWay { get; set; }
		public IPAddress DNS1 { get; set; }
		public IPAddress DNS2 { get; set; }

		public IPAddress[] DNS {
			set {
				if (value[0] == null && value[1] == null) {
					IsDNSDHCP = true;
				}
				else
					IsDNSDHCP = false;
				if (value.Length > 0)
					DNS1 = value[0];
				if (value.Length > 1)
					DNS2 = value[1];
			}
		}

		#endregion

		#region public

		public NetworkInterfaceSettings() {
			IP = new IPAddress();
			GateWay = new IPAddress();
			Mask = new IPAddress();
			DNS1 = new IPAddress();
			DNS2 = new IPAddress();
		}

		public NetworkInterfaceSettings(NetworkInterfaceSettings other) {
			SettingId = other.SettingId;
			Name = other.Name;
			IsDHCP = other.IsDHCP;
			IsDNSDHCP = other.IsDNSDHCP;
			IP = new IPAddress(other.IP);
			GateWay = new IPAddress(other.GateWay);
			Mask = new IPAddress(other.Mask);
			DNS1 = new IPAddress(other.DNS1);
			DNS2 = new IPAddress(other.DNS2);
		}

		public NetworkInterfaceSettings(AdapterDataHelper adapter) {
			SettingId = adapter.ID;
			IsDHCP = adapter.DHCP;
			IP = adapter.IP;
			GateWay = adapter.GW;
			Mask = adapter.Mask;
			Name = adapter.Name;
			DNS = adapter.DNS;
		}

		public bool Validate(out string error) {
			error = "";
			if (IsDHCP)
				return true;
			var message = new StringBuilder();

			if (!Mask.SubnetMaskValidation())
				message.Append(ClientServiceLocator.GetService<ILanguage>().GetText("NonValidSubNetMask") + "\n");
			error = message.ToString();
			return error.Length == 0;
		}

		public override int GetHashCode() {
			return Name.GetHashCode();
		}

		#endregion
	}
}
