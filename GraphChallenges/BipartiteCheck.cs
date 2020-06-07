using System;
using System.Collections.Generic;

namespace CodingChallenges.GraphChallenges
{
    internal class BipartiteCheck
    {
        enum Color
        {
            NotSet,
            Red,
            Blue
        }

        public static bool IsBipartite(int[][] edges)
        {
            var m = edges.Length;
            if (m == 0)
            {
                return false;
            }


            var dict = new Dictionary<int, HashSet<int>>();
            var colors = new Dictionary<int, Color>();
            /*
             * https://leetcode.com/problems/is-graph-bipartite/
            for (var row = 0; row < m; row++)
            {
                var firstNode = row;
                if (!dict.ContainsKey(firstNode)) {
                    dict.Add(firstNode, new HashSet<int>());    
                }
                
                var n = graph[row].Length;
                for (var col = 0; col < n; col++)
                {
                    var secondNode = graph[row][col];
                    dict[firstNode].Add(secondNode);
                    
                     if (!dict.ContainsKey(secondNode)) {
                        dict.Add(secondNode, new HashSet<int>());    
                        dict[secondNode].Add(firstNode);
                    }
                }
            }*/

            /* https://leetcode.com/problems/possible-bipartition/  */
            for (var row = 0; row < m; row++)
            {
                var a = edges[row][0];
                var b = edges[row][1];
                if (!dict.ContainsKey(a))
                {
                    dict.Add(a, new HashSet<int>());
                }
                if (!dict.ContainsKey(b))
                {
                    dict.Add(b, new HashSet<int>());
                }

                dict[a].Add(b);
                dict[b].Add(a);
            }

            foreach (var startNode in dict.Keys)
            {
                var stack = new Stack<Tuple<int, Color>>();
                stack.Push(Tuple.Create(startNode, Color.NotSet));
                if (colors.ContainsKey(startNode))
                {
                    continue;
                }

                while (stack.Count != 0)
                {
                    var item = stack.Pop();
                    var node = item.Item1;
                    var color = item.Item2;
                    if (color == Color.NotSet)
                    {
                        if (dict.ContainsKey(node))
                        {
                            foreach (var node2 in dict[node])
                            {
                                if (colors.ContainsKey(node2))
                                {
                                    color = colors[node2] == Color.Red ? Color.Blue : Color.Red;
                                }
                            }
                        }

                        if (color == Color.NotSet)
                        {
                            color = Color.Red;
                        }

                        if (colors.ContainsKey(node))
                        {
                            colors[node] = color;
                        }
                        else
                        {
                            colors.Add(node, color);
                        }
                    }

                    var colorsContainsNodeKey = colors.ContainsKey(node);
                    if (colorsContainsNodeKey && colors[node] != color)
                    {
                        return false;
                    }
                    else
                    {
                        if (!colorsContainsNodeKey)
                        {
                            colors.Add(node, color);
                        }

                        if (dict.ContainsKey(node))
                        {
                            var nextColor = color == Color.Red ? Color.Blue : Color.Red;
                            foreach (var node2 in dict[node])
                            {
                                if (colors.ContainsKey(node2))
                                {
                                    if (colors[node2] != nextColor)
                                    {
                                        return false;
                                    }

                                }
                                else
                                {
                                    stack.Push(Tuple.Create(node2, nextColor));
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Bipartite Check ***");
            RunSample4();
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }

        private static void RunSample1()
        {
            /* https://leetcode.com/problems/is-graph-bipartite/ */
            var graph = new int[][]
            {
                new int[] { 1, 2, 3 },
                new int[] { 0, 2 },
                new int[] { 0, 1, 3 },
                new int[] { 0, 2 }
            };

            Console.WriteLine(IsBipartite(graph));
        }

        private static void RunSample2()
        {
            /* https://leetcode.com/problems/possible-bipartition/  */
            var graph = new int[][]
                        {
                            new int[] { 1, 2 },
                            new int[] { 1, 3 },
                            new int[] { 2, 3 }
                        };

            Console.WriteLine(IsBipartite(graph));
        }

        private static void RunSample3()
        {
            /* https://leetcode.com/problems/possible-bipartition/ */
            var graph = new int[][]
                                    {
                                        new int[] { 1, 2 },
                                        new int[] { 1, 3 },
                                        new int[] { 2, 4 }
                                    };

            Console.WriteLine(IsBipartite(graph));
        }

        private static void RunSample4()
        {
            /* https://leetcode.com/problems/possible-bipartition/ 
             *  [[5,9],[5,10],[5,6],[5,7],[1,5],[4,5],[2,5],[5,8],[3,5]]
             *  Expected: true
             */
            var edges = new int[][]
                                   {
                                        new int[] { 5, 9 },
                                        new int[] { 5, 10 },
                                        new int[] { 5, 6 },
                                        new int[] { 5, 7 },
                                        new int[] { 1, 5 },
                                        new int[] { 4, 5 },
                                        new int[] { 2, 5 },
                                        new int[] { 5, 8 },
                                        new int[] { 3, 5 },
                                   };
            foreach(var edge in edges)
            {
                Console.WriteLine(string.Join(", ", edge));
            }

            Console.WriteLine($"Is Possible Biparition: {IsBipartite(edges)}");
        }    
    }
}
