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

		Thread _NotifWorkerThread;
		NotificationFakeForm _notifFakeForm ;
		String _notificationText;

		public ActionsManager()
		{
			new Thread(this.RunFakeForm).Start();
		}

		delegate void FormClose();
		public void Shutdown()
		{
			FormClose closefrm = new FormClose(_notifFakeForm.Close);
			_notifFakeForm.Invoke(closefrm);
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
		
		public void ShowNotification(string text)
		{
			if (_NotifWorkerThread != null)
			{
				_NotifWorkerThread.Join();
			}
			_notificationText = text;
			_NotifWorkerThread = new Thread(new ThreadStart(this.ShowNotificationThread));
			_NotifWorkerThread.Start();
		}

		private void RunFakeForm()
		{
			_notifFakeForm = new NotificationFakeForm();
			Application.Run(_notifFakeForm);			
		}

		delegate void ShowNotific(String text);
		private void ShowNotificationThread()
		{
			while (_notifFakeForm == null) Thread.Sleep(1);
			ShowNotific showNo = new ShowNotific(_notifFakeForm.ShowNotif);
			_notifFakeForm.Invoke(showNo, new Object[] { _notificationText });
		}
	}
}
