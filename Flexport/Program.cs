using System;
using System.Collections.Generic;

namespace Flexport
{

    public enum LightStatus
    {
        Green,
        red
    }

    public enum Directions
	{
        North,
        South,
        East,
        West
	}

    public class Road
    {
        public int _traficLight;

        public Directions _directions;

        public Dictionary<int, LightStatus> lightMap = new Dictionary<int, LightStatus>();

        public int costTime()
        {
            return _traficLight + 1;
        }

        public Road(int traficLight)
        {
            while (traficLight > 0)
            {
                lightMap.Add(traficLight, LightStatus.Green);
                traficLight--;
            }
        }

        public void ToggleAllLights(LightStatus status)
        {

            var dictionary = new Dictionary<int, LightStatus>();
            var keys = new List<int>(lightMap.Keys);
            
            foreach (int key in keys)
            {
                dictionary[key] = status;
            }

            lightMap = dictionary;
           
        }

        public bool ToggleOneLight(int nbLight, LightStatus status)
        {

           if(lightMap.ContainsKey(nbLight))
			{
                lightMap[nbLight] = status;
                return true;
			}

            return false;
        }

    }


    class Solution
    {
        static void Main(string[] args)
        {

            Road r1 = new Road(2);

            Console.WriteLine("Truck driving time: " + r1.costTime());

            r1.ToggleAllLights(LightStatus.red);

            r1.ToggleOneLight(1, LightStatus.Green);

            foreach (KeyValuePair<int, LightStatus> kvp in r1.lightMap)
            {
                Console.WriteLine("Light " + kvp.Key + " is " + (LightStatus)kvp.Value);
            }

        }
    }
}
