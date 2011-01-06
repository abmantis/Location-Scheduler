using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using HelperLib;

namespace Core
{
	class Program
	{
		static void Main(string[] args)
		{
			TaskMonitor tm = new TaskMonitor();
			Console.ReadLine();
			tm.Shutdown();
		}

	
	}
}
