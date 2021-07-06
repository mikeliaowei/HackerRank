using TreeStructure;
using LinkedListStructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RunningSumArray;
using System.Security.Cryptography;
using System.Diagnostics;
using static RunningSumArray.SamplesDelegate;

namespace LeedCodeTest
{
	public class Program
	{
		/*
		 * 
		 * Leetcode 273: Integer to English word
		 * Input: num = 123
		 * Output: "One Hundred Twenty Three"
		 * 
		 * 
		 */
		private static string[] LessThan20 = {"", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
								   "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"};
		private static string[] Tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
		private static string[] Thousands = { "", "Thousand", "Million", "Billion" };

		public static string NumberToWords(int num)
		{

			if (num == 0)
				return "Zero";

			int i = 0; // point to Thousands
			string res = "";
			while (num > 0)
			{
				if (num % 1000 != 0)
				{
					res = Helper(num % 1000) + Thousands[i] + " " + res;
				}

				num /= 1000;
				i++;
			}

			return res.Trim();
		}

		private static string Helper(int num)
		{
			if (num == 0)
				return "";
			else if (num < 20)
				return LessThan20[num] + " ";
			else if (num < 100)
				return Tens[num / 10] + " " + Helper(num % 10);
			else
				return LessThan20[num / 100] + " Hundred " + Helper(num % 100);
		}


		public static void convert_to_words(char[] num)
		{
			// Get number of digits
			// in given number
			int len = num.Length;

			// Base cases
			if (len == 0)
			{
				Console.WriteLine("empty string");
				return;
			}
			if (len > 4)
			{
				Console.WriteLine("Length more than "
								  + "4 is not supported");
				return;
			}

			/* The first string is not used,
			   it is to make array indexing simple */
			string[] single_digits = new string[] {
			"zero", "one", "two",   "three", "four",
			"five", "six", "seven", "eight", "nine"
		};

			/* The first string is not used,
			   it is to make array indexing simple */
			string[] two_digits = new string[] {
			"",          "ten",      "eleven",  "twelve",
			"thirteen",  "fourteen", "fifteen", "sixteen",
			"seventeen", "eighteen", "nineteen"
		};

			/* The first two string are not used,
			   they are to make array indexing simple*/
			string[] tens_multiple = new string[] {
			"",      "",      "twenty",  "thirty", "forty",
			"fifty", "sixty", "seventy", "eighty", "ninety"
		};

			string[] tens_power
				= new string[] { "hundred", "thousand" };

			/* Used for debugging purpose only */
			Console.Write((new string(num)) + ": ");

			/* For single digit number */
			if (len == 1)
			{
				Console.WriteLine(single_digits[num[0] - '0']);
				return;
			}

			/* Iterate while num
				is not '\0' */
			int x = 0;
			while (x < num.Length)
			{

				/* Code path for first 2 digits */
				if (len >= 3)
				{
					if (num[x] - '0' != 0)
					{
						Console.Write(
							single_digits[num[x] - '0'] + " ");
						Console.Write(tens_power[len - 3]
									  + " ");

						// here len can be 3 or 4
					}
					--len;
				}

				/* Code path for last 2 digits */
				else
				{
					/* Need to explicitly handle
					10-19. Sum of the two digits
					is used as index of "two_digits"
					array of strings */
					if (num[x] - '0' == 1)
					{
						int sum = num[x] - '0' + num[x] - '0';
						Console.WriteLine(two_digits[sum]);
						return;
					}

					/* Need to explicitely handle 20 */
					else if (num[x] - '0' == 2
							 && num[x + 1] - '0' == 0)
					{
						Console.WriteLine("twenty");
						return;
					}

					/* Rest of the two digit
					numbers i.e., 21 to 99 */
					else
					{
						int i = (num[x] - '0');
						if (i > 0)
							Console.Write(tens_multiple[i]
										  + " ");
						else
							Console.Write("");
						++x;
						if (num[x] - '0' != 0)
							Console.WriteLine(
								single_digits[num[x] - '0']);
					}
				}
				++x;
			}
		}

		/*
		 * Leetcode 41: First Missing Positive
		 * Given an unsorted integer array nums, find the smallest missing positive integer.
		 * You must implement an algorithm that runs in O(n) time and uses constant extra space.
		 * Input: nums = [3,4,-1,1]
		 * Output: 2
		 */
		public static int FirstMissingPositive(int[] A)
		{

			int len = A.Length;

			for (int i = 0; i < len; i++)
			{
				if (A[i] < 0)
					A[i] = 0;
			}


			for (int i = 0; i < len; i++)
			{
				int val = Math.Abs(A[i]);
				if (val >= 1 && val <= len)
				{

					if (A[val - 1] > 0)
						A[val - 1] *= -1;
					else if (A[val - 1] == 0)
						A[val - 1] = -1 * (len + 1);

				}
			}

			for (int i = 1; i < len + 1; i++)
			{
				if (A[i - 1] >= 0)
					return i;
			}

			return len + 1;
		}

		/* Leetcode 70 
		 * You are climbing a staircase. It takes n steps to reach the top.
		 *	Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?
		 * 
		 */
		public static int ClimbStairs(int n)
		{
			if (n <= 2)
				return n;

			int[] res = new int[n + 1];

			res[1] = 1;
			res[2] = 2;

			for (int i = 3; i <= n; i++)
				res[i] = res[i - 1] + res[i - 2];

			return res[n];
		}

		/*
		 * 67 Add Binary
		 * Given two binary strings a and b, return their sum as a binary string.
		 */
		public static string AddBinary(string a, string b)
		{
			int i = a.Length - 1;
			int j = b.Length - 1;
			var result = new StringBuilder();

			int carryover = 0;

			while (i >= 0 || j >= 0 || carryover == 1)
			{
				int achar = 0;
				int bchar = 0;

				if (i >= 0)
					achar = a[i] - '0';
				if (j >= 0)
					bchar = b[j] - '0';

				int bitSum = achar + bchar + carryover;
				if (bitSum >= 2)
				{
					carryover = 1;
					result.Insert(0, bitSum - 2);
				}
				else
				{
					carryover = 0;
					result.Insert(0, bitSum);
				}

				i--;
				j--;
			}

			return result.ToString();
		}


		/*
		 * Leetcode 69: Sqrt(x)
		 */
		public static int MySqrt(int x)
		{
			long result = 0;
			long dumy = 0;
			bool found = false;

			while (!found)
			{
				dumy = result;
				result++;

				if (result * result == x)
				{
					dumy = result;
					break;
				}

				if (result * result > x)
				{
					break;
				}
			}

			return (int)dumy;
		}

		

		//202. Happy Number
		/*  
		 *  A happy number is a number defined by the following process:
			Starting with any positive integer, replace the number by the sum of the squares of its digits.
			Repeat the process until the number equals 1 (where it will stay), or it loops endlessly in a cycle which does not include 1.
			Those numbers for which this process ends in 1 are happy.
			Return true if n is a happy number, and false if not.
		*/
		public static bool IsHappyNumber(int n)
		{
			bool isHappy = false;
			string nstr = n.ToString();
			int limit = 10000;
			int length = nstr.Length;

			while (!isHappy && limit > 0)
			{

				int j = 0;
				int sum = 0;
				while (j < length)
				{
					sum += (int)Math.Pow(Int32.Parse(nstr[j].ToString()), 2);
					j++;
				}

				if (sum == 1)
				{
					isHappy = true;
					break;
				}
				else
				{
					nstr = sum.ToString();
					length = nstr.Length;
				}

				limit--;
			}


			return isHappy;
		}

		/* Leetcode 136: Single Number
		 * Given a non-empty array of integers nums, every element appears twice except for one. Find that single one.
		 *  
		 */
		public static int SingleNumber(int[] nums)
		{
			Hashtable ht = new Hashtable();
			int length = nums.Length;


			if (length == 1) return nums[0];

			if (length == 2)
			{
				if (nums[0] != nums[1])
					return nums[0];
				else
					return 0;
			}

			for (int i = 0; i < length; i++)
			{
				if (ht.Count == 0)
				{
					ht.Add(nums[i], 1);
				}
				else
				{
					if (ht.ContainsKey(nums[i]))
					{
						ht[nums[i]] = (int)ht[nums[i]] + 1;
					}
					else
					{
						ht.Add(nums[i], 1);
					}
				}
			}


			return ht.Keys.OfType<int>().FirstOrDefault(a => (int)ht[a] == 1);
		}

		/* Leedcode 118, Pascal's triangle
		 * 
		 * 
		 */
		public static IList<IList<int>> GeneratePascalTriangle(int numRows)
		{
			var res = new List<IList<int>>();
			int start = numRows;
			List<int> dumy = new List<int>();

			if (start == 1)
			{
				res.Add(new List<int> { 1 });
				return res;
			}
			else if (start == 2)
			{
				res.Add(new List<int> { 1, 1 });
				return res;
			}

			int i = 0;
			while (i < start)
			{
				if (i == 0)
				{
					res.Add(new List<int> { 1 });
				}
				else if (i == 1)
				{
					res.Add(new List<int> { 1, 1 });
				}
				else
				{
					int j = 0;
					dumy = new List<int>();
					dumy.Add(1);
					while (j < i && j + 1 < i && i - 1 <= res.Count)
					{
						dumy.Add(res[i - 1][j] + res[i - 1][j + 1]);
						j++;
					}
					dumy.Add(1);

					res.Add(dumy);
				}

				i++;
			}

			return res;
		}

		public static int IntegerBreak(int n)
		{
			if (n < 2 || n > 58)
			{
				Console.WriteLine("Invalid number!");
				return n;
			}

			if (n < 5)
			{
				if (n == 2)
				{
					return 1;
				}

				if (n == 3)
				{
					return 2;
				}

				if (n == 4)
				{
					return 4;
				}

			}


			int rest = n;
			int pw = 0;

			while (rest - 3 >= 0 && rest - 3 != 1)
			{
				rest = rest - 3;
				pw++;
			}

			if (rest > 0)
			{
				return (int)Math.Pow(3, pw) * rest;
			}

			return (int)Math.Pow(3, pw);

		}

		public static int LengthOfLastWord(string s)
		{
			string[] split = s.Split(' ');
			int length = split.Length;

			int j = length;
			while (j > 0)
			{
				if (j - 1 >= 0 && split[j - 1] == "")
				{
					j--;
				}
				else
				{
					break;
				}
			}

			return j - 1 >= 0 ? split[j - 1].Length : 0;
		}


		public static int[] PlusOne(int[] digits)
		{
			int carry = 0;
			int length = digits.Length - 1;
			Stack<int> s = new Stack<int>();

			while (length >= 0)
			{
				if (length == digits.Length - 1)
				{
					s.Push((digits[length] + 1) % 10 + carry);
					carry = digits[length] + 1 >= 10 ? 1 : 0;
				}
				else
				{
					s.Push((digits[length] + carry) % 10);
					carry = digits[length] + carry >= 10 ? 1 : 0;
				}

				if (length == 0 && carry > 0)
				{
					s.Push(carry);
				}

				length--;
			}

			int[] result = new int[s.Count];
			int i = 0;
			while (s.Count != 0)
			{
				result[i] = s.Pop();
				i++;
			}

			return result;
		}


		public static IList<IList<int>> LargeGroupPositions(string s)
		{
			/*
			Dictionary<string, int> dic = new Dictionary<string, int>();
			StringBuilder sb = new StringBuilder();
			string l = "";
			int starti = 0;
			var res = new List<IList<int>>();

			for (int i = 0; i < s.Length; i++)
			{
				int j = starti;
				i = starti;
				while (j + 1 < s.Length)
				{
					if (j == i)
					{
						sb.Append(s[j]);
					}

					if (s[j] != s[j + 1])
					{
						starti++;
						break;
					}
					else
					{
						starti++;
						sb.Append(s[j]);
					}
					j++;
				}

				if (!dic.ContainsKey(sb.ToString()) && !string.IsNullOrEmpty(sb.ToString()))
				{
					dic.Add(sb.ToString(), i);
					if (sb.ToString().Length > l.Length)
					{
						l = sb.ToString();
					}
				}
				sb = new StringBuilder();
			}

			if (dic.Count == 0) return res;

			int value = dic[l];

			IList<int> list = new int[] { value, value + l.Length - 1 };
			return new List<IList<int>>() { list };
			
			*/

			int start = 0, count = 1;
			var res = new List<IList<int>>();

			for (var i = 0; i < s.Length - 1; i++)
			{
				if (s[i] == s[i + 1]) count++;
				else
				{
					if (count >= 3)
						res.Add(new List<int> { start, i });
					start = i + 1;
					count = 1;
				}
			}

			if (count >= 3)
				res.Add(new List<int> { start, s.Length - 1 });

			return res;
		}

		/*
		 * Leetcode 33: Search in Rotated Sorted Array
		 * Input: nums = [4,5,6,7,0,1,2], target = 0
		 * Output: 4
		 */
		public static int Search(int[] nums, int target)
		{

			int l = 0, r = nums.Length - 1;

			while (l <= r)
			{
				int mid = (l + r) / 2;
				if (target == nums[mid])
					return mid;

				if (nums[l] <= nums[mid])
				{
					if (target > nums[mid] || target < nums[l])
						l = mid + 1;
					else
						r = mid - 1;
				}
				else
				{
					if (target < nums[mid] || target > nums[r])
						r = mid - 1;
					else
						l = mid + 1;
				}
					
			}

			return -1;

		}

		/*
		 * Leetcode 34: Find First and Last Position of Element in Sorted Array
		 * Given an array of integers nums sorted in ascending order, find the starting and ending position of a given target value.
		 * Input: nums = [5,7,7,8,8,10], target = 8
		 * Output: [3,4]
		 */
		public int[] SearchRange(int[] nums, int target)
		{

			int left = binSearch(nums, target, true);
			int right = binSearch(nums, target, false);

			return new int[] { left, right };

		}

		//leftBias=[true/false], if false, res is rightBiased
		int binSearch(int[] nums, int target, bool leftBias)
		{

			int l = 0, r = nums.Length - 1, i = -1;

			while (l <= r)
			{
				int m = (l + r) / 2;

				if (target > nums[m])
					l = m + 1;
				else if (target < nums[m])
					r = m - 1;
				else
				{
					i = m;
					if (leftBias)
						r = m - 1;
					else
						l = m + 1;
				}

			}

			return i;
		}

		public static int SearchInsert(int[] nums, int target)
		{
			int inserti = 0;
			for (int i = 0; i < nums.Length; i++)
			{
				inserti = i;
				if (nums[i] >= target)
				{
					break;
				}

				if (nums[i] >= target && i == nums.Length - 1)
				{
					inserti++;
				}
			}

			return inserti;
		}

		public static int RemoveElement(int[] nums, int val)
		{
			int len = 0;
			for (int i = 0; i < nums.Length; i++)
			{
				if (nums[i] == val && i + len < nums.Length)
				{
					int j = i;
					while (j + 1 < nums.Length)
					{
						nums[j] = nums[j + 1];
						j++;
					};
					i = i - 1;
					len++;
				}
			}

			return nums.Length - len;
		}

		public static int RemoveDuplicates(int[] nums)
		{
			HashSet<int> uniqArray = new HashSet<int>();

			for (int i = 0; i < nums.Length; i++)
			{
				uniqArray.Add(nums[i]);
			}

			int j = 0;
			foreach (int item in uniqArray)
			{
				nums[j] = item;
				j++;
			}

			return uniqArray.Count;
		}

		public static bool ParenthesesIsValid(string s)
		{
			Stack<char> buffer = new Stack<char>();

			bool isValid = true;

			for (int i = 0; i < s.Length; i++)
			{
				switch (s[i])
				{
					case '(':
					case '{':
					case '[':
						// if opening bracket, just add it to the stack
						buffer.Push(s[i]);
						break;

					case ')':
						// if the stack has elements, pop the top; if matches the closing continue to next char; if doesn't match, it's not - the parenthesis are not matching
						isValid = (buffer.Count > 0 && buffer.Pop() == '(');
						break;

					case '}':
						// if the stack has elements, pop the top; if matches the closing continue to next char; if doesn't match, it's not - the parenthesis are not matching
						isValid = (buffer.Count > 0 && buffer.Pop() == '{');
						break;

					case ']':
						// if the stack has elements, pop the top; if matches the closing continue to next char; if doesn't match, it's not - the parenthesis are not matching
						isValid = (buffer.Count > 0 && buffer.Pop() == '[');
						break;

					default:
						break;
				}

				if (!isValid) break; // if found non-matching brackets already, just leave
			}

			return isValid && buffer.Count == 0; // all brackets were found and there are no pending brackets in the stack
		}


		public static string LongestCommonPrefix(string[] strs)
		{
			List<string> orderStrs = strs.OrderByDescending(c => c.Length).ToList();
			int maxLength = 0;
			if (orderStrs.Count > 0) maxLength = orderStrs[0].Length;
			Dictionary<int, char> dic = new Dictionary<int, char>(maxLength);


			for (int i = 0; i < orderStrs.Count; i++)
			{
				int j = 0;
				if (i == 0)
				{
					while (!dic.ContainsKey(j) && j < orderStrs[i].Length)
					{
						dic[j] = orderStrs[i][j];
						j++;
					}
				}
				else
				{
					bool foundDif = false;
					while (j < maxLength)
					{
						if (j < orderStrs[i].Length && dic.ContainsKey(j) && dic[j] != orderStrs[i][j] && !foundDif)
						{
							foundDif = true;
						}

						if (foundDif && dic.ContainsKey(j) || j >= orderStrs[i].Length)
						{
							dic.Remove(j);
						}

						j++;
					}

					if (dic.Count == 1 && orderStrs[i].Length == 0) dic.Remove(0);
				}

			}

			return string.Join("", dic.Select(kv => kv.Value).ToArray());
		}

		public static int HammingWeight(uint n)
		{
			int res = 0;

			while (n != 0)
			{
				uint cur = n;

				if ((cur & 1) == 1)
					res++;

				n = n >> 1;
			}

			return res;
		}

		public static string RestoreString(string s, int[] indices)
		{
			Dictionary<int, char> dic = new Dictionary<int, char>(indices.Length);

			for (int i = 0; i < indices.Length; i++)
			{
				dic[indices[i]] = s[i];
			}

			StringBuilder words = new StringBuilder();

			foreach (KeyValuePair<int, char> entry in dic.OrderBy(c => c.Key))
			{
				words.Append(entry.Value);
			}

			return words.ToString();
		}

		public static int LengthOfLongestSubstring(string s)
		{
			Dictionary<char, int> prevAppearance = new Dictionary<char, int>();
			int maxSubstringLength = 0;
			int prevNonRepeatingSubstringStartIndex = 0;
			for (int i = 0; i < s.Length; i++)
			{
				if (prevAppearance.Keys.Contains(s[i]))
				{
					prevNonRepeatingSubstringStartIndex =
						Math.Max(prevAppearance[s[i]] + 1, prevNonRepeatingSubstringStartIndex);
				}
				maxSubstringLength =
					Math.Max(maxSubstringLength, i - prevNonRepeatingSubstringStartIndex + 1);
				prevAppearance[s[i]] = i;
			}

			return maxSubstringLength;
		}

		public static IList<IList<int>> FourSum(int[] nums, int target)
		{

			Array.Sort(nums);

			List<IList<int>> res = new List<IList<int>>();

			for (int i = 0; i < nums.Length - 1; i++)
			{

				List<IList<int>> threeSum = ThreeSum(nums, target - nums[i], i + 1).ToList();

				foreach(List<int> lst in threeSum)
				{
					lst.Add(nums[i]);
					lst.Sort();
					res.Add(lst);
				}

			}

			List<IList<int>>  dist = 
				res.Select(o =>
				{
					var t = o.OrderBy(x => x).Select(i => i.ToString());
					return new { Key = string.Join("", t), List = o };
				})
				.GroupBy(o => o.Key)
				.Select(o => o.FirstOrDefault())
				.Select(o => o.List)
				.ToList();

			return dist;

		}


