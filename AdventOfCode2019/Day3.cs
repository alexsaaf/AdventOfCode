using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2019 {
    class Day3 {

        public static double TaskA(string[] input) {
            List<Vector2> wireA = GetPointsAlongPath(input[0]);
            List<Vector2> wireB = GetPointsAlongPath(input[1]);

            //Console.WriteLine(firstLinePoints.Count() + "," + secondLinePoints.Count());
            var common = wireA.Intersect(wireB);
            //Console.WriteLine(common.Count());
            //common.Select(x => Util.CalculateManhattan(0, 0, (int)x.X, (int)x.Y)).ToList().ForEach(x => Console.WriteLine(x));
            //Console.WriteLine(common.Select(x => Util.CalculateManhattan(0, 0, (int)x.X, (int)x.Y)).First());
            var res = common.Min(x => Math.Abs(x.X) + Math.Abs(x.Y));

            // get shared

            // Pick closest shared

            return res; 
        }

        static Vector2 up = new Vector2(0, 1);
        static Vector2 right = new Vector2(1, 0);

        static List<Vector2> GetPointsAlongPath(string path) {
            List<Vector2> result = new List<Vector2>();

            long x = 0;
            long y = 0;

            string[] moves = path.Split(",");

            foreach (string move in moves) {
                char dir = move[0];
                int dist = int.Parse(move.Substring(1));
                switch (dir) {
                    case 'U':
                        result.AddRange(WalkDistance(x, y, up, dist));
                        y += dist;
                        break;
                    case 'D':
                        result.AddRange(WalkDistance(x, y, up * -1, dist));
                        y -= dist;
                        break;
                    case 'R':
                        result.AddRange(WalkDistance(x, y, right, dist));
                        x += dist;
                        break;
                    case 'L':
                        result.AddRange(WalkDistance(x, y, right * -1, dist));
                        x -= dist;
                        break;
                }
            }

            return result;
        }

        static List<Vector2> WalkDistance(long x, long y, Vector2 dir, int distance) {
            List<Vector2> result = new List<Vector2>();
            for (int i = 1; i <= distance; i++) {
                result.Add(new Vector2(x + dir.X * i, y + dir.Y * i));
            };
            return result;
        }
    }
}
