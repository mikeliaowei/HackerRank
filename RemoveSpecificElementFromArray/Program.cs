using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveSpecificElementFromArray
{
	class Program
	{
		public static void RunTestcase1()
		{
			
		}

		public static class ArrayRemover
		{
			public static int[] RemoveByWhere(int[] array,  int item)
			{
				int[] newArray = array.Where(e => e != item).ToArray();
				return newArray;
			}

			public static int[] RemoveByFindAll(int[] array, int item)
			{
				int[] newArray = Array.FindAll(array, e => e != item).ToArray();
				return newArray;
			}

			public static int[] RemoveByExcept(int[] array, int item)
			{
				int[] newArray = array.Except(new int[] { item }).ToArray();
				return newArray;
			}

			public static int[] RemoveFirstOccurence(int[] array, int item)
			{
				int index = Array.IndexOf(array, item);
				array = array.Where((e, i) => i != index).ToArray();
				return array;
			}
		}

		static void Main(string[] args)
		{
			int[] array = { 1, 3, 4, 5, 4, 2 };
			int item = 4;

			Console.WriteLine("Where: " + String.Join(",", ArrayRemover.RemoveByWhere(array, item)));

			Console.WriteLine("FindAll: "+String.Join(",", ArrayRemover.RemoveByFindAll(array, item)));

			Console.WriteLine("Except: " + String.Join(",", ArrayRemover.RemoveByExcept(array, item)));

			Console.WriteLine("Remove first occurence: " + String.Join(",", ArrayRemover.RemoveFirstOccurence(array, item)));


		}
	}
}
