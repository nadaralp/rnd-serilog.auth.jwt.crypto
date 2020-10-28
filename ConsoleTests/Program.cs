using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            #region LINQ

            // Select vs SelectMany
            //List<Employee> employees = new List<Employee>();
            //Employee emp1 = new Employee { Name = "Deepak", Skills = new List<string> { "C", "C++", "Java" } };
            //Employee emp2 = new Employee { Name = "Karan", Skills = new List<string> { "SQL Server", "C#", "ASP.NET" } };

            //Employee emp3 = new Employee { Name = "Lalit", Skills = new List<string> { "C#", "ASP.NET MVC", "Windows Azure", "SQL Server" } };

            //employees.Add(emp1);
            //employees.Add(emp2);
            //employees.Add(emp3);

            //IEnumerable<List<string>> skills = employees.Select(s => s.Skills);

            //foreach (List<string> skillsCollection in skills)
            //{
            //    foreach (string skill in skillsCollection)
            //    {
            //        Console.WriteLine(skill);
            //    }
            //}

            //Console.WriteLine("\nsecond variation =====\n\n");

            //// Select many concatenates the results
            //IEnumerable<string> skillsMany = employees.SelectMany(s => s.Skills);

            //foreach (string skill2 in skillsMany)
            //{
            //    Console.WriteLine(skill2);
            //}

            #endregion LINQ

            Stopwatch stopwatch = Stopwatch.StartNew();

            // Variation 1
            //await LongRunningTask1();
            //await LongRunningTask2();

            // Variation 2
            //var tasks = new List<Task>()
            //{
            //    LongRunningTask1(),
            //    LongRunningTask2()
            //};

            //await Task.WhenAll(tasks);

            // Variation 3
            //var task1 = Task.Run(() => LongRunningTask1());
            //var task2 = Task.Run(() => LongRunningTask2());

            //await Task.WhenAll(task1, task2);

            Console.WriteLine($"running main code on thread {Thread.CurrentThread.ManagedThreadId}");
            var task1 = LongRunningTask1();
            Console.WriteLine("do some more work while waiting");
            Console.WriteLine("do some more work while waiting 2");
            await Task.Delay(2000);
            Console.WriteLine("do some more work while waiting 3");

            await task1;

            stopwatch.Stop();
            Console.WriteLine($"ran in: {stopwatch.ElapsedMilliseconds / 1000d} seconds");
            Console.WriteLine($"finished on thread {Thread.CurrentThread.ManagedThreadId}");
        }

        private static async Task LongRunningTask1()
        {
            Console.WriteLine($"Starting {nameof(LongRunningTask1)} at thread: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(3000);
            Console.WriteLine("Did really heavy computations 1");
            Console.WriteLine($"finished {nameof(LongRunningTask1)} at thread: {Thread.CurrentThread.ManagedThreadId}");
        }

        private static async Task LongRunningTask2()
        {
            Console.WriteLine($"Starting {nameof(LongRunningTask2)} at thread: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(3000);
            Console.WriteLine("Did really heavy computations 2");
        }
    }
}