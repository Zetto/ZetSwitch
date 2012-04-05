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
using Rhino.Mocks;
using Is = Rhino.Mocks.Constraints.Is;
using ZetSwitch;
using Rhino.Mocks.Interfaces;

namespace Tests {
	[TestFixture]
	class WelcomeControllerTests {
		MockRepository mocks;
		IUserConfiguration config;
		IViewFactory factory;
		IWelcomeView view;

		[SetUp]
		public void Init() {
			mocks = new MockRepository();
			config = mocks.StrictMock<IUserConfiguration>();
			factory = mocks.Stub<IViewFactory>();
			view = mocks.DynamicMock<IWelcomeView>();
			ClientServiceLocator.Register(config);
		}

		[TearDown]
		public void TearDown() {
			mocks.VerifyAll();
		}

		[Test]
		public void ShouldNotShowViewIfShowWelcomeIsDisabled() {
			var state = new ConfigurationState {ShowWelcome = false};
			factory.Stub(x => x.CreateWelcomeView()).Return(view);
			config.Stub(x => x.LoadConfiguration()).Repeat.Once().Return(state);
			view.Stub(x => x.ShowView()).Repeat.Never();
			mocks.ReplayAll();

			var controller = new WelcomeController(factory);
			controller.TryShow();
		}

		private void InitForSucces(ConfigurationState state) {
			state.ShowWelcome = true;
			factory.Stub(x => x.CreateWelcomeView()).Return(view);
			config.Stub(x => x.LoadConfiguration()).Repeat.Once().Return(state);
			view.Stub(x => x.ShowView()).Repeat.Once().Return(true);
		}

		[Test]
		public void ShouldSaveUserSettings() {
			var state = new ConfigurationState();
			InitForSucces(state);
			config.Stub(x => x.SaveConfigurate(state)).Repeat.Once();
			mocks.ReplayAll();

			var controller = new WelcomeController(factory);
			controller.TryShow();
		}

		[Test]
		public void ShouldReactToChangeLanguageEvent() {
			var state = new ConfigurationState();
			InitForSucces(state);

			config.Stub(x => x.SaveConfigurate(state)).Repeat.Twice();
			view.LanguageChanged += null;
			LastCall.Constraints(Is.NotNull());
			IEventRaiser changeLangRaiser = LastCall.GetEventRaiser();
			view.Stub(x => x.ResetLanguage()).Repeat.Once();
			mocks.ReplayAll();

			var controller = new WelcomeController(factory);
			controller.TryShow();
			changeLangRaiser.Raise(null,null);
		}
	}
}
