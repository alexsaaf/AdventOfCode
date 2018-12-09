using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018 {
    class Day8 {

        public string TaskA(string input) {
            int i = 0;
            Node root = ConstructNode(input.Split(' ').Select(Int32.Parse).ToList(), ref i);
            return root.SumMetaData().ToString();
        }

        public string TaskB(string input) {
            int i = 0;
            Node root = ConstructNode(input.Split(' ').Select(Int32.Parse).ToList(), ref i);
            return root.Value().ToString();
        }

        public static Node ConstructNode(List<int> numbers, ref int i) {
            var node = new Node();
            var children = numbers[i++];
            var metadata = numbers[i++];
            for (int j = 0; j < children; j++) {
                node.Nodes.Add(ConstructNode(numbers, ref i));
            }

            for (int j = 0; j < metadata; j++) {
                node.Metadata.Add(numbers[i++]);
            }
            return node;
        }

    }




    public class Node {
        public List<int> Metadata { get; set; } = new List<int>();
        public List<Node> Nodes { get; set; } = new List<Node>();

        public int SumMetaData() {
            return Metadata.Sum() + Nodes.Sum(a => a.SumMetaData());
        }

        public int Value() {
            if (!Nodes.Any()) {
                return Metadata.Sum();
            }

            var value = 0;
            foreach (var m in Metadata) {
                if (m <= Nodes.Count) {
                    value += Nodes[m - 1].Value();
                }
            }

            return value;
        }
    }
}
