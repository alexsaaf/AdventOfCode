using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day10 {

        public class Point {
            public int x, y;
            public int velX, velY;

            public Point(int x, int y, int velX, int velY) {
                this.x = x;
                this.y = y;
                this.velX = velX;
                this.velY = velY;
            }

            public void Advance() {
                this.x += velX;
                this.y += velY;
            }
        }

        public static string TaskA(string[] input) {
            HashSet<Point> points = new HashSet<Point>();
            // Read points
            foreach (string line in input) {
                string[] split = line.Split('<');
                string posStr = split[1].Split('>')[0];
                int x = Int32.Parse(posStr.Split(',')[0]);
                int y = Int32.Parse(posStr.Split(',')[1]);
                int xVel = Int32.Parse(split[2].Split(',')[0]);
                int yVel = Int32.Parse(split[2].Split(',')[1].Remove(split[2].Split(',')[1].Length - 1));
                points.Add(new Point(x, y, xVel, yVel));
            }

            // Found 10417 by running up to 20000 and picking the one with the smallest MinMax difference
            for (int i = 0; i <= 10417; i++) {
                foreach (Point item in points) {
                    item.Advance();
                }
                int minX = points.Min(a => a.x);
                int minY = points.Min(a => a.y);
                int maxY = points.Max(a => a.y);
                int maxX = points.Max(a => a.x);
                //Console.WriteLine(i + ":" + (maxX - minX).ToString() + "," + (maxY - minY).ToString());

            }
            PrintPoints(points);

            return "";
        }

        public static void PrintPoints(HashSet<Point> points) {
            int minX = points.Min(a => a.x);
            int minY = points.Min(a => a.y);
            int maxY = points.Max(a => a.y);
            int maxX = points.Max(a => a.x);

            for (var i = minY; i <= maxY; i++) {
                for (var j = minX; j <= maxX; j++) {
                    Console.Write(points.Any(a => a.y == i && a.x == j) ? '#' : '.');
                }
                Console.WriteLine();
            }

        }
    }
}
