using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HelperLib
{
	[Serializable]
	public class LSTasksConfig
	{
		private List<Task> _taskList = new List<Task>();
		private bool _useCellPosition = false;		
		private int _updateRate = 2; //secs

		public LSTasksConfig()
		{
		}

		#region Properties
		public List<Task> TaskList
		{
			get { return _taskList; }
			set { _taskList = value; }
		}
		public bool UseCellLocation
		{
			get { return _useCellPosition; }
			set { _useCellPosition = value; }
		}
		public int UpdateRate
		{
			get { return _updateRate; }
			set { _updateRate = value; }
		}
		#endregion
	}
}
