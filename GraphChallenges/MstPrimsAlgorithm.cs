using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenges.GraphChallenges
{
    internal class MstPrimsAlgorithm
    {
        /* 
         * https://www.hackerrank.com/challenges/primsmstsub/problem
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

        public static int FindMinimumWeight(int n, int[][] edges, int start)
        {
            var graph = GetGraph(edges);
            var mst = new List<Edge>();
            var connected = new List<int>();

            var heap = new Heap<Edge>(2*edges.Length, true);
            connected.Add(start);
            foreach(var e in graph[start])
            {
                heap.Insert(new Edge(start, e.Key, e.Value));
            }

            while(!heap.IsEmpty)
            {
                if (mst.Count == n -1)
                {
                    break;
                }

                var edge = heap.Delete();
                if (!connected.Contains(edge.To))
                {
                    connected.Add(edge.To);
                    mst.Add(edge);
                    foreach(var to in graph[edge.To])
                    {
                        heap.Insert(new Edge(edge.To, to.Key, to.Value));
                    }
                }
            }        
                
            return mst.Sum(x => x.Weight);
        }

        private static Dictionary<int, Dictionary<int, int>> GetGraph(int[][] edges)
        {
            var graph = new Dictionary<int, Dictionary<int, int>>();

            for (var i = 0; i < edges.Length; i++)
            {
                var f = edges[i][0];
                var t = edges[i][1];
                var w = edges[i][2];
                if (!graph.ContainsKey(f))
                {
                    graph.Add(f, new Dictionary<int, int>());
                }

                if (!graph[f].ContainsKey(t))
                {
                    graph[f].Add(t, w);
                }
                else if (graph[f][t] > w)
                {
                    graph[f][t] = w;
                }

                if (!graph.ContainsKey(t))
                {
                    graph.Add(t, new Dictionary<int, int>());
                }

                if (!graph[t].ContainsKey(f))
                {
                    graph[t].Add(f, w);
                }
                else if (graph[t][f] > w)
                {
                    graph[t][f] = w;
                }
            }

            return graph;
        }

        public static void RunSample()
        {
            /*
             *  1 2 3
                1 3 4
                4 2 6
                5 2 2
                2 3 5
                3 5 7
                Expected: 15
             */

            var edges = new int[][]
            {
                new int [] { 1, 2, 3 },
                new int [] { 1, 3, 4 },
                new int [] { 4, 2, 6 },
                new int [] { 5, 2, 2 },
                new int [] { 2, 3, 5 },
                new int [] { 3, 5, 7 }
            };

            ConsoleHelper.WriteGreen("*** Prims Algorithm Sample***");
            var sum = FindMinimumWeight(5, edges, 1);
            var e = edges.Select(x => $"{ x[0] }<--[{x[2]}]-->{x[1]}");
            Console.WriteLine($"Edges are {string.Join(", ", e)}");
            Console.WriteLine($"Minimum sum = {sum}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
