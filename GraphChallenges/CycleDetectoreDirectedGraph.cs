using System;
using System.Collections.Generic;

namespace CodingChallenges.GraphChallenges
{
    internal class CycleDetectoreDirectedGraph
    {
        public static bool CanFinish(int numCourses, int[][] prerequisites)
        {
            /* https://leetcode.com/problems/course-schedule/ */

            var graph = new Dictionary<int, HashSet<int>>();
            for (var n = 0; n < numCourses; n++)
            {
                graph.Add(n, new HashSet<int>());
            }

            foreach (var coursePair in prerequisites)
            {
                var course = coursePair[0];
                var prereq = coursePair[1];
                graph[course].Add(prereq);
            }

            return !HasCycle(graph);
        }

        public static bool HasCycle(Dictionary<int, HashSet<int>> graph)
        {
            var visited = new HashSet<int>();
            var intermediate = new HashSet<int>();
            foreach (var vertex in graph.Keys)
            {
                if (HasCycleUsingDfs(graph, vertex, visited, intermediate))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasCycleUsingDfs(Dictionary<int, HashSet<int>> graph, int vertex, HashSet<int> visited, HashSet<int> intermediate)
        {
            intermediate.Add(vertex);
            foreach (var neighbor in graph[vertex])
            {
                if (visited.Contains(neighbor))
                {
                    continue;
                }

                if (intermediate.Contains(neighbor))
                {
                    return true;
                }

                if (HasCycleUsingDfs(graph, neighbor, visited, intermediate))
                {
                    return true;
                }
            }

            intermediate.Remove(vertex);
            return false;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Cyclic Directed Graph Detector ***");
            RunSample1();
            RunSample2();
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }       

        private static void RunSample1()
        {
            /*
             *  3
                [[0,1],[0,2],[1,2]]   
                Expected: true
             */

            var courses = new[] { new[] { 0, 1 }, new[] { 0, 2 }, new[] { 1, 2 } };
            var canFinish = CanFinish(3, courses);
            foreach(var coursePair in courses)
            {
                Console.WriteLine($"Course Prereq Pair: {string.Join(", ", coursePair)}");
            }

            Console.WriteLine($"Course can be finished: {canFinish}");
        }

        private static void RunSample2()
        {
            /*
             * 4
                [[0,1],[3,1],[1,3],[3,2]]
                Expected: false
             */

            var courses = new[] { new[] { 0, 1 }, new[] { 3, 1 }, new[] { 1, 3 }, new[] { 3, 2 } };
            var canFinish = CanFinish(4, courses);
            foreach (var coursePair in courses)
            {
                Console.WriteLine($"Course Prereq Pair: {string.Join(", ", coursePair)}");
            }

            Console.WriteLine($"Course can be finished: {canFinish}");
        }      
    }
}
