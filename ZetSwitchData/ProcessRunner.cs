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
				proces.StartInfo.Arguments = "-p " + name;
				proces.Start();
				proces.WaitForExit();
				return proces.ExitCode == 0;
			}
		}
	}
}
