using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CruiseInterview
{
    public class Tile
    {
        public int index;
        public string url;
    }

    public interface IMapping
    {
        public int AddTiles(List<Tile> tiles);
        public List<Tile> GetTiles(int versionId);
    }

    public class Mapping : IMapping
    {

        Dictionary<int, List<Tile>> dic = new Dictionary<int, List<Tile>>();
        int latestVerNb;

        public int AddTiles(List<Tile> tiles)
        {
            //if mapping dic is empty
            if (dic.Count() == 0)
            {
                latestVerNb++;
                dic.Add(latestVerNb, tiles);


            }
            else
            {
                latestVerNb++;
                if (!dic.ContainsKey(latestVerNb))
                    dic.Add(latestVerNb, tiles);
            }

            return latestVerNb;
        }


        public List<Tile> GetTiles(int versionId)  // 2: 1 colletion + 2 collection
        {
            int i = 1;

            List<Tile> temp = new List<Tile>();
            while (i <= versionId)
            {
                foreach (Tile t in dic[i])
                {
                    /* Merge and decide if latest one already existing, update with latest one 
                    [1, url1]
                    ....
                    [1, url4]
                    */
                    var found = temp.Find(f => f.index == t.index);
                    if (found != null)
                    {
                        found.url = t.url;
                    }
                    else
                    {
                        temp.Add(t);
                    }

                }

                i++;

            }


            return temp;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Mapping m = new Mapping();

            List<Tile> input1 = new List<Tile>(){

          new Tile(){ index = 1, url = "url1"},
          new Tile(){ index = 2, url = "url2"},
          new Tile(){ index = 3, url = "url3"}
        };

            List<Tile> input2 = new List<Tile>(){

          new Tile(){ index = 1, url = "url1-1"},
          new Tile(){ index = 4, url = "url3"}
        };

            Console.WriteLine("Version: " + m.AddTiles(input1));

            Console.WriteLine("Version: " + m.AddTiles(input2));

            List<Tile> find = m.GetTiles(2);

            foreach (Tile t in find)
            {
                Console.WriteLine("Index of " + t.index + ": " + t.url);
            }
        }
    }
}
