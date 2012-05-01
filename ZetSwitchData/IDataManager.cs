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
		List<string> GetBrowsersNames();
	}
}
