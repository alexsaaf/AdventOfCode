using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdventOfCode2018 {
    class Day6 {

        public string TaskA(string[] input) {
            // Convert to tuples
            var coords = input.Select(s => s.Split(new[] { ", " }, StringSplitOptions.None))
                        .Select(s => s.Select(i => Convert.ToInt32(i)).ToArray())
                        .Select(s => (x: s[0], y: s[1]))
                        .ToArray();

            // Find minmax
            int maxX = coords.Max(a => a.x);
            int maxY = coords.Max(a => a.y);
            int[,] map = new int[maxX + 2, maxY + 2];

            for (int x = 0; x <= maxX + 1; x++) {
                for (int y = 0; y <= maxY + 1; y++) {
                    // Calculate distances from all the coords
                    var distances = coords
                        .Select((c, i) => (i: i, distance: GetManhattan(c.x, c.y, x, y)))
                        .OrderBy(c => c.distance)
                        .ToArray();
                    // If they are equal, we have a draw for this tile
                    if (distances[1].distance != distances[0].distance) {
                        map[x, y] = distances[0].i;
                    } else {
                        map[x, y] = -1;
                    }
                }
            }

            var excluded = new List<int>();
            var counts = Enumerable.Range(-1, coords.Length + 1).ToDictionary(i => i, _ => 0);

            // Count number of tiles with each coord as closest
            for (int x = 0; x <= maxX + 1; x++)
                for (int y = 0; y <= maxY + 1; y++) {
                    if (x == 0 || y == 0 ||
                        x == maxX + 1 || y == maxY + 1) {
                        // Edge ones are infinite, remember which ones
                        excluded.Add(map[x, y]);
                    }
                    counts[map[x, y]] += 1;
                }

            excluded = excluded.Distinct().ToList();
            // Find the largest count
            int res = counts
                .Where(kvp => !excluded.Contains(kvp.Key))
                .OrderByDescending(kvp => kvp.Value).ToArray()[0].Value;

            return res.ToString();
        }

        public string TaskB(string[] input) {
            // Convert to tuples
            var coords = input.Select(s => s.Split(new[] { ", " }, StringSplitOptions.None))
                        .Select(s => s.Select(i => Convert.ToInt32(i)).ToArray())
                        .Select(s => (x: s[0], y: s[1]))
                        .ToArray();
            // Find minmax
            int maxX = coords.Max(a => a.x);
            int maxY = coords.Max(a => a.y);

            // Keep track of found safe ones
            long safeTiles = 0;

            for (int x = -100; x <= maxX + 100; x++) {
                for (int y = -100; y <= maxY + 100; y++) {
                    var distances = coords
                        .Select((c, i) => (i: i, distance: Math.Abs(c.x - x) + Math.Abs(c.y - y)))
                        .OrderBy(c => c.distance)
                        .ToArray();

                    // Simply check the total distance
                    int totalDistance = distances.Sum(a => a.distance);
                    if (totalDistance < 10000) {
                        // This tile is safe
                        safeTiles++;
                    }
                }
            }

            return safeTiles.ToString();
        }


        int GetManhattan(int x1, int y1, int x2, int y2) {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

    }
}
