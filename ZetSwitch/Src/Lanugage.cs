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

        public bool LoadLanguage(string LangName, Hashtable Words)
        {
			StreamReader Read = new StreamReader(Application.StartupPath + "\\Data\\Lang\\" + LangName + ".lng");
			Words.Clear();
            string Line;
            string[] Buf = new string[2];
            while ((Line = Read.ReadLine())!=null)
            {
                Buf = Line.Split(';');
                if (Buf == null || Buf[0] == null || Buf[1] == null)
                    continue;
                Words.Add(Buf[0],Buf[1]);
            }

            return true;
        }

        public bool LoadLanguage(StringReader Read, Hashtable Words)
        {
            string Line;
            Words.Clear();
            string[] Buf = new string[2];
            while ((Line = Read.ReadLine()) != null)
            {
                Buf = Line.Split(';');
                if (Buf == null || Buf.Length!=2 || Buf[0] == null || Buf[1] == null)
                    continue;
                Words.Add(Buf[0], Buf[1]);
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
            LanguagesStore Store = new LanguagesStore();
            Store.LoadLanguage(new StringReader(Properties.Resources.DefaultLang), _Default);

            if (_LanguageName.Length != 0)
            {
				try
				{
					Store.LoadLanguage(_LanguageName, _Words);
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
