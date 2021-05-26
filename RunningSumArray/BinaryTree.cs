using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure
{
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

		/*
		 * 108. Convert Sorted Array to Binary Search Tree
		 * 
		 */
		public static TreeNode SortedArrayToBST(int[] nums)
		{
			if (nums == null || nums.Length == 0)
				return null;

			return BuildBST(nums, 0, nums.Length - 1);
		}

		private static TreeNode BuildBST(int[] nums, int startIndex, int endIndex)
		{
			TreeNode currentRoot = null;
			int currentRootIndex = endIndex + (startIndex - endIndex) / 2;

			if (startIndex <= endIndex)
			{
				currentRoot = new TreeNode(nums[currentRootIndex]);
				currentRoot.left = BuildBST(nums, startIndex, currentRootIndex - 1);
				currentRoot.right = BuildBST(nums, currentRootIndex + 1, endIndex);
			}

			return currentRoot;
		}

		public void Insert(int data)
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

			if (newNode.val < root.val)
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
			System.Console.Write(root.val + " ");
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
					if (currentNode.val != null) result.Add(Int32.Parse(currentNode.val.ToString()));
					currentNode = currentNode.right;
				}
			}

			return result;
		}


		/*
		 * 112 Path sum
		 * Given the root of a binary tree and an integer targetSum, 
		 * return true if the tree has a root-to-leaf path such that adding up all the values along the path equals targetSum.
		 */
		public static bool HasPathSum(TreeNode root, int targetSum)
		{
			if (root == null)
				return false;

			if (root.left == null && root.right == null)
				return targetSum == root.val;

			return HasPathSum(root.left, targetSum - root.val) || HasPathSum(root.right, targetSum - root.val);
		}

		/*
		 * 110. Balanced Binary Tree 
		 * Given a binary tree, determine if it is height-balanced.
		 */
		public static bool IsBalanced(TreeNode root)
		{
			if (root == null)
				return true;

			bool res = true;
			IsBalancedHelper(root, ref res);

			return res;
		}

		private static int IsBalancedHelper(TreeNode node, ref bool res)
		{
			if (node == null)
				return 0;

			int leftHeight = IsBalancedHelper(node.left, ref res) + 1;
			int rightHeight = IsBalancedHelper(node.right, ref res) + 1;

			if (Math.Abs(leftHeight - rightHeight) > 1)
				res = false;

			return Math.Max(leftHeight, rightHeight);
		}

		/*
		 * 111. Minimum Depth of Binary Tree
		 * 
		 */
		public static int MinDepth(TreeNode root)
		{
			if (root == null)
				return 0;
			else if (root.left == null)
				return MinDepth(root.right) + 1;
			else if (root.right == null)
				return MinDepth(root.left) + 1;
			else
				return Math.Min(MinDepth(root.left), MinDepth(root.right)) + 1;
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

		public static bool IsSubtree(TreeNode s, TreeNode t)
		{
			return Traverse(s, t);
		}

		private static bool Traverse(TreeNode s, TreeNode t)
		{
			return s != null && (Equal(s, t) || Traverse(s.left, t) || Traverse(s.right, t));
		}

		private static bool Equal(TreeNode s, TreeNode t)
		{
			if (s == null && t == null) return true;
			if (s == null || t == null) return false;
			return s.val == t.val && Equal(s.left, t.left) && Equal(s.right, t.right);
		}

		/*
		 * 100 Same tree
		 * 
		 */
		public static bool IsSameTree(TreeNode p, TreeNode q)
		{
			if (p == null && q == null) return true;
			if (p == null || q == null) return false;
			return p.val == q.val && IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
		}


		/*
		 * 101 Symmetric Tree
		 * Given the root of a binary tree, check whether it is a mirror of itself
		 */
		public static bool IsSymmetric(TreeNode root)
		{
			if (root == null) return false;

			return IsMirror(root.left, root.right);
		}

		public static bool IsMirror(TreeNode p, TreeNode q)
		{
			if (p == null && q == null) return true;
			if (p == null || q == null) return false;
			return p.val == q.val && IsMirror(p.left, q.right) && IsMirror(p.right, q.left);
		}
	}
}
