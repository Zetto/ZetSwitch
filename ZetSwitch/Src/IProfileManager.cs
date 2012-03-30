using System;
namespace ZetSwitch {
	interface IProfileManager {
		Profile New();
		Profile Clone(string name);
		Profile GetProfile(string name);
		void Add(Profile profile);
		bool Apply(string name);
		void Delete(string name);
		string GetNewProfileName();
		void Change(string oldName, Profile profile);
	}
}
