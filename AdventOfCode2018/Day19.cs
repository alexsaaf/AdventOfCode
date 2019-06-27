using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day19 {

        public static string TaskA(string[] input) {
            int[] registers = new int[6];
            // For part B
            //registers[0] = 1;
            int instructionPointerReg = 1;

            int i = registers[instructionPointerReg] - 1;
            string[] instructions = input.Skip(1).ToArray();
            while (i < instructions.Length) {
                // Read from instructionPointerReg
                i++;
                registers[instructionPointerReg] = i;
                PerformAction(ref registers, instructions[i]);
                i = registers[instructionPointerReg];
               // PrintRegisters(registers);
            }
            return registers[0].ToString();
        } 

        
        static void PrintRegisters(int[] registers) {
            string res = "[";
            foreach (int i in registers) {
                res += i + ",";
            }
            res += "]";
            Console.WriteLine(res);
        }

        static void PerformAction(ref int[] registers, string instruction) {
            string[] comp = instruction.Split(' ');
            int a = int.Parse(comp[1]);
            int b = int.Parse(comp[2]);
            int c = int.Parse(comp[3]);
            switch (comp[0]) {
                case "addr":
                    Addr(ref registers, a, b, c);
                    break;
                case "addi":
                    Addi(ref registers, a, b, c);
                    break;
                case "mulr":
                    Mulr(ref registers, a, b, c);
                    break;
                case "muli":
                    Muli(ref registers, a, b, c);
                    break;
                case "setr":
                    Setr(ref registers, a, b, c);
                    break;
                case "seti":
                    Seti(ref registers, a, b, c);
                    break;
                case "gtir":
                    Gtir(ref registers, a, b, c);
                    break;
                case "gtri":
                    Gtri(ref registers, a, b, c);
                    break;
                case "gtrr":
                    Gtrr(ref registers, a, b, c);
                    break;
                case "eqrr":
                    Eqrr(ref registers, a, b, c);
                    break;
                default:
                    Console.WriteLine("Foundothercommand " + comp[0]);
                    break;
            }
        }


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


    }
}
