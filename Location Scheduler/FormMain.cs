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



namespace Location_Scheduler
{
    public partial class FormMain : SenseListForm
	{
		#region Declarations

		TasksLoader mTasksLoader = null;
		int mTaskCounter = 0;
		List<Task> mTaskArray = null;
		SensePanelButtonItem mBtnAddTask = null;

		#endregion

		public FormMain()
        {
            InitializeComponent();
			senseHeaderCtrl.Text = StringTable.AppTittle;

			mTasksLoader = new TasksLoader();
        }

        #region Events

		private void FormMain_Load(object sender, EventArgs e)
		{			
			SetupControls();
			LoadTasks();
		}

        private void menuItem1_Click(object sender, EventArgs e)
        {
			//HelperLib.MessageQueueManager mqm = new MessageQueueManager(false);
			//mqm.Write("Hello there?!");
			//mqm.Shutdown();

            Application.Exit();
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
			if (Globals.ShowDialog(frmtaskedit, this) == DialogResult.OK)
			{
				panelIt.PrimaryText = task.Subject;
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
			mTaskCounter++;
			task.InternalIdentifier = mTaskCounter;
			AddPanelItemForTask(task);

			mTaskArray.Add(task);
			mTasksLoader.SaveTasksToFile(mTaskArray);
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
			try
			{
				mTaskArray = mTasksLoader.LoadTasksFromFile();
				// Set internal identifiers and add panel items for each task
				foreach (Task task in mTaskArray)
				{
					mTaskCounter++;
					task.InternalIdentifier = mTaskCounter;
					AddPanelItemForTask(task);
				}				
			}
#pragma warning disable 0168
			catch (FileNotFoundException ex)
#pragma warning restore 0168
			{
				mTaskArray = new List<Task>();
			}			
		}
        #endregion		
    }
}