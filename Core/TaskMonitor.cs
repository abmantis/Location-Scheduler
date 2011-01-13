using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HelperLib;
using System.Threading;
using StedySoft.SenseSDK;
using Position_Lib;

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
		List<TasksMonitorTask> _taskMonitorList = new List<TasksMonitorTask>();
		bool _initDone = false;
		bool _shutdown = false;
		int _updateRate = 2000; 
		Time _lastUpdate = null;
		int _errorCount = 0;
		PositionTools _positionTools = new PositionTools();

		public TasksMonitor()
		{
			_MsgQueueMgr.Received += new MessageQueueManager.ReceivedEventHandler(MsgQueueMgr_Received);
			_MsgQueueMgr.InitQueueEndPoints();

			int msToNext = LoadTasks();
			if (_taskMonitorList.Count > 0)
			{				
				_initDone = true;
				Globals.WriteToDebugFile("TaskMonitor: TasksMonitor init done.");
				SleepingPrincess(msToNext);
				StartUpdateCycle();
			}
			Shutdown();
		}

		private void Shutdown()
		{
			Globals.WriteToDebugFile("TaskMonitor: Shutdown");
			_MsgQueueMgr.Shutdown();
			_positionTools.Shutdown();
		}

		private void MsgQueueMgr_Received(object sender, ReceivedMessageArgs args)
		{
			switch (args.Message())
			{
				case HelperLib.NotifMessages.NOTIF_STOP:
					Globals.WriteToDebugFile("TaskMonitor: Received shutdown notification");
					_shutdown = true;
					_resetEvent.Set();
					break;
				case HelperLib.NotifMessages.NOTIF_SCAN:
					Globals.WriteToDebugFile("TaskMonitor: Received rescan notification");
					if (!_initDone) { Globals.WriteToDebugFile("TaskMonitor: Skiping rescan"); return; }
					LoadTasks();
					_resetEvent.Set();
					break;
			}
		}

		private int LoadTasks()
		{
			lock (_taskMonitorList)
			{
				Globals.WriteToDebugFile("TaskMonitor: Loading Tasks");
				int msToNext = 86400000; //24h * 60m * 60s * 1000 <- the max wait time is 24h
				Time now = new Time(DateTime.Now);
				
				LSTasksConfig tasksCfg = TasksLoader.LoadTasksFromFile();
				_updateRate = tasksCfg.UpdateRate * 1000;
				_positionTools.UseCellLocation = tasksCfg.UseCellLocation;
				List<Task> taskList = tasksCfg.TaskList;
				
				foreach (Task task in taskList)
				{
					TasksMonitorTask tmt = new TasksMonitorTask();
					tmt.task = task;
					tmt.msToStart = GetMsToTask(now, task);
					_taskMonitorList.Add(tmt);
					if (tmt.msToStart < msToNext) msToNext = tmt.msToStart.Value;
				}
				_lastUpdate = now;
				return msToNext;
			}	
		}

		private void StartUpdateCycle()
		{
			Globals.WriteToDebugFile("TaskMonitor: Starting Update Cycle");
			while (!_shutdown)
			{
				// Lock - Make sure there's nothing changing the task list
				int msToWait = 0;
				lock (_taskMonitorList)
				{
					msToWait = ProcessTasks();
				}
				SleepingPrincess(msToWait);
				if (_errorCount > 5) _shutdown = true;
			}
		}

		private int ProcessTasks()
		{
			int msToNext = 86400000; //24h * 60m * 60s * 1000 <- the max wait time is 24h
			Time now = new Time(DateTime.Now);
			int msFromLastUpd = TimeFuncs.GetMsFromTo(_lastUpdate, now);
			Coordinates currentPos = _positionTools.GetCurrentPosition();			

			foreach (TasksMonitorTask tmt in _taskMonitorList)
			{
				tmt.msToStart -= msFromLastUpd;
				
				if (tmt.msToStart <= 0)
				{
					tmt.msToStart = GetMsToTask(now, tmt.task);

					// re-check
					if (tmt.msToStart <= 0)
					{						
						if (currentPos.IsValid())
						{
							if (ProcessTask(tmt.task, currentPos))
							{
								tmt.msToStart = TimeFuncs.GetMsFromTo(now, tmt.task.MonitorStartTime);
							}
						}
					}					
				}				

				if (msToNext > tmt.msToStart.Value)
				{
					msToNext = tmt.msToStart.Value;
				}
			}

			if (msToNext < _updateRate)
			{
				msToNext = _updateRate;
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
				//if (nowStartTimeCompare <= 0)
				//{
				//    // Task will start later today
				//    msToTask = TimeFuncs.HoursToMilliseconds(task.MonitorStartTime.Hour - now.Hour);
				//}
				//else
				//{
				//    // Task will only start "tomorrow"
				//    msToTask = TimeFuncs.HoursToMilliseconds((23 - now.Hour) + task.MonitorStartTime.Hour);
				//}
				msToTask = TimeFuncs.GetMsFromTo(now, task.MonitorStartTime);
			}

			return msToTask;
		}

		private bool ProcessTask(Task task, Coordinates currentPos)
		{
			if(!task.LocationCoord.IsValid()) return false;		
			
			double distance = PositionTools.GetDistance(currentPos, task.LocationCoord);
			Globals.WriteToDebugFile("TaskMonitor: Task " + task.Subject + " distance: " + distance);
			if (distance > task.Radius) return false;

			Globals.WriteToDebugFile("TaskMonitor: Processed task: " + task.Subject);
			return true;
		}

		private void SleepingPrincess(int sleepMs)
		{
			if (sleepMs < 0)
			{
				Globals.WriteToDebugFile("TaskMonitor: ERROR: Invalid sleep time: " + sleepMs);
				sleepMs = 0;
				_errorCount++;
				return;
			}
			Globals.WriteToDebugFile("TaskMonitor: Sleeping time: " + sleepMs);
			_resetEvent.WaitOne(sleepMs, false);
		}
	}
}
