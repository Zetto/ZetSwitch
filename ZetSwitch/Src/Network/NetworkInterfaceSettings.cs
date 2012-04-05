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

namespace ZetSwitch.Network {
	[Serializable]
	public class NetworkInterfaceSettings {
		#region variables

		private string name;
		private string id;
		private bool isDHCP;
		private bool isDNSDHCP;
		private IPAddress ip;
		private IPAddress mask;
		private IPAddress gateWay;
		private IPAddress dns1;
		private IPAddress dns2;

		#endregion

		#region access

		public string SettingId {
			get { return id; }
			set { id = value; }
		}

		public string Name {
			get { return name; }
			set { name = value; }
		}

		public bool IsDNSDHCP {
			get { return isDNSDHCP; }
			set { isDNSDHCP = value; }
		}

		public bool IsDHCP {
			get { return isDHCP; }
			set { isDHCP = value; }
		}

		public IPAddress IP {
			get { return ip; }
			set { ip = value; }
		}

		public IPAddress Mask {
			get { return mask; }
			set { mask = value; }
		}

		public IPAddress GateWay {
			get { return gateWay; }
			set { gateWay = value; }
		}

		public IPAddress DNS1 {
			get { return dns1; }
			set { dns1 = value; }
		}

		public IPAddress DNS2 {
			get { return dns2; }
			set { dns2 = value; }
		}

		public IPAddress[] DNS {
			set {
				if (value[0] == null && value[1] == null) {
					isDNSDHCP = true;
				}
				else
					isDNSDHCP = false;
				if (value.Length > 0)
					dns1 = value[0];
				if (value.Length > 1)
					dns2 = value[1];
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
			if (isDHCP)
				return true;
			var message = new StringBuilder();

			if (!Mask.SubnetMaskValidation())
				message.Append(Language.GetText("NonValidSubNetMask") + "\n");
			else {
				if (!IP.ValidateIPWithMask(Mask))
					message.Append(Language.GetText("NonValidIPAgainMask") + "\n");
				if (!GateWay.IsZero() && !GateWay.ValidateIPWithMask(Mask))
					message.Append(Language.GetText("NonValidGWAgainMask") + "\n");
			}
			error = message.ToString();
			return error.Length == 0;
		}

		public override int GetHashCode() {
			return Name.GetHashCode();
		}

		#endregion
	}
}
