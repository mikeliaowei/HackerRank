using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigSum
{
	class Program
	{
		// Complete the aVeryBigSum function below.
		static long aVeryBigSum(long[] ar)
		{
			long result = 0;
			for(int i=0; i < ar.Length; i ++)
			{
				result += ar[i];
			}
			return result;
		}

		static void Main(string[] args)
		{

			long[] ar = new long[] { 1000000001, 1000000002, 1000000003, 1000000004, 1000000005 };

			long result = aVeryBigSum(ar);

			System.Diagnostics.Debug.WriteLine(String.Join(" ", result));
		}
	}
}
