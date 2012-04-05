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

using NUnit.Framework;
using ZetSwitch;
using Rhino.Mocks;

namespace Tests {
	[TestFixture]
	public class SettingsControllerTests {
		MockRepository mocks;
		IUserConfiguration config;
		IViewFactory factory;
		ISettingsView view;

		[SetUp]
		public void Init() {
			mocks = new MockRepository();
			config = mocks.StrictMock<IUserConfiguration>();
			factory = mocks.Stub<IViewFactory>();
			view = MockRepository.GenerateStub<ISettingsView>();
			ClientServiceLocator.Register(config);
		}

		[TearDown]
		public void TearDown() {
			mocks.VerifyAll();
		}

		[Test]
		public void ShouldSaveUserConfigurationAfterUserChooseSaveSettings() {
			var state = new ConfigurationState();
			factory.Stub(x => x.CreateSettingsView()).Return(view);
			view.Stub(x => x.ShowView()).Return(true);
			config.Stub(x => x.LoadConfiguration()).Repeat.Once().Return(state);
			config.Stub(x => x.SaveConfigurate(state)).Repeat.Once();
			mocks.ReplayAll();
			
			var controller = new SettingsController(factory);
			Assert.AreEqual(true,controller.Show());
		}

		[Test]
		public void ShouldNotSaveUserConfigurationAfterUserChooseCancel() {
			var state = new ConfigurationState();
			factory.Stub(x => x.CreateSettingsView()).Return(view);
			view.Stub(x => x.ShowView()).Return(false);
			config.Stub(x => x.LoadConfiguration()).Repeat.Once().Return(state);
			config.Stub(x => x.SaveConfigurate(state)).Repeat.Never();
			mocks.ReplayAll();

			var controller = new SettingsController(factory);
			Assert.AreEqual(false,controller.Show());
		}
	}
}
