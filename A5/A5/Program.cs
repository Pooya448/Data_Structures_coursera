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
        static void Main(string[] args) { }

        public static long[] BinarySearch1(long[] a , long [] b)
        {
            
            List<long> results = new List<long>();
            foreach (var item in b)
            {
                long low = 0; long high = a.Length - 1;
                while (true)
                {
                    long mid = (low + high) / 2;
                    if (item == a[mid]) { results.Add(mid); break; }
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
            TestTools.Process(inStr, BinarySearch1);

        public static long MajorityElement2(long n, long[] a)
        {
            a = a.OrderBy(x => x).ToArray();
            foreach (var item in a)
            {
                if (isMajority(a,n,item))
                {
                    return 1;
                }
            } return 0;
            
        }

        static long binarySearch(long[] a, long low,long high, long x)
        {
            if (high >= low)
            {
                long mid = (low + high) / 2;
                if ((mid == 0 || x > a[mid - 1]) && (a[mid] == x))
                    return mid;
                else if (x > a[mid])
                    return binarySearch(a, (mid + 1),high, x);
                else
                    return binarySearch(a, low,
                                          (mid - 1), x);
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

        public static long[] ImprovingQuickSort3(long n, long[] a)
        {
            //write your code here          
            return new long[] { 0 };
        }

        public static string ProcessImprovingQuickSort3(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[]>)ImprovingQuickSort3);

        public static long NumberofInversions4(long n, long[] a)
        {
            //write your code here
            return 0;
        }

        public static string ProcessNumberofInversions4(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)NumberofInversions4);

        public static long[] OrganizingLottery5(long[] points, long[] startSegments,
            long[] endSegment)
        {
            //write your code here
            return new long[] { 0 };
        }

        public static string ProcessOrganizingLottery5(string inStr) =>
            TestTools.Process(inStr,OrganizingLottery5);

        public static double ClosestPoints6(long n, long[] xPoints, long[] yPoints)
        {
            //write your code here
            return 0;
        }

        public static string ProcessClosestPoints6(string inStr) =>
           TestTools.Process(inStr, (Func<long, long[], long[], double>)
               ClosestPoints6);

    }
}
