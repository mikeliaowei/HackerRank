using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningSumArray
{
	public class SubwaySystem
	{
		Dictionary<int, UserTripTime> userTripDic = new Dictionary<int, UserTripTime>();
		Dictionary<string, int> tripAverage = new Dictionary<string, int>();

		public void checkInSystem(int userId, string stationName, int time)
		{
			if (!userTripDic.ContainsKey(userId))
			{
				userTripDic.Add(userId,  new UserTripTime() { UserId = userId, TripTime = time, StationName = stationName });
			}
		}

		public void checkOutSystem(int userId, string stationName, int time)
		{
			if (userTripDic.ContainsKey(userId))
			{
				var firstTrip = userTripDic[userId];
				string tripName = userTripDic[userId].StationName + stationName;
				if (!tripAverage.ContainsKey(tripName))
				{
					tripAverage.Add(tripName, (time - firstTrip.TripTime));
				}
				else
				{
					int firstAv = tripAverage[tripName];
					tripAverage[tripName] = (firstAv + (time - firstTrip.TripTime)) / 2;
				}
				
			}

		}

		public int getAverageTravelTime(string stationStart, string stationEnd)
		{
			int avg = 0;

			if(tripAverage.ContainsKey(stationStart+stationEnd))
			{
				avg = tripAverage[stationStart + stationEnd];
			}

			return avg;
		}
	}

	public class UserTripTime
	{
		public int UserId { get; set; }

		public string StationName { get; set; }

		public int TripTime { get; set; }
	}
}
