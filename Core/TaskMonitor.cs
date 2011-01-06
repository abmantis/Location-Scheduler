using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HelperLib;
using System.Threading;

namespace Core
{
	class TaskMonitor
	{
		MessageQueueManager _MsgQueueMgr = new MessageQueueManager(true);
		ManualResetEvent _manualEvent = new ManualResetEvent(false);

		public TaskMonitor()
		{
			_MsgQueueMgr.Received += new MessageQueueManager.ReceivedEventHandler(MsgQueueMgr_Received);
			_manualEvent.WaitOne();//(-1, false);
			Console.WriteLine("after wait");
		}

		public void Shutdown()
		{
			_MsgQueueMgr.Shutdown();
		}

		private void MsgQueueMgr_Received(object sender, ReceivedMessageArgs args)
		{
			//Console.WriteLine("BLA: " + args.Message());
			switch (args.Message())
			{
				case HelperLib.NotifMessages.NOTIF_STOP:
					this.Shutdown();
					_manualEvent.Set();
					break;
				case HelperLib.NotifMessages.NOTIF_SCAN:
					Globals.WriteToDebugFile("Rescan");
					_manualEvent.Set();
					break;
				
			}
			
		}
	}
}
