﻿using System;
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

		int mLastTaskID = 0;
		List<Task> mTaskArray = null;
		SensePanelButtonItem mBtnAddTask = null;
		MessageQueueManager _MsgQueueMgr = new MessageQueueManager(false);

		#endregion

		public FormMain()
        {
            InitializeComponent();
			senseHeaderCtrl.Text = StringTable.AppTittle;
			int x = 0;
			SenseAPIs.SenseNumericBox.Show("blalala",ref x);
        }

        #region Events

		private void FormMain_Load(object sender, EventArgs e)
		{			
			SetupControls();
			LoadTasks();
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

		}

		private void menuItemUseNonGpsLoc_Click(object sender, EventArgs e)
		{

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
			Task task = mTaskArray.Find(delegate(Task p) { return p.InternalIdentifier == (int)panelIt.Tag; });
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
			mBtnAddTask = new SensePanelButtonItem("mBtnAddTask");
			mBtnAddTask.LabelText = "";
			mBtnAddTask.Text = "Add Task";
			mBtnAddTask.OnClick += new SensePanelButtonItem.ClickEventHandler(btAdd_Click);
			this.senseListCtrl.AddItem(mBtnAddTask);	

			this.senseListCtrl.AddItem(new SensePanelDividerItem("DividerItemTasks", "Tasks"));
			

			// we are done so turn on UI updating
			this.senseListCtrl.EndUpdate();

			setupSIP();
		}

		private void AddTask(Task task)
		{
			lock (senseListCtrl)
			{
				mLastTaskID++;
				task.InternalIdentifier = mLastTaskID;
				AddPanelItemForTask(task);

				mTaskArray.Add(task);
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

				mTaskArray.Remove(task);
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
			mTaskArray = TasksLoader.LoadTasksFromFile();
			// Set internal identifiers and add panel items for each task
			foreach (Task task in mTaskArray)
			{
				mLastTaskID++;
				task.InternalIdentifier = mLastTaskID;
				AddPanelItemForTask(task);
			}		
		}

		private void SaveTasks()
		{
			TasksLoader.SaveTasksToFile(mTaskArray);
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