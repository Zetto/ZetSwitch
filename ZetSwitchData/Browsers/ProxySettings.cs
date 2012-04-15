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
using System.Text;

namespace ZetSwitchData
{
    [Serializable]
    public class ProxySettings
    {
        public bool Enabled;
        public bool UseAdrForAll;
        public string HTTP;
        public int HTTPPort;
        public string FTP;
        public int FTPPort;
        public string SSL;
        public int SSLPort;
        public string Socks;
        public int SocksPort;

		public ProxySettings() {
		}

    	public ProxySettings(ProxySettings other) {
    		Enabled = other.Enabled;
    		UseAdrForAll = other.UseAdrForAll;
    		HTTP = other.HTTP;
    		HTTPPort = other.HTTPPort;
    		FTP = other.FTP;
    		FTPPort = other.FTPPort;
    		SSL = other.SSL;
    		SSLPort = other.SSLPort;
    		Socks = other.Socks;
    		SocksPort = other.SocksPort;
    	}

    	public override bool Equals(object obj) {
			var other = obj as ProxySettings;
			if (other == null)
				return false;
			return Enabled == other.Enabled && UseAdrForAll == other.UseAdrForAll && HTTP == other.HTTP &&
				HTTPPort == other.HTTPPort && FTP == other.FTP && FTPPort == other.FTPPort && SSL == other.SSL
				&& SSLPort == other.SSLPort && Socks == other.Socks && SocksPort == other.SocksPort;
		}

		public override int GetHashCode() {
			return (Enabled.ToString(CultureInfo.InvariantCulture) + UseAdrForAll.ToString(CultureInfo.InvariantCulture) + HTTP +
			        HTTPPort.ToString(CultureInfo.InvariantCulture) + FTP + FTPPort.ToString(CultureInfo.InvariantCulture) + SSL + 
					SSLPort.ToString(CultureInfo.InvariantCulture) + Socks + SocksPort.ToString(CultureInfo.InvariantCulture)
					).GetHashCode();
		}

		public override string ToString() {
			var b = new StringBuilder();
			b.Append("Enabled: ");
			b.Append(Enabled.ToString(CultureInfo.InvariantCulture));
			b.Append("for all: ");
			b.Append(UseAdrForAll.ToString(CultureInfo.InvariantCulture));
			b.Append("HTTP: ");
			b.Append(HTTP);
			b.Append("HTTP port: ");
			b.Append(HTTPPort.ToString(CultureInfo.InvariantCulture));
			b.Append("FTP: ");
			b.Append(FTP);
			b.Append("FTP port: ");
			b.Append(FTPPort.ToString(CultureInfo.InvariantCulture));
			b.Append("SSL: ");
			b.Append(SSL);
			b.Append("SSL port: ");
			b.Append(SSLPort.ToString(CultureInfo.InvariantCulture));
			b.Append("Socks: ");
			b.Append(Socks);
			b.Append("Socks port: ");
			b.Append(SocksPort.ToString(CultureInfo.InvariantCulture));
			return b.ToString();
		}
    }
}


