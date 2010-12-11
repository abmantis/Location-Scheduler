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
            string title = currentForm.Text;
            currentForm.Text = "";
            System.Windows.Forms.DialogResult result = newForm.ShowDialog();
            currentForm.Text = title;
            currentForm.Show();
            currentForm.BringToFront();
            return result;
        }
    }
}
