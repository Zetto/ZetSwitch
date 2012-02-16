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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Management;
using System.Windows;
using ZetSwitch.Forms;

namespace ZetSwitch
{
    

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			DataModel model = new DataModel();
			model.LoadData(); // todo: strcit nekam do paze ... Musi byt volano pred loadconfig v manageru, nebo tuhle zavislost nejak rozbit...
			ProfileManager.GetInstance().Model = model;

            SetDebugSettings();
            LoadLanguage();

            Arguments Arg = new Arguments();

            if (!Arg.Parse(Args))    //bad arguments
            {
				NativeMethods.AllocConsole();
                Console.Write(Arg.Errors);
				NativeMethods.FreeConsole();
                return;
            }
            else if (Arg.Count != 0)  //console
            {
				NativeMethods.AllocConsole();
                try
                {
					ProfileManager.GetInstance().Model = model;
                    ProfileManager.GetInstance().LoadSettings();
                }
                catch (Exception e)
                {
                    Console.WriteLine(Language.GetText("Error") + ": " + e.Message);
                    UseTrace(e);
                    return;
                }
                foreach (ConsoleActions Act in Arg.Actions)
                {
                    switch (Act)
                    {
                        case ConsoleActions.UseProfile:
                            foreach (string profile in Arg.Profiles)
                            {
								if (ProfileManager.GetInstance().ApplyProfile(profile))
									Console.WriteLine(profile + ": OK");
								else
									Console.WriteLine(profile + ": FAILED");
                            }
                            break;
                        default:
                            break;
                    }
                }
				NativeMethods.FreeConsole();
            }
            else   //win form
            {

                if (Properties.Settings.Default.ShowWelcomeDialog)
                {
					using (WelcomeScreen WlcDlg = new WelcomeScreen()) {
						WlcDlg.ShowDialog();
					}
                }

                try
                {
					ProfileManager.GetInstance().LoadSettings();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Language.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UseTrace(e);
                }

				MainForm Frm = new MainForm();
                try
                {
					Frm.Model = model;
                    if (Arg.Minimalize)
                    {
                        Frm.GoToTray();
                        Application.Run();
                    }
                    else
                        Application.Run(Frm);
                }
                catch (Exception e)
                {
					using (ExceptionForm form = new ExceptionForm(e.Message + "\n\n" + e.StackTrace)) {
						form.FormBorderStyle = FormBorderStyle.FixedDialog;
						form.StartPosition = FormStartPosition.CenterScreen;
						form.ShowDialog();
					}
                    UseTrace(e);
                } 
				finally {
					Frm.Dispose();
				}
            }
            FlushDebug();
            
        }

		public static void UseTrace(Exception e)
        {
            
            Trace.Write(e.Message);
            Trace.WriteLine(' ');
            Trace.Write(e.StackTrace);
            Trace.WriteLine(' ');
            Trace.Flush();
        }

      
        private static bool LoadLanguage()
        {
            try
            {

                Language.SetLang(Properties.Settings.Default.ActLanguage);
                Language.LoadWords();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UseTrace(e);
                return false;
            }
            return true;
        }

        private static void SetDebugSettings()
        {
			using (TextWriterTraceListener fileListener = new TextWriterTraceListener("errorlog.txt")) {
				Trace.Listeners.Clear();
				Trace.Listeners.Add(fileListener);
			}
        }

        private static void FlushDebug()
        {
            foreach (TraceListener listener in Trace.Listeners)
            {
                listener.Flush();
                listener.Close();
            }
        }
    }


	internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        internal static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        internal static extern Boolean FreeConsole();
    }

}


