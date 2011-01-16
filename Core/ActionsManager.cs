using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsMobile.PocketOutlook;
using StedySoft.SenseSDK.DrawingCE;
using StedySoft.SenseSDK;
using Microsoft.WindowsCE.Forms;
using christec.windowsce.forms;
using System.Threading;
using System.Windows.Forms;

namespace Core
{
	public class ActionsManager
	{
		private NotificationWithSoftKeys _softkeyNotification;
		Thread _NotifWorkerThread;
		private int _notifIndex = 1;
		private List<string> _notifTexts = new List<string>();
		object _notifLocker = new object();
		NotificationFakeForm nff ;
		private Microsoft.WindowsCE.Forms.Notification notification1;

		public ActionsManager()
		{
			new Thread(this.RunFakeForm).Start();
		}

		delegate void FormClose();
		public void Shutdown()
		{
			FormClose closefrm = new FormClose(nff.Close);
			nff.Invoke(closefrm);
		}

		public void ProcessTask(HelperLib.Task task)
		{
			switch (task.ActionType)
			{
				case HelperLib.Task.ActionTypes.SMS:
					SendSMS(task.SmsRecipient, task.SmsBody, task.Subject);
					break;
				case HelperLib.Task.ActionTypes.NOTIFICATION:
					ShowNotification("<b>" + task.Subject + "</b><br><br>" + task.Notes);
					break;
				case HelperLib.Task.ActionTypes.APP:
					break;
				default:
					break;
			}
		}

		static public void SendSMS(string recipient, string text, string taskSubject)
		{
			try
			{
				if (recipient.Length != 0 && text.Length != 0)
				{
					SmsMessage msg = new SmsMessage();
					Recipient receptor = new Recipient(recipient);
					msg.To.Add(receptor);
					msg.Body = text;
					msg.Send();
				}
			}
			catch (InvalidSmsRecipientException)
			{
				SenseAPIs.SenseMessageBox.Show(
					"Task " + taskSubject + ": Cannot send SMS due to invalid recipient",
					"Location Scheduler", SenseMessageBoxButtons.OK);
			}
			catch (Exception)
			{
				SenseAPIs.SenseMessageBox.Show(
					"Task " + taskSubject + ": Error while trying to send SMS",
					"Location Scheduler", SenseMessageBoxButtons.OK);
			}
		}

		delegate void ShowNotific();
		public void ShowNotification(string text)
		{
			_notifTexts.Add(text);
			_notifIndex = 1;

			//nff.Show();
			//nff.Hide();

			if (_NotifWorkerThread != null)
			{
				_NotifWorkerThread.Join();
			}
			_NotifWorkerThread = new Thread(new ThreadStart(this.ShowNotificationThread));
			_NotifWorkerThread.Start();
		}

		private void RunFakeForm()
		{
			nff = new NotificationFakeForm();
			Application.Run(nff);
			return;

			//notification1 = new Microsoft.WindowsCE.Forms.Notification();
			//notification1.Icon = Resources.Notif;

			//notification1.Caption = "Notification scenario - data download";

			//// If notification is urgent, set to true.
			//notification1.Critical = false;

			//// Create the text for the notification.
			//// Use a StringBuilder for better performance.
			//StringBuilder HTMLString = new StringBuilder();

			//HTMLString.Append("<html><body>");
			//HTMLString.Append("<font color=\"#0000FF\"><b>Data ready to download</b></font>");
			//HTMLString.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"settings\">Settings</a>");
			//HTMLString.Append("<br><form method=\"GET\" action=notify>");
			//HTMLString.Append("<SELECT NAME=\"lstbx\">");
			//HTMLString.Append("<OPTION VALUE=\"0\">Start now</OPTION><OPTION VALUE=\"1\">In 1 hr</OPTION>");
			//HTMLString.Append("<OPTION VALUE=\"2\">In 2 hrs</OPTION><OPTION VALUE=\"3\">In 3 hrs</OPTION>");
			//HTMLString.Append("<OPTION VALUE=\"4\">In 4 hrs</OPTION></SELECT>");
			//HTMLString.Append("<input type=checkbox name=chkbx>Notify completion");
			//HTMLString.Append("<br><input type='submit'>");
			//HTMLString.Append("<input type=button name='cmd:2' value='Postpone'>");
			//HTMLString.Append("</body></html>");

			//// Set the Text property to the HTML string.
			//notification1.Text = HTMLString.ToString();

			//notification1.InitialDuration = 20;
			//notification1.Critical = true;
			//notification1.Visible = true;


			//return;



			//lock (_notifLocker)
			//{
			//    if (_softkeyNotification == null)
			//    {
			//        _softkeyNotification = new NotificationWithSoftKeys();
			//        _softkeyNotification.Icon = Resources.Notif;
			//        _softkeyNotification.InitialDuration = 20;
			//        _softkeyNotification.Spinners = true;
			//        _softkeyNotification.SpinnerClick += new SpinnerClickEventHandler(softkeyNotification_SpinnerClick);
			//        _softkeyNotification.LeftSoftKey = new NotificationSoftKey(SoftKeyType.Hide, "Hide");
			//        _softkeyNotification.RightSoftKey = new NotificationSoftKey(SoftKeyType.Dismiss, "Dismiss all");
			//        _softkeyNotification.RightSoftKeyClick += new EventHandler(softkeyNotificationCustom2_RightSoftKeyClick);
			//    }

			//    _softkeyNotification.Caption = String.Format("Location Scheduler\t{0} of {1}", _notifIndex, _notifTexts.Count);
			//    _softkeyNotification.Text = "XXX";
			//    _softkeyNotification.Visible = true;	
			//}
			
		}

		private void ShowNotificationThread()
		{
			while (nff == null) Thread.Sleep(1);
			ShowNotific showNo = new ShowNotific(nff.ShowNotif);
			nff.Invoke(showNo);
		}

		private void softkeyNotification_SpinnerClick(object sender, SpinnerClickEventArgs e)
		{
			lock (_notifLocker)
			{
				int notifCount = _notifTexts.Count;
				if (e.Forward)
					_notifIndex = Math.Min(notifCount, ++_notifIndex);
				else
					_notifIndex = Math.Max(1, --_notifIndex);

				_softkeyNotification.Caption = String.Format("Location Scheduler\t{0} of {1}", _notifIndex, notifCount);
				_softkeyNotification.Text = _notifTexts[notifCount - _notifIndex];
			}
		}

		private void softkeyNotificationCustom2_RightSoftKeyClick(object sender, EventArgs e)
		{
			lock (_notifLocker)
			{
				_notifIndex = 1;
				_notifTexts.Clear();
				_softkeyNotification = null;
			}
		}
	}
}
