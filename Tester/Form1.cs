using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Core;
using System.Threading;
using christec.windowsce.forms;
using System.Runtime.InteropServices;

namespace Tester
{
	public partial class Form1 : Form
	{
		ActionsManager am = new ActionsManager();
		//FakeForm _fakeNotifForm = new FakeForm();
		// worker thread
		//Thread m_WorkerThread = null;
		//public NotificationWithSoftKeys _softkeyNotification = null;
		public Form1()
		{
			InitializeComponent();
			//this.Hide();
		}

		private void button1_Click(object sender, EventArgs e)
		{			
			//if (_fakeNotifForm._softkeyNotification == null)
			//{
				//_fakeNotifForm.Show();
				//_fakeNotifForm.Hide();
				//_fakeNotifForm.ShowNot();
				
			//    _fakeNotifForm._softkeyNotification = new NotificationWithSoftKeys();
			//    _fakeNotifForm._softkeyNotification.Icon = Properties.Resources.Notif;
			//    _fakeNotifForm._softkeyNotification.InitialDuration = 20;
			//    _fakeNotifForm._softkeyNotification.LeftSoftKey = new NotificationSoftKey(SoftKeyType.Hide, "Hide");
			//    _fakeNotifForm._softkeyNotification.RightSoftKey = new NotificationSoftKey(SoftKeyType.Dismiss, "Dismiss all");
			//}
			//_fakeNotifForm._softkeyNotification.Caption = "LS";
			//_fakeNotifForm._softkeyNotification.Text = "XPTO";
			//_fakeNotifForm._softkeyNotification.Visible = true;
			//_fakeNotifForm.Refresh();

			am.ShowNotification("<b>AAAAAAAAA</b><br><br>" + DateTime.Now);

			//return;
			//if (m_WorkerThread != null)
			//{
			//    m_WorkerThread.Join();
			//}

			//// create worker thread instance			
			//m_WorkerThread = new Thread(new ThreadStart(this.WorkerThreadFunction));			
			//m_WorkerThread.Start();
		
			Thread.Sleep(6000);
		}

		// Worker thread function.
		// Called indirectly from btnStartThread_Click
		//private void WorkerThreadFunction()
		//{
		//    if (_softkeyNotification == null)
		//    {

		//        _softkeyNotification = new NotificationWithSoftKeys();
		//        _softkeyNotification.Icon = Properties.Resources.Notif;
		//        _softkeyNotification.InitialDuration = 20;
		//        _softkeyNotification.LeftSoftKey = new NotificationSoftKey(SoftKeyType.Hide, "Hide");
		//        _softkeyNotification.RightSoftKey = new NotificationSoftKey(SoftKeyType.Dismiss, "Dismiss all");
		//    }
		//    _softkeyNotification.Caption = "LS";
		//    _softkeyNotification.Text = "XPTO";
		//    _softkeyNotification.Visible = true;
		//}

		private void button2_Click(object sender, EventArgs e)
		{
			am.Shutdown();
			this.Close();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			ShowWindow(this.Handle, SW_MINIMIZED);
		}
		[DllImport("coredll.dll")]
		static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
		const int SW_MINIMIZED = 6;
	}
}