		public static IList<IList<int>> ThreeSum(int[] nums, int target, int starti)
		{

			List<IList<int>> res = new List<IList<int>>();

			Array.Sort(nums);

			for (int i = starti; i < nums.Length; i++)
			{

				int l = i + 1, r = nums.Length - 1;

				while (l < r)
				{
					int threesum = nums[i] + nums[l] + nums[r];

					if (threesum > target)
					{
						r--;
					}
					else if (threesum < target)
					{
						l++;
					}
					else
					{

						res.Add(new List<int>() { nums[i], nums[l], nums[r] });

						l++;

						while (l < r && nums[l] == nums[l - 1])
							l++;
					}
				}
			}

			return res;

		}

		/*
		 * Leetcode 15: three sum
		 * Input: nums = [-1,0,1,2,-1,-4]
		 * Output: [[-1,-1,2],[-1,0,1]]
		 */
		public static IList<IList<int>> ThreeSum(int[] nums)
		{

			Array.Sort(nums);  //O(nlogn)

			List<IList<int>> res = new List<IList<int>>();

			for (int i = 0; i < nums.Length; i++)
			{

				if (i > 0 && nums[i] == nums[i - 1])
					continue;

				int l = i + 1, r = nums.Length - 1;

				while (l < r)
				{

					int threesum = nums[i] + nums[l] + nums[r];

					if (threesum > 0)
						r--;
					else if (threesum < 0)
						l++;
					else
					{
						res.Add(new List<int> { nums[i], nums[l], nums[r] });
						l++;
						while (nums[l] == nums[l - 1] && l < r)
							l++;
					}


				}

			}

			return res;

		}


		/*Leetcode 16: 3 Sum Closest
		 * 
		 * Given an array nums of n integers and an integer target, 
		 * find three integers in nums such that the sum is closest to target. Return the sum of the three integers. You may assume that each input would have exactly one solution.
		 * Input: nums = [-1,2,1,-4], target = 1
		 * Output: 2
		 * Explanation: The sum that is closest to the target is 2. (-1 + 2 + 1 = 2).
		 * Solution: The two pointers pattern requires the array to be sorted, so we do that first. As our BCR is \mathcal{O}(n^2)O(n2 ), 
		 * the sort operation would not change the overall time complexity.
		 */
		public int ThreeSumClosest(int[] nums, int target)
		{

			Array.Sort(nums);

			int len = nums.Length;

			int diff = Int32.MaxValue;

			for (int i = 0; i < len && diff != 0; i++)
			{
				int left = i + 1, right = len - 1;

				while (left < right)
				{

					int sum = nums[i] + nums[left] + nums[right];

					if (Math.Abs(target - sum) < Math.Abs(diff))
					{
						diff = target - sum;
					}

					if (sum < target)
						++left;
					else
						--right;

				}

			}

			return target - diff;

		}

		/*
		 * Leetcode 75: sort colors
		 * 
		 * Input: nums = [2,0,2,1,1,0]
		 * Output: [0,0,1,1,2,2]
		 */
		public void SortColors(int[] nums)
		{

			int l = 0, r = nums.Length - 1, i = 0;

			while (i <= r)
			{

				if (nums[i] == 0)
				{
					swap(l, i);
					l++;
				}
				else if (nums[i] == 2)
				{
					swap(i, r);
					r--;
					i--;
				}

				i++;
			}

			void swap(int j, int k)
			{
				int temp = nums[j];
				nums[j] = nums[k];
				nums[k] = temp;
			}

		}

		public static long countDecreasingSubarrays(int[] arr)
		{

			int len = arr.Length;

			int i = 0, j = len - 1;

			HashSet<int> hs = new HashSet<int>();
			int sum1 = 0, sum2 = 0;

			for(int start = 0; start < len; start++)
			{
				i = start;
				j = len - 1 - i;

				while (i < len && j >= 0)
				{
					hs.Add(arr[i]);

					if (i + 1 < len && arr[i] > arr[i + 1])
					{
						sum1 += arr[i] + arr[i + 1];
						hs.Add(sum1);
					}
					else
					{
						sum1 = 0;
					}


					if (j - 1 >= 0 && arr[j - 1] > arr[j])
					{
						sum2 += arr[j - 1] + arr[j];
						hs.Add(sum2);
					}
					else
					{
						sum2 = 0;
					}

					j--;
					i++;
				}
			}


			return hs.Count;

		}

		/*Leetcode 5: Longest Palindromic Substring
		 * Given a string s, return the longest palindromic substring in s.
		 * Input: s = "babad"
		 * Output: "bab"
		 * Note: "aba" is also a valid answer.
		 */
		public static string LongestPalindrome(string s)
		{
			int length = s.Length;

			int istart = 0, maxlen = 1;

			for (int icenter = 0; icenter < length - 1; icenter++)
			{
				int i = icenter, j = icenter;

				while (j + 1 < length && s[i] == s[j + 1])
				{
					j++;
				}

				icenter = j;

				while (i > 0 && j + 1 < length && s[i - 1] == s[j + 1])
				{
					i--;
					j++;
				}

				if (j - i + 1 > maxlen)
				{
					istart = i;
					maxlen = j - i + 1;
				}
			}

			return s.Substring(istart, maxlen);
		}

		public static string LongestPalindrome2(string s)
		{
			int length = s.Length, maxLen = 0;

			string res = "";

			for (int i = 0; i < length; i++)
			{
				//even length
				int left = i, right = i + 1;
				while (left >= 0 && right < length && s[left] == s[right])
				{
					if (right - left + 1 > maxLen)
					{
						res = s.Substring(left, right - left + 1);
						maxLen = right - left + 1;
					}

					left--;
					right++;
				}

				//odd length
				left = i;
				right = i;
				while (left >= 0 && right < length && s[left] == s[right])
				{
					if (right - left + 1 > maxLen)
					{
						res = s.Substring(left, right - left + 1);
						maxLen = right - left + 1;
					}

					left--;
					right++;
				}


			}

			return res;
		}

		/*
		 * Leetcode 647: Palindromic Substrings
		 * Given a string s, return the number of palindromic substrings in it.
		 * Input: s = "abc"
		 * Output: 3
		 * Explanation: Three palindromic strings: "a", "b", "c".
		 */
		public int CountPalindromeSubstrings(string s)
		{

			int len = s.Length;
			int right = 0, left = 0, res = 0;

			for (int i = 0; i < len; i++)
			{

				//odd item
				left = i;
				right = i;
				while (left >= 0 && right < len && s[left] == s[right])
				{
					res++;
					left--;
					right++;
				}

				//even item
				left = i;
				right = i + 1;
				while (left >= 0 && right < len && s[left] == s[right])
				{
					res++;
					left--;
					right++;
				}

			}

			return res;
		}

		/*
		 * Leedcod 119
		 * Pascal's triangle
		 * Given an integer rowIndex, return the rowIndexth (0-indexed) row of the Pascal's triangle.
		 * In Pascal's triangle, each number is the sum of the two numbers directly above it as shown:
		 * 
		 */
		public IList<int> GetRow(int rowIndex)
		{
			List<int>[] results = new List<int>[rowIndex + 1];

			results[0] = new List<int>();
			results[0].Add(1);

			if (rowIndex != 0)
			{
				for (int row = 1; row <= rowIndex; row++)
				{
					results[row] = new List<int>();

					results[row].Add(1);

					if (row > 1)
						for (int i = 1; i <= row - 1; i++)
							results[row].Add(results[row - 1][i - 1] + results[row - 1][i]);

					results[row].Add(1);
				}
			}

			return results[rowIndex];
		}

		/*
		 * Leetcode 442: find all duplicates in an array
		 * 
		 * 
		 */
		public IList<int> FindDuplicates(int[] nums)
		{
			int len = nums.Length;

			Array.Sort(nums);

			HashSet<int> res = new HashSet<int>();

			int i = 0;

			while (i + 1 < len)
			{

				if (nums[i] == nums[i + 1])
				{
					res.Add(nums[i]);
				}

				i++;
			}


			return res.ToList();
		}

		/*
		 * Leedcode 509
		 * Fibonacci Number
		 * 
		 * F(0) = 0, F(1) = 1
		 * F(n) = F(n - 1) + F(n - 2), for n > 1.
		 */

		public int Fibonacci(int n)
		{
			if (n <= 0)
			{
				return 0;
			}

			int a = 0, b = 1, c = 1;
			for (int i = 1; i < n; i++)
			{
				c = a + b;
				a = b;
				b = c;
			}

			return c;

		}

		public static int combinatonsOfTwoNumber(int[] nums, int target)
		{
			int res = 0;
			LinkedList<int> comb = new LinkedList<int>();

			backtrackcombinatonsOfTwoNumber(1, nums, ref res, comb, target);


			return res;
		}

		private static void backtrackcombinatonsOfTwoNumber(int index, int[] nums, ref int results, LinkedList<int> permutation, int target)
		{
			string str = string.Join("", permutation.ToList());
			if (!string.IsNullOrEmpty(str)&& Int16.Parse(str) > target)
			{
				results = Int16.Parse(str);
				return;
			}

			//make a choice
			permutation.AddLast(index % 2 != 0 ? nums[0] : nums[1]);

			backtrackcombinatonsOfTwoNumber(index+1, nums, ref results, permutation, target);

			//undo the choice
			permutation.RemoveLast();

		}


		public static int MaxProfitSum(int[] prices)
		{
			if (prices == null ||
				prices.Length == 0 ||
				prices.Length == 1)
				return 0;

			int j = 0, g = 0;
			for (int i = 1; i < prices.Length; i++)
			{
				if (prices[i] > prices[j])
				{
					g += prices[i] - prices[j];
				}

				j += 1;
			}

			return g;

		}

		/*Leetcode 1027 Longest Arithmetic Subsequence
		 * Given an array nums of integers, return the length of the longest arithmetic subsequence in nums.
		 * Input: nums = [20,1,15,3,10,5,8]
			Output: 4
			Explanation: 
			The longest arithmetic subsequence is [20,15,10,5].
		 * 
		 */
		public static int longestArithmeticSeqLength(int[] A)
		{
			Dictionary<int, int>[] differences = new Dictionary<int, int>[A.Length];

			int max = 1;

			for (int i = 0; i < A.Length; i++)
			{
				differences[i] = new Dictionary<int, int>();

				for (int j = 0; j < i; j++)
				{
					int diff = A[i] - A[j];

					int amount = 1;

					if (differences[j].ContainsKey(diff))
					{
						amount = differences[j][diff] + 1;
					}

					if (amount > max)
						max = amount;

					if (differences[i].ContainsKey(diff))
					{
						differences[i][diff] = amount;
					}
					else
					{
						differences[i].Add(diff, amount);
					}

				}
			}


			return max + 1;
		}

		/*
		 * Leetcode 125: Valid Palindrome
		 * Given a string s, determine if it is a palindrome, considering only alphanumeric characters and ignoring cases.
		 * Input: s = "A man, a plan, a canal: Panama"
		 * Output: true
		 * Explanation: "amanaplanacanalpanama" is a palindrome.
		 */
		public static bool isPalindrome(string s)
		{

			if (s == null || s.Length < 2) return true;

			int i = 0, j = s.Length - 1;

			while (i < j)
			{

				while (i < j && !(Char.IsLetterOrDigit(s[i])))
					i++;

				while (i < j && !(Char.IsLetterOrDigit(s[j])))
					j--;

				char ch1 = char.ToUpper(s[i]);
				char ch2 = char.ToUpper(s[j]);

				if (ch1.CompareTo(ch2) != 0) return false;

				i++;
				j--;
			}

			return true;
		}

		public static bool checkPalindrome(string inputString)
		{
			StringBuilder reverse = new StringBuilder();
			int length = inputString.Length;

			int i = length - 1;
			while(i >= 0)
			{
				reverse.Append(inputString[i]);
				i--;
			}

			return inputString.Equals(reverse.ToString());
		}

		public static int firstDuplicate(int[] a)
		{
			Dictionary<int, int> dic = new Dictionary<int, int>();
			int found = 0;

			for(int i = 0; i < a.Length; i++)
			{
				if (!dic.ContainsKey(a[i]))
				{
					dic.Add(a[i], 1);
				}
				else
				{
					found = a[i];
					break;
				}
			}

			return found;
		}

		public static char firstNotRepeatingCharacter(string s)
		{
			char res = '_';
			Dictionary<char, int> dic = new Dictionary<char, int>();

			if (s.Length == 1) return res;

			for(int i = 0; i < s.Length; i++)
			{
				if (!dic.ContainsKey(s[i]))
				{
					dic[s[i]] = 1;
				}
				else
				{
					dic[s[i]] += 1;
				}
			}

			foreach(KeyValuePair<char, int> item in dic)
			{
				if(item.Value == 1)
				{
					res = item.Key;
					break;
				}
			}		

			return res;
		}

		public static int[] RunningSum(int[] nums)
		{
			int[] result = new int[nums.Length];

			for (int i = 0; i < nums.Length; i++)
			{
				int total = 0;
				for (int j = 0; j <= i; j++)
				{
					total += nums[j];
				}
				result[i] = total;
			}

			return result;
		}

		public static string DefangIPaddr(string address)
		{
			StringBuilder newIpAddress = new StringBuilder();
			foreach (char c in address)
			{
				if (c.Equals('.'))
				{
					newIpAddress.Append("[.]");
				}
				else
				{
					newIpAddress.Append(c);
				}
			}

			return newIpAddress.ToString();
		}

		public static int ReverseInteger(int x)
		{
			if (x == 0) return x;

			string intStr = x.ToString();
			string newIntStr = "";
			int length = intStr.Length;
			Stack stack = new Stack();

			if (x < 0)
			{
				newIntStr = "-";
				intStr = intStr.Substring(1);
				length -= 1;
			}

			foreach (char c in intStr)
			{
				stack.Push(c);
			}
			while (length > 0)
			{
				newIntStr += stack.Peek();
				stack.Pop();
				length--;
			}


			int reverseInt = 0;

			Int32.TryParse(newIntStr, out reverseInt);

			return reverseInt;
		}

		public static bool IsPalindrome(int x)
		{
			if (x < 0)
			{
				return false;
			}

			if (x == 0) return true;

			string newIntStr = "";
			string intStr = x.ToString();

			for (int i = intStr.Length - 1; i >= 0; i--)
			{
				newIntStr += intStr[i];
			}


			if (intStr.Equals(newIntStr))
			{
				return true;
			}

			return false;

		}

		/*
		 * Leetcode 12. Integer to roman
		 * num = 3, output = III
		 * num = 4, output = IV
		 * num = 1994, output MCMXCIV
		 * M = 1000, CM = 900, XC = 90 and IV = 4.
		 */
		private static int[] values = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
		private static String[] symbols = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

		public string IntToRoman(int num)
		{

			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < values.Length && num > 0; i++)
			{

				while (values[i] <= num)
				{

					num -= values[i];

					sb.Append(symbols[i]);
				}

			}

			return sb.ToString();
		}

		/*Leetcode 13: roman to integer
		 * 
		 * 
		 */
		public static int RomanToInt(string s)
		{
			if (s.Length == 0 || s.Length > 15)
				return 0;

			var counter = 0;

			var dictionary = new Dictionary<char, int> {
								{ 'I', 1 },
								{ 'V', 5 },
								{ 'X', 10 },
								{ 'L', 50 },
								{ 'C', 100 },
								{ 'D', 500 },
								{ 'M', 1000 }
							};

			for (var i = 0; i < s.Length; i++)
			{
				if (i == 0 || dictionary[s[i - 1]] >= dictionary[s[i]])
				{
					// keep going
					counter += dictionary[s[i]];
				}
				else
				{
					counter = counter + dictionary[s[i]] - (2 * dictionary[s[i - 1]]);
				}
			}

			return counter;
		}


		public static int StrStr(string haystack, string needle)
		{
			if (haystack.Equals("") && needle.Equals("")) return 0;

			return haystack.IndexOf(needle);

		}


		/*
		 * Leetcode 53: Given an integer array nums, find the contiguous subarray (containing at least one number) which has the largest sum and return its sum.
		 * 
		 * O(nxn)
		 */
		public static int MaxSubArray(int[] nums)
		{
			int maxSubarray = Int16.MinValue;
			for (int i = 0; i < nums.Length; i++)
			{
				int currentSubarray = 0;
				for (int j = i; j < nums.Length; j++)
				{
					currentSubarray += nums[j];
					maxSubarray = Math.Max(maxSubarray, currentSubarray);
				}
			}

			return maxSubarray;
		}

		/*
		 * Docusign interview
		 * 
		 */
		public static int maximumSubArrayK(int[] nums, int k)
		{
			int max = Int16.MinValue;

			for (int i = 0; i < nums.Length; i++)
			{
				int sum = 0;
				int counter = 0;
				int index = i;
				while (counter < k && index < nums.Length)
				{
					sum += nums[index];
					index++;
					counter++;
				}

				max = Math.Max(max, sum);

			}

			return max;
		}

		/*
		 * O(n)
		 */
		public static int maximumSubArrayK2(int[] nums, int k)
		{
			int max = Int16.MinValue;
			int sum = 0, counter = 0;

			for (int i = 0; i < nums.Length; i++)
			{
				if(counter < 3)
				{
					sum += nums[i];
				}
				else
				{
					sum = sum - nums[i - k] + nums[i];
				}

				max = Math.Max(max, sum);

				counter++;
			}

			return max;
		}

		/**
		 * O(n)
		 */
		public static int MaxSubArrayII(int[] nums)
		{
			var prevSum = nums[0];
			int maxSum = nums[0];
			for (int i = 1; i < nums.Length; i++)
			{
				prevSum = Math.Max(nums[i], nums[i] + prevSum);
				maxSum = Math.Max(maxSum, prevSum);
			}

			return maxSum;
		}

		/*
		 * Leetcode 560: Subarray Sum Equals K
		 * 
		 * 
		 */
		public int SubarraySum(int[] nums, int k)
		{

			int res = 0;

			for (int i = 0; i < nums.Length; i++)
			{

				int j = i;
				int sum = 0;
				while (j < nums.Length)
				{
					sum += nums[j];

					if (sum == k)
					{
						res++;
					}

					j++;
				}

			}

			return res;

		}

		public static int SubarraySumII(int[] nums, int k)
		{

			int res = 0, curSum = 0;

			Dictionary<int, int> prefixSums = new Dictionary<int, int>();
			prefixSums.Add(0, 1);

			foreach (int n in nums)
			{

				curSum += n;

				int diff = curSum - k;

				res += prefixSums.ContainsKey(diff) ? prefixSums[diff] : 0;

				prefixSums[curSum] = 1 + (prefixSums.ContainsKey(curSum) ? prefixSums[curSum] : 0);

			}

			return res;

		}


		/*
		 * Leetcode 2: Two Sum
		 * iven an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.
		 * Input: nums = [2,7,11,15], target = 9
		 * Output: [0,1]
		 * Output: Because nums[0] + nums[1] == 9, we return [0, 1].
		 */
		public static int[] TwoSum(int[] nums, int target)
		{

			Dictionary<int, int> prevMap = new Dictionary<int, int>();
			int[] res = new int[2];

			for (int i = 0; i < nums.Length; i++)
			{
				int diff = target - nums[i];
				if (prevMap.ContainsKey(diff))
				{
					res = new int[] { prevMap[diff], i };
				}
				else
				{
					prevMap.Add(nums[i], i);
				}

			}

			return res;
		}

		/* Leetcode 1344: Given two numbers, hour and minutes. 
		 * Return the smaller angle (in degrees) formed between the hour and the minute hand.
		 * 
		 * 
		 */
		public static double AngleClock(int hour, int minutes)
		{
			int oneMinAngle = 6;
			int oneHourAngle = 30;

			double minutesAngle = oneMinAngle * minutes;
			double hourAngle = (hour % 12 + minutes / 60.0) * oneHourAngle;

			double diff = Math.Abs(hourAngle - minutesAngle);
			return Math.Min(diff, 360 - diff);
		}

		/*
		 * Leetcode 121: Best Time to Buy and Sell Stock
		 * 
		 * Input: prices = [7,1,5,3,6,4]
		 * Output: 5
		 * 
		 */
		public static int MaxProfit(int[] prices)
		{
			var n = prices.Length;

			if (n == 0) return 0;

			var globalMaxProfit = 0;
			var globalMin = prices[0];
			for (int i = 1; i < n; i++)
			{
				var curPrice = prices[i];
				var localMaxProfit = Math.Max(0, curPrice - globalMin);
				globalMaxProfit = Math.Max(localMaxProfit, globalMaxProfit);
				globalMin = Math.Min(globalMin, curPrice);
			}

			return globalMaxProfit;
		}

