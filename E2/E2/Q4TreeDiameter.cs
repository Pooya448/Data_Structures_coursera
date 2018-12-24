using System;
using System.Collections.Generic;
using System.Linq;

namespace E2
{
    public class Q4TreeDiameter
    {

        public class Node
        {
            public List<Node> Connected { get; set; }
            public bool IsChecked;
            public int Id { get; set; }
            public int DepthLevel { get; set; }
            public Node(int id)
            {
                Connected = new List<Node>();
                Id = id;
                IsChecked = false;
            }
        }
        public class Tree
        {
            
            public Node Root { get; set; }
            public List<Node> Nodes { get; set; }
            public int Height { get; set; }
            public Tree(List<int>[] tree)
            {
                Nodes = new List<Node>();
                for (int i = 0; i < tree.Length; i++)
                    Nodes.Add(new Node(i));
                for (int i = 0; i < tree.Length; i++)
                    for (int j = 0; j < tree[i].Count; j++)
                    {
                        Nodes[i].Connected.Add(Nodes[tree[i][j]]);
                        Nodes[tree[i][j]].Connected.Add(Nodes[i]);
                    }
                Root = Nodes[0];
                return;
            }
            
            public (Node,int) FindFurthest(Node root)
            {
                Node furthest = root;
                int distance = 0;
                Queue<Node> bfsQueue = new Queue<Node>();
                root.DepthLevel = 0;
                root.IsChecked = true;
                bfsQueue.Enqueue(root);
                while (bfsQueue.Count > 0)
                {
                    var temp = bfsQueue.Dequeue();
                    if (temp.DepthLevel - root.DepthLevel > distance)
                    {
                        furthest = temp;
                        distance = temp.DepthLevel - root.DepthLevel;
                    }
                    foreach (var item in temp.Connected)
                        if (!item.IsChecked)
                        {
                            bfsQueue.Enqueue(item);
                            item.IsChecked = true;
                            int newDepth = temp.DepthLevel + 1;
                            item.DepthLevel = newDepth;
                        }
                }
                Reset();
                return (furthest,distance);
            }
            
            private void Reset()
            {
                foreach (var item in Nodes)
                {
                    item.IsChecked = false;
                    item.DepthLevel = 0;
                }
            }
        }
            
        public List<int>[] Nodes;
        public Tree MainTree;

        public Q4TreeDiameter(int nodeCount, List<int>[] preCreatedTree = null, int seed = 0)
        {
            Nodes = preCreatedTree == null ? GenerateRandomTree(size: nodeCount, seed: seed) : preCreatedTree;
            MainTree = new Tree(Nodes);
            return;
        }

        public int TreeDiameterN2()
        {
            var furthestFromRoot = MainTree.FindFurthest(MainTree.Root).Item1;
            return MainTree.FindFurthest(furthestFromRoot).Item2;
            
        }

        public int TreeDiameterN()
        {
            return 0;
        }

        private static List<int>[] GenerateRandomTree(int size, int seed)
        {
            Random rnd = new Random(seed);
            List<int>[] nodes = Enumerable.Range(0, size)
                .Select(n => new List<int>())
                .ToArray();
            
            List<int> orphans = 
                new List<int>(Enumerable.Range(1, size-1)); // 0 is root it will remain orphan
            Queue<int> parentsQ = new Queue<int>();
            parentsQ.Enqueue(0);
            while (orphans.Count > 0)
            {
                int parent = parentsQ.Dequeue();
                int childCount = rnd.Next(1, 4);
                for (int i=0; i< Math.Min(childCount, orphans.Count); i++)
                {
                    int orphanIdx = rnd.Next(0, orphans.Count-1);
                    int orphan = orphans[orphanIdx];
                    orphans.RemoveAt(orphanIdx);
                    nodes[parent].Add(orphan);
                    parentsQ.Enqueue(orphan);
                }
            }
            return nodes;
        }
    }
}