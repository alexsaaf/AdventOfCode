using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day13 {
        
        class Cart {
            public int x, y;
            string direction;
            int turnIndex;
            char covering;

            public Cart(int x, int y, string direction) {
                this.x = x;
                this.y = y;
                this.direction = direction;
                if (direction == "north" || direction == "south") {
                    covering = '|';
                } else {
                    covering = '-';
                }
                this.turnIndex = 0;
            }

            public void Remove(ref char[,] map) {
                if (covering == '^' || covering == 'v' || covering == '>' || covering == '<') {
                    return;
                } else {
                    map[y, x] = covering;
                }
            }

            public bool Step(ref char[,] map) {
                int newX = 0, newY = 0;
                if (direction == "east") {
                    newX = x + 1;
                    newY = y;
                } else if (direction == "west") {
                    newX = x - 1;
                    newY = y;
                } else if (direction == "north") {
                    newY = y - 1;
                    newX = x;
                } else if (direction == "south") {
                    newY = y + 1;
                    newX = x;
                }
                map[y,x] = covering;

                covering = map[newY, newX];
                x = newX;
                y = newY;

                if (covering == '^' || covering == 'v' || covering == '>' || covering == '<') {
                    return true;
                }

                if (covering == '/') {
                    switch (direction) {
                        case "north":
                            direction = "east";
                            break;
                        case "south":
                            direction = "west";
                            break;
                        case "west":
                            direction = "south";
                            break;
                        case "east":
                            direction = "north";
                            break;
                    }
                } else if (covering == '\\') {
                    switch (direction) {
                        case "north":
                            direction = "west";
                            break;
                        case "south":
                            direction = "east";
                            break;
                        case "west":
                            direction = "north";
                            break;
                        case "east":
                            direction = "south";
                            break;
                    }
                } else if (covering == '+') {
                    if (turnIndex == 0) {
                        turnIndex++;
                        switch (direction) {
                            case "north":
                                direction = "west";
                                break;
                            case "south":
                                direction = "east";
                                break;
                            case "west":
                                direction = "south";
                                break;
                            case "east":
                                direction = "north";
                                break;
                        }
                    } else if (turnIndex == 1) {
                        turnIndex++;
                        // Move straight
                    } else if (turnIndex == 2) {
                        turnIndex = 0;
                        switch (direction) {
                            case "north":
                                direction = "east";
                                break;
                            case "south":
                                direction = "west";
                                break;
                            case "west":
                                direction = "north";
                                break;
                            case "east":
                                direction = "south";
                                break;
                        }
                    }
                }
                switch (direction) {
                    case "north":
                        map[newY, newX] = '^';
                        break;
                    case "south":
                        map[newY, newX] = 'v';
                        break;
                    case "west":
                        map[newY, newX] = '<';
                        break;
                    case "east":
                        map[newY, newX] = '>';
                        break;
                }
                return false;
            }
        }

        public static string TaskA(string[] input) {
            char[,] map = new char[input.Length, input[0].Length];


            HashSet<Cart> carts = new HashSet<Cart>();

            for (int y = 0; y < input.Length; y++) {
                for (int x = 0; x < input[y].Length; x++) {
                    map[y, x] = input[y][x];
                    if (input[y][x] == '>') {
                        Cart cart = new Cart(x, y, "east");
                        carts.Add(cart);
                    } else if (input[y][x] == '^') {
                        Cart cart = new Cart(x, y, "north");
                        carts.Add(cart);
                    } else if (input[y][x] == 'v') {
                        Cart cart = new Cart(x, y, "south");
                        carts.Add(cart);
                    } else if (input[y][x] == '<') {
                        Cart cart = new Cart(x, y, "west");
                        carts.Add(cart);
                    }
                }
            }

            // Make step
            for (int i = 0; i < 50000; i++) {
                //Console.WriteLine("Generation " + i);
                List<Cart> cartList = carts.ToList().OrderBy(a => a.y).ThenBy(a => a.x).ToList();
                foreach (Cart cart in cartList) {
                    bool crash = cart.Step(ref map);
                    if (crash) {
                        Console.WriteLine("Crash at: " + cart.x + ":" + cart.y);
                        return "";
                    }
                }
            }
            return "";
        }

        public static string TaskB(string[] input) {
            char[,] map = new char[input.Length, input[0].Length];


            HashSet<Cart> carts = new HashSet<Cart>();

            for (int y = 0; y < input.Length; y++) {
                for (int x = 0; x < input[y].Length; x++) {
                    map[y, x] = input[y][x];
                    if (input[y][x] == '>') {
                        Cart cart = new Cart(x, y, "east");
                        carts.Add(cart);
                    } else if (input[y][x] == '^') {
                        Cart cart = new Cart(x, y, "north");
                        carts.Add(cart);
                    } else if (input[y][x] == 'v') {
                        Cart cart = new Cart(x, y, "south");
                        carts.Add(cart);
                    } else if (input[y][x] == '<') {
                        Cart cart = new Cart(x, y, "west");
                        carts.Add(cart);
                    }
                }
            }

            // Make step
            for (int i = 0; i < 50000; i++) {
                //Console.WriteLine("Generation " + i);
                List<Cart> cartList = carts.ToList().OrderBy(a => a.y).ThenBy(a => a.x).ToList();
                foreach (Cart cart in cartList) {
                    if (!carts.Contains(cart)) {
                        continue;
                    }
                    bool crash = cart.Step(ref map);
                    if (crash) {
                        // remove carts at position
                        foreach (Cart removing in carts.Where(a => a.x == cart.x && a.y == cart.y)) {
                            removing.Remove(ref map);
                        }
                        carts.RemoveWhere(a => a.x == cart.x && a.y == cart.y);
                    }
                }
                if (carts.Count == 1) {
                    Cart cart = carts.First();
                    Console.WriteLine("Cart is at: " + cart.x + ":" + cart.y);
                    return "";
                }
            }
            return "";
        }
    }
}