		public static int[] isZigzag(int[] numbers)
		{
			List<int> temp = new List<int>();

			for (int i = 0; i + 2 < numbers.Length; i++)
			{
				if ((numbers[i] < numbers[i + 1] && numbers[i + 1] > numbers[i + 2])
					|| (numbers[i] > numbers[i + 1] && numbers[i + 1] < numbers[i + 2]))
				{
					temp.Add(1);
				}
				else
				{
					temp.Add(0);
				}
			}

			return temp.ToArray();
		}

		public static bool makeIncreasing(int[] numbers)
		{
			bool isTrue = true;
			int len = numbers.Length;
			if (len == 1) return true;

			int i = 0;


			while (i < len - 1)
			{

				if (numbers[i] > numbers[i + 1])
				{
					if (i-1 >= 0 && !checkAllPossibles(numbers[i], numbers[i + 1], numbers[i-1]))
					{

						//Console.WriteLine(string.Format("i = {3}, numbers[i] = {0}, numbers[i+1] = {1}", numbers[i], numbers[i + 1], i));

						if (i - 1 >= 0) Console.Write(", numbers[i-1]" + numbers[i - 1]);

						isTrue = false;
						break;
					}
				}

				i++;
			}

			return isTrue;
		}


		public static bool checkAllPossibles(int numb, int target1, int target2)
		{

			List<int> lstRev = reverseNumber(numb);
			bool isAnySmallThan = false;

			foreach (int i in lstRev)
			{
				if (i < target1 && i >  target2)
				{
					isAnySmallThan = true;
					break;
				}
			}

			return isAnySmallThan;
		}

		public static List<int> reverseNumber(int num)
		{
			int len = num.ToString().Length;
			string str = num.ToString();

			List<int> lst = new List<int>();

			if (len == 0) return lst;

			if (len == 1) lst.Add(num);

			if (len == 2)
			{
				lst.Add(Int16.Parse(str[1].ToString() + str[0].ToString()));
			}

			if (len == 3)
			{
				lst.Add(Int16.Parse(str[2].ToString() + str[1].ToString() + str[0].ToString()));
				lst.Add(Int16.Parse(str[2].ToString() + str[0].ToString() + str[1].ToString()));
				lst.Add(Int16.Parse(str[1].ToString() + str[0].ToString() + str[2].ToString()));
				lst.Add(Int16.Parse(str[1].ToString() + str[2].ToString() + str[1].ToString()));
				lst.Add(Int16.Parse(str[0].ToString() + str[2].ToString() + str[1].ToString()));
			}

			if (len == 4)
			{
				lst.Add(1);
			}

			return lst;
		}


		//public static int countWaysToSplit(string s)
		//{
		//	Dictionary<int, char> countDic = new Dictionary<int, char>();

		//	string temp = s;
		//	while(temp.Length > 1)
		//	{


		//	}

		//}

		public static string mergeStrings(string s1, string s2)
		{
			StringBuilder sb = new StringBuilder();
			int i = 0;

			while (!string.IsNullOrEmpty(s1) || !string.IsNullOrEmpty(s2))
			{
				string c1 = "";
				string c2 = "";

				if (!string.IsNullOrEmpty(s1))
				{
					c1 = s1[0].ToString();
				}

				if (!string.IsNullOrEmpty(s2))
				{
					c2 = s2[0].ToString();
				}

				if (i == 0 && !string.IsNullOrEmpty(c1) && !string.IsNullOrEmpty(c2) && c1[0] > c2[0])
				{
					sb.Append(s1).Append(s2);
					break;
				}

				if (!string.IsNullOrEmpty(c1) && !string.IsNullOrEmpty(c2) && c1[0] < c2[0])
				{
					sb.Append(c1);
					s1 = s1.Substring(1);
				} else if (string.IsNullOrEmpty(c1) && !string.IsNullOrEmpty(c2))
				{
					sb.Append(c2);
					s2 = s2.Substring(1);
				} else if (!string.IsNullOrEmpty(c1) && string.IsNullOrEmpty(c2))
				{
					sb.Append(c1);
					s1 = s1.Substring(1);
				}
				else
				{
					sb.Append(c2);
					s2 = s2.Substring(1);
				}

				i++;
			}

			return sb.ToString();
		}


		/*
		 * Leetcode 198: House robber
		 * 
		 */
		public static int robber(int[] nums)
		{
			if (nums == null || nums.Length == 0)
			{
				return 0;
			}

			int rob1 = 0;
			int rob2 = 0;
			int temp = 0;


			//[rob1, rob2, n, n+1, ....]
			for (int i = 0; i < nums.Length; i++)
			{
				temp = Math.Max(nums[i] + rob1, rob2);
				rob1 = rob2;
				rob2 = temp;
			}

			return rob2;
		}

		/*
		 * Leetcode 213: House robber 2
		 * Input: nums = [2,3,2]
		 * Output: 3
		 * Explanation: You cannot rob house 1 (money = 2) and then rob house 3 (money = 2), because they are adjacent houses.
		 */
		public int Rob(int[] nums)
		{

			return Math.Max(nums[0],
							 Math.Max(robber(new List<int>(nums).GetRange(1, nums.Length - 1).ToArray()),
									  robber(new List<int>(nums).GetRange(0, nums.Length - 1).ToArray()))
						   );

		}


		/*
		 * Leetcode 200: number of islands
		 * Given an m x n 2D binary grid grid which represents a map of '1's (land) and '0's (water), return the number of islands.
		 * An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically.
		 * You may assume all four edges of the grid are all surrounded by water.
		 */
		public int NumIslands(char[][] grid)
		{
			if (grid.Length == 0)
				return 0;

			int rows = grid.Length, cols = grid[0].Length;
			bool[,] visit = new bool[rows, cols];


			int islands = 0;

			for (int r = 0; r < rows; r++)
			{

				for (int c = 0; c < cols; c++)
				{

					if (grid[r][c] == '1' && visit[r, c] == false)
					{

						islands += 1;
						dfs(r, c, grid, visit);

					}
				}

			}

			return islands;

		}

		public void dfs(int r, int c, char[][] grid, bool[,] visited)
		{

			visited[r, c] = true;

			if (r - 1 >= 0 && !visited[r - 1, c] && grid[r - 1][c] != '0')
				dfs(r - 1, c, grid, visited);
			if (r + 1 < grid.Length && !visited[r + 1, c] && grid[r + 1][c] != '0')
				dfs(r + 1, c, grid, visited);
			if (c - 1 >= 0 && !visited[r, c - 1] && grid[r][c - 1] != '0')
				dfs(r, c - 1, grid, visited);
			if (c + 1 < grid[0].Length && !visited[r, c + 1] && grid[r][c + 1] != '0')
				dfs(r, c + 1, grid, visited);

		}


		public static int NumIslands2(char[][] grid)
		{
			if (grid == null)
				return 0;

			int rows = grid.Length, cols = grid[0].Length;
			bool[,] visit = new bool[rows, cols];
			int islands = 0;

			for (int i = 0; i < rows; i++)
			{

				for (int j = 0; j < cols; j++)
				{

					if (grid[i][j] == '1' && visit[i, j] == false)
					{
						bfs(i, j);
						islands++;
					}
				}
			}

			void bfs(int r, int c)
			{
				Queue<int[]> q = new Queue<int[]>();
				visit[r, c] = true;

				q.Enqueue(new int[] { r, c });

				while (q.Count > 0)
				{
					int[] temp = q.Dequeue();
					int row = temp[0], col = temp[1];
					int[][] directions = new int[][] {
									new int[] { 1,0 },
									new int[] { -1,0},
									new int[] { 0,1},
									new int[] { 0,-1}
					};

					foreach (int[] item in directions)
					{
						r = row + item[0];
						c = col + item[1];

						if (Enumerable.Range(0, rows).Contains(r)
						  && Enumerable.Range(0, cols).Contains(c)
						   && grid[r][c] == '1'
						   && visit[r, c] == false
						  )
						{
							q.Enqueue(new int[] { r, c });
							visit[r, c] = true;
						}
					}
				}
			}

			return islands;

		}

		/*
		 * Leetcode 695: Max Area of island
		 * You are given an m x n binary matrix grid. An island is a group of 1's (representing land) connected 4-directionally (horizontal or vertical.) 
		 * You may assume all four edges of the grid are surrounded by water.
		 * 
		 */
		public int MaxAreaOfIsland(int[][] grid)
		{

			bool[,] seen = new bool[grid.Length, grid[0].Length];
			int ans = 0;
			for (int r = 0; r < grid.Length; r++)
			{
				for (int c = 0; c < grid[0].Length; c++)
				{
					ans = Math.Max(ans, area(r, c, grid, seen));
				}
			}
			return ans;
		}

		public int area(int r, int c, int[][] grid, bool[,] seen)
		{
			if (r < 0 || r >= grid.Length
				|| c < 0 || c >= grid[0].Length
				|| seen[r, c]
				|| grid[r][c] == 0)
				return 0;

			seen[r, c] = true;

			return (1 + area(r + 1, c, grid, seen) + area(r - 1, c, grid, seen)
					  + area(r, c - 1, grid, seen) + area(r, c + 1, grid, seen));
		}

		/*
		 * Leetcode 204: count primes
		 *  In math, prime numbers are whole numbers greater than 1, that have only two factors – 1 and the number itself
		 *  For example, 5 is prime because the only ways of writing it as a product, 1 × 5 or 5 × 1
		 */
		public int CountPrimes(int n)
		{
			// Because 1 or 2 contains 0 prime
			if (n < 3) return 0;

			// The idea of Sieve of Eratosthenes is multiplies of a prime number are not prime numbers.
			// For example, 2 is a prime number, so 2*2, 2*3, 2*4,... are not primes.       
			// Define an array and initialize every number (indexes of the array) as true to assume every number is a prime.
			// Later we will mark the non-prime numbers as false.
			bool[] dp = Enumerable.Repeat<bool>(true, n).ToArray();


			// Since 2 is the smallest prime number, so we start the loop from 2.
			// Why the boundary is i*i < n instead of i < n? 
			// If n is divisible by i, then there is a number j which n = i * j. 
			// Assume i <= j, then i * i <= n. So i <= sqrt(n)
			for (int i = 2; i * i < n; i++)
			{
				// mark multiplies of primes as non-primes
				if (dp[i])
				{
					// Why j starts with i * i instead of 2*i?
					// If i is a prime, then non-primes before i*i have already been checked before.
					// For example, if i = 5, we don't need to mark 5*2, 5*3, 5*4, since they have been checked before when i= 2,3,4.
					for (int j = i * i; j < n; j += i)
						dp[j] = false;
				}
			}

			// count the number of primes starting with 2
			int result = dp.Where(c => c == true).Count();

			return result - 2;  //Because result include [0],[1] is true.
		}

		/*
		 * Leetcode 253: Meeting rooms
		 * 
		 * Given an array of meeting time intervals intervals where intervals[i] = [starti, endi], 
		 * return the minimum number of conference rooms required.
		 * 
		 */
		public static int MeetingRooms(int[][] intervals)
		{

			int[] start = new int[intervals.Length];
			int[] end = new int[intervals.Length];

			int i = 0;
			foreach (int[] item in intervals)
			{
				start[i] = item[0];
				end[i] = item[1];

				i++;
			}

			Array.Sort(start);
			Array.Sort(end);

			int res = 0, count = 0, sIndex = 0, eIndex = 0;

			while (sIndex < intervals.Length)
			{
				if (start[sIndex] < end[eIndex])
				{
					sIndex += 1;
					count += 1;
				}
				else
				{
					eIndex += 1;
					count -= 1;
				}

				res = Math.Max(res, count);
			}

			return res;
		}

		/*
		 * Leetcode 973: K Closest Points to Origin
		 * 
		 * 
		 */
		public int[][] KClosest(int[][] points, int k)
		{

			List<Point> minHeap = new List<Point>();

			foreach (int[] point in points)
			{
				int dist = (int)(Math.Pow(point[0], 2) + Math.Pow(point[1], 2));
				Point p = new Point();
				p.distance = dist;
				p.cord = new int[] { point[0], point[1] };

				minHeap.Add(p);
			}

			int[][] res = new int[k][];
			int i = 0;
			foreach (Point item in minHeap.OrderBy(c=>c.distance))
			{
				if (i < k)
				{
					res[i] = new int[] { item.cord[0], item.cord[1] };
				}
				else
				{
					break;
				}
				i++;
			}

			return res;
		}

		public struct Point
		{
			public int distance;
			public int[] cord;
		}

		/*
		 * Leetcode 300:
		 * Given an integer array nums, return the length of the longest strictly increasing subsequence.
		 * Input: nums = [10,9,2,5,3,7,101,18]
		 * Output: 4
		 * Explanation: The longest increasing subsequence is [2,3,7,101], therefore the length is 4.
		 */
		public int LengthOfLIS(int[] nums)
		{
			int[] LIS = Enumerable.Repeat(1, nums.Length).ToArray();

			for (int i = LIS.Length - 1; i >= 0; i--)
			{

				for (int j = i + 1; j < LIS.Length; j++)
				{

					if (nums[i] < nums[j])
						LIS[i] = Math.Max(LIS[i], 1 + LIS[j]);
				}
			}


			return LIS.Max();
		}

		/*
		 * Leetcode 1143: Longest Common Subsequence  DP
		 * Given two strings text1 and text2, return the length of their longest common subsequence. If there is no common subsequence, return 0.
		 * Input: text1 = "abcde", text2 = "ace" 
		 * Output: 3  
		 */
		public static int LongestCommonSubsequence(string text1, string text2)
		{

			int m = text1.Length, n = text2.Length;
			int[][] res = Enumerable.Range(0, m + 1)
						  .Select(x => Enumerable.Repeat(0, n + 1).ToArray())
						  .ToArray();

			for (int i = m - 1; i > -1; i--)
			{

				for (int j = n - 1; j > -1; j--)
				{
					if (text1[i] == text2[j])
					{
						res[i][j] = 1 + res[i + 1][j + 1];
					}
					else
					{
						res[i][j] = Math.Max(res[i][j + 1], res[i + 1][j]);
					}
				}

			}


			return res[0][0];
		}


		/*
		 * Leetcode 152: Maximum Product Subarray
		 * Input: nums = [2,3,-2,4]
		 * Output: 6
		 * Explanation: [2,3] has the largest product 6.
		 * 
		 */
		public static int MaxProduct(int[] nums)
		{

			int max = nums[0], min = nums[0], res = nums[0];

			for (int i = 1; i < nums.Length; i++)
			{
				int product1 = nums[i];

				// nums[i] and max are positive
				int product2 = nums[i] * max;

				// nums[i] and min are negative
				int product3 = nums[i] * min;

				max = Math.Max(Math.Max(product1, product2), product3);
				min = Math.Min(Math.Min(product1, product2), product3);

				res = Math.Max(res, max);
			}

			return res;

		}

		public static int MaxKilledEnemies(char[][] grid)
		{
			int max = 0;
			int sum = 0;
			for(int i=0; i < grid.Length; i++)
			{
				sum = 0;

				for (int j = 0; j< grid[i].Length; j++)
				{

					if(grid[i][j] == '0')
					{
						Console.WriteLine("i = "+ i + ", j = " + j);

						//left side
						if (j-1 >= 0 && j-1< grid[i].Length && grid[i][j - 1] == 'E')
						{
							sum += 1;
						}
						//right side
						if(j + 1 < grid[i].Length && grid[i][j+ 1] == 'E')
						{
							sum += 1;
						}
						//top side
						if (i-1 >= 0 && i-1 < grid.Length && grid[i-1][j] == 'E')
						{
							sum += 1;
						}
						//bottom side
						if (i + 1 < grid.Length && grid[i + 1][j] == 'E')
						{
							sum += 1;
						}

						if (sum > max) max = sum;
					}

				}
			}

			return max;
		}

		public static void Rotate(int[][] matrix)
		{

			int x = matrix.Length;
			int y = matrix[0].Length;
			int[][] res = new int[x][];

			for (int i = 0; i < y; i++)
			{
				int k = 0;
				int[] temp = new int[x];
				for (int j = x - 1; j >= 0; j--)
				{
					temp[k] = matrix[j][i];
					k++;
				}
				res[i] = temp;
			}

			Array.Copy(res, matrix, res.Length);

		}

		public static int[][] UpdateMatrix(int[][] mat)
		{
			int x = mat.Length;
			int y = mat[0].Length;
			int[][] res = new int[x][];

			for (int i = 0; i < x; i++)
			{
				int k = 0;
				int[] temp = new int[y];
				for (int j = 0; j < y; j++)
				{
					if(mat[i][j] == 0)
					{
						temp[k] = 0;
					}
					else
					{
						if((j-1>=0 && mat[i][j-1] == 0) 
							|| (i-1>=0 && mat[i-1][j] == 0)
							|| (i + 1 < x && mat[i + 1][j] == 0)
							|| (j + 1 < y && mat[i][j+1] == 0))
						{
							temp[k] = 1;
						}
						else
						{
							int step = 1;
							while(j - step >= 0)
							{
								if (mat[i][j - step] == 0)
								{
									temp[k] = step;
									break;
								}
								step++;
							}

							step = 1;
							while (j + step < y)
							{
								if (mat[i][j + step] == 0)
								{
									temp[k] = step;
									break;
								}
								step++;
							}

							step = 1;
							while (i - step >= 0)
							{
								if (mat[i - step][j] == 0)
								{
									temp[k] = step;
									break;
								}
								step++;
							}

							step = 1;
							while ( i + step < x)
							{
								if ( mat[i + step][j] == 0)
								{
									temp[k] = step;
									break;
								}
								step++;
							}
						}
					}
					k++;
				}
				res[i] = temp;

			}

			return res;
		}

		/*
		 * Leetcode 62 Unique paths
		 * 
		 */

		public static int UniquePaths(int m, int n)
		{

			// Matrix to store the value of possible ways to reach each cell
			int[][] dp = new int[m][];

			// C# thing, as we cannot define int[][] dp = new int[m][n], fill every cell with 1; 
			for (int i = 0; i < m; i++)
			{
				dp[i] = Enumerable.Repeat(1, n).ToArray();
			}

			for (int i = 1; i < m; ++i)
			{
				for (int j = 1; j < n; ++j)
				{

					dp[i][j] = dp[i - 1][j] + dp[i][j - 1];

				}

			}

			return dp[m - 1][n - 1];

		}

		/*
		 * Leetcode 63: Unique Paths II
		 * A robot is located at the top-left corner of a m x n grid (marked 'Start' in the diagram be
		 * An obstacle and space is marked as 1 and 0 respectively in the grid.
		 */
		public static int UniquePathsWithObstacles(int[][] obstacleGrid)
		{
			int rows = obstacleGrid.Length;
			int cols = obstacleGrid[0].Length;

			// If the starting cell has an obstacle, then simply return as there would be
			// no paths to the destination.
			if (obstacleGrid[0][0] == 1)
			{
				return 0;
			}

			// Number of ways of reaching the starting cell = 1.
			obstacleGrid[0][0] = 1;

			// Filling the values for the first column
			for (int i = 1; i < rows; i++)
			{
				obstacleGrid[i][0] = (obstacleGrid[i][0] == 0 && obstacleGrid[i - 1][0] == 1) ? 1 : 0;
			}

			// Filling the values for the first row
			for (int i = 1; i < cols; i++)
			{
				obstacleGrid[0][i] = (obstacleGrid[0][i] == 0 && obstacleGrid[0][i - 1] == 1) ? 1 : 0;
			}

			// Starting from cell(1,1) fill up the values
			// No. of ways of reaching cell[i][j] = cell[i - 1][j] + cell[i][j - 1]
			// i.e. From above and left.
			for (int i = 1; i < rows; i++)
			{
				for (int j = 1; j < cols; j++)
				{
					if (obstacleGrid[i][j] == 0)
					{
						obstacleGrid[i][j] = obstacleGrid[i - 1][j] + obstacleGrid[i][j - 1];
					}
					else
					{
						obstacleGrid[i][j] = 0;
					}
				}
			}

			// Return value stored in rightmost bottommost cell. That is the destination.
			return obstacleGrid[rows - 1][cols - 1];
		}


