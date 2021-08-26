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
        public bool endOfWord = false;
    }

    public class Trie
    {

        TrieNode root = new TrieNode();

        /** Initialize your data structure here. */
        public Trie()
        {

        }

        /** Inserts a word into the trie. */
        public void Insert(string word)
        {
            TrieNode cur = root;

            foreach (char c in word)
            {
                if (!cur.children.ContainsKey(c))
                {
                    cur.children.Add(c, new TrieNode());
                }
                cur = cur.children[c];
            }
            cur.endOfWord = true;

        }

        /** Returns if the word is in the trie. */
        public bool Search(string word)
        {
            TrieNode cur = root;

            foreach (char c in word)
            {
                if (!cur.children.ContainsKey(c))
                {
                    return false;
                }
                cur = cur.children[c];
            }
            return cur.endOfWord;
        }

        /** Returns if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            TrieNode cur = root;

            foreach (char c in prefix)
            {
                if (!cur.children.ContainsKey(c))
                {
                    return false;
                }
                cur = cur.children[c];
            }
            return true;
        }
    }

}
