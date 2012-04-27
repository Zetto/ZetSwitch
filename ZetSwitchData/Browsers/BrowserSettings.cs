namespace ZetSwitchData.Browsers {
	public class BrowserSettings {
		public string HomePage { get; set; }
		public ProxySettings Proxy { get; set; }

		public BrowserSettings() {
			HomePage = "";
			Proxy = new ProxySettings();
		}

		public BrowserSettings(BrowserSettings other) {
			HomePage = other.HomePage;
			Proxy = new ProxySettings(other.Proxy);
		}

		public bool IsValid() {
			return Proxy.IsValid();
		}
	}
}
