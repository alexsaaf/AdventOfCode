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
            Console.WriteLine("--- Day 1: Chronal Calibration ---");
            Console.WriteLine("Answer to part 1 is: " + day1taskA.ToString());
            Console.WriteLine("Answer to part 2 is: " + day1TaskB.ToString());
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
