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
using System.Collections;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ZetSwitch
{
    class LanguagesStore
    {

        public bool LoadLanguage(string LangName, Hashtable words)
        {
			using (StreamReader Read = new StreamReader(Application.StartupPath + "\\Data\\Lang\\" + LangName + ".lng")) {
				words.Clear();
				string line;
				string[] buf = new string[2];
				while ((line = Read.ReadLine()) != null) {
					buf = line.Split(';');
					if (buf == null || buf[0] == null || buf[1] == null)
						continue;
					words.Add(buf[0], buf[1]);
				}
			}

            return true;
        }

        public bool LoadLanguage(StringReader read, Hashtable words)
        {
            string line;
            words.Clear();
            string[] buf = new string[2];
            while ((line = read.ReadLine()) != null)
            {
                buf = line.Split(';');
                if (buf == null || buf.Length!=2 || buf[0] == null || buf[1] == null)
                    continue;
                words.Add(buf[0], buf[1]);
            }

            return true;
        }
    }

    static class Language
    {
        static string _LanguageName;
        static Hashtable _Words;
        static Hashtable _Default;
       
        static Language()
        {
            _Words = new Hashtable();
            _Default = new Hashtable();
        }

        static public string GetText(string ID)
        {
            string Text = (string)_Words[ID];
            if (Text == null || Text.Length == 0)
            {
                Text = (string) _Default[ID];
                if (Text == null)
                    return "";
                return Text;
            }

            return Text;
        }

        static public void SetLang(string Name)
        {
            _LanguageName = Name;
        }

        static public bool LoadWords()
        {
            LanguagesStore store = new LanguagesStore();
			using (StringReader reader = new StringReader(Properties.Resources.DefaultLang)) {
				store.LoadLanguage(reader, _Default);
			}

            if (_LanguageName.Length != 0)
            {
				try
				{
					store.LoadLanguage(_LanguageName, _Words);
				}
				catch (Exception e)
				{
					Program.UseTrace(e);
				}
            }
            return false;
        }
    }
}
