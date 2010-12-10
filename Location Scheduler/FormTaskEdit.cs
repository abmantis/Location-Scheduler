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
		enum ActionTypes { SMS = 0, NOTIFICATION = 1, APP = 2 };

		private SensePanelButtonItem btnSetLocation = null;
		private SensePanelButtonItem btnSelectApplication = null;
		private SensePanelTextboxItem tboxSubject = null;
		private SensePanelTextboxItem tboxNotes = null;
		private SensePanelTextboxItem tboxSMSRecipient = null;
		private SensePanelTextboxItem tboxSMSBody = null;
		private SensePanelComboItem cbbActionType = null;
		private SensePanelTimeItem timeMonitorStart = null;
		private SensePanelTimeItem timeMonitorEnd = null;
		private SensePanelItem txtLocation = null;

		private FormMap frmMap = null;

		#endregion

        public FormTaskEdit() : base()
        {
            InitializeComponent();
        }

        private void FormTaskEdit_Load(object sender, EventArgs e)
        {
            SetupControls();

        }
        private void SetupControls()
        {
            // turn off UI updating
            this.senseListCtrl.BeginUpdate();
			
			// Subject textbox
			tboxSubject = new SensePanelTextboxItem();
			tboxSubject = new SensePanelTextboxItem("tboxSubject");
			tboxSubject.LayoutSytle = SenseTexboxLayoutStyle.Vertical;
			tboxSubject.LabelText = "Subject:";
			tboxSubject.Text = "";
			this.senseListCtrl.AddItem(tboxSubject);

			// location button            
			btnSetLocation = new SensePanelButtonItem("btnSetLocation");
			btnSetLocation.LabelText = "Location:";
			btnSetLocation.Text = "Set Location";
			btnSetLocation.OnClick += new SensePanelButtonItem.ClickEventHandler(OnBtnSetLocation);
			this.senseListCtrl.AddItem(btnSetLocation);

			// location label
			txtLocation = new SensePanelItem("txtLocation");
			txtLocation.PrimaryTextAlignment = SenseAPIs.SenseFont.PanelTextAlignment.Top;
//			txtLocation.PrimaryTextLineHeight = SenseAPIs.SenseFont.PanelTextLineHeight.SingleLine;
			txtLocation.Visible = false;
			this.senseListCtrl.AddItem(txtLocation);

			// Action type combobox
			cbbActionType = new SensePanelComboItem("cbbActionType");
			cbbActionType.OnSelectedIndexChanged += new SensePanelComboItem.SelectedIndexChangedEventHandler(OnCbbActionTypeSelectedIndexChanged);
			this.senseListCtrl.AddItem(cbbActionType);			
			cbbActionType.Items.Add(new SensePanelComboItem.Item("Show notification", ActionTypes.NOTIFICATION));
			cbbActionType.Items.Add(new SensePanelComboItem.Item("Send SMS", ActionTypes.SMS));
			cbbActionType.Items.Add(new SensePanelComboItem.Item("Launch application", ActionTypes.APP));
			cbbActionType.LabelText = "Action type:";
			cbbActionType.SelectedIndex = 0;

			// SMS recipient textbox
			tboxSMSRecipient = new SensePanelTextboxItem();
			tboxSMSRecipient = new SensePanelTextboxItem("tboxSMSRecipient");
			tboxSMSRecipient.LayoutSytle = SenseTexboxLayoutStyle.Vertical;
			tboxSMSRecipient.LabelText = "SMS Recipient:";
			tboxSMSRecipient.Text = "";
			tboxSMSRecipient.Visible = false;
			this.senseListCtrl.AddItem(tboxSMSRecipient);

			// SMS body textbox
			tboxSMSBody = new SensePanelTextboxItem();
			tboxSMSBody = new SensePanelTextboxItem("tboxSMSBody");
			tboxSMSBody.LayoutSytle = SenseTexboxLayoutStyle.Vertical;
			tboxSMSBody.LabelText = "SMS Text:";
			tboxSMSBody.Multiline = true;
			tboxSMSBody.Height = 200;
			tboxSMSBody.Text = "";
			tboxSMSBody.Visible = false;
			this.senseListCtrl.AddItem(tboxSMSBody);

			// Select application button            
			btnSelectApplication = new SensePanelButtonItem("btnSelectApplication");
			btnSelectApplication.LabelText = "Application:";
			btnSelectApplication.Text = "Select application";
			btnSelectApplication.OnClick += new SensePanelButtonItem.ClickEventHandler(OnBtnSelectApplication);
			btnSelectApplication.Visible = false;
			this.senseListCtrl.AddItem(btnSelectApplication);


			// monitoring period time start
			this.senseListCtrl.AddItem(new SensePanelDividerItem("DividerItemMonitorPeriod", "Monitoring period"));
			timeMonitorStart = new SensePanelTimeItem("timeMonitorStart");
			timeMonitorStart.AutoShowDialog = true;
			timeMonitorStart.ButtonAnimation = true;
			timeMonitorStart.PrimaryText = "Start monitoring at";
//			timeMonitorStart.SecondaryText = "Style set for auto dialog...";
			timeMonitorStart.Time = new Time(DateTime.Now);
			this.senseListCtrl.AddItem(timeMonitorStart);

			// monitoring period time end
			timeMonitorEnd = new SensePanelTimeItem("timeMonitorEnd");
			timeMonitorEnd.AutoShowDialog = true;
			timeMonitorEnd.ButtonAnimation = true;
			timeMonitorEnd.PrimaryText = "End monitoring at";
			timeMonitorEnd.Time = new Time(DateTime.Now);
			this.senseListCtrl.AddItem(timeMonitorEnd);

			// Notes textbox
			this.senseListCtrl.AddItem(new SensePanelDividerItem("DividerItemTaskNotes", "Task description"));
			tboxNotes = new SensePanelTextboxItem();
			tboxNotes = new SensePanelTextboxItem("tboxNotes");
			tboxNotes.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
			tboxNotes.LabelText = "";
			tboxNotes.ShowSeparator = false;
			tboxNotes.Multiline = true;
			tboxNotes.Height = 200;
			tboxNotes.Text = "";
			this.senseListCtrl.AddItem(tboxNotes);

			// we are done so turn on UI updating
			this.senseListCtrl.EndUpdate();

			setupSIP();
		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
			Close();
		}

		void OnBtnSetLocation(object Sender)
		{
			if (frmMap == null)
			{
				frmMap = new FormMap();
			}
			Globals.ShowDialog(frmMap, this);
			if (frmMap.IsPosSelected)
			{				
				Placemark place = GMaps.Instance.GetPlacemarkFromGeocoder(frmMap.CenterCross.Position);
				if (place != null)
				{
					txtLocation.PrimaryText = place.Address;
				}
				else
				{
					txtLocation.PrimaryText = frmMap.CenterCross.Position.ToString();
				}
				btnSetLocation.ShowSeparator = false;
				txtLocation.Visible = true;
			}
		}

		void OnBtnSelectApplication(object Sender)
		{
			
		}
				
		void OnCbbActionTypeSelectedIndexChanged(object Sender, int Index)
		{
			this.senseListCtrl.BeginUpdate();
			tboxSMSRecipient.Visible = false;
			tboxSMSBody.Visible = false;
			btnSelectApplication.Visible = false;

			ActionTypes type = (ActionTypes)cbbActionType.Items[Index].Value;

			switch (type)
			{
			case ActionTypes.SMS:
				tboxSMSRecipient.Visible = true;
				tboxSMSBody.Visible = true;
				break;
			case ActionTypes.NOTIFICATION:

			break;
			case ActionTypes.APP:
				btnSelectApplication.Visible = true;
				break;
			default:
			break;
			}

			this.senseListCtrl.EndUpdate();
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}