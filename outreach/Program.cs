using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace outreach
{


	public struct AvailableTime
	{
		public int startTime;
		public int duration;
	}

	public class Program
	{

        public static int maint_window_start(int[][] busyTimes, int duration_mins)
        {

            //Generate the free time list
            List<AvailableTime> lstAlab = new List<AvailableTime>();

            //Fill out the free time list according with busy blocks
            if (busyTimes[0][0] > 0)
                lstAlab.Add(new AvailableTime { startTime = 0, duration = busyTimes[0][0] - 0 });

            int lastStart = 0;
            for (int i = 0; i < busyTimes.Length - 1; i++)
            {
                var av = new AvailableTime();
                int end = busyTimes[i][1];
                int nextStart = busyTimes[i + 1][0];
                lastStart = busyTimes[i + 1][1];
                
                av = new AvailableTime { startTime = end, duration = nextStart - end };

                lstAlab.Add(av);
            }

            if(lastStart < 1440)
			{
                lstAlab.Add(new AvailableTime { startTime = lastStart, duration = 1440 - lastStart });
			}

            var find = lstAlab.Where(c => c.duration >= duration_mins);

            return find.Count() == 0 ? -1 : find.ElementAt(0).startTime;

        }

        static void Main(string[] args)
		{
			int[][] busytimes = new int[][] {
                new int[] { 5, 30},
                new int[] { 120, 241},
                new int[] { 790, 1015}
            };

            Console.WriteLine("Start time = " + maint_window_start(busytimes, 600));
        }
	}
}
