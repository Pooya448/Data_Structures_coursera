using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3
{
    public class Program
    {
        static void Main(string[] args)
        {
            
        }

        public static string Process(string inStr, Func<long,long> longProcessor)
        {
            long n = long.Parse(inStr);
            return longProcessor(n).ToString();
        }

        public static string Process(string inStr, Func<long, long, long> longProcessor)
        {
            var toks = inStr.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            long a = long.Parse(toks[0]);
            long b = long.Parse(toks[1]);
            return longProcessor(a, b).ToString();
        }

        public static string ProcessFibonacci(string inStr)
            => Process(inStr, Fibonacci);

        public static string ProcessFibonacci_LastDigit(string inStr)
            => Process(inStr, Fibonacci_LastDigit);

        public static string ProcessGCD(string inStr)
            => Process(inStr, GCD);

        public static string ProcessLCM(string inStr)
            => Process(inStr, LCM);

        public static string ProcessFibonacci_Mod(string inStr)
            => Process(inStr, Fibonacci_Mod);

        public static string ProcessFibonacci_Sum(string inStr)
            => Process(inStr, Fibonacci_Sum);

        public static string ProcessFibonacci_Partial_Sum(string inStr)
            => Process(inStr, Fibonacci_Partial_Sum);

        public static string ProcessFibonacci_Sum_Squares(string inStr)
            => Process(inStr, Fibonacci_Sum_Squares);

        public static long GCD(long small,long big)
        {
            
            while(small != 0)
            {
                long temp = small;
                small = big % small;
                big = temp;

            }
            return big;
        }

        public static long LCM(long small, long big)
        {

            return (small * big) / GCD(small, big);
        }

        public static long Fibonacci_LastDigit (long n)
        {
            List<long> modList = new List<long>() { 0, 1 };
            long element1 = 0;
            long element2 = 1;

            if (n == 0)
            {
                return 0;
            }
            for (int i = 2; i < int.MaxValue; i++)
            {
                long temp = element2 % 10;
                element2 = element1 % 10 + temp;
                if (element2 >= 10)
                {
                    element2 = element2 % 10;
                }
                modList.Add(element2);
                element1 = temp;
                if (modList[i] == 1 && modList[i - 1] == 0)
                {
                    break;
                }
            }
            long PeriodLength = modList.Count - 2;
            return modList[(int)(n % PeriodLength)];
        }

        public static long Fibonacci(long n)
        {
            long element1 = 1;
            long element2 = 1;
            if (n == 0)
            {
                return 0;
            }
            for (int i = 0; i < n-2; i++)
            {
                long temp = element2;
                element2 = element1 + temp;
                element1 = temp;
            }
            return element2;
        }

        public static long Fibonacci_Mod(long n, long m)
        {
            List<long> modList = new List<long>() { 0, 1 };
            long element1 = 0;
            long element2 = 1;

            if (n == 0)
            {
                return 0;
            }
            for (int i = 2; i < int.MaxValue; i++)
            {
                long temp = element2 % m;
                element2 = element1 % m + temp;
                if (element2 >= m)
                {
                    element2 = element2 % m;
                }
                modList.Add(element2);
                element1 = temp;
                if (modList[i] == 1 && modList[i - 1] == 0)
                {
                    break;
                }
            }
            long PeriodLength = modList.Count - 2;
            return modList[(int)(n % PeriodLength)];
        }

        public static long Fibonacci_Sum(long n)
        {
            List<long> modList = new List<long>() { 0, 1 };
            long element1 = 0;
            long element2 = 1;

            if (n == 0)
            {
                return 0;
            }
            for (int i = 2; i < int.MaxValue; i++)
            {
                long temp = element2 % 10;
                element2 = element1 % 10 + temp;
                if (element2 >= 10)
                {
                    element2 = element2 % 10;
                }
                modList.Add(element2);
                element1 = temp;
                if (modList[i] == 1 && modList[i - 1] == 0)
                {
                    modList.RemoveAt(i);
                    modList.RemoveAt(i-1);
                    modList.RemoveAt(0);
                    break;
                }
            }
            long Rm = modList.Sum() % 10;
            long Remainded_Index = (n % 60);
            long Periods = (n / 60);
            long Remainder = modList.Take((int)Remainded_Index).Sum();
            long Res = ((Rm * Periods) + Remainder) % 10;
            return Res;
        }

        public static long Fibonacci_Partial_Sum(long n,long m)
        {
            long Big = n > m ? n : m;
            long Small = n < m ? n : m;
            int digit = (int)(Fibonacci_Sum(Big) - Fibonacci_Sum(Small-1));
            if (digit < 0)
                digit += 10;
            return digit;
        }

        public static long Fibonacci_Sum_Squares(long n)
        {
            long Width = Fibonacci_Mod(n,10);
            long Length = (Fibonacci_Mod(n - 1,10) + Width) % 10;
            return (Width * Length) % 10;
        }

        public static void Print (long n)
        {
            Console.WriteLine(n.ToString());
        }

        

    }
}
