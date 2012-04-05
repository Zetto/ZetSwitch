using System;
using Rhino.Mocks;
using Is = Rhino.Mocks.Constraints.Is;
using Rhino.Mocks.Interfaces;


namespace Tests {
	class EventHelper {
		readonly IEventRaiser raiser;

		public delegate void EventDelegate();

		public EventHelper(EventDelegate evntDelegate) {
			evntDelegate();
			LastCall.Constraints(Is.NotNull());
			raiser = LastCall.GetEventRaiser();
		}

		static public void EventIsAttached(EventDelegate evntDelegate) {
			evntDelegate();
			LastCall.Constraints(Is.NotNull());
		}

		public void Raise() {
			raiser.Raise(null, null);
		}

		public void Raise(object o, EventArgs e) {
			raiser.Raise(o, e);
		}
	}
}
