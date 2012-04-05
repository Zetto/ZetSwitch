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
		Ie,
		Firefox
	}

	[Serializable]
	public abstract class Browser {

		public ProxySettings Proxy { set; get; }
		public string HomePage { set; get; }
		public bool IsDetected { get; set; }

		protected Browser() {
			Proxy = new ProxySettings();
			IsDetected = false;
		}

		protected abstract bool LoadProxySettings();
		protected abstract bool LoadHomePage();
		protected abstract bool SaveProxySettings();
		protected abstract bool SaveHomePage();
		protected abstract bool Find();

	
		public virtual bool LoadData()
		{
			try
			{
				IsDetected = Find();
				if (!IsDetected)
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
			if (!IsDetected)
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
	}
}
