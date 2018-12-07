using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day7 {

        public string TaskA(string[] input) {
            // Build the requirements representation
            Dictionary<string, HashSet<string>> requirements = new Dictionary<string, HashSet<string>>();
            foreach (string line in input) {
                string[] split = line.Split(' ');
                if (requirements.ContainsKey(split[7])) {
                    requirements[split[7]].Add(split[1]);
                } else {
                    requirements.Add(split[7], new HashSet<string>());
                    requirements[split[7]].Add(split[1]);
                }
                if (!requirements.ContainsKey(split[1])) {
                    requirements.Add(split[1], new HashSet<string>());
                }
            }

            string solution = "";

            // Step through requirements, picking the first one (alphabetically) which is available
            while (requirements.Count != 0) {
                var free = requirements.Select(a => a).Where(a => a.Value.Count == 0).OrderBy(a => a.Key).ToList();
                var step = free[0];
                solution += step.Key;
                requirements.Remove(step.Key);
                foreach (string key in requirements.Keys) {
                    if (requirements[key].Contains(step.Key)) {
                        requirements[key].Remove(step.Key);
                    }
                }
            }
            return solution;
        }

        public string TaskB(string[] input) {
            // Build the requirements representation
            Dictionary<string, HashSet<string>> requirements = new Dictionary<string, HashSet<string>>();
            foreach (string line in input) {
                string[] split = line.Split(' ');
                if (requirements.ContainsKey(split[7])) {
                    requirements[split[7]].Add(split[1]);
                } else {
                    requirements.Add(split[7], new HashSet<string>());
                    requirements[split[7]].Add(split[1]);
                }
                if (!requirements.ContainsKey(split[1])) {
                    requirements.Add(split[1], new HashSet<string>());
                }
            }

            Dictionary<string, int> currentWork = new Dictionary<string, int>();
            int currentTime = 0;

            while (requirements.Count != 0) {
                // Find the next job
                var free = requirements.Select(a => a).Where(a => a.Value.Count == 0 && !currentWork.Keys.Contains(a.Key)).OrderBy(a => a.Key).ToList();

                // Put them on currentWork
                int idx = 0;
                while (currentWork.Count < 5 && free.Count > idx) {
                    var step = free[idx];
                    idx++;
                    currentWork[step.Key] = ((int)step.Key[0] - 65 + 61);
                }

                // Find lowest time to finish a work
                var firstToFinish = currentWork.Select(a => a).OrderBy(a => a.Value).ToArray()[0];
                var finishedStep = firstToFinish.Key;
                // Finish it and progress time
                currentTime += firstToFinish.Value;
                currentWork.Remove(finishedStep);

                // Advance time of the others
                foreach (var item in currentWork.Keys.ToList()) {
                    currentWork[item] -= firstToFinish.Value;
                }

                // Remove finished job from requirement list
                requirements.Remove(finishedStep);
                foreach (string key in requirements.Keys) {
                    if (requirements[key].Contains(finishedStep)) {
                        requirements[key].Remove(finishedStep);
                    }
                }
            }
            return currentTime.ToString();
        }
    }
}
