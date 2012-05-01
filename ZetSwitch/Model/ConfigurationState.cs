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
