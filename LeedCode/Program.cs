using TreeStructure;
using LinkedListStructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RunningSumArray;

namespace LeedCodeTest
{
	public class Program
	{

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
				int j = i + 1, k = len - 1;

				while (j < k)
				{

					int sum = nums[i] + nums[j] + nums[k];

					if (Math.Abs(target - sum) < Math.Abs(diff))
					{
						diff = target - sum;
					}

					if (sum < target)
						++j;
					else
						--k;

				}

			}

			return target - diff;

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
				if (i == 0)
				{
					counter += dictionary[s[i]];
					continue;
				}
				if (dictionary[s[i - 1]] >= dictionary[s[i]])
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

		public static int MaxSubArray(int[] nums)
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



		public static bool IsValidSudoku2(char[][] grid)
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

		public static IList<string> GenerateParenthesis(int n)
		{
			List<string> result = new List<string>();
			LinkedList<char> comb = new LinkedList<char>();
			int open = 0, close = 0;

			backtrackParenthesis(n, open, close, comb, result);

			return result;
		}

		private static void backtrackParenthesis(int n, int openN, int closeN, LinkedList<char> comb, List<string> result)
		{
			if( openN == closeN && openN == n)
			{
				result.Add(string.Join("", comb));
			}

			if (openN < n)
			{   
				//make a choise to add open parenthesis
				comb.AddLast('(');
				backtrackParenthesis(n, openN + 1, closeN, comb, result);

				//undo the choice not include the open aprenthesisi
				comb.RemoveLast();

			}

			if (closeN < openN)
			{
				//make a choise to add close parenthesis
				comb.AddLast(')');
				backtrackParenthesis(n, openN, closeN+1, comb, result);

				//undo the choice not include the open aprenthesisi
				comb.RemoveLast();
			}


		}


		/**
		 * Leetcode 
		 * 
		 * 
		 */
		public static void nextPermutation(int[] nums)
		{
			int k = nums.Length - 2;

			while (k >= 0 && nums[k] >= nums[k + 1])
			{
				--k;
			}

			if (k == -1)
			{
				reverse(nums, 0, nums.Length - 1);
				return;
			}

			for(int l = nums.Length - 1; l > k; --l)
			{
				if (nums[l] > nums[k])
				{
					int temp = nums[k];
					nums[k] = nums[l];
					nums[l] = temp;
					break;
				}
			}

			reverse(nums, k + 1, nums.Length - 1);

		}

		private static void reverse(int[] nums, int start, int end)
		{
			while (start < end)
			{
				int tmp = nums[start];
				nums[start] = nums[end];
				nums[end] = tmp;

				start++;
				end--;
			}
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

		private static void backtrackCombinationsSumUnique(int[] candidates, int target, LinkedList<int> cur, int starti, List<IList<int>> result)
		{
			if (target == 0)
			{
				result.Add(new List<int>(cur));
				return;
			}

			for(int i = starti; i < candidates.Length; i++)
			{
				if (i != starti && candidates[i] == candidates[i - 1])
					continue;

				if (candidates[i] > target)
					break;

				//make a choice
				cur.AddLast(candidates[i]);

				backtrackCombinationsSumUnique(candidates, target - candidates[i], cur, i+1, result);

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

			cur.AddLast(candidates[i]);
			backtrackCombinations(i, candidates, cur, total + candidates[i], target, result);

			cur.RemoveLast();
			backtrackCombinations(i + 1, candidates, cur, total, target, result);

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

		static void Main(string[] args)
		{

			MyPow(2, -5);

			Multiply("212", "33");

			CombinationSum2(new int[] { 10, 1, 2, 7, 6, 1, 5 }, 8);

			CombinationSum(new int[] { 2, 3, 6,7 }, 7);

			CountAndSay(4);

			nextPermutation(new int[] { 1, 2, 3, 9, 8, 7, 6, 5 });

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

		}
	}
}
