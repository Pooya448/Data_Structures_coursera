﻿using System;
using TestCommon;
using System.Collections.Generic;
using System.Linq;

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
        public bool IsBST()
        {
            if (List.Count() == 0)
                return true;
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
                        if (List[pivot.RightChild].Key < pivot.Key)
                            return false;
                        else
                            inOrderStack.Push(List[pivot.RightChild]);
                    inOrderStack.Push(pivot);
                    if (pivot.LeftChild != -1)
                        if (List[pivot.LeftChild].Key >= pivot.Key)
                            return false;
                        else
                            inOrderStack.Push(List[pivot.LeftChild]);
                    pivot.Checked = true;
                }
            }
            Reset();
            return true;
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
    public class BinaryTreeTraversals : Processor
    {
        public BinaryTreeTraversals(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], long[][]>)Solve);


        public long[][] Solve(long[][] nodes)
        {
            Tree tree = new Tree(nodes);
            var result = new long[3][];
            result[0] = tree.InOrder();
            result[1] = tree.PreOrder();
            result[2] = tree.PostOrder();
            return result;
        }
    }
}
