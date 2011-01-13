using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HelperLib
{	
	public class TasksLoader
	{
		private static String _FileToSaveTasks = Globals.GetCurrentPath() + "\\tasks.xml";

		public static void SaveTasksToFile(LSTasksConfig tasksConfig)
		{
			ObjectXMLSerializer<LSTasksConfig>.Save(tasksConfig, _FileToSaveTasks);
		}

		public static LSTasksConfig LoadTasksFromFile()
		{
			LSTasksConfig tasksConfig;
			try
			{
				tasksConfig = ObjectXMLSerializer<LSTasksConfig>.Load(_FileToSaveTasks);
			}
#pragma warning disable 0168
			catch (FileNotFoundException ex)
#pragma warning restore 0168
			{
				tasksConfig = new LSTasksConfig();
			}
			return tasksConfig;
		}
	}
}
