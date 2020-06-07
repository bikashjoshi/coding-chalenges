using System;
using System.Collections;
using System.Collections.Generic;

namespace CodingChallenges.StackAndQueueChallenges
{
    internal class QueueFromTwoStacks<T> : IEnumerable<T>
    {
        Stack<T> _firstStack = new Stack<T>();
        Stack<T> _secondStack = new Stack<T>();

        public bool IsEmpty
        {
            get
            {
               return  _firstStack.Count == 0 && _secondStack.Count == 0;
            }
        }

        public void Enqueue(T item)
        {
            _firstStack.Push(item);
        }

        public T Dequeue()
        {
            if (_secondStack.Count > 0)
            {
                return _secondStack.Pop();
            }

            while(_firstStack.Count > 0)
            {
                _secondStack.Push(_firstStack.Pop());
            }

            return _secondStack.Pop();
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (!IsEmpty)
                yield return Dequeue();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public static void RunSample()
        {
            ConsoleHelper.WriteGreen("Queue from Two Stacks");
            var queue = new QueueFromTwoStacks<int>();
            Console.WriteLine("Enqueing 1, 2, 4, 7");
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(4);
            queue.Enqueue(7);
            Console.WriteLine("Dequeing first item {0}", queue.Dequeue());
            Console.WriteLine("Enqueing 9, 0, 33, 45");
            queue.Enqueue(9);
            queue.Enqueue(0);
            queue.Enqueue(33);
            queue.Enqueue(45);
            Console.WriteLine($"Dequeing all items: {string.Join(", ", queue)}");
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
