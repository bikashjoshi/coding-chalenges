using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenges.GraphChallenges
{
    internal class CourseSorting
    {
        /* https://leetcode.com/problems/course-schedule-ii/
         */

        public static int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            var allCourses = new HashSet<int>();
            for (int i = 0; i < numCourses; i++)
            {
                allCourses.Add(i);
            }

            if (prerequisites.Length == 0)
            {
                return allCourses.ToArray();
            }

            var courses = new Dictionary<int, HashSet<int>>();

            foreach (var coursePair in prerequisites)
            {
                var course = coursePair[0];
                var prerequisite = coursePair[1];
                if (!courses.ContainsKey(course))
                {
                    courses.Add(course, new HashSet<int>());
                }

                courses[course].Add(prerequisite);
            }

            var toDoCourses = new Queue<int>();

            var results = new List<int>();

            foreach (var course in allCourses)
            {
                if (!courses.Keys.Contains(course))
                {
                    toDoCourses.Enqueue(course);
                    results.Add(course);
                }
            }

            if (toDoCourses.Count == 0)
            {
                return new int[] { };
            }
            
            while (toDoCourses.Count > 0)
            {
                var courseToRemove = toDoCourses.Dequeue();               

                var keysToRemove = new HashSet<int>();
                foreach (var courseKVP in courses)
                {
                    courseKVP.Value.Remove(courseToRemove);
                    if (courseKVP.Value.Count == 0)
                    {
                        toDoCourses.Enqueue(courseKVP.Key);                        
                        keysToRemove.Add(courseKVP.Key);
                    }
                }

                foreach(var key in keysToRemove)
                {
                    if (courses.ContainsKey(key))
                    {
                        courses.Remove(key);
                        results.Add(key);
                    }
                }                                             
            }

            return results.Count < allCourses.Count ? new int[] { } : results.ToArray();
        }

        public static void RunSample()
        {
            // [[1,0],[2,0],[3,1],[3,2]]
            // Expected: [0,1,2,3] or [0,2,1,3]
            ConsoleHelper.WriteGreen("*** Running Topological Sort Example *** ");
            var prereqs = new int[][] { new int[] { 1, 0 }, new int[] { 2, 0 }, new int[] { 3, 1 },  new int[] { 3, 2 } };
            foreach(var row in prereqs)
            {
                Console.WriteLine(string.Join(", ", row));
            }

            var result = FindOrder(4, prereqs);
            Console.WriteLine($"The sorted course is : { string.Join(", ", result) }");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }               
    }
}
