namespace Location_Scheduler
{
	partial class FormMapSearch
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
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.senseHeaderCtrl = new StedySoft.SenseSDK.SenseHeaderControl();
			this.tboxAddress = new StedySoft.SenseSDK.SenseTextboxControl();
			this.senseLabelControl1 = new StedySoft.SenseSDK.SenseLabelControl();
			this.btSearch = new StedySoft.SenseSDK.SenseButtonControl();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.menuItem2);
			// 
			// menuItem1
			// 
			this.menuItem1.Text = "Search";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Text = "Cancel";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// senseHeaderCtrl
			// 
			this.senseHeaderCtrl.Dock = System.Windows.Forms.DockStyle.Top;
			this.senseHeaderCtrl.Location = new System.Drawing.Point(0, 0);
			this.senseHeaderCtrl.Name = "senseHeaderCtrl";
			this.senseHeaderCtrl.Size = new System.Drawing.Size(240, 25);
			this.senseHeaderCtrl.TabIndex = 0;
			this.senseHeaderCtrl.Text = "Search Location";
			// 
			// tboxAddress
			// 
			this.tboxAddress.AcceptsReturn = false;
			this.tboxAddress.AcceptsTab = false;
			this.tboxAddress.HideSelection = true;
			this.tboxAddress.Location = new System.Drawing.Point(4, 52);
			this.tboxAddress.MaxLength = 32767;
			this.tboxAddress.Multiline = true;
			this.tboxAddress.Name = "tboxAddress";
			this.tboxAddress.PasswordChar = '\0';
			this.tboxAddress.ReadOnly = false;
			this.tboxAddress.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.tboxAddress.Size = new System.Drawing.Size(233, 57);
			this.tboxAddress.TabIndex = 1;
			this.tboxAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.tboxAddress.WordWrap = false;
			// 
			// senseLabelControl1
			// 
			this.senseLabelControl1.Location = new System.Drawing.Point(3, 28);
			this.senseLabelControl1.Name = "senseLabelControl1";
			this.senseLabelControl1.Size = new System.Drawing.Size(234, 19);
			this.senseLabelControl1.TabIndex = 0;
			this.senseLabelControl1.Text = "Address:";
			this.senseLabelControl1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// btSearch
			// 
			this.btSearch.Location = new System.Drawing.Point(4, 115);
			this.btSearch.Name = "btSearch";
			this.btSearch.Size = new System.Drawing.Size(233, 28);
			this.btSearch.TabIndex = 2;
			this.btSearch.Text = "Search";
			this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
			// 
			// FormMapSearch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.ControlBox = false;
			this.Controls.Add(this.senseHeaderCtrl);
			this.Controls.Add(this.btSearch);
			this.Controls.Add(this.senseLabelControl1);
			this.Controls.Add(this.tboxAddress);
			this.Menu = this.mainMenu1;
			this.Name = "FormMapSearch";
			this.Text = "Location Scheduler";
			this.ResumeLayout(false);

		}

		#endregion

		private StedySoft.SenseSDK.SenseHeaderControl senseHeaderCtrl;
		private StedySoft.SenseSDK.SenseTextboxControl tboxAddress;
		private StedySoft.SenseSDK.SenseLabelControl senseLabelControl1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private StedySoft.SenseSDK.SenseButtonControl btSearch;
	}
}