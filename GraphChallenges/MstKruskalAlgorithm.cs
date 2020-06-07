using System;
using System.Collections.Generic;

namespace CodingChallenges.GraphChallenges
{
    internal partial class MstKruskalAlgorithm
    {
        /*
         * https://www.hackerrank.com/challenges/kruskalmstrsub/problem
         */

        public class Edge : IComparable<Edge>
        {
            public int From { get; private set; }
            public int To { get; private set; }
            public int Weight { get; set; }

            public Edge(int from, int to, int weight)
            {
                From = from;
                To = to;
                Weight = weight;
            }

            public int CompareTo(Edge other)
            {
                return Weight.CompareTo(other.Weight);
            }

            public override string ToString()
            {
                return $"{ From }<--[{Weight}]-->{To}";
            }
        }

        private static Dictionary<int, HashSet<int>> GetClonedGraph(Dictionary<int, HashSet<int>> graph, Edge edgeToAdd)
        {
            var clonedGraph = new Dictionary<int, HashSet<int>>();
            foreach (var kvp in graph)
            {
                if (!clonedGraph.ContainsKey(kvp.Key))
                {
                    clonedGraph.Add(kvp.Key, new HashSet<int>());
                }

                foreach (var node in kvp.Value)
                {
                    clonedGraph[kvp.Key].Add(node);

                    if (!clonedGraph.ContainsKey(node))
                    {
                        clonedGraph.Add(node, new HashSet<int>());
                    }

                    clonedGraph[node].Add(kvp.Key);
                }
            }

            if (!clonedGraph.ContainsKey(edgeToAdd.From))
            {
                clonedGraph.Add(edgeToAdd.From, new HashSet<int>());
            }

            clonedGraph[edgeToAdd.From].Add(edgeToAdd.To);

            if (!clonedGraph.ContainsKey(edgeToAdd.To))
            {
                clonedGraph.Add(edgeToAdd.To, new HashSet<int>());
            }

            clonedGraph[edgeToAdd.To].Add(edgeToAdd.From);

            return clonedGraph;
        }

        private static bool CanCreateCycle(Dictionary<int, HashSet<int>> graph, Edge edgeToAdd)
        {
            var clonedGraph = GetClonedGraph(graph, edgeToAdd);
            return CycleDetectorUndirectedGraph.HasCycleUsingBfs(clonedGraph);
        }

        public static int FindMinimumWeight(int nodes, IList<Edge> edges)
        {
            var graph = new Dictionary<int, Dictionary<int, int>>();
            foreach (var item in edges)
            {
                if (!graph.ContainsKey(item.From))
                {
                    graph.Add(item.From, new Dictionary<int, int>());
                }

                if (graph[item.From].ContainsKey(item.To))
                {
                    if (graph[item.From][item.To] > item.Weight)
                    {
                        graph[item.From][item.To] = item.Weight;
                    }
                }
                else
                {
                    graph[item.From].Add(item.To, item.Weight);
                }
            }

            var heap = new Heap<Edge>(edges.Count, true);
            foreach (var parentNode in graph.Keys)
            {
                foreach (var neighbor in graph[parentNode])
                {
                    heap.Insert(new Edge(parentNode, neighbor.Key, neighbor.Value));
                }
            }

            var mst = new Dictionary<int, HashSet<int>>();
            var sum = 0;
            var edgesCount = 0;
            while (edgesCount < nodes - 1)
            {
                if (heap.IsEmpty)
                {
                    break;
                }

                var item = heap.Delete();
                if (!CanCreateCycle(mst, item))
                {
                    if (!mst.ContainsKey(item.From))
                    {
                        mst.Add(item.From, new HashSet<int>());
                    }

                    mst[item.From].Add(item.To);

                    edgesCount++;
                    sum += item.Weight;
                }
            }

            return sum;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Kruskal Algorithm Sample***");
            RunSample1();
            RunSample2();
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }

        private static void RunSample1()
        {
            /*
             *  1 2 1
                3 2 150
                4 3 99
                1 4 100
                3 1 200
             */
            // Expected: 200

            var list = new List<Edge>
            {
                new Edge(1, 2, 1),
                new Edge(3, 2, 150),
                new Edge(4, 3, 99),
                new Edge(1, 4, 100),
                new Edge(3, 1, 200)
            };

            Console.WriteLine($"Edges are {string.Join(", ", list)}");
            var sum = FindMinimumWeight(4, list);
            Console.WriteLine($"Minimum sum = {sum}");
        }

        private static void RunSample2()
        {
            /*
             * 2 1 1000
                3 4 299
                2 4 200
                2 4 100
                3 2 300
                3 2 6
             */
            // Expected 1106

            var list = new List<Edge>
            {
                new Edge(2, 1, 1000),
                new Edge(3, 4, 299),
                new Edge(2, 4, 200),
                new Edge(2, 4, 100),
                new Edge(3, 2, 300),
                new Edge(3, 2, 6)
            };

            Console.WriteLine($"Edges are {string.Join(", ", list)}");
            var sum = FindMinimumWeight(4, list);
            Console.WriteLine($"Minimum sum = {sum}");
        }
    }
}
