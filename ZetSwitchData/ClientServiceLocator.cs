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

namespace ZetSwitchData {
	public class ClientServiceLocator {
		private static readonly Dictionary<Type, object> Services = new Dictionary<Type,object>();

		static private object GetService(Type type) {
			if (!Services.ContainsKey(type))
				throw new ApplicationException(string.Format("{0} is not set",type.ToString()));
			return Services[type];
		}

		static public bool Register<T>(T service) where T : class {
			if (service == null)
				throw new ArgumentNullException("service");

			if (Services.ContainsKey(typeof(T))) {
				Services.Remove(typeof(T));
//				return false;
			}
			Services.Add(typeof(T),service);
			return true;
		}

		static public T GetService<T>() {
			return (T)GetService(typeof(T));
		}
	}
}
