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
		PointLatLng startPos = new PointLatLng(41.1780080, -8.6087320);
		GMapMarkerCross centerCross;
		GMapOverlay overlay;
		public FormMap()
		{
			InitializeComponent();

		}

		private void menuItemSelect_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void FormMap_Load(object sender, EventArgs e)
		{
#if DEBUG && false
			this.map.Manager.Mode = AccessMode.ServerAndCache;
#else
			this.map.Manager.Mode = AccessMode.ServerOnly;
#endif
			menuItemSatellite.Checked = false;
			this.map.MapType = MapType.GoogleMap;
			this.map.MaxZoom = 20;
			this.map.MinZoom = 1;
			this.map.Zoom = this.map.MinZoom + 5;
			this.map.CurrentPosition = startPos;

			// map events
			map.OnCurrentPositionChanged += new CurrentPositionChanged(Map_OnCurrentPositionChanged);

			// Overlay setup
			overlay = new GMapOverlay(this.map, "overlay");
			this.map.Overlays.Add(overlay);

			// Center cross
			centerCross = new GMapMarkerCross(this.map.CurrentPosition);
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

	}
}