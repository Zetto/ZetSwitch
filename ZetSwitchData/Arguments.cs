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
using System.Collections;

namespace ZetSwitchData {
	public enum ConsoleActions {
		UseProfile
	}

	public class Arguments {
		private readonly StringBuilder errors;

		public string Errors {
			get { return errors.ToString(); }
		}

		public ArrayList Actions { get; private set; }
		public ArrayList Profiles { get; private set; }
		public bool ConsoleMode { get; private set; }
		public bool Minimalize { get; private set; }
		public int Count { get; private set; }

		public Arguments() {
			errors = new StringBuilder();
			Actions = new ArrayList();
			Profiles = new ArrayList();
		}

		public bool Parse(string[] args) {
			var c = args.Length;

			for (int i = 0; i < c; i++) {
				switch (args[i]) {
					case "-autorun":
						Minimalize = true;
						return true;

					case "-p":
						++i;
						if (i >= c) {
							errors.Append(ClientServiceLocator.GetService<ILanguage>().GetText("ConsoleNotProfiles"));
							break;
						}
						GetProfilesString(i, args);
						Actions.Add(ConsoleActions.UseProfile);
						Count++;
						ConsoleMode = true;
						break;
					default:
						errors.Append(ClientServiceLocator.GetService<ILanguage>().GetText("ConsoleInvalidArgument"));
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
