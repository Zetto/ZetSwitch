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
using System.Text;
using System.IO;

namespace ZetSwitchData.Tools {
	public class DirectoryPath {
		private string[] directories;
		private string disk;


		public DirectoryPath(string filePath) {
			Parse(filePath);
		}

		private bool Parse(string filePath) {
			if (filePath == null)
				return false;
			string[] buff = filePath.Split('\\');

			int firstIndex = 0;
			int lastIndex = buff.Length;

			if (buff.Length < 1)
				throw new Exception();

			if (filePath[filePath.Length - 1] == '\\') {
				FileName = null;
				lastIndex--;
			}
			else {
				FileName = buff[buff.Length - 1];
				lastIndex--;
			}

			if (filePath.Length > 2 && filePath[1] == ':') {
				disk = buff[0];
				firstIndex++;
			}
			directories = new string[lastIndex - firstIndex];

			for (int i = firstIndex; i < lastIndex; i++)
				directories[i - firstIndex] = buff[i];


			return true;
		}

		public bool SetDirectory(string path) {
			return Parse(path);
		}

		public string DirectoryName {
			get {
				var str = new StringBuilder();
				str.Append(disk + '\\');
				foreach (string t in directories) {
					str.Append(t);
					str.Append('\\');
				}
				return str.ToString();
			}
		}

		public string[] DirectoryArray {
			get { return directories; }
		}

		public string FileName { get; private set; }

		public string CompletePath {
			get { return DirectoryName + FileName; }
		}

		public bool IsSubDirectory(DirectoryPath comparePath) {
			int len = directories.Length;
			if (comparePath.DirectoryArray.Length < directories.Length)
				return false;

			if (!comparePath.disk.Equals(disk))
				return false;

			for (int i = 0; i < len; i++) {
				if (!comparePath.DirectoryArray[i].Equals(directories[i]))
					return false;
			}
			return true;
		}

		public void CreateDirectory() {
			if (!DirectoryExists())
				Directory.CreateDirectory(DirectoryName);
		}

		public bool DirectoryExists() {
			return Directory.Exists(DirectoryName);
		}

		public string[] GetFiles() {
			return Directory.GetFiles(DirectoryName);
		}

		public string ReducePath(string name) {
			int i = name.IndexOf(DirectoryName, System.StringComparison.Ordinal);
			return i == 0 ? name.Substring(DirectoryName.Length) : null;
		}
	}
}