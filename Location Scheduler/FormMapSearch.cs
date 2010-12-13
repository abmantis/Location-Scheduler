using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using StedySoft.SenseSDK.DrawingCE;
using StedySoft.SenseSDK;
using HelperLib;

namespace Location_Scheduler
{
	public partial class FormMapSearch : SenseListForm
	{
		private SensePanelTextboxItem tboxAddress = null;
		private SensePanelButtonItem btSearch = null;
		private PointLatLng? pos = null;
		public PointLatLng Pos
		{
			get { return pos.Value; }
		}
		public FormMapSearch() : base()
		{
			InitializeComponent();
			senseHeaderCtrl.Text = "Search Location";
		}

		private void FormMapSearch_Load(object sender, EventArgs e)
		{
			// turn off UI updating
			this.senseListCtrl.BeginUpdate();

			// Address textbox
//			this.senseListCtrl.AddItem(new SensePanelDividerItem("DividerItemTaskNotes", "Task description"));
			tboxAddress = new SensePanelTextboxItem();
			tboxAddress = new SensePanelTextboxItem("tboxAddress");
			tboxAddress.LayoutSytle = SenseTexboxLayoutStyle.Vertical;
			tboxAddress.LabelText = "Address:";
			tboxAddress.ShowSeparator = false;
			tboxAddress.Multiline = true;
			tboxAddress.Height = GetLongTextBoxSize();
			tboxAddress.Text = "";
			this.senseListCtrl.AddItem(tboxAddress);

			// Search button            
			btSearch = new SensePanelButtonItem("btSearch");
			btSearch.LabelText = "";
			btSearch.Text = "Search";
			btSearch.OnClick += new SensePanelButtonItem.ClickEventHandler(btSearch_Click);
			this.senseListCtrl.AddItem(btSearch);
			
			// we are done so turn on UI updating
			this.senseListCtrl.EndUpdate();
			
			setupSIP();
		}

		private void btSearch_Click(object sender)
		{
			DoSearch();
		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
			DoSearch();
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void DoSearch()
		{
			try
			{
				GeoCoderStatusCode status = GeoCoderStatusCode.Unknow;
				pos = GMaps.Instance.GetLatLngFromGeocoder(tboxAddress.Text, out status);
				if (pos != null && status == GeoCoderStatusCode.G_GEO_SUCCESS)
				{					
					this.DialogResult = DialogResult.OK;
					Close();
				}
				else
				{
					SenseAPIs.SenseMessageBox.Show(status.ToString(), "Error", SenseMessageBoxButtons.OK);
				}
			}
			catch (System.Exception ex)
			{
				SenseAPIs.SenseMessageBox.Show(ex.Message, "Error", SenseMessageBoxButtons.OK);
			}			
		}
	}
}