using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;


namespace Location_Scheduler
{
    public partial class FormTaskEdit : Form
    {

		#region Declarations
		enum ActionTypes { SMS = 0, NOTIFICATION = 1, APP = 2 };

		StedySoft.SenseSDK.SensePanelButtonItem btnSetLocation = null;
		StedySoft.SenseSDK.SensePanelButtonItem btnSelectApplication = null;
		StedySoft.SenseSDK.SensePanelTextboxItem tboxSubject = null;
		StedySoft.SenseSDK.SensePanelTextboxItem tboxNotes = null;
		StedySoft.SenseSDK.SensePanelTextboxItem tboxSMSRecipient = null;
		StedySoft.SenseSDK.SensePanelTextboxItem tboxSMSBody = null;		
		StedySoft.SenseSDK.SensePanelComboItem cbbActionType = null;
		StedySoft.SenseSDK.SensePanelTimeItem timeMonitorStart = null;
		StedySoft.SenseSDK.SensePanelTimeItem timeMonitorEnd = null;

		#endregion

        public FormTaskEdit()
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
			tboxSubject = new StedySoft.SenseSDK.SensePanelTextboxItem();
			tboxSubject = new StedySoft.SenseSDK.SensePanelTextboxItem("tboxSubject");
			tboxSubject.LayoutSytle = SenseTexboxLayoutStyle.Vertical;
			tboxSubject.LabelText = "Subject:";
			tboxSubject.Text = "";
			this.senseListCtrl.AddItem(tboxSubject);

			// location button            
			btnSetLocation = new StedySoft.SenseSDK.SensePanelButtonItem("btnSetLocation");
			btnSetLocation.LabelText = "Location:";
			btnSetLocation.Text = "Set Location";
			btnSetLocation.OnClick += new SensePanelButtonItem.ClickEventHandler(OnBtnSetLocation);
			this.senseListCtrl.AddItem(btnSetLocation);

			// Action type combobox
			cbbActionType = new StedySoft.SenseSDK.SensePanelComboItem("cbbActionType");
			cbbActionType.OnSelectedIndexChanged += new SensePanelComboItem.SelectedIndexChangedEventHandler(OnCbbActionTypeSelectedIndexChanged);
			this.senseListCtrl.AddItem(cbbActionType);			
			cbbActionType.Items.Add(new SensePanelComboItem.Item("Show notification", ActionTypes.NOTIFICATION));
			cbbActionType.Items.Add(new SensePanelComboItem.Item("Send SMS", ActionTypes.SMS));
			cbbActionType.Items.Add(new SensePanelComboItem.Item("Launch application", ActionTypes.APP));
			cbbActionType.LabelText = "Action type:";
			cbbActionType.SelectedIndex = 0;

			// SMS recipient textbox
			tboxSMSRecipient = new StedySoft.SenseSDK.SensePanelTextboxItem();
			tboxSMSRecipient = new StedySoft.SenseSDK.SensePanelTextboxItem("tboxSMSRecipient");
			tboxSMSRecipient.LayoutSytle = SenseTexboxLayoutStyle.Vertical;
			tboxSMSRecipient.LabelText = "SMS Recipient:";
			tboxSMSRecipient.Text = "";
			tboxSMSRecipient.Visible = false;
			this.senseListCtrl.AddItem(tboxSMSRecipient);

			// SMS body textbox
			tboxSMSBody = new StedySoft.SenseSDK.SensePanelTextboxItem();
			tboxSMSBody = new StedySoft.SenseSDK.SensePanelTextboxItem("tboxSMSBody");
			tboxSMSBody.LayoutSytle = SenseTexboxLayoutStyle.Vertical;
			tboxSMSBody.LabelText = "SMS Text:";
			tboxSMSBody.Multiline = true;
			tboxSMSBody.Height = 200;
			tboxSMSBody.Text = "";
			tboxSMSBody.Visible = false;
			this.senseListCtrl.AddItem(tboxSMSBody);

