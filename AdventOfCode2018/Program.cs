using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AdventOfCode2018 {
    class Program {
        static void Main(string[] args) {
            Stopwatch sw = new Stopwatch();

            #region Day1
            Day1 day1 = new Day1();
            sw.Start();
            var day1taskA = day1.TaskA(ReadLinesFromFile("Inputs/Day1Input.txt"));
            var day1TaskB = day1.TaskB(ReadLinesFromFile("Inputs/Day1Input.txt"));
            sw.Stop();
            Console.WriteLine("--- Day 1: Inverse Captcha ---");
            Console.WriteLine("Answer to part 1 is: " + day1taskA);
            Console.WriteLine("Answer to part 2 is: " + day1TaskB);
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion

            Console.ReadLine();
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
