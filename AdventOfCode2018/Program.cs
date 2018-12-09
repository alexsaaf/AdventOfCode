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
            Console.WriteLine("--- Day 1: Chronal Calibration ---");
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
            // Skipping this because it takes time
            //var day3TaskB = day3.TaskB(ReadLinesFromFile("Inputs/Day3Input.txt"));
            sw.Stop();
            Console.WriteLine("--- Day 3: No Matter How You Slice It ---");
            Console.WriteLine("Answer to part 1 is: " + day3TaskA);
            //Console.WriteLine("Answer to part 2 is: " + day3TaskB);
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion

            #region Day4
            Day4 day4 = new Day4();
            sw.Start();
            var day4TaskA = day4.TaskA(ReadLinesFromFile("Inputs/Day4Input.txt"));
            var day4TaskB = day4.TaskB(ReadLinesFromFile("Inputs/Day4Input.txt"));
            sw.Stop();
            Console.WriteLine("--- Day 4: ---");
            Console.WriteLine("Answer to part 1 is: " + day4TaskA);
            Console.WriteLine("Answer to part 2 is: " + day4TaskB);
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion

            #region Day5
            Day5 day5 = new Day5();
            sw.Start();
            //var day5TaskA = day5.TaskA(ReadLinesFromFile("Inputs/Day5Input.txt")[0]);
            //var day5TaskB = day5.TaskB(ReadLinesFromFile("Inputs/Day5Input.txt"));
            sw.Stop();
            Console.WriteLine("--- Day 5: ---");
            Console.WriteLine("Skipped");
            //Console.WriteLine("Answer to part 1 is: " + day5TaskA);
            //Console.WriteLine("Answer to part 2 is: " + day5TaskB);
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion

            #region Day6
            Day6 day6 = new Day6();
            sw.Start();
            var day6TaskA = day6.TaskA(ReadLinesFromFile("Inputs/Day6Input.txt"));
            var day6TaskB = day6.TaskB(ReadLinesFromFile("Inputs/Day6Input.txt"));
            sw.Stop();
            Console.WriteLine("--- Day 6: ---");
            Console.WriteLine("Answer to part 1 is: " + day6TaskA);
            Console.WriteLine("Answer to part 2 is: " + day6TaskB);
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion

            #region Day7
            Day7 day7 = new Day7();
            sw.Start();
            var day7TaskA = day7.TaskA(ReadLinesFromFile("Inputs/Day7Input.txt"));
            var day7TaskB = day7.TaskB(ReadLinesFromFile("Inputs/Day7Input.txt"));
            sw.Stop();
            Console.WriteLine("--- Day 7: ---");
            Console.WriteLine("Answer to part 1 is: " + day7TaskA);
            Console.WriteLine("Answer to part 2 is: " + day7TaskB);
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion


            #region Day8
            Day8 day8 = new Day8();
            sw.Start();
            var day8TaskA = day8.TaskA(ReadLinesFromFile("Inputs/Day8Input.txt")[0]);
            var day8TaskB = day8.TaskB(ReadLinesFromFile("Inputs/Day8Input.txt")[0]);
            sw.Stop();
            Console.WriteLine("--- Day 8: ---");
            Console.WriteLine("Answer to part 1 is: " + day8TaskA);
            Console.WriteLine("Answer to part 2 is: " + day8TaskB);
            Console.WriteLine("Time elapsed: " + sw.Elapsed.Milliseconds + "\n");
            #endregion


            #region Day9
            sw.Start();
            var day9TaskA = Day9.TaskA(466,71436);
            var day9TaskB = Day9.TaskA(466,71436 * 100);
            sw.Stop();
            Console.WriteLine("--- Day 9: ---");
            Console.WriteLine("Answer to part 1 is: " + day9TaskA);
            Console.WriteLine("Answer to part 2 is: " + day9TaskB);
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
