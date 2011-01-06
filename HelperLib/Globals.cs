using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Resources;
using System.IO;

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

		public static String GetCurrentPath()
		{
			return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
		}

		public static void WriteToDebugFile(string text)
		{
			StreamWriter SW;
			SW = File.AppendText("\\Storage Card\\debug.txt");
			SW.WriteLine(text);
			SW.Close();
		}
    }

	public static class NotifMessages
	{
		public const string NOTIF_STOP = "1";
		public const string NOTIF_SCAN = "2";
	}
}
