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

	public static class NotifMessages
	{
		// Assign integers to messages.
		// Note that custom Window messages start at WM_USER = 0x400.
		public const int NOTIF_START = 0x0401;
	}
}
