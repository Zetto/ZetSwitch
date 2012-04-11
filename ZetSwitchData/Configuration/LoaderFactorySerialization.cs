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

namespace ZetSwitchData.Configuration
{
	class LoaderFactorySerialization : ILoaderFactory
	{
		string fileName;
		public void InitString(string init)
		{
			fileName = init;
		}

		public ILoader GetLoader()
		{
			if (fileName == null)
				return new LoaderDefault();

			// todo: resolve version and get propriet loader
			var loader = new LoaderSerializationV03();
			loader.SetFileName(fileName);
			return loader;
		}
	}
}
