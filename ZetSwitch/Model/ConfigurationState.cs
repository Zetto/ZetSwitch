using System.Collections.Generic;
using System.Linq;
using ZetSwitchData;

namespace ZetSwitch {
	public class ConfigurationState {
		public List<LanguageDescription> AvailableLanguages { get; set; }
		public bool Autorun { get; set; }
		public bool ShowWelcome { get; set; }
		public string Language { get; set; }
		
		public List<string> GetLanguages() {
			return AvailableLanguages.Select(l => l.Name).ToList();
		}

		public string LanguageShort {
			get {
				LanguageDescription desc = AvailableLanguages.Find(i => i.Name == Language);
				return desc != null ? desc.ShortName : "";
			}
			set {
				LanguageDescription desc =AvailableLanguages.Find(i => i.ShortName == value);
				Language = desc.Name;
			}
		}
	}
}