		/*
		 * Leetcode 64: Minimum path sum
		 * Given a m x n grid filled with non-negative numbers, 
		 * find a path from top left to bottom right, which minimizes the sum of all numbers along its path.
		 * 
		 */
		public static int MinPathSum(int[][] grid)
		{

			int m = grid.Length;
			if (m == 0) return 0;
			int n = grid[0].Length;

			for (int i = 0; i < m; i++)
			{

				for (int j = 0; j < n; j++)
				{

					if (i == 0 && j == 0) continue;

					if (i == 0)
						grid[i][j] += grid[i][j - 1];
					else if (j == 0)
						grid[i][j] += grid[i - 1][j];
					else
						grid[i][j] += Math.Min(grid[i][j - 1], grid[i - 1][j]);


				}

			}

			return grid[m - 1][n - 1];

		}


		/*
		 * Leetcode 72: edit distance
		 * Given two strings word1 and word2, return the minimum number of operations required to convert word1 to word2.
		 * You have the following three operations permitted on a word:
		 * Insert a character
		 * Delete a character
		 * Replace a character
		 * Input: word1 = "horse", word2 = "ros"
		 * Output: 3
		 * 
		 */
		public static int MinDistance(string word1, string word2)
		{
			int m = word1.Length, n = word2.Length;

			int[,] cache = new int[m + 1, n + 1];

			for (int j = 0; j < n + 1; j++)
				cache[m, j] = n - j;

			for (int i = 0; i < m + 1; i++)
				cache[i, n] = m - i;

			for (int i = m - 1; i > -1; i--)
			{

				for (int j = n - 1; j > -1; j--)
				{

					if (word1[i] == word2[j])
						cache[i, j] = cache[i + 1, j + 1];
					else
						cache[i, j] = 1 + Math.Min(cache[i + 1, j], Math.Min(cache[i, j + 1], cache[i + 1, j + 1]));

				}

			}

			return cache[0, 0];

		}

		/*
		 * Leetcode 139: word break, dp
		 * Given a string s and a dictionary of strings wordDict, return true if s can be segmented into a space-separated sequence of one or more dictionary words.
		 * Input: s = "leetcode", wordDict = ["leet","code"]
		 * Output: true
		 * 
		 */
		public static bool WordBreak(string s, IList<string> wordDict)
		{

			bool[] dp = new bool[s.Length + 1];
			dp[s.Length] = true;

			for (int i = s.Length - 1; i > -1; i--)
			{
				foreach (string w in wordDict)
				{

					if (i + w.Length <= s.Length && s.Substring(i, w.Length) == w)
					{
						dp[i] = dp[i + w.Length];
					}

					if (dp[i])
						break;
				}
			}

			return dp[0];


		}


		/*
		 * 243 Shortest Word Distance
		 * 
		 */
		public static int ShortestDistance(string[] wordsDict, string word1, string word2)
		{
			int first = int.MaxValue;
			int second = int.MaxValue;
			int result = wordsDict.Length;
			for (int i = 0; i < wordsDict.Length; i++)
			{
				if (wordsDict[i].Equals(word1))
				{
					first = i;
				}
				else if (wordsDict[i].Equals(word2))
				{
					second = i;
				}

				if (first != int.MaxValue && second != int.MaxValue)
				{
					result = Math.Min(result, Math.Abs(first - second));
				}
			}

			return result;
		}

		public static int[] mutateTheArray(int n, int[] a)
		{
			int[] b = new int[n];

			for (int i = 0; i < n; i++){

				int sum = (i - 1 >= 0 && i - 1 < a.Length ? a[i - 1] : 0) + (i >= 0 && i < a.Length ? a[i] : 0) + (i + 1 >= 0 && i + 1 < a.Length ? a[i + 1] : 0);
				b[i] = sum;
			}

			return b;

		}

		public static bool alternatingSort(int[] a)
		{
			bool isAlternating = true;
			int length = a.Length;
			int[] b = new int[a.Length];

			int i = 0;
			int j = length - 1;
			int index = 0;
			while (i <= j)
			{
				if (i < j)
				{
					b[index] = a[i];
					b[index+1] = a[j];
					index = index + 2;
				}else if (i == j)
				{
					b[index] = a[i];
					index = index + 1;
				}
				i++;
				j--;
			}

			index = 0;
			while(index + 1 < b.Length)
			{
				if (b[index] >= b[index + 1])
				{
					isAlternating = false;
					break;
				}

				index++;
			}

			return isAlternating;
		}

		public static int[] longestInversionalSubarray(int[] a, int[] b, int[] c)
		{
			List<int> s = new List<int>();
			List<int> dumy = new List<int>();

			int length = a.Length, starti = 0, j = 0;

			while(starti < length)
			{
				j = starti;
				dumy = new List<int>();
				while(j < length)
				{
					if (b.Contains(a[j]) && !c.Contains(a[j]))
					{
						dumy.Add(a[j]);
						j++;
					}
					else
					{
						break;
					}
				}

				if (dumy.Count > s.Count)
				{
					s = dumy;
				}

				starti++;
			}

			return s.ToArray();
		}

		bool areFollowingPatterns(string[] strings, string[] patterns)
		{
			Dictionary<string, string> dict = new Dictionary<string, string>();
			Dictionary<string, string> dictPat = new Dictionary<string, string>();

			for (int i = 0; i < strings.Length; i++)
			{
				if (dict.ContainsKey(strings[i]))
				{
					if (dict[strings[i]] != patterns[i]) return false;
				}
				else dict.Add(strings[i], patterns[i]);

				if (dictPat.ContainsKey(patterns[i]))
				{
					if (dictPat[patterns[i]] != strings[i]) return false;
				}
				else dictPat.Add(patterns[i], strings[i]);
			}
			return true;
		}

		public static bool WordPattern(string patterns, string s)
		{
			bool isMatched = true;

			Dictionary<char, string> pdic = new Dictionary<char, string>();
			Dictionary<string, string> worddic = new Dictionary<string, string>();

			for(int i=0; i < patterns.Length; i++)
			{
				if (pdic.ContainsKey(patterns[i]))
				{
					pdic[patterns[i]] += (i.ToString());
				}
				else
				{
					pdic.Add(patterns[i], i.ToString());
				}
			}

			string[] splitWord = s.Split(' ');

			for (int i = 0; i < splitWord.Length; i++)
			{
				if (worddic.ContainsKey(splitWord[i]))
				{
					worddic[splitWord[i]] += (i.ToString());
				}
				else
				{
					worddic.Add(splitWord[i], i.ToString());
				}
			}

			if (pdic.Count != worddic.Count) return false;

			for(int j =0; j < pdic.Count; j++)
			{
				if(pdic.ElementAt(j).Value != worddic.ElementAt(j).Value)
				{
					isMatched = false;
					break;
				}
			}

			return isMatched;
		}

		public static int centuryFromYear(int year)
		{
			int century = year / 100;

			int rest = year % 100;

			return century +  (rest > 0? 1: 0);
		}

		/*
		 * Leetcode 37: sudoku solver
		 * 
		 * 
		 */
		private bool[,] rows = new bool[9, 10],
					cols = new bool[9, 10],
					boxes = new bool[9, 10];

		public void SolveSudoku(char[][] board)
		{
			if (board == null || board.Length == 0)
				return;

			for (int i = 0; i < board.Length; i++)
				for (int j = 0; j < board[0].Length; j++)
					if (board[i][j] != '.')
					{
						rows[i, board[i][j] - '0'] = true;
						cols[j, board[i][j] - '0'] = true;
						boxes[(i / 3) * 3 + j / 3, board[i][j] - '0'] = true;
					}

			Helper(board, 0, 0);
		}

		private bool Helper(char[][] board, int i, int j)
		{
			while (i < 9 && board[i][j] != '.')
			{
				j++;

				if (j > 8)
				{
					i++;
					j = 0;
				}
			}

			if (i == 9)
				return true;

			for (int k = 1; k < 10; k++)
				if (!rows[i, k] && !cols[j, k] && !boxes[(i / 3) * 3 + j / 3, k])
				{
					rows[i, k] = true;
					cols[j, k] = true;
					boxes[(i / 3) * 3 + j / 3, k] = true;
					board[i][j] = (char)(k + 48);

					if (Helper(board, i, j))
						return true;

					rows[i, k] = false;
					cols[j, k] = false;
					boxes[(i / 3) * 3 + j / 3, k] = false;
					board[i][j] = '.';
				}

			return false;
		}

		/*
		 * Leetcode 36, Medium
		 * 
		 * Determine if a 9 x 9 Sudoku board is valid. Only the filled cells need to be validated according to the following rules:
		 * Each row must contain the digits 1-9 without repetition.
		 * Each column must contain the digits 1-9 without repetition.
		 * Each of the nine 3 x 3 sub-boxes of the grid must contain the digits 1-9 without repetition.
		 */
		public bool IsValidSudoku(char[][] board)
		{
			//This is very fast
			/*
			 * int a = 5;
			 * int b = 3;
			 * int div = a / b; //quotient is 1
			 * int mod = a % b; //remainder is 2
			 * 
			 */
			for (var i = 0; i < 9; i++)
			{
				var rowHash = new HashSet<char>();
				var colHash = new HashSet<char>();
				var subHash = new HashSet<char>();
				
				var rowindex = i / 3;
				var colindex = i % 3;

				for (var j = 0; j < 9; j++)
				{
					if (board[i][j] != '.' && !rowHash.Add(board[i][j]))
						return false;
					if (board[j][i] != '.' && !colHash.Add(board[j][i]))
						return false;

					var row = 3 * rowindex + j / 3;
					var col = 3 * colindex + j % 3;

					if (board[row][col] != '.' && !subHash.Add(board[row][col]))
						return false;
				}
			}

			return true;
		}

		/*
		 * Easy understanding code
		 * 
		 */
		public bool IsValidSudoku2(char[][] board)
		{

			HashSet<string> seen = new HashSet<string>();

			for (int i = 0; i < 9; i++)
			{

				for (int j = 0; j < 9; j++)
				{

					char current_val = board[i][j];

					if (current_val != '.')
					{

						if (!seen.Add(current_val + "found in row" + i)
						  || !seen.Add(current_val + "found in column" + j)
						  || !seen.Add(current_val + "found in sub box" + i / 3 + "-" + j / 3)
						  )
						{
							return false;
						}
					}
				}

			}

			return true;

		}



		public static bool IsValidSudoku3(char[][] grid)
		{
			return isValidConfig(grid, 9);
		}

