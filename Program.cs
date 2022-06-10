using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threaded_mem_allocation_test
{
	public class Worker
	{
		protected volatile bool shouldStop;
		protected Action action;
		public Worker(Action doSomething)
		{
			action = doSomething;
		}

		public void DoWork()
		{
			while (!shouldStop)
			{
				action();
			}
		}

		public void RequestStop()
		{
			shouldStop=true;
		}
	}

	class Program
	{
		static int ALLOCATIONS = 10000;
		static int ALLOCATION_SIZE = 16384;
		static int FACTORIAL_OF = 100;

		static void Main(string[] args)
		{
			//ProcessTest();
			ThreadTest();
		}

		static void ThreadTest()
		{
			List<Thread> threads = new List<Thread>();
			List<Worker> workers = new List<Worker>();
			int n = Environment.ProcessorCount;

			for (int i = 0; i < n; i++)
			{
				Worker worker = new Worker(AllocationTest);
				// Worker worker = new Worker(FactorialTest);
				Thread thread = new Thread(worker.DoWork);
				workers.Add(worker);
				threads.Add(thread);
				Console.WriteLine("Threads added = " + threads.Count);
			}

			threads.ForEach(t=>t.Start());

			Console.WriteLine("Press ENTER key to stop...");
			Console.ReadLine();

			workers.ForEach(w=>w.RequestStop());
			threads.ForEach(t=>t.Join());

			Console.WriteLine("Done");
		}

		static void ProcessTest()
		{
			List<Process> processes = new List<Process>();
			int n = Environment.ProcessorCount;

			for (int i = 0; i < n; i++)
			{
				Process p = Process.Start("ProcessWorker.exe");
				processes.Add(p);
			}

			Console.WriteLine("Press ENTER key to stop...");
			Console.ReadLine();

			processes.ForEach(p => p.Kill());

			Console.WriteLine("Done");
		}

		static void AllocationTest()
		{
			// Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
			object[] objects = new object[ALLOCATIONS];

			for (int i = 0; i < ALLOCATIONS; i++)
			{
				objects[i] = new byte[ALLOCATION_SIZE];
			}
		}

		static void FactorialTest()
		{
			decimal f = 1;

			for (int i = 0; i < FACTORIAL_OF; i++)
			{
				f = f * i;
			}
		}
	}
}


