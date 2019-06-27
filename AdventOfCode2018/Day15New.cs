using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {

    public class Unit {
        public (int, int) position;
        public int hitPoints;
        public int attackPower;

        public Unit(int x, int y, int attackpower) {
            this.position = (x, y);
            this.attackPower = attackpower;
            hitPoints = 200;
        }
    }

    class Day15New {

        char[,] map;
        List<Unit> elves;
        List<Unit> goblins;
        int rounds;

        int height, width;

        private bool CombatOver() {
            return elves.Count == 0 || goblins.Count == 0;
        }

        public string TaskA(string[] input) {
            map = new char[input[0].Length, input.Length];
            height = input.Length;
            width = input[0].Length;
            int attackPower = 3;
            rounds = 0;

            elves = new List<Unit>();
            goblins = new List<Unit>();

            for (int y = 0; y < input.Length; y++) {
                for (int x = 0; x < input.Length; x++) {
                    if (input[y][x] == '#') {
                        map[y, x] = '#';
                    } else {
                        map[y, x] = '.';
                    }
                    if (input[y][x] == 'E') {
                        // Create elf
                        elves.Add(new Unit(x, y, attackPower));
                    } else if (input[y][x] == 'G') {
                        // Create goblin
                        goblins.Add(new Unit(x, y, 3));
                    }
                }
            }

            while(!CombatOver()) {
                Console.WriteLine("Turn: " + rounds);
                Console.WriteLine("Elves: " + elves.Count.ToString());
                Console.WriteLine("Goblins: " + goblins.Count.ToString());
                //PrintMap();
                Simulate();
            }


            return GetResult().ToString();
        }

        void PrintMap() {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    sb.Append(map[i, j]);
                }
                sb.Append("\n");
            }
            Console.WriteLine(sb.ToString());
        }

        private int GetResult() {
            var totalHp = goblins.Sum(g => g.hitPoints);
            goblins.ForEach(a => Console.WriteLine(a.hitPoints));
            Console.WriteLine("TotalHp: " + totalHp);
            Console.WriteLine("Rounds: " + rounds);
            return rounds * totalHp;
        }

        public void Simulate() {
            var units = elves.Concat(goblins).OrderBy(a => a.position.Item2).ThenBy(a => a.position.Item1);

            foreach (Unit unit in units) {
                // Check if over
                if (CombatOver()) {
                    return;
                }

                // Check if dead
                if (!elves.Contains(unit) && !goblins.Contains(unit)) {
                    continue;
                } 

                // Try to attack
                if (Attack(unit)) {
                    continue;
                }

                // Move, then try attack again
                Move(unit);
                Attack(unit);
            }
            rounds++;
        }

        void Move(Unit unit) {
            // Get list of enemies
            var enemies = new List<Unit>();
            if (elves.Contains(unit)) {
                enemies = enemies.Concat(goblins).ToList();
            } 
            if (goblins.Contains(unit)) {
                enemies = enemies.Concat(elves).ToList();
            }

            //Console.WriteLine("Starting dijsktra");
            var dijkstra = new Dijkstra(map, elves, goblins, unit.position, height, width);
            //Console.WriteLine("Done with dijkstra");

            var destinations = enemies.Aggregate(new List<(int,int)>(), (acc,a) => (List<(int,int)>)acc.Concat(GetReachableAdjacentPoints(a.position)).ToList()).ToList();

            if (destinations.Count == 0) {
                return;
            }

            int minDist = destinations.Min(a => dijkstra.GetDistance(a));
            var possibleDests = destinations.Where(a => dijkstra.GetDistance(a) == minDist);
            // Tie break
            var destination = possibleDests.OrderBy(a => a.Item2).ThenBy(a => a.Item2).First();
            
            if (dijkstra.Unreachable(destination)) {
                return;
            }

            var shortestPath = dijkstra.GetShortestPath(destination);

            shortestPath.RemoveFirst();
            Console.WriteLine("I am at " + unit.position.Item1 + ":" + unit.position.Item2 + " moving to " + shortestPath.First().Item1 + ":" + shortestPath.First().Item2);

            // Move
            unit.position = shortestPath.First();
        }

        bool Attack(Unit unit) {
            // Get list of enemies
            var enemies = new List<Unit>();
            if (elves.Contains(unit)) {
                enemies = enemies.Concat(goblins).ToList();
            }
            if (goblins.Contains(unit)) {
                enemies = enemies.Concat(elves).ToList();
            }

            // Check if any enemy is adjacent
            var adjacentEnemies = enemies.Where(enemy => GetAdjacentPoints(unit.position).Any(a => a.Item1 == enemy.position.Item1 && a.Item2 == enemy.position.Item2)).ToList();

            if (adjacentEnemies.Count == 0) {
                return false;
            }

            int lowestHitPoints = adjacentEnemies.Min(a => a.hitPoints);
            var lowest = adjacentEnemies.Where(a => a.hitPoints == lowestHitPoints);
            lowest = lowest.OrderBy(a => a.position.Item2).ThenBy(a => a.position.Item1);

            var preferred = lowest.First();

            preferred.hitPoints -= unit.attackPower;

            if (preferred.hitPoints <= 0) {
                elves.Remove(preferred);
                goblins.Remove(preferred);
            }

            return true;
        }

        List<(int, int)> GetAdjacentPoints((int, int) pos) {
            var res = new List<(int, int)>();
            res.Add((pos.Item1 - 1, pos.Item2));
            res.Add((pos.Item1 + 1, pos.Item2));
            res.Add((pos.Item1, pos.Item2 - 1));
            res.Add((pos.Item1, pos.Item2 + 1));

            // Filter points
            res.RemoveAll(a => a.Item1 < 0 || a.Item1 >= width);
            res.RemoveAll(a => a.Item2 < 0 || a.Item2 >= height);
            res.RemoveAll(a => map[a.Item1, a.Item2] == '#');

            return res;
        }

        List<(int, int)> GetReachableAdjacentPoints((int, int) pos) {
            var res = GetAdjacentPoints(pos);
            // Remove spots already occupied
            res.RemoveAll(a => elves.Any(e => e.position.Item1 == a.Item1 && e.position.Item2 == a.Item2));
            res.RemoveAll(a => goblins.Any(e => e.position.Item1 == a.Item1 && e.position.Item2 == a.Item2)); 
            return res;
        }

    }



    public class Dijkstra {
        char[,] map;
        int height, width;
        List<Unit> elves;
        List<Unit> goblins;

        Dictionary<(int, int), int> distanceTo = new Dictionary<(int, int), int>();

        Dictionary<(int, int), (int, int)> edgeTo = new Dictionary<(int, int), (int, int)>();

        public Dijkstra(char[,] map, List<Unit> elves, List<Unit> goblins, (int,int) startingPoint, int height, int width) {
            this.map = map;
            this.elves = elves;
            this.goblins = goblins;
            this.height = height;
            this.width = width;

            var visited = new HashSet<(int, int)>();
            var stack = new Stack<(int, int)>();
            stack.Push(startingPoint);
            while (stack.Count != 0) {
                var p = stack.Pop();
                if (!visited.Contains(p)) {
                    distanceTo.Add(p, int.MaxValue);
                    visited.Add(p);
                    GetReachableAdjacentEdges(p).ForEach(a => stack.Push(a));
                }
            }
            distanceTo[startingPoint] = 0;

           // Console.WriteLine("Done with stack");

            // C# has no priority queue.
            var queue = new List<(int, int)>();
            queue.AddRange(visited);

            while (queue.Count != 0) {
                //Console.WriteLine("QUeue count: " + queue.Count);
                // Sort the list (Imagine its a queue ;) )
                queue = queue.OrderBy(a => distanceTo[a]).ThenBy(a => a.Item2).ThenBy(a => a.Item1).ToList();
                var point = queue.First();
                queue.Remove(point);

                GetReachableAdjacentEdges(point).ForEach(adjacent => {
                    if (distanceTo[point] == int.MaxValue) {
                        //Console.WriteLine("Dist = maxval");
                        return;
                    }
                    var currentDist = distanceTo[adjacent];
                    var proposedDist = distanceTo[point] + 1;
                    if (proposedDist < currentDist) {
                        distanceTo[adjacent] = proposedDist;
                        edgeTo[adjacent] = point;
                        queue.Remove(adjacent);
                        queue.Add(adjacent);
                    }
                    if (proposedDist == currentDist) {
                        (int, int)[] tmp = new(int, int)[] { point, edgeTo[adjacent] };
                        tmp = tmp.OrderBy(a => a.Item2).ThenBy(a => a.Item1).ToArray();
                        edgeTo[adjacent] = tmp[0];
                    }
                });

            }

        }

        public LinkedList<(int, int)> GetShortestPath((int, int) pos) {
            var path = new LinkedList<(int, int)>();
            if (Unreachable(pos)) {
                return path;
            }
            for ((int,int)? p = pos; p != null;) {
                path.AddFirst(p.Value);
                if (edgeTo.ContainsKey(p.Value)) {
                    p = edgeTo[p.Value];
                } else {
                    p = null;
                }
            }
            return path;
        }

        public bool Unreachable((int,int) pos) {
            return !distanceTo.ContainsKey(pos);
        }

        public int GetDistance((int,int) pos) {
            if (distanceTo.ContainsKey(pos)) {
               return distanceTo[pos];
            } else {
                return int.MaxValue;
            }
        }

        List<(int,int)> GetReachableAdjacentEdges((int,int) pos) {
            var res = new List<(int, int)>();
            res.Add((pos.Item1 - 1, pos.Item2));
            res.Add((pos.Item1 + 1, pos.Item2));
            res.Add((pos.Item1, pos.Item2 - 1));
            res.Add((pos.Item1, pos.Item2 + 1));

            // Filter points
            res.RemoveAll(a => a.Item1 < 0 || a.Item1 >= width);
            res.RemoveAll(a => a.Item2 < 0 || a.Item2 >= height);
            res.RemoveAll(a => map[a.Item1, a.Item2] == '#');

            res.RemoveAll(a => elves.Any(e => e.position.Item1 == a.Item1 && e.position.Item2 == a.Item2));
            res.RemoveAll(a => goblins.Any(e => e.position.Item1 == a.Item1 && e.position.Item2 == a.Item2));
            return res;
        }
    }
}
