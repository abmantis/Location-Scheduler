﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using HelperLib;

namespace Location_Scheduler
{
	public partial class FormMap : Form
	{
		#region Declarations

		private GMapOverlay _overlay;
		private PointLatLng _startPos = new PointLatLng(41.1780080, -8.6087320);
		private bool _hasStartPos = false;
		private GMapMarkerCross _centerCross = null;

		#region Properties
		public GMap.NET.PointLatLng StartPos
		{
			get { return _startPos; }
			set { _startPos = value; _hasStartPos = true; }
		}
		public GMapMarkerCross CenterCross
		{
			get { return _centerCross; }
		}
		#endregion

		#endregion

		public FormMap()
		{
			InitializeComponent();
		}

		#region Events

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
			_overlay = new GMapOverlay(this.map, "_overlay");
			this.map.Overlays.Add(_overlay);

			this.map.CurrentPosition = _startPos;
			_centerCross = new GMapMarkerCross(this.map.CurrentPosition);			

			// Add Center cross			
			_overlay.Markers.Add(_centerCross);

			if (_hasStartPos) map.Zoom = 14;
		}

		private void Map_OnCurrentPositionChanged(PointLatLng point)
		{
			_centerCross.Position = point;
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
			FormMapSearch frmSearch = new FormMapSearch();
			if (Globals.ShowDialog(frmSearch, this) == DialogResult.OK)
			{
				map.CurrentPosition = frmSearch.Pos;
				map.Zoom = 14;
			}		
		}
		
		private void menuItemSelect_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void menuItemCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			Close();
		}

		#endregion

	}
}