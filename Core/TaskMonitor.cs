using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HelperLib;
using System.Threading;
using StedySoft.SenseSDK;

namespace Core
{
	class TasksMonitorTask
	{
		public Task task = null;
		public int? msToStart = null;
	}

	class TasksMonitor
	{
		MessageQueueManager _MsgQueueMgr = new MessageQueueManager(true);
		AutoResetEvent _resetEvent = new AutoResetEvent(false);
		List<TasksMonitorTask> _taskMonitorList = null;
		bool _initDone = false;
		bool _shutdown = false;
		int _updateInterval = 10 * 1000; 
		Time _lastUpdate = null;

		public TasksMonitor()
		{
			_MsgQueueMgr.Received += new MessageQueueManager.ReceivedEventHandler(MsgQueueMgr_Received);
			if (LoadTasks())
			{				
				_initDone = true;
				Globals.WriteToDebugFile("TasksMonitor init done.");
				_resetEvent.WaitOne();
			}
		}

		public void Shutdown()
		{
			_MsgQueueMgr.Shutdown();
		}

		private void MsgQueueMgr_Received(object sender, ReceivedMessageArgs args)
		{
			switch (args.Message())
			{
				case HelperLib.NotifMessages.NOTIF_STOP:
					_shutdown = true;
					_resetEvent.Set();
					break;
				case HelperLib.NotifMessages.NOTIF_SCAN:
					if (!_initDone) { Globals.WriteToDebugFile("Skiping rescan"); return; }
					LoadTasks();
					_resetEvent.Set();
					break;
			}
		}

		private bool LoadTasks()
		{
			Globals.WriteToDebugFile("Loading Tasks");
			Time now = new Time(DateTime.Now);
			List<Task> taskList = TasksLoader.LoadTasksFromFile();
			foreach (Task task in taskList)
			{
				TasksMonitorTask tmt = new TasksMonitorTask();
				tmt.task = task;
				tmt.msToStart = GetMsToTask(now, task);
				_taskMonitorList.Add(tmt);
			}
			_lastUpdate = now;

			if (_taskMonitorList.Count > 0) return true;
			return false;			
		}

		private void Update()
		{
			while (!_shutdown)
			{
				int msToWait = ProcessTasks();
				_resetEvent.WaitOne(msToWait, false);
			}

			Shutdown();
		}

		private int ProcessTasks()
		{
			int msToNext = 86400000; //24h * 60m * 60s * 1000 <- the max wait time is 24h
			Time now = new Time(DateTime.Now);
			int msFromLastUpd = TimeFuncs.GetMsFromTo(_lastUpdate, now);
			foreach (TasksMonitorTask tmt in _taskMonitorList)
			{
				tmt.msToStart -= msFromLastUpd;

				if (tmt.msToStart <= 0)
				{
					ProcessTask(tmt.task);
					tmt.msToStart = GetMsToTask(now, tmt.task);
				}				

				if (msToNext > tmt.msToStart.Value)
				{
					msToNext = tmt.msToStart.Value;
				}
			}

			if (msToNext <= 0)
			{
				msToNext = _updateInterval;
			}

			_lastUpdate = now;
			return msToNext;
		}

		private int GetMsToTask(Time now, Task task)
		{
			int msToTask;
			bool isWithinTaskTime = false;
			int taskTimeCompare = TimeFuncs.CompareTime(task.MonitorStartTime, task.MonitorEndTime);
			int nowStartTimeCompare = TimeFuncs.CompareTime(now, task.MonitorStartTime);
			int nowEndTimeCompare = TimeFuncs.CompareTime(now, task.MonitorEndTime);

			if (taskTimeCompare < 0) // start is smaller than end
			{
				if (nowStartTimeCompare >= 0 && nowEndTimeCompare <= 0)
				{
					isWithinTaskTime = true;
				}
			}
			else // start is bigger than end (or equal)  - monitor time across two days
			{
				if ((nowStartTimeCompare > 0 && nowEndTimeCompare > 0)
					|| (nowStartTimeCompare < 0 && nowEndTimeCompare < 0))
				{
					isWithinTaskTime = true;
				}
			}

			if (isWithinTaskTime)
			{
				msToTask = 0;
			}
			else
			{
				if (nowStartTimeCompare <= 0)
				{
					// Task will start later today
					msToTask = TimeFuncs.HoursToMilliseconds(task.MonitorStartTime.Hour - now.Hour);
				}
				else
				{
					// Task will only start "tomorrow"
					msToTask = TimeFuncs.HoursToMilliseconds((23 - now.Hour) + task.MonitorStartTime.Hour);
				}
				msToTask += TimeFuncs.MinutesToMilliseconds(task.MonitorStartTime.Minute - now.Minute);
			}

			return msToTask;
		}

		private void ProcessTask(Task t)
		{

		}
	}
}
