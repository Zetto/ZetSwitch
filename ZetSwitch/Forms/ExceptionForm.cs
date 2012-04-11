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
using System.Windows.Forms;
using ZetSwitchData;

namespace ZetSwitch.Forms {
	public partial class ExceptionForm : Form {
		public ExceptionForm(string trace) {
			InitializeComponent();
			textTrace.Text = trace;
		}

		private void OkClick(object sender, EventArgs e) {
			Close();
		}

		private void MailLinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			try {
				System.Diagnostics.Process.Start("mailto:tomas.skarecky@gmail.com");
			}
			catch (System.ComponentModel.Win32Exception noBrowser) {
				if (noBrowser.ErrorCode == -2147467259)
					MessageBox.Show(noBrowser.Message);
			}
		}

		private void ResetLanguage() {
			lblMessage.Text = ClientServiceLocator.GetService<ILanguage>().GetText("ExecptionError");
			lblAttach.Text = ClientServiceLocator.GetService<ILanguage>().GetText("ExceptionAttach");
			Text = ClientServiceLocator.GetService<ILanguage>().GetText("Welcome");
		}
	}
}
