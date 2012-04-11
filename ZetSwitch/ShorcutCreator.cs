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
