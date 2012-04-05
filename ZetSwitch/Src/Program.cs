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
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ZetSwitch.Forms;

namespace ZetSwitch
{
    static class Program {
		private const int AttachParentProcess = -1;

        static private void BadArgs(Arguments arg) {
            NativeMethods.AttachConsole(AttachParentProcess);
            Console.Write(arg.Errors);
            NativeMethods.FreeConsole();
        }

        static private void ConsoleApp(Arguments arg) {
            NativeMethods.AttachConsole(AttachParentProcess);
            var model = new DataModel();
        	var manager = new ProfileManager {Model = model};
        	try {
                manager.LoadSettings();
            }
            catch (Exception e) {
                Console.WriteLine(Language.GetText("Error") + ": " + e.Message);
                UseTrace(e);
                model.Dispose();
                return;
            }

            foreach (ConsoleActions act in arg.Actions) {
                switch (act) {
                    case ConsoleActions.UseProfile:
                        foreach (string profile in arg.Profiles) {
                            if (manager.Apply(profile))
                                Console.WriteLine(profile + ": OK");
                            else
                                Console.WriteLine(profile + ": FAILED");
                        }
                        break;
                }
            }
            model.Dispose();
            NativeMethods.FreeConsole();
        }

        static void WinFormApp(Arguments arg) {
            InitServices();
            var model = new DataModel();
        	var manager = new ProfileManager {Model = model};
        	try {
                manager.LoadSettings();
                model.LoadData();
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                UseTrace(e);
            }

            var welcome = new WelcomeController(new ViewFactory());
            welcome.TryShow();

            var frm = new MainForm(manager);
			try {
				new MainController(frm, manager);
				if (arg.Minimalize) {
					frm.GoToTray();
					Application.Run();
				}
				else
					Application.Run(frm);
				manager.SaveSettings();
			}
			catch (Exception e) {
				using (var form = new ExceptionForm(e.Message + "\n\n" + e.StackTrace)) {
					form.FormBorderStyle = FormBorderStyle.FixedDialog;
					form.StartPosition = FormStartPosition.CenterScreen;
					form.ShowDialog();
				}
				UseTrace(e);
			}
			finally {
				model.Dispose();
				frm.Dispose();
				Properties.Settings.Default.Save();
			}
            
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
		/// 
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SetDebugSettings();
            LoadLanguage();

            var arg = new Arguments();
            if (!arg.Parse(args))  {  //bad arguments
                BadArgs(arg);
            }
            else if (arg.Count != 0)  { //console
                ConsoleApp(arg); 
            }
            else {   //win form
                WinFormApp(arg);
			}
            FlushDebug();
        }


		private static void InitServices() {
			IViewFactory viewFactory = new ViewFactory();
			ClientServiceLocator.Register<IUserConfiguration>(new UserConfiguration());
			ClientServiceLocator.Register<ISettingsController>(new SettingsController(viewFactory));
			ClientServiceLocator.Register<IAboutController>(new AboutController(viewFactory));
			ClientServiceLocator.Register<IShortcutCreator>(new ShorcutCreator());
		}

		#region TOOLS

		public static void UseTrace(Exception e) {
            Trace.Write(e.Message);
            Trace.WriteLine(' ');
            Trace.Write(e.StackTrace);
            Trace.WriteLine(' ');
            Trace.Flush();
        }

      
        private static void LoadLanguage() {
        	try {
				var store = new LanguagesStore();
				Language.LoadDefault(store);
				Language.LoadWords(Properties.Settings.Default.ActLanguage, store);
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                UseTrace(e);
            }
        }

    	private static void SetDebugSettings() {
			using (var fileListener = new TextWriterTraceListener("errorlog.txt")) {
				Trace.Listeners.Clear();
				Trace.Listeners.Add(fileListener);
			}
        }

        private static void FlushDebug() {
            foreach (TraceListener listener in Trace.Listeners) {
                listener.Flush();
                listener.Close();
            }
        }
    }


	internal static class NativeMethods
    {
		[DllImport("kernel32.dll")]
		internal static extern bool AttachConsole(int dwProcessId);
		[DllImport("kernel32.dll")]
		internal static extern Boolean FreeConsole();
    }

	#endregion

}


