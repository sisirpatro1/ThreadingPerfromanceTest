using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessWorker
{
	class Program
	{
		static int ALLOCATIONS = 10000;
		static int ALLOCATION_SIZE = 16384;
		static int FACTORIAL_OF = 100;

		static void Main(string[] args)
		{
			while (true)
			{
				AllocationTest();
			}
		}

		static void AllocationTest()
		{
			object[] objects = new object[ALLOCATIONS];

			for (int i = 0; i < ALLOCATIONS; i++)
			{
				objects[i] = new byte[ALLOCATION_SIZE];
				Console.WriteLine("Inside {0} thread allocating the memory of {1}", i, ALLOCATION_SIZE);
			}
			
			Thread.Sleep(100);
		}
	}
}
