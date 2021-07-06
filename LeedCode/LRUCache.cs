using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningSumArray
{
    public class LRUCache
    {

        public class Cache
        {
            public int CacheKey;
            public int CacheVal;
            public Cache(int key, int val)
            {
                CacheKey = key;
                CacheVal = val;
            }
        }

        public int Capacity = 0;

        /* dic: maintain the key-value mapping for 2 purposes:
           1. Get cache value by cache key in O(1)
           2. Use LinkedListNode as the dictionry value, so that later we can use LinkedList.Remove(dic[key]) to remove an element in O(1).
        */
        public Dictionary<int, LinkedListNode<Cache>> dic = new Dictionary<int, LinkedListNode<Cache>>();

        /*
            lruList: 
            1. Most recent used cache will be added in the head of the LinkedList.
            2. Use LinkedListNode to add/remove element to achieve O(1) time complexity.
            3. LinkedList<T>.Last returns the last LinkedListNode<T>.
        */
        public LinkedList<Cache> lruList = new LinkedList<Cache>();

        public LRUCache(int capacity)
        {
            this.Capacity = capacity;
        }

        public int Get(int key)
        {
            if (!dic.ContainsKey(key))
                return -1;

            var cache = dic[key];

            // move the cache to the head of lruList, indicating it is the most recent used cache.
            lruList.Remove(cache);
            lruList.AddFirst(cache);

            return cache.Value.CacheVal;
        }

        public void Put(int key, int value)
        {
            if (dic.ContainsKey(key))
            {
                // update the cache value
                dic[key].Value.CacheVal = value;

                // move the updated cache to the head of the lruList
                var cache = dic[key];
                lruList.Remove(cache);
                lruList.AddFirst(cache);
            }
            else
            {
                // add a new cache to the dic and lru
                Cache cache = new Cache(key, value);
                dic.Add(key, new LinkedListNode<Cache>(cache));
                lruList.AddFirst(dic[key]);

                if (dic.Count > Capacity)
                {
                    // remove the last cache from the dic and lruList if capacity execeeds the limit
                    // use lruList.Last instead of lruList.Last() to get the LinkedListNode object
                    // this helps remove lastCache from lruList in O(1)
                    LinkedListNode<Cache> lastCache = lruList.Last;
                    dic.Remove(lastCache.Value.CacheKey);
                    lruList.Remove(lastCache);
                }
            }
        }
    }
}
