namespace HelperLib
{
	partial class SenseListForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		protected System.ComponentModel.IContainer components = null;
		protected StedySoft.SenseSDK.SenseHeaderControl senseHeaderCtrl;
		protected StedySoft.SenseSDK.SenseListControl senseListCtrl;
		protected Microsoft.WindowsCE.Forms.InputPanel sip;

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
			this.senseHeaderCtrl.Text = "HeaderCtrl";
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
			// SenseListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.Controls.Add(this.senseListCtrl);
			this.Controls.Add(this.senseHeaderCtrl);
			this.Name = "SenseListForm";
			this.Text = "SenseListForm";
			this.ResumeLayout(false);

		}

		#endregion
	}
}