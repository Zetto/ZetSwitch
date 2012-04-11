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
using ZetSwitchData;

namespace ZetSwitch {
	public class WelcomeController {
		readonly IViewFactory factory;
		public WelcomeController(IViewFactory factory) {
			this.factory = factory;
		}

		public void TryShow() {
			var config = ClientServiceLocator.GetService<IUserConfiguration>();
			var state = config.LoadConfiguration();
			if (!state.ShowWelcome)
				return;
			using (var view = factory.CreateWelcomeView()) {
				view.LanguageChanged += new EventHandler( (o,e) => {
				                                          	config.SaveConfigurate(state);
				                                          	if (view != null) view.ResetLanguage();
				                                          });

				view.SetState(state);
				view.ShowView();
				config.SaveConfigurate(state);
			}
		}
	}
}
