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

namespace ZetSwitch
{
    enum ConsoleActions
    {
        UseProfile
    }
    class Arguments
    {
        StringBuilder strErrors;
        public string Errors
        {
            get { return strErrors.ToString(); }
        }
        ArrayList actions;
       public ArrayList Actions
        {
            get { return actions; }
        }
        ArrayList profiles;
        public ArrayList Profiles
        {
            get { return profiles; }
        }
    
        bool consoleMode;
        public bool ConsoleMode
        {
            get { return consoleMode; }
        }
        bool minimalize;
        public bool Minimalize
        {
            get { return minimalize; }
        }
        int count;
        public int Count
        {
            get { return count;}
        }

        public Arguments()
        {
            strErrors = new StringBuilder();
            actions = new ArrayList();
            profiles = new ArrayList();
        }

        public bool Parse(string[] Args)
        {
            int c = Args.Length;

            for (int i = 0; i < c; i++)
            {
                switch (Args[i])
                {
                    case "-autorun":
                        minimalize = true;
                        return true;
                        
                    case "-p":
                        ++i;
                        if (i >= c)
                        {
                            strErrors.Append(Language.GetText("ConsoleNotProfiles"));
                            break;
                        }
                        GetProfilesString(i,Args);
                        actions.Add(ConsoleActions.UseProfile);
                        count++;
                        consoleMode = true;
                        break;
                    default:
                        strErrors.Append(Language.GetText("ConsoleInvalidArgument"));
                        break;
                }
            }
            return (strErrors.Length == 0);
        }

        int GetProfilesString(int start, string[] Args)
        {
            string[] prof = Args[start].Split(';');
            if (prof.Length < 1)
            {
                strErrors.Append(Language.GetText("ConsoleNotProfiles"));
            }
            foreach (string str in prof)
            {
                if (str.Length > 0)
                    profiles.Add(str);
            }
            return 1;
        }
    }
}
