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
using IWshRuntimeLibrary;
using System.IO;
using ZetSwitchData;

namespace ZetSwitch {
	public interface IShortcutCreator {
		void CreateProfileLnk(Profile profile);
	}

	internal class ShorcutCreator : IShortcutCreator {
		public void CreateProfileLnk(Profile profile) {
			var wshShell = new WshShellClass();
			var shortcut =
				wshShell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + profile.Name +
				                        ".lnk") as IWshShortcut;
			if (shortcut == null)
				return;

			shortcut.TargetPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			var directoryInfo = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Directory;
			if (directoryInfo == null)
				return;

			shortcut.WorkingDirectory = directoryInfo.FullName;
			shortcut.Arguments = "-p " + profile.Name;
			shortcut.Save();
		}
	}
}
