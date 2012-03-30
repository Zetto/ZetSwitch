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
	public class LanguageDescription {
		string name = "";
		string shortName = "";

		public string Name {
			get { return name; }
		}

		public string ShortName {
			get { return shortName; }
		}
		
		public LanguageDescription() { }
		public LanguageDescription(string name, string shortName) {
			this.name = name;
			this.shortName = shortName;
		}
	}
	
    class LanguagesStore
    {
		List<LanguageDescription> languages;
		static LanguageDescription defaultLanguage = new LanguageDescription(@"English", @"en");
		bool listLoaded = false;

		public LanguagesStore() {
			languages = new List<LanguageDescription>();
			languages.Add(defaultLanguage);
		}

		public Dictionary<string, string> LoadDefaultLanguage() {
			Dictionary<string, string> words;
			using (StringReader reader = new StringReader(Properties.Resources.DefaultLang)) {
				words = LoadData(reader);
			}
			return words;
		}

		public Dictionary<string, string> LoadLanguage(string name) {
			Dictionary<string, string> words;
			using (StreamReader reader = new StreamReader(Application.StartupPath + "\\Data\\Lang\\" + name + ".lng")) {
				words = LoadData(reader);
			}
			return words;
		}

		private Dictionary<string, string> LoadData(TextReader read) {
			Dictionary<string, string> words = new Dictionary<string, string>();
            string line;
			string[] buf = new string[2];
			while ((line = read.ReadLine()) != null) {
				buf = line.Split(';');
				if (buf == null || buf.Length != 2 || buf[0] == null || buf[1] == null)
					continue;
				words.Add(buf[0], buf[1]);
			}
            return words;
        }

		public List<LanguageDescription> GetAvailableLanguages() {
			if (!listLoaded)
				ReloadLanguageList();
			return languages;
		}

		private void ReloadLanguageList() {
			StreamReader reader = null;
			try {
				reader = new StreamReader(Application.StartupPath + "\\Data\\Lang\\languages.info");
				string line;
				string[] buf = new string[2];
				while ((line = reader.ReadLine()) != null) {
					buf = line.Split(';');
					if (buf == null || buf.Length != 2 || buf[0] == null || buf[1] == null)
						continue;
					if (languages.Find(i=>i.Name==buf[0]) != null)
						continue;
					languages.Add(new LanguageDescription(buf[0], buf[1]));
				}
			}
			catch(Exception e) {
				Program.UseTrace(e);
			}
			finally {
				if (reader != null)
					reader.Dispose();
			}
			listLoaded = true;
		}

		
    }

    static class Language
    {
		static Dictionary<string, string> actualLang;
		static Dictionary<string, string> defaultLang;
       
        static Language() {
			actualLang = new Dictionary<string, string>();
			defaultLang = new Dictionary<string, string>();
        }

        static public string GetText(string ID) {
			string Text = actualLang.ContainsKey(ID) ? actualLang[ID] : null ;
            if (Text == null || Text.Length == 0) 
                return defaultLang.ContainsKey(ID) ? defaultLang[ID] : "";
            return Text;
        }

		static public void LoadDefault(LanguagesStore store) {
			try {
				defaultLang = store.LoadDefaultLanguage();
			} catch (Exception e) {
				Program.UseTrace(e);
			}
		}

        static public bool LoadWords(string name, LanguagesStore store) {
			try {
				actualLang = store.LoadLanguage(name);
			}
			catch (Exception e) {
				Program.UseTrace(e);
			}
            return true;
        }
    }
}
