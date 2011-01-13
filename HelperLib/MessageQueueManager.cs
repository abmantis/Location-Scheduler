using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using J2i.Net.NamedObjects;
using System.Threading;

namespace HelperLib
{
	// custom attributes 
	public class ReceivedMessageArgs : System.EventArgs
	{
		private string message;

		public ReceivedMessageArgs(string m)
		{
			this.message = m;
		}

		public string Message()
		{
			return message;
		}
	} 

	public class MessageQueueManager
	{
		public delegate void ReceivedEventHandler(object sender, ReceivedMessageArgs args);
		public event ReceivedEventHandler Received;

		const string QueueEndpointName = "LocationScheduler.Scheduler2Core.Messages";

		const int maxItems = 8;
		const int maxMessageSize = 1024;
		bool _shuttingDown = false;
		bool _isReader = true;

		MessageQueueWriter _writer = null;
		MessageQueueReader _reader = null;
		SystemEvent _readerWaitEvent;

		public MessageQueueManager(bool isReader)
		{
			_isReader = isReader;
//			InitQueueEndPoints();
			
		}

		/// <summary>
		/// Create the message queues and system event. 
		/// </summary>
		public void InitQueueEndPoints()
		{
			//Create out reader and writer queues.  Since this queue has a name it is accessible
			// by other programs. 
			_writer = new MessageQueueWriter(QueueEndpointName, maxItems, maxMessageSize);

			if (_isReader)
			{
				_reader = new MessageQueueReader(QueueEndpointName, maxItems, maxMessageSize);

				//Create an unnamed event.  This will be used to unblock the read thread
				//when the program is terminating. 
				_readerWaitEvent = new SystemEvent(null, false, true);

				Thread readThread = new Thread(new ThreadStart(ReaderThread));
				readThread.IsBackground = true;
				readThread.Start();
			}			
		}

		public void Shutdown()
		{
			_shuttingDown = true;
			if(_isReader) _readerWaitEvent.SetEvent();
			_writer.Dispose();
		}

		public void Write(String message)
		{
			_writer.Write(message);
		}

		/// <summary>
		/// Waits on messages to be placed on the queue and displays them as they arrive
		/// </summary>
		void ReaderThread()
		{
			using (_reader)
			{
				while (!_shuttingDown)
				{
					//The following call will block this thread until there is either a message
					//on the queue to read or the thread is being signaled to run to prepare
					//for program termination. Since the following call blocks ths thread until
					//it is time to do work it is not subject to the same batter killing
					//affect of other similar looking code patterns ( http://tinyurl.com/6rxoc6 ).
					if (SyncBase.WaitForMultipleObjects(_readerWaitEvent, _reader) == _reader)
					{
						string msg;
						_reader.Read(out msg);  //Get the next message
						this.Received(this, new ReceivedMessageArgs(msg));
					}
				}
			}
		}		
	}
}
