using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace Location_Scheduler
{
	public partial class FormMap : Form
	{
		#region Declarations

		private GMapOverlay overlay;
		private PointLatLng startPos = new PointLatLng(41.1780080, -8.6087320);

		private GMapMarkerCross centerCross;
		public GMapMarkerCross CenterCross
		{
			get { return centerCross; }
			set { centerCross = value; isPosSelected = true; }
		}

		private bool isPosSelected = false;
		public bool IsPosSelected
		{
			get { return isPosSelected; }
		}
		
		#endregion

		public FormMap()
		{
			InitializeComponent();

		}

		private void FormMap_Load(object sender, EventArgs e)
		{
			menuItemSatellite.Checked = false;

			this.map.Manager.Mode = AccessMode.ServerOnly;			
			this.map.MapType = MapType.GoogleMap;
			this.map.MaxZoom = 20;
			this.map.MinZoom = 1;
			this.map.Zoom = this.map.MinZoom + 5;
			
			// map events
			map.OnCurrentPositionChanged += new CurrentPositionChanged(Map_OnCurrentPositionChanged);

			// Overlay setup
			overlay = new GMapOverlay(this.map, "overlay");
			this.map.Overlays.Add(overlay);


			if (isPosSelected)
			{
				this.map.CurrentPosition = CenterCross.Position;
			}
			else
			{
				this.map.CurrentPosition = startPos;
				centerCross = new GMapMarkerCross(this.map.CurrentPosition);				
			}

			// Center cross			
			overlay.Markers.Add(centerCross);
		}

		private void Map_OnCurrentPositionChanged(PointLatLng point)
		{
			centerCross.Position = point;
		}

		private void menuItemSatellite_Click(object sender, EventArgs e)
		{
			menuItemSatellite.Checked = !menuItemSatellite.Checked;
			if (menuItemSatellite.Checked)
			{
				this.map.MapType = MapType.GoogleSatellite;
			}
			else
			{
				this.map.MapType = MapType.GoogleMap;
			}
			this.map.ReloadMap();
		}

		private void btZoomIn_Click(object sender, EventArgs e)
		{
			this.map.Zoom += 1;
		}

		private void btZoomOut_Click(object sender, EventArgs e)
		{
			this.map.Zoom -= 1;
		}

		private void btSearch_Click(object sender, EventArgs e)
		{
			FormMapSearch frmSearch = new FormMapSearch(this.map);
			frmSearch.ShowDialog();
		}
		
		private void menuItemSelect_Click(object sender, EventArgs e)
		{
			isPosSelected = true;
			Close();
		}

		private void menuItemCancel_Click(object sender, EventArgs e)
		{
			isPosSelected = false;
		}

	}
}