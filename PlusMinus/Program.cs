using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus
{
	class Program
	{
		// Complete the plusMinus function below.
		static void plusMinus(int[] arr)
		{
			int positiveCal = 0;
			int negativeCal = 0;
			int zeroCal = 0;
			for(int i=0; i < arr.Length; i++)
			{
				if(arr[i] > 0)
				{
					positiveCal++;
				}else if (arr[i] == 0)
				{
					zeroCal++;
				}
				else
				{
					negativeCal++;
				}
			}

			double number1 = (double)positiveCal / arr.Length;
			double number2 = (double)negativeCal / arr.Length;
			double number3 = (double)zeroCal / arr.Length;

			Console.WriteLine(number1);
			Console.WriteLine(number2);
			Console.WriteLine(number3);

		}

		static void Main(string[] args)
		{
			int[] arr = new int[] { -4, 3, -9, 0, 4, 1 };

			plusMinus(arr);

			
		}
	}
}
