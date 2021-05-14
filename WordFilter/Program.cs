using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordFilter
{
	public class WordFilter
	{
        public static void RunTestcase1()
        {
            // work on a test case
            // go over the example https://leetcode.com/problems/prefix-and-suffix-search/discuss/110053/Python-few-ways-to-do-it-with-EXPLANATIONS!-U0001f389
            var filter = new WordFilter(new string[] { "bat", "bar" });
            var index = filter.F("b", "at");
            Debug.Assert(index == 0);  // "bat"
        }

        public static void RunTestcase2()
        {
            // work on a test case
            // go over the example https://leetcode.com/problems/prefix-and-suffix-search/discuss/110053/Python-few-ways-to-do-it-with-EXPLANATIONS!-U0001f389
            var filter = new WordFilter(new string[] { "bat", "bar" });
            var index = filter.F("ba", "ar");
            Debug.Assert(index == 1);  // "bar" - index 1
        }

        public static void RunTestcase3()
        {
            // work on a test case
            // go over the example https://leetcode.com/problems/prefix-and-suffix-search/discuss/110053/Python-few-ways-to-do-it-with-EXPLANATIONS!-U0001f389
            var filter = new WordFilter(new string[] { "bat", "bar" });
            var index = filter.F("ba", "r");
            Debug.Assert(index == 1);  // "bar" - index 1
        }

        public static void RunTestcase4()
        {
            // work on a test case
            // go over the example https://leetcode.com/problems/prefix-and-suffix-search/discuss/110053/Python-few-ways-to-do-it-with-EXPLANATIONS!-U0001f389
            var filter = new WordFilter(new string[] { "WordFilter", "f" });
            var index = filter.F("a","e");
            Debug.Assert(index == 1);  // "bar" - index 1
        }

        public class TrieNode
        {
            public TrieNode[] Children = new TrieNode[27];
            public int Weight;

            public static void Insert(TrieNode root, string word, int weight)
            {
                string wrapped = word + "{" + word; // { - 'a' = 27

                int end = 2 * word.Length + 1;
                for (int i = 0; i <= word.Length; i++)
                {
                    insert(root, wrapped, i, end, weight);
                }
            }

            private static void insert(TrieNode root, string word, int start, int end, int weight)
            {
                TrieNode node = root;
                for (int i = start; i < end; i++)
                {
                    int index = word[i] - 'a';
                    if (node.Children[index] == null)
                    {
                        node.Children[index] = new TrieNode();
                    }

                    node = node.Children[index];
                    node.Weight = weight;
                }
            }

            public static int Search(TrieNode root, string word)
            {
                TrieNode node = root;
                foreach (char c in word)
                {
                    int index = c - 'a';
                    if (node.Children[index] == null)
                    {
                        return -1;
                    }

                    node = node.Children[index];
                }

                return node.Weight;
            }

        }


        TrieNode root;

        public WordFilter(string[] words)
        {
            root = new TrieNode();

            for (int i = 0; i < words.Length; i++)
            {
                TrieNode.Insert(root, words[i], i);
            }
        }

        public int F(string prefix, string suffix)
        {
            TrieNode node = root;
            string searchWord = suffix + "{" + prefix;

            int weight = TrieNode.Search(node, searchWord);
            return weight;
        }

        static void Main(string[] args)
        {
            //RunTestcase1();
            //RunTestcase2();
            RunTestcase4();
        }
    }
}
