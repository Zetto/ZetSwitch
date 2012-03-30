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
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;

namespace ZetSwitch
{
    partial class AboutBox : Form, IAboutView
    {
        public AboutBox() {
            InitializeComponent();
		    pictureBox1.Image = Properties.Resources.about;
            ResetLanguage();
        }

        private void ResetLanguage() {
            this.label1.Text = Language.GetText("ZetSwitch") +" "+ Properties.Resources.Version; 
            this.button1.Text = Language.GetText("Ok");
            this.lblEmail.Text = Language.GetText("Email");
            this.lblAuthor.Text = Language.GetText("Author");
            this.Text = Language.GetText("Welcome");
        }

		public void ShowView() {
			ShowDialog(); ;
		}

		private void lbEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			if (EmailClicked != null)
				EmailClicked(this, null);
		}

		public event EventHandler EmailClicked;
    }
}
