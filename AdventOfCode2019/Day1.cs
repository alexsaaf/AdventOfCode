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

            return modules.Select(module => double.Parse(module)).Sum(mass => {
                double added = 0;
                double newFuel = CalculateFuel(mass);
                while (newFuel > 0) {
                    added += newFuel;
                    newFuel = CalculateFuel(newFuel);
                }
                return added;
            });
        }

        public static double TaskBAlternative(string[] input) {
            IEnumerable<double> modules = new List<string>(input).Select(module => double.Parse(module));

            double CalculateFuel(double weight) {
                double baseFuel = Math.Floor(weight / 3) - 2;
                if (baseFuel <= 0)
                    return 0;
                return baseFuel + CalculateFuel(baseFuel);
            }

            return modules.Sum(CalculateFuel);
        }

    }
}
