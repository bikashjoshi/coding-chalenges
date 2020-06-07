using System;
using System.Collections.Generic;

namespace CodingChallenges.GraphChallenges
{
    internal class CycleDetectorUndirectedGraph
    {
        private static bool Search(Dictionary<int, HashSet<int>> graph, int vertex, HashSet<int> visited, int parent)
        {
            visited.Add(vertex);

            foreach (var neighbor in graph[vertex])
            {
                if (!visited.Contains(neighbor))
                {
                    if (Search(graph, neighbor, visited, vertex))
                    {
                        return true;
                    }
                }
                else if (parent != neighbor)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasCycleUsingDfs(Dictionary<int, HashSet<int>> graph)
        {
            var visited = new HashSet<int>();
            foreach (var vertex in graph.Keys)
            {
                if (!visited.Contains(vertex))
                {
                    if (Search(graph, vertex, visited, -1))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool HasCycleUsingBfs(Dictionary<int, HashSet<int>> graph)
        {
            var visited = new Dictionary<int, int>();
            foreach (var vertex in graph.Keys)
            {
                if (visited.ContainsKey(vertex))
                {
                    continue;
                }

                var queue = new Queue<Tuple<int, int>>();
                queue.Enqueue(Tuple.Create(vertex, -1));
                while (queue.Count != 0)
                {
                    var item = queue.Dequeue();
                    var currentNode = item.Item1;
                    var currentNodeParent = item.Item2;

                    if (!visited.ContainsKey(currentNode))
                    {
                        visited.Add(currentNode, currentNodeParent);
                        foreach (var neighbor in graph[currentNode])
                        {
                            if (!visited.ContainsKey(neighbor))
                            {
                                queue.Enqueue(Tuple.Create(neighbor, currentNode));
                            }
                        }
                    }
                    else
                    {
                        var vistedNodeParent = visited[currentNode];
                        if (vistedNodeParent != currentNodeParent)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
