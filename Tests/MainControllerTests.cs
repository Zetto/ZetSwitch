using NUnit.Framework;
using Is = Rhino.Mocks.Constraints.Is;
using Rhino.Mocks;
using ZetSwitch;
using Rhino.Mocks.Interfaces;

namespace Tests {
	[TestFixture]
	public class MainControllerTests {
		MockRepository mocks;
		IUserConfiguration config;
		IMainView view;
		IProfileManager manager;
		Profile profile;
		private const string name = "test";

		[SetUp]
		public void Init() {
			mocks = new MockRepository();
			config = mocks.StrictMock<IUserConfiguration>();
			mocks.Stub<IViewFactory>();
			manager = mocks.DynamicMock<IProfileManager>();
			view = mocks.DynamicMock<IMainView>();
			ClientServiceLocator.Register(config);
			profile = new Profile {Name = name};

		}

		[TearDown]
		public void TearDown() {
			mocks.VerifyAll();
		}

		[Test]
		public void ShouldAttachToAllEvents() {
			EventHelper.EventIsAttached(()=> { view.ApplyProfile += null; });
			EventHelper.EventIsAttached(()=> { view.RemoveProfile += null; });
			EventHelper.EventIsAttached(()=> { view.ChangeProfile += null; });
			EventHelper.EventIsAttached(()=> { view.NewProfile += null; });
			EventHelper.EventIsAttached(()=> { view.Exit += null; });
			EventHelper.EventIsAttached(()=> { view.OpenAbout += null; });
			EventHelper.EventIsAttached(()=> { view.OpenSettings += null; });
			EventHelper.EventIsAttached(()=> { view.CreateShortcut += null; });
			mocks.ReplayAll();
			new MainController(view, manager);
		}

		[Test]
		public void ShouldCraeteShortcut() {
			var shortcut = mocks.StrictMock<IShortcutCreator>();
			ClientServiceLocator.Register(shortcut);

			view.Stub(x => x.GetSelectedProfile()).Return(name);
			manager.Stub(x => x.GetProfile(name)).Return(profile);
			shortcut.Stub(x => x.CreateProfileLnk(profile)).Repeat.Once();
			view.CreateShortcut += null;
			LastCall.Constraints(Is.NotNull());
			IEventRaiser createShortcutEvent = LastCall.GetEventRaiser();
			mocks.ReplayAll();

			new MainController(view, manager);
			createShortcutEvent.Raise(null, null);
		}

		[Test]
		public void ShouldCancelApplyProfileAfterUserCancel() {
			var evnt = new EventHelper(() => { view.ApplyProfile += null; });
			view.Stub(x => x.AskToApplyProfile(name)).Repeat.Once().Return(false);
			manager.Stub(x => x.Apply(name)).Repeat.Never().Return(true);

			mocks.ReplayAll();

			new MainController(view, manager);
			evnt.Raise(null, new ProfileEventArgs(name));
		}

		[Test]
		public void ShouldAskForApplyAndApplyProfile() {
			var evnt = new EventHelper(() => {view.ApplyProfile += null;});

			view.Stub(x => x.GetSelectedProfile()).Return(name);
			manager.Stub(x => x.GetProfile(name)).Return(profile);
			view.Stub(x => x.AskToApplyProfile(name)).Repeat.Once().Return(true);
			manager.Stub(x => x.Apply(name)).Repeat.Once().Return(true);
			view.Stub(x => x.ShowInfoMessage("", "")).IgnoreArguments().Repeat.Once();
			view.Stub(x => x.ShowErrorMessage("")).IgnoreArguments().Repeat.Never();

			mocks.ReplayAll();

			new MainController(view, manager);
			evnt.Raise(null, new ProfileEventArgs(name));
		}

		[Test]
		public void ShouldDeleteProfile() {
			var evnt = new EventHelper(() => { view.RemoveProfile += null; });

			view.Stub(x => x.GetSelectedProfile()).Return(name);
			view.Stub(x => x.AskQuestion("","")).IgnoreArguments().Repeat.Once().Return(true);
			manager.Stub(x => x.Delete(name)).Repeat.Once();
			view.Stub(x => x.ReloadList()).Repeat.Once();
			mocks.ReplayAll();
	
			new MainController(view, manager);
			evnt.Raise();
		}

		[Test]
		public void ShouldOpenAbout() {
			var evnt = new EventHelper(() => { view.OpenAbout += null; });
			var about = mocks.StrictMock<IAboutController>();
			ClientServiceLocator.Register(about);
			about.Stub(x => x.Show()).Repeat.Once();			
			mocks.ReplayAll();

			new MainController(view, manager);
			evnt.Raise();
		}

		[Test]
		public void ShouldOpenSettingsAndResetLanguage() {
			var evnt = new EventHelper(() => { view.OpenSettings += null; });
			var settings = mocks.StrictMock<ISettingsController>();
			ClientServiceLocator.Register(settings);
			settings.Stub(x => x.Show()).Repeat.Once().Return(true);
			view.Stub(x => x.ResetLanguage()).Repeat.Once();
			mocks.ReplayAll();

			new MainController(view, manager);
			evnt.Raise();
		}
	}
}
