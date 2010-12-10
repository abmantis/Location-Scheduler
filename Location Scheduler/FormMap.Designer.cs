namespace Location_Scheduler
{
	partial class FormMap
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MainMenu mainMenu1;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItemSelect = new System.Windows.Forms.MenuItem();
			this.menuItemOptions = new System.Windows.Forms.MenuItem();
			this.menuItemSatellite = new System.Windows.Forms.MenuItem();
			this.menuItemCancel = new System.Windows.Forms.MenuItem();
			this.senseHeaderCtrl = new StedySoft.SenseSDK.SenseHeaderControl();
			this.map = new GMap.NET.WindowsForms.GMapControl();
			this.btSearch = new StedySoft.SenseSDK.SenseButtonControl();
			this.btZoomIn = new StedySoft.SenseSDK.SenseButtonControl();
			this.btZoomOut = new StedySoft.SenseSDK.SenseButtonControl();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItemSelect);
			this.mainMenu1.MenuItems.Add(this.menuItemOptions);
			// 
			// menuItemSelect
			// 
			this.menuItemSelect.Text = "Select";
			this.menuItemSelect.Click += new System.EventHandler(this.menuItemSelect_Click);
			// 
			// menuItemOptions
			// 
			this.menuItemOptions.MenuItems.Add(this.menuItemSatellite);
			this.menuItemOptions.MenuItems.Add(this.menuItemCancel);
			this.menuItemOptions.Text = "Options";
			// 
			// menuItemSatellite
			// 
			this.menuItemSatellite.Text = "Satellite view";
			this.menuItemSatellite.Click += new System.EventHandler(this.menuItemSatellite_Click);
			// 
			// menuItemCancel
			// 
			this.menuItemCancel.Text = "Cancel";
			this.menuItemCancel.Click += new System.EventHandler(this.menuItemCancel_Click);
			// 
			// senseHeaderCtrl
			// 
			this.senseHeaderCtrl.Dock = System.Windows.Forms.DockStyle.Top;
			this.senseHeaderCtrl.Location = new System.Drawing.Point(0, 0);
			this.senseHeaderCtrl.Name = "senseHeaderCtrl";
			this.senseHeaderCtrl.Size = new System.Drawing.Size(240, 25);
			this.senseHeaderCtrl.TabIndex = 0;
			this.senseHeaderCtrl.Text = "Set Location";
			// 
			// map
			// 
			this.map.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.map.BackColor = this.BackColor;
			this.map.CanDragMap = true;
			this.map.Location = new System.Drawing.Point(4, 29);
			this.map.MarkersEnabled = true;
			this.map.MaxZoom = 2;
			this.map.MinZoom = 2;
			this.map.Name = "map";
			this.map.Size = new System.Drawing.Size(233, 174);
			this.map.TabIndex = 0;
			this.map.Zoom = 0;
			// 
			// btSearch
			// 
			this.btSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.btSearch.Location = new System.Drawing.Point(4, 237);
			this.btSearch.Name = "btSearch";
			this.btSearch.Size = new System.Drawing.Size(233, 28);
			this.btSearch.TabIndex = 3;
			this.btSearch.Text = "Search";
			this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
			// 
			// btZoomIn
			// 
			this.btZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btZoomIn.Location = new System.Drawing.Point(4, 207);
			this.btZoomIn.Name = "btZoomIn";
			this.btZoomIn.Size = new System.Drawing.Size(112, 28);
			this.btZoomIn.TabIndex = 1;
			this.btZoomIn.Text = "Zoom in";
			this.btZoomIn.Click += new System.EventHandler(this.btZoomIn_Click);
			// 
			// btZoomOut
			// 
			this.btZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.btZoomOut.Location = new System.Drawing.Point(125, 207);
			this.btZoomOut.Name = "btZoomOut";
			this.btZoomOut.Size = new System.Drawing.Size(112, 28);
			this.btZoomOut.TabIndex = 2;
			this.btZoomOut.Text = "Zoom out";
			this.btZoomOut.Click += new System.EventHandler(this.btZoomOut_Click);
			// 
			// FormMap
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.ControlBox = false;
			this.Controls.Add(this.btZoomOut);
			this.Controls.Add(this.btZoomIn);
			this.Controls.Add(this.btSearch);
			this.Controls.Add(this.map);
			this.Controls.Add(this.senseHeaderCtrl);
			this.Menu = this.mainMenu1;
			this.Name = "FormMap";
			this.Text = StringTable.AppTittle;
			this.Load += new System.EventHandler(this.FormMap_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private StedySoft.SenseSDK.SenseHeaderControl senseHeaderCtrl;
		private GMap.NET.WindowsForms.GMapControl map;
		private System.Windows.Forms.MenuItem menuItemSelect;
		private System.Windows.Forms.MenuItem menuItemOptions;
		private System.Windows.Forms.MenuItem menuItemCancel;
		private System.Windows.Forms.MenuItem menuItemSatellite;
		private StedySoft.SenseSDK.SenseButtonControl btSearch;
		private StedySoft.SenseSDK.SenseButtonControl btZoomIn;
		private StedySoft.SenseSDK.SenseButtonControl btZoomOut;
	}
}