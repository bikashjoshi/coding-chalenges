using System;
using System.Collections.Generic;

namespace CodingChallenges.LinkedListChallenges
{
    class LRUCache
    {
        /* https://leetcode.com/problems/lru-cache/ */

        internal class Node
        {
            public int Key { get; set; }
            public int Value { get; set; }
            public Node Next { get; set; }
            public Node Previous { get; set; }
        }

        class LinkedList
        {
            Node _root;
            Node _tail;

            public Node Insert(int key, int value)
            {
                var node = new Node { Key = key, Value = value };

                if (_root == null)
                {
                    _root = node;
                }
                else
                {
                    if (_tail == null)
                    {
                        _tail = _root;
                    }

                    _root.Previous = node;
                    node.Next = _root;
                    _root = node;                                  
                }

                return node;
            }

            public void MoveToRoot(Node node)
            {
                if (node == _root)
                {
                    return;
                }

                if (node == _tail)
                {
                    _tail = _tail.Previous;
                    _tail.Next = null;
                }

                var previous = node.Previous;
                var next = node.Next;
                previous.Next = next;
                if (next != null)
                {
                    next.Previous = previous;
                }                

                node.Next = _root;
                _root.Previous = node;
                node.Previous = null;
                _root = node;
            }

            public int RemoveTail()
            {
                if (_tail == null)
                {
                    var rootKey = _root.Key;
                    _root = null;
                    return rootKey;
                }

                var key = _tail.Key;
                var previous = _tail.Previous;
                previous.Next = null;
                _tail = previous;
                return key;
            }

            public Node GetRoot()
            {
                return _root;
            }
        }

        Dictionary<int, Node> _cache = new Dictionary<int, Node>();
        LinkedList _list = new LinkedList();

        int _counter = 0;
        int _capacity = 0;

        public LRUCache(int capacity)
        {
            _capacity = capacity;
        }

        public int Get(int key)
        {
            if (_cache.ContainsKey(key))
            {
                var node = _cache[key];
                _list.MoveToRoot(node);
                return node.Value;
            }

            return -1;
        }

        public void Put(int key, int value)
        {          
            if (_cache.ContainsKey(key))
            {
                _cache[key].Value = value;
                _list.MoveToRoot(_cache[key]);
                return;
            }
            _counter++;
            if (_counter > _capacity)
            {
                var tailKey = _list.RemoveTail();
                _cache.Remove(tailKey);
                _counter--;
            }

            var node = _list.Insert(key, value);
            _cache.Add(key, node);
        }

        public static void RunSample()
        {
            /*
            * ["LRUCache","put","put","get","put","get","put","get","get","get"]
               [[2],[1,1],[2,2],[1],[3,3],[2],[4,4],[1],[3],[4]]
            */
            ConsoleHelper.WriteGreen("*** LRU Cache Sample ***");
            Console.WriteLine("LRUCache: 2");
            var lruCache = new LRUCache(2);
            Console.WriteLine("put: [1,1], put: [2,2]");
            lruCache.Put(1, 1);
            lruCache.Put(2, 2);            
            Console.WriteLine("get: 1 => " + lruCache.Get(1));
            Console.WriteLine("put: [3,3]");
            lruCache.Put(3, 3);                        
            Console.WriteLine("get: 2 => " + lruCache.Get(2));
            Console.WriteLine("put: [4,4]");
            lruCache.Put(4, 4);
            Console.WriteLine("get: 1 => " + lruCache.Get(1));            
            Console.WriteLine("get: 3 => " + lruCache.Get(3));            
            Console.WriteLine("get: 4 => " + lruCache.Get(4));                        
            ConsoleHelper.WriteBlue("-------------------------------------------------------");
        }
    }
}
