using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day4 {
        public string TaskA(string[] input) {
            Dictionary<int, List<long>> sleep = new Dictionary<int, List<long>>();

            // Sort records. This could have done using only 1 DateTime.
            List<string> records = input.ToList().OrderBy(i => DateTime.Parse(i.Split(' ')[0].Remove(0, 1))).ThenBy(i 
                => DateTime.Parse(i.Split(' ')[1].Remove(i.Split(' ')[1].Length - 1,1))).ToList<string>();

            // Parse the records
            int currentGuard = 0;
            int startSleep = 0;
            foreach (string record in records) {
                string[] split = record.Split(' ');
                if (split[2] == "Guard") {
                    // New shift, who is it?
                    int newGuard = Int32.Parse(split[3].Remove(0, 1));
                    currentGuard = newGuard;
                } else if (split[2] == "falls") {
                    startSleep = Int32.Parse(split[1].Remove(split[1].Length - 1, 1).Split(':')[1]);
                } else if (split[2] == "wakes") {
                    int wakeUpTime = Int32.Parse(split[1].Remove(split[1].Length - 1, 1).Split(':')[1]);
                    if (!sleep.ContainsKey(currentGuard)) {
                        // Since we are only interested in 00-59, 60 will be enough.
                        sleep.Add(currentGuard, new List<long>(new long[60]));
                    }
                    for (int i = startSleep; i < wakeUpTime; i++) {
                        sleep[currentGuard][i] += 1;
                    }
                }
            }

            // Find the guard that slept the most
            int mostSleepy = -1;
            long mostSleep = 0;
            foreach (int guardId in sleep.Keys) {
                long totalSleep = sleep[guardId].Sum(); 
                if (totalSleep > mostSleep) {
                    mostSleep = totalSleep;
                    mostSleepy = guardId;
                }
            }

            // Find which minute he slept the most
            int maxMinute = sleep[mostSleepy].IndexOf(sleep[mostSleepy].Max());
            return (mostSleepy * maxMinute).ToString() + " minutes";
        }

        public string TaskB(string[] input) {
            Dictionary<int, List<long>> sleep = new Dictionary<int, List<long>>();

            // Sort records. This could have done using only 1 DateTime.
            List<string> records = input.ToList().OrderBy(i => DateTime.Parse(i.Split(' ')[0].Remove(0, 1))).ThenBy(i
                => DateTime.Parse(i.Split(' ')[1].Remove(i.Split(' ')[1].Length - 1, 1))).ToList<string>();

            // Parse the records
            int currentGuard = 0;
            int startSleep = 0;
            foreach (string record in records) {
                string[] split = record.Split(' ');
                if (split[2] == "Guard") {
                    // New shift, who is it?
                    int newGuard = Int32.Parse(split[3].Remove(0, 1));
                    currentGuard = newGuard;
                } else if (split[2] == "falls") {
                    startSleep = Int32.Parse(split[1].Remove(split[1].Length - 1, 1).Split(':')[1]);
                } else if (split[2] == "wakes") {
                    int wakeUpTime = Int32.Parse(split[1].Remove(split[1].Length - 1, 1).Split(':')[1]);
                    if (!sleep.ContainsKey(currentGuard)) {
                        // Since we are only interested in 00-59, 60 will be enough.
                        sleep.Add(currentGuard, new List<long>(new long[60]));
                    }
                    for (int i = startSleep; i < wakeUpTime; i++) {
                        sleep[currentGuard][i] += 1;
                    }
                }
            }

            // Find the highest value for any guard/minute pair
            int mostSleepy = -1;
            long mostSleep = 0;
            int mostSleepAt = 0;
            foreach (int guardId in sleep.Keys) {
                // Check his maxminute
                int maxMinute = sleep[guardId].IndexOf(sleep[guardId].Max());
                long maxSleep = sleep[guardId].Max();
                if (maxSleep > mostSleep) {
                    mostSleep = maxSleep;
                    mostSleepAt = maxMinute;
                    mostSleepy = guardId;
                }
            }

            return (mostSleepy * mostSleepAt).ToString() + " minutes";
        }
    }
}
