using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure
{
	public class TreeNode
	{
		public int val;
		public TreeNode left { get; set; }
		public TreeNode right { get; set; }

		public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
		{
			this.val = val;
			this.left = left;
			this.right = right;
		}

		public TreeNode(int val)
		{
			this.val = val;
		}

	}

	public class TreeNode<T>
	{
		public T value { get; set; }
		public TreeNode<T> left { get; set; }
		public TreeNode<T> right { get; set; }
	}
}
