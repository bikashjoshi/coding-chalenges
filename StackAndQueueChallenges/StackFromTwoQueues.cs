using System;
using System.Collections;
using System.Collections.Generic;

namespace CodingChallenges.StackAndQueueChallenges
{
    internal class StackFromTwoQueues<T> : IEnumerable<T>
    {
        Queue<T> _firstQueue = new Queue<T>();
        Queue<T> _secondQueue = new Queue<T>();

        public bool IsEmpty
        {
            get
            {
                return _firstQueue.Count == 0 && _secondQueue.Count == 0;
            }
        }     

        public T Pop()
        {
            T nextItem = default(T);
            if (_secondQueue.Count == 0)
            {
                while (_firstQueue.Count > 0)
                {
                    nextItem = _firstQueue.Dequeue();
                    if (_firstQueue.Count == 0)
                        break;
                    _secondQueue.Enqueue(nextItem);
                }

                return nextItem;
            }

            while (_secondQueue.Count > 0)
            {
                nextItem = _secondQueue.Dequeue();
                if (_secondQueue.Count == 0)
                    break;
                _firstQueue.Enqueue(nextItem);
            }

            return nextItem;
        }

        public void Push(T item)
        {
            if (_secondQueue.Count == 0)
                _firstQueue.Enqueue(item);
            else
                _secondQueue.Enqueue(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (!IsEmpty)
                yield return Pop();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("Stack from Two Queues");
            var stack = new StackFromTwoQueues<int>();
            Console.WriteLine("Pushing 1, 2, 4, 7");
            stack.Push(1);
            stack.Push(2);
            stack.Push(4);
            stack.Push(7);
            Console.WriteLine("Poping last item {0}", stack.Pop());
            Console.WriteLine("Pusing 9, 0, 33, 45");
            stack.Push(9);
            stack.Push(0);
            stack.Push(33);
            stack.Push(45);
            Console.WriteLine($"Poping all items: {string.Join(", ", stack)}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }       
    }
}
