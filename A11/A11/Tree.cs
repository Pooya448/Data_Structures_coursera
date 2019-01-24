using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A11
{
    public class Node
    {
        public bool Checked { get; set; }
        public int LeftChild { get; set; }
        public int RightChild { get; set; }
        public long Key { get; set; }
        public Node(long key, int rightChild = -1, int leftChild = -1)
        {
            LeftChild = leftChild;
            RightChild = rightChild;
            Key = key;
            Checked = false;
        }

    }
    public class Tree
    {
        public Node[] List { get; set; }
        public Tree(long[][] nodes)
        {
            int n = nodes.GetLength(0);
            Node[] nodesArray = new Node[n];
            for (int i = 0; i < nodesArray.Length; i++)
                nodesArray[i] = new Node(-1);
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                nodesArray[i].Key = nodes[i][0];
                nodesArray[i].RightChild = (int)nodes[i][2];
                nodesArray[i].LeftChild = (int)nodes[i][1];
            }
            List = nodesArray;
        }
        void Reset()
        {
            foreach (var item in List)
                item.Checked = false;
        }
        public long[] InOrder()
        {
            List<long> inOrderList = new List<long>();
            Stack<Node> inOrderStack = new Stack<Node>();
            Node pivot = List[0];
            inOrderStack.Push(pivot);
            while (inOrderStack.Any())
            {
                pivot = inOrderStack.Pop();
                if (pivot.Checked)
                    inOrderList.Add(pivot.Key);
                else
                {
                    if (pivot.RightChild != -1)
                        inOrderStack.Push(List[pivot.RightChild]);
                    inOrderStack.Push(pivot);
                    if (pivot.LeftChild != -1)
                        inOrderStack.Push(List[pivot.LeftChild]);
                    pivot.Checked = true;
                }
            }
            Reset();
            return inOrderList.ToArray();
        }
        public bool IsBSTHard()
        {
            if (List.Count() == 0)
                return true;
            Stack<Node> inOrderStack = new Stack<Node>();
            Node pivot = List[0];
            inOrderStack.Push(pivot);
            while (inOrderStack.Any())
            {
                pivot = inOrderStack.Pop();
                if (pivot.Checked)
                    continue;
                else
                {
                    if (pivot.RightChild != -1)
                        if (List[pivot.RightChild].Key < pivot.Key || MinSubtree(List[pivot.RightChild]) < pivot.Key)
                            return false;
                        else
                            inOrderStack.Push(List[pivot.RightChild]);
                    inOrderStack.Push(pivot);
                    if (pivot.LeftChild != -1)
                        if (List[pivot.LeftChild].Key >= pivot.Key || MaxSubtree(List[pivot.LeftChild]) >= pivot.Key)
                            return false;
                        else
                            inOrderStack.Push(List[pivot.LeftChild]);
                    pivot.Checked = true;
                }
            }
            Reset();
            return true;
        }
        public long MaxSubtree(Node vertex)
        {
            Node pivot = vertex;
            while (pivot.RightChild != -1)
                pivot = List[pivot.RightChild];
            return pivot.Key;
        }
        public long MinSubtree(Node vertex)
        {
            Node pivot = vertex;
            while (pivot.LeftChild != -1)
            {
                pivot = List[pivot.LeftChild];
            }
            return pivot.Key;
        }
        public long[] PreOrder()
        {
            List<long> preOrderList = new List<long>();
            Stack<Node> preOrderStack = new Stack<Node>();
            Node pivot = List[0];
            preOrderStack.Push(pivot);
            while (preOrderStack.Any())
            {
                pivot = preOrderStack.Pop();
                if (pivot.Checked)
                    preOrderList.Add(pivot.Key);
                else
                {
                    if (pivot.RightChild != -1)
                        preOrderStack.Push(List[pivot.RightChild]);
                    if (pivot.LeftChild != -1)
                        preOrderStack.Push(List[pivot.LeftChild]);
                    preOrderStack.Push(pivot);
                    pivot.Checked = true;
                }
            }
            Reset();
            return preOrderList.ToArray();
        }
        public long[] PostOrder()
        {
            List<long> postOrderList = new List<long>();
            Stack<Node> postOrderStack = new Stack<Node>();
            Node pivot = List[0];
            postOrderStack.Push(pivot);
            while (postOrderStack.Any())
            {
                pivot = postOrderStack.Pop();
                if (pivot.Checked)
                    postOrderList.Add(pivot.Key);
                else
                {
                    postOrderStack.Push(pivot);
                    if (pivot.RightChild != -1)
                        postOrderStack.Push(List[pivot.RightChild]);
                    if (pivot.LeftChild != -1)
                        postOrderStack.Push(List[pivot.LeftChild]);
                    pivot.Checked = true;
                }
            }
            Reset();
            return postOrderList.ToArray();
        }
    }
}
