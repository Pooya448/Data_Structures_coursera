using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;


namespace A5
{
    public class Program
    {
        static void Main(string[] args)
        {
            long[] x = {0,3};
            long[] y = {0,4};
            Console.WriteLine(ClosestPoints6(2,x,y));
            Console.Read();
        }

        public static long[] BinarySearch1(long[] a, long[] b)
        {

            List<long> results = new List<long>();
            foreach (var item in b)
            {
                long low = 0; long high = a.Length - 1;
                while (true)
                {
                    long mid = (low + high) / 2;
                    if ((mid == 0 || item > a[mid - 1]) && (a[mid] == item)) { results.Add(mid); break; }
                    else if (item < a[mid]) high = mid - 1;
                    else low = mid + 1;
                    if (low > high)
                    {
                        results.Add(-1);
                        break;
                    }
                }
            }
            return results.ToArray();
        }

        public static string ProcessBinarySearch1(string inStr) =>
            TestTools.Process(inStr,(Func<long[],long[],long[]>)BinarySearch1);

        public static long MajorityElement2(long n, long[] a)
        {
            a = a.OrderBy(x => x).ToArray();
            foreach (var item in a)
            {
                if (isMajority(a, n, item))
                {
                    return 1;
                }
            }
            return 0;

        }

        static long binarySearch(long[] a, long low, long high, long x)
        {
            if (high >= low)
            {
                long mid = (low + high) / 2;
                if ((mid == 0 || x > a[mid - 1]) && (a[mid] == x))
                    return mid;
                else if (x > a[mid])
                    return binarySearch(a, (mid + 1), high, x);
                else
                    return binarySearch(a, low, (mid - 1), x);
            }
            return -1;
        }

        static bool isMajority(long[] a, long n, long x)
        {
            long i = binarySearch(a, 0, n - 1, x);
            if (i == -1)
                return false;
            if (((i + n / 2) <= (n - 1)) && a[i + n / 2] == x)
                return true;
            else
                return false;
        }

        public static string ProcessMajorityElement2(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)MajorityElement2);

        public static long GetRandom(int start, int end)
        {
            Random rnd = new Random();
            return rnd.Next(start, end);
        }

        public static void Swap(ref long a, ref long b)
        {
            var temp = a;
            a = b;
            b = temp;
            return;
        }

        public static void QuickSort(long[] mainArray, int start, int end)
        {
            int lt = start, gt = end;
            if (start < end)
            {
                long pivotIndex = GetRandom(start, end);
                Partition(mainArray, pivotIndex, start, end, ref gt, ref lt);

                QuickSort(mainArray, start, lt - 1);
                QuickSort(mainArray, gt + 1, end);
            }
        }

        private static void Partition(long[] mainArray,
                                      long pivotIndex, int start, int end,
                                      ref int gt, ref int lt)
        {
            Swap(ref mainArray[pivotIndex], ref mainArray[start]);
            var pivotValue = mainArray[start];
            int i = start;
            while (i <= gt)
            {
                if (mainArray[i] < pivotValue)
                {
                    Swap(ref mainArray[i], ref mainArray[lt]);
                    lt++;
                    i++;
                }
                else if (mainArray[i] > pivotValue)
                {
                    Swap(ref mainArray[i], ref mainArray[gt]);
                    gt--;
                }
                else if (mainArray[i] == pivotValue)
                {
                    i++;
                }
            }
        }

        public static long[] ImprovingQuickSort3(long n, long[] a)
        {
            QuickSort(a, 0, (int)n - 1);
            return a;
        }

