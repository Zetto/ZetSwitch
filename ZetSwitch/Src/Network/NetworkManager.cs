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
using System.Management;
using System.Diagnostics;
using Microsoft.Win32;
using System.ComponentModel;
using System.Threading;

namespace ZetSwitch.Network {
	public class NetworkManager : IDisposable {
		#region variables

		private readonly List<NetworkInterfaceSettings> connections;
		private readonly BackgroundWorker loader = new BackgroundWorker();
		private bool loaded;
		private bool disposed;
		private readonly AutoResetEvent waitingEvent = new AutoResetEvent(false);


		public event EventHandler DataLoaded;

		#endregion

		~NetworkManager() {
			Dispose(false);
		}

		private void LoaderDoWork(object o, DoWorkEventArgs e) {
			var worker = (BackgroundWorker) o;
			connections.Clear();
			try {
				ManagementObjectCollection adaptersCollection = LoadAdapters();
				if (adaptersCollection == null)
					return;

				foreach (ManagementObject objMo in adaptersCollection) {
					if (worker.CancellationPending) {
						e.Cancel = true;
						connections.Clear();
						waitingEvent.Set();
						return;
					}
					try {
						var If = new NetworkInterfaceSettings(new AdapterDataHelper(objMo));
						connections.Add(If);
					}
					catch (Exception ex) {
						Program.UseTrace(ex);
					}
				}
			}
			catch (NullReferenceException ex) {
				Program.UseTrace(ex);
				Trace.WriteLine(ex.Source);
				Trace.WriteLine(ex.Data);
				throw new NullReferenceException("error", ex);
			}
			catch (Exception ex) {
				Program.UseTrace(ex);
			}
			finally {
				waitingEvent.Set();
			}
		}

		private void LoaderRunWorkerCompleted(object o, RunWorkerCompletedEventArgs e) {
			waitingEvent.Set();
			if (e.Cancelled)
				return;
			loaded = true;
			if (DataLoaded != null)
				DataLoaded(this, null);
		}

		#region private

		private void SaveDataToRegistry(NetworkInterfaceSettings settings, string controlSet) {
			var key =
				Registry.LocalMachine.CreateSubKey("SYSTEM\\" + controlSet + "\\Services\\Tcpip\\Parameters\\Interfaces\\" +
				                                   settings.SettingId);
			if (key != null) {
				var multi = new string[1];
				if (settings.IsDHCP) {
					multi[0] = "";
					key.SetValue("EnableDHCP", 1);
					key.SetValue("IPAddress", multi, RegistryValueKind.MultiString);
					key.SetValue("SubnetMask", multi, RegistryValueKind.MultiString);
					key.SetValue("DefaultGateway", multi, RegistryValueKind.MultiString);
				}
				else {
					key.SetValue("EnableDHCP", 0);
					multi[0] = settings.IP.ToString();
					key.SetValue("IPAddress", multi, RegistryValueKind.MultiString);
					multi[0] = settings.Mask.ToString();
					key.SetValue("SubnetMask", multi, RegistryValueKind.MultiString);
					multi[0] = settings.GateWay.ToString();
					key.SetValue("DefaultGateway", multi, RegistryValueKind.MultiString);
				}
			}
		}

		private static ManagementObjectCollection LoadAdapters() {
			ManagementObjectCollection adaptersCollection;
			using (var objMC = new ManagementObjectSearcher()) {
				objMC.Query = new ObjectQuery("Select * from Win32_NetworkAdapterConfiguration Where IPEnabled = True");
				adaptersCollection = objMC.Get();
			}
			return adaptersCollection;
		}

		#endregion

		#region public

		public NetworkManager() {
			connections = new List<NetworkInterfaceSettings>();
			loader.WorkerSupportsCancellation = true;
			loader.DoWork += LoaderDoWork;
			loader.RunWorkerCompleted += LoaderRunWorkerCompleted;
		}

		public List<NetworkInterfaceSettings> Connections {
			get { return connections; }
		}

		public int InterfaceCount {
			get { return connections.Count; }
		}

		public bool IsLoaded() {
			return loaded;
		}

		public void StartLoad() {
			if (!loaded && !loader.IsBusy)
				loader.RunWorkerAsync();
		}

		public bool Save(NetworkInterfaceSettings settings) {
			ManagementObjectCollection adaptersCollection;

			using (var objMC = new ManagementObjectSearcher()) {
				objMC.Query = new ObjectQuery("Select * from Win32_NetworkAdapterConfiguration Where IPEnabled = True");

				adaptersCollection = objMC.Get();
			}

			foreach (ManagementObject objMo in adaptersCollection) {
				if (Convert.ToBoolean(objMo["ipEnabled"]) == false)
					continue;

				if (settings.SettingId != (string) objMo["SettingID"])
					continue;

				//IP
				if (settings.IsDHCP) {
					objMo.InvokeMethod("EnableDHCP", null, null);
				}
				else {

					ManagementBaseObject objNewIP = objMo.GetMethodParameters("EnableStatic");
					ManagementBaseObject objNewGate = objMo.GetMethodParameters("SetGateways");

					objNewGate["DefaultIPGateway"] = new[] {settings.GateWay.ToString()};
					objNewGate["GatewayCostMetric"] = new[] {1};

					objNewIP["IPAddress"] = new[] {settings.IP.ToString()};
					objNewIP["SubnetMask"] = new[] {settings.Mask.ToString()};

					objMo.InvokeMethod("EnableStatic", objNewIP, null);
					objMo.InvokeMethod("SetGateways", objNewGate, null);
				}

				ManagementBaseObject objNewDNS = objMo.GetMethodParameters("SetDNSServerSearchOrder");
				var buff = new string[1];

				if (settings.IsDNSDHCP)
					buff = null;
				else {
					if (settings.DNS2.IP[0] != 0) {
						buff = new string[2];
						buff[1] = settings.DNS2.ToString();
					}
					buff[0] = settings.DNS1.ToString();

				}
				objNewDNS["DNSServerSearchOrder"] = buff;
				objMo.InvokeMethod("SetDNSServerSearchOrder", objNewDNS, null);

			}

			SaveDataToRegistry(settings, "CurrentControlSet");
			SaveDataToRegistry(settings, "ControlSet001");

			return true;
		}

		public List<NetworkInterfaceSettings> GetNetworkInterfaceSettings() {
			return connections;
		}

		#endregion

		private void Dispose(bool disposing) {
			if (!disposed) {
				if (disposing) {
					if (loader.IsBusy) {
						loader.CancelAsync();
						waitingEvent.WaitOne();
					}
					loader.Dispose();
				}
			}
			disposed = true;
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}