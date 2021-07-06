using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningSumArray
{

	public class PointSystem
	{

		Dictionary<string, PersonPoint> records = new Dictionary<string, PersonPoint>();

		public int getPoint(string name)
		{
			if (!records.ContainsKey(name))
			{
				records.Add(name, new PersonPoint() { PersonName = name, Point = 1 });
			}
			else
			{
				var found = records[name];
				found.Point += 1;
			}

			var sorted = records.OrderByDescending(kvp => kvp.Value.Point).ToDictionary(x => x.Key, x => x.Value);

			return sorted.Keys.ToList().IndexOf(name)+1;
		}



		public class PersonPoint
		{
			public string PersonName { get; set; }
			public int Point { get; set; }
		}

	}


}