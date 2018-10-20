using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }

        public static string ProcessChangingMoney1(string inStr)
            => TestTools.Process(inStr, (Func<long, long>)ChangingMoney1);

        public static string ProcessMaximizingLoot2(string inStr)
            => TestTools.Process(inStr,
                (Func<long, long[], long[], long>)MaximizingLoot2);

        public static string ProcessMaximizingOnlineAdRevenue3(string inStr)
            => TestTools.Process(inStr,
                (Func<long, long[], long[], long>)MaximizingOnlineAdRevenue3);

        public static string ProcessCollectingSignatures4(string inStr)
            => TestTools.Process(inStr,
                (Func<long, long[], long[], long>)CollectingSignatures4);

        public static string ProcessMaximizeNumberOfPrizePlaces5(string inStr)
            => TestTools.Process(inStr,
                (Func<long, long[]>)MaximizeNumberOfPrizePlaces5);

        public static string ProcessMaximizeSalary6(string inStr)
            => TestTools.Process(inStr,MaximizeSalary6);

        public static long ChangingMoney1(long money)
        {
            return (money / 10) + (money % 10) / 5 + (money % 5);
        }

        public static long MaximizingLoot2(long capacity, long[] weights, long[] values)
        {
            
            var densities = values
                .Zip(weights, (v, w) => new { Density = (double)v / w, Weight = w })
                .OrderByDescending(x => x.Density).ToList();
            double totalValue = 0;
            foreach(var density in densities)
            {
                if (density.Weight <= capacity)
                {
                    totalValue += (density.Weight*density.Density);
                    capacity -= (long)density.Weight;
                    continue;
                } else if (density.Weight > capacity)
                {
                    totalValue += (capacity * density.Density);
                    capacity = 0;
                }
            }
            return (long)totalValue;

        }

        public static long MaximizingOnlineAdRevenue3(long slotCount, long[] adRevenue, long[] averageDailyClick) 
            =>   adRevenue.OrderByDescending(x => x)
                .Zip(averageDailyClick.OrderByDescending(x => x), (r, c) => new { Venue = r*c })
                .Select(x => x.Venue)
                .Sum();


        private static long CollectingSignatures4(long tenantCount, long[] startTimes, long[] endTimes)
        {

            var NList = startTimes
                .Zip(endTimes, (s, e) => new { Start = s, End = e, Range = e - s, Group = (long)0 })
                .OrderBy(x => x.End)
                .ToList();

            var Point = NList[0];
            List<dynamic> CommonPoints = new List<dynamic>();
            CommonPoints.Add(Point);

            for (int i = 1; i < NList.Count; i++)
            {
                if (Point.End < NList[i].Start || Point.End > NList[i].End)
                {
                    CommonPoints.Add(Point);
                    Point = NList[i];
                }
            }
            return CommonPoints.Count;   
        }

        public static long[] MaximizeNumberOfPrizePlaces5(long n)
        {
            List<long> Prizes = new List<long>();
            for (int i = 1; n-i >= 0 ; i++)
            {
                n -= i;
                Prizes.Add(i);
            }
            Prizes[Prizes.Count - 1] += n;
            return Prizes.ToArray();
        }

        private static string MaximizeSalary6(long n,long[] numbers)
        {
            for (int i = 0; i < n-1; i++)
            {
                for (int j = i+1; j < n; j++)
                {
                    if (Compare6(numbers[i],numbers[j]))
                    {
                        long temp = numbers[i];
                        numbers[i] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
            string result = string.Empty;
            foreach (var item in numbers)
            {
                result += item;
            }
            return result;
        }

        private static bool Compare6 (long no1,long no2)
        {
            string no1no2 = no1.ToString() + no2.ToString();
            string no2no1 = no2.ToString() + no1.ToString();
            if (long.Parse(no1no2) >= long.Parse(no2no1))
            {
                return false;
            } else { return true; }

        }

    



    }
}
