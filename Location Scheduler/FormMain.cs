using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;
using HelperLib;
using GMap.NET;
using System.IO;
using System.Globalization;
using System.Threading;
using Microsoft.WindowsCE.Forms;
using System.Runtime.InteropServices;
using OpenNETCF.Threading;
using System.Diagnostics;



namespace Location_Scheduler
{
    public partial class FormMain : SenseListForm
	{
		#region Declarations

		int _LastTaskID = 0;
		LSTasksConfig _tasksConfig = new LSTasksConfig();
		SensePanelButtonItem _BtnAddTask = null;
		MessageQueueManager _MsgQueueMgr = new MessageQueueManager(false);

		#endregion

		public FormMain()
        {
            InitializeComponent();
			_MsgQueueMgr.InitQueueEndPoints();
			senseHeaderCtrl.Text = StringTable.AppTittle;
        }

        #region Events

		private void FormMain_Load(object sender, EventArgs e)
		{			
			SetupControls();
			LoadTasks();

			this.menuItemUseNonGpsLoc.Checked = _tasksConfig.UseCellLocation;
		}

        private void menuItem1_Click(object sender, EventArgs e)
        {
			_MsgQueueMgr.Shutdown();
            Application.Exit();
        }

		private void menuItemStartDaemon_Click(object sender, EventArgs e)
		{
			StartDaemon();
		}

		private void menuItemStopDaemon_Click(object sender, EventArgs e)
		{
			// Just to make sure that a daemon is running... 
			using (NamedMutex mutex = new NamedMutex(false, "Global\\LocationScheduler\\LSCore"))
			{
				if (!mutex.WaitOne(0, false))
				{
					// Could not grab mutex-> daemon is running
					_MsgQueueMgr.Write(NotifMessages.NOTIF_STOP);						
				}
				else
				{
					SenseAPIs.SenseMessageBox.Show("No daemon running!", "Error", SenseMessageBoxButtons.OK);
				}				
			}			
		}

		private void menuItemSetRefreshRate_Click(object sender, EventArgs e)
		{
			int rate = _tasksConfig.UpdateRate;
			SenseAPIs.SenseNumericBox.Show("Refresh rate", 1, 99, ref rate);
			_tasksConfig.UpdateRate = rate;
		}

		private void menuItemUseNonGpsLoc_Click(object sender, EventArgs e)
		{
			menuItemUseNonGpsLoc.Checked = !menuItemUseNonGpsLoc.Checked;
			_tasksConfig.UseCellLocation = menuItemUseNonGpsLoc.Checked;
		}


        private void btAdd_Click(object sender)
        {
            FormTaskEdit formTaskEdit = new FormTaskEdit();
			if (Globals.ShowDialog(formTaskEdit, this) == DialogResult.OK)
			{
				AddTask(formTaskEdit.Task);
			}
        }

		void taskList_Click(object Sender)
		{
			SensePanelItem panelIt = (SensePanelItem)Sender;
			Task task = _tasksConfig.TaskList.Find(delegate(Task p) { return p.InternalIdentifier == (int)panelIt.Tag; });
			FormTaskEdit frmtaskedit = new FormTaskEdit(task);
			DialogResult taskEditResult = Globals.ShowDialog(frmtaskedit, this);
			switch (taskEditResult)
			{
				case DialogResult.OK:
				{
					panelIt.PrimaryText = task.Subject;
					SaveTasks();
					break;
				}
				case DialogResult.Abort:
				{
					DelTask(task);
					break;
				}
			}
		}

        #endregion

        #region Functions

        private void SetupControls()
		{
			// turn off UI updating
			this.senseListCtrl.BeginUpdate();

			// location button            
			_BtnAddTask = new SensePanelButtonItem("_BtnAddTask");
			_BtnAddTask.LabelText = "";
			_BtnAddTask.Text = "Add Task";
			_BtnAddTask.OnClick += new SensePanelButtonItem.ClickEventHandler(btAdd_Click);
			this.senseListCtrl.AddItem(_BtnAddTask);	

			this.senseListCtrl.AddItem(new SensePanelDividerItem("DividerItemTasks", "Tasks"));

			// we are done so turn on UI updating
			this.senseListCtrl.EndUpdate();

			setupSIP();
		}

		private void AddTask(Task task)
		{
			lock (senseListCtrl)
			{
				_LastTaskID++;
				task.InternalIdentifier = _LastTaskID;
				AddPanelItemForTask(task);

				_tasksConfig.TaskList.Add(task);
				SaveTasks();
			}
		}

		private void DelTask(Task task)
		{
			lock (senseListCtrl)
			{
				int panelCount = senseListCtrl.Count;
				for(int i = 0; i < panelCount; i++ ) 
				{
					int id = (senseListCtrl[i].Tag == null) ? -1 : (int)senseListCtrl[i].Tag;
					if (id == task.InternalIdentifier)
					{
						senseListCtrl.RemoveItem(i);
						break;
					}
				}

				_tasksConfig.TaskList.Remove(task);
				SaveTasks();
			}
		}

		private void AddPanelItemForTask(Task task)
		{
			SensePanelItem panelIt = new SensePanelItem("panelTask_" + task.InternalIdentifier);
			panelIt.Tag = task.InternalIdentifier;
			panelIt.PrimaryTextAlignment = SenseAPIs.SenseFont.PanelTextAlignment.Top;
			panelIt.PrimaryText = task.Subject;
			//			panelIt.PrimaryTextLineHeight = SenseAPIs.SenseFont.PanelTextLineHeight.SingleLine;
			panelIt.OnClick += new SensePanelItem.ClickEventHandler(taskList_Click);
			this.senseListCtrl.AddItem(panelIt);
		}

		private void LoadTasks()
		{
			_tasksConfig = TasksLoader.LoadTasksFromFile();
			List<Task> taskList = _tasksConfig.TaskList;
			// Set internal identifiers and add panel items for each task
			foreach (Task task in taskList)
			{
				_LastTaskID++;
				task.InternalIdentifier = _LastTaskID;
				AddPanelItemForTask(task);
			}		
		}

		private void SaveTasks()
		{
			TasksLoader.SaveTasksToFile(_tasksConfig);
			_MsgQueueMgr.Write(NotifMessages.NOTIF_SCAN);
		}

		private void StartDaemon()
		{
			String coreAppCmd = Globals.GetCurrentPath() + "\\LSCore.exe";
			System.Diagnostics.Process.Start(coreAppCmd, "");
			//ProcessStartInfo startInfo = new ProcessStartInfo();
			//startInfo.FileName = coreAppCmd;
			//Process process = new Process();
			//process.StartInfo = startInfo;
			//process.Start();			
			
		}

        #endregion		

		
    }
}