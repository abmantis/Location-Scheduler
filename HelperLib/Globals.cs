﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Resources;
using System.IO;
using StedySoft.SenseSDK;

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
#if DEBUG
			DateTime now = DateTime.Now;
			StreamWriter SW;
			SW = File.AppendText("\\Storage Card\\debug.txt");
			SW.WriteLine(now.ToString() + ": " + text);
			SW.Close();
#endif
		}

		
    }

	public static class TimeFuncs
	{
		public static int CompareTime(Time t1, Time t2)
		{
			t1.ConvertTo24HrFormat();
			t2.ConvertTo24HrFormat();

			if (t1.Hour < t2.Hour)
			{
				return -1;
			}
			else if (t1.Hour > t2.Hour)
			{
				return 1;
			}
			else
			{
				if (t1.Minute < t2.Minute)
				{
					return -1;
				}
				else if (t1.Minute > t2.Minute)
				{
					return 1;
				}
				else
				{
					return 0;
				}

			}
		}
		//public static int CompareTime(DateTime t1, Time t2)
		//{
		//    return CompareTime(new Time(t1), t2);
		//}
		//public static int CompareTime(Time t1, DateTime t2)
		//{
		//    return CompareTime(t1, new Time(t2));
		//}
		public static int HoursToMilliseconds(int hours)
		{
			return hours * 3600000; // hours * 60m * 60s * 1000
		}
		public static int MinutesToMilliseconds(int minutes)
		{
			return minutes * 60000; // minutes * 60s * 1000

		}
		public static int GetMsFromTo(Time from, Time to)
		{
			from.ConvertTo24HrFormat();
			to.ConvertTo24HrFormat();

			int msDif;
			int compare = CompareTime(from, to);
			if (compare <= 0)
			{
				msDif = HoursToMilliseconds(to.Hour - from.Hour);
			}
			else
			{
				msDif = HoursToMilliseconds(24 - from.Hour + to.Hour);
			}
			msDif += MinutesToMilliseconds(to.Minute - from.Minute);
			return msDif;
		}
	}

	public static class NotifMessages
	{
		public const string NOTIF_STOP = "1";
		public const string NOTIF_SCAN = "2";
	}
}
