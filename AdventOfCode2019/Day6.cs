using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2019 {
    class Day6 {
        public static int CalculateOrbits(string[] input) {
            Dictionary<string, List<string>> orbits = new Dictionary<string, List<string>>();

            foreach (string orbit in input) {
                string[] splitOrbit = orbit.Split(")");

                if (!orbits.ContainsKey(splitOrbit[0])) {
                    orbits[splitOrbit[0]] = new List<string>();
                }
                orbits[splitOrbit[0]].Add(splitOrbit[1]);
            }
            return GetOrbits(orbits, "COM", 0);
        }

        static int GetOrbits(Dictionary<string, List<string>> map, string next, int count) {
            if (!map.ContainsKey(next) || map[next].Count == 0) {
                return count;
            } else if (map[next].Count == 1) {
                return count + GetOrbits(map, map[next][0], count + 1);
            } else {
                return count + map[next].Sum(x => GetOrbits(map, x, count + 1));
            }
        }

        public static int CalculateOrbitsBetween(string[] input) {
            Dictionary<string, string> orbits = new Dictionary<string, string>();

            foreach (string orbit in input) {
                string[] splitOrbit = orbit.Split(")");

                orbits[splitOrbit[1]] = splitOrbit[0];
            }

            var santaPath = GetPathToCenter("SAN", orbits);
            var yourPath = GetPathToCenter("YOU", orbits);
            var res = santaPath.Intersect(yourPath).First();
            return santaPath.IndexOf(res) + yourPath.IndexOf(res);
        }

        static List<string> GetPathToCenter(string planet, Dictionary<string, string> map) {
            List<string> path = new List<string>();
            var current = planet;
            while (current != "COM") {
                current = map[current];
                path.Add(current);
            }
            return path;
        }
    }
}
