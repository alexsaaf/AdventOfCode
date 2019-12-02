using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2019 {
    class Day2 {

        public static double TaskA(string[] input) {
            List<int> program = input[0].Split(',').Select(num => int.Parse(num)).ToList();
            return IntcodeComputer.ExecuteIntcode(12, 2, program);
        }

        public static double TaskB(string[] input) {
            List<int> program = input[0].Split(',').Select(num => int.Parse(num)).ToList();
            for (int i = 0; i < 100; i++) {
                for (int y = 0; y < 100; y++) {
                    if (IntcodeComputer.ExecuteIntcode(i, y, program) == 19690720) {
                        return 100 * i + y;
                    }
                    program = input[0].Split(',').Select(num => int.Parse(num)).ToList();
                }
            }
            return -1;
        }
    }
}
