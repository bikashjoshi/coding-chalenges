using System;

namespace CodingChallenges
{
    public class Heap<T> where T : IComparable<T>
    {
        private readonly T[] _items;
        private int nextIndex = 0;
        private readonly int _maxSize;
        private readonly Func<T, T, bool> _comparision;

        public int Size { get { return nextIndex; } }

        public bool IsEmpty { get { return nextIndex == 0; } }

        public Heap(int size, bool useMin = false)
        {
            _items = new T[size];
            _maxSize = size;
            if (useMin)
                _comparision = (t1, t2) => t1.CompareTo(t2) < 0;
            else
                _comparision = (t1, t2) => t1.CompareTo(t2) > 0;
        }

        /// <summary>
        /// Inserts an item into a heap.
        /// </summary>
        /// <param name="item"></param>
        public void Insert(T item)
        {
            if (Size > _maxSize)
            {
                throw new ArgumentOutOfRangeException("Cannot add further items.");
            }

            _items[nextIndex++] = item;
            // Trickle in proper position            
            // Parent of the node in items[i] - other - than a root - is alwasys stored at items[(i-1)/2]
            int currentIndex = Size - 1;
            int parentIndex = GetParentIndex(currentIndex);
            while (parentIndex >= 0 && _comparision(_items[currentIndex], _items[parentIndex]))
            {
                Swap(parentIndex, currentIndex);
                currentIndex = parentIndex;
                parentIndex = GetParentIndex(currentIndex);
            }
        }

        /// <summary>
        /// Peeks the first item
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return _items[0];
        }

        /// <summary>
        /// Retrieves and deletes the item in the root of a heap.
        /// </summary>
        public T Delete()
        {
            if (IsEmpty)
            {
                return default(T);
            }

            var root = _items[0];
            _items[0] = _items[Size - 1];
            nextIndex--;
            HeapRebuild(0);
            return root;
        }

        private void HeapRebuild(int parentIndex)
        {
            int childIndex = GetLeftChildIndex(parentIndex);
            if (childIndex < Size)
            {
                int rightChildIndex = childIndex + 1;
                if (rightChildIndex < Size && _comparision(_items[rightChildIndex], _items[childIndex]))
                {
                    childIndex = rightChildIndex;
                }

                if (_comparision(_items[childIndex], _items[parentIndex]))
                {
                    Swap(parentIndex, childIndex);
                    HeapRebuild(childIndex);
                }
            }
        }

        private void Swap(int index1, int index2)
        {
            var temp = _items[index1];
            _items[index1] = _items[index2];
            _items[index2] = temp;
        }

        private static int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        private static int GetLeftChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 1;
        }

        public static void RunSample()
        {
            var heap = new Heap<int>(15);
            heap.Insert(3);
            heap.Insert(7);
            heap.Insert(4);
            heap.Insert(1);
            heap.Insert(10);
            heap.Insert(5);
            heap.Insert(20);
            heap.Insert(11);
            heap.Insert(3);
            heap.Insert(10);
            heap.Insert(6);
            heap.Insert(2);

            while (!heap.IsEmpty)
            {
                Console.Write("{0}, ", heap.Delete());
            }

            Console.WriteLine("\r\n");

            heap = new Heap<int>(15, true);
            heap.Insert(3);
            heap.Insert(7);
            heap.Insert(4);
            heap.Insert(1);
            heap.Insert(10);
            heap.Insert(5);
            heap.Insert(20);
            heap.Insert(11);
            heap.Insert(3);
            heap.Insert(10);
            heap.Insert(6);
            heap.Insert(2);

            while (!heap.IsEmpty)
            {
                Console.Write("{0}, ", heap.Delete());
            }
        }
    }
}
