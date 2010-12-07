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

namespace Location_Scheduler
{
	public partial class FormMapSearch : Form
	{
		GMap.NET.WindowsForms.GMapControl map;
		public FormMapSearch(GMap.NET.WindowsForms.GMapControl map)
		{
			InitializeComponent();
			this.map = map;
		}

		private void btSearch_Click(object sender, EventArgs e)
		{
			DoSearch();
		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
			DoSearch();
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void DoSearch()
		{
			try
			{
				GeoCoderStatusCode status = GeoCoderStatusCode.Unknow;
				PointLatLng? pos = GMaps.Instance.GetLatLngFromGeocoder(tboxAddress.Text, out status);
				if (pos != null && status == GeoCoderStatusCode.G_GEO_SUCCESS)
				{
					map.CurrentPosition = pos.Value;
					map.Zoom = 12;
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