using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day11 {

        public static string TaskA() {
            int serial = 8199;
            int[,] fuelCells = new int[300,300];
            for (int y = 0; y < 300; y++) {
                for (int x = 0; x < 300; x++) {
                    int rackId = (x + 1) + 10;
                    int startVal = rackId * (y + 1);
                    startVal += serial;
                    int power = ((startVal * rackId) % 1000) / 100 - 5;
                    fuelCells[x, y] = power;
                }
            }

            int maxSum = int.MinValue;
            var coord = (x: 0, y: 0);
            int size = 0;

            for (int k = 1; k <= 300; k++) {
                for (int y = 0; y < 300 - k + 1; y++) {
                    for (int x = 0; x < 300 - k + 1; x++) {
                        int sum = 0;
                        for (int i = 0; i < k; i++) {
                            for (int j = 0; j < k; j++) {
                                sum += fuelCells[x + i, y + j];
                            }
                        }
                        if (sum > maxSum) {
                            maxSum = sum;
                            coord = (x + 1, y + 1);
                            size = k;
                        }

                    }
                }
            //Console.WriteLine("X: " + coord.x + " Y: " + coord.y + " size:" + size);
            }
            return "X: " + coord.x + " Y: " + coord.y + " size:" + size;
        }

    }
}
