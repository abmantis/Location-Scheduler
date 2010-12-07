using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;

namespace Location_Scheduler
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btAdd_Click_1(object sender, EventArgs e)
        {
            FormTaskEdit formTaskEdit = new FormTaskEdit();
            formTaskEdit.ShowDialog();
        }
    }
}