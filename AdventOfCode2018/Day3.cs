using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day3 {



        public string TaskA(string[] input) {
            int[,] fabric = new int[5000,5000];

            foreach (string claim in input) {
                string[] split = claim.Split(' ');
                int yStart = Int32.Parse(split[2].Split(',')[1].Remove(split[2].Split(',')[1].Length - 1));
                int xStart = Int32.Parse(split[2].Split(',')[0]);
                int width = Int32.Parse(split[3].Split('x')[0]);
                int height = Int32.Parse(split[3].Split('x')[1]);

                //Console.WriteLine("Adding from: x:" + xStart + " y:" + yStart + " w: " + width + " h: " + height);

                for (int x = xStart; x < xStart + width; x++) {
                    for (int y = yStart; y < yStart +height; y++) {
                        fabric[y, x]++;
                    }
                }
            }
            long count = 0;
            foreach (int val in fabric) {
                if (val >= 2) {
                    count++;
                }
            }
            return count.ToString();
        }

        /// <summary>
        /// This solution is somewhat slow but it was the first one to come to mind.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string TaskB(string[] input) {
            // Keep a set of which claims have tried to clame each square
            HashSet<int>[,] fabric = new HashSet<int>[5000, 5000];
            long highestClaim = 0;
            foreach (string claim in input) {
                string[] split = claim.Split(' ');
                int yStart = Int32.Parse(split[2].Split(',')[1].Remove(split[2].Split(',')[1].Length - 1));
                int xStart = Int32.Parse(split[2].Split(',')[0]);
                int width = Int32.Parse(split[3].Split('x')[0]);
                int height = Int32.Parse(split[3].Split('x')[1]);
                int claimId = Int32.Parse(split[0].Remove(0, 1));

                for (int x = xStart; x < xStart + width; x++) {
                    for (int y = yStart; y < yStart + height; y++) {
                        if (fabric[y,x] != null) {
                            fabric[y, x].Add(claimId);
                        } else {
                            fabric[y, x] = new HashSet<int>();
                            fabric[y, x].Add(claimId);
                        }
                    }
                }
                highestClaim = claimId;
            }
            // For each ID, check if they are alone in their squares
            for (int i = 1; i <= highestClaim; i++) {
                bool foundIssue = false;
                foreach (HashSet<int> set in fabric) {
                    if (set == null) {
                        // OK!
                    } else if (!set.Contains(i)) {
                        // Ok!
                    } else if (set.Count == 1) {
                        // OK!
                    } else {
                        // Bad!
                        foundIssue = true;
                    }
                }
                if (foundIssue) {
                    continue;
                } else {
                    return i.ToString();
                }
            }
            return "nothing found";
        }
    }
}
