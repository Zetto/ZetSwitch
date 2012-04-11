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

using System.IO;
using System.Xml;

namespace ZetSwitchData.Configuration
{
	class LoaderFactoryXML : ILoaderFactory {
		private const string IdVersion = "version";
		private const string DefActualVersion = "0.3.0";
		string fileName;

		private static string GetVersion(XmlDocument document)
		{
			XmlElement elmnt = document.DocumentElement;
			return elmnt != null ? elmnt.GetAttribute(IdVersion) : "";
		}

		public void InitString(string init)
		{
			fileName = init;
		}

		public ILoader GetLoader()
		{
			if (fileName == null)
				return new LoaderDefault();
			var version = DefActualVersion;
			var document = new XmlDocument();
			if (File.Exists(fileName))
			{
				document.Load(fileName);
				version = GetVersion(document);
			}
			switch (version)
			{
				case "0.3.0":
					var xmlLoader = new LoaderXmlv03();
					xmlLoader.SetDocument(document,fileName);
					return xmlLoader;
				default:
					return new LoaderDefault();
			}
		}
	}
}
