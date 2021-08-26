using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningSumArray
{
    /**
    * Your TimeMap object will be instantiated and called as such:
    * TimeMap obj = new TimeMap();
    * obj.Set(key,value,timestamp);
    * string param_2 = obj.Get(key,timestamp);
    */

    public class TimeMap
    {

        Dictionary<string, SortedList<int, string>> map = new Dictionary<string, SortedList<int, string>>();

        /** Initialize your data structure here. */
        public TimeMap()
        {

        }

        public void Set(string key, string value, int timestamp)
        {
            if (!map.ContainsKey(key))
                map.Add(key, new SortedList<int, string>());

            SortedList<int, string> temp = map[key];
            temp.Add(timestamp, value);
        }

        public string Get(string key, int timestamp)
        {
            if (!map.ContainsKey(key))
                return string.Empty;

            IList<int> temp = map[key].Keys;

            int left = 0;
            int right = temp.Count - 1;
            while (left < right)
            {
                int mid = (left + right + 1) / 2;
                if (temp[mid] == timestamp)
                    return map[key].Values[mid];

                if (temp[mid] < timestamp)
                    left = mid;
                else
                    right = mid - 1;
            }
            if (left == 0 && map[key].Keys[0] > timestamp)
                return string.Empty;
            return map[key].Values[left];
        }
    }
   
}
