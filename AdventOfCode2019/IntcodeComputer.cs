using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019 {
    class IntcodeComputer {

        public static int ExecuteIntcode(int inputA, int inputB, List<int> memory) {
            memory[1] = inputA;
            memory[2] = inputB;

            for (int i = 0; i < memory.Count(); i += 4) {
                if (memory[i] == 1) {
                    memory[memory[i + 3]] = memory[memory[i + 1]] + memory[memory[i + 2]];
                } else if (memory[i] == 2) {
                    memory[memory[i + 3]] = memory[memory[i + 1]] * memory[memory[i + 2]];
                } else {
                    return memory[0];
                }
            }
            return 0;
        }
    }
}
