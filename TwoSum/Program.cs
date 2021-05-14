using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoSum
{
	public class TwoSum
	{
		public static int[] TwoSumCalculate(int[] nums, int target)
		{
			int[] result = { };

			if (nums.Length == 0)
				return result;
			bool found = false;

			for(int i = 0; i< nums.Length; i++)
			{
				for(int j = 0; j < nums.Length; j++)
				{
					if (i != j && nums[i] + nums[j] == target)
					{
						found = true;
						result = new int[2]{ i, j};
						break;
					}
				}
				if (found)
				{
					break;
				}
			}

			return result;
		}

		static void Main(string[] args)
		{
			int[] nums = { 3, 2, 4 };
			int[] result = TwoSum.TwoSumCalculate(nums, 6);
			Console.WriteLine("Result: " + result.ToArray());
		}
	}

	[TestClass]
	public class TwoSumTests
	{
		[TestMethod]
		public void TwoSumUnitTest()
		{
			int[] nums = { 2, 7, 11, 15 };
			int[] result = TwoSum.TwoSumCalculate(nums, 9);
			Assert.AreEqual("[0,1]", result.ToString());

		}
	}
}
