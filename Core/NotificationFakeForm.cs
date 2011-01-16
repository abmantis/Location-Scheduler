using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using christec.windowsce.forms;

namespace Core
{
	public partial class FakeForm : Form
	{
		public NotificationWithSoftKeys _softkeyNotification = null;
		public FakeForm()
		{
			InitializeComponent();
			this.Hide();
		}
	}
}