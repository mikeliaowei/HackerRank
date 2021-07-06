using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningSumArray
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> children = new Dictionary<char, TrieNode>();
        public bool word = false;
        public TrieNode() { }
    }

    public class WordDictionary
    {

        TrieNode root;

        /** Initialize your data structure here. */
        public WordDictionary()
        {
            root = new TrieNode();
        }

        public void AddWord(string word)
        {
            TrieNode cur = this.root;

            foreach (char c in word)
            {
                if (!cur.children.ContainsKey(c))
                    cur.children.Add(c, new TrieNode());
                cur = cur.children[c];
            }

            cur.word = true;

        }

        public bool Search(string word)
        {

            return dfs(0, this.root);

            bool dfs(int j, TrieNode root)
            {
                TrieNode cur = root;

                for (int i = j; i < word.Length; i++)
                {
                    char c = word[i];

                    if (c == '.')
                    {
                        foreach (TrieNode child in cur.children.Values.ToList())
                        {
                            if (dfs(i + 1, child))
                                return true;
                        }

                        return false;
                    }
                    else
                    {
                        if (!cur.children.ContainsKey(c))
                            return false;
                        cur = cur.children[c];
                    }
                }

                return cur.word;
            }

        }
    }

}
