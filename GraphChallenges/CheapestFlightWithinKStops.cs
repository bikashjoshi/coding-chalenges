using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenges.GraphChallenges
{
    internal class CheapestFlightWithinKStops
    {
        /*
         *  https://leetcode.com/problems/cheapest-flights-within-k-stops/
         */       
        private class Route : IComparable<Route>
        {
            public int City;
            public int Cost;
            public int AllowedStops;
            public List<int> Visited = new List<int>(10);
            public int CompareTo(Route other)
            {
                return Cost.CompareTo(other.Cost);
            }            
        }

        public static int FindCheapestPriceUsingBfs(int n, int[][] flights, int src, int dst, int K)
        {
            var graph = BuildGraph(flights);

            var queue = new Queue<Route>();            
            var flightPaths = new List<Route>();
            int? minCost = null;

            var start = new Route { City = src, Cost = 0, AllowedStops = K + 1 };
            start.Visited.Add(src);
            queue.Enqueue(start);
          
            while (queue.Count > 0)
            {
                var route = queue.Dequeue();
                
                var nextDestination = route.City;
                if (nextDestination == dst)
                {
                    if (route.AllowedStops >= 0)
                    {
                        flightPaths.Add(route);

                        if (minCost == null)
                        {
                            minCost = route.Cost;
                        }
                        else if (minCost > route.Cost)
                        {
                            minCost = route.Cost;
                        }
                    }                    

                    continue;
                }

                if (route.AllowedStops <= 0)
                {
                    continue;
                }

                if (!graph.ContainsKey(nextDestination))
                {
                    continue;
                }             
               
                foreach (var destination in graph[nextDestination].OrderBy(x => x.Value))
                {
                    if (!route.Visited.Contains(destination.Key) && (minCost == null || route.Cost + destination.Value < minCost.Value))
                    {
                        var next = new Route { City = destination.Key, Cost = route.Cost + destination.Value, AllowedStops = route.AllowedStops - 1 };
                        next.Visited.AddRange(route.Visited);                        
                        next.Visited.Add(next.City);
                        queue.Enqueue(next);
                    }
                }
            }
         
            foreach(var path in flightPaths)
            {
                ConsoleHelper.WriteYellow(string.Join(" => ", path.Visited) + " : " + path.Cost);
            }

            return minCost == null ? -1 : minCost.Value;
        }       

        public static int FindCheapestPriceUsingHeap(int n, int[][] flights, int src, int dst, int K)
        {
            var graph = BuildGraph(flights);

            var heap = new Heap<Route>(16500, true);
            Route minRoute = null;

            var start = new Route { City = src, Cost = 0, AllowedStops = K + 1 };
            start.Visited.Add(src);
            heap.Insert(start);

            while (!heap.IsEmpty)
            {
                var route = heap.Delete();

                var nextDestination = route.City;
                if (nextDestination == dst)
                {
                    minRoute = route;
                    break;
                }

                if (route.AllowedStops <= 0)
                {
                    continue;
                }

                if (!graph.ContainsKey(nextDestination))
                {
                    continue;
                }

                foreach (var destination in graph[nextDestination].OrderBy(x => x.Value))
                {
                    if (!route.Visited.Contains(destination.Key))
                    {
                        var next = new Route { City = destination.Key, Cost = route.Cost + destination.Value, AllowedStops = route.AllowedStops - 1 };
                        next.Visited.AddRange(route.Visited);
                        next.Visited.Add(next.City);
                        heap.Insert(next);
                    }
                }
            }

            if (minRoute != null)
            {
                ConsoleHelper.WriteYellow(string.Join(" => ", minRoute.Visited) + $" : {minRoute.Cost}[{minRoute.AllowedStops} stops remainning]");                
            }

            return minRoute == null ? -1 : minRoute.Cost;
        }

        private static Route PopMin(IList<Route> list)
        {
            var minIndex = 0;
            var minValue = int.MaxValue;
            for (var i = 0; i < list.Count; i++)
            {
                if (minValue > list[i].Cost)
                {
                    minValue = list[i].Cost;
                    minIndex = i;
                }
            }

            var route = list[minIndex];
            list.RemoveAt(minIndex);

            return route;
        }

        public static int FindCheapestPriceUsingList(int n, int[][] flights, int src, int dst, int K)
        {
            var graph = BuildGraph(flights);

            var list = new List<Route>(500);
            Route minRoute = null;

            var start = new Route { City = src, Cost = 0, AllowedStops = K + 1 };
            start.Visited.Add(src);
            list.Add(start);

            while (list.Count > 0)
            {
                var route = PopMin(list);

                var nextDestination = route.City;
                if (nextDestination == dst)
                {
                    minRoute = route;
                    break;
                }

                if (route.AllowedStops <= 0)
                {
                    continue;
                }

                if (!graph.ContainsKey(nextDestination))
                {
                    continue;
                }

                foreach (var destination in graph[nextDestination])
                {
                    if (!route.Visited.Contains(destination.Key))
                    {
                        var next = new Route { City = destination.Key, Cost = route.Cost + destination.Value, AllowedStops = route.AllowedStops - 1 };
                        next.Visited.AddRange(route.Visited);
                        next.Visited.Add(next.City);
                        list.Add(next);
                    }
                }
            }

            if (minRoute != null)
            {
                ConsoleHelper.WriteYellow(string.Join(" => ", minRoute.Visited) + $" : {minRoute.Cost}[{minRoute.AllowedStops} stops remainning]");
            }

            return minRoute == null ? -1 : minRoute.Cost;
        }

        private static Dictionary<int, Dictionary<int, int>> BuildGraph(int[][] flights)
        {
            var graph = new Dictionary<int, Dictionary<int, int>>();
            for(var i = 0; i < flights.Length; i++)
            {
                var src = flights[i][0];
                var dst = flights[i][1];
                var weight = flights[i][2];
                if (!graph.ContainsKey(src))
                {
                    graph.Add(src, new Dictionary<int, int>());                    
                }

                graph[src].Add(dst, weight);
            }

            return graph;
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Cheapest Flight with K Stops ***");
            RunSample1();
            Console.WriteLine();
            RunSample2();
            Console.WriteLine();
            RunSample3();
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }

        private static void RunSample1()
        {
            /*
             * 3
                [[0,1,100],[1,2,100],[0,2,500]]
                0
                2
                1
             */

            var flights = new int[][]
          {
                new int[] {0, 1, 100 },
                new int[] {1, 2, 100 },
                new int[] {0, 2, 500 }
          };

            var min = FindCheapestPriceUsingList(3, flights, 0, 2, 1);
            Console.WriteLine($"Min cost is {min}");
        }

        private static void RunSample2()
        {
            /*
             *  10
                [[3,4,4],[2,5,6],[4,7,10],[9,6,5],[7,4,4],[6,2,10],[6,8,6],[7,9,4],[1,5,4],[1,0,4],[9,7,3],[7,0,5],[6,5,8],[1,7,6],[4,0,9],[5,9,1],[8,7,3],[1,2,6],[4,1,5],[5,2,4],[1,9,1],[7,8,10],[0,4,2],[7,2,8]]
                   start: 6
                   dest: 0
                   stops: 7
                Expected: 14
                   */
            var flights = new int[][]
                         {
                            new int[] {3,4,4},
                            new int[] {2,5,6},
                            new int[] {4,7,10},
                            new int[] {9,6,5},
                            new int[] {7,4,4},
                            new int[] {6,2,10},
                            new int[] {6,8,6},
                            new int[] {7,9,4},
                            new int[] {1,5,4},
                            new int[] {1,0,4},
                            new int[] {9,7,3},
                            new int[] {7,0,5},
                            new int[] {6,5,8},
                            new int[] {1,7,6},
                            new int[] {4,0,9},
                            new int[] {5,9,1},
                            new int[] {8,7,3},
                            new int[] {1,2,6},
                            new int[] {4,1,5},
                            new int[] {5,2,4},
                            new int[] {1,9,1},
                            new int[] {7,8,10},
                            new int[] {0,4,2},
                            new int[] {7,2,8}
                         };
            var min = FindCheapestPriceUsingList(10, flights, 6, 0, 7);
            Console.WriteLine($"Min cost is {min}");
        }

        private static void RunSample3()
        {           
            var flights = new int[][]
            {
                new int[] {0, 1, 20 },
                new int[] {1, 2, 20 },
                new int[] {2, 3, 30 },
                new int[] {3, 4, 30 },
                new int[] {4, 5, 30 },
                new int[] {5, 6, 30 },
                new int[] {6, 7, 30 },
                new int[] {7, 8, 30 },
                new int[] {8, 9, 30 },
                new int[] {0, 2, 9999 },
                new int[] {2, 4, 9998 },
                new int[] {4, 7, 9997 }
            };

            var min = FindCheapestPriceUsingList(10, flights, 0, 9, 4);
            Console.WriteLine($"Min cost is {min}");
        }
    }
}
