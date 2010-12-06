namespace Location_Scheduler
{
    partial class FormTaskEdit
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
			this.components = new System.ComponentModel.Container();
			this.senseHeaderCtrl = new StedySoft.SenseSDK.SenseHeaderControl();
			this.senseListCtrl = new StedySoft.SenseSDK.SenseListControl();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.sip = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
			this.SuspendLayout();
			// 
			// senseHeaderCtrl
			// 
			this.senseHeaderCtrl.Dock = System.Windows.Forms.DockStyle.Top;
			this.senseHeaderCtrl.Location = new System.Drawing.Point(0, 0);
			this.senseHeaderCtrl.Name = "senseHeaderCtrl";
			this.senseHeaderCtrl.Size = new System.Drawing.Size(240, 25);
			this.senseHeaderCtrl.TabIndex = 0;
			this.senseHeaderCtrl.Text = "Edit Task";
			// 
			// senseListCtrl
			// 
			this.senseListCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.senseListCtrl.FocusedItem = null;
			this.senseListCtrl.IsSecondaryScrollType = false;
			this.senseListCtrl.Location = new System.Drawing.Point(0, 25);
			this.senseListCtrl.MinimumMovement = 15;
			this.senseListCtrl.Name = "senseListCtrl";
			this.senseListCtrl.ShowScrollIndicator = true;
			this.senseListCtrl.Size = new System.Drawing.Size(240, 243);
			this.senseListCtrl.Springback = 0.35F;
			this.senseListCtrl.TabIndex = 1;
			this.senseListCtrl.ThreadSleep = 100;
			this.senseListCtrl.TopIndex = 0;
			this.senseListCtrl.Velocity = 0.35F;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.Text = "Back";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// FormTaskEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.ControlBox = false;
			this.Controls.Add(this.senseListCtrl);
			this.Controls.Add(this.senseHeaderCtrl);
			this.Menu = this.mainMenu1;
			this.Name = "FormTaskEdit";
			this.Text = "Location Scheduler";
			this.Load += new System.EventHandler(this.FormTaskEdit_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private StedySoft.SenseSDK.SenseHeaderControl senseHeaderCtrl;
        private StedySoft.SenseSDK.SenseListControl senseListCtrl;
        private System.Windows.Forms.MenuItem menuItem1;
		private Microsoft.WindowsCE.Forms.InputPanel sip;
    }
}