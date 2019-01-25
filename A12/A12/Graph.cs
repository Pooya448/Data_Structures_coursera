using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A12
{
    class Vertex
    {
        public List<Vertex> ConnectedVertices { get; set; }
        public List<Vertex> ConnectedToVertices { get; set; }
        public int Id { get; set; }
        public bool Check { get; set; }
        public int SccId { get; set; }
        public int PostVisit { get; set; }
        public int PreVisit { get; set; }
        public Vertex(int id)
        {
            Id = id;
            ConnectedVertices = new List<Vertex>();
            ConnectedToVertices = new List<Vertex>();
            return;
        }
    }
    class Graph
    {
        public int CcNumber { get; private set; }
        public List<Vertex> Vertices { get; set; }
        public List<long> TopoligicalSort { get; set; }
        public int VertexCount { get; set; }
        private int Clock;
        public Graph(long nodeCount, long[][] edges, bool isDirected = false, bool transpose = false)
        {
            Clock = 0;
            Vertices = new List<Vertex>();
            CcNumber = 0;
            VertexCount = (int)nodeCount;
            for (int i = 1; i <= VertexCount; i++)
                Vertices.Add(new Vertex(i));
            if (isDirected)
                for (int i = 0; i < edges.GetLength(0); i++)
                {
                    Vertices[(int)edges[i][0] - 1].ConnectedVertices.Add(Vertices[(int)edges[i][1] - 1]);
                    Vertices[(int)edges[i][1] - 1].ConnectedToVertices.Add(Vertices[(int)edges[i][0] - 1]);
                }
            else
                for (int i = 0; i < edges.GetLength(0); i++)
                {
                    Vertices[(int)edges[i][0] - 1].ConnectedVertices.Add(Vertices[(int)edges[i][1] - 1]);
                    Vertices[(int)edges[i][1] - 1].ConnectedVertices.Add(Vertices[(int)edges[i][0] - 1]);
                }
            return;
        }
        public bool FindPath(int start, int end)
        {
            Queue<Vertex> bfsQueue = new Queue<Vertex>();
            Vertex startVertex = Vertices[(int)(start - 1)];
            Vertex target = Vertices[end - 1];
            if (startVertex == target)
                return true;
            startVertex.Check = true;
            bfsQueue.Enqueue(startVertex);
            while (bfsQueue.Any())
            {
                var temp = bfsQueue.Dequeue();
                temp.Check = true;
                foreach (var item in temp.ConnectedVertices)
                    if (!item.Check)
                    {
                        if (item == target)
                            return true;
                        bfsQueue.Enqueue(item);
                    }
            }
            return false;
        }
        public int FindCC()
        {
            Stack<Vertex> dfsStack = new Stack<Vertex>();
            int ccCounter = 0;
            foreach (var vertex in Vertices)
                if (!vertex.Check)
                {
                    vertex.SccId = ccCounter;
                    dfsStack.Push(vertex);
                    while (dfsStack.Any())
                    {
                        var temp = dfsStack.Pop();
                        temp.Check = true;
                        temp.SccId = vertex.SccId;
                        foreach (var item in temp.ConnectedVertices)
                            if (!item.Check)
                                dfsStack.Push(item);
                    }
                    ccCounter++;
                }
            CcNumber = ccCounter;
            return CcNumber;
        }
        // Checks if the graph is a DAG , and if so, calculates the topological sort of the graph
        public bool IsDAG()
        {
            TopoligicalSort = new List<long>();
            bool foundSink = false;
            for (int i = VertexCount; i > 0; i--)
            {
                for (int j = 0; j < Vertices.Count; j++)
                    if (Vertices[j].ConnectedVertices.Count == 0)
                    {
                        for (int k = 0; k < Vertices[j].ConnectedToVertices.Count; k++)
                            Vertices[j].ConnectedToVertices[k].ConnectedVertices.Remove(Vertices[j]);
                        TopoligicalSort.Add(Vertices[j].Id);
                        Vertices.Remove(Vertices[j]);
                        foundSink = true;
                        j = Vertices.Count;
                    }
                if (!foundSink)
                    return false;
                else
                    foundSink = false;
            }
            TopoligicalSort.Reverse();
            return true;
        }
        public long FindSCCs()
        {
            Transpose();
            foreach (var item in Vertices)
                if (!item.Check)
                    DFS(item);
            Reset();
            Transpose();
            Vertices = Vertices.OrderByDescending(x => x.PostVisit).ToList();
            return FindCC();
        }
        public void DFS(Vertex v)
        {
            v.Check = true;
            v.PreVisit = ++Clock;
            foreach (var item in v.ConnectedVertices)
                if (!item.Check)
                    DFS(item);
            v.PostVisit = ++Clock;
        }
        public void Transpose()
        {
            foreach (var vertex in Vertices)
            {
                var tempTo = vertex.ConnectedToVertices;
                vertex.ConnectedToVertices = vertex.ConnectedVertices;
                vertex.ConnectedVertices = tempTo.ToList();
            }
            return;
        }
        public void Reset()
        {
            Clock = 0;
            foreach (var item in Vertices)
                item.Check = false;
        }
    }
}
