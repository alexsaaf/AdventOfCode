using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day1 {

        public float TaskA(string[] input) {
            long result = 0;

            foreach (string change in input) {
                var digit = Int32.Parse(change.Substring(1));
                if (change[0] == '+') {
                    result += digit;
                } else {
                    result -= digit;
                }
            }

            return result;
        }

        public float TaskB(string[] input) {
            HashSet<long> foundFreq = new HashSet<long>();
            long result = 0;
            foundFreq.Add(0);

            while (true) {

            foreach (string change in input) {
                var digit = Int32.Parse(change.Substring(1));
                if (change[0] == '+') {
                    result += digit;
                } else {
                    result -= digit;
                }
                if (foundFreq.Contains(result)) {
                    return result;
                }
                foundFreq.Add(result);
            }

            }
        }
    }
}
