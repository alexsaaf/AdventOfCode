using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day18 {
        static int height;
        static int width;

        public static string TaskA(string[] input) {
            height = input.Length;
            width = input[0].Length;
            char[,] map = new char[width, height];


            // Read map
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    map[x, y] = input[y][x];
                }
            }
            //PrintMap(map);

            int minutes = 10;
            for (int i = 0; i < minutes; i++) {
                map = Advance(map, width, height);
                
            }

            int trees = 0;
            int mills = 0;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (map[x,y] == '#') {
                        mills++;
                    } else if (map[x,y] == '|') {
                        trees++;
                    }
                }
            }
            Console.WriteLine("Trees: " + trees + " mills: " + mills);
            //PrintMap(map);
            return (trees * mills).ToString();
        }

        public static string TaskB(string[] input) {
            height = input.Length;
            width = input[0].Length;
            char[,] map = new char[width, height];


            // Read map
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    map[x, y] = input[y][x];
                }
            }

            Dictionary<char[,], long> seenMaps = new Dictionary<char[,], long>();
            int minutes = 1000000000;
            for (long i = 0; i < minutes; i++) {
                map = Advance(map, width, height);

                // This is a bit wonky, but we cannot simple check if dictionary contains key because map is char[,]
                if (seenMaps.Where(a => a.Key.Rank == map.Rank &&
    Enumerable.Range(0, a.Key.Rank).All(dimension =>
         a.Key.GetLength(dimension) == map.GetLength(dimension)) && a.Key.Cast<char>().SequenceEqual(map.Cast<char>())).Count() > 0) {
                    var found = seenMaps.Where(a => a.Key.Rank == map.Rank &&
  Enumerable.Range(0, a.Key.Rank).All(dimension =>
       a.Key.GetLength(dimension) == map.GetLength(dimension)) && a.Key.Cast<char>().SequenceEqual(map.Cast<char>())).First();

                    long Skip = (minutes - i) / (i - seenMaps[found.Key]);
                    i += Skip * (i - seenMaps[found.Key]);
                } else {
                    seenMaps.Add(map, i);
                }
                if (i == minutes - 1) {
                    break;
                }
            }

            int trees = 0;
            int mills = 0;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (map[x, y] == '#') {
                        mills++;
                    } else if (map[x, y] == '|') {
                        trees++;
                    }
                }
            }
            Console.WriteLine("Trees: " + trees + " mills: " + mills);
            //PrintMap(map);
            return (trees * mills).ToString();
        }

        static void PrintMap(char[,] map) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    sb.Append(map[j, i]);
                }
                sb.Append("\n");
            }
            Console.WriteLine(sb.ToString());
        }

        static char[,] Advance(char[,] map, int width, int height) {
            char[,] nextMap = new char[width, height];

            // Check every tile
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    var neighborCoordinates = GetNeighbors(x, y);
                    // Check rules
                    if (map[x,y] == '.' && neighborCoordinates.Count(a => map[a.Item1, a.Item2] == '|') >= 3) {
                        nextMap[x, y] = '|';
                    } else if (map[x,y] == '|' && neighborCoordinates.Count(a => map[a.Item1, a.Item2] == '#') >= 3) {
                        nextMap[x, y] = '#';
                    } else if (map[x, y] == '#' && (neighborCoordinates.Count(a => map[a.Item1, a.Item2] == '#') < 1
                        || neighborCoordinates.Count(a => map[a.Item1, a.Item2] == '|') < 1)) {
                        nextMap[x, y] = '.';
                    } else {
                        nextMap[x, y] = map[x, y];
                    }
                }
            }
            return nextMap;
        }

        static List<(int,int)> GetNeighbors(int x, int y) {
            List<(int, int)> res = new List<(int, int)>();
            if (x > 0) {
                res.Add((x - 1, y));
                if (y > 0) {
                    res.Add((x - 1, y - 1));
                }
                if (y < height - 1) {
                    res.Add((x - 1, y + 1));
                }
            }
            if (x < width - 1) {
                res.Add((x + 1, y));
                if (y > 0) {
                    res.Add((x + 1, y - 1));
                }
                if (y < height - 1) {
                    res.Add((x + 1, y + 1));
                }
            }
            if (y > 0) {
                res.Add((x, y - 1));
            }
            if (y < height - 1) {
                res.Add((x, y + 1));
            }
            return res;
        }


    }
}
