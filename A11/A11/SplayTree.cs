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
        public Vertex(long key, Vertex parent = null, Vertex rightChild = null, Vertex leftChild = null)
        {
            LeftChild = leftChild;
            RightChild = rightChild;
            Parent = parent;
            Key = key;
        }

    }
    public class SplayTree
    {
        public Vertex Root { get; set; }

        public SplayTree()
        {

        }
        public Vertex FindNode(long key)
        {
            Vertex Leaf = Root;
            Vertex pivot = Root;
            while (pivot != null)
            {
                Leaf = pivot;
                if (key > pivot.Key)
                {
                    pivot = pivot.RightChild;
                }
                else if (key < pivot.Key)
                {
                    pivot = pivot.LeftChild;
                }
                else
                {
                    return pivot;
                }
                
            }
            return Leaf;
        }
        public Vertex Find(long key)
        {
            //SplayNode lastVisit = Root;
            //SplayNode pivot = Root;
            //while (pivot != null)
            //{
            //    lastVisit = pivot;
            //    pivot = key > pivot.Key ? pivot.RightChild : pivot.LeftChild;
            //}
            //Splay(lastVisit);
            //return;
            if (Root == null)
            {
                return null;
            }
            var temp = FindNode(key);
            if (temp != null)
            {

            Splay(temp);
            }
            return temp;
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
            {
                return;
            }
            var newNode = new Vertex(key, place);
            if (place.Key > key)
            {
                place.LeftChild = newNode;
            }
            else if (place.Key < key)
            {
                place.RightChild = newNode;
            }
            Splay(newNode);
        }
        public void Delete(long key)
        {
            if (Root == null)
            {
                return;
            }
            var place = FindNode(key);
            if (place == null)
            {
                return;
            }
            Splay(place);
            if (Root.Key != key)
                return;
            var tempRight = Root.RightChild;
            var tempLeft = Root.LeftChild;
            Root = tempLeft;
            if (Root != null)
            {
                place.RightChild = tempRight;
            }
            //place = place.LeftChild;
            //if (tempRight != null && place != null)
            //{
            //    place.RightChild = tempRight;
            //}
            //else
            //    place.RightChild = null;
                
            
        }
        public void Splay(Vertex node)
        {
            if (Root.Key == node.Key)
            {
                return;
            }
            while (node.Parent != null)
            {
                if (node.Parent.Parent == null)
                {
                    if (node.Parent.RightChild == node)
                        Zig(node);
                    else
                        Zag(node);
                }
                else if (node.Parent.LeftChild == node && node.Parent.Parent.LeftChild == node.Parent)
                {
                    ZigZig(node.Parent.Parent);
                }
                else if (node.Parent.RightChild == node && node.Parent.Parent.RightChild == node.Parent)
                {
                    ZagZag(node.Parent.Parent);
                }
                else if (node.Parent.LeftChild == node && node.Parent.Parent.RightChild == node.Parent)
                {
                    ZigZag(node);
                }
                else
                {
                    ZagZig(node);
                }
            }
        }
        public void ZigZig(Vertex node)
        {
            Zig(node);
            Zig(node);
        }
        public void ZigZag(Vertex node)
        {
            Zig(node.Parent);
            Zag(node.Parent);
        }
        public void ZagZig(Vertex node)
        {
            Zag(node.Parent);
            Zig(node.Parent);
        }
        public void ZagZag(Vertex node)
        {
            Zag(node);
            Zag(node);
        }
        // right rotation
        public void Zig(Vertex dad)
        {
            Vertex son = dad.LeftChild;
            if (son != null)
            {
                if (son.RightChild != null)
                {
                    son.RightChild.Parent = dad;
                }
                dad.LeftChild = son.RightChild;
                son.Parent = dad.Parent;
                son.RightChild = dad;
            }
            if (dad.Parent == null)
            {
                Root = son;
            }
            else if (dad == dad.Parent.LeftChild)
            {
                dad.Parent.LeftChild = son;
            } else
            {
                dad.Parent.RightChild = son;
            }
            dad.Parent = son;
        }
        // left rotation
        public void Zag(Vertex dad)
        {
            Vertex son = dad.RightChild;
            if (son != null)
            {
                if (son.LeftChild != null)
                {
                    son.LeftChild.Parent = dad;
                }
                dad.RightChild = son.LeftChild;
                son.Parent = dad.Parent;
                son.LeftChild = dad;
            }
            if (dad.Parent == null)
            {
                Root = son;
            }
            else if (dad == dad.Parent.LeftChild)
            {
                dad.Parent.LeftChild = son;
            }
            else
            {
                dad.Parent.RightChild = son;
            }
            dad.Parent = son;
        }
    }
}
