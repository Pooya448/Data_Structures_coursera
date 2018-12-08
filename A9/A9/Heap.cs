using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A9
{
    public class Heap
    {
        List<long> HeapArray;
        public List<Tuple<long, long>> Swaps { get; set; }
        public long Size { get; set; }
        public Heap(long[] array)
        {
            Size = array.Length;
            HeapArray = array.ToList();
            Swaps = new List<Tuple<long, long>>();
        }
        private (int, long) RightChild(int i)
            => (2 * i + 2, HeapArray[(2 * i) + 2]);
        private (int, long) LeftChild(int i)
            => (2 * i + 1, HeapArray[(2 * i) + 1]);
        private bool HasLeftChild(int i)
            => 2 * i + 1 < Size;
        private bool HasRightChild(int i)
            => 2 * i + 2 < Size;
        private void Swap(int i, int j)
        {
            var temp = HeapArray[i];
            HeapArray[i] = HeapArray[j];
            HeapArray[j] = temp;
            Swaps.Add(new Tuple<long, long>(i, j));
            return;
        }
        private void Heapify(int index)
        {
            var element = HeapArray[index];
            if (HasLeftChild(index))
            {
                var L = LeftChild(index);
                if (HasRightChild(index))
                {

                    var R = RightChild(index);
                    if (
                    R.Item2 < element &&
                    R.Item2 < L.Item2)
                    {
                        Swap(index, R.Item1);
                        Heapify(R.Item1);
                    }
                    else if (
                      L.Item2 < element &&
                      R.Item2 > L.Item2)
                    {
                        Swap(index, L.Item1);
                        Heapify(L.Item1);
                    }
                }
                else if (L.Item2 < element)
                {
                    Swap(index, L.Item1);
                    Heapify(L.Item1);
                }
            }
            return;
        }

        private (bool, int) FindMin(int i)
        {
            var sortUtil = new List<(long, int)>();
            if (HasLeftChild(i))
            {
                var L = LeftChild(i);
                sortUtil.Add((L.Item2, L.Item1));
            }
            if (HasRightChild(i))
            {
                var R = RightChild(i);
                sortUtil.Add((R.Item2, R.Item1));
            }
            if (sortUtil.Any())
            {
                var Min = sortUtil.OrderBy(x => x.Item1).First();
                if (Min.Item1 == HeapArray[i])
                    return (false, -1);
                else
                    return (true, Min.Item2);
            }
            return (false, -1);
        }

        public void BuildMinHeap()
        {
            int startIndex = (HeapArray.Count - 2) / 2;
            for (int i = startIndex; i >= 0; i--)
                Heapify(i);
        }
    }
}
