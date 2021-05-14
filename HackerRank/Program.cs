using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
	class Program
	{
		// Complete the compareTriplets function below.
		static List<int> compareTriplets(List<int> a, List<int> b)
		{
			List<int> results = new List<int>();
			int first = 0;
			int second = 0;
			for(int i =0; i< a.Count; i++)
			{
				if(a[i] > b[i])
				{
					first++;
				}

				if (b[i] > a[i])
				{
					second++;
				}
			}

			results.Add(first);
			results.Add(second);

			return results;
		}

		static void Main(string[] args)
		{

			List<int> a = new List<int>() { 17, 28, 30 };

			List<int> b = new List<int>() { 99, 16, 8 };

			List<int> result = compareTriplets(a, b);

			System.Diagnostics.Debug.WriteLine(String.Join(" ", result));
		}
	}
}
