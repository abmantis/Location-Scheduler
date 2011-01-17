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
	public partial class FormMapRadius : SenseListForm
	{
		private SensePanelTextboxItem textRadius = null;
		private SensePanelButtonItem btSet = null;
		private int radius = 0;
		public int Radius
		{
			get { return radius; }
			set { radius = value; }
		}
		
		public FormMapRadius() : base()
		{
			InitializeComponent();
			senseHeaderCtrl.Text = "Search Location";
		}

		private void FormMapRadius_Load(object sender, EventArgs e)
		{
			// turn off UI updating
			this.senseListCtrl.BeginUpdate();

			// Address textbox
//			this.senseListCtrl.AddItem(new SensePanelDividerItem("DividerItemTaskNotes", "Task description"));
			textRadius = new SensePanelTextboxItem("textRadius");
			textRadius.LayoutSytle = SenseTexboxLayoutStyle.Vertical;
			textRadius.LabelText = "Radius (in meters):";
			textRadius.ShowSeparator = false;
//			textRadius.Height = GetLongTextBoxSize();
			//textRadius.StdInputMask = SenseInputMaskType.Custom;
			//textRadius.InputMask = "00000";
			//textRadius.Value = radius.ToString();
			textRadius.Text = radius.ToString();
			this.senseListCtrl.AddItem(textRadius);

			// Search button            
			btSet = new SensePanelButtonItem("btSet");
			btSet.LabelText = "";
			btSet.Text = "Set";
			btSet.OnClick += new SensePanelButtonItem.ClickEventHandler(btSet_Click);
			this.senseListCtrl.AddItem(btSet);
			
			// we are done so turn on UI updating
			this.senseListCtrl.EndUpdate();
			
			setupSIP();
		}

		private void btSet_Click(object sender)
		{
			radius = int.Parse(textRadius.Text);
			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
			SetRadius();
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			SetRadius();
		}

		private void SetRadius()
		{
			try
			{
				radius = int.Parse(textRadius.Text);

				this.DialogResult = DialogResult.OK;
				Close();
			}
			catch (System.Exception)
			{
				SenseAPIs.SenseMessageBox.Show("Radius must be numeric", "Error", SenseMessageBoxButtons.OK);
			}			
		}
	}
}