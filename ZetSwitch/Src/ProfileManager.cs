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
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ZetSwitch.Network;
using System.Runtime.Serialization;
using System.Security;

namespace ZetSwitch
{
	public class ProfileManager
	{
		List<Profile> profiles = new List<Profile>();
		DataModel model;
		static ProfileManager instance = new ProfileManager();


		#region private

		private ProfileManager()
		{
		}

		private void MergeNetworkSettings()
		{
			foreach (Profile profile in profiles)
				profile.MergeNetworkInterfaces(model.GetNetworkInterfaceSettings());
		}

		#endregion

		#region public

		public List<Profile> Profiles
		{
			get { return profiles; }
		}

		public DataModel Model
		{
			set { model = value; }
		}

		
		static public ProfileManager GetInstance()
		{
			return instance;
		}

		public void LoadSettings()
		{
			DataStore data = new DataStore();
			ILoaderFactory factory = data.GetLoaderFactory(LOADERS.XML);
			factory.InitString(".\\Data\\profiles.xml");
			profiles = factory.GetLoader().LoadProfiles();
			MergeNetworkSettings();
		}

		public void SaveSettings()
		{
			foreach (Profile profile in profiles)
				profile.PrepareSave();
			DataStore data = new DataStore();
			ILoaderFactory factory = data.GetLoaderFactory(LOADERS.XML);
			factory.InitString(".\\Data\\profiles.xml");
			factory.GetLoader().SaveProfiles(profiles);
		}

		public bool ApplyProfile(string name)
		{
			Profile profile = GetProfile(name);
			if (profile == null)
			{
				// todo: EXCEPTION
				return false;
			}
			model.ApplyProfile(profile);
			return true;
		}

		public Profile GetProfile(string name)
		{
			return profiles.Find(o => o.Name == name);
		}

		public Profile GetNewProfile()
		{
			Profile profile = new Profile();
			profile.Name = GetNewProfileName();
			profile.Connections = new ProfileNetworkSettingsList(model.GetNetworkInterfaceSettings());
			profile.Browsers = model.GetBrowsers();
			return profile;
		}

		public Profile GetCloneProfile(string name)
		{
			Profile old = null;
			if ((old= profiles.Find(o => o.Name == name)) == null)
				return GetNewProfile();
			return old.cloneProfile();
		}

		public void Add(Profile profile)
		{
			if (profiles.Contains(profile))
			{
				// todo: create exception
				throw new Exception("Profile already exists");
			}
			profiles.Add(profile);
		}

		public void Delete(Profile profile)
		{
			profiles.Remove(profile);
		}

		public void Delete(string name)
		{
			Profile profile = null;
			if ((profile = profiles.Find(o => o.Name == name)) != null)
				profiles.Remove(profile);
		}

		public void Change(string oldName, Profile profile)
		{
			Profile old = profiles.Find(item => item.Name == oldName);
			if (old != null)
			{
				int index = profiles.IndexOf(old);
				profiles.Remove(old);
				profiles.Insert(index, profile);
			}
			else
				profiles.Add(profile);
		}

		public string GetNewProfileName()
		{
			string newNameBase = Language.GetText("Profile");
			string newName = newNameBase;
			int offset = 1;
			while (profiles.Find(o => o.Name == newName) != null)
			{
				newName = newNameBase + " " + offset.ToString();
				offset++;
			}
			return newName;
		}

		#endregion
	}
}
