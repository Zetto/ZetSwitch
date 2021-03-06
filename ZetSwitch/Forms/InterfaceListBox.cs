﻿using System;
using System.Windows.Forms;
using System.Drawing;
using ZetSwitchData;

namespace ZetSwitch {
	public class InterfaceListBox : CheckedListBox {
		readonly Timer timer = new Timer();
		bool loaded;
		int dotsCount = 1;
		private string label = "loading";

		public InterfaceListBox() {
			timer.Interval = 500;
			timer.Tick += TimerTick;
			SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint , true);
			timer.Start();
			//label = ClientServiceLocator.GetService<ILanguage>().GetText("loading");

		}

		public bool IsLoaded {
			set { 
				loaded = value;
				if (loaded)
					timer.Stop();
				SetStyle(ControlStyles.UserPaint, false);
				timer.Dispose();
			}
		}

		private void TimerTick(object o, EventArgs e) {
			dotsCount++;
			if (dotsCount > 3)
				dotsCount = 1;
			Refresh();
		}

		protected override void OnPaint(PaintEventArgs e) {
			var titleFont = new Font("Ariel", 10);
			Brush solid = new SolidBrush(Color.Black);
			try {
				var textSize = TextRenderer.MeasureText(label, titleFont);
				var tmp = label;
				for (int i = 0; i < dotsCount; i++) {
					tmp += ".";
				}
				var p = new Point((Width - textSize.Width) / 2, (Height - textSize.Height) / 2);
				e.Graphics.DrawString(tmp, titleFont, solid, p);
			} finally {
				titleFont.Dispose();
				solid.Dispose();
			}
		}

		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			if (disposing)
				timer.Dispose();
		}
	}
}
