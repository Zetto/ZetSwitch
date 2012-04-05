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

using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using ZTools;


namespace ZetSwitch {
	internal class LoadedImage {
		public string Name;
		public Bitmap Image;
	}

	internal class ImgCollection {
		private static ImgCollection _instance;
		private readonly List<LoadedImage> images;
		private DirectoryPath collectionPath;

		private ImgCollection() {
			images = new List<LoadedImage>();
		}

		private int LoadNewName() {
			return Properties.Settings.Default.NewItemName;
		}

		private void SaveNewName(int name) {
			// todo: load data in patch change
			Properties.Settings.Default.NewItemName = name;
		}

		private bool ValidateBitmap(Image bit) {
			return bit.Size.Height == 40 && bit.Size.Width == 40;
		}

		private string SaveImage(string name) {
			int iName = LoadNewName();
			string dirName = collectionPath.DirectoryName;
			string newName = iName.ToString(CultureInfo.InvariantCulture);
			newName += ".bmp";

			while (File.Exists(dirName + newName)) {
				iName++;
				newName = iName.ToString(CultureInfo.InvariantCulture) + ".bmp";
			}

			using (var picture = new Bitmap(Image.FromFile(name), new Size(40, 40))) {
				picture.Save(dirName + newName);
				SaveNewName(++iName);
			}
			return newName;
		}

		private Bitmap LoadImage(string name) {
			var picture = new Bitmap(name);
			if (picture == null)
				throw new FileNotFoundException();

			if (!ValidateBitmap(picture)) {
				picture.Dispose();
				name = SaveImage(name);
				picture = new Bitmap(collectionPath.DirectoryName + name);
			}

			var item = new LoadedImage {Image = picture, Name = name};
			images.Add(item);
			return picture;
		}

		private bool IsManagedFile(string name) {
			var selPath = new DirectoryPath(name);
			if (collectionPath.IsSubDirectory(selPath)) {
				if (images.Find(item => item.Name == name) != null)
					return true;
				if (!File.Exists(name))
					return false;
				return true;
			}
			return false;
		}

		public static ImgCollection Instance {
			get { return _instance ?? (_instance = new ImgCollection()); }
			set { _instance = value; }
		}

		public string PathToImages {
			set {
				collectionPath = new DirectoryPath(value);
				collectionPath.CreateDirectory();
			}
		}

		public Bitmap GetImage(string name) {
			if (name == "default" || name.Length == 0)
				return Properties.Resources._default;
			if (!Path.IsPathRooted(name))
				name = collectionPath.CompletePath + name;
			var selPath = new DirectoryPath(name);
			if (collectionPath.IsSubDirectory(selPath)) { //image is in our directory
				LoadedImage picture = images.Find(item => item.Name == name);
				return (picture == null) ? LoadImage(name) : picture.Image;
			}
			return Properties.Resources._default;
		}

		public string InitImage(string name) {
			if (name == "default" || name.Length == 0)
				return "default";
			return IsManagedFile(name) ? name : SaveImage(name);
		}
	}
}
