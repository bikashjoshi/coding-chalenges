using System;

namespace CodingChallenges
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayChallenges._3Sum.RunSample();
            ArrayChallenges.BinarySearchInRotatedArray.RunSample();
            ArrayChallenges.DutchFlagSorting.RunSample();
            ArrayChallenges.MaxProfitOnStock.RunSample();
            ArrayChallenges.MedianOfTwoSortedArrays.RunSample();
            ArrayChallenges.ProductExceptSelf.RunSample();
            ArrayChallenges.SecondLargestForLastStoneRemainning.RunSample();

            DynamicProgramming.KadanesToFindLargestSum.RunSample();
            DynamicProgramming.KnapSack.RunSample();
            DynamicProgramming.LongestCommonSubsequence.RunSample();
            DynamicProgramming.LongestIncreasingSubsequence.RunSample();
            DynamicProgramming.MaximalSquare.RunSample();
            DynamicProgramming.PaintingHouses.RunSample();
            DynamicProgramming.Staircase.RunSample();

            GraphChallenges.BellmanFord.RunSample();
            GraphChallenges.BipartiteCheck.RunSample();
            GraphChallenges.CheapestFlightWithinKStops.RunSample();
            GraphChallenges.CourseSorting.RunSample();
            GraphChallenges.CycleDetectorDirectedGraph.RunSample();
            GraphChallenges.Dijkstra.RunSample();
            GraphChallenges.Island.RunSample();
            GraphChallenges.MstKruskalAlgorithm.RunSample();
            GraphChallenges.MstPrimsAlgorithm.RunSample();
            GraphChallenges.ShortestPathBreakingThroughWalls.RunSample();
            GraphChallenges.WordSearch.RunSample();

            HeapChallenges.RunningMedian.RunSample();

            LinkedListChallenges.DetectionOfCircularLinkedList.RunSample();
            LinkedListChallenges.LRUCache.RunSample();
            LinkedListChallenges.MergeTwoSortedList.RunSample();

            Misc.IsHappyNumber.RunSample();

            Puzzle.NQueens.RunSample();
            Puzzle.SnakeAndLadder.RunSample();

            StackAndQueueChallenges.QueueFromTwoStacks<int>.RunSample();
            StackAndQueueChallenges.StackFromTwoQueues<int>.RunSample();
            StackAndQueueChallenges.CheckValidString.RunSample();

            StringChallenges.GroupingAnagram.RunSample();
            StringChallenges.ValidAnagram.RunSample();

            TreeChallenges.BinarySearchTreeBalanceDetector.RunSample();
            TreeChallenges.BinarySearchTreeBalanceOperation.RunSample();
            TreeChallenges.BinarySearchTreeConstruction.RunSample();
            TreeChallenges.BinaryTreeConstruction.RunSample();
            TreeChallenges.BinaryTreeDiameter.RunSample();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
