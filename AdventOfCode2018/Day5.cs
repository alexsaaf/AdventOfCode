using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day5 {


        public string TaskA(string input) {
            string chain = input;
            bool foundReduction = true;
            while (foundReduction) {
                foundReduction = false;
                HashSet<int> removeIndices = new HashSet<int>();
                for (int i = 0; i < chain.Length - 1; i++) {
                    if (chain[i] != chain[i+1] && Char.ToLower(chain[i]) == Char.ToLower(chain[i + 1])) {
                       // Console.WriteLine("Found " + chain[i] + chain[i + 1]);
                        removeIndices.Add(i);
                        foundReduction = true;
                        i++;
                    }
                }
                //Console.WriteLine("Chain: " + chain);
                int indexMod = 0;
                foreach (int index in removeIndices) {
                   //Console.WriteLine("removing " + chain[index] + chain[index + 1]);
                    chain = chain.Remove(index - indexMod, 2);
                    indexMod += 2;
                }
                //Console.WriteLine(removeIndices.Count);
            }

            return chain.Length.ToString();
        }


        public string TaskB(string[] input) {
            int shortest = int.MaxValue;
            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };


            foreach (char character in alphabet) {
                // Remove character from chain and calculate with taskA
                string chainCopy = input[0];
                chainCopy = chainCopy.Replace(character.ToString(), "");
                chainCopy = chainCopy.Replace(Char.ToUpper(character).ToString(), "");
                int result = Int32.Parse(TaskA(chainCopy));
                if (result < shortest)
                    shortest = result;
            }


            return shortest.ToString();
        }

    }
}
