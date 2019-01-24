using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A11
{
    public class Vertex
    {
        public Vertex Parent { get; set; }
        public Vertex LeftChild { get; set; }
        public Vertex RightChild { get; set; }
        public bool IsRightChild => Parent != null && Parent.RightChild.Key == this.Key;
        public long Key { get; set; }
        public bool IsChecked;
        public Vertex(long key, Vertex parent = null, Vertex rightChild = null, Vertex leftChild = null)
        {
            LeftChild = leftChild;
            RightChild = rightChild;
            Parent = parent;
            Key = key;
            IsChecked = false;
        }

    }
    public class SplayTree
    {
        public Vertex Root { get; set; }

        public SplayTree() { }

        public Vertex FindNode(long key)
        {
            Vertex Leaf = null;
            Vertex pivot = Root;
            while (pivot != null)
            {
                Leaf = pivot;
                if (key > pivot.Key)
                    pivot = pivot.RightChild;
                else if (key < pivot.Key)
                    pivot = pivot.LeftChild;
                else
                    return pivot;
            }
            return Leaf;
        }
        
        public Vertex Find(long key)
        {
            if (Root == null)
                return null;
            var temp = FindNode(key);
            if (temp != null)
                Splay(temp);
            return temp;
        }
        public long[] InOrder()
        {
            List<long> inOrderList = new List<long>();
            Stack<Vertex> inOrderStack = new Stack<Vertex>();
            Vertex pivot = Root;
            inOrderStack.Push(pivot);
            while (inOrderStack.Any())
            {
                pivot = inOrderStack.Pop();
                if (pivot.IsChecked)
                {
                    inOrderList.Add(pivot.Key);
                    pivot.IsChecked = false;
                }
                else
                {
                    if (pivot.RightChild != null)
                        inOrderStack.Push(pivot.RightChild);
                    inOrderStack.Push(pivot);
                    if (pivot.LeftChild != null)
                        inOrderStack.Push(pivot.LeftChild);
                    pivot.IsChecked = true;
                }
            }
            return inOrderList.ToArray();
        }
        public void Insert(long key)
        {
            if (Root == null)
            {
                Root = new Vertex(key);
                return;
            }
            var place = FindNode(key);
            if (place.Key == key)
                return;
            var newNode = new Vertex(key, place);
            if (place.Key > key)
                place.LeftChild = newNode;
            else if (place.Key < key)
                place.RightChild = newNode;
            Splay(newNode);
        }
        public long RangeSum(long l, long r)
        {
            if (Root == null)
                return 0;
            var order = InOrder();
            long sum = 0;
            foreach (var num in order)
                if (num <= r && num >= l)
                    sum += num;
            return sum;
        }
        public void Delete(long key)
        {
            if (Root == null)
                return;
            var place = FindNode(key);
            if (place.Key != key)
                return;
            Splay(place);
            if (place.LeftChild != null && place.RightChild != null)
            {
                var tempLeft = place.LeftChild;
                while (tempLeft.RightChild != null)
                {
                    tempLeft = tempLeft.RightChild;
                }
                tempLeft.RightChild = place.RightChild;
                place.RightChild.Parent = tempLeft;
                place.LeftChild.Parent = null;
                Root = place.LeftChild;
            }
            else if (place.RightChild != null)
            {
                place.RightChild.Parent = null;
                Root = place.RightChild;
            }
            else if (place.LeftChild != null)
            {
                place.LeftChild.Parent = null;
                Root = place.LeftChild;
            }
            else
                Root = null;
            place.Parent = null;
            place.LeftChild = null;
            place.RightChild = null;
            place = null;
        }
        public void Splay(Vertex node)
        {
            if (Root.Key == node.Key)
                return;
            while (node.Parent != null)
            {
                if (node.Parent.Parent == null)
                    if (node.Parent.RightChild == node)
                        Zag(node,node.Parent);
                    else
                        Zig(node,node.Parent);
                else if (node.Parent.LeftChild == node && node.Parent.Parent.LeftChild == node.Parent)
                    ZigZig(node);
                else if (node.Parent.RightChild == node && node.Parent.Parent.RightChild == node.Parent)
                    ZagZag(node);
                else if (node.Parent.LeftChild == node && node.Parent.Parent.RightChild == node.Parent)
                    ZigZag(node);
                else
                    ZagZig(node);
            }
            Root = node;
        }
        public void ZigZig(Vertex node)
        {
            Zig(node.Parent, node.Parent.Parent);
            Zig(node, node.Parent);
        }
        public void ZigZag(Vertex node)
        {
            Zig(node, node.Parent);
            Zag(node, node.Parent);
        }
        public void ZagZig(Vertex node)
        {
            Zag(node, node.Parent);
            Zig(node, node.Parent);
        }
        public void ZagZag(Vertex node)
        {
            Zag(node.Parent, node.Parent.Parent);
            Zag(node, node.Parent);
        }
        // right rotation
        public void Zig(Vertex vertex, Vertex vertexParent)
        {
            if (vertex == null || vertexParent == null)
                return;

            if (vertexParent.Parent != null)
            {
                if (vertexParent == vertexParent.Parent.LeftChild)
                    vertexParent.Parent.LeftChild = vertex;
                else
                    vertexParent.Parent.RightChild = vertex;
            }
            if (vertex.RightChild != null)
                vertex.RightChild.Parent = vertexParent;
            vertex.Parent = vertexParent.Parent;
            vertexParent.Parent = vertex;
            vertexParent.LeftChild = vertex.RightChild;
            vertex.RightChild = vertexParent;
        }
        // left rotation
        public void Zag(Vertex vertex, Vertex vertexParent)
        {
            if (vertex == null || vertexParent == null)
                return;
            if (vertexParent.Parent != null)
            {
                if (vertexParent == vertexParent.Parent.LeftChild)
                    vertexParent.Parent.LeftChild = vertex;
                else
                    vertexParent.Parent.RightChild = vertex;
            }
            if (vertex.LeftChild != null)
                vertex.LeftChild.Parent = vertexParent;
            vertex.Parent = vertexParent.Parent;
            vertexParent.Parent = vertex;
            vertexParent.RightChild = vertex.LeftChild;
            vertex.LeftChild = vertexParent;
        }
    }
}
