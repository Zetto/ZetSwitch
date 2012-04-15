using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZetSwitchData.Browsers {
	public class BrowsersManager {
		private Dictionary<BROWSERS, Browser> browsers;

		public BrowsersManager() {
			browsers = new Dictionary<BROWSERS, Browser>();
		}

		public BrowsersManager(BrowsersManager other) {
			browsers = new Dictionary<BROWSERS, Browser>(other.browsers);
		}
	}
}
