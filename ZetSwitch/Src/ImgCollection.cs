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
using System.Text;
using System.Drawing;
using System.IO;
using ZTools;
using System.Windows.Forms;


namespace ZetSwitch
{
	class LoadedImage
	{
		public string Name;
		public Bitmap Image;
	}

	class ImgCollection
	{
		private List<LoadedImage> images;
		private DirectoryPath collectionPath;
		static private ImgCollection instance;



		#region private

		private ImgCollection()
		{
			images = new List<LoadedImage>();
		}

		private int LoadNewName()
		{
			return Properties.Settings.Default.NewItemName;
		}

		private bool SaveNewName(int name)
		{
			// todo: load data in patch change
			Properties.Settings.Default.NewItemName = name;
			return true;
		}

		private bool ValidateBitmap(Bitmap Bit)
		{
			if (Bit.Size.Height != 40 || Bit.Size.Width != 40)
				return false;
			return true;
		}

		private string SaveImage(string name)
		{
			int iName = LoadNewName();
			string DirName = collectionPath.DirectoryName;
			string NewName = iName.ToString();
			NewName += ".bmp";

			while (File.Exists(DirName + NewName))
			{
				iName++;
				NewName = iName.ToString() + ".bmp";
			}

			using (Bitmap picture = new Bitmap(Image.FromFile(name), new Size(40, 40))) {
				picture.Save(DirName + NewName);
				SaveNewName(++iName);
			}
			return NewName;
		}

		private Bitmap LoadImage(string Name)
		{
			Bitmap picture = new Bitmap(Name);
			if (picture == null)
				throw new FileNotFoundException();

			if (!ValidateBitmap(picture))
			{
				picture.Dispose();
				Name = SaveImage(Name);
				picture = new Bitmap(collectionPath.DirectoryName + Name);
			}

			LoadedImage item = new LoadedImage();
			item.Image = picture;
			item.Name = Name;

			images.Add(item);
			return picture;
		}

		private bool IsManagedFile(string name)
		{
			DirectoryPath SelPath = new DirectoryPath(name);
			if (collectionPath.IsSubDirectory(SelPath))
			{
				if (images.Find(item => item.Name == name) != null)
					return true;
				if (!File.Exists(name))
					return false;
				return true;
			}
			return false;
		}

		#endregion

		#region public

		static public ImgCollection Instance
		{
			get 
			{
				if (instance == null)
					instance = new ImgCollection();
				return instance; 
			}
			set { instance = value; }
		}

		public string PathToImages
		{
			set 
			{
				collectionPath = new DirectoryPath(value);
				collectionPath.CreateDirectory();
			}
		}

		public Bitmap GetImage(string name)
		{
			if (name == "default" || name.Length == 0)
				return Properties.Resources._default;
			if (!Path.IsPathRooted(name))
				name = collectionPath.CompletePath + name;
			DirectoryPath SelPath = new DirectoryPath(name);
			if (collectionPath.IsSubDirectory(SelPath))      //image is in our directory
			{
				LoadedImage picture = images.Find(item => item.Name == name);
				return (picture == null) ? LoadImage(name) : picture.Image;
			}
			return Properties.Resources._default;;
		}

		public string InitImage(string name)
		{
			if (name == "default" || name.Length == 0)
				return "default";
			if (IsManagedFile(name))
				return name;
			return SaveImage(name);
		}

		#endregion
	}
}
