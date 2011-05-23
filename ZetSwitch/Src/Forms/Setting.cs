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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Win32;

namespace ZetSwitch
{
    public partial class Setting : Form
    {
        string OldLang;
        public Setting()
        {
            InitializeComponent();
            OldLang = Properties.Settings.Default.ActLanguage;
            checkBoxRunAuto.Checked = LoadAutorun();
            switch (Properties.Settings.Default.ActLanguage)
            {
                case "cz":
                    comboBoxLang.Text = "Česky";
                    break;
                case "en":
                default:
                    comboBoxLang.Text = "English";
                    break;
            }
        }

        private void comboBoxLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxLang.Text)
            {
                case "Česky":
                    Properties.Settings.Default.ActLanguage = "cz";
                    break;
                case "English":
                default:
                    Properties.Settings.Default.ActLanguage = "en";
                    break;
            }
            ResetLanguage();
        }

        private void ResetLanguage()
        {
            Language.SetLang(Properties.Settings.Default.ActLanguage);
            Language.LoadWords();

            this.Text = Language.GetText("Settings");
            this.checkBoxRunAuto.Text = Language.GetText("RunAuto");
            this.label6.Text = Language.GetText("Language");
            this.buttonOk.Text = Language.GetText("Ok");
            this.buttonCancel.Text = Language.GetText("Cancel");

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            switch (comboBoxLang.Text)
            {
                case "Česky":
                    Properties.Settings.Default.ActLanguage = "cz";
                    break;
                case "English":
                default:
                    Properties.Settings.Default.ActLanguage = "en";
                    break;
            }
            
            if (OldLang != Properties.Settings.Default.ActLanguage)
            {
                Language.SetLang(Properties.Settings.Default.ActLanguage);
                Language.LoadWords();
            }
            SaveAutoRun(checkBoxRunAuto.Checked);            
        }

        private void SaveAutoRun(bool Run)
        {
            RegistryKey Key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
            if (Key != null)
            {
                if (Run)
                {
                    Key.SetValue("Zet Switch","\""+Application.ExecutablePath+"\" -autorun",RegistryValueKind.String); 
                }
                else if (Key.GetValue("Zet Switch")!=null)
                {
                    Key.DeleteValue("Zet Switch");
                }
                
            }
            Key.Close();
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool LoadAutorun()
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
            if (Key != null)
            {
                if (Key.GetValue("Zet Switch") != null)
                {
                    Key.Close();
                    return true;
                }
                else
                {
                    Key.Close();
                    return false;
                }

            }
            else
                return false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (OldLang != Properties.Settings.Default.ActLanguage)
            {
                Properties.Settings.Default.ActLanguage = OldLang;
                Language.SetLang(OldLang);
                Language.LoadWords();
            }
        }
    }
}
