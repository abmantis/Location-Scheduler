using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;
using GMap.NET;
using HelperLib;


namespace Location_Scheduler
{
    public partial class FormTaskEdit : SenseListForm
    {

		#region Declarations

		private SensePanelButtonItem mBtnSetLocation = null;
		private SensePanelButtonItem mBtnSelectApplication = null;
		private SensePanelTextboxItem mTboxSubject = null;
		private SensePanelTextboxItem mTboxNotes = null;
		private SensePanelTextboxItem mTboxSMSRecipient = null;
		private SensePanelTextboxItem mTboxSMSBody = null;
		private SensePanelComboItem mCbbActionType = null;
		private SensePanelTimeItem mTimeMonitorStart = null;
		private SensePanelTimeItem mTimeMonitorEnd = null;
		private SensePanelItem mTxtLocation = null;
		private bool mEditMode = false;

		private Task mTask = new Task();
		public HelperLib.Task Task
		{
			get { return mTask; }
		}
		#endregion

        public FormTaskEdit() : base()
        {
            InitializeComponent();
			senseHeaderCtrl.Text = "Edit Task";
		}

		public FormTaskEdit(Task task) : this()
		{
			mEditMode = true;
			mTask = task;
		}

		#region Events

		private void FormTaskEdit_Load(object sender, EventArgs e)
        {
            SetupControls();
			if (mEditMode)
			{
				TaskClassToDialog();
			}
			else
			{
				menuItemDelete.Enabled = false;
			}
        }
        
		private void menuItem1_Click(object sender, EventArgs e)
		{
			DialogToTaskClass();
			String errorMsg = "";
			if (mTask.Validate(out errorMsg) == false)
			{
				SenseAPIs.SenseMessageBox.Show(errorMsg, "Invalid fields", SenseMessageBoxButtons.OK);
				return;
			}
			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void menuItemCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			Close();
		}
		private void menuItemDelete_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Abort;
			Close();
		}

		void OnBtnSetLocation(object Sender)
		{
			FormMap frmMap = new FormMap();
			if (mTask.LocationCoord != null)
			{
				frmMap.StartPos = new PointLatLng(mTask.LocationCoord.Latitude, mTask.LocationCoord.Longitude);
			}
			if (Globals.ShowDialog(frmMap, this) == DialogResult.OK)
			{
				Position_Lib.Coordinates coord = new Position_Lib.Coordinates();
				coord.Latitude = frmMap.CenterCross.Position.Lat;
				coord.Longitude = frmMap.CenterCross.Position.Lng;
				mTask.LocationCoord = coord;

				Placemark place = GMaps.Instance.GetPlacemarkFromGeocoder(frmMap.CenterCross.Position);
				if (place != null)
				{
					mTask.LocationAddress = place.Address;
				}
				else
				{
					mTask.LocationAddress = frmMap.CenterCross.Position.ToString();
				}
				SetLocationAddress(mTask.LocationAddress);
			}
		}

		void OnBtnSelectApplication(object Sender)
		{
			
		}
				
		void OnCbbActionTypeSelectedIndexChanged(object Sender, int Index)
		{			
			ProcessCbbType();		
		}

		#endregion

		#region Functions

