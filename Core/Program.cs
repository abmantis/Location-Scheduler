using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using HelperLib;
using OpenNETCF.Threading;
using System.Windows.Forms;

namespace Core
{
	class Program
	{		
		static void Main(string[] args)
		{			
			Globals.WriteToDebugFile("--- Init ---");
			using (NamedMutex mutex = new NamedMutex(false, "Global\\LocationScheduler\\LSCore"))
			{
				if (!mutex.WaitOne(0, false))
				{
					Globals.WriteToDebugFile("Instance already running");
					return;
				}			

				GC.Collect();

				// We have to force the cursor to the default one, otherwise
				// it will stay in the "wait" cursor". This is probably because
				// of the fact that the thread locks right at the startup...
				Cursor.Current = Cursors.Default; 
				TasksMonitor tm = new TasksMonitor();

			}

			Globals.WriteToDebugFile("--- Shutdown ---");
		}

		

	
	}
}
