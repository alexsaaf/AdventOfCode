using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
   
    class Day16 {

        interface operation {
            string solve(TestCase test);
        }

        class TestCase {
            public Int32[] beforeState;
            public int opCode;
            public int A;
            public int B;
            public int C;
            public Int32[] afterState;

            public TestCase(Int32[] beforeState, int opCode, int inputA, int inputB, int outPut, Int32[] afterState) {
                this.beforeState = beforeState;
                this.opCode = opCode;
                this.A = inputA;
                this.B = inputB;
                this.C = outPut;
                this.afterState = afterState;
            }
        }


        public static string TaskA(string[] input) {
            int res = 0;


            List<int>[] possible = new List<int>[16];

            for (int i = 0; i < 16; i++)
                possible[i] = new List<int>();

            // Read all the cases
            for (int i = 0; i < input.Length; i += 4) {
                // Parse beforestate
                int reg1, reg2, reg3, reg4;
                reg1 = Convert.ToInt32(input[i][9].ToString());
                reg2 = Convert.ToInt32(input[i][12].ToString());
                reg3 = Convert.ToInt32(input[i][15].ToString());
                reg4 = Convert.ToInt32(input[i][18].ToString());

                var middleLine = input[i + 1].Split(' ');
                int opCode = Convert.ToInt32(middleLine[0]);
                int inputA = Convert.ToInt32(middleLine[1]);
                int inputB = Convert.ToInt32(middleLine[2]);
                int output = Convert.ToInt32(middleLine[3]);
                Console.WriteLine("Read input: opCode: " + opCode + " A: " + inputA + " B: " + inputB + " C: " + output);

                // Parse afterState
                int res1, res2, res3, res4;
                res1 = Convert.ToInt32(input[i + 2][9].ToString());
                res2 = Convert.ToInt32(input[i + 2][12].ToString());
                res3 = Convert.ToInt32(input[i + 2][15].ToString());
                res4 = Convert.ToInt32(input[i + 2][18].ToString());


                ops[] commands = new ops[16] {
                 Addr,
                 Addi,
                Mulr,
                Muli,
                Banr,
                Bani,
                Borr,
                Bori,
                Setr,
                Seti,
                Gtir,
                Gtri,
                Gtrr,
                Eqir,
                Eqri,
                Eqrr
            };

                int matches = 0;
                for (int j = 0; j < commands.Length; j++) {
                    int[] registry = { reg1, reg2, reg3, reg4 };
                    commands[j](ref registry, inputA, inputB, output);
                    if (registry[0] == res1
                        && registry[1] == res2
                        && registry[2] == res3
                        && registry[3] == res4) {
                        if (!possible[j].Contains(opCode))
                            possible[j].Add(opCode);
                        matches++;
                    }
                }
                if (matches >= 3) {
                    res++;
                }
            }

            return res.ToString();
        }

        public static string TaskB(string[] input) {
            ops[] commands = new ops[16] {
            Eqir,
            Addi,
            Gtir,
            Setr,
            Mulr,
            Seti,
            Muli,
            Eqri,
            Bori,
            Bani,
            Gtrr,
            Eqrr,
            Addr,
            Gtri,
            Borr,
            Banr
        };


            int[] reg = { 0, 0, 0, 0 };

            foreach (string line in input) {
                var split = line.Split(' ');
                int opCode = Convert.ToInt32(split[0]);
                int inputA = Convert.ToInt32(split[1]);
                int inputB = Convert.ToInt32(split[2]);
                int output = Convert.ToInt32(split[3]);

                commands[opCode](ref reg, inputA, inputB, output);
            }

            return reg[0].ToString();
        }


        public static void Solve() {
            Console.WriteLine("Day 16 - Question 1:");

            string input = File.ReadAllText("Inputs/Day16Input.txt");

            //foreach (char c in input) {
            //    Console.WriteLine(c);
            //}

            string[] inputs = input.Split(new string[] { "\n\r\n" }, StringSplitOptions.None);

            int three = 0;

            List<int>[] possible = new List<int>[16];

            for (int i = 0; i < 16; i++)
                possible[i] = new List<int>();

            foreach (string s in inputs) {
                string[] lines = s.Split('\n');

                int reg1, reg2, reg3, reg4;
                reg1 = Convert.ToInt32(lines[0][9].ToString());
                reg2 = Convert.ToInt32(lines[0][12].ToString());
                reg3 = Convert.ToInt32(lines[0][15].ToString());
                reg4 = Convert.ToInt32(lines[0][18].ToString());

                int com1, com2, com3, com4;
                string[] coms = lines[1].Split(' ');
                com1 = Convert.ToInt32(coms[0]);
                com2 = Convert.ToInt32(coms[1]);
                com3 = Convert.ToInt32(coms[2]);
                com4 = Convert.ToInt32(coms[3]);

                int res1, res2, res3, res4;
                res1 = Convert.ToInt32(lines[2][9].ToString());
                res2 = Convert.ToInt32(lines[2][12].ToString());
                res3 = Convert.ToInt32(lines[2][15].ToString());
                res4 = Convert.ToInt32(lines[2][18].ToString());

                ops[] re = new ops[16] {
            Addr,
            Addi,
            Mulr,
            Muli,
            Banr,
            Bani,
            Borr,
            Bori,
            Setr,
            Seti,
            Gtir,
            Gtri,
            Gtrr,
            Eqir,
            Eqri,
            Eqrr
        };

                int correct = 0;
                for (int i = 0; i < re.Length; i++) {
                    int[] registry = { reg1, reg2, reg3, reg4 };
                    re[i](ref registry, com2, com3, com4);
                    if (registry[0] == res1 && registry[1] == res2 && registry[2] == res3 && registry[3] == res4) {
                        correct++;
                        if (!possible[i].Contains(com1))
                            possible[i].Add(com1);
                    }
                }

                if (correct >= 3)
                    three++;

                //Console.WriteLine($"{reg1} - {reg2} - {reg3} - {reg4} --- {com1} - {com2} - {com3} - {com4} --- {res1} - {res2} - {res3} - {res4}");
            }
            Console.WriteLine($"Answer Q16.1: {three}\n");
            /*
            input = File.ReadAllText($"{path}Advent of Code - Day 16 - Op Code Program.txt");
            inputs = input.Split('\n');

            int[] reg = { 0, 0, 0, 0 };

            ops[] commands = new ops[16] {
            Borr,
            Seti,
            Mulr,
            Eqri,
            Banr,
            Bori,
            Bani,
            Gtri,
            Addr,
            Muli,
            Addi,
            Eqrr,
            Gtir,
            Eqir,
            Setr,
            Gtrr
        };

            foreach (string s in inputs) {
                int com1, com2, com3, com4;
                string[] coms = s.Split(' ');
                com1 = Convert.ToInt32(coms[0]);
                com2 = Convert.ToInt32(coms[1]);
                com3 = Convert.ToInt32(coms[2]);
                com4 = Convert.ToInt32(coms[3]);

                commands[com1](ref reg, com2, com3, com4);
            }

            Console.WriteLine($"Answer Q16.2: {reg[0]}\n");*/
        }

        private delegate void ops(ref int[] reg, int a, int b, int c);

        private static void Addr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] + reg[b];

        private static void Addi(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] + b;

        private static void Mulr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] * reg[b];

        private static void Muli(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] * b;

        private static void Banr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] & reg[b];

        private static void Bani(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] & b;

        private static void Borr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] | reg[b];

        private static void Bori(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] | b;

        private static void Setr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a];

        private static void Seti(ref int[] reg, int a, int b, int c) => reg[c] = a;

        private static void Gtir(ref int[] reg, int a, int b, int c) => reg[c] = a > reg[b] ? 1 : 0;

        private static void Gtri(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] > b ? 1 : 0;

        private static void Gtrr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] > reg[b] ? 1 : 0;

        private static void Eqir(ref int[] reg, int a, int b, int c) => reg[c] = a == reg[b] ? 1 : 0;

        private static void Eqri(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] == b ? 1 : 0;

        private static void Eqrr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] == reg[b] ? 1 : 0;

        /*
        static int[] addr(TestCase test) {
            int[] result = test.beforeState;
            int val = test.beforeState[test.A] + test.beforeState[test.B];
            result[test.C] = val;
            return result;
        }

        static int[] addi(TestCase test) {
            int[] result = test.beforeState;
            int val = test.beforeState[test.A] + test.B;
            result[test.C] = val;
            return result;
        }

        static int[] mulr(TestCase test) {
            int[] result = test.beforeState;
            int val = test.beforeState[test.A] * test.beforeState[test.B];
            result[test.C] = val;
            return result;
        }

        static int[] muli(TestCase test) {
            int[] result = test.beforeState;
            int val = test.beforeState[test.A] * test.B;
            result[test.C] = val;
            return result;
        }


        static int[] banr(TestCase test) {
            int[] result = test.beforeState;
            int val = test.beforeState[test.A] & test.beforeState[test.B];
            result[test.C] = val;
            return result;
        }

        static int[] bani(TestCase test) {
            int[] result = test.beforeState;
            int val = test.beforeState[test.A] & test.B;
            result[test.C] = val;
            return result;
        }

        static int[] borr(TestCase test) {
            int[] result = test.beforeState;
            int val = test.beforeState[test.A] | test.beforeState[test.B];
            result[test.C] = val;
            return result;
        }

        static int[] bori(TestCase test) {
            int[] result = test.beforeState;
            int val = test.beforeState[test.A] | test.B;
            result[test.C] = val;
            return result;
        }

        static int[] setr(TestCase test) {
            int[] result = test.beforeState;
            result[test.C] = test.beforeState[test.A];
            return result;
        }

        static int[] seti(TestCase test) {
            int[] result = test.beforeState;
            result[test.C] = test.A;
            return result;
        }

        static int[] gtir(TestCase test) {
            int[] result = test.beforeState;
            if (test.A > test.beforeState[test.B]) {
                result[test.C] = 1;
            } else {
                result[test.C] = 0;
            }
            return result;
        }

        static int[] gtri(TestCase test) {
            int[] result = test.beforeState;
            if (test.beforeState[test.A] > test.B) {
                result[test.C] = 1;
            } else {
                result[test.C] = 0;
            }
            return result;
        }

        static int[] gtrr(TestCase test) {
            int[] result = test.beforeState;
            if (test.beforeState[test.A] > test.beforeState[test.B]) {
                result[test.C] = 1;
            } else {
                result[test.C] = 0;
            }
            return result;
        }

        static int[] eqir(TestCase test) {
            int[] result = test.beforeState;
            if (test.A == test.beforeState[test.B]) {
                result[test.C] = 1;
            } else {
                result[test.C] = 0;
            }
            return result;
        }

        static int[] eqri(TestCase test) {
            int[] result = test.beforeState;
            if (test.beforeState[test.A] == test.B) {
                result[test.C] = 1;
            } else {
                result[test.C] = 0;
            }
            return result;
        }

        static int[] eqrr(TestCase test) {
            int[] result = test.beforeState;
            if (test.beforeState[test.A] == test.beforeState[test.B]) {
                result[test.C] = 1;
            } else {
                result[test.C] = 0;
            }
            return result;
        }*/
    }
}
