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

namespace Core
{
	public class ActionsManager
	{
		private NotificationWithSoftKeys _softkeyNotification;
		Thread _NotifWorkerThread;
		private int _notifIndex = 1;
		private List<string> _notifTexts = new List<string>();
		object _notifLocker = new object();

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
				
		public void ShowNotification(string text)
		{
			_notifTexts.Add(text);
			_notifIndex = 1;

			if (_NotifWorkerThread != null)
			{
				_NotifWorkerThread.Join();
			}
			_NotifWorkerThread = new Thread(new ThreadStart(this.NotifWorkerThreadFunction));
			_NotifWorkerThread.Start();						
			
		}

		private void NotifWorkerThreadFunction()
		{
			lock (_notifLocker)
			{
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
				_softkeyNotification.Text = "XXX";
				_softkeyNotification.Visible = true;	
			}
			
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
