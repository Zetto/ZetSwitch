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
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ZetSwitch
{
	class LoaderSerializationV0_3 : ILoader
	{
		string fileName;

		public List<Profile> LoadProfiles() {
			List<Profile> profiles = new List<Profile>();
			if (fileName == null)
				return profiles;

			using (Stream stream = new FileStream(fileName, FileMode.Open)) {
				if (stream.Length == 0)
					return profiles;
				BinaryFormatter form = new BinaryFormatter();
				profiles = (List<Profile>)form.Deserialize(stream);
			}
			return profiles;
		}

		public bool SaveProfiles(List<Profile> list) {
			if (fileName == null)
				return false;
			if (!File.Exists(fileName)) {
				using (File.Create(fileName)) {}
			}

			using (Stream stream = new FileStream(fileName, FileMode.Open)) {
				BinaryFormatter form = new BinaryFormatter();
				form.Serialize(stream, list);
			}
			return true;
		}

		public void SetFileName(string name) {
			fileName = name;
		}
	}
}
