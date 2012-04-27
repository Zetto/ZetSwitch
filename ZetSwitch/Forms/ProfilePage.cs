using System;
using System.Windows.Forms;
using ZetSwitchData;

namespace ZetSwitch.Forms {
	public partial class ProfilePage : UserControl, ISettingPanel {
		private Profile actProfile;
		public ProfilePage() {
			InitializeComponent();
		}

		private void ButtonPictureChangeClick(object sender, EventArgs e) {
			if (SelectProfileIcon != null)
				SelectProfileIcon(this, null);
		}

		public void UpdateData() {
			actProfile.Name = TextBoxName.Text;
		}

		public void SetData(Profile profile) {
			actProfile = profile;
			UpdatePanel();
			
		}

		public void ResetLanguage() {
			label6.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Name");
			ButtonPictureChange.Text = ClientServiceLocator.GetService<ILanguage>().GetText("Change");
		}

		public void UpdateIcon() {
			Picture.Image = actProfile.GetIcon();
		}

		private void UpdatePanel() {
			TextBoxName.Text = actProfile.Name;
			Picture.Image = actProfile.GetIcon();
		}

		public event EventHandler SelectProfileIcon;
	}
}
