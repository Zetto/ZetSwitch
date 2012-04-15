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

namespace Tests {
	[TestFixture]
	class AboutControllerTests {
		MockRepository mocks;
		IViewFactory factory;
		IAboutView view;

		[SetUp]
		public void Init() {
			mocks = new MockRepository();
			factory = mocks.Stub<IViewFactory>();
			view = mocks.DynamicMock<IAboutView>();
		}

		[TearDown]
		public void TearDown() {
			mocks.VerifyAll();
		}

		[Test]
		public void ShouldShowViewAndAttachEmailClickedEvent() {
			factory.Stub(x => x.CreateAboutView()).Return(view);
			view.Stub(x => x.ShowView()).Repeat.Once();
			view.EmailClicked += null;
			LastCall.Constraints(Is.NotNull());
			mocks.ReplayAll();

			var controller = new AboutController(factory);
			controller.Show();
		}
	}
}
