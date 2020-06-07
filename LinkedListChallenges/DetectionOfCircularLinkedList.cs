using System;

namespace CodingChallenges.LinkedListChallenges
{
    public class Node<T>
    {
        public T Value { get; set; }

        public Node<T> Next { get; set; }
    }

    internal static class DetectionOfCircularLinkedList
    {
        public static bool IsCircular<T>(this Node<T> node)
        {
            if (node == null)
            {
                return false;
            }

            var fast = node;
            var slow = node;
            do
            {
                if (slow == null)
                {
                    return false;
                }

                slow = slow.Next;

                if (fast == null || fast.Next == null)
                {
                    return false;
                }

                fast = fast.Next.Next;

                if (slow == fast)
                {
                    return true;
                }

            } while (true);
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("*** Detection of Circular Linked List Sample ***");
            var node1 = new Node<int>() { Value = 1 };
            var node2 = new Node<int>() { Value = 2 };
            var node3 = new Node<int>() { Value = 3 };
            var node4 = new Node<int>() { Value = 4 };
            node1.Next = node2;
            node2.Next = node3;
            node3.Next = node4;
            Console.WriteLine("Is circular: {0}", node1.IsCircular());
            node4.Next = node2;
            Console.WriteLine("Is circular: {0}", node1.IsCircular());
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
