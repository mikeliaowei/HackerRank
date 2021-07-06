using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListStructure
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

		/*
		 * Leetcode 876 Middle of the Linked List
		 * 
		 */
		public ListNode MiddleNode(ListNode head)
		{
			List<ListNode> lst = new List<ListNode>();

			while (head != null)
			{
				lst.Add(head);
				head = head.next;
			}

			//Get middle index
			int index = lst.Count / 2 + 1;
			//Console.WriteLine("index = " + index);

			return lst.ElementAt(index - 1);

		}

		/*
		 *Leetcode 234 Palindrome Linked List
		 * 
		 * 
		 */
		public bool IsPalindrome(ListNode head)
		{
			List<int> vals = new List<int>();

			// Convert LinkedList into ArrayList.
			ListNode currentNode = head;
			while (currentNode != null)
			{
				vals.Add(currentNode.val);
				currentNode = currentNode.next;
			}

			// Use two-pointer technique to check for palindrome.
			int front = 0;
			int back = vals.Count - 1;
			while (front < back)
			{
				// Note that we must use ! .equals instead of !=
				// because we are comparing Integer, not int.
				if (!vals.ElementAt(front).Equals(vals.ElementAt(back)))
				{
					return false;
				}
				front++;
				back--;
			}
			return true;
		}

		/*
		 * Leetcode 160 intersection of Two linked lists
		 * 
		 * Given the heads of two singly linked-lists headA and headB, return the node at which the two lists intersect. 
		 * If the two linked lists have no intersection at all, return null.
		 */
		public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
		{
			ListNode a = headA,
					 b = headB;

			while (a != b)
			{
				a = a == null ? headB : a.next;
				b = b == null ? headA : b.next;
			}

			return a;
		}



		/*
		 * Leetcode 141 Linked List cycle
		 * Given head, the head of a linked list, determine if the linked list has a cycle in it.
		 */
		public bool HasCycle(ListNode head)
		{
			Dictionary<ListNode, int> dic = new Dictionary<ListNode, int>();

			if (head != null && head.next == null) return false;

			bool hasCycle = false;

			while (head != null)
			{

				if (!dic.ContainsKey(head))
				{
					dic.Add(head, 1);
				}
				else
				{
					hasCycle = true;
					break;
				}

				head = head.next;
			}

			return hasCycle;

		}

		/*
		 * Leetcode 206 Reverse Linked List
		 * Given the head of a singly linked list, reverse the list, and return the reversed list.
		 * 
		 */
		public ListNode ReverseList(ListNode head)
		{
			ListNode pres = null, current = head, next = null;

			while(current != null)
			{
				next = current.next;
				current.next = pres;
				pres = current;
				current = next;
			}

			return pres;
		}

		/*
		 * Leetcode 92:  Reverse Linked List II
		 * Given the head of a singly linked list and two integers left and right where left <= right, 
		 * reverse the nodes of the list from position left to position right, and return the reversed list..
		 * Input: head = [1,2,3,4,5], left = 2, right = 4
		 * Output: [1,4,3,2,5]
		 */
		public ListNode ReverseBetween(ListNode head, int left, int right)
		{
			if (head == null) return null;

			ListNode prev = null;
			ListNode current_node = head;

			while (left > 1)
			{
				prev = current_node;
				current_node = current_node.next;

				left--;
				right--;
			}

			ListNode connection = prev;
			ListNode tail = current_node;

			while (right > 0)
			{
				ListNode next_node = current_node.next;
				current_node.next = prev;
				prev = current_node;
				current_node = next_node;

				right--;
			}

			if (connection != null)
			{
				connection.next = prev;
			}
			else
			{
				head = prev;
			}

			tail.next = current_node;

			return head;

		}

		/* 19. Remove Nth Node From End of List
		 * 
		 * Given the head of a linked list, remove the nth node from the end of the list and return its head.
		 */
		public ListNode RemoveNthFromEnd(ListNode head, int n)
		{
			int length = 0;

			ListNode dummy = new ListNode(0);
			dummy.next = head;

			ListNode first = head;

			while (first != null)
			{
				length += 1;
				first = first.next;
			}

			length = length - n;

			first = dummy;

			while (length > 0)
			{
				length--;

				first = first.next;
			}

			first.next = first.next.next;

			return dummy.next;

		}



		public static ListNode RotateRight(ListNode head, int k)
		{
			if (head == null) return null;
			if (head.next == null || k == 0) return head;


			ListNode last = head;
			ListNode pre = null;

			while (k % 100 == 0)
			{
				k = k / 100;
			}

			while (k > 0)
			{

				while (last.next != null)
				{
					pre = last;
					//Console.WriteLine("Pre = " +pre.val);
					last = last.next;
					//Console.WriteLine("last = " +last.val);
				}

				pre.next = null;
				last.next = head;
				head = last;

				k--;
			}

			return head;

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

		/*
		 * Leetcode 23: Merge k Sorted Lists
		 * You are given an array of k linked-lists lists, each linked-list is sorted in ascending order.
		 * 
		 */
		public ListNode MergeKLists(ListNode[] lists)
		{
			if (lists == null || lists.Length == 0) return null;

			while (lists.Length > 1)
			{
				List<ListNode> mergedLists = new List<ListNode>();

				for (int i = 0; i < lists.Length; i += 2)
				{

					ListNode l1 = lists[i];
					ListNode l2 = i + 1 < lists.Length ? lists[i + 1] : null;

					mergedLists.Add(MergeTwoLists(l1, l2));
				}

				lists = mergedLists.ToArray();

			}

			return lists[0];

		}

		/*
		 * Leetcode 21: Merge two list
		 * 
		 * 
		 */
		public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
		{

			ListNode dumy = new ListNode();
			ListNode tail = dumy;

			while (l1 != null && l2 != null)
			{
				if (l1.val < l2.val)
				{
					tail.next = l1;
					l1 = l1.next;
				}
				else
				{
					tail.next = l2;
					l2 = l2.next;
				}

				tail = tail.next;
			}

			if (l1 != null)
				tail.next = l1;
			else if (l2 != null)
				tail.next = l2;

			return dumy.next;
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

		public static ListNode DeleteDuplicates(ListNode head)
		{
			if (head != null && head.next != null)
			{
				var curr = head;
				while (curr != null && curr.next != null)
				{
					if (curr.val == curr.next.val)
						curr.next = curr.next.next;
					else
						curr = curr.next;
				}
			}

			return head;
		}
	}


	public class ListNode<T>
	{
		public T value { get; set; }
		public ListNode<T> next { get; set; }

		public static ListNode<int> removeKFromList(ListNode<int> head, int k)
		{
			var dummy = new ListNode<int>();
			var current = dummy;
			while (head != null)
			{
				if (head.value != k)
				{
					current.next = head;
					current = current.next;
				}
				head = head.next;
			}
			current.next = null;
			
			return dummy.next;
		}

		ListNode<int> removeKFromListRecursive(ListNode<int> l, int k)
		{

			if (l == null) return null;

			l.next = removeKFromListRecursive(l.next, k);

			if (l.value == k)
				return l.next;
			else
				return l;
		}

		public static ListNode<int> removeFromList(ListNode<int> head, int target)
		{
			ListNode<int> travel1 = head, travel2 = head;

			while (travel2 != null)
			{
				if(travel2.value == target)
				{
					travel1.next = travel2.next;
				}

				travel1 = travel2;
				travel2 = travel2.next;
			}

			return head;
		}

		public static ListNode<int> addTwoHugeNumbers(ListNode<int> a, ListNode<int> b)
		{
			Dictionary<int, int> dica = new Dictionary<int, int>();
			Dictionary<int, int> dicb = new Dictionary<int, int>();

			int i = 0;
			while (a != null)
			{
				dica.Add(i, a.value);
				a = a.next;
				i++;
			}

			i = 0;
			while (b != null)
			{
				dicb.Add(i, b.value);
				b = b.next;
				i++;
			}

			ListNode<int> result = dica.Count >= dicb.Count ? addTwoDictionary(dica, dicb) : addTwoDictionary(dicb, dica);
			ListNode<int> copy = Duplicate(result);

			ListNode<int> r = reverseList(copy);

			return r;
		}

		private static ListNode<int> addTwoDictionary(Dictionary<int, int> dica, Dictionary<int, int> dicb)
		{
			int carry = 0, sum = 0;

			// create the head node, keeping it for later return
			ListNode<int> first = new ListNode<int>();
			first.value = sum;

			// the 'temp' pointer points to the current "last" node in the new list
			ListNode<int> temp = first;

			int lena = dica.Count - 1;
			int lenb = dicb.Count - 1;

			while (lena >= 0)
			{
				if (lenb >= 0)
				{
					sum = dica[lena] + dicb[lenb] + carry;
					carry = sum > 9999 ? 1 : 0;
					if (carry == 1)
					{
						sum = sum - 10000;
					}
				}
				else
				{
					sum = dica[lena] + carry;
					carry = sum > 9999 ? 1 : 0;
				}

				ListNode<int> n2 = new ListNode<int>();
				n2.value = sum;
				// modify the Next pointer of the last node to point to the new last node
				temp.next = n2;
				temp = n2;

				lena--;
				lenb--;
			}

			if (carry == 1)
			{
				ListNode<int> n2 = new ListNode<int>();
				n2.value = carry;
				// modify the Next pointer of the last node to point to the new last node
				temp.next = n2;
				temp = n2;
			}

			return first.next;

		}

		public static bool isListPalindrome(ListNode<int> l)
		{
			ListNode<int> copy = Duplicate(l);
			ListNode<int> r = reverseList(l);

			return areIdentical(copy, r);

		}

		public static ListNode<int> Duplicate(ListNode<int> n)
		{
			// handle the degenerate case of an empty list
			if (n == null)
			{
				return null;
			}

			// create the head node, keeping it for later return
			ListNode<int> first = new ListNode<int>();
			first.value = n.value;

			// the 'temp' pointer points to the current "last" node in the new list
			ListNode<int> temp = first;

			n = n.next;
			while (n != null)
			{
				ListNode<int> n2 = new ListNode<int>();
				n2.value = n.value;
				// modify the Next pointer of the last node to point to the new last node
				temp.next = n2;
				temp = n2;
				n = n.next;
			}

			return first;

		}

		public static ListNode<int> rearrangeLastN(ListNode<int> l, int n)
		{
			var current = l;
			if (n == 0) return l;

			for (int i = 0; i < n; i++)
			{
				if (current == null) return l;
				current = current.next;
			}

			if (current == null) return l;

			var head = l;
			while (current.next != null)
			{
				current = current.next;
				head = head.next;
			}

			var result = head.next;
			head.next = null;
			current.next = l;

			return result;
		}

		public static ListNode<int> GetLastNode(ListNode<int> head)
		{
			ListNode<int> temp = head;
			while (temp.next != null)
			{
				temp = temp.next;
			}
			return temp;
		}

		public static ListNode<int> reverseNodesInKGroups(ListNode<int> head, int k)
		{
			if (head == null)
				return null;

			ListNode<int> curr = head;
			ListNode<int> prev = null;

			for (int i = 0; i < k; i++)
			{
				if (curr == null)
				{
					// We need to reset the nodes that have been reversed
					return reverseNodesInKGroups(prev, i);
				}
				ListNode<int> tmp = curr.next;
				curr.next = prev;
				prev = curr;
				curr = tmp;
			}

			head.next = reverseNodesInKGroups(curr, k);
			return prev;
		}

		public static ListNode<int> reverseList(ListNode<int> l)
		{
			ListNode<int> pres = null, current = l, next = null;

			while (current != null)
			{
				next = current.next;
				current.next = pres;
				pres = current;
				current = next;
			}

			return pres;
		}

		public static bool areIdentical(ListNode<int> a, ListNode<int> b)
		{
			ListNode<int> lista = a, listb = b;


			while (lista != null && listb != null)
			{
				if (lista.value != listb.value)
					return false;

				/* If we reach here, then a and b are not null
				and their data is same, so move to next nodes
				in both lists */
				lista = lista.next;
				listb = listb.next;
			}

			// If linked lists are identical,
			// then 'a' and 'b' must
			// be null at this point.
			return (lista == null && listb == null);
		}

		public static ListNode<int> MergeTwoLists(ListNode<int> l1, ListNode<int> l2, bool isSorted)
		{
			if (l1 == null && l2 == null) return null;
			if (l1 == null && l2 != null) return l2;
			if (l1 != null && l2 == null) return l1;

			Dictionary<int, int> dicNodeValue = new Dictionary<int, int>();
			ListNode<int> sortedNode = new ListNode<int>();

			int i = 0;
			while (l1 != null)
			{
				dicNodeValue[i] = l1.value;
				if (l1 != null)
				{
					l1 = l1.next;
				}
				i++;
			}


			while (l2 != null)
			{
				dicNodeValue[i] = l2.value;
				if (l2 != null)
				{
					l2 = l2.next;
				}
				i++;
			}

			int j = 0;

			ListNode<int> dumy = new ListNode<int>();

			if (isSorted)
			{
				dicNodeValue.OrderBy(c => c.Value);
			}

			foreach (KeyValuePair<int, int> entry in dicNodeValue)
			{
				if (j == 0)
				{
					sortedNode.value = entry.Value;
					sortedNode.next = dumy;
				}
				else if (j < dicNodeValue.Count - 1)
				{
					ListNode<int> newNode = new ListNode<int>();
					dumy.value = entry.Value;
					dumy.next = newNode;
					dumy = dumy.next;
				}
				else
				{
					dumy.value = entry.Value;
				}
				j++;
			}

			return sortedNode;
		}

	}
}
