using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019 {
    class Util {
        public static int CalculateManhattan(int x1, int y1, int x2, int y2) {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
    }

    public class Point<T> {
        public T X { get; set; }
        public T Y { get; set; }
    }


}
