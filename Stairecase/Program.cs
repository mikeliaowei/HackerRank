using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stairecase
{
	class Program
	{
		static void staircase(int n)
		{
			for (int i = 1; i < n + 1; i++)
			{
				string stair = "";

				for (int j = 0; j < n - i; j++)
				{
					stair += ' ';
				}

				for (int k = 0; k < i; k++)
				{
					stair += "#";
				}
				Console.WriteLine(stair);
			}
		}

		static void Main(string[] args)
		{
			int n = Convert.ToInt32(Console.ReadLine());

			staircase(n);
		}
	}
}