			// Select application button            
			btnSelectApplication = new StedySoft.SenseSDK.SensePanelButtonItem("btnSelectApplication");
			btnSelectApplication.LabelText = "Application:";
			btnSelectApplication.Text = "Select application";
			btnSelectApplication.OnClick += new SensePanelButtonItem.ClickEventHandler(OnBtnSelectApplication);
			btnSelectApplication.Visible = false;
			this.senseListCtrl.AddItem(btnSelectApplication);


			// monitoring period time start
			this.senseListCtrl.AddItem(new StedySoft.SenseSDK.SensePanelDividerItem("DividerItemMonitorPeriod", "Monitoring period"));
			timeMonitorStart = new StedySoft.SenseSDK.SensePanelTimeItem("timeMonitorStart");
			timeMonitorStart.AutoShowDialog = true;
			timeMonitorStart.ButtonAnimation = true;
			timeMonitorStart.PrimaryText = "Start monitoring at";
//			timeMonitorStart.SecondaryText = "Style set for auto dialog...";
			timeMonitorStart.Time = new Time(DateTime.Now);
			this.senseListCtrl.AddItem(timeMonitorStart);

			// monitoring period time end
			timeMonitorEnd = new StedySoft.SenseSDK.SensePanelTimeItem("timeMonitorEnd");
			timeMonitorEnd.AutoShowDialog = true;
			timeMonitorEnd.ButtonAnimation = true;
			timeMonitorEnd.PrimaryText = "End monitoring at";
			timeMonitorEnd.Time = new Time(DateTime.Now);
			this.senseListCtrl.AddItem(timeMonitorEnd);

			// Notes textbox
			this.senseListCtrl.AddItem(new StedySoft.SenseSDK.SensePanelDividerItem("DividerItemTaskNotes", "Task description"));
			tboxNotes = new StedySoft.SenseSDK.SensePanelTextboxItem();
			tboxNotes = new StedySoft.SenseSDK.SensePanelTextboxItem("tboxNotes");
			tboxNotes.LayoutSytle = SenseTexboxLayoutStyle.Horizontal;
			tboxNotes.LabelText = "";
			tboxNotes.ShowSeparator = false;
			tboxNotes.Multiline = true;
			tboxNotes.Height = 200;
			tboxNotes.Text = "";
			this.senseListCtrl.AddItem(tboxNotes);

			// we are done so turn on UI updating
			this.senseListCtrl.EndUpdate();

			// enable Tap n' Hold & auto SIP for SensePanelTextboxItem(s)
			SIP.Enable(this.senseListCtrl.Handle);
			this.sip.EnabledChanged += new EventHandler(sip_EnabledChanged);
        }

		private void menuItem1_Click(object sender, EventArgs e)
		{
			Close();
		}

		void OnBtnSetLocation(object Sender)
		{
			FormMap frmMap = new FormMap();
			frmMap.ShowDialog();
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

		private int _sipOffset = 0;
		void sip_EnabledChanged(object sender, EventArgs e)
		{
			if (this.sip.Enabled)
			{
				SenseListControl.ISenseListItem IItem = this.senseListCtrl.FocusedItem;
				Rectangle r = IItem.ClientRectangle;
				r.Offset(0, this.senseListCtrl.Bounds.Top);
				if (IItem is SensePanelTextboxItem)
				{
					if (r.Bottom > this.sip.VisibleDesktop.Height)
					{
						this._sipOffset = Math.Abs(this.sip.VisibleDesktop.Height - r.Bottom);
						this.senseListCtrl.ScrollList(-this._sipOffset);
						this.senseListCtrl.Invalidate();
					}
				}
			}
			else
			{
				if (!this._sipOffset.Equals(0))
				{
					this.senseListCtrl.ScrollList(this._sipOffset);
					this.senseListCtrl.Invalidate();
				}
				this._sipOffset = 0;
			}
		}
	}
}