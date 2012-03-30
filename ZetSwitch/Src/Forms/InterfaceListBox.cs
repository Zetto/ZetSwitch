using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ZetSwitch {
	public class InterfaceListBox : CheckedListBox {

		Timer timer = new Timer();
		bool loaded = false;
		int dotsCount = 1;

		public InterfaceListBox() {
			timer.Interval = 500;
			timer.Tick += new EventHandler(timer_Tick);
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint , true);
			timer.Start();
		}

		public bool IsLoaded {
			set { 
				loaded = value;
				if (loaded)
					timer.Stop();
				this.SetStyle(ControlStyles.UserPaint, false);
				timer.Dispose();
			}
		}

		private void timer_Tick(object o, EventArgs e) {
			dotsCount++;
			if (dotsCount > 3)
				dotsCount = 1;
			Refresh();
		}

		protected override void OnPaint(PaintEventArgs e) {
			Font titleFont = new Font("Ariel", 10);
			Brush solid = new SolidBrush(Color.Black);
			try {
				string label = Language.GetText("loading");
				Size textSize = TextRenderer.MeasureText(label, titleFont);
				for (int i = 0; i < dotsCount; i++) {
					label += ".";
				}
				Point p = new Point((Width - textSize.Width) / 2, (Height - textSize.Height) / 2);
				e.Graphics.DrawString(label, titleFont, solid, p);
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
