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

using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using ZetSwitch;
using ZetSwitchData;
using ZetSwitchData.Network;

namespace Tests {
	[TestFixture]
	class ProfileControllerTests {
		MockRepository mocks;
		IUserConfiguration config;
		IProfileView view;
		IDataManager manager;

		[SetUp]
		public void Init() {
			mocks = new MockRepository();
			config = mocks.StrictMock<IUserConfiguration>();
			view = mocks.DynamicMock<IProfileView>();
			manager = mocks.DynamicMock<IDataManager>();
			ClientServiceLocator.Register(config);
		}


		[TearDown]
		public void TearDown() {
			mocks.VerifyAll();
		}

		[Test]
		public void ShouldAttachToAllEvents() {
			view = mocks.StrictMock<IProfileView>();
			EventHelper.EventIsAttached(() => { view.Confirm += null; });
			EventHelper.EventIsAttached(() => { view.SelectProfileIcon += null; });
			mocks.ReplayAll();
			var controller = new ProfileController();
			controller.SetView(view);
		}

		[Test]
		public void ShouldAcceptNewProfile() {
			const string name = "test";
			var evnt = new EventHelper(() => { view.Confirm += null; });
			
			var profile = new Profile {Name = name};
			manager.Stub(x => x.ContainsProfile(name)).Repeat.Once().Return(false);
			view.Stub(x => x.ShowError(new[] {""})).IgnoreArguments().Repeat.Never();
			mocks.ReplayAll();
			var controller = new ProfileController();
			controller.SetView(view);
			controller.SetManager(manager);
			controller.SetProfile(profile, true);
			evnt.Raise();
		}

		[Test]
		public void ShouldDeclineExistingProfile() {
			const string name = "test";
			var evnt = new EventHelper(() => { view.Confirm += null; });

			var profile = new Profile { Name = name };
			manager.Stub(x => x.ContainsProfile(name)).Repeat.Once().Return(true);
			view.Stub(x => x.ShowError(new[] { "" })).IgnoreArguments().Repeat.Once();
			mocks.ReplayAll();
			var controller = new ProfileController();
			controller.SetView(view);
			controller.SetManager(manager);
			controller.SetProfile(profile, true);
			evnt.Raise();
		}

		[Test]
		public void ShouldAllowChangeName() {
			const string name = "test";
			const string name2 = "test2";
			var evnt = new EventHelper(() => { view.Confirm += null; });

			var profile = new Profile { Name = name };
			manager.Stub(x => x.ContainsProfile(name2)).Repeat.Once().Return(false);
			view.Stub(x => x.ShowError(new[] { "" })).IgnoreArguments().Repeat.Never();
			mocks.ReplayAll();
			var controller = new ProfileController();
			controller.SetView(view);
			controller.SetManager(manager);
			controller.SetProfile(profile, false);
			profile.Name = name2;
			evnt.Raise();
		}

		[Test]
		public void ShouldAttachToLoadModel() {
			manager = mocks.StrictMock<IDataManager>();
			EventHelper.EventIsAttached(() => { manager.DataLoaded += null; });
			manager.Stub(x => x.IsIFLoaded()).Return(false);
			mocks.ReplayAll();
			var controller = new ProfileController();
			controller.SetManager(manager);
		}

		[Test]
		public void ShouldMergeInterfacesFromProfileAndModel() {
			var allNames = new[] {"if1","if2","if3"};
			var evnt = new EventHelper(() => { manager.DataLoaded += null;  });
			var ifs = new List<NetworkInterfaceSettings> { new NetworkInterfaceSettings { Name = allNames[0] }, new NetworkInterfaceSettings { Name = allNames[2] } };
	
			var profile = new Profile();
			profile.Connections = new ProfileNetworkSettingsList { new ProfileNetworkSettings { Settings = new NetworkInterfaceSettings { Name = allNames[1] } },
																	new ProfileNetworkSettings { Settings = new NetworkInterfaceSettings { Name = allNames[2] } }};
			manager.Stub(x => x.IsIFLoaded()).Return(false);
			manager.Stub(x => x.GetNetworkInterfaceSettings()).Return(ifs);

			mocks.ReplayAll();
			var controller = new ProfileController();
			controller.SetView(view);
			controller.SetManager(manager);
			controller.SetProfile(profile, false);
			evnt.Raise();

			Assert.AreEqual(3,profile.Connections.GetNetworkInterfaceNames().Count);
		}
	}
}
