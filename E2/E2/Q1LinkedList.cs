
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace E2
{

    public class Q1LinkedList
    {
        public class Node
        {
            public Node(int key) { this.Key = key;  }
            public int Key;
            public Node Next = null;
            public Node Prev = null;
            public override string ToString() => ToString(4);

            public string ToString(int maxDepth)
            {
                return maxDepth == 1 || Next == null ?
                    $"{Key.ToString()}" + (Next != null ? "..." : string.Empty) :
                    $"{Key.ToString()} {Next.ToString(maxDepth - 1)}";
            }
        }

        private Node Head = null;
        private Node Tail = null;

        public void Insert(int key)
        {
            if (Head == null)
            {
                Head = Tail = new Node(key);
            }
            else
            {
                var newNode = new Node(key);
                Tail.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
            }
        }

        public override string ToString() => Head.ToString();

        public void Reverse()
        {
            if (Head == null)
                return;
            Reverse(Head);
            var temp = Head;
            Head = Tail;
            Tail = temp;
            return;
        }

        private void Reverse(Node node)
        {
            if (node.Next != null)
                Reverse(node.Next);
            var tempNext = node.Next;
            node.Next = node.Prev;
            node.Prev = tempNext;
            return;
        }

        public void DeepReverse()
        {
            var pivot = Head;
            while (pivot != null)
            {
                var tempNext = pivot.Next;
                pivot.Next = pivot.Prev;
                pivot.Prev = tempNext;
                pivot = pivot.Prev;
            }
            var temp = Head;
            Head = Tail;
            Tail = temp;
            return;
        }
        public IEnumerable<int> GetForwardEnumerator()
        {
            var it = this.Head;
            while (it != null)
            {
                yield return it.Key;
                it = it.Next;
            }
        }

        public IEnumerable<int> GetReverseEnumerator()
        {
            var it = this.Tail;
            while (it != null)
            {
                yield return it.Key;
                it = it.Prev;
            }
        }
    }
}