using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IWshRuntimeLibrary;
using System.IO;

namespace ZetSwitch {
	class ShorcutCreator {
		public void CreateProfileLnk(Profile profile) {
			WshShellClass wshShell = new WshShellClass();
			IWshRuntimeLibrary.IWshShortcut shortcut;
			shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"\\"+profile.Name+".lnk");
			shortcut.TargetPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			shortcut.WorkingDirectory = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Directory.FullName;
			shortcut.Arguments = "-p " + profile.Name;
			shortcut.Save();
		}
	}
}
