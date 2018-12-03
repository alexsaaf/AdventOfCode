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

            #region Day2
            Day2 day2 = new Day2();
            sw.Start();
            var day2TaskA = day2.CalculateCheckSum(ReadLinesFromFile("Inputs/Day2Input.txt"));
            var day2TaskB = day2.FindCorrectBoxes(ReadLinesFromFile("Inputs/Day2Input.txt"));
            sw.Stop();
            Console.WriteLine("--- Day 2: Inventory Management System ---");
            Console.WriteLine("Answer to part 1 is: " + day2TaskA);
            Console.WriteLine("Answer to part 2 is: " + day2TaskB);
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion

            #region Day3
            Day3 day3 = new Day3();
            sw.Start();
            var day3TaskA = day3.TaskA(ReadLinesFromFile("Inputs/Day3Input.txt"));
            var day3TaskB = day3.TaskB(ReadLinesFromFile("Inputs/Day3Input.txt"));
            sw.Stop();
            Console.WriteLine("--- Day 3: ---");
            Console.WriteLine("Answer to part 1 is: " + day3TaskA);
            Console.WriteLine("Answer to part 2 is: " + day3TaskB);
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