		private void SetupControls()
		{
			// turn off UI updating
			this.senseListCtrl.BeginUpdate();

			// Subject textbox
			mTboxSubject = new SensePanelTextboxItem();
			mTboxSubject = new SensePanelTextboxItem("mTboxSubject");
			mTboxSubject.LayoutSytle = SenseTexboxLayoutStyle.Vertical;
			mTboxSubject.LabelText = "Subject:";
			mTboxSubject.Text = "";
			this.senseListCtrl.AddItem(mTboxSubject);

			// location button            
			mBtnSetLocation = new SensePanelButtonItem("mBtnSetLocation");
			mBtnSetLocation.LabelText = "Location:";
			mBtnSetLocation.Text = "Set Location";
			mBtnSetLocation.OnClick += new SensePanelButtonItem.ClickEventHandler(OnBtnSetLocation);
			this.senseListCtrl.AddItem(mBtnSetLocation);

			// location label
			mTxtLocation = new SensePanelItem("mTxtLocation");
			mTxtLocation.PrimaryTextAlignment = SenseAPIs.SenseFont.PanelTextAlignment.Top;
			//			mTxtLocation.PrimaryTextLineHeight = SenseAPIs.SenseFont.PanelTextLineHeight.SingleLine;
			mTxtLocation.Visible = false;
			this.senseListCtrl.AddItem(mTxtLocation);

			// Action type combobox
			mCbbActionType = new SensePanelComboItem("mCbbActionType");
			mCbbActionType.OnSelectedIndexChanged += new SensePanelComboItem.SelectedIndexChangedEventHandler(OnCbbActionTypeSelectedIndexChanged);
			this.senseListCtrl.AddItem(mCbbActionType);
			mCbbActionType.Items.Add(new SensePanelComboItem.Item("Show notification", Task.ActionTypes.NOTIFICATION));
			mCbbActionType.Items.Add(new SensePanelComboItem.Item("Send SMS", Task.ActionTypes.SMS));
			mCbbActionType.Items.Add(new SensePanelComboItem.Item("Launch application", Task.ActionTypes.APP));
			mCbbActionType.LabelText = "Action type:";
			mCbbActionType.SelectedIndex = 0;

			// SMS recipient textbox
			mTboxSMSRecipient = new SensePanelTextboxItem();
			mTboxSMSRecipient = new SensePanelTextboxItem("mTboxSMSRecipient");
			mTboxSMSRecipient.LayoutSytle = SenseTexboxLayoutStyle.Vertical;
			mTboxSMSRecipient.LabelText = "SMS Recipient:";
			mTboxSMSRecipient.Text = "";
			mTboxSMSRecipient.Visible = false;
			this.senseListCtrl.AddItem(mTboxSMSRecipient);

			// SMS body textbox
			mTboxSMSBody = new SensePanelTextboxItem();
			mTboxSMSBody = new SensePanelTextboxItem("mTboxSMSBody");
			mTboxSMSBody.LayoutSytle = SenseTexboxLayoutStyle.Vertical;
			mTboxSMSBody.LabelText = "SMS Text:";
			mTboxSMSBody.Multiline = true;
			mTboxSMSBody.Height = GetLongTextBoxSize();
			mTboxSMSBody.Text = "";
			mTboxSMSBody.Visible = false;
			this.senseListCtrl.AddItem(mTboxSMSBody);

			// Select application button            
			mBtnSelectApplication = new SensePanelButtonItem("mBtnSelectApplication");
			mBtnSelectApplication.LabelText = "Application:";
			mBtnSelectApplication.Text = "Select application";
			mBtnSelectApplication.OnClick += new SensePanelButtonItem.ClickEventHandler(OnBtnSelectApplication);
			mBtnSelectApplication.Visible = false;
			this.senseListCtrl.AddItem(mBtnSelectApplication);


			// monitoring period time start
			this.senseListCtrl.AddItem(new SensePanelDividerItem("DividerItemMonitorPeriod", "Monitoring period"));
			mTimeMonitorStart = new SensePanelTimeItem("mTimeMonitorStart");
			mTimeMonitorStart.AutoShowDialog = true;
			mTimeMonitorStart.ButtonAnimation = true;
			mTimeMonitorStart.PrimaryText = "Start monitoring at";
			//			mTimeMonitorStart.SecondaryText = "Style set for auto dialog...";
			mTimeMonitorStart.Time = new Time(DateTime.Now);
			this.senseListCtrl.AddItem(mTimeMonitorStart);

			// monitoring period time end
			mTimeMonitorEnd = new SensePanelTimeItem("mTimeMonitorEnd");
			mTimeMonitorEnd.AutoShowDialog = true;
			mTimeMonitorEnd.ButtonAnimation = true;
			mTimeMonitorEnd.PrimaryText = "End monitoring at";
			mTimeMonitorEnd.Time = new Time(DateTime.Now);
			this.senseListCtrl.AddItem(mTimeMonitorEnd);

			// Notes textbox
			this.senseListCtrl.AddItem(new SensePanelDividerItem("DividerItemTaskNotes", "Notes"));
			mTboxNotes = new SensePanelTextboxItem();
			mTboxNotes = new SensePanelTextboxItem("mTboxNotes");
			mTboxNotes.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
			mTboxNotes.LabelText = "";
			mTboxNotes.ShowSeparator = false;
			mTboxNotes.Multiline = true;
			mTboxNotes.Height = GetLongTextBoxSize();
			mTboxNotes.Text = "";
			this.senseListCtrl.AddItem(mTboxNotes);

			// we are done so turn on UI updating
			this.senseListCtrl.EndUpdate();

			setupSIP();
		}

		private void DialogToTaskClass()
		{
			mTask.ActionType = (Task.ActionTypes)mCbbActionType.SelectedItem.Value;
			mTask.MonitorEndTime = mTimeMonitorEnd.Time;
			mTask.MonitorStartTime = mTimeMonitorStart.Time;
			mTask.Notes = mTboxNotes.Text;
			mTask.SmsBody = mTboxSMSBody.Text;
			mTask.SmsRecipient = mTboxSMSRecipient.Text;
			mTask.Subject = mTboxSubject.Text;
		}

		private void TaskClassToDialog()
		{
			mCbbActionType.SelectedValue = mTask.ActionType;			
			SetLocationAddress(mTask.LocationAddress);
			mTimeMonitorEnd.Time = mTask.MonitorEndTime;
			mTimeMonitorStart.Time = mTask.MonitorStartTime;
			mTboxNotes.Text = mTask.Notes;
			mTboxSMSBody.Text = mTask.SmsBody;
			mTboxSMSRecipient.Text = mTask.SmsRecipient;
			mTboxSubject.Text = mTask.Subject;

			ProcessCbbType();
		}

		private void SetLocationAddress(String address)
		{
			mTxtLocation.PrimaryText = address;
			if (address.Trim().Length > 0)
			{
				mBtnSetLocation.ShowSeparator = false;
				mTxtLocation.Visible = true;
			}			
		}

		private void ProcessCbbType()
		{
			this.senseListCtrl.BeginUpdate();
			mTboxSMSRecipient.Visible = false;
			mTboxSMSBody.Visible = false;
			mBtnSelectApplication.Visible = false;

			Task.ActionTypes type = (Task.ActionTypes)mCbbActionType.SelectedValue;

			switch (type)
			{
				case Task.ActionTypes.SMS:
					mTboxSMSRecipient.Visible = true;
					mTboxSMSBody.Visible = true;
					break;
				case Task.ActionTypes.NOTIFICATION:

					break;
				case Task.ActionTypes.APP:
					mBtnSelectApplication.Visible = true;
					break;
				default:
					break;
			}

			this.senseListCtrl.EndUpdate();

		}

		#endregion

	}
}