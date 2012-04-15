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
using ZetSwitch.Forms;
using ZetSwitchData;
using ZetSwitchData.Browsers;

namespace ZetSwitch
{
    static class Program {
		static void WinFormApp(Arguments arg) {
            InitServices();

			var welcome = new WelcomeController(new ViewFactory());
            welcome.TryShow();

			var manager = new DataManager();

			try {
				manager.StartDelayedLoading();
				manager.LoadProfiles();
			}
			catch (Exception e) {
				MessageBox.Show(e.Message, ClientServiceLocator.GetService<ILanguage>().GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				UseTrace(e);
			}

            var frm = new MainForm(manager);

			try {
				new MainController(frm, manager);
				if (arg.Minimalize) {
					frm.GoToTray();
					Application.Run();
					manager.SaveSettings();
				}
				else if (arg.ConsoleMode) {
					ApplyProfile(manager, arg.Profiles[0]);
				}
				else {
					Application.Run(frm);
					manager.SaveSettings();
				}

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
				manager.Dispose();
				frm.Dispose();
				Properties.Settings.Default.Save();
			}
            
        }

    	private static void ApplyProfile(DataManager manager, string profile) {
			using (var tmpForm = new Form() { TopMost = true }) {
				if (manager.RequestApply(profile)) {

					MessageBox.Show(tmpForm, ClientServiceLocator.GetService<ILanguage>().GetText("ProfileApplied"),
					                ClientServiceLocator.GetService<ILanguage>().GetText("Succes"), MessageBoxButtons.OK,
					                MessageBoxIcon.Information);
				}
				else {
					MessageBox.Show(tmpForm, ClientServiceLocator.GetService<ILanguage>().GetText("CannotApply"),
					                ClientServiceLocator.GetService<ILanguage>().GetText("Error"), MessageBoxButtons.OK,
					                MessageBoxIcon.Error);
				}
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

            var arg = new Arguments(false);
            if (arg.Parse(args))  
				WinFormApp(arg);

            FlushDebug();
        }


		private static void InitServices() {
			IViewFactory viewFactory = new ViewFactory();
			var imageRepository = new ImageRepository();
			ClientServiceLocator.Register(viewFactory);
			ClientServiceLocator.Register<IUserConfiguration>(new UserConfiguration());
			ClientServiceLocator.Register<ISettingsController>(new SettingsController(viewFactory));
			ClientServiceLocator.Register<IAboutController>(new AboutController(viewFactory));
			ClientServiceLocator.Register<IProfileController>(new ProfileController());
			ClientServiceLocator.Register<IShortcutCreator>(new ShorcutCreator());
			ClientServiceLocator.Register<IImageRepository>(imageRepository);
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
        		var language = new Language();
				ClientServiceLocator.Register<ILanguage>(language);
				var store = new LanguagesStore();
				language.LoadDefault(store);
				language.LoadWords(Properties.Settings.Default.ActLanguage, store);
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, ClientServiceLocator.GetService<ILanguage>().GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
	#endregion

}


