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
using System.Collections;
using System.Diagnostics;
using Microsoft.Win32;

namespace ZetSwitch.Network
{
    public class NetworkManager
	{
		#region variables

		List<NetworkInterfaceSettings> connections;
		
		#endregion

		#region private

		private void SaveDataToRegistry(NetworkInterfaceSettings settings,string ControlSet)
		{
			RegistryKey Key = Registry.LocalMachine.CreateSubKey("SYSTEM\\" + ControlSet + "\\Services\\Tcpip\\Parameters\\Interfaces\\" + settings.SettingId);
			if (Key != null)
			{
				string[] Multi = new string[1];
				if (settings.IsDHCP)
				{
					Multi[0] = "";
					Key.SetValue("EnableDHCP", 1);
					Key.SetValue("IPAddress", Multi, RegistryValueKind.MultiString);
					Key.SetValue("SubnetMask", Multi, RegistryValueKind.MultiString);
					Key.SetValue("DefaultGateway", Multi, RegistryValueKind.MultiString);
				}
				else
				{
					Key.SetValue("EnableDHCP", 0);
					Multi[0] = settings.IP.ToString();
					Key.SetValue("IPAddress", Multi, RegistryValueKind.MultiString);
					Multi[0] = settings.Mask.ToString();
					Key.SetValue("SubnetMask", Multi, RegistryValueKind.MultiString);
					Multi[0] = settings.GateWay.ToString();
					Key.SetValue("DefaultGateway", Multi, RegistryValueKind.MultiString);
				}
			}
		}

		private ManagementObjectCollection LoadAdapters()
		{
			ManagementObjectCollection AdaptersCollection = null;
			using (ManagementObjectSearcher objMC = new ManagementObjectSearcher()) {
				using (ManagementObjectSearcher SearchAdapt = new ManagementObjectSearcher()) {
					objMC.Query = new ObjectQuery("Select * from Win32_NetworkAdapterConfiguration Where IPEnabled = True");

					if (objMC == null) {
						Trace.WriteLine("Nepodarilo se vytvorit tridu Win32_NetworkAdapterConfiguration. Nelze pokracovat");
						Trace.Flush();
						return null;
					}
					AdaptersCollection = objMC.Get();
					if (AdaptersCollection == null) {
						Trace.WriteLine("Nepodarilo se ziskat informace o pripojenich");
						Trace.Flush();
						return null;
					}
				}
			}
			return AdaptersCollection;
		}

		#endregion

		#region public

		public NetworkManager()
		{
			connections = new List<NetworkInterfaceSettings>();
		}

		public List<NetworkInterfaceSettings> Connections
		{
			get { return connections; }
		}

		public int InterfaceCount
		{
			get { return connections.Count; }
		}

		public bool LoadData()
		{
			connections.Clear();
			try
			{
				ManagementObjectCollection AdaptersCollection = LoadAdapters();
				if (AdaptersCollection == null)
					return false;

				foreach (ManagementObject ObjMo in AdaptersCollection)
				{
					try
					{
						NetworkInterfaceSettings If = new NetworkInterfaceSettings(new AdapterDataHelper(ObjMo));
						connections.Add(If);
					}
					catch (Exception e)
					{
						Program.UseTrace(e);
					}
				}
			}
			catch (NullReferenceException e)
			{
				Program.UseTrace(e);
				Trace.WriteLine(e.Source);
				Trace.WriteLine(e.Data);
				throw new NullReferenceException("error", e);
			}
			catch (Exception e)
			{
				Program.UseTrace(e);
			}
			return true;
		}

		public bool Save(NetworkInterfaceSettings settings)
		{
			ManagementObjectCollection AdaptersCollection = null;

			using (ManagementObjectSearcher objMC = new ManagementObjectSearcher()) {
				objMC.Query = new ObjectQuery("Select * from Win32_NetworkAdapterConfiguration Where IPEnabled = True");

				if (objMC == null)
				{
					Trace.WriteLine("Nepodarilo se vytvorit tridu Win32_NetworkAdapterConfiguration. Nelze pokracovat");
					Trace.Flush();
				}

				AdaptersCollection = objMC.Get();
				if (AdaptersCollection == null)
				{
					Trace.WriteLine("Nepodarilo se ziskat informace o pripojenich");
					Trace.Flush();
					return false;
				}
			}

			foreach (ManagementObject ObjMo in AdaptersCollection)
			{
				if (Convert.ToBoolean(ObjMo["ipEnabled"]) == false)
					continue;

				if (settings.SettingId != (string)ObjMo["SettingID"])
					continue;

				ManagementBaseObject objNewIP = null;
				ManagementBaseObject objSetIP = null;
				ManagementBaseObject objNewGate = null;
				//IP
				if (settings.IsDHCP)
				{
					objSetIP = ObjMo.InvokeMethod("EnableDHCP", null, null);
				}
				else
				{

					objNewIP = ObjMo.GetMethodParameters("EnableStatic");
					objNewGate = ObjMo.GetMethodParameters("SetGateways");

					objNewGate["DefaultIPGateway"] = new string[] { settings.GateWay.ToString() };
					objNewGate["GatewayCostMetric"] = new int[] { 1 };

					objNewIP["IPAddress"] = new string[] { settings.IP.ToString() };
					objNewIP["SubnetMask"] = new string[] { settings.Mask.ToString() };

					objSetIP = ObjMo.InvokeMethod("EnableStatic", objNewIP, null);
					objSetIP = ObjMo.InvokeMethod("SetGateways", objNewGate, null);
				}

				ManagementBaseObject objNewDNS = null;
				objNewDNS = ObjMo.GetMethodParameters("SetDNSServerSearchOrder");
				string[] Buff = new string[1];

				if (settings.IsDNSDHCP)
					Buff = null;
				else
				{
					if (settings.DNS2.m_IP[0] != 0)
					{
						Buff = new string[2];
						Buff[1] = settings.DNS2.ToString();
					}
					Buff[0] = settings.DNS1.ToString();

				}
				objNewDNS["DNSServerSearchOrder"] = Buff;
				objSetIP = ObjMo.InvokeMethod("SetDNSServerSearchOrder", objNewDNS, null);

			}

			SaveDataToRegistry(settings, "CurrentControlSet");
			SaveDataToRegistry(settings, "ControlSet001");

			return true;
		}

		public List<NetworkInterfaceSettings> GetNetworkInterfaceSettings()
		{
			return connections;
		}
		#endregion
    }
}
