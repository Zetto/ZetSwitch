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

namespace ZetSwitch.Browsers
{
	public enum BROWSERS
	{
		IE,
		FIREFOX
	}

	[Serializable]
	public abstract class Browser
	{
		#region variables

		protected ProxySettings proxy = new ProxySettings();
		protected string homePage = "";
		protected bool detected = false;

		#endregion

		#region getset

		public ProxySettings Proxy 
		{
			set { proxy = value; }
			get { return proxy; }
		}

		public string HomePage
		{
			set { homePage = value; }
			get { return homePage; }
		}

		public bool isDetected
		{
			get { return detected; }
		}

		public Browser()
		{
			detected = false;
		}

		#endregion

		#region IF

		protected abstract bool LoadProxySettings();
		protected abstract bool LoadHomePage();
		protected abstract bool SaveProxySettings();
		protected abstract bool SaveHomePage();
		protected abstract bool Find();

		#endregion

		#region public methods

		public virtual bool LoadData()
		{
			try
			{
				detected = Find();
				if (!detected)
					return false;
				LoadProxySettings();
				LoadHomePage();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public virtual bool SaveData()
		{
			if (!isDetected)
				return false;
			try
			{
				SaveHomePage();
				SaveProxySettings();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		#endregion
	}
}