		public static bool isValidConfig(char[][] arr, int n)
		{
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					// If current row or current column or
					// current 3x3 box is not valid, return
					// false
					if (!isValid(arr, i, j))
						return false;
				}
			}
			return true;
		}

		// Checks whether current row and current column and
		// current 3x3 box is valid or not
		public static bool isValid(char[][] arr, int row,
								   int col)
		{
			return notInRow(arr, row) && notInCol(arr, col)
				&& notInBox(arr, row - row % 3, col - col % 3);
		}

		// Checks whether there is any duplicate
		// in current row or not
		public static bool notInRow(char[][] arr, int row)
		{

			// Set to store characters seen so far.
			HashSet<char> st = new HashSet<char>();

			for (int i = 0; i < 9; i++)
			{

				// If already encountered before,
				// return false
				if (st.Contains(arr[row][i]))
					return false;

				// If it is not an empty cell, insert value
				// at the current cell in the set
				if (arr[row][i] != '.')
					st.Add(arr[row][i]);
			}
			return true;
		}

		// Checks whether there is any duplicate
		// in current column or not.
		public static bool notInCol(char[][] arr, int col)
		{
			HashSet<char> st = new HashSet<char>();

			for (int i = 0; i < 9; i++)
			{

				// If already encountered before,
				// return false
				if (st.Contains(arr[i][col]))
					return false;

				// If it is not an empty cell,
				// insert value at the current
				// cell in the set
				if (arr[i][col] != '.')
					st.Add(arr[i][col]);
			}
			return true;
		}

		// Checks whether there is any duplicate
		// in current 3x3 box or not.
		public static bool notInBox(char[][] arr, int startRow,
									int startCol)
		{
			HashSet<char> st = new HashSet<char>();

			for (int row = 0; row < 3; row++)
			{
				for (int col = 0; col < 3; col++)
				{
					char curr
						= arr[row + startRow][col + startCol];

					// If already encountered before, return false
					if (st.Contains(curr))
						return false;

					// If it is not an empty cell,
					// insert value at current cell in set
					if (curr != '.')
						st.Add(curr);
				}
			}
			return true;
		}

		public static bool isCryptSolution(string[] crypt, char[][] solution)
		{
			bool isCrypt = false;
			Dictionary<char, char> dic = new Dictionary<char, char>();
			for(int i = 0; i < solution.Length; i++)
			{
				if (!dic.ContainsKey(solution[i][0]))
				{
					dic.Add(solution[i][0], solution[i][1]);
				}
			}

			StringBuilder sb1 = new StringBuilder();
			StringBuilder sb2 = new StringBuilder();
			StringBuilder sb3 = new StringBuilder();

			int j = 0;
			while (j < crypt[0].Length)
			{
				if (dic.ContainsKey(crypt[0][j]))
				{
					sb1.Append(dic.First(c => c.Key == crypt[0][j]).Value);
				}
				j++;
			}

			j = 0;
			while(j < crypt[1].Length)
			{
				if (dic.ContainsKey(crypt[1][j]))
				{
					sb2.Append(dic.First(c => c.Key == crypt[1][j]).Value);
				}
				j++;
			}

			j = 0;
			while (j < crypt[2].Length)
			{
				if (dic.ContainsKey(crypt[2][j]))
				{
					sb3.Append(dic.First(c => c.Key == crypt[2][j]).Value);
				}
				j++;
			}

			if (Int64.Parse(sb1.ToString()) + Int64.Parse(sb2.ToString()) == Int64.Parse(sb3.ToString()))
			{
				isCrypt = true;
				if (sb1.Length > 1 && sb2.Length > 1 && sb3.Length > 1 && (sb1[0] == '0' || sb2[0] == '0' || sb3[0] == '0'))
				{
					isCrypt = false;
				}
			}

			return isCrypt;
		}

		public static bool[] boundedRatio(int[] a, int l, int r)
		{
			int lenght = a.Length;
			bool[] b = new bool[lenght];

			for(int i = 0; i < lenght; i++)
			{
				int x = a[i] % (i + 1) == 0 ? a[i]/(i+1):0;
				if ( x != 0 && x >= l && x <= r)
				{
					b[i] = true;
				}else
				{
					b[i] = false;
				}
			}

			return b;
		}


		public static int countTinyPairs(int[] a, int[] b, int k)
		{
			int tiny = 0;
			int l1 = a.Length;
			int l2 = b.Length;
			StringBuilder sb;

			int i = 0;
			int j = l2 - 1;
			while(i < l1 && j >= 0)
			{
				sb = new StringBuilder();
				sb.Append(a[i]);
				sb.Append(b[j]);

				if(Int64.Parse(sb.ToString()) < k)
				{
					tiny++;
				}

				i++;
				j--;
			}
			return tiny;
		}

		/*
		 * Leetcode 322: Coin Change
		 * You are given an integer array coins representing coins of different denominations and an integer amount representing a total amount of money.
		 * Input: coins = [1,2,5], amount = 11
		 * Output: 3
		 * Explanation: 11 = 5 + 5 + 1
		 */
		public static int CoinChange(int[] coins, int amount)
		{

			// initialize the DP array
			var dp = Enumerable.Repeat(amount + 1, amount + 1).ToArray();
			dp[0] = 0;

			for (int a = 1; a < amount + 1; a++)
			{
				foreach (int c in coins)
				{

					if (a - c >= 0)
					{

						dp[a] = Math.Min(dp[a], 1 + dp[a - c]);

					}

				}

			}

			return dp[amount] != amount + 1 ? dp[amount] : -1;

		}

		public static long subarraysCountBySum(int[] arr, int k, long s)
		{
			long res = 0;
			Stack<int> st = new Stack<int>();
			int i = 0;
			while (i < arr.Length)
			{
				st.Push(arr[i]);
				i++;
			}

			int sum = 0;
			for(int j = 0; j< k; j++)
			{
				Stack<int> dump = new Stack<int>(new Stack<int>(st));
				int step = 0;
				while(dump.Count > 0)
				{
					sum = 0;
					step = j;
					while (dump.Count > 0 && step >= 0)
					{
						sum += dump.Pop();
						step--;
					}

					if(sum == s)
					{
						res++;
					}
				}
			}

			return res;
		}

		public static List<string> lexicographicallyArray(List<string> words)
		{
			if (words.Count == 1) return words;

			var lst = words;
			lst.Sort(new LexicographicalComparer());

			return lst;
		}

		public class LexicographicalComparer : IComparer<string>
		{
			public int Compare(string first, string second)
			{
				return islexicographically(first, second) ? 0 : 1;
			}
		}

		public static bool islexicographically(string s1, string s2)
		{
			int l1 = s1.Length >= s2.Length ? s2.Length : s1.Length;
			int l2 = s1.Length >= s2.Length ? s1.Length : s2.Length;
			bool islexicographically = true;

			int i = 0, j = 0;

			while (i < l1 && j < l2)
			{
				if (i < l1 && j < l2 && i <= j)
				{
					if (s1[i] == s2[j])
					{
						i++;
						j++;
						continue;
					}

					if(s1[i] < s2[j])
					{
						break;
					}

					if (s1[i] > s2[j])
					{
						islexicographically = false;
						break;
					}
				}

				i++;
				j++;
			}

			return islexicographically;
		}

		public static int removeOneDigital(string s, string t)
		{
			int sum = 0;

			int i = 0;

			while (i < s.Length)
			{
				int num;
				if(Int32.TryParse(s[i].ToString(), out num))
				{
					string newStr = "";
					if (i == 0)
					{
						newStr = s.Substring(1);
					}
					else
					{
						newStr = s.Substring(0, i) + s.Substring(i + 1);
					}

					if(islexicographically(newStr, t))
					{
						sum += 1;
					}
				}

				i++;
			}

			i = 0;

			while (i < t.Length)
			{
				int num;
				if (Int32.TryParse(t[i].ToString(), out num))
				{
					string newStr = "";
					if (i == 0)
					{
						newStr = t.Substring(1);
					}
					else
					{
						newStr = t.Substring(0, i) + t.Substring(i + 1);
					}

					if (islexicographically(s, newStr))
					{
						sum += 1;
					}
				}

				i++;
			}


			return sum;
		}


		/*
		 * Leetcode: string to integer(atoi)
		 * Implement the myAtoi(string s) function, which converts a string to a 32-bit signed integer (similar to C/C++'s atoi function).
		 */
		public int MyAtoi(string str)
		{

			long res = 0;
			var sign = 1;
			str = str.Trim();

			if (string.IsNullOrEmpty(str)) return 0;

			int index = 0;

			if (str[0] == '+' || str[0] == '-')
			{
				sign = str[0] == '+' ? 1 : -1;
				index++;
			}

			while (index < str.Length)
			{
				if (char.IsNumber(str[index]))
				{
					res = res * 10 + str[index] - '0';

					if (res * sign > int.MaxValue) return int.MaxValue;

					if (res * sign < int.MinValue) return int.MinValue;
				}
				else
				{
					break;
				}

				index++;
			}

			return (int)res * sign;
		}

		/*Leetcode 54: Spiral Matrix
		 * Given an m x n matrix, return all elements of the matrix in spiral order.
		 * Input: matrix = [[1,2,3],[4,5,6],[7,8,9]]
		 * Output: [1,2,3,6,9,8,7,4,5]
		 */
		public IList<int> SpiralOrder(int[][] matrix)
		{

			List<int> res = new List<int>();

			if (matrix.Length == 0)
				return res;

			int r1 = 0, r2 = matrix.Length - 1;
			int c1 = 0, c2 = matrix[0].Length - 1;

			while (r1 <= r2 && c1 <= c2)
			{
				for (int c = c1; c <= c2; c++)
					res.Add(matrix[r1][c]);

				for (int r = r1 + 1; r <= r2; r++)
					res.Add(matrix[r][c2]);

				if (r1 < r2 && c1 < c2)
				{

					for (int c = c2 - 1; c > c1; c--)
						res.Add(matrix[r2][c]);

					for (int r = r2; r > r1; r--)
						res.Add(matrix[r][c1]);

				}

				r1++;
				r2--;
				c1++;
				c2--;
			}

			return res;

		}

		/*
		 * Leetcode 59:  Spiral Matrix II
		 * 
		 * Given a positive integer n, generate an n x n matrix filled with elements from 1 to n2 in spiral order.
		 */
		public static int[][] GenerateSpiralMatrix(int n)
		{
			int[][] res = new int[n][];

			for (var i = 0; i < n; i++)
				res[i] = new int[n];

			int rowBegin = 0;
			int rowEnd = n - 1;
			int columnBegin = 0;
			int columnEnd = n - 1;

			int counter = 1;

			while (rowBegin <= rowEnd && columnBegin <= columnEnd)
			{

				for (int i = columnBegin; i <= columnEnd; i++)
					res[rowBegin][i] = counter++;

				rowBegin++;

				for (int i = rowBegin; i <= rowEnd; i++)
					res[i][columnEnd] = counter++;

				columnEnd--;

				if (rowBegin <= rowEnd)
				{
					for (int i = columnEnd; i >= columnBegin; i--)
					{
						res[rowEnd][i] = counter++;
					}
				}

				rowEnd--;

				if (columnBegin <= columnEnd)
				{
					for (int i = rowEnd; i >= rowBegin; i--)
					{
						res[i][columnBegin] = counter++;
					}
				}

				columnBegin++;
			}

			return res;

		}

		/*
		 * Leetcode 73. Set Matrix Zeros
		 * Given an m x n matrix. If an element is 0, set its entire row and column to 0. Do it in-place.
		 */
		public void SetZeroes(int[][] matrix)
		{

			int m = matrix.Length;
			int n = matrix[0].Length;

			List<int[]> pos = new List<int[]>();

			for (int i = 0; i < m; i++)
			{

				for (int j = 0; j < n; j++)
				{

					if (matrix[i][j] == 0)
					{
						pos.Add(new int[] { i, j });
					}
				}
			}

			int row = 0, col = 0;
			foreach (int[] item in pos)
			{
				row = item[0];
				col = item[1];
				int index = 0;

				//fill row with 0
				while (index < n)
				{
					matrix[row][index] = 0;
					index++;
				}

				index = 0;
				//fill column with 0
				while (index < m)
				{
					matrix[index][col] = 0;
					index++;
				}
			}


		}


		/*
		 * Leetcode 794: Valid Tic-Tac-Toe state
		 * 
		 * 
		 */

		public bool ValidTicTacToe(string[] board)
		{

			// If xWins and oWins are the number of win conditions for X and O respectively, 
			//      then if xWins > 0 && oWins > 0 it is invalid
			//      then if xWins > 0 && oWins == 0 it is valid
			//      then if xWins == 0 && oWins > 0 it is valid
			//      then if xWins == 0 && oWins == 0 it is valid
			//      if either xWins or oWins > 2, it is invalid. but this will be caught in the xCount and oCount part due to one having too many.

			int xCount = 0, oCount = 0, xWins = 0, oWins = 0;
			// CHECK FOR # OF WIN[s?, CONDITIONS?]
			for (int i = 0; i < 3; ++i)
			{
				// check horizontal win
				if (board[i][0] != ' ' && board[i][0] == board[i][1] && board[i][1] == board[i][2])
				{
					if (board[i][0] == 'X')
					{
						xWins++;
					}
					else if (board[i][0] == 'O')
					{    // TODO: if guaranteed only "X", "O", or " ", then this line can just be an else (no if needed).
						oWins++;
					}
				}

				// check vertical win
				if (board[0][i] != ' ' && board[0][i] == board[1][i] && board[1][i] == board[2][i])
				{
					if (board[0][i] == 'X')
					{
						xWins++;
					}
					else if (board[0][i] == 'O')
					{
						oWins++;
					}
				}
			}

			// check diag ↘⇘⤡
			if (board[0][0] != ' ' && board[0][0] == board[1][1] && board[1][1] == board[2][2])
			{
				if (board[0][0] == 'X')
				{
					xWins++;
				}
				else if (board[0][0] == 'O')
				{
					oWins++;
				}
			}

			// check diag ↙⇙⤢
			if (board[0][2] != ' ' && board[0][2] == board[1][1] && board[1][1] == board[2][0])
			{
				// winner
				if (board[0][2] == 'X')
				{
					xWins++;
				}
				else if (board[0][2] == 'O')
				{
					oWins++;
				}
			}

			if (xWins > 0 && oWins > 0)
			{
				// Invalid, cant have 2 winners.
				return false;
			}

			// Check to make sure both players were taking the appropriate number of turns

			for (int i = 0; i < 3; ++i)
			{
				for (int j = 0; j < 3; ++j)
				{
					if (board[i][j] == 'X')
					{
						xCount++;
					}
					else if (board[i][j] == 'O')
					{
						oCount++;
					}
				}
			}


			// if x is the winner, and both have the same count, it is invalid
			if (xWins > 0 && oWins == 0 && xCount == oCount)
			{
				return false;
			}
			// if o is the winner, x must equal o
			if (xWins == 0 && oWins > 0 && xCount != oCount)
			{
				return false;
			}
			// if no winners, they must be equal or X may have one more than O
			if (!(xCount == oCount || xCount == oCount + 1))
			{
				return false;
			}


			return true;
		}


		/*
		 * Codesignal practice test Matrix diagonal
		 * Leetcode 1329 simulate, but dirrection are opsite.
		 * 
		 */
		public static int[][] diagonalsSort(int[][] mat)
		{
			var map = new Dictionary<int, List<int>>();// key= i-j and value is all the values in that diagonal;

			for (int i = 0; i < mat.Length; i++)
			{
				// Just add everything to the map;
				for (int j = 0; j < mat[0].Length; j++)
				{
					var key = i + j;
					if (!map.ContainsKey(key))
					{
						map.Add(key, new List<int>());
					}

					map[key].Add(mat[i][j]);
				}
			}

			// Since we dont have priority queue in c#, just sort individual rows
			foreach (var key in map.Keys)
				map[key].Sort();

			for (int i = 0; i < mat.Length; i++)
			{
				// Pick the first element from the list and remove that element;
				for (int j = 0; j < mat[0].Length; j++)
				{

					var val = map[i + j][0];
					mat[j][i] = val;
					map[i + j].RemoveAt(0);
				}
			}

			return mat;


		}

		public static string[][] groupingDishes(string[][] dishes)
		{
			if (dishes.Length < 2) return new string[0][];

			SortedDictionary<string, List<string>> map = new SortedDictionary<string, List<string>>(StringComparer.Ordinal);

			int duplCount = 0;
			for (int i = 0; i < dishes.Length; i++)
			{
				for (int j = 1; j < dishes[i].Length; j++)
				{
					if (map.ContainsKey(dishes[i][j]))
					{
						map[dishes[i][j]].Add(dishes[i][0]);
						if (map[dishes[i][j]].Count == 2) duplCount++;
					}
					else
					{
						map.Add(dishes[i][j], new List<string>(new string[] { dishes[i][0] }));
					}
				}
			}

			var result = new string[duplCount][];
			int ind = 0;
			foreach (var pair in map)
			{
				if (pair.Value.Count > 1)
				{
					pair.Value.Sort((s1, s2) => { return String.Compare(s1, s2, StringComparison.Ordinal); });

					pair.Value.Insert(0, pair.Key);
					result[ind] = pair.Value.ToArray();
					ind++;
				}
			}

			return result;
		}

		public static string swapLexOrder(string str, int[][] pairs)
		{
			Dictionary<int, HashSet<int>> f = new Dictionary<int, HashSet<int>>();
			foreach (int[] swap in pairs)
			{
				if (!f.ContainsKey(swap[0]))
				{
					f[swap[0]] = new HashSet<int>() { swap[0] };
				}
				if (!f.ContainsKey(swap[1]))
				{
					f[swap[1]] = new HashSet<int>() { swap[1] };
				}

				HashSet<int> hs0 = f[swap[0]];
				HashSet<int> hs1 = f[swap[1]];

				foreach (int x in hs0)
				{
					f[x] = hs1;
					hs1.Add(x);
				}
			}

			char[] newString = str.ToArray<char>();
			for (int i = 0; i < str.Length; i++)
			{
				// No swaps for this guy.
				if (!f.ContainsKey(i + 1))
				{
					continue;
				}

				HashSet<int> swapIndices = f[i + 1];
				int best = i;
				foreach (int swapIndex in swapIndices)
				{
					int realSwapIndex = swapIndex - 1;
					if (realSwapIndex > i && newString[best] < newString[realSwapIndex])
					{
						best = realSwapIndex;
					}
				}

				if (best != i)
				{
					char temp = newString[i];
					newString[i] = newString[best];
					newString[best] = temp;
				}
			}

			return new string(newString);
		}

		public static string[] frameGenerator(int n)
		{
			return new string[1];
		}

		static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
		{
			if (length == 1) return list.Select(t => new T[] { t });

			return GetCombinations(list, length - 1)
				.SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
		}

		static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count)
		{
			int i = 0;
			foreach (var item in items)
			{
				if (count == 1)
					yield return new T[] { item };
				else
				{
					foreach (var result in GetPermutations(items.Skip(i + 1), count - 1))
						yield return new T[] { item }.Concat(result);
				}

				++i;
			}
		}


		public static int possibleSums(int[] coins, int[] quantity)
		{
			var seen = new HashSet<int>() { 0 };

			for (int i = 0; i < coins.Length; i++)
			{
				foreach (int s in seen.ToList())
				{
					for (int q = 1; q <= quantity[i]; q++)
					{
						seen.Add(s + q * coins[i]);
					}
				}
			}

			return seen.Count - 1;
		}


		/*
		 * 1391. Check if there is a valide path in a grid
		 * Given a m x n grid. Each cell of the grid represents a street. The street of grid[i][j] can be:
			1 which means a street connecting the left cell and the right cell.
			2 which means a street connecting the upper cell and the lower cell.
			3 which means a street connecting the left cell and the lower cell.
			4 which means a street connecting the right cell and the lower cell.
			5 which means a street connecting the left cell and the upper cell.
			6 which means a street connecting the right cell and the upper cell.
		 * 
		 * 
		 */
		public static bool HasValidPath(int[][] grid)
		{
			int m = grid.Length, n = grid[0].Length;
			bool[][] g = new bool[m * 3][];
			for (int i = 0; i < m; i++)
			{
				g[i * 3] = new bool[n * 3];
				g[i * 3 + 1] = new bool[n * 3];
				g[i * 3 + 2] = new bool[n * 3];
				for (int j = 0; j < n; j++)
				{
					int val = grid[i][j];
					g[i * 3 + 1][j * 3 + 1] = true;
					g[i * 3 + 0][j * 3 + 1] = val == 2 || val == 5 || val == 6;
					g[i * 3 + 2][j * 3 + 1] = val == 2 || val == 3 || val == 4;
					g[i * 3 + 1][j * 3 + 0] = val == 1 || val == 3 || val == 5;
					g[i * 3 + 1][j * 3 + 2] = val == 1 || val == 4 || val == 6;
				}
			}
			return dfs(g, 1, 1);
		}

		private static bool dfs(bool[][] g, int i, int j)
		{
			if (i < 0 || j < 0 || i > g.Length - 1 || j > g[0].Length - 1 || !g[i][j])
				return false;
			if (i == g.Length - 2 && j == g[0].Length - 2)
				return true;
			g[i][j] = false;
			return dfs(g, i - 1, j) || dfs(g, i + 1, j) || dfs(g, i, j - 1) || dfs(g, i, j + 1);
		}

		static int longestPath(string input)
		{

			var lines = input.Split('\f');
			var max = 0;

			var pathLengthMap = new Dictionary<int, int>();
			pathLengthMap.Add(0, 0);

			for (int i = 0; i < lines.Length; i++)
			{

				var line = lines[i];
				var name = line.TrimStart('\t');
				var depth = line.Length - name.Length + 1;
				var length = pathLengthMap[depth - 1];
				length += name.Length;

				// if is file
				if (name.Contains('.'))
				{
					max = Math.Max(max, length);
				}
				else // if is not file
				{
					length++;

					if (pathLengthMap.ContainsKey(depth))
						pathLengthMap[depth] = length;
					else
						pathLengthMap.Add(depth, length);
				}

			}
			return max;
		}

		static IEnumerable composeRanges(int[] nums)
		{
			for (int i = 0; i < nums.Length; i++)
			{
				int c = nums[i];
				while (i + 1 < nums.Length && nums[i] + 1 == nums[i + 1]) i++;
				if (c != nums[i])
					yield return c + "->" + nums[i];
				else
					yield return c + "";
			}
		}


		/*
		 * CodeSignal
		 * 
		 * 
		 * 
		 */
		public static int segmentsWithSum(int[] a, int m, int sum)
		{

			List<int> lst = new List<int>();
			int i = 0, j = 0, cal = 0;

			while ( i < a.Length - (m-1))
			{
				j = 0;
				while(j < m)
				{
					lst.Add(a[i+j]);
					j++;
				}

				cal += countSegmentsListWithTarget(lst, sum);
				lst = new List<int>();

				i++;
			}

			return cal;
		}

		private static int countSegmentsListWithTarget(List<int> lst, int target)
		{
			int segmentNum = 0;
			int index = 0;

			while(index < lst.Count - 1)
			{
				if( lst.ElementAt(index) + lst.ElementAt(index+1) >=target)
				{
					segmentNum += 1;
				}
				index++;
			}

			return segmentNum;
		}

		public static IList<IList<string>> SolveNQueens(int n)
		{
			var result = new List<IList<string>>();
			DFS(new int[n], 0, result);
			return result;
		}

		private static void DFS(int[] board, int n, List<IList<string>> list)
		{
			if (n == board.Length)
			{
				list.Add(ConvertToResult(board));
				return;
			}

			for (int i = 0; i < board.Length; i++)
			{
				if (IsValid(board, n, i))
				{
					board[n] = i;
					DFS(board, n + 1, list);
				}
			}
		}

		private static bool IsValid(int[] board, int index, int val)
		{
			for (int i = 0; i < index; i++)
				if (i + board[i] == val + index || i - board[i] == index - val || board[i] == val)
					return false;
			return true;
		}

		private static IList<string> ConvertToResult(int[] board)
		{
			var result = new List<string>();
			for (int i = 0; i < board.Length; i++)
			{
				var charArray = Enumerable.Repeat('.', board.Length).ToArray();
				charArray[board[i]] = 'Q';
				result.Add(new string(charArray));
			}

			return result;
		}

		public static long digitAnagrams(int[] a)
		{

			int len = a.Length;
			int pairNum = 0;

			if (len == 1) return 0;

			int i = 0;
			int j = 0;

			while (i < len)
			{
				j = i + 1;

				while (j < len)
				{

					if (isDigitAnagrams(a[i], a[j]))
					{
						pairNum += 1;
					}

					j++;
				}

				i++;
			}

			return pairNum;
		}

		static bool isDigitAnagrams(int a, int b)
		{

			int alen = a.ToString().Length;
			int blen = b.ToString().Length;
			bool isDigitaAnagram = true;

			if (alen == 1 && blen == 1) return a == b;

			if (alen != blen) return false;

			string s = a.ToString();

			SortedList<int, char> dic1 = new SortedList<int, char>();
			SortedList<int, char> dic2 = new SortedList<int, char>();

			int i = 0;
			while(i < alen)
			{
				dic1.Add(i, a.ToString()[i]);
				i++;
			}

			i = 0;
			while (i < alen)
			{
				dic2.Add(i, b.ToString()[i]);
				i++;
			}

			var order1 = dic1.OrderBy(kvp => kvp.Value);
			var order2 = dic2.OrderBy(kvp => kvp.Value);

			for (int j = 0; j < dic1.Count; j++)
			{
				if (order1.ElementAt(j).Value != order2.ElementAt(j).Value)
				{
					isDigitaAnagram = false;
					break;
				}
			}


			return isDigitaAnagram;
		}

		/*
		 * Leetcode 394. Decode String
		 * The encoding rule is: k[encoded_string], where the encoded_string inside the square brackets is being repeated exactly k times. 
		 * Note that k is guaranteed to be a positive integer.
		 * Input: s = "3[a]2[bc]"
		 * Output: "aaabcbc"
		 */
		public static string decodeBracketsString(string s)
		{
			Stack<int> numstack = new Stack<int>();
			Stack<string> strstack = new Stack<string>();

			//parse the string;
			int i = 0;
			string str = "";
			while (i < s.Length)
			{
				if (Char.IsDigit(s[i]))
				{
					int num = 0;
					while (i < s.Length && Char.IsDigit(s[i]))
					{
						num = num * 10 + (s[i] - '0');
						i++;
					}

					numstack.Push(num);
					strstack.Push(str);

					str = "";
				}
				else if (Char.IsLetter(s[i]))
				{
					while (i < s.Length && Char.IsLetter(s[i]))
					{
						str += s[i];
						i++;
					}
				}
				else if (s[i] == ']')
				{
					//take the string from the stack out,
					StringBuilder temp = new StringBuilder(strstack.Pop());
					int multipler = numstack.Pop();
					//str has the current string for the multiplier
					for (int j = 0; j < multipler; j++)
					{
						temp.Append(str);
					}
					//switch the str with temp;
					str = temp.ToString();
					i++;
				}
				else
					i++;

			}

			return str; //it has the full string;
		}

		int segmentsCovering(int[][] segments)
		{

			int len = segments.Length;

			if (len == 1) return 1;

			Array.Sort(segments, (a, b) => { return a[0] - b[0]; });

			int result = 0;
			bool[] used = new bool[len];

			int i = 0;

			while (i < len)
			{

				used[i] = true;
				if (i + 1 < len && segments[i][1] <= segments[i + 1][0] && used[i + 1] == false)
				{
					used[i + 1] = true;
					result++;
				}

				i++;
			}

			return result;
		}

		public static string processBracketsDuplicate(string str)
		{
			int len = str.Length;

			if (len <= 1) return str;

			Stack<char> st = new Stack<char>();

			int starti = 0, endi = 0;
			int i = 0;

			string temp = "";
			int repeatNub = 0;
			int brascketNub = 0;

			while (i < len && str.IndexOf('[') != -1)
			{
				//Console.WriteLine("i = " + i);

				if (str[i] == '[')
				{
					st.Push('[');

					if (brascketNub == 0)
					{
						starti = i + 1;
						repeatNub = Int16.Parse(str[i - 1].ToString());
					}

					brascketNub += 1;
					//Console.WriteLine(string.Format("stack push [ at {0}, and starti = {1}, repeatNub = {2}", i, starti, repeatNub));
				}

				if (str[i] == ']')
				{
					st.Pop();
					endi = i;

					//Console.WriteLine(string.Format("stack pop ] at {0}, and endi = {1}", i, endi));

					if (st.Count == 0)
					{

						temp = str.Substring(0, starti - 2);

						int j = 0;
						while (j < repeatNub)
						{
							if (starti == endi)
								temp += str.Substring(starti, 1);
							else
								temp += str.Substring(starti, endi-starti);
							j++;
						}

						temp += str.Substring(endi + 1);

						repeatNub = 0;
						brascketNub = 0;
						str = temp;
						len = str.Length;
						i = 0;
						continue;
					}
				}



				i++;
			}

			return str;
		}

		public static string ReformatDate(string date)
		{

			// time: O(1)
			// String.Split() takes O(N). But the input string date contains at most 13 chars, so the time complexity is O(13) = O(1).
			string[] strs = date.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

			// time: O(1)
			// StringBuilder.Append takes O(1) to append a character at the end and takes O(N) to append a string.
			// year only contains 4 chars, so the time complexity is O(4) = O(1)
			StringBuilder sb = new StringBuilder();
			sb.Append(strs[2]);
			sb.Append("-");

			// time: O(1)
			// search in a dictionary takes O(1)
			// space O(1)
			// dictionary contains 12 key-value paris, which has O(12) = O(1) space complexity.
			// Set month string
			Dictionary<string, string> months = new Dictionary<string, string>()
			{{"Jan", "01"}, {"Feb", "02"}, {"Mar", "03"}, {"Apr", "04"}, {"May","05"}, {"Jun", "06"},
			{"Jul", "07"}, {"Aug", "08"}, {"Sep", "09"}, {"Oct", "10"}, {"Nov", "11"}, {"Dec", "12"}};
			sb.Append(months[strs[1]]);
			sb.Append("-");

			// time: O(1)
			// String.Substring() takes O(N) time complexity. day contains at most 4 chars, so the time complexity is O(1)
			string day = strs[0].Substring(0, strs[0].Length - 2);
			if (day.Length == 1)
				day = "0" + day;
			sb.Append(day);

			return sb.ToString();
		}

		protected static void backtrack(int remain, int k, LinkedList<int> comb, int next_start, List<IList<int>> results)
		{

			if (remain == 0 && comb.Count == k)
			{

				results.Add(new List<int>(comb));
				return;

			}
			else if (remain < 0 || comb.Count == k)
			{

				return;
			}

			for (int i = next_start; i < 9; ++i)
			{
				comb.AddLast(i + 1);
				backtrack(remain - i - 1, k, comb, i + 1, results);
				comb.RemoveLast();
			}

		}

		public static IList<IList<int>> CombinationSum3(int k, int n)
		{
			List<IList<int>> results = new List<IList<int>>();
			LinkedList<int> comb = new LinkedList<int>();

			backtrack(n, k, comb, 0, results);

			return results;
		}

		public static IList<IList<int>> PermutUnique(int[] nums)
		{
			List<IList<int>> results = new List<IList<int>>();
			LinkedList<int> perm = new LinkedList<int>();
			Dictionary<int, int> dic = new Dictionary<int, int>();

			foreach(int n in nums)
			{
				if (!dic.ContainsKey(n))
				{
					dic.Add(n, 1);
				}
				else
				{
					dic[n] += 1;
				}
			}

			backtrackUnique(nums, results, perm,  dic);

			return results;
		}

		private static void backtrackUnique(int[] nums, List<IList<int>> results, LinkedList<int> permutation, Dictionary<int, int> dic)
		{
			if (permutation.Count == nums.Count())
			{
				results.Add(new List<int>(permutation));
				return;
			}

			for (int i = 0; i < dic.Count; i++)
			{
				if (dic.ElementAt(i).Value > 0)
				{
					//make a choice
					permutation.AddLast(dic.ElementAt(i).Key);
					dic[dic.ElementAt(i).Key] -= 1;

					backtrackUnique(nums, results, permutation, dic);

					//undo the choice
					dic[dic.ElementAt(i).Key] += 1;
					permutation.RemoveLast();
				}
			}


		}

		public static IList<IList<int>> Permute(int[] nums)
		{
			List<IList<int>> results = new List<IList<int>>();
			LinkedList<int> comb = new LinkedList<int>();
			bool[] used = new bool[nums.Length];

			backtrackPermutaion(nums, results, comb, used);


			return results;
		}

		private static void backtrackPermutaion(int[] nums, List<IList<int>> results, LinkedList<int> permutation, bool[] used)
		{

			if(permutation.Count == nums.Count())
			{
				results.Add(new List<int>(permutation));
				return;
			}

			for(int i = 0; i < nums.Length; i++)
			{
				if (!used[i])
				{
					//make a choice
					used[i] = true;
					permutation.AddLast(nums[i]);
					backtrackPermutaion(nums, results, permutation, used);
					//undo the choice
					used[i] = false;
					permutation.RemoveLast();

				}
			}
		}


		public static IList<IList<int>> Subsets(int[] nums)
		{
			List<IList<int>> result = new List<IList<int>>();
			LinkedList<int> comb = new LinkedList<int>();

			backtrackSubsets(0, nums, comb, result);

			return result;
		}

		private static void backtrackSubsets(int i, int[] nums, LinkedList<int> subset, List<IList<int>> result)
		{
			if (i >= nums.Length)
			{
				result.Add(new List<int>(subset));
				return;
			}


			//make a choice to include nums[i]
			subset.AddLast(nums[i]);
				
			backtrackSubsets(i+1, nums, subset, result);
				
			//undo the choice not include nums[i]
			subset.RemoveLast();
			backtrackSubsets(i + 1, nums, subset, result);

		}

		/*
		 * 
		 * Leetcode 77: Combinations
		 * Given two integers n and k, return all possible combinations of k numbers out of the range [1, n].
		 */
		public IList<IList<int>> Combine(int n, int k)
		{
			List<IList<int>> output = new List<IList<int>>();
			LinkedList<int> curr = new LinkedList<int>();

			backtrack(1, curr, output, n, k);

			return output;
		}

		public void backtrack(int first, LinkedList<int> comb, List<IList<int>> output, int n, int k)
		{

			// if the combination is done
			if (comb.Count == k)
				output.Add(new List<int>(comb));

			for (int i = first; i < n + 1; ++i)
			{
				// add i into the current combination
				comb.AddLast(i);

				// use next integers to complete the combination
				backtrack(i + 1, comb, output, n, k);

				// backtrack
				comb.RemoveLast();
			}
		}

		/*
		 * Leetcode 22: Generate Parentheses
		 * Given n pairs of parentheses, write a function to generate all combinations of well-formed parentheses.
		 * 
		 * 
		 */
		public static IList<string> GenerateParenthesis(int n)
		{
			List<string> res = new List<string>();

			backtrack(res, "", 0, 0, n);

			return res;
		}

		public static void backtrack(List<string> res, string cur, int open, int close, int max)
		{
			if (cur.Length == 2 * max)
			{
				res.Add(cur);
				return;
			}

			if (open < max) backtrack(res, cur + "(", open + 1, close, max);

			if (close < open) backtrack(res, cur + ")", open, close + 1, max);

		}


		/*
		 * Leetcode 207: Course Schedule
		 * 
		 * 
		 */
		public static bool CanFinish(int numCourses, int[][] prerequisites)
		{

			IDictionary<int, List<int>> coursesMap = new Dictionary<int, List<int>>();
			HashSet<int> visited = new HashSet<int>();
			HashSet<int> completed = new HashSet<int>();
			BuildMap();
			for (int i = 0; i < numCourses; i++)
			{
				if (!visited.Contains(i))
				{
					if (!dfs(i))
						return false;
				}
			}

			return true;


			void BuildMap()
			{

				for (int i = 0; i < numCourses; i++)
				{
					coursesMap.Add(i, new List<int>());
				}

				foreach (int[] course in prerequisites)
				{
					int courseToTake = course[0];
					int courseDependOn = course[1];
					coursesMap[courseToTake].Add(courseDependOn);
				}
			}

			bool dfs(int course)
			{

				visited.Add(course);
				IList<int> coursesToCompletedList = coursesMap[course];
				foreach (int c in coursesToCompletedList)
				{

					if (!visited.Contains(c))
					{
						if (!dfs(c)) return false;
					}
					if (!completed.Contains(c))
					{
						return false;
					}
				}
				completed.Add(course);
				return true;
			}
		}

		/*
		 * Leetcode 261: Graph valid tree
		 * Input: n = 5, edges = [[0,1],[0,2],[0,3],[1,4]]
		 * Output: true
		 */
		public bool ValidTree(int n, int[][] edges)
		{

			if (n == 0)
				return true;

			IDictionary<int, List<int>> adj = new Dictionary<int, List<int>>();

			for (int i = 0; i < n; i++)
			{
				adj.Add(i, new List<int>());
			}

			foreach (int[] edge in edges)
			{
				int n1 = edge[0];
				int n2 = edge[1];
				adj[n1].Add(n2);
				adj[n2].Add(n1);
			}

			HashSet<int> visit = new HashSet<int>();

			bool dfs(int i, int prev)
			{
				if (visit.Contains(i))
					return false;

				visit.Add(i);
				foreach (int j in adj[i])
				{
					if (j == prev)
						continue;
					if (!dfs(j, i))
						return false;
				}

				return true;
			}

			return dfs(0, -1) && n == visit.Count;

		}

		/*
		 * Leetcode 323. Number of Connected Components in an Undirected Graph
		 * Input: n = 5, edges = [[0,1],[1,2],[3,4]]
		 * Output: 2
		 */
		public static int CountComponents(int n, int[][] edges)
		{
			int[] par = Enumerable.Range(0, n).ToArray();
			int[] rank = Enumerable.Repeat(1, n).ToArray();

			int find(int n1)
			{
				int res = n1;

				while (res != par[res])
				{
					par[res] = par[par[res]];
					res = par[res];
				}

				return res;
			}

			int union(int n1, int n2)
			{

				int p1 = find(n1), p2 = find(n2);

				if (p1 == p2)
					return 0;

				if (rank[p2] > rank[p1])
				{
					par[p1] = p2;
					rank[p2] += rank[p1];
				}
				else
				{
					par[p2] = p1;
					rank[p1] += rank[p2];
				}

				return 1;
			}

			int result = n;
			for (int i = 0; i < edges.Length; i++)
			{
				result -= union(edges[i][0], edges[i][1]);
			}

			return result;
		}

		/*
		 * Leetcode 417: Pacific Atlantic Water Flow
		 * 
		 * 
		 * 
		 */
		public IList<IList<int>> PacificAtlantic(int[][] heights)
		{
			int ROWS = heights.Length, COLS = heights[0].Length;

			HashSet<Position> pac = new HashSet<Position>();
			HashSet<Position> atl = new HashSet<Position>();


			void dfs(int r, int c, HashSet<Position> visit, int prevHeight)
			{
				Position p = new Position();
				p.row = r;
				p.column = c;

				if (visit.Contains(p) || r < 0 || c < 0 || r == ROWS || c == COLS
				  || heights[r][c] < prevHeight)
					return;

				visit.Add(p);
				dfs(r + 1, c, visit, heights[r][c]);
				dfs(r - 1, c, visit, heights[r][c]);
				dfs(r, c + 1, visit, heights[r][c]);
				dfs(r, c - 1, visit, heights[r][c]);
			}

			for (int c = 0; c < COLS; c++)
			{
				dfs(0, c, pac, heights[0][c]);
				dfs(ROWS - 1, c, atl, heights[ROWS - 1][c]);
			}

			for (int r = 0; r < ROWS; r++)
			{
				dfs(r, 0, pac, heights[r][0]);
				dfs(r, COLS - 1, atl, heights[r][COLS - 1]);
			}

			List<IList<int>> res = new List<IList<int>>();

			for (int r = 0; r < ROWS; r++)
			{
				for (int c = 0; c < COLS; c++)
				{
					Position p = new Position();
					p.row = r;
					p.column = c;
					if (pac.Contains(p) && atl.Contains(p))
					{
						res.Add(new List<int>() { r, c });
					}
				}

			}

			return res;

		}


		public struct Position
		{
			public int row;
			public int column;
		}

		/*
		 * Leetcode 128: Longest Consecutive Sequence
		 * Given an unsorted array of integers nums, return the length of the longest consecutive elements sequence.
		 * 
		 * Input: nums = [100,4,200,1,3,2]
		 * Output: 4
		 * Explanation: The longest consecutive elements sequence is [1, 2, 3, 4]. Therefore its length is 4.
		 */
		public static int LongestConsecutive(int[] nums)
		{
			//Generate sequence hash set
			var numSet = new HashSet<int>(nums);

			int longest = 0;

			for (int i = 0; i < nums.Length; i++)
			{
				//check if its the start of a sequence
				if (!numSet.Contains(nums[i] - 1))
				{
					int length = 0;
					while (numSet.Contains(nums[i] + length))
					{
						length++;
					}

					longest = Math.Max(longest, length);
				}
			}

			return longest;


		}

		static bool canFormPalindrome(string str)
		{
			HashSet<char> set = new HashSet<char>();

			int i = 0;

			while (i < str.Length)
			{
				if (set.Contains(str[i]))
				{
					set.Remove(str[i]);
				}
				else
				{
					set.Add(str[i]);
				}

				i++;
			}

			return set.Count <= 0 ? true : false;

		}

		/*
		 * Leetcode 131: Palindrome Partitioning
		 * Given a string s, partition s such that every substring of the partition is a palindrome. Return all possible palindrome partitioning of s.
		 * Input: s = "aab"
		 * Output: [["a","a","b"],["aa","b"]]
		 */
		public static IList<IList<string>> PalindromePartition(string s)
		{

			List<IList<string>> res = new List<IList<string>>();
			LinkedList<string> part = new LinkedList<string>();

			dfs(0);

			return res;

			void dfs(int startIndex)
			{
				if (startIndex >= s.Length)
				{
					res.Add(new List<string>(part));
					return;
				}

				for (int j = startIndex; j < s.Length; j++)
				{
					var sub = s.Substring(startIndex, j - startIndex + 1);
					if (isPali(sub))
					{
						// Add choice
						part.AddLast(sub);

						dfs(j + 1);

						// Remove choice
						part.RemoveLast();
					}
				}
			}
		}

		private static bool isPali(string str)
		{
			for (int i = 0, j = str.Length - 1; i < j;)
			{
				if (str[i++] != str[j--]) return false;
			}
			return true;
		}

		/*
		 * Leetcode 17: Letter Combinations of a Phone Number
		 * 
		 * Input: digits = "23"
		 * Output: ["ad","ae","af","bd","be","bf","cd","ce","cf"]
		 * 
		 */
		private Dictionary<char, string> digitToChar = new Dictionary<char, string>{
		{'2', "abc"}, {'3', "def"}, {'4', "ghi"}, {'5', "jkl"},
		{'6', "mno"}, {'7', "pqrs"}, {'8', "tuv"}, {'9', "wxyz"}};

		public IList<string> LetterCombinations(string digits)
		{

			List<string> res = new List<string>();


			if (digits.Length != 0)
				backtrack(0, "");

			return res;

			void backtrack(int i, string curStr)
			{
				if (curStr.Length == digits.Length)
				{
					res.Add(curStr);
					return;
				}

				foreach (char c in digitToChar[digits[i]])
				{

					backtrack(i + 1, curStr + c);

				}

			}
		}

		/**
		 * Leetcode 31: Next Permutation
		 * Input: nums = [1,2,3]
		 * Output: [1,3,2]
		 */
		public static void NextPermutation(int[] nums)
		{
			int i = nums.Length - 2;
			while (i >= 0 && nums[i + 1] <= nums[i])
			{
				i--;
			}

			if (i >= 0)
			{
				int j = nums.Length - 1;
				while (nums[j] <= nums[i])
				{
					j--;
				}
				swap(nums, i, j);
			}

			reverse(nums, i + 1);
		}

		private static void reverse(int[] nums, int start)
		{
			int i = start, j = nums.Length - 1;
			while (i < j)
			{
				swap(nums, i, j);
				i++;
				j--;
			}
		}

		private static void swap(int[] nums, int i, int j)
		{
			int temp = nums[i];
			nums[i] = nums[j];
			nums[j] = temp;
		}

		/*
		 * Leetcode: Count and Say
		 * countAndSay(1) = "1"
		 * countAndSay(n) is the way you would "say" the digit string from countAndSay(n-1), which is then converted into a different digit string.
		 * 
		 */
		public static string CountAndSay(int n)
		{
			string finalstr = "1";

			if (n == 1)
				return finalstr;

			int characterPointer = 0, countPointer = 0;
			string strInprocess = "";

			while (n > 1)
			{
				while (countPointer < finalstr.Length)
				{

					while (characterPointer < finalstr.Length
						   && countPointer < finalstr.Length
						   && finalstr[characterPointer] == finalstr[countPointer])
					{
						countPointer++;
					}

					strInprocess += (countPointer - characterPointer).ToString();
					strInprocess += finalstr[characterPointer];
					characterPointer = countPointer;
				}

				finalstr = strInprocess;
				strInprocess = "";
				characterPointer = 0;
				countPointer = 0;
				n--;
			}

			return finalstr;
		}


		/*Leetcode 40: Combination Sum II
		 * 
		 * Each number in candidates may only be used once in the combination.
		 */
		public static IList<IList<int>> CombinationSum2(int[] candidates, int target)
		{
			List<IList<int>> result = new List<IList<int>>();
			LinkedList<int> cur = new LinkedList<int>();

			Array.Sort(candidates);

			backtrackCombinationsSumUnique(candidates, target, cur, 0, result);

			return result;
		}

		private static void backtrackCombinationsSumUnique(int[] candidates, int target, LinkedList<int> comb, int starti, List<IList<int>> result)
		{
			if (target == 0)
			{
				result.Add(new List<int>(comb));
				return;
			}

			for(int i = starti; i < candidates.Length; i++)
			{
				if (i != starti && candidates[i] == candidates[i - 1])
					continue;

				if (candidates[i] > target)
					break;

				//make a choice
				comb.AddLast(candidates[i]);

				backtrackCombinationsSumUnique(candidates, target - candidates[i], comb, i+1, result);

				//undo the choice
				comb.RemoveLast();
			}
			
		}


		public static List<int> MaxRepeating(string sequence, string[] words)
		{
			List<int> res = new List<int>();

			foreach(string word in words)
			{
				string test = word;
				int max = 0;
				while (true)
				{
					if (sequence.Contains(test))
					{
						max++;
						test += word;
					}
					else
						break;
				}

				res.Add(max);
			}

			
			return res;
		}

		public static IList<IList<int>> combineTheGivenNumber(int[] candidates, int target)
		{
			List<IList<int>> result = new List<IList<int>>();
			LinkedList<int> cur = new LinkedList<int>();
			string total = "";

			backtrackTheGivenNumber(0, candidates, cur, total, target, result);

			return result;

		}

		private static void backtrackTheGivenNumber(int starti, int[] candidates, LinkedList<int> cur, string total, int target, List<IList<int>> result)
		{
			if (total.ToString().Equals(target.ToString()))
			{
				result.Add(new List<int>(cur));
				return;
			}

			for (int i = starti; i < candidates.Length; i++)
			{
				if (i != starti && candidates[i] == candidates[i - 1])
					continue;

				//make a choice
				cur.AddLast(candidates[i]);

				backtrackTheGivenNumber(i+1, candidates, cur, total + candidates[i].ToString(), target,  result);

				//undo the choice
				cur.RemoveLast();
			}
		}


		/*
		 * Leetcode 39:
		 * 
		 */
		public static IList<IList<int>> CombinationSum(int[] candidates, int target)
		{

			List<IList<int>> result = new List<IList<int>>();
			LinkedList<int> cur = new LinkedList<int>();
			int total = 0;

			backtrackCombinations(0, candidates, cur, total, target, result);

			return result;

		}

		private static void backtrackCombinations(int i, int[] candidates, LinkedList<int> cur, int total, int target, List<IList<int>> result)
		{
			
			if (total == target)
			{
				result.Add(new List<int>(cur));
				return;
			}

			if (i >= candidates.Length || total > target)
				return;

			//Make a choice
			cur.AddLast(candidates[i]);
			backtrackCombinations(i, candidates, cur, total + candidates[i], target, result);

			//undo a choice
			cur.RemoveLast();
			backtrackCombinations(i + 1, candidates, cur, total, target, result);

		}

		/*
		 * Leetcode 79: word search backtracking
		 * Given an m x n grid of characters board and a string word, return true if word exists in the grid.
		 * Input: board = [["A","B","C","E"],["S","F","C","S"],["A","D","E","E"]], word = "ABCCED"
		 * Output: true
		 */
		public static bool Exist(char[][] board, string word)
		{

			int rows = board.Length;
			int columns = board[0].Length;
			bool[][] visited = new bool[rows][];

			for (int i = 0; i < rows; i++)
			{
				visited[i] = new bool[columns];
			}

			for (int i = 0; i < rows; i++)
			{

				for (int j = 0; j < columns; j++)
				{

					if (word[0] == board[i][j] && searchWord(i, j, 0, board, word, visited))
					{
						return true;
					}
				}

			}

			return false;
		}

		public static bool searchWord(int i, int j, int index, char[][] board, string word, bool[][] visited)
		{
			if (index == word.Length)
			{
				return true;
			}

			if (i < 0 || i >= board.Length || j < 0 || j >= board[0].Length
			   || word[index] != board[i][j] || visited[i][j])
			{
				return false;
			}

			visited[i][j] = true;

			if (searchWord(i + 1, j, index + 1, board, word, visited) ||
				searchWord(i - 1, j, index + 1, board, word, visited) ||
				searchWord(i, j + 1, index + 1, board, word, visited) ||
				searchWord(i, j - 1, index + 1, board, word, visited))
			{
				return true;
			}

			visited[i][j] = false;

			return false;
		}

		/*
		 * Leetcode 43: Multiply Strings
		 * 
		 * 
		 */
		public static string Multiply(string num1, string num2)
		{
			int m = num1.Length, n = num2.Length;
			int[] vals = new int[m + n];

			for (int i = m - 1; i >= 0; --i)
			{

				for (int j = n - 1; j >= 0; --j)
				{
					int mul = (num1[i] - '0') * (num2[j] - '0');
					int sum = vals[i + j + 1] + mul;
					vals[i + j] = vals[i + j] + sum / 10;  //carry, by example  sum = 15, we save 1 to the first array, 5 save to the second array
					vals[i + j + 1] = sum % 10;            //actually value
				}
			}

			StringBuilder sb = new StringBuilder();
			foreach (int val in vals)
			{
				if (sb.Length != 0 || val != 0)
					sb.Append(val);
			}

			return sb.Length == 0 ? "0" : sb.ToString();
		}

		/**
		 * Leetcode 49: Group Anagrams
		 * Given an array of strings strs, group the anagrams together. You can return the answer in any order.
		 */
		public IList<IList<string>> GroupAnagrams(string[] strs)
		{

			List<IList<string>> result = new List<IList<string>>();

			Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

			foreach (string str in strs)
			{
				string sorted = getSortedString(str);

				if (!map.ContainsKey(sorted))
				{
					map.Add(sorted, new List<string>());
				}

				map[sorted].Add(str);
			}

			foreach (KeyValuePair<string, List<string>> kvp in map)
			{
				result.Add(kvp.Value);
			}

			return result;

		}

		public string getSortedString(string s1)
		{
			char[] c1 = s1.ToCharArray();

			Array.Sort(c1);

			s1 = String.Join("", c1);

			return s1;
		}

		/*
		 * Leetcode 50: Pow(x, n)
		 * Implement pow(x, n), which calculates x raised to the power n (i.e., xn).
		 */
		public static double MyPow(double x, int n)
		{
			bool isNegative = false;
			if (n < 0)
			{
				x = 1 / x;
				isNegative = true;
				n = -(n + 1);
			}

			double ans = 1;
			double tmp = x;
			while (n != 0)
			{
				if (n % 2 == 1)
				{
					ans *= tmp;
				}
				tmp *= tmp;
				n /= 2;
			}

			if (isNegative)
			{
				ans *= x;
			}


			return ans;
		}

		public static double MyPow2(double num, int power)
		{
			if (power == 0 || num == 1) return 1;
			if (power == 1) return num;

			//use -(power+1) to avoid MIN_Value case
			if (power < 0) return 1 / (num * MyPow2(num, -(power + 1)));

			double res = 1;

			while(power > 1)
			{
				if(power %2 == 1)
				{
					res *= num;
				}
				num *= num;
				power /= 2;
			}

			res *= num;

			return res;

		}

		/*
		 * Leetcode 55. Jump game
		 * Given an array of non-negative integers nums, you are initially positioned at the first index of the array.
		 * Each element in the array represents your maximum jump length at that position.
		 * Determine if you are able to reach the last index.
		 */
		public static bool CanJump(int[] nums)
		{
			int lastGoodIndexPosition = nums.Length - 1;

			for (int i = nums.Length - 1; i >= 0; i--)
			{

				if (i + nums[i] >= lastGoodIndexPosition)
				{
					lastGoodIndexPosition = i;
				}
			}

			return lastGoodIndexPosition == 0;
		}

		/*
		 * Leetcode 1871: Jump Game VII
		 * You are given a 0-indexed binary string s and two integers minJump and maxJump. 
		 * In the beginning, you are standing at index 0, which is equal to '0'. You can move from index i to index j if the following conditions are fulfilled:
		 * Input: s = "011010", minJump = 2, maxJump = 3
		 * Output: true
		 * Explanation:
		 * In the first step, move from index 0 to index 3. 
		 * In the second step, move from index 3 to index 5.
		 * 
		 */
		public static bool CanReach(string s, int minJump, int maxJump)
		{
			if (s[s.Length - 1] == '1') return false;

			Queue<int> q = new Queue<int>();
			q.Enqueue(0);
			int farthest = 0;

			while (q.Count > 0)
			{

				int i = q.Dequeue();

				int start = Math.Max(i + minJump, farthest + 1);
				int end = Math.Min(i + maxJump + 1, s.Length);

				for (int j = start; j < end; j++)
				{

					if (s[j] == '0')
						q.Enqueue(j);

					if (j == s.Length - 1)
						return true;
				}

				farthest = i + maxJump;

			}

			return false;

		}


		/*
		 * Leetcode 56: Merge intervals
		 * Given an array of intervals where intervals[i] = [starti, endi], merge all overlapping intervals, 
		 * and return an array of the non-overlapping intervals that cover all the intervals in the input.
		 * 
		 */
		public static int[][] Merge(int[][] intervals)
		{
			if (intervals.Length == 1)
			{
				return intervals;
			}

			Array.Sort(intervals, (a, b) => { return a[0] - b[0]; });

			List<int[]> output_arr = new List<int[]>();

			int[] current_interval = intervals[0];
			output_arr.Add(current_interval);

			foreach (int[] interval in intervals)
			{
				int current_begin = current_interval[0];
				int current_end = current_interval[1];
				int next_begin = interval[0];
				int next_end = interval[1];

				if (current_end >= next_begin)
				{
					current_interval[1] = Math.Max(current_end, next_end);
				}
				else
				{
					current_interval = interval;
					output_arr.Add(current_interval);
				}

			}


			return output_arr.ToArray();
		}

		public static int[][] Insert(int[][] intervals, int[] newInterval)
		{
			List<int[]> res = new List<int[]>();

			int i = 0;
			int n = intervals.Length;

			int nStart = newInterval[0];
			int nEnd = newInterval[1];

			//Sort the intervals first
			Array.Sort(intervals, (a, b) => { return a[0] - b[0]; });

			while (i < n && intervals[i][1] < nStart)
			{
				res.Add(intervals[i]);
				i++;
			}

			if (i == n)
			{
				res.Add(newInterval);
				return res.ToArray();
			}

			nStart = Math.Min(intervals[i][0], nStart);

			while (i < n && intervals[i][0] <= nEnd)
			{
				nEnd = Math.Max(nEnd, intervals[i][1]);
				i++;
			}

			res.Add(new int[] { nStart, nEnd });

			while (i < n)
			{
				res.Add(intervals[i++]);
			}

			return res.ToArray();
		}


		/*
		 * Expedia group coding interview simulate like Leetcode 1046
		 * 
		 * 
		 */
		public static int getStonesWeight(List<int> weights)
		{
			int len = weights.Count;

			if (len == 1) return weights.ElementAt(0);

			int i = len - 1;

			while( i - 1 >= 0)
			{
				if(weights.ElementAt(i) == weights.ElementAt(i-1))
				{
					weights.Remove(weights.ElementAt(i));
					weights.Remove(weights.ElementAt(i-1));
					i--;
				}
				else
				{
					int mins = Math.Abs(weights.ElementAt(i) - weights.ElementAt(i - 1));
					weights[i - 1] = mins;
					weights.Remove(weights.ElementAt(i));
					weights.Sort();
				}
				i--;
			}

			return weights.Count == 0 ? 0 : weights.ElementAt(0);

		}

		/*Leetcode 1046: Last Stone Weight
		 * Input: [2,7,4,1,8,1]
		 * Output: 1
		 */
		public static int LastStoneWeight(int[] stones)
		{
			int len = stones.Length;

			if (len == 1) return stones[0];

			List<int> weights = new List<int>(stones);

			weights.Sort();

			int i = len - 1;

			while (i - 1 >= 0)
			{
				if (weights.ElementAt(i) == weights.ElementAt(i - 1))
				{
					weights.Remove(weights.ElementAt(i));
					weights.Remove(weights.ElementAt(i - 1));
					i--;
				}
				else
				{
					int mins = Math.Abs(weights.ElementAt(i) - weights.ElementAt(i - 1));
					weights[i - 1] = mins;
					weights.Remove(weights.ElementAt(i));
					weights.Sort();
				}
				i--;
			}

			return weights.Count == 0 ? 0 : weights.ElementAt(0);

		}

		/*
		 * Leetcode 71: Simplify Path
		 * path = "/home//foo/"
		 * "/home/foo"
		 */
		public static string SimplifyPath(string path)
		{
			if (path == "")
				return "/";

			string[] p = path.Split('/');
			Stack<string> stack = new Stack<string>();

			for (int i = 0; i < p.Length; i++)
			{
				if (p[i] == "" || p[i] == ".")
					continue;
				else if (p[i] == "..")
				{
					if (stack.Count > 0)
					{
						stack.Pop();
						continue;
					}
				}
				else
					stack.Push(p[i]);
			}

			StringBuilder sb = new StringBuilder();
			while (stack.Count > 0)
			{
				sb.Insert(0, "/" + stack.Pop());
			}

			return sb.Length == 0 ? "/" : sb.ToString();
		}

		public static string SimplifyPath2(string path)
		{
			Stack<string> stk = new Stack<string>();

			foreach (string p in path.Split('/'))
			{
				switch (p)
				{
					case ".":
					case "":
						break;
					case "..":
						if (stk.Count > 0)
							stk.Pop();
						break;
					default:
						stk.Push(p);
						break;
				}
			}

			string[] str = new string[stk.Count];
			int len = stk.Count;
			for (int i = len - 1; i >= 0; i--)
			{
				str[i] = stk.Pop();
			}

			return $"/{string.Join("/", str)}";
		}

		static string ComputeSha256Hash(string rawData)
		{
			// Create a SHA256   
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// ComputeHash - returns byte array  
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

				// Convert byte array to a string   
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}

		/*
		 * Leetcode 224: basic calculator
		 * Given a string s representing a valid expression, implement a basic calculator to evaluate it, and return the result of the evaluation.
		 * Input: s = "(1+(4+5+2)-3)+(6+8)"
		 * Output: 23
		 */
		public static int Calculate(string s)
		{
			Stack<int> stack = new Stack<int>();
			int operand = 0;
			int result = 0; // For the on-going result
			int sign = 1;  // 1 means positive, -1 means negative

			for (int i = 0; i < s.Length; i++)
			{

				char ch = s[i];
				if (Char.IsDigit(ch))
				{

					// Forming operand, since it could be more than one digit
					operand = 10 * operand + (int)(ch - '0');

				}
				else if (ch == '+')
				{

					// Evaluate the expression to the left,
					// with result, sign, operand
					result += sign * operand;

					// Save the recently encountered '+' sign
					sign = 1;

					// Reset operand
					operand = 0;

				}
				else if (ch == '-')
				{

					result += sign * operand;
					
					sign = -1;
					operand = 0;

				}
				else if (ch == '(')
				{

					// Push the result and sign on to the stack, for later
					// We push the result first, then sign
					stack.Push(result);
					stack.Push(sign);

					// Reset operand and result, as if new evaluation begins for the new sub-expression
					sign = 1;
					result = 0;

				}
				else if (ch == ')')
				{

					// Evaluate the expression to the left
					// with result, sign and operand
					result += sign * operand;

					// ')' marks end of expression within a set of parenthesis
					// Its result is multiplied with sign on top of stack
					// as stack.pop() is the sign before the parenthesis
					result *= stack.Pop();

					// Then add to the next operand on the top.
					// as stack.pop() is the result calculated before this parenthesis
					// (operand on stack) + (sign on stack * (result from parenthesis))
					result += stack.Pop();

					// Reset the operand
					operand = 0;
				}
			}

			return result + (sign * operand);
		}

		/*
		 * Leetcode 1849: Splitting a String Into Descending Consecutive Values
		 * Input: s = "050043"
		 * Output: true
		 * Explanation: s can be split into ["05", "004", "3"] with numerical values [5,4,3].
		 * The values are in descending order with adjacent values differing by 1.
		 */
		public static bool SplitString(String s)
		{
			char[] chars = s.ToCharArray();
			long value = 0;
			for (int i = 0; i < chars.Length - 1; i++)
			{
				value = value * 10 + chars[i] - '0';
				bool result = dfs(chars, i + 1, value - 1);
				if (result)
				{
					return true;
				}
			}

			return false;

			bool dfs(char[] rest, int start, long target)
			{
				if (start == chars.Length)
				{
					return true;
				}

				long val = 0;
				for (int i = start; i < chars.Length; i++)
				{
					val = val * 10 + chars[i] - '0';
					if (val != target)
					{
						continue;
					}
					else
					{
						bool result = dfs(rest, i + 1, val - 1);
						if (result)
						{
							return true;
						}
					}
				}
				return false;
			}

		}

		static void DoSomething(int i)
		{
			Console.WriteLine(i);
		}

		static double CalculateSomething(int i)
		{
			return (double)i / 2;
		}

		public static int seatsCount(int N, string S)
		{
			if (string.IsNullOrEmpty(S))
				return N * 3;

			string[][] seatMap = new string[N][];

			for(int i = 0; i < N; i++)
			{
				int j = 0;
				seatMap[i] = new string[10];
				while(j < 8)
				{
					seatMap[i][j] = (i + 1).ToString() + Convert.ToChar(Convert.ToInt32('A') + j);
					j++;
				}
				seatMap[i][8] = (i + 1).ToString() + 'J';
				seatMap[i][9] = (i + 1).ToString() + 'K';
			}

			string[] reserved = S.Split(' ');
			Array.Sort(reserved);


			int[][] dp = new int[N][];
			for (int i = 0; i < N; i++)
			{
				dp[i] = new int[3] { 3, 4, 3};
				
			}


			foreach (string r in reserved)
			{
				for (int i = 0; i < N; i++)
				{
					if (seatMap[i].Contains(r))
					{
						if (r.Contains('A') || r.Contains('B') || r.Contains('C'))
						{
							dp[i][0] -= 1;
						}

						if (r.Contains('D') || r.Contains('G'))
						{
							dp[i][1] -= 1;
						}

						if (r.Contains('E') || r.Contains('F'))
						{
							dp[i][1] = 0;
						}

						if ( r.Contains('H') || r.Contains('J') || r.Contains('K'))
						{
							dp[i][2] -= 1;
						}

					}
				}
			}

			int res = 0;

			foreach(int[] row in dp)
			{
				foreach(int sn in row)
				{

					if (sn >= 3)
						res++;
				}

			}

			return res;

		}


		static void Main(string[] args)
		{
			int[][] edges = new int[][] {
				new int[] { 0, 1},
				new int[] { 0, 2},
				new int[] { 3, 4},
			};

			CountComponents(5, edges);

			int[][] courses = new int[][] {
				new int[] { 0, 1 },
				new int[] { 0, 2},
				new int[] { 1, 3},
				new int[] { 1, 4 },
				new int[] { 3, 4},
			};

			CanFinish(5, courses);

			int[][] flights = new int[][] {
				new int[] { 1, 4 },
				new int[] { 2, 6},
				new int[] { 9, 12},
				new int[] { 5, 9 },
				new int[] { 5, 12},
			};

			MeetingRooms(flights);

			seatsCount(2, "1A 2D 2G 1C");

			FourSum(new int[] { 2, 2, 2, 2, 2 }, 8);

			combinatonsOfTwoNumber(new int[] { 3, 5}, 334);

			NumberToWords(1234);


			WordDictionary wordDictionary = new WordDictionary();
			wordDictionary.AddWord("bad");
			wordDictionary.AddWord("dad");
			wordDictionary.AddWord("mad");
			wordDictionary.Search(".ad");



			//Action and Func sample
			Action<int> myAction = new Action<int>(DoSomething);
			myAction(123);           // Prints out "123"
									 // can be also called as myAction.Invoke(123);

			Func<int, double> myFunc = new Func<int, double>(CalculateSomething);
			Console.WriteLine(myFunc(5));   // Prints out "2.5"

			SplitString("050043");

			Calculate("(1+(4+5+2)-3)+(6+8)");

			PalindromePartition("aab");

			Console.WriteLine(canFormPalindrome("mmrrsso"));

			Search(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 0);

			// Polymorphism at work #1: a Rectangle, Triangle and Circle
			// can all be used whereever a Shape is expected. No cast is
			// required because an implicit conversion exists from a derived
			// class to its base class.
			var shapes = new List<Shape>
						{
							new Rectangle(),
							new Triangle(),
							new Circle()
						};

			// Polymorphism at work #2: the virtual method Draw is
			// invoked on each of the derived classes, not the base class.
			foreach (var shape in shapes)
			{
				shape.Draw();
			}
			/* Output:
                Drawing a rectangle
                Performing base class drawing tasks
                Drawing a triangle
                Performing base class drawing tasks
                Drawing a circle
                Performing base class drawing tasks
            */

			// Creates one delegate for each method. For the instance method, an
			// instance (mySC) must be supplied. For the static method, use the
			// class name.
			mySampleClass mySC = new mySampleClass();
			myMethodDelegate myD1 = new myMethodDelegate(mySC.myStringMethod);
			myMethodDelegate myD2 = new myMethodDelegate(mySampleClass.mySignMethod);

			// Invokes the delegates.
			Console.WriteLine("{0} is {1}; use the sign \"{2}\".", 5, myD1(5), myD2(5));
			Console.WriteLine("{0} is {1}; use the sign \"{2}\".", -3, myD1(-3), myD2(-3));
			Console.WriteLine("{0} is {1}; use the sign \"{2}\".", 0, myD1(0), myD2(0));


			SubarraySumII(new int[] { 1, -1, 1, 1, 1, 1, 1, 1 }, 3);

			maximumSubArrayK2(new int[] { 1, 2, 6, 2, 4, 1 }, 3);

			AngleClock(12, 15);


			CanReach("01", 1, 1);

			/*
			 * Parallel computing
			 * 
			 */
			// 2 million
			var limit = 2_000_000;
			var numbers = Enumerable.Range(0, limit).ToList();

			var watch = Stopwatch.StartNew();
			var primeNumbersFromForeach = ParallelExample.GetPrimeList(numbers);
			watch.Stop();

			var watchForParallel = Stopwatch.StartNew();
			var primeNumbersFromParallelForeach = ParallelExample.GetPrimeListWithParallel(numbers);
			watchForParallel.Stop();

			Console.WriteLine($"Classical foreach loop | Total prime numbers : {primeNumbersFromForeach.Count} | Time Taken : {watch.ElapsedMilliseconds} ms.");
			Console.WriteLine($"Parallel.ForEach loop  | Total prime numbers : {primeNumbersFromParallelForeach.Count} | Time Taken : {watchForParallel.ElapsedMilliseconds} ms.");

			//Console.WriteLine("Press any key to exit.");
			//Console.ReadLine();


			string[] serverName = new string[] { "Inidana", "Califolia", "Washionton", "Texas", "Alabama",
			"Alaska", "Arizona", "colorado", "Delaware", "Florida", "Georgia", "Hawaii"};

			Dictionary<int, string> distributionTable = new Dictionary<int, string>();

			foreach(string name in serverName)
			{
				string hs = name.GetHashCode().ToString();
				int lastDigitalHashCode = Int16.Parse(hs.Substring(hs.Length - 1));

				if (distributionTable.ContainsKey(lastDigitalHashCode))
				{
					distributionTable[lastDigitalHashCode] = name;
				}
				else
				{
					distributionTable.Add(lastDigitalHashCode, name);
				}
			}

			//.NET string object has a GetHashCode() function.It returns an integer. Convert it into a hex and then to an 8 characters long string.
		   string hashCode = String.Format("{0:X}", "abcdef".GetHashCode());


		   Console.WriteLine(ComputeSha256Hash("Mahesh"));

			int[][] obstacles = new int[][] {
				new int[] { 0,0,0 },
				new int[] { 0,1,0},
				new int[] { 0,0,0}
			};


			UniquePathsWithObstacles(obstacles);

			int[][] pathsum = new int[][] {
				new int[] { 1,3,1 },
				new int[] { 1,5,1},
				new int[] { 4,2,1}
			};

			MinPathSum(pathsum);

			combineTheGivenNumber(new int[] { 12, 21, 121, 11, 1}, 121121);

			MaxRepeating("ababcbabc", new string[] { "ab", "babc", "bca" });


			FirstMissingPositive(new int[] { 3, 4, -1, 1 });

			UniquePaths(3, 3);

			SimplifyPath("/a/b/c/../..");

			List<int> weights = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 7, 9 };
			getStonesWeight(weights);

			int[][] intervals = new int[][] {
				new int[] { 1, 2 },
				new int[] { 3, 5 },
				new int[] { 6, 7 },
				new int[] { 8, 10 },
				new int[] { 12, 16 },
			};


			int[] newInterval = new int[] { 4, 8 };

			Merge(intervals);

			Insert(intervals, newInterval);


			char[][] islands = new char[][] {
				new char[] { '1','1','1','1','0' },
				new char[] { '1','1','0','1','0' },
				new char[] { '1','1','0','1','0' },
				new char[] { '0', '0', '0', '0', '0'}
			};

			NumIslands2(islands);

			PointSystem ps = new PointSystem();

			Console.WriteLine("getPoint('Bob') ->" + ps.getPoint("Bob"));
			Console.WriteLine("getPoint('Alice') ->" + ps.getPoint("Alice"));
			Console.WriteLine("getPoint('Bob') ->" + ps.getPoint("Bob"));
			Console.WriteLine("getPoint('Alice') ->" + ps.getPoint("Alice"));
			Console.WriteLine("getPoint('Bob') ->" + ps.getPoint("Bob"));
			Console.WriteLine("getPoint('Scott') ->" + ps.getPoint("Scott"));
			Console.WriteLine("getPoint('Alice') ->" + ps.getPoint("Alice"));
			Console.WriteLine("getPoint('Alice') ->" + ps.getPoint("Alice"));

			MyPow(2, -5);

			Multiply("212", "33");

			CombinationSum2(new int[] { 10, 1, 2, 7, 6, 1, 5 }, 8);

			CombinationSum(new int[] { 2, 3, 6,7 }, 7);

			CountAndSay(4);

			NextPermutation(new int[] { 1, 5, 8, 4, 7, 6, 5, 3, 1});

			GenerateParenthesis(3);

			Subsets(new int[] { 1, 2, 3 });

			PermutUnique(new int[] { 1, 1, 2 });

			Permute(new int[] { 1, 2, 3 });

			CombinationSum3(3, 9);


			processBracketsDuplicate("abc2[sad3[z]e]y");

			decodeBracketsString("abc2[sad3[z]e]y");

			countDecreasingSubarrays(new int[] { 9,8,4,3 });

			convert_to_words("9923".ToCharArray());

			MaxProfitSum(new int[] { 7, 1, 5, 3, 6, 4 });

			longestArithmeticSeqLength(new int[] { 20, 1, 15, 3, 10, 5, 8 });

			LongestPalindrome("abbabbaaaag");

			LongestPalindrome2("abbabbaaaag");

			digitAnagrams(new int[] { 25, 35, 872, 228, 53, 278, 872 });

			//removeOneDigital("ab12c", "1zz456");

			removeOneDigital("ab12c", "ab24z");

			segmentsWithSum(new int[] { 2, 4, 2, 7, 1, 6, 1, 1, 1 }, 4, 8);

			int[] incnums = new int[] { 590, 692, 965, 697 };
			makeIncreasing(incnums);

			int[] numscomp = new int[] {-1, 0, 1, 2, 6, 7, 9};

			IEnumerable comp = composeRanges(numscomp);

			string fileSystem = "user\f\tpictures\f\t\tphoto.png\f\t\tcamera\f\tdocuments\f\t\tlectures\f\t\t\tnotes.txt";

			longestPath(fileSystem);

			SubwaySystem sb = new SubwaySystem();
			sb.checkInSystem(1, "station1", 10);
			sb.checkInSystem(2, "station1", 15);

			sb.checkOutSystem(1, "station2", 20);
			sb.checkOutSystem(2, "station2", 37);

			sb.getAverageTravelTime("station1", "station2");

			int[][] path1 = new int[][] { 
				new int[] { 2, 4, 3 }, 
				new int[] { 6, 5, 2 } };

			bool isValidPath = HasValidPath(path1);

			int[] myarrint = new int[] { 1, 2, 2 };
			int[] coins = new int[] { 5, 10, 50 };
			possibleSums(coins, myarrint);


			IEnumerable<IEnumerable<int>> result = GetCombinations(myarrint, myarrint.Length);

			string[] strcom = new string[] { "a", "d", "c" };
			IEnumerable<IEnumerable<string>> result1 = GetPermutations(strcom, 3);

			string email = "a@leetcode.com";

			int atIndex = email.IndexOf('@');
			int dotIndex = email.IndexOf('.');

			string newEmail = email;
			while(dotIndex < atIndex)
			{
				newEmail = newEmail.Remove(dotIndex, 1);
				dotIndex = newEmail.IndexOf('.');
				atIndex = newEmail.IndexOf('@');
			}

			int plusIndex = newEmail.IndexOf('+');
			atIndex = newEmail.IndexOf('@');

			if (plusIndex != -1 && plusIndex < atIndex)
			{
				newEmail = newEmail.Substring(0, plusIndex) + newEmail.Substring(atIndex, newEmail.Length - atIndex);
			}

			HashSet<string> seen = new HashSet<string>();
			char[] pair1 = new char[] { 'a', 'd', 'c' };

			string str = "dznsxamwoj";
			int[][] pairs = new int[][] 
			{ 
				new int[] { 1, 2 }, 
				new int[] { 3, 4 },
				new int[] { 6, 5 },
				new int[] { 8, 10 }
			};

			int[][] diagonal = new int[][]
			{
				new int[] { 1, 3, 9, 4 },
				new int[] { 9, 5, 7, 7 },
				new int[] { 3, 6, 9, 7 },
				new int[] { 1, 2, 2, 2 }
			};

			diagonalsSort(diagonal);

			//swapLexOrder(str, pairs);

			str = "abdc";
			pairs = new int[][]
			{
				new int[] { 1, 4 },
				new int[] { 3, 4 }
			};

			swapLexOrder(str, pairs);


			islexicographically("cat", "Caterpillar");

			string[] word1 = new string[] { " KQDLrdsCpYKWjPCZlEsiCBxl",
											"CtgwAutobmreCj",
											"Ad",
											""};

			lexicographicallyArray(word1.ToList());

			frameGenerator(5);

			string[] dish1 = new string[] { "Salad", "Tomato", "Cucumber", "Salad", "Sauce" };
			string[] dish2 = new string[] { "Pizza", "Tomato", "Sausage", "Sauce", "Dough" };
			string[] dish3 = new string[] { "Quesadilla", "Chicken", "cheese", "Sauce" };
			string[] dish4 = new string[] { "Sandwich", "Salad", "Bread", "Tomato", "cheese" };

			string[][] tables = new string[4][] { dish1, dish2, dish3, dish4 };

			groupingDishes(tables);

			int[] a = new int[] { 1, 2, 4, -1, 6, 1 };

			subarraysCountBySum(a, 3, 6);

			bool[] bounded1= boundedRatio(a, 1, 3);

			int[] a1 = new int[] { 1, 2, 3 };
			int[] b1 = new int[] { 1, 2, 3 };
			countTinyPairs(a1, b1, 31);


			string[] crypt = { "WASD",
							 "IJKL",
							 "AOPAS" };
			char[] char1 = { 'W', '2' };
			char[] char2 = { 'A', '0' };
			char[] char3 = { 'S', '4' };
			char[] char4 = { 'D', '1' };
			char[] char5 = { 'I', '5' };
			char[] char6 = { 'J', '8' };
			char[] char7 = { 'K', '6' };
			char[] char8 = { 'L', '3' };
			char[] char9 = { 'O', '7' };
			char[] char10 = { 'P', '9' };
			char[][] solution = { char1, char2, char3, char4, char5, char6, char7, char8, char9, char10 };
			isCryptSolution(crypt, solution);

			string[] crypt1 = { "AA", "BB", "AA" };
			char[] char11 = { 'A', '1' };
			char[] char12= { 'B', '0' };
			char[][] s2 = { char11, char12 };
			isCryptSolution(crypt1, s2);

			string nonrepeat = "abacabad";

			firstNotRepeatingCharacter(nonrepeat);

			centuryFromYear(1905);

			string pattern = "abba";
			string search = "dog dog dog dog";
			WordPattern(pattern, search);


			int[] a2 = new int[] { 4, 0, 1, -2, 3 };

			mutateTheArray(5, a1);

			a1 = new int[] { 9 };
			mutateTheArray(1, a1);

			int[] a3 = new int[] { -92, -23, 0, 45, 89, 96, 99, 95, 89, 41, -17, -48 };
			alternatingSort(a2);

			a1 = new int[] { 5519, -9407, -4641, 8610, -1167, 4261, -225, 81, -6339, 7896, -6665, 1709, 5391, -442, 5149, 6588, -9343, 6122, 3684, 9536, 6888, 8381, -9398, -7081, 5991, 1692, 7592, -1691, 2206, 2066, -3775, 2952, -1302, -6150, -1833, -86, 741, 8939, -7780, -5996, 7879, -1071, 8691, -1808, 7071, -4549, -1050, -4763, 6330, 7939, 2204, -6655, 3295, -2457, -7963, -8782, -9285, -3996, -4986, 7270, -5131, -6745, 2516, 3127, -2714, -106, -3948, 1977, -537, 4794, 1842, -4911, -4137, 8687, -2991, -9948, -8082, -8479, -6549, 2503, 3659, -7471, 8843, -6879, -6402, 9460, -5010, -9705, 8368, 3946, -8757, -2565, 3394, -1538, -3265, 6109, 9638, 2721, 4664, -6583 };
			a2 = new int[] { -99, -98, -97, -96, -95, -94, -93, -92, -91, -90, -89, -88, -87, -86, -85, -84, -83, -82, -81, -80, -79, -78, -77, -76, -75, -74, -73, -72, -71, -70, -69, -68, -67, -66, -65, -64, -63, -62, -61, -60, -59, -58, -57, -56, -55, -54, -53, -52, -51, -50, -49, -48, -47, -46, -45, -44, -43, -42, -41, -40, -39, -38, -37, -36, -35, -34, -33, -32, -31, -30, -29, -28, -27, -26, -25, -24, -23, -22, -21, -20, -19, -18, -17, -16, -15, -14, -13, -12, -11, -10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0 };
			int[] a4 = new int[] { 384, 47, -509, 971, -202, -71, -554, -729, 893, 937, -281, -258, 799, 17, -257, -34, -959, 286, 128, 624, 186, 995, 899, 103, 180, 328, -177, -746, 911, -472, -186, -588, -740, -62, -255, -896, 482, -867, -320, -857, 656, -881, 789, 83, 109, 640, 154, 657, -56, 443, 335, -683, -3, 825, -163, 841, -491, -932, 301, 970, -893, -513, -815, -911, 991, -788, -359, -955, -670, -579, 166, 531, -399, -43, -802, 453, 631, -73, 782, 313, 734, 86, 522, 417, -348, -891, -851, 57, -733, -505, -112, 98, 439, 921, 606, -530, 78, -580, 581, 312 };

			longestInversionalSubarray(a1, a2, a3);

			string[] worddic = new string[] { "a", "c", "b", "b", "a" };
			ShortestDistance(worddic, "a", "b");

			MinDistance("horse", "ros");

			//Rob(new int[] { 1,2, 3,1 });
			//Rob(new int[] { 2, 7, 9, 3, 1 });
			//Rob(new int[] { 1,2, 1, 1 });
			robber(new int[] { 1, 2, 3, 1 });

			int[] int1 = new int[] { 1, 2, 3};
			int[] int2 = new int[] { 4, 5, 6};
			int[] int3 = new int[] { 7, 8, 9};

			int[][] chart = new int[][] { int1, int2, int3 };

			Rotate(chart);

			int1 = new int[] { 0, 0, 0 };
			int2 = new int[] { 0, 1, 0 };
			int3 = new int[] { 1, 1, 1 };

			chart = new int[][] { int1, int2, int3 };

			UpdateMatrix(chart);

			mergeStrings("dce", "cccbd");
			mergeStrings("abcd", "efghi");

			isZigzag(new int[] { 1,2,1,3,4 });
			isZigzag(new int[] { 1, 2, 3, 4 });
			isZigzag(new int[] { });

			int[] nums = { 1, 2, 3, 4 };
			Console.WriteLine("Sum of Array: ", string.Join(",", (RunningSum(nums).ToArray())));

			string address = "1.1.1.1";
			Console.WriteLine("new ip address: " + DefangIPaddr(address));

			int x = -123;
			Console.WriteLine("Reverse int: " + x + ", results: " + ReverseInteger(x));

			x = 10;
			Console.WriteLine("is Palindrom number: " + IsPalindrome(x));

			string input = "MCMXCIV";
			Console.WriteLine("Roman to Int: " + RomanToInt(input));

			BinaryTree tree = new BinaryTree();
			tree.Insert(3);
			tree.Insert(4);
			tree.Insert(5);
			tree.Insert(1);
			tree.Insert(2);

			BinaryTree subtree = new BinaryTree();
			subtree.Insert(5);
			subtree.Insert(4);
			subtree.Insert(8);
			subtree.Insert(11);
			subtree.Insert(13);
			subtree.Insert(4);
			subtree.Insert(7);


			BinaryTree.InorderTraversal(tree.Tree);

			BinaryTree.IsSubtree(tree.Tree, subtree.Tree);


			BinaryTree tree3 = new BinaryTree();
			tree3.Insert(1);
			tree3.Insert(2);
			tree3.Insert(2);
			tree3.Insert(3);
			tree3.Insert(4);
			tree3.Insert(3);
			BinaryTree.IsSymmetric(tree3.Tree);

			int[] numsb = { -10, -3, 0, 5, 9 };
			BinaryTree.SortedArrayToBST(numsb);

			string[] words = new string[] { "Apple", "Melon", "Orange", "Watermelon" };

			string[] parts = new string[] { "a", "mel", "lon", "el", "An" };
			
			BinaryTree bt = new BinaryTree();

			bt.BuildTree(new int[] { 3, 9, 20, 15, 7 }, new int[] { 9, 3, 15, 20, 7 });


			string s = "civilwartestingwhetherthatnaptionoranynartionsoconceivedandsodedicatedcanlongendureWeareqmetonagreatbattlefiemldoftzhatwarWehavecometodedicpateaportionofthatfieldasafinalrestingplaceforthosewhoheregavetheirlivesthatthatnationmightliveItisaltogetherfangandproperthatweshoulddothisButinalargersensewecannotdedicatewecannotconsecratewecannothallowthisgroundThebravelmenlivinganddeadwhostruggledherehaveconsecrateditfaraboveourpoorponwertoaddordetractTgheworldadswfilllittlenotlenorlongrememberwhatwesayherebutitcanneverforgetwhattheydidhereItisforusthelivingrathertobededicatedheretotheulnfinishedworkwhichtheywhofoughtherehavethusfarsonoblyadvancedItisratherforustobeherededicatedtothegreattdafskremainingbeforeusthatfromthesehonoreddeadwetakeincreaseddevotiontothatcauseforwhichtheygavethelastpfullmeasureofdevotionthatweherehighlyresolvethatthesedeadshallnothavediedinvainthatthisnationunsderGodshallhaveanewbirthoffreedomandthatgovernmentofthepeoplebythepeopleforthepeopleshallnotperishfromtheearth";


			Console.WriteLine("Longest Palindrome string '" + s + "' is: " + LongestPalindrome(s));

			ListNode n11 = new ListNode(9), n12 = new ListNode(9, n11), n13 = new ListNode(9, n12), n14 = new ListNode(9, n13), n15 = new ListNode(9, n14), n16 = new ListNode(9, n15), n17 = new ListNode(9, n16);

			ListNode n21 = new ListNode(9), n22 = new ListNode(6, n21), n23 = new ListNode(9, n22), n24 = new ListNode(9, n23);

			ListNode test = ListNode.AddTwoNumbers(n17, n24);

			Console.WriteLine("LengthOfLongestSubstring is: " + LengthOfLongestSubstring("abcabcbb"));

			s = "aaiougrt";
			int[] indices = { 4, 0, 2, 6, 7, 3, 1, 5 };
			Console.WriteLine("RestoreString is: " + RestoreString(s, indices));

			string[] strs = { "ab", "a" };

			Console.WriteLine("LongestCommonPrefix() : " + LongestCommonPrefix(strs));

			s = "[(]){}";
			Console.WriteLine("ParenthesesIsValid() is: " + ParenthesesIsValid(s));

			ListNode s11 = new ListNode(3), s12 = new ListNode(3, s11), s13 = new ListNode(2, s12), s14 = new ListNode(1, s13), s15 = new ListNode(1, s14);
			ListNode s21 = new ListNode(1), s22 = new ListNode(3, s21), s23 = new ListNode(4, s22);

			ListNode.MergeTwoLists(s13, s23);

			ListNode.DeleteDuplicates(s15);

			ListNode<int> ln1 = new ListNode<int>();
			ln1.value = 1;

			ListNode<int> ln2 = new ListNode<int>();
			ln2.value = 2;

			ListNode<int> ln3 = new ListNode<int>();
			ln3.value = 3;

			ListNode<int> ln4 = new ListNode<int>();
			ln4.value = 4;

			ListNode<int> ln5 = new ListNode<int>();
			ln5.value = 5;

			ln1.next = ln2;
			ln2.next = ln3;
			ln3.next = ln4;
			ln4.next = ln5;

			//ListNode<int> res = ListNode<int>.reverseNodesInKGroups(ln1, 2);

			ListNode<int> res1 = ListNode<int>.rearrangeLastN(ln1, 3);

			//ListNode<int>.removeKFromList(ln1, 1000);

			//ListNode<int>.isListPalindrome(ln1);

			ln1 = new ListNode<int>();
			ln1.value = 9876;

			ln2 = new ListNode<int>();
			ln2.value = 5432;

			ln3 = new ListNode<int>();
			ln3.value = 1999;

			ln1.next = ln2;
			ln2.next = ln3;

			ln4 = new ListNode<int>();
			ln4.value = 3000;

			ln5 = new ListNode<int>();
			ln5.value = 1;

			ListNode<int> ln6 = new ListNode<int>();
			ln6.value = 8001;

			ln4.next = ln5;
			ln5.next = ln6;

			ListNode<int>.addTwoHugeNumbers(ln1, ln4);


			int[] indices1 = { 1, 1, 2 };
			RemoveDuplicates(indices1);

			int[] indices2 = { 0, 1, 2, 2, 3, 0, 4, 2 };
			RemoveElement(indices2, 2);

			int[] indices3 = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
			MaxSubArray(indices3);

			s = "aba";

			LargeGroupPositions(s);

			int[] indices4 = { 0 };
			PlusOne(indices4);

			s = " a ";
			LengthOfLastWord(s);

			IntegerBreak(20);
			IntegerBreak(10);
			IntegerBreak(19);


			//Console.WriteLine(" GeneratePascalTriangle(1):" + GeneratePascalTriangle(1));
			//Console.WriteLine(" GeneratePascalTriangle(2):" + GeneratePascalTriangle(2));
			Console.WriteLine(" GeneratePascalTriangle(5):" + GeneratePascalTriangle(5));

			int[] nums1 = { 2, 2, 1 };
			Console.WriteLine("SingleNumber[2,2,1]:" + SingleNumber(nums1));
			int[] nums2 = { 4, 1, 2, 1, 2 };
			Console.WriteLine("SingleNumber[2,2,1]:" + SingleNumber(nums2));
			int[] nums3 = { 2, 2 };
			Console.WriteLine("SingleNumber[2,2,1]:" + SingleNumber(nums3));

			Console.WriteLine("19 is happy number: " + IsHappyNumber(19));

			Console.WriteLine("2 is happy number: " + IsHappyNumber(2));


			AddBinary("10001", "11");

			MySqrt(2147483647);

			ClimbStairs(5);

			MaxProfit(new int[] { 3,2,6,5,0,3 });


			ClassB cb = new ClassB();
			ClassA ca = cb;
			ca.foo(); // Prints A::foo
			cb.foo(); // Prints B::foo
			ca.bar(); // Prints B::bar
			cb.bar(); // Prints B::bar

			Entity.SetNextSerialNo(1000);
			Entity e1 = new Entity();
			Entity e2 = new Entity();
			Console.WriteLine(e1.GetSerialNo());          // Outputs "1000"
			Console.WriteLine(e2.GetSerialNo());          // Outputs "1001"
			Console.WriteLine(Entity.GetNextSerialNo());  // Outputs "1002"
		}
	}
}
