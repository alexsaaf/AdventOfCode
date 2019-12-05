using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2019 {
    class Program {
        static void Main(string[] args) {
            Stopwatch sw = new Stopwatch();

            #region Day1
            sw.Start();
            var day1taskA = Day1.TaskA(ReadLinesFromFile("Inputs/Day1Input.txt"));
            var day1TaskB = Day1.TaskBAlternative(ReadLinesFromFile("Inputs/Day1Input.txt"));
            sw.Stop();
            Console.WriteLine("--- Day 1: The Tyranny of the Rocket Equation ---");
            Console.WriteLine("Answer to part 1 is: " + day1taskA.ToString());
            Console.WriteLine("Answer to part 2 is: " + day1TaskB.ToString());
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion

            #region Day2
            sw.Start();
            var day2TaskA = Day2.TaskA(ReadLinesFromFile("Inputs/Day2Input.txt"));
            var day2TaskB = Day2.TaskB(ReadLinesFromFile("Inputs/Day2Input.txt"));
            sw.Stop();
            Console.WriteLine("--- Day 2: 1202 Program Alarm ---");
            Console.WriteLine("Answer to part 1 is: " + day2TaskA.ToString());
            Console.WriteLine("Answer to part 2 is: " + day2TaskB.ToString());
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion

            #region Day3
            sw.Start();
            var day3 = Day3.TaskA(ReadLinesFromFile("Inputs/Day3Input.txt"));
            //var day2TaskB = Day2.TaskB(ReadLinesFromFile("Inputs/Day2Input.txt"));
            sw.Stop();
            Console.WriteLine("--- Day 3: Crossed Wires ---");
            Console.WriteLine("Answer to part 1 is: " + day3.Item1.ToString());
            Console.WriteLine("Answer to part 2 is: " + day3.Item2.ToString());
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion

            #region Day4
            sw.Start();
            var day4 = Day4.GetNumberOfPasswords(256310, 732736, false);
            var day4B = Day4.GetNumberOfPasswords(256310, 732736, true);
            sw.Stop();
            Console.WriteLine("--- Day 4: Secure Container ---");
            Console.WriteLine("Answer to part 1 is: " + day4.ToString());
            Console.WriteLine("Answer to part 2 is: " + day4B.ToString());
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion

            #region Day5
            sw.Start();
            var day5 = Day5.SolveIntCodeProgram(ReadLinesFromFile("Inputs/Day5Input.txt"));
            //var day4B = Day4.GetNumberOfPasswords(256310, 732736, true);
            sw.Stop();
            Console.WriteLine("--- Day 4: Secure Container ---");
            Console.WriteLine("Answer to part 1 is: " + day5.ToString());
            //Console.WriteLine("Answer to part 2 is: " + day4B.ToString());
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion
        }


        static string[] ReadLinesFromFile(string fileName) {
            //Open the file
            StreamReader file = new System.IO.StreamReader(fileName);

            //Read all the lines
            List<string> instructions = new List<string>();
            string line;
            while ((line = file.ReadLine()) != null) {
                instructions.Add(line);
            }

            //Return them as array
            return instructions.ToArray();
        }
    }
}
