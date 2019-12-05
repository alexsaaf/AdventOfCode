using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2019 {
    class Day5 {

        public static double SolveIntCodeProgram(string[] input) {
            List<int> program = input[0].Split(',').Select(num => int.Parse(num)).ToList();
            return IntcodeComputer.ExecuteAdvancedIntcode(program);
        }

    }
}
