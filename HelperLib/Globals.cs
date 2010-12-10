using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Resources;

namespace HelperLib
{
    public static class Globals
    {
        public static System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.Form newForm, System.Windows.Forms.Form currentForm)
        {
            string s = currentForm.Text;
            currentForm.Text = "";
            System.Windows.Forms.DialogResult r = newForm.ShowDialog();
            currentForm.Text = s;
            currentForm.Show();
            currentForm.BringToFront();
            return r;
        }
    }
}
