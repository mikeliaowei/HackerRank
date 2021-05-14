using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeTwoSortedArray
{
	class Program
	{
		public static void Merge(int[] nums1, int m, int[] nums2, int n)
		{
            int arrayIndex = nums1.Length - 1;
            int num1End = m - 1;
            int num2End = n - 1;

            while (num1End >= 0 && num2End >= 0)
            {
                if (nums2[num2End] >= nums1[num1End])
                {
                    nums1[arrayIndex] = nums2[num2End];
                    num2End--;
                }
                else
                {
                    nums1[arrayIndex] = nums1[num1End];
                    num1End--;
                }
                arrayIndex--;
            }

            while (num2End >= 0 && num1End < 0)
            {
                nums1[arrayIndex] = nums2[num2End];
                arrayIndex--;
                num2End--;
            }
        }

		static void Main(string[] args)
		{
			int[] numb1 = { 1, 2, 3, 7, 0, 0 };
			int[] numb2 = { 2, 5, 6 };
			Merge(numb1, 4, numb2, 3);

            Console.WriteLine(string.Join(",", numb1));


			int[] numb3 = { 1 };
			int[] numb4 = { 4, 3 };

			Merge(numb3, 1, numb4, 2);
            Console.WriteLine(string.Join(",", numb3));
        }
	}
}
