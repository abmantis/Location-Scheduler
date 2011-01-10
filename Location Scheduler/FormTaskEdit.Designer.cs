namespace Location_Scheduler
{
    partial class FormTaskEdit
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
			this.menuItemDelete = new System.Windows.Forms.MenuItem();
			this.menuItemCancel = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.menuItem2);
			// 
			// menuItem1
			// 
			this.menuItem1.Text = "Save";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.MenuItems.Add(this.menuItemDelete);
			this.menuItem2.MenuItems.Add(this.menuItemCancel);
			this.menuItem2.Text = "Menu";
			// 
			// menuItemDelete
			// 
			this.menuItemDelete.Text = "Delete";
			this.menuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
			// 
			// menuItemCancel
			// 
			this.menuItemCancel.Text = "Cancel";
			this.menuItemCancel.Click += new System.EventHandler(this.menuItemCancel_Click);
			// 
			// FormTaskEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.ControlBox = false;
			this.Menu = this.mainMenu1;
			this.Name = "FormTaskEdit";
			this.Text = "Location Scheduler";
			this.Load += new System.EventHandler(this.FormTaskEdit_Load);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItemDelete;
		private System.Windows.Forms.MenuItem menuItemCancel;
    }
}