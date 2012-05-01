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

using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace ZetSwitchData {
	public class ProcessRunner {
		const string ProgramName = @"\ZetswitchWorker.exe";

		public bool Apply(string name)  {
			using (var proces = new Process()) {
				string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				proces.StartInfo.FileName =  dir + ProgramName;
				proces.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				proces.StartInfo.Arguments = "-p \"" + name + "\"";
				proces.Start();
				proces.WaitForExit();
				return proces.ExitCode == 0;
			}
		}
	}
}
