using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    class Node
    {
        public List<Node> Childs { get; set; }
        public int Id { get; set; }
        public int DepthLevel { get; set; }
        public Node(int id)
        {
            Childs = new List<Node>();
            Id = id;
        }
    }
    class Tree
    {
        public Node Root { get; set; }
        public List<Node> Nodes { get; set; }
        public int Height { get; set; }
        public Tree(long[] tree)
        {
            Nodes = new List<Node>();
            for (int i = 0; i < tree.Length; i++)
                Nodes.Add(new Node(i));
            for (int i = 0; i < tree.Length; i++)
                if (tree[i] == -1)
                {
                    Root = Nodes[i];
                    Root.DepthLevel = 1;
                } else
                    Nodes[(int)tree[i]].Childs.Add(Nodes[i]);
        }
        public long FindHeight()
        {
            int height = Root.DepthLevel;
            Queue<Node> bfsQueue = new Queue<Node>();
            bfsQueue.Enqueue(Root);
            while (bfsQueue.Count > 0)
            {
                var temp = bfsQueue.Dequeue();
                for (int i = 0; i < temp.Childs.Count; i++)
                {
                    bfsQueue.Enqueue(temp.Childs[i]);
                    int newDepth = temp.DepthLevel + 1;
                    temp.Childs[i].DepthLevel = newDepth;
                    height = height > newDepth ? height : newDepth;
                }
            }
            Height = height;
            return height;
        }
    }
    public class TreeHeight : Processor
    {
        public TreeHeight(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long nodeCount, long[] treeList)
        {
            Tree tree = new Tree(treeList);
            return tree.FindHeight();
        }
    }
}
