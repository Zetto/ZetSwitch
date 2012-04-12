using System;
using System.Collections.Generic;
using ZetSwitchData.Network;

namespace ZetSwitchData {
	public interface IDataManager : IDisposable {
		Profile New();
		Profile Clone(string name);
		Profile GetProfile(string name);
		void Add(Profile profile);
		bool Apply(string name);
		void Delete(string name);
		void Change(string oldName, Profile profile);
		bool IsIFLoaded();
		bool ContainsProfile(string name);

		event EventHandler DataLoaded;
		List<NetworkInterfaceSettings> GetNetworkInterfaceSettings();
		void StartDelayedLoading();
		bool RequestApply(string profile);
	}
}
