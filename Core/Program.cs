using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using HelperLib;
using OpenNETCF.Threading;

namespace Core
{
	class Program
	{		
		static void Main(string[] args)
		{			
			Globals.WriteToDebugFile("\r\n--- Init ---");
			using (NamedMutex mutex = new NamedMutex(false, "Global\\LocationScheduler\\LSCore"))
			{
				if (!mutex.WaitOne(0, false))
				{
					Globals.WriteToDebugFile("Instance already running");
					return;
				}			

				GC.Collect();
				
				TaskMonitor tm = new TaskMonitor();
				Console.ReadLine();
				tm.Shutdown();			
			}

			Globals.WriteToDebugFile("--- Shutdown ---");
		}

		

	
	}
}
