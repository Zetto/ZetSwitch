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

namespace ZetSwitch
{
    [Serializable]
    public class IPAddress
    {
        private byte[] IP;

        public IPAddress()
        {
            IP = new byte[4];
        }

        public IPAddress(byte[] Bytes)
        {
            IP = new byte[4];
            IP = Bytes;
        }

        public IPAddress(IPAddress Old)
        {
            IP = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                IP[i] = Old.IP[i];
            }
        }

        public IPAddress(string Str)
        {
            IP = ConvertStringIPToByteIP(Str);
        }

        public byte[] m_IP
        {
            get { return IP; }
        }


        public byte[] ConvertStringIPToByteIP(string StrIP)
        {

            byte[] ByteIP = new byte[4];
            string[] StrAr = StrIP.Split('.');
            if (StrAr.Length != 4)
				return new byte[4];
			try
			{
				for (int i = 0; i < 4; i++)
				{
					if (StrAr[i] == "")
						ByteIP[i] = 0;
					else
						ByteIP[i] = Convert.ToByte(StrAr[i]);
				}
			}
			catch (Exception) { }

            return ByteIP;
        }

        public void Clear()
        {
            IP = new byte[4];
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                str.Append(IP[i]);
                if (i != 3)
                    str.Append('.');
            }
            return str.ToString();
        }


        public static implicit operator byte[](IPAddress MIP)
        {
            return MIP.IP;
        }

        public static implicit operator IPAddress(string StrIP)
        {
            return new IPAddress(StrIP);
        }

        public static IPAddress operator ~(IPAddress MIP)
        {
            byte[] NewIP = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                NewIP[i] = (byte)~MIP.m_IP[i];
            }
            return new IPAddress(NewIP);
        }


        public bool Compare(IPAddress b)
        {
            for (int i = 0; i < 4; i++)
            {
                if (this.m_IP[i] != b.m_IP[i])
                    return false;
            }
            return true;
        }


        public static IPAddress operator &(IPAddress a, IPAddress b)
        {
            IPAddress NewIP = new IPAddress(a);
            for (int i = 0; i < 4; i++)
                NewIP.m_IP[i] &= b.m_IP[i];
            return NewIP;
        }

        public static IPAddress operator ^(IPAddress a, IPAddress b)
        {
            IPAddress NewIP = new IPAddress(a);
            for (int i = 0; i < 4; i++)
                NewIP.m_IP[i] ^= b.m_IP[i];
            return NewIP;
        }

        public bool Validation()
        {
            return true;
        }

        public bool IsZero()
        {
            for (int i = 0; i < 4; i++)
            {
                if (this.m_IP[i] != 0)
                    return false;
            }
            return true;
        }

        public bool SubnetMaskValidation()
        {
            bool WasZero = false;
            if (IP[0] < 224)
                return false;

            for (int i = 0; i < 4; i++)
            {
                byte Mask = 128;
                int res = 0;
                for (int j = 0; j < 8; j++)
                {
                    res = IP[i] & Mask;
                    if (res == 0)
                    {
                        WasZero = true;
                    }
                    else if (WasZero)
                        return false;
                    Mask >>= 1;
                }
            }
            return true;
        }

        public bool ValidateIPWithMask(IPAddress Mask)
        {
            if (!Mask.SubnetMaskValidation())
                throw new Exception(Language.GetText("NonValidSubNetMask"));

            IPAddress NegMask = ~Mask;
            IPAddress PCAddress = this & NegMask;
            if (PCAddress.IsZero())   //address of network
                return false;
            IPAddress BroadTest = PCAddress ^ NegMask;
            if (BroadTest.IsZero())  //broadcast address
                return false;
            return true;
        }

        public bool ComapreIPGWNet(IPAddress Mask, IPAddress GW)
        {
            if (!Mask.SubnetMaskValidation())
                throw new Exception(Language.GetText("NonValidSubNetMask"));

            IPAddress GWNet = GW & Mask;
            IPAddress IPNet = this & Mask;

            return GWNet.Compare(IPNet);
        }
    }
}
