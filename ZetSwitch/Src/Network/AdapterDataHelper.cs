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
using System.Management;
using System.Windows;
using System.Windows.Forms;
using System.Diagnostics;

namespace ZetSwitch
{
    public class AdapterDataHelper
    {
        ManagementObject obj;
	

        public AdapterDataHelper(ManagementObject Ob)
        {
            obj = Ob;
        }

        public AdapterDataHelper()
        {
        }


        public string ID
        {
            get 
            {
                return (string)obj["SettingID"];
            }
        }
        
        public IPAddress IP
        {
            get 
            {
                string[] Buff;
                Buff = (string[])obj["IPAddress"];
                if (Buff != null)
                    return new IPAddress(Buff[0]);
                else
                    return new IPAddress();
            }
        }

        public IPAddress Mask
        {
            get 
            {
                string[] Buff;
                Buff = (string[])obj["IPSubnet"];
                if (Buff != null)
                    return new IPAddress(Buff[0]);
                else
                    return new IPAddress();
            }
        }

        public IPAddress GW
        {
            get 
            {
                string[] Buff;
                Buff = (string[])obj["DefaultIPGateway"];
                if (Buff != null)
                    return new IPAddress(Buff[0]);
                else
                    return new IPAddress();
            }
        }

        public bool DHCP
        {
            get
            {
                return Convert.ToBoolean(obj["DHCPEnabled"].ToString());
            }
        }

        public string Name
        {
            get
            {
				ManagementObjectCollection name = null;
				using (ManagementObjectSearcher SearchAdapt = new ManagementObjectSearcher()) {
					SearchAdapt.Query = new ObjectQuery("Select * from Win32_NetworkAdapter Where MACAddress ='" + obj["MACAddress"] + "'");
					name = SearchAdapt.Get();
				}
				
                foreach (ManagementObject o in name)
                {
                    try
                    {
                           return o["NetConnectionID"].ToString();
                    }
                    catch (Exception e)
                    {
                        Program.UseTrace(e);
                    }
                }
                return "";
            }
        }

        public IPAddress[] DNS
        {
            get
            {
				IPAddress[] MIP = new IPAddress[2];
				string[] IP =(string[]) obj["DNSServerSearchOrder"];
                if (IP == null)
                    return MIP;
                for (int i = 0; i < IP.Length && i < MIP.Length; i++)
                {
                    MIP[i] = IP[i];
                }
                return MIP;
            }
        }
                        
    }
}
