using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningSumArray
{
    /** 
    * Leetcode 460. LFU Cache
    * Your LFUCache object will be instantiated and called as such:
    * LFUCache obj = new LFUCache(capacity);
    * int param_1 = obj.Get(key);
    * obj.Put(key,value);
    */
    public class LFUCache
    {

        public class Cache
        {
            public int key;
            public int val;
            public int freq;
            public Cache(int k, int v, int f)
            {
                key = k;
                val = v;
                freq = f;
            }
        }

        public int size = 0;
        public int minFreq = 0;
        public Dictionary<int, LinkedListNode<Cache>> cacheDic = new Dictionary<int, LinkedListNode<Cache>>();
        public Dictionary<int, LinkedList<Cache>> freqCacheDic = new Dictionary<int, LinkedList<Cache>>();

        public LFUCache(int capacity)
        {
            size = capacity;
        }

        public int Get(int key)
        {
            if (size <= 0 || !cacheDic.ContainsKey(key))
                return -1;

            // get the cache by the key
            LinkedListNode<Cache> node = cacheDic[key];

            // remove the cache from freqCacheDic 
            freqCacheDic[node.Value.freq].Remove(node);

            // increase the freq of node, then add it to freqCacheDic
            node.Value.freq++;
            if (!freqCacheDic.ContainsKey(node.Value.freq))
                freqCacheDic.Add(node.Value.freq, new LinkedList<Cache>() { });
            freqCacheDic[node.Value.freq].AddLast(node);

            // initialize minFreq
            // also increase minFreq when there is no cache with current minFreq
            if (freqCacheDic[minFreq].Count == 0)
                minFreq++;

            return node.Value.val;
        }

        public void Put(int key, int value)
        {

            if (size <= 0)
                return;

            // check if there is any cache has this key. If yes, update the cache's val.   
            int cacheValue = Get(key);
            if (cacheValue != -1)
            {
                // don't need to update the the freq since it has been updated in the Get()
                cacheDic[key].Value.val = value;
            }
            else
            {
                // remove the lru if capacity has reached the limit
                if (cacheDic.Count == size)
                {
                    LinkedListNode<Cache> node = freqCacheDic[minFreq].First;
                    cacheDic.Remove(node.Value.key);
                    freqCacheDic[minFreq].Remove(node);
                }
                // create a new cache
                Cache cache = new Cache(key, value, 1);
                if (!freqCacheDic.ContainsKey(1))
                    freqCacheDic.Add(1, new LinkedList<Cache>());
                LinkedListNode<Cache> cacheNode = freqCacheDic[1].AddLast(cache);
                cacheDic.Add(key, cacheNode);
                minFreq = 1;
            }
        }
    }
}
