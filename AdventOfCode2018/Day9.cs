using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day9 {

        // Also works for Task B apparantly
        public static string TaskA(int players, int lastMarble) {
            long[] scores = new long[players];
            LinkedList<int> placed = new LinkedList<int>();
            LinkedListNode<int> current = placed.AddFirst(0);

            for (int m = 1; m < lastMarble; ++m) {
                if (((m) % 23) == 0) {
                    int playerNum = m % players;
                    // Keep this one
                    scores[playerNum] += m;

                    for (int i = 0; i < 7; i++) {
                        current = current.Previous ?? placed.Last;
                    }
                    // Take the one we are removing
                    scores[playerNum] += current.Value;

                    var tmp = current;
                    current = current.Next ?? placed.First;
                    placed.Remove(tmp);
                } else {
                    current = current.Next ?? placed.First;
                    current = placed.AddAfter(current, m);
                }
            }
            return scores.Max().ToString();
        }


    }
}
