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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZetSwitchData {
	public enum ConsoleActions {
		UseProfile
	}

	public class Arguments {
		private readonly StringBuilder errors;
		private readonly bool console;

		public string Errors {
			get { return errors.ToString(); }
		}

		public List<ConsoleActions> Actions { get; set; }
		public List<string> Profiles { get; set; }
		public bool ConsoleMode { get; private set; }
		public bool Minimalize { get; private set; }
		public int Count { get; private set; }

		public Arguments(bool console) {
			Actions = new List<ConsoleActions>();
			Profiles = new List<string>();
			this.console = console;
			errors = new StringBuilder();
		}

		public bool Parse(string[] args) {
			var c = args.Length;
			if (console && c < 2) {
				errors.Append(ClientServiceLocator.GetService<ILanguage>().GetText("ConsoleInvalidArgument"));
				return false;
			}
			for (int i = 0; i < c; i++) {
				switch (args[i]) {
					case "-autorun":
						if (console) {
							errors.Append(ClientServiceLocator.GetService<ILanguage>().GetText("FormInvalidArgument"));
							return false;
						}
						Minimalize = true;
						return true;
					case "-p":
						++i;
						if (i >= c) {
							errors.Append(ClientServiceLocator.GetService<ILanguage>().GetText("ConsoleNotProfiles"));
							return false;
						}
						GetProfilesString(i, args);
						Actions.Add(ConsoleActions.UseProfile);
						Count++;
						ConsoleMode = true;
						break;
					default:
						errors.Append(ClientServiceLocator.GetService<ILanguage>().GetText(console ? "ConsoleInvalidArgument" : "FormInvalidArgument"));
						break;
				}
			}
			return (errors.Length == 0);
		}

		private void GetProfilesString(int start, IList<string> args) {
			if (args == null) throw new ArgumentNullException("args");
			var prof = args[start].Split(';');
			if (prof.Length < 1)
				errors.Append(ClientServiceLocator.GetService<ILanguage>().GetText("ConsoleNotProfiles"));
			foreach (var str in prof.Where(str => str.Length > 0))
				Profiles.Add(str);
		}
	}
}