        public static string ProcessImprovingQuickSort3(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[]>)ImprovingQuickSort3);

        public static long NumberofInversions4(long n, long[] a)
        {
            long invCount = 0;
            MergeSort(a.ToList(), 0, (int)n - 1, ref invCount);
            return invCount;
        }

        public static void MergeSort(List<long> nums, int left, int right, ref long invCount)
        {
            if (right > left)
            {
                int mid = (right + left) / 2;

                MergeSort(nums, left, mid, ref invCount);
                MergeSort(nums, mid + 1, right, ref invCount);

                Merge(nums, left, mid, right, ref invCount);
            }
        }

        private static void Merge(List<long> nums, int left, int mid, int right, ref long invCount)
        {
            List<long> LeftHalf = nums.GetRange(left, mid - left + 1);
            List<long> RightHalf = nums.GetRange(mid + 1, right - mid);

            int i = 0;
            int j = 0;
            int k = left;

            while (i < LeftHalf.Count && j < RightHalf.Count)
            {
                if (LeftHalf[i] > RightHalf[j])
                {
                    nums[k++] = RightHalf[j++];
                    invCount += LeftHalf.Count - i;
                }
                else
                {
                    nums[k++] = LeftHalf[i++];
                }
            }

            while (i < LeftHalf.Count)
            {
                nums[k++] = LeftHalf[i++];
            }
            while (j < RightHalf.Count)
            {
                nums[k++] = RightHalf[j++];
            }

        }

        public static string ProcessNumberofInversions4(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)NumberofInversions4);

        public enum Segment
        {
            Point = 2,
            Start = 1,
            End = 3
        }

        public static long[] OrganizingLottery5(long[] points, long[] startSegments, long[] endSegment)
        {
            List<(Segment, long, int)> Data = new List<(Segment, long, int)>();

            for (int i = 0; i < points.Length; i++)
                Data.Add((Segment.Point, points[i], i));

            for (int i = 0; i < startSegments.Length; i++)
                Data.Add((Segment.Start, startSegments[i], i));

            for (int i = 0; i < startSegments.Length; i++)
                Data.Add((Segment.End, endSegment[i], i));

            Data = Data.OrderBy(x => x.Item2).ThenBy(x => x.Item1).ToList();

            long startCounter = 0, endCounter = 0;
            long[] result = new long[points.Length];

            foreach (var item in Data)
            {
                if (item.Item1 == Segment.End)
                    endCounter++;
                else if (item.Item1 == Segment.Start)
                    startCounter++;
                else
                    result[item.Item3] = startCounter - endCounter;
            }
            return result;
        }

        public static string ProcessOrganizingLottery5(string inStr) =>
            TestTools.Process(inStr,(Func<long[], long[], long[], long[]>) OrganizingLottery5);

        public static double ClosestPoints6(long n, long[] xPoints, long[] yPoints)
        {
            var Data = xPoints
                       .Zip(yPoints, (x, y) => (x, y))
                       .OrderBy(p => p.y)
                       .ToList();

            if (n <= 3)
            {
                return BruteForce(Data);
            }
            return Math.Round(FindDistance(Data, 0, (int)n - 1), 4);
        }

        private static double BruteForce(List<(long x, long y)> data)
        {
            double Min = double.MaxValue;
            for (int i = 0; i < data.Count; i++)
                for (int j = i + 1; j < data.Count; j++)
                    Min = CalculateDis(data[i], data[j]) < Min ? CalculateDis(data[i], data[j]) : Min;
            return Min;
        }

        public static double FindDistance(List<(long, long)> Points, int low, int high)
        {
            if (low >= high)
                return double.MaxValue;
            if (high - low == 1)
            {
                return CalculateDis(Points[low], Points[high]);
            }
            int mid = (low + high) / 2;
            double LeftDistance = FindDistance(Points, low, mid);
            double RightDistance = FindDistance(Points, mid + 1, high);   
            double MinDistance = FindMin(LeftDistance, RightDistance);
            var Strip = Points
                       .GetRange(low, Points.Count - high - 1)
                       .Where(p => Math.Abs(p.Item1 - Points[mid].Item1) < MinDistance)
                       .ToList();
            
            for (int i = 0; (i < Strip.Count); i++)
                for (int j = i + 1;  j <= i + 7 && j < Strip.Count; j++)
                    MinDistance = MinDistance < CalculateDis(Strip[i], Strip[j]) ? MinDistance : CalculateDis(Strip[i], Strip[j]);
            return MinDistance;
        }

        private static double CalculateDis((long, long) p1, (long, long) p2)
        {
            return Math.Abs(Math.Sqrt(Math.Pow((p1.Item1 - p2.Item1), 2) + Math.Pow((p1.Item2 - p2.Item2), 2)));
        }

        private static double FindMin(params double[] values)
        {
            return values.OrderBy(x => x).First();
        }

        public static string ProcessClosestPoints6(string inStr) =>
           TestTools.Process(inStr, (Func<long, long[], long[], double>)
               ClosestPoints6);

        
    }
}
