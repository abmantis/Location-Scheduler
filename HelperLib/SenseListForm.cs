using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StedySoft.SenseSDK;

namespace HelperLib
{
	public partial class SenseListForm : Form
	{
		public SenseListForm()
		{
			InitializeComponent();
		}

		protected void setupSIP()
		{
			// enable Tap n' Hold & auto SIP for SensePanelTextboxItem(s)
			SIP.Enable(this.senseListCtrl.Handle);
			this.sip.EnabledChanged += new EventHandler(sip_EnabledChanged);
		}

		private int _sipOffset = 0;
		protected void sip_EnabledChanged(object sender, EventArgs e)
		{
			if (this.sip.Enabled)
			{
				SenseListControl.ISenseListItem IItem = this.senseListCtrl.FocusedItem;
				System.Drawing.Rectangle r = IItem.ClientRectangle;
				r.Offset(0, this.senseListCtrl.Bounds.Top);
				if (IItem is SensePanelTextboxItem)
				{
					if (r.Bottom > this.sip.VisibleDesktop.Height)
					{
						this._sipOffset = Math.Abs(this.sip.VisibleDesktop.Height - r.Bottom);
						this.senseListCtrl.ScrollList(-this._sipOffset);
						this.senseListCtrl.Invalidate();
					}
				}
			}
			else
			{
				if (!this._sipOffset.Equals(0))
				{
					this.senseListCtrl.ScrollList(this._sipOffset);
					this.senseListCtrl.Invalidate();
				}
				this._sipOffset = 0;
			}
		}
	}
}