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


namespace Location_Scheduler
{
    public partial class FormMain : SenseListForm
	{
		#region Declarations

		int mTaskCounter = 0;
		List<Task> mTaskArray = null;
		String mFileToSaveTasks = null;
		
		SensePanelButtonItem mBtnAddTask = null;

		#endregion

		public FormMain()
        {
            InitializeComponent();
			senseHeaderCtrl.Text = StringTable.AppTittle;

			mFileToSaveTasks = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			mFileToSaveTasks += "\\tasks.xml";
        }

        #region Events

		private void FormMain_Load(object sender, EventArgs e)
		{			
			SetupControls();
			LoadTasksFromFile();
		}

        private void menuItem1_Click(object sender, EventArgs e)
        {
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
			SaveTasksToFile();
		}

		private void AddPanelItemForTask(Task task)
		{
			SensePanelItem panelIt = new SensePanelItem("panelTask_" + task.InternalIdentifier);
			panelIt.Tag = task.InternalIdentifier;
			panelIt.PrimaryTextAlignment = SenseAPIs.SenseFont.PanelTextAlignment.Top;
			panelIt.PrimaryText = task.Subject;
			//			panelIt.PrimaryTextLineHeight = SenseAPIs.SenseFont.PanelTextLineHeight.SingleLine;
			this.senseListCtrl.AddItem(panelIt);
		}

		private void SaveTasksToFile()
		{
			ObjectXMLSerializer<List<Task>>.Save(mTaskArray, mFileToSaveTasks);
		}

		private void LoadTasksFromFile()
		{
			try
			{
				mTaskArray = ObjectXMLSerializer<List<Task>>.Load(mFileToSaveTasks);
				String s ="";
				foreach (Task task in mTaskArray)
				{					
					s += task.InternalIdentifier + ";";
				}
				MessageBox.Show(s);

				// Set internal identifiers and add panel items for each task
				foreach (Task task in mTaskArray)
				{
					mTaskCounter++;
					task.InternalIdentifier = mTaskCounter;
					AddPanelItemForTask(task);
				}

				s = "";
				foreach (Task task in mTaskArray)
				{
					s += task.InternalIdentifier + ";";
				}
				MessageBox.Show(s);
				
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