using System;

namespace ZetSwitch {
	public interface IProfileView : IDisposable {
		bool ShowView();
		void SetProfile(Profile actProfile);
		void UpdateInterfaceList();
		void UpdateIcon();
		string AskToSelectNewIcon(string path, string filter);

		event EventHandler SelectProfileIcon;

		
	}
}
