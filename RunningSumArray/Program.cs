using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeedCodeEasy
{
	public class ListNode
	{
		public int val;
		public ListNode next;
		public ListNode(int val = 0, ListNode next = null)
		{
			this.val = val;
			this.next = next;
		}

		public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
		{
			int carry = 0;
			ListNode dummy = new ListNode(0);
			ListNode pre = dummy;

			while (l1 != null || l2 != null || carry == 1)
			{
				int sum = (l1 == null ? 0 : l1.val) + (l2 == null ? 0 : l2.val) + carry;
				carry = sum < 10 ? 0 : 1;
				pre.next = new ListNode(sum % 10);
				pre = pre.next;

				if (l1 != null)
				{
					l1 = l1.next;
				}

				if (l2 != null)
				{
					l2 = l2.next;
				}
			}

			return dummy.next;
		}

		public static void DeleteNode(ListNode node)
		{
			ListNode currentNode = node;
			while (currentNode != null)
			{
				if (currentNode.next != null)
				{
					currentNode.val = currentNode.next.val;
					if (currentNode.next.next == null)
					{
						currentNode.next = null;
						break;
					}
				}
				currentNode = currentNode.next;
			}
		}
	}


	public class TreeNode
	{
		public object Data { get; set; }
		public TreeNode left { get; set; }
		public TreeNode right { get; set; }

		public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
		{
			this.Data = val;
			this.left = left;
			this.right = right;
		}

		public TreeNode(object val)
		{
			this.Data = val;
		}
	}

	public class BinaryTree
	{
		private TreeNode _root;

		public TreeNode Tree
		{
			get { return _root; }
		}

		public BinaryTree()
		{
			_root = null;
		}
		public void Insert(object data)
		{
			// 1. If the tree is empty, return a new, single node 
			if (_root == null)
			{
				_root = new TreeNode(data);
				return;
			}
			// 2. Otherwise, recur down the tree 
			InsertRec(_root, new TreeNode(data));
		}
		private void InsertRec(TreeNode root, TreeNode newNode)
		{
			if (root == null)
				root = newNode;

			int newNodeValue = 0;
			int rootValue = 0;
			if (newNode.Data != null) Int32.TryParse(newNode.Data.ToString(), out newNodeValue);
			if (root.Data != null) Int32.TryParse(root.Data.ToString(), out rootValue);


			if (newNodeValue < rootValue)
			{
				if (root.left == null)
					root.left = newNode;
				else
					InsertRec(root.left, newNode);

			}
			else
			{
				if (root.right == null)
					root.right = newNode;
				else
					InsertRec(root.right, newNode);
			}
		}
		private void DisplayTree(TreeNode root)
		{
			if (root == null) return;

			DisplayTree(root.left);
			System.Console.Write(root.Data + " ");
			DisplayTree(root.right);
		}
		public void DisplayTree()
		{
			DisplayTree(_root);
		}

		public static IList<int> InorderTraversal(TreeNode root)
		{
			List<int> result = new List<int>();
			Stack<TreeNode> stack = new Stack<TreeNode>();
			TreeNode currentNode = root;

			while (currentNode != null || stack.Count != 0)
			{
				while (currentNode != null)
				{
					stack.Push(currentNode);
					currentNode = currentNode.left;
				}

				if (stack.Count != 0)
				{
					currentNode = stack.Pop();
					if (currentNode.Data != null) result.Add(Int32.Parse(currentNode.Data.ToString()));
					currentNode = currentNode.right;
				}
			}

			return result;
		}

		public static int MaxDepth(TreeNode root)
		{
			if (root == null)
				return 0;

			if (root.left == null && root.right == null)
				return 1;

			int left = MaxDepth(root.left),
				right = MaxDepth(root.right);

			return left >= right ? left + 1 : right + 1;
		}

	}

	class Program
	{

		public static string LongestCommonPrefix(string[] strs)
		{
			List<string> orderStrs = strs.OrderByDescending(c => c.Length).ToList();
			int maxLength = 0;
			if (orderStrs.Count > 0) maxLength = orderStrs[0].Length;
			Dictionary<int, char> dic = new Dictionary<int, char>(maxLength);


			for(int i = 0; i < orderStrs.Count; i++)
			{
				int j = 0;
				if( i == 0)
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

			for(int i = 0; i < indices.Length; i++)
			{
				dic[indices[i]] = s[i];
			}

			StringBuilder words = new StringBuilder();

			foreach(KeyValuePair<int, char> entry in dic.OrderBy(c => c.Key))
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

		public static string LongestPalindrome(string s)
		{
			if (s == null || s.Length < 2)
			{
				return s;
			}

			int length = s.Length;

			bool[,] isPalindrome = new bool[length, length];

			int left = 0;
			int right = 0;

			for (int j = 1; j < length; j++)
			{
				for (int i = 0; i < j; i++)
				{
					bool isInnerWordPalindrome = isPalindrome[i + 1, j - 1] || j - i <= 2;

					if (s[i] == s[j] && isInnerWordPalindrome)
					{
						isPalindrome[i, j] = true;

						if (j - i > right - left)
						{
							left = i;
							right = j;
						}
					}
				}
			}

			return s.Substring(left, right - left + 1);
		}

		public static int[] RunningSum(int[] nums)
		{
			int[] result = new int[nums.Length];

			for(int i=0; i<nums.Length; i++)
			{
				int total = 0;
				for(int j = 0 ; j <= i; j++)
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
			foreach(char c in address)
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

			for(int i = intStr.Length - 1; i >= 0; i--)
			{
				newIntStr += intStr[i];
			}


			if(intStr.Equals(newIntStr))
			{
				return true;
			}

			return false;

		}


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

		public static string IntToRoman(int num)
		{
			string roman = "";

			var dictionary = new Dictionary<char, int> {
								{ 'I', 1 },
								{ 'V', 5 },
								{ 'X', 10 },
								{ 'L', 50 },
								{ 'C', 100 },
								{ 'D', 500 },
								{ 'M', 1000 }
							};


			return roman;
		}


		static void Main(string[] args)
		{
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
			tree.Insert(1);
			tree.Insert(null);
			tree.Insert(2);
			tree.Insert(3);
			tree.Insert(4);
			tree.DisplayTree();

			BinaryTree.InorderTraversal(tree.Tree);

			string s = "civilwartestingwhetherthatnaptionoranynartionsoconceivedandsodedicatedcanlongendureWeareqmetonagreatbattlefiemldoftzhatwarWehavecometodedicpateaportionofthatfieldasafinalrestingplaceforthosewhoheregavetheirlivesthatthatnationmightliveItisaltogetherfangandproperthatweshoulddothisButinalargersensewecannotdedicatewecannotconsecratewecannothallowthisgroundThebravelmenlivinganddeadwhostruggledherehaveconsecrateditfaraboveourpoorponwertoaddordetractTgheworldadswfilllittlenotlenorlongrememberwhatwesayherebutitcanneverforgetwhattheydidhereItisforusthelivingrathertobededicatedheretotheulnfinishedworkwhichtheywhofoughtherehavethusfarsonoblyadvancedItisratherforustobeherededicatedtothegreattdafskremainingbeforeusthatfromthesehonoreddeadwetakeincreaseddevotiontothatcauseforwhichtheygavethelastpfullmeasureofdevotionthatweherehighlyresolvethatthesedeadshallnothavediedinvainthatthisnationunsderGodshallhaveanewbirthoffreedomandthatgovernmentofthepeoplebythepeopleforthepeopleshallnotperishfromtheearth";

			s = "aba";

			Console.WriteLine("Longest Palindrome string '" +s+ "' is: " + LongestPalindrome(s));

			ListNode n11 = new ListNode(9), n12 = new ListNode(9, n11), n13 = new ListNode(9, n12), n14 = new ListNode(9, n13), n15 = new ListNode(9, n14), n16 = new ListNode(9, n15), n17 = new ListNode(9, n16);

			ListNode n21 = new ListNode(9), n22 = new ListNode(6, n21), n23= new ListNode(9, n22), n24 = new ListNode(9, n23);

			ListNode test = ListNode.AddTwoNumbers(n17, n24);

			Console.WriteLine("LengthOfLongestSubstring is: " + LengthOfLongestSubstring("abcabcbb"));

			s = "aaiougrt";
			int[] indices = { 4, 0, 2, 6, 7, 3, 1, 5 };
			Console.WriteLine("RestoreString is: " + RestoreString(s, indices));

			string[] strs = { "ab", "a" };

			Console.WriteLine("LongestCommonPrefix() : " + LongestCommonPrefix(strs));
		}
	}
}
