using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using christec.windowsce.forms;

namespace Core
{
	public partial class NotificationFakeForm : Form
	{
		public NotificationWithSoftKeys _softkeyNotification = null;
		public NotificationFakeForm()
		{
			InitializeComponent();
			

		}

	
		private void NotificationFakeForm_Load(object sender, EventArgs e)
		{
			
			//_softkeyNotification.Visible = false;
			this.Visible = false;
			//ShowNotif();
		}

		public void ShowNotif()
		{
			this.Visible = false;
			if (_softkeyNotification == null)
			{
				_softkeyNotification = new NotificationWithSoftKeys();
				_softkeyNotification.Icon = Resources.Notif;
				_softkeyNotification.InitialDuration = 20;
				_softkeyNotification.Spinners = true;
				_softkeyNotification.SpinnerClick += new SpinnerClickEventHandler(softkeyNotification_SpinnerClick);
				_softkeyNotification.LeftSoftKey = new NotificationSoftKey(SoftKeyType.Hide, "Hide");
				_softkeyNotification.RightSoftKey = new NotificationSoftKey(SoftKeyType.Dismiss, "Dismiss all");
				_softkeyNotification.RightSoftKeyClick += new EventHandler(softkeyNotificationCustom2_RightSoftKeyClick);
			}

			_softkeyNotification.Caption = String.Format("Location Scheduler\t{0} of {1}", 0, 2);
			_softkeyNotification.Text = "XXX";
			_softkeyNotification.Visible = true;
		}

		private void NotificationFakeForm_Activated(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		private void softkeyNotification_SpinnerClick(object sender, SpinnerClickEventArgs e)
		{
			MessageBox.Show("LALALA");
			//lock (_notifLocker)
			//{
			//    int notifCount = _notifTexts.Count;
			//    if (e.Forward)
			//        _notifIndex = Math.Min(notifCount, ++_notifIndex);
			//    else
			//        _notifIndex = Math.Max(1, --_notifIndex);

			//    _softkeyNotification.Caption = String.Format("Location Scheduler\t{0} of {1}", _notifIndex, notifCount);
			//    _softkeyNotification.Text = _notifTexts[notifCount - _notifIndex];
			//}
		}

		private void softkeyNotificationCustom2_RightSoftKeyClick(object sender, EventArgs e)
		{
			MessageBox.Show("XXX");
			//_softkeyNotification = null;
		}
	}
}