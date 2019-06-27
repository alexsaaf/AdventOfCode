using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day12 {
        public static string TaskA(string[] input) {
            string initialState = input[0].Split(':')[1].Remove(0, 1);
            int origin = initialState.Length + 2;
            char[] pots = new char[initialState.Length * 3];

            for (int i = 0; i < origin; i++) {
                pots[i] = '.';
            }
            for (int i = origin; i < origin + initialState.Length; i++) {
                pots[i] = initialState[i - origin];
            }
            for (int i = origin + initialState.Length; i < pots.Length; i++) {
                pots[i] = '.';
            }

            Dictionary<string, char> rules = new Dictionary<string, char>();
            foreach(string rule in input.Skip(2)) {
                string[] split = rule.Split(' ');
                string key = split[0];
                char res = split[2] == "." ? '.' : '#';
                rules.Add(key, res);
            }

            // The twenty generations
            for (int i = 0; i < 20; i++) {
                char[] nextState = new char[pots.Length]; 
                for (int pot = 0; pot < pots.Length; pot++) {
                    // Initially set no plant
                    nextState[pot] = '.';
                    var left2 = (pot == 0 || pot == 1) ? '.' : pots[pot - 2];
                    var left1 = pot == 0 ? '.' : pots[pot - 1];
                    var right1 = (pot == pots.Length - 1) ? '.' : pots[pot + 1];
                    var right2 = (pot == pots.Length - 1 || pot == pots.Length - 2) ? '.' : pots[pot + 2];
                    foreach (var rule in rules) {
                        // Check if the rule matches
                        if (rule.Key[0] == left2 
                            && rule.Key[1] == left1
                            && rule.Key[2] == pots[pot]
                            && rule.Key[3] == right1
                            && rule.Key[4] == right2) {
                            // We have a match!
                            nextState[pot] = rule.Value;
                            break;
                        }
                    }
                }
                pots = nextState;
            }

            int sum = 0;
            for (int i = 0; i < pots.Length; i++) {
                if (pots[i] == '#') {
                    sum += (i - origin);
                }
            }
            return sum.ToString();
        }

        /// <summary>
        /// Had to remake the approach for task 2. The approach in task 1 does not allow the problem to grow endlessly (left/right).
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string TaskB(string[] input) {
            string initialState = input[0].Split(':')[1].Remove(0, 1);

            Dictionary<string, char> rules = new Dictionary<string, char>();
            foreach (string rule in input.Skip(2)) {
                string[] split = rule.Split(' ');
                string key = split[0];
                char res = split[2] == "." ? '.' : '#';
                rules.Add(key, res);
            }

            string currentGen = initialState;
            long maxGens = 50000000000;
            int currLeft = 0;   // Used to keep track of how many digits are left of "0".
            long score = 0;
            long lastScore = 0;
            long scoreDiff = 0;
            long prevDiff = 0;

            for (int gen = 1; gen <= maxGens; gen++) {
                StringBuilder nextGen = new StringBuilder();
                for (int pos = -2; pos < currentGen.Length + 2; pos++) {
                    string state = string.Empty;
                    int distFromEnd = currentGen.Length - pos;
                    // Build the 5 character string we are currently checking for
                    if (pos <= 1)
                        state = new string('.', 2 - pos) + currentGen.Substring(0, 3 + pos);
                    else if (distFromEnd <= 2)
                        state = currentGen.Substring(pos - 2, distFromEnd + 2) + new string('.', 3 - distFromEnd);
                    else
                        state = currentGen.Substring(pos - 2, 5);
                    // Check if we can find this rule, if not, put a .
                    nextGen.Append(rules.TryGetValue(state, out char newState) ? newState : '.');
                }
                currentGen = nextGen.ToString();
                currLeft -= 2;  // We added two characters to the left end of the string

                score = 0;
                // Calculate score
                for (int pos = 0; pos < currentGen.Length; pos++) {
                    score += currentGen[pos].ToString() == "." ? 0 : pos + currLeft;
                }
                // Check the diff. If we have the same diff, we have found the stable increase
                scoreDiff = score - lastScore;
                if (scoreDiff == prevDiff) {
                    score = score + (maxGens - (long)gen) * prevDiff;
                    break;
                }
                prevDiff = scoreDiff;
                lastScore = score;
            }
            return score.ToString();
        }
    }
}
