using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day17 {

        int maxY = 0;
        int minY = int.MaxValue;
        char[,] grid;

        public string TaskA(string[] input) {
            var x = 3000;
            var y = 3000;
            grid = new char[x,y];

            foreach (string line in input) {
                var l = line.Split(new[] { '=', ',', '.' });

                if (l[0] == "x") {
                    x = int.Parse(l[1]);
                    y = int.Parse(l[3]);
                    var len = int.Parse(l[5]);
                    for (var i = y; i <= len; i++) {
                        grid[x, i] = '#';
                    }
                } else {
                    y = int.Parse(l[1]);
                    x = int.Parse(l[3]);
                    var len = int.Parse(l[5]);
                    for (var i = x; i <= len; i++) {
                        grid[i, y] = '#';
                    }
                }
                if (y > maxY) {
                    maxY = y;
                }

                if (y < minY) {
                    minY = y;
                }
            }

            int springX = 500;
            int springY = 0;

            GoDown(springX, springY);

            // count spaces with water
            var t = 0;
            for (y = minY; y < grid.GetLength(1); y++) {
                for (x = 0; x < grid.GetLength(0); x++) {
                    //if (grid[x, y] == 'W' || grid[x, y] == '|') // Part 1
                    if (grid[x,y] == 'W') // Part 2
                    {
                        t++;
                    }
                }
            }
            return t.ToString();

        }

        private bool SpaceTaken(int x, int y) {
            return grid[x, y] == '#' || grid[x, y] == 'W';
        }

        public void GoDown(int x, int y) {
            grid[x, y] = '|';
            while (grid[x, y + 1] != '#' && grid[x, y + 1] != 'W') {
                y++;
                if (y > maxY)
                    return;
                grid[x, y] = '|';
            }

            do {
                bool downLeft = false;
                bool downRight = false;

                int minX;
                for (minX = x; minX >= 0; minX--) {
                    if (SpaceTaken(minX, y + 1) == false) {
                        downLeft = true;
                        break;
                    }

                    grid[minX, y] = '|';

                    if (SpaceTaken(minX - 1, y)) {
                        break;
                    }
                }

                int maxX;
                for (maxX = x; maxX < grid.GetLength(0); maxX++) {
                    if (SpaceTaken(maxX, y + 1) == false) {
                        downRight = true;
                        break;
                    }

                    grid[maxX, y] = '|';

                    if (SpaceTaken(maxX + 1, y)) {
                        break;
                    }
                }
                if (downLeft) {
                    if (grid[minX, y] != '|')
                        GoDown(minX, y);
                }

                if (downRight) {
                    if (grid[maxX, y] != '|')
                        GoDown(maxX, y);
                }
                if (downLeft || downRight) {
                    return;
                }

                // Fill this row
                for (int a = minX; a < maxX + 1; a++) {
                    grid[a, y] = 'W';
                }

                y--;
            } while (true);
        } 
    }
}
