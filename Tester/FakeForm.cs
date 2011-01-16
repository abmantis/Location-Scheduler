using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Core;
using christec.windowsce.forms;

namespace Tester
{
	public partial class FakeForm : Form
	{
		public NotificationWithSoftKeys _softkeyNotification = null;
		//ActionsManager am = new ActionsManager();
		public FakeForm()
		{
			InitializeComponent();

			//am.ShowNotification("<b>teste</b><br><br>Lalalala " + DateTime.Now);
		}

		public void ShowNot()
		{
			if (_softkeyNotification == null)
			{				
				_softkeyNotification = new NotificationWithSoftKeys();
				_softkeyNotification.Icon = Properties.Resources.Notif;
				_softkeyNotification.InitialDuration = 20;
				_softkeyNotification.LeftSoftKey = new NotificationSoftKey(SoftKeyType.Hide, "Hide");
				_softkeyNotification.RightSoftKey = new NotificationSoftKey(SoftKeyType.Dismiss, "Dismiss all");
			}
			_softkeyNotification.Caption = "LS";
			_softkeyNotification.Text = "XPTO";
			_softkeyNotification.Visible = true;
		}
		
	}
}