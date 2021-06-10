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

		

		TreeNode<int> deleteFromBST(TreeNode<int> t, int[] queries)
		{

			foreach (var i in queries) t = deleteFromBSTSingle(t, i);
			return t;
		}

		TreeNode<int> deleteFromBSTSingle(TreeNode<int> root, int key)
		{
			if (root == null) return root;
			
			if (root.value == key)
			{
				if (root.left != null)
				{
					var ch = getRightChild(root.left, root);
					ch.left = root.left;
					ch.right = root.right;
					return ch;
				}
				else
				{
					return root.right;
				}
			}
			else
			{
				if (root.value > key)
				{
					root.left = deleteFromBSTSingle(root.left, key);
				}
				else
				{
					root.right = deleteFromBSTSingle(root.right, key);
				}

				return root;
			}
		}

		TreeNode<int> getRightChild(TreeNode<int> subtree, TreeNode<int> parent)
		{
			if (subtree.right == null)
			{
				if (parent.right == subtree) parent.right = subtree.left;
				else if (parent.left == subtree) parent.left = subtree.left;
				return subtree;
			}
			return getRightChild(subtree.right, subtree);
		}

		public TreeNode<int> restoreBinaryTree(int[] inorder, int[] preorder)
		{
			return buildTree(inorder, preorder, 0, inorder.Length - 1);
		}

		int count = 0;

		TreeNode<int> buildTree(int[] inorder, int[] preorder, int inStart, int inEnd)
		{
			if (inStart > inEnd)
			{
				return null;
			}

			TreeNode<int> node = new TreeNode<int>();
			node.value = preorder[count];
			count += 1;

			if (inStart == inEnd)
			{
				return node;
			}

			int inIndex = Search(inorder, inStart, inEnd, node.value);

			node.left = buildTree(inorder, preorder, inStart, inIndex - 1);
			node.right = buildTree(inorder, preorder, inIndex + 1, inEnd);
			return node;
		}

		int Search(int[] arr, int start, int end, int target)
		{
			for (var i = start; i <= end; i++)
			{
				if (arr[i] == target)
				{
					return i;
				}
			}
			return 0;
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
					result.Add(Int32.Parse(currentNode.val.ToString()));
					currentNode = currentNode.right;
				}
			}

			return result;
		}

		/*
		 * Leetcode 144 Binary Tree Preorder Traversal
		 * 
		 * 
		 */
		public IList<int> PreorderTraversal(TreeNode root)
		{

			List<int> result = new List<int>();

			if (root == null) return result;

			Stack<TreeNode> nodeStack = new Stack<TreeNode>();

			nodeStack.Push(root);

			while (nodeStack.Count > 0)
			{
				TreeNode node = nodeStack.Pop();
				result.Add(node.val);

				if (node.right != null)
				{
					nodeStack.Push(node.right);
				}

				if (node.left != null)
				{
					nodeStack.Push(node.left);
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

		/// <summary>
		/// Breadth First Search traversal.
		/// </summary>
		/// <returns>Returns the tree data in BFS order</returns>
		public List<int> BFS(TreeNode<int> root)
		{
			// If tree is empty, return null
			if (root == null)
				return null;

			List<int> bfsData = new List<int>();
			Queue<TreeNode<int>> visited = new Queue<TreeNode<int>>();
			visited.Enqueue(root);

			TreeNode<int> currentNode = root;
			while (visited.Count > 0)
			{
				currentNode = visited.Dequeue();
				bfsData.Add(currentNode.value);

				// Add the left node and right node to the queue
				if (currentNode.left != null)
					visited.Enqueue(currentNode.left);
				if (currentNode.right != null)
					visited.Enqueue(currentNode.right);
			}


			return bfsData;
		}

	}
}
