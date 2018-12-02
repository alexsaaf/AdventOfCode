using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day2 {

        public long CalculateCheckSum(string[] input) {
            // Keep track of how many twos and threes we have found
            long twos = 0;
            long threes = 0;

            foreach (string id in input) {
                Dictionary<char, long> characters = new Dictionary<char, long>();
                foreach (char character in id) {
                    if (characters.ContainsKey(character)) {
                        characters[character]++;
                    } else {
                        characters.Add(character, 1);
                    }
                }
                bool foundTwo = false;
                bool foundThree = false;
                foreach (long item in characters.Values) {
                    if (item == 2) {
                        foundTwo = true;
                    } else if (item == 3) {
                        foundThree = true;
                    }
                }
                if (foundTwo) {
                    twos++;
                }
                if (foundThree) {
                    threes++;
                }
            }
            return twos * threes;
        }

        public string FindCorrectBoxes(string[] input) {
            for (int i = 0; i < input.Length; i++) {
                for (int y = i + 1; y < input.Length; y++) {
                    var matchAt = CheckMatch(input[i], input[y]);
                    if (matchAt != -1) {
                        // Return the value
                        return input[i].Remove(matchAt, 1);
                    }
                } 
            }
            return "No match found";
        }

        int CheckMatch(string a, string b) {
            // Finds where the strings have a diff, returns -1 if there are more than one char diff
            int diffAt = -1;
            for (int i = 0; i < a.Length; i++) {
                if (a[i] == b[i]) {
                    continue;
                } else if (diffAt == -1) {
                    diffAt = i;
                } else {
                    return -1;
                }
            }
            return diffAt;
        }
    }
}
