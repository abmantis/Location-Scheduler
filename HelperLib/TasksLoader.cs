using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HelperLib
{
	public static class TasksLoader
	{
		static String mFileToSaveTasks = Globals.GetCurrentPath() + "\\tasks.xml";


		public static void SaveTasksToFile(List<Task> taskArray)
		{
			ObjectXMLSerializer<List<Task>>.Save(taskArray, mFileToSaveTasks);
		}

		public static List<Task> LoadTasksFromFile()
		{
			List<Task> taskArray;
			try
			{

				taskArray = ObjectXMLSerializer<List<Task>>.Load(mFileToSaveTasks);
			}
#pragma warning disable 0168
			catch (FileNotFoundException ex)
#pragma warning restore 0168
			{
				taskArray = new List<Task>();
			}
			return taskArray;
		}
	}
}
