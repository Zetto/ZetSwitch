using System;
using System.Diagnostics;
using ZetSwitchData;

namespace ZetswitcWorker {
	class Program {

		static private int ConsoleApp(Arguments arg) {
			var manager = new DataManager();
			try {
				manager.LoadProfiles();
			}
			catch (Exception e) {
				Console.WriteLine("Error: " + e.Message);
				manager.Dispose();
				return 1;
			}
			bool result = true;
			foreach (ConsoleActions act in arg.Actions) {
				switch (act) {
					case ConsoleActions.UseProfile:
						foreach (string profile in arg.Profiles) {
							bool r = manager.Apply(profile);
							result &= r;
							if (r)
								Console.WriteLine(profile + ": OK");
							else
								Console.WriteLine(profile + ": FAILED");
						}
						break;
				}
			}
			manager.Dispose();
			return result ? 0 : 1;
		}

		static int Main(string[] args) {
			SetDebugSettings();
			LoadLanguage();

			var arg = new Arguments(true);
			if (arg.Parse(args)) {
				return ConsoleApp(arg); 
			}
			Console.WriteLine(arg.Errors);
			return 1;
		}

		private static void SetDebugSettings() {
			using (var fileListener = new TextWriterTraceListener("errorlog.txt")) {
				Trace.Listeners.Clear();
				Trace.Listeners.Add(fileListener);
			}
		}

		private static void LoadLanguage() {
			try {
				var language = new Language();
				ClientServiceLocator.Register<ILanguage>(language);
				var store = new LanguagesStore();
				language.LoadDefault(store);
			}
			catch (Exception ) {
			}
		}




	}
}
