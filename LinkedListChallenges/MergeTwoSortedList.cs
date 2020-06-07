using System;

namespace CodingChallenges.LinkedListChallenges
{
    public class ListNode
    {
       public int val;
       public ListNode next;

       public ListNode(int val = 0, ListNode next = null)
       {
            this.val = val;
            this.next = next;
       }
    }

    internal class MergeTwoSortedList
    {
        /*
         * https://leetcode.com/problems/merge-two-sorted-lists/
         */

        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            ListNode l = null;
            ListNode head = null;

            Func<ListNode, ListNode> assignAndMoveNext = x => {
                if (head == null)
                {
                    head = new ListNode(x.val);
                    l = head;
                }
                else
                {
                    l.next = new ListNode(x.val);
                    l = l.next;
                }

                return x.next;
            };

            while (l1 != null && l2 != null)
            {
                if (l1.val < l2.val)
                {
                    l1 = assignAndMoveNext(l1);
                }
                else
                {
                    l2 = assignAndMoveNext(l2);
                }
            }

            while (l1 != null)
            {
                l1 = assignAndMoveNext(l1);
            }

            while (l2 != null)
            {
                l2 = assignAndMoveNext(l2);
            }

            return head;
        }

        public static void RunSample()
        {
            /*
             *  Input: 1->2->4, 1->3->4
                Output: 1->1->2->3->4->4
             */

            ConsoleHelper.WriteGreen("*** Merge Two Sorted List ***");
            var l1 = new ListNode(1);
            l1.next = new ListNode(2);
            l1.next.next = new ListNode(4);

            var l2 = new ListNode(1);
            l2.next = new ListNode(3);
            l2.next.next = new ListNode(4);

            var l = MergeTwoLists(l1, l2);

            Console.Write("First Linked List: ");
            Print(l1);
            Console.Write("Second Linked List: ");
            Print(l2);
            Console.Write("Merged Linked List: ");
            Print(l);
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }

        private static void Print(ListNode l)
        {
            var node = l;
            while(node != null)
            {
                Console.Write($"{node.val}");
                node = node.next;
                if (node != null)
                {
                    Console.Write("->");
                }
            }

            Console.Write(Environment.NewLine);
        }
    }   
}
