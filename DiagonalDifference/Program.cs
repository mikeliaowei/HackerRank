using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagonalDifference
{
	class Program
	{
		public static int DiagonalDifference(List<List<int>> arr)
		{
			int result = 0;
			int index1 = arr.Count;
			int index2 = index1 > 0 ? arr[0].Count : 0;
			int test1 = 0, test2 = 0;
			if ( index1 > 0 && index2 > 0)
			{
				int j = index2;
				for(int i = 0; i < index1; i++)
				{
					test1 += arr[i][i];
					test2 += arr[j-1][i];
					j--;
				}
			}

			return result = Math.Abs(test1 - test2);
		}
		static void Main(string[] args)
		{
			List<List<int>> arr = new List<List<int>>();
			List<int> arr1 = new List<int>() { 11, 2, 4 };
			List<int> arr2 = new List<int>() { 4, 5, 6 };
			List<int> arr3 = new List<int>() { 10, 8, -12 };
			arr.Add(arr1);
			arr.Add(arr2);
			arr.Add(arr3);

			int result = DiagonalDifference(arr);

			System.Diagnostics.Debug.WriteLine(String.Join(" ", result));
		}
	}
}
