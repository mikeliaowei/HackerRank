using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rippling
{
	class Program
	{

		public static ListEmployees LoadJson()
		{
			using (StreamReader r = new StreamReader("C:\\Study\\HackerRank\\Rippling\\employee.json"))
			{
				string json = r.ReadToEnd();
				ListEmployees items = JsonConvert.DeserializeObject<ListEmployees>(json);
				return items;
			}
		}

		public class Employee
		{
			public string userId;
			public string jobTitleName;
			public string firstName; 
			public string lastName;
			public string preferredFullName;
			public string employeeCode;
			public string region;
			public string phoneNumber;
			public string emailAddress;
			public int salary;
		}

		public class ListEmployees
		{
			public List<Employee> Employees;
		}

		static void Main(string[] args)
		{
			ListEmployees employees = LoadJson();

			Console.WriteLine("Sum of salary = " + employees.Employees.Sum(e => e.salary));

			Console.WriteLine("Average of salary = " + employees.Employees.Average(e => e.salary));

			Console.WriteLine("Max of salary = " + employees.Employees.Max(e => e.salary));

			Console.WriteLine("Number of salary more than 100,000 = " + employees.Employees.Where(e => e.salary > 100000).Count());
		}
	}
}
