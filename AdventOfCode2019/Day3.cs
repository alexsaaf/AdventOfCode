using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2019 {
    class Day3 {

        public static Tuple<int, int> TaskA(string[] input) {
            Dictionary<(int, int), int> wireA = GetPointsInWire(input[0]);
            Dictionary<(int, int), int> wireB = GetPointsInWire(input[1]);

            var common = wireA.Keys.Intersect(wireB.Keys);
            var res = common.Min(x => Math.Abs(x.Item1) + Math.Abs(x.Item2));
            var resB = common.Min(x => wireA[x] + wireB[x]) + 2;

            return Tuple.Create(res, resB); 
        }

        static Dictionary<(int,int),int> GetPointsInWire(string path) {
            Dictionary<(int, int), int> result = new Dictionary<(int, int), int>();

            int x = 0, y = 0, steps = 0;

            string[] moves = path.Split(",");
            foreach (string move in moves) {
                int dist = int.Parse(move.Substring(1));
                switch (move[0]) {
                    case 'U':
                        for (int i = 0; i < dist; i++) {
                            result.TryAdd((x, ++y), steps++);
                        }
                        break;
                    case 'D':
                        for (int i = 0; i < dist; i++) {
                            result.TryAdd((x, --y), steps++);
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < dist; i++) {
                            result.TryAdd((++x, y), steps++);
                        }
                        break;
                    case 'L':
                        for (int i = 0; i < dist; i++) {
                            result.TryAdd((--x, y), steps++);
                        }
                        break;
                }
            }
            return result;
        }
    }
}
