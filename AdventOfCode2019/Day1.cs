using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2019 {
    class Day1 {
            
        public static double TaskA(string[] input) {
            IEnumerable<string> modules = new List<string>(input);
            return modules.Select(module => CalculateFuel(double.Parse(module))).Sum();
        }


        static double CalculateFuel(double mass) {
            return Math.Floor(mass / 3) - 2;
        }

        public static double TaskB(string[] input) {
            IEnumerable<string> modules = new List<string>(input);

            double fuel = 0;

            foreach (double module in modules.Select(module => double.Parse(module))) {
                double requiredFule = CalculateFuel(module);
                fuel += requiredFule;

                double additionalFule = CalculateFuel(requiredFule);
                while (additionalFule > 0) {
                    fuel += additionalFule;
                    additionalFule = CalculateFuel(additionalFule);
                }
            }

            return fuel;
        }

    }
}
