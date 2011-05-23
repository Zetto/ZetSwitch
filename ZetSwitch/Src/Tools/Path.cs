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
using System.IO;

namespace ZTools
{
    class DirectoryPath
    {
        string[] _Directories;
        string _File;
        string _Disk;

       
        public DirectoryPath(string FilePath)
        {
            Parse(FilePath);
        }

        private bool Parse(string FilePath)
        {
            if (FilePath == null)
                return false;
            string[] Buff = FilePath.Split('\\');
            
            int FirstIndex = 0;
            int LastIndex = Buff.Length;

            if (Buff.Length < 1)
                throw new Exception();

            if (FilePath[FilePath.Length - 1] == '\\')
            {
                _File = null;
                LastIndex--;
            }
            else
            {
                _File = Buff[Buff.Length - 1];
                LastIndex--;                
            }

            if (FilePath.Length > 2 && FilePath[1] == ':')
            {
                _Disk = Buff[0];
                FirstIndex++;
            }
            _Directories = new string[LastIndex - FirstIndex];
            
            for (int i = FirstIndex; i < LastIndex; i++)
                _Directories[i - FirstIndex] = Buff[i];
            

            return true;
        }

        public bool SetDirectory(string Path)
        {
            return Parse(Path);
        }

        public string DirectoryName
        {
            get 
            { 
                StringBuilder str = new StringBuilder();
                str.Append(_Disk + '\\');
                for (int i = 0; i < _Directories.Length;i++)
                {
                    str.Append( _Directories[i]);
                    str.Append('\\');
                }
                return str.ToString();
            }
        }

        public string[] DirectoryArray
        {
            get { return _Directories; } 
        }

        public string FileName
        {
            get { return _File; } 
        }

        public string CompletePath
        {
            get { return DirectoryName + FileName; }
        }

        public bool IsSubDirectory(DirectoryPath ComparePath)
        {
            int Len = _Directories.Length;
            if (ComparePath.DirectoryArray.Length < _Directories.Length)
                return false;

            if (!ComparePath._Disk.Equals(_Disk))
                return false;

            for (int i = 0; i < Len; i++)
            {
                if (!ComparePath.DirectoryArray[i].Equals(_Directories[i]))
                    return false;
            }
            return true;
        }

        public void CreateDirectory()
        {
            if (!DirectoryExists())
                Directory.CreateDirectory(DirectoryName);
        }

        public bool DirectoryExists()
        {
           return Directory.Exists(DirectoryName);
        }

        public string[] GetFiles()
        {
            return Directory.GetFiles(DirectoryName);
        }

        public string ReducePath(string Name)
        {
            int i = Name.IndexOf(DirectoryName);
            if (i==0)
                return Name.Substring(DirectoryName.Length);
            return null;
        }
    }
}