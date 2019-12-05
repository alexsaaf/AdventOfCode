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

        /// <summary>
        /// This is really messy. Needs big rewrite, but I don't have time this day.
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        public static int ExecuteAdvancedIntcode(List<int> program) {
            int input = 5;
            int lastOutput = 0;
            for (int i = 0; i < program.Count(); ) {
                var commandString = program[i].ToString();
                //Console.WriteLine("Checking pos: " + i + ", command: " + commandString);
                if (program[i].ToString().EndsWith("3")) {
                    // Input 
                    program[program[i + 1]] = input;
                    i += 2;
                } else if (program[i].ToString().EndsWith("4")) {
                    // Output
                    var param = GetParam(1, commandString, i, program);
                    Console.WriteLine(param);
                    lastOutput = param;
                    i += 2;
                } else if (commandString.EndsWith("1")) {
                    // Add
                    var param1 = GetParam(1, commandString, i, program);
                    var param2 = GetParam(2, commandString, i, program);
                    program[program[i + 3]] = param1 + param2;
                    i += 4;
                } else if (commandString.EndsWith("2")) {
                    // Multiply
                    var param1 = GetParam(1, commandString, i, program);
                    var param2 = GetParam(2, commandString, i, program);
                    program[program[i + 3]] = param1 * param2;
                    i += 4;
                } else if (commandString.EndsWith("5")) {
                    // Jump if true
                    var param1 = GetParam(1, commandString, i, program);
                    var param2 = GetParam(2, commandString, i, program);

                    if (param1 != 0) {
                        i = param2;
                    } else {
                        i += 3;
                    }
                } else if (commandString.EndsWith("6")) {
                    // Jump if false
                    var param1 = GetParam(1, commandString, i, program);
                    var param2 = GetParam(2, commandString, i, program);

                    if (param1 == 0) {
                        i = param2;
                    } else {
                        i += 3;
                    }
                } else if (commandString.EndsWith("7")) {
                    // Less than
                    var param1 = GetParam(1, commandString, i, program);
                    var param2 = GetParam(2, commandString, i, program);

                    program[program[i + 3]] = param1 < param2 ? 1 : 0;
                    i += 4;
                } else if (commandString.EndsWith("8")) {
                    // Equals
                    var param1 = GetParam(1, commandString, i, program);
                    var param2 = GetParam(2, commandString, i, program);

                    program[program[i + 3]] = param1 == param2 ? 1 : 0;
                    i += 4;

                } else if (commandString.EndsWith("99")) {
                    return lastOutput;
                }
            }
            return 0;

        }

        static int GetParam(int pos, string commandString, int i, List<int> program) {
            var param = 0;
            if (commandString.Length >= pos + 2) {
                var param1ModePosition = commandString.Length - (pos + 2);
                var param1Mode = commandString[param1ModePosition];
                param = param1Mode == '0' ? program[program[i + pos]] : program[i + pos];
            } else {
                param = program[program[i + pos]];
            }
            return param;
        }

    }
}
