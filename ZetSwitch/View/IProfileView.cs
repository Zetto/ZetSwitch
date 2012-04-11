using System;
using System.Collections.Generic;
using ZetSwitchData;

namespace ZetSwitch {
	public interface IProfileView : IDisposable {
		bool ShowView();
		void SetProfile(Profile actProfile);
		void UpdateInterfaceList();
		void UpdateIcon();
		string AskToSelectNewIcon(string path, string filter);
		void ShowError(IList<string> messages);
		void Accept();

		event EventHandler SelectProfileIcon;
		event EventHandler Confirm;

	}
}
