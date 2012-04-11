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
	[Serializable]
	public class IPAddress {
		public IPAddress() {
			IP = new byte[4];
		}

		public IPAddress(byte[] bytes) {
			IP = new byte[4];
			IP = bytes;
		}

		public IPAddress(IPAddress old) {
			IP = new byte[4];
			for (var i = 0; i < 4; i++) {
				IP[i] = old.IP[i];
			}
		}

		public IPAddress(string str) {
			IP = ConvertStringIPToByteIP(str);
		}

		public byte[] IP { get; private set; }


		public byte[] ConvertStringIPToByteIP(string strIP) {

			var byteIP = new byte[4];
			var strAr = strIP.Split('.');
			if (strAr.Length != 4)
				return new byte[4];
			for (var i = 0; i < 4; i++) {
				if (strAr[i] == "")
					byteIP[i] = 0;
				else
					Byte.TryParse(strAr[i],out byteIP[i]);
			}
			return byteIP;
		}

		public void Clear() {
			IP = new byte[4];
		}

		public override string ToString() {
			var str = new StringBuilder();
			for (var i = 0; i < 4; i++) {
				str.Append(IP[i]);
				if (i != 3)
					str.Append('.');
			}
			return str.ToString();
		}


		public static implicit operator byte[](IPAddress mip) {
			return mip.IP;
		}

		public static implicit operator IPAddress(string strIP) {
			return new IPAddress(strIP);
		}

		public static IPAddress operator ~(IPAddress mip) {
			var newIP = new byte[4];
			for (var i = 0; i < 4; i++) {
				newIP[i] = (byte) ~mip.IP[i];
			}
			return new IPAddress(newIP);
		}


		public bool Compare(IPAddress b) {
			for (var i = 0; i < 4; i++) {
				if (IP[i] != b.IP[i])
					return false;
			}
			return true;
		}


		public static IPAddress operator &(IPAddress a, IPAddress b) {
			var newIP = new IPAddress(a);
			for (var i = 0; i < 4; i++)
				newIP.IP[i] &= b.IP[i];
			return newIP;
		}

		public static IPAddress operator ^(IPAddress a, IPAddress b) {
			var newIP = new IPAddress(a);
			for (var i = 0; i < 4; i++)
				newIP.IP[i] ^= b.IP[i];
			return newIP;
		}

		public bool Validation() {
			return true;
		}

		public bool IsZero() {
			for (int i = 0; i < 4; i++) {
				if (IP[i] != 0)
					return false;
			}
			return true;
		}

		public bool SubnetMaskValidation() {
			bool wasZero = false;
			if (IP[0] < 224)
				return false;

			for (int i = 0; i < 4; i++) {
				byte mask = 128;
				for (int j = 0; j < 8; j++) {
					int res = IP[i] & mask;
					if (res == 0) {
						wasZero = true;
					}
					else if (wasZero)
						return false;
					mask >>= 1;
				}
			}
			return true;
		}

		public bool ValidateIPWithMask(IPAddress mask) {
			if (!mask.SubnetMaskValidation())
				throw new Exception(ClientServiceLocator.GetService<ILanguage>().GetText("NonValidSubNetMask"));

			IPAddress negMask = ~mask;
			IPAddress pcAddress = this & negMask;
			if (pcAddress.IsZero()) //address of network
				return false;
			IPAddress broadTest = pcAddress ^ negMask;
			return !broadTest.IsZero();
		}

		public bool ComapreIPGWNet(IPAddress mask, IPAddress gw) {
			if (!mask.SubnetMaskValidation())
				throw new Exception(ClientServiceLocator.GetService<ILanguage>().GetText("NonValidSubNetMask"));

			IPAddress gwNet = gw & mask;
			IPAddress ipNet = this & mask;

			return gwNet.Compare(ipNet);
		}
	}
}