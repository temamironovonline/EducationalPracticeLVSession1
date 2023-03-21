using System;
using System.Collections.Generic;


namespace SF2022User_NN_Lib.dll
{
    public class Calculations
{
        public static string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {
            if (beginWorkingTime > endWorkingTime || consultationTime <= 0)
            {
                return null;
            }
            List<TimeSpan> WorkTime = new List<TimeSpan>();
            int i = 0;
            foreach (var item in startTimes)
            {
                if (item < beginWorkingTime)
                {
                    i++;
                }
            }
            while (beginWorkingTime.Add(new TimeSpan(0, consultationTime, 0)) <= endWorkingTime)
            {
                if (startTimes.Length != i && durations.Length != i)
                {
                    if (beginWorkingTime.Add(new TimeSpan(0, consultationTime, 0)) > startTimes[i])
                    {
                        beginWorkingTime = startTimes[i].Add(new TimeSpan(0, durations[i], 0));
                        i++;
                        continue;
                    }
                }

                WorkTime.Add(beginWorkingTime);
                beginWorkingTime = beginWorkingTime.Add(new TimeSpan(0, consultationTime, 0));
            }
            List<string> strings = new List<string>();
            foreach (TimeSpan item in WorkTime)
            {
                strings.Add(item.ToString(@"hh\:mm") + "-" + item.Add(new TimeSpan(0, consultationTime, 0)).ToString(@"hh\:mm"));
            }

            return strings.ToArray();
        }
    }
}
