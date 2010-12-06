namespace Location_Scheduler
{
    partial class FormMain
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
			this.btAdd = new StedySoft.SenseSDK.SenseButtonControl();
			this.lstTarf = new System.Windows.Forms.ListBox();
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
			this.menuItem2.Text = "Ok";
			// 
			// btAdd
			// 
			this.btAdd.Location = new System.Drawing.Point(4, 5);
			this.btAdd.Name = "btAdd";
			this.btAdd.Size = new System.Drawing.Size(233, 28);
			this.btAdd.TabIndex = 0;
			this.btAdd.Text = "Add task";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click_1);
			// 
			// lstTarf
			// 
			this.lstTarf.Location = new System.Drawing.Point(4, 37);
			this.lstTarf.Name = "lstTarf";
			this.lstTarf.Size = new System.Drawing.Size(233, 184);
			this.lstTarf.TabIndex = 1;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.Controls.Add(this.btAdd);
			this.Controls.Add(this.lstTarf);
			this.Menu = this.mainMenu1;
			this.Name = "FormMain";
			this.Text = "Location Scheduler";
			this.ResumeLayout(false);

        }

        #endregion

        private StedySoft.SenseSDK.SenseButtonControl btAdd;
        public System.Windows.Forms.ListBox lstTarf;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;

    }
}

