namespace Location_Scheduler
{
    partial class FormMain
    {
        private System.Windows.Forms.MainMenu mainMenu1;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemSetRefreshRate = new System.Windows.Forms.MenuItem();
			this.menuItemUseNonGpsLoc = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItemStartDaemon = new System.Windows.Forms.MenuItem();
			this.menuItemStopDaemon = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.menuItem2);
			// 
			// menuItem1
			// 
			this.menuItem1.Text = "Exit";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.MenuItems.Add(this.menuItemSetRefreshRate);
			this.menuItem2.MenuItems.Add(this.menuItemUseNonGpsLoc);
			this.menuItem2.MenuItems.Add(this.menuItem5);
			this.menuItem2.MenuItems.Add(this.menuItemStartDaemon);
			this.menuItem2.MenuItems.Add(this.menuItemStopDaemon);
			this.menuItem2.Text = "Options";
			// 
			// menuItemSetRefreshRate
			// 
			this.menuItemSetRefreshRate.Text = "Set refresh rate";
			this.menuItemSetRefreshRate.Click += new System.EventHandler(this.menuItemSetRefreshRate_Click);
			// 
			// menuItemUseNonGpsLoc
			// 
			this.menuItemUseNonGpsLoc.Text = "Use non-gps location";
			this.menuItemUseNonGpsLoc.Click += new System.EventHandler(this.menuItemUseNonGpsLoc_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Text = "-";
			// 
			// menuItemStartDaemon
			// 
			this.menuItemStartDaemon.Text = "Start daemon";
			this.menuItemStartDaemon.Click += new System.EventHandler(this.menuItemStartDaemon_Click);
			// 
			// menuItemStopDaemon
			// 
			this.menuItemStopDaemon.Text = "Stop daemon";
			this.menuItemStopDaemon.Click += new System.EventHandler(this.menuItemStopDaemon_Click);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.Menu = this.mainMenu1;
			this.Name = "FormMain";
			this.Text = "Location Scheduler";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItemStartDaemon;
		private System.Windows.Forms.MenuItem menuItemStopDaemon;
		private System.Windows.Forms.MenuItem menuItemSetRefreshRate;
		private System.Windows.Forms.MenuItem menuItemUseNonGpsLoc;
		private System.Windows.Forms.MenuItem menuItem5;

    }
}

