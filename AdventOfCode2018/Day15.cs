using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day15 {

        static int height, width;

        public class Unit {
            public char team;
            public int x, y;
            public int hitPoints;
            public bool dead;

            public Unit(char team, int x, int y, int hitPoints) {
                this.team = team;
                this.x = x;
                this.y = y;
                this.hitPoints = hitPoints;
                this.dead = false;
            }

            public List<KeyValuePair<(int,int),(int,int,(int,int))>> GetEnemyList(char[,] map) {

                HashSet<(int, int)> visited = new HashSet<(int, int)>();

                Dictionary<(int, int), (int, int)> distances = new Dictionary<(int, int), (int, int)>();
                Dictionary<(int, int), (int, int, (int,int))> enemyDistances = new Dictionary<(int, int), (int, int, (int,int))>();
                Queue<(int, int)> frontier = new Queue<(int, int)>();

                frontier.Enqueue((y, x));

               // Console.WriteLine("I am at: " + y + ":" + x);

                while (frontier.Count != 0) {
                    //Console.WriteLine(frontier.Count);
                    // Take the first position of the queue
                    var pos = frontier.Dequeue();
                    visited.Add(pos);

                    // Check if neighbors are valid
                    for (int i = -1; i < 2; i++) {
                        for (int j = -1; j < 2; j++) {
                            if (i == 0 && j == 0) {
                                continue;
                            } else if (j != 0 && i != 0) {
                                continue;
                            }
                            
                            if (pos.Item1 + j < 0 || pos.Item1 + j >= height|| pos.Item2 + i < 0 || pos.Item2 + i >= width) {
                                continue;
                            }
                            (int, int) newPos = pos;
                            newPos.Item1 += j;
                            newPos.Item2 += i;
                            //Console.WriteLine("Pos is now: " + pos);
                            //Console.WriteLine("Going to: " + newPos);
                            if (map[pos.Item1 + j, pos.Item2 + i] == '#') {
                                // Wall, dont add
                            } else if (map[pos.Item1 + j, pos.Item2 + i] == '.') {
                                // Empty, add
                                if (!frontier.Contains((pos.Item1 + j, pos.Item2 + i)) &&
                                    !visited.Contains((pos.Item1 + j, pos.Item2 + i))) {
                                    frontier.Enqueue((pos.Item1 + j, pos.Item2 + i));
                                }
                                if (distances.ContainsKey(pos) && !distances.ContainsKey((pos.Item1 + j, pos.Item2 + i))) {
                                    distances.Add((pos.Item1 + j, pos.Item2 + i), (distances[pos].Item1 + 1, distances[pos].Item2));
                                } else if (distances.ContainsKey(pos)) {
                                    if (distances[(pos.Item1 + j, pos.Item2 + i)].Item1 == distances[pos].Item1 + 1
                                        && distances[(pos.Item1 + j, pos.Item2 + i)].Item2 > distances[pos].Item2) {
                                        // Overwrite
                                        distances[(pos.Item1 + j, pos.Item2 + i)] = (distances[pos].Item1 + 1, distances[pos].Item2);
                                    } 


                                } else {
                                    int dir = 0;
                                    if (j == -1) {
                                        dir = 0;
                                    } else if (i == -1) {
                                        dir = 1;
                                    } else if (j == 1) {
                                        dir = 3;
                                    } else if (i == 1) {
                                        dir = 2;
                                    }
                                    if (!distances.ContainsKey((pos.Item1 + j, pos.Item2 + i))) {
                                        distances.Add((pos.Item1 + j, pos.Item2 + i), (1, dir));
                                    }
                                }
                            } else if (map[pos.Item1 + j, pos.Item2 + i] == (team == 'E' ? 'G' : 'E')) {
                                if (distances.ContainsKey(pos) && !enemyDistances.ContainsKey((pos.Item1, pos.Item2))) {
                                    enemyDistances.Add((pos.Item1, pos.Item2), (distances[pos].Item1, distances[pos].Item2, (pos.Item1 + j, pos.Item2 + i)));
                                } else if (distances.ContainsKey(pos)) {
                                    if (enemyDistances[(pos.Item1, pos.Item2)].Item1 == distances[pos].Item1
                                        && enemyDistances[(pos.Item1, pos.Item2)].Item2 > distances[pos].Item2) {
                                        // Overwrite
                                        enemyDistances[(pos.Item1, pos.Item2)] = (distances[pos].Item1, distances[pos].Item2, (pos.Item1 + j, pos.Item2 + i));
                                    }


                                } else {
                                    int dir = 0;
                                    if (j == -1) {
                                        dir = 0;
                                    } else if (i == -1) {
                                        dir = 1;
                                    } else if (i == 1) {
                                        dir = 2;
                                    } else if (j == 1) {
                                        dir = 3;
                                    }
                                    if (!enemyDistances.ContainsKey((pos.Item1, pos.Item2))) {
                                        enemyDistances.Add((pos.Item1, pos.Item2), (1, dir, (pos.Item1 + j, pos.Item2 + i)));

                                    }
                                }
                                // Found enemy
                            }
                        }
                    }
                }
               // Console.WriteLine("Enemies found: " + enemyDistances.Count);
                // Order enemies
                var enemyList = enemyDistances.ToList().OrderBy(a => a.Value.Item1).ThenBy(a => a.Value.Item2).ToList();
                return enemyList;
            }

            public bool MakeMove(ref char[,] map, ref List<Unit> units) {
                //Console.WriteLine("I am at: " + y + ":" + x);
                // Search for paths to enemies
                var enemyList = GetEnemyList(map);
               // Console.WriteLine(enemyList.Count);
                if (enemyList.Count == 0) {
                    return false;
                }
                //Console.WriteLine("Found " + enemyList.Count + " enemies Closest is at range: " + enemyList[0].Value.Item1);
                // Move 
                if (enemyList[0].Value.Item1 == 0) {
                    // We are already in attacking range
                   // Console.WriteLine("I am in attack range!");
                } else {
                    map[y, x] = '.';
                    enemyList.OrderBy(a => a.Value.Item1).ThenBy(a => a.Key.Item1).ThenBy(a => a.Key.Item2).ThenBy(a => a.Value.Item2);
                    switch (enemyList[0].Value.Item2) {
                        case 0:
                            y--;
                            break;
                        case 1:
                            x--;
                            break;
                        case 2:
                            x++;
                            break;
                        case 3:
                            y++;
                            break;
                    }
                    //Console.WriteLine("Moving towards enemy " + enemyList[0].Value.Item3);
                    //Console.WriteLine("New position: " + y + ":" + x);
                    map[y, x] = team;
                }

                // Check enemies in range
                // Need to search again?
                enemyList = GetEnemyList(map);

                // Attack the one with lowest hit points
                var enemyPositions = enemyList.Where(a => a.Value.Item1 == 0).ToList();
                if (enemyPositions.Count >= 1) {
                    List<(Unit,int)> enemies = new List<(Unit,int)>();
                    foreach (var enemyPosition in enemyPositions) {
                        enemies.Add((units.Find(e => e.x == enemyPosition.Value.Item3.Item2 && e.y == enemyPosition.Value.Item3.Item1),enemyPosition.Value.Item2));
                    }
                    enemies = enemies.Where(a => !a.Item1.dead).OrderBy(a => a.Item1.hitPoints).ThenBy(a => a.Item2).ToList();
                    if (enemies.Count > 0) {
                        enemies[0].Item1.hitPoints -= 3;
                        if (enemies[0].Item1.hitPoints <= 0) {
                            // Console.WriteLine(enemies[0].Item1.team + " dying");
                            enemies[0].Item1.dead = true;
                            map[enemies[0].Item1.y, enemies[0].Item1.x] = '.';
                        }
                    }
                }
                return true;
            }
        }

        static void PrintBoard(char[,] map) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    sb.Append(map[i, j]);
                }
                sb.Append("\n");
            }
            Console.WriteLine(sb.ToString());
        }

        public static string TaskA(string[] input) {
            // Create
            height = input.Length;
            width = input[0].Length;
            char[,] map = new char[input.Length, input[0].Length];

            List<Unit> units = new List<Unit>();

            for (int y = 0; y < input.Length; y++) {
                for (int x = 0; x < input.Length; x++) {
                    map[y, x] = input[y][x];
                    if (input[y][x] == 'E') {
                        // Create elf
                        units.Add(new Unit('E', x, y, 200));
                    } else if (input[y][x] == 'G') {
                        // Create goblin
                        units.Add(new Unit('G', x, y, 200));
                    }
                }
            }

            int turn = 0;
            PrintBoard(map);
            // Let them start taking turns
            while (true) {
                //Console.WriteLine("Turn: " + turn);

                turn++;
                List<Unit> ordered = units.OrderBy(a => a.y).ThenBy(a => a.x).ToList();
                int unitCount = ordered.Count;
                int takenTurn = 0;
                foreach (Unit unit in ordered) {
                    takenTurn++;
                    if (unit.dead)
                        continue;
                    bool hasEnemy = unit.MakeMove(ref map, ref units);
                    char enemyTeam = (unit.team =='E') ? 'G' : 'E';
                    bool _gameOver = units.Count(a => a.team == 'E' && !a.dead) == 0 || units.Count(a => a.team == 'G' && !a.dead) == 0;
                    if (_gameOver) {
                        // We are done
                        //Console.WriteLine("Turn: " + turn);
                        //PrintBoard(map);
                        int healthPoints = units.Where(a => a.hitPoints > 0).Sum(a => a.hitPoints);
                        if (takenTurn != unitCount) {
                            turn -= 1;
                        }
                        Console.WriteLine(takenTurn + ":" + unitCount);
                        Console.WriteLine(turn);
                        Console.WriteLine(healthPoints);
                        Console.WriteLine((healthPoints * (turn)).ToString());
                        return "";
                    }
                }
                foreach (var unit in units) {
                    if (unit.dead) {
                        map[unit.y, unit.x] = '.';
                    }
                }
                units.RemoveAll(s => s.dead);
                bool gameOver = units.Count(a => a.team == 'E') == 0 || units.Count(a => a.team == 'G') == 0;
                if (gameOver) {
                    // We are done
                    //Console.WriteLine("Turn: " + turn);
                    //PrintBoard(map);
                    int healthPoints = units.Sum(a => a.hitPoints);
                    Console.WriteLine(turn);
                    Console.WriteLine(healthPoints);
                    Console.WriteLine((healthPoints * (turn)).ToString());
                    return "";
                }
                PrintBoard(map);
            }
            return "";
        }
    }
}
