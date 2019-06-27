using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day14 {

        public static string Taskb() {
            int input = 209231;

            int[] currentRecipe = new int[2];
            List<int> recipes = new List<int>();
            recipes.Add(3);
            recipes.Add(7);
            currentRecipe[0] = 0;
            currentRecipe[1] = 1;

            int recipesCreated = 2;
            string res = "";


            while (recipesCreated < input) {
                // Create recipes
                long mix = recipes[currentRecipe[0]] + recipes[currentRecipe[1]];
                int[] intList = mix.ToString().Select(digit => int.Parse(digit.ToString())).ToArray();
                foreach (int recipe in intList) {
                        recipes.Add(recipe);
                        recipesCreated++;
                        if (recipesCreated > input) {
                        res += recipe.ToString();
                    }
                }
                

                // Update position of 0
                int newVal = 1 + recipes[currentRecipe[0]];
                currentRecipe[0] = (currentRecipe[0] + newVal) % recipes.Count;

                // Update position of 1
                int newVal1 = 1 + recipes[currentRecipe[1]];
                currentRecipe[1] = (currentRecipe[1] + newVal1) % recipes.Count;

            }


            while (res.Length < 10) {
                // Create recipes
                long mix = recipes[currentRecipe[0]] + recipes[currentRecipe[1]];
                int[] intList = mix.ToString().Select(digit => int.Parse(digit.ToString())).ToArray();
                foreach (int recipe in intList) {
                    recipes.Add(recipe);
                    if (res.Length < 10) {
                        res += recipe.ToString();
                    }
                }

                // Update position of 0
                int newVal = 1 + recipes[currentRecipe[0]];
                currentRecipe[0] = (currentRecipe[0] + newVal) % recipes.Count;

                // Update position of 1
                int newVal1 = 1 + recipes[currentRecipe[1]];
                currentRecipe[1] = (currentRecipe[1] + newVal1) % recipes.Count;
            }
            Console.WriteLine(res);
            return res;
        }

        public static string TaskA() {
            int input = 209231;

            int[] currentRecipe = new int[2];
            List<int> recipes = new List<int>();
            recipes.Add(3);
            recipes.Add(7);
            currentRecipe[0] = 0;
            currentRecipe[1] = 1;

            int recipesCreated = 2;
            string res = "";


            while (true) {
                // Create recipes
                long mix = recipes[currentRecipe[0]] + recipes[currentRecipe[1]];
                int[] intList = mix.ToString().Select(digit => int.Parse(digit.ToString())).ToArray();
                foreach (int recipe in intList) {
                    recipes.Add(recipe);
                    recipesCreated++;
                        res += recipe.ToString();
                    if (res == input.ToString()) {
                        Console.WriteLine(recipesCreated - input.ToString().Length);
                        return (recipesCreated - input.ToString().Length).ToString();
                    } else if (input.ToString().StartsWith(res)) {
                        // Do nothing
                    } else {
                        res = "";
                    }
                }
                // Update position of 0
                int newVal = 1 + recipes[currentRecipe[0]];
                currentRecipe[0] = (currentRecipe[0] + newVal) % recipes.Count;

                // Update position of 1
                int newVal1 = 1 + recipes[currentRecipe[1]];
                currentRecipe[1] = (currentRecipe[1] + newVal1) % recipes.Count;
            }
            return "";
        }


    }
}
