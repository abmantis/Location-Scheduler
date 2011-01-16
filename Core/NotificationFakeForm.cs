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
		private int _notifIndex = 1;
		private List<string> _notifTexts = new List<string>();
		object _notifLocker = new object();
		public NotificationWithSoftKeys _softkeyNotification = null;

		public NotificationFakeForm()
		{
			InitializeComponent();
		}
			
		private void NotificationFakeForm_Load(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		public void ShowNotif(String text)
		{
			_notifTexts.Add(text);
			_notifIndex = 1;

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

			_softkeyNotification.Caption = String.Format("Location Scheduler\t{0} of {1}", _notifIndex, _notifTexts.Count);
			_softkeyNotification.Text = text;
			_softkeyNotification.Visible = true;	
		}

		private void NotificationFakeForm_Activated(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		private void softkeyNotification_SpinnerClick(object sender, SpinnerClickEventArgs e)
		{
			int notifCount = _notifTexts.Count;
			if (e.Forward)
				_notifIndex = Math.Min(notifCount, ++_notifIndex);
			else
				_notifIndex = Math.Max(1, --_notifIndex);

			_softkeyNotification.Caption = String.Format("Location Scheduler\t{0} of {1}", _notifIndex, notifCount);
			_softkeyNotification.Text = _notifTexts[notifCount - _notifIndex];
		}

		private void softkeyNotificationCustom2_RightSoftKeyClick(object sender, EventArgs e)
		{
			_notifIndex = 1;
			_notifTexts.Clear();
			_softkeyNotification = null;
		}
	}
}