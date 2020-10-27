using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // Select vs SelectMany
            List<Employee> employees = new List<Employee>();
            Employee emp1 = new Employee { Name = "Deepak", Skills = new List<string> { "C", "C++", "Java" } };
            Employee emp2 = new Employee { Name = "Karan", Skills = new List<string> { "SQL Server", "C#", "ASP.NET" } };

            Employee emp3 = new Employee { Name = "Lalit", Skills = new List<string> { "C#", "ASP.NET MVC", "Windows Azure", "SQL Server" } };

            employees.Add(emp1);
            employees.Add(emp2);
            employees.Add(emp3);


            IEnumerable<List<string>> skills = employees.Select(s => s.Skills);

            foreach (List<string> skillsCollection in skills)
            {
                foreach (string skill in skillsCollection)
                {
                    Console.WriteLine(skill);
                }
            }

            Console.WriteLine("\nsecond variation =====\n\n");

            // Select many concatenates the results
            IEnumerable<string> skillsMany = employees.SelectMany(s => s.Skills);

            foreach (string skill2 in skillsMany)
            {
                Console.WriteLine(skill2);
            }


        }
    }
}
