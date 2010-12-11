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

		List<Task> taskArray = null;
		String fileToSaveTasks = null;
		
		SensePanelButtonItem btnAddTask = null;

		#endregion

		public FormMain()
        {
            InitializeComponent();			

			fileToSaveTasks = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			fileToSaveTasks += "tasks.xml";
        }

        #region Events

		private void FormMain_Load(object sender, EventArgs e)
		{
			loadTasksFromFile();
			SetupControls();
// 			Task t = new Task();
// 			t.Subject = "Notifica-me";
// 			t.Notes = "Não te esqueças de notificar. ta?";
// 			t.LocationAddress = "Uma rua qualquer por ali";
// 			t.LocationCoord = new PointLatLng(0.123, 0.456);
// 			t.ActionType = Task.ActionTypes.NOTIFICATION;
// 			taskArray.Add(t);			
			
		}

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btAdd_Click(object sender)
        {
            FormTaskEdit formTaskEdit = new FormTaskEdit();
			Globals.ShowDialog(formTaskEdit, this);
        }

        #endregion

        #region Functions

		private void saveTasksToFile()
		{			
			ObjectXMLSerializer<List<Task>>.Save(taskArray, fileToSaveTasks);
		}

        private void SetupControls()
		{
			// turn off UI updating
			this.senseListCtrl.BeginUpdate();

			// location button            
			btnAddTask = new SensePanelButtonItem("btnAddTask");
			btnAddTask.LabelText = "";
			btnAddTask.Text = "Add Task";
			btnAddTask.OnClick += new SensePanelButtonItem.ClickEventHandler(btAdd_Click);
			this.senseListCtrl.AddItem(btnAddTask);	

			this.senseListCtrl.AddItem(new SensePanelDividerItem("DividerItemTasks", "Tasks"));
			

			// we are done so turn on UI updating
			this.senseListCtrl.EndUpdate();

			setupSIP();
		}

		private void loadTasksFromFile()
		{
			try
			{
				taskArray = ObjectXMLSerializer<List<Task>>.Load(fileToSaveTasks);
			}
			catch (FileNotFoundException ex)
			{                
				taskArray = new List<Task>();
			}			
		}
        #endregion		
    }
}