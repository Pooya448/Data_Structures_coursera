using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int N = 50;
            int M = 10000;
            while (true)
            {
                Random nrand = new Random();
                int n = nrand.Next(2, N);
                int[] Arr = new int[n];
                for (int i = 0; i < n; i++)
                {
                    Arr[i] = nrand.Next(0, M);
                }
                foreach (var item in Arr)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
                int resNaive = Program.NaiveMaxPairewiseProduct(Arr.ToList());
                int resFast = Program.FastMaxPairewiseProduct(Arr.ToList());
                if (resFast == resNaive)
                {
                    Console.WriteLine("OK");
                }
                else
                {
                    Console.WriteLine($"Wrong, {resNaive}, {resFast}");
                    break;
                }
            }
            Console.ReadKey();
        }

        public static int NaiveMaxPairewiseProduct(List<int> numbers)
        {
            int product = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    int temp = numbers[i] * numbers[j];
                    product = temp < product ? product : temp;
                }
            }
            return product;
        }

        public static int FastMaxPairewiseProduct(List<int> numbers)
        {
            int index1 = 0;
            for (int i = index1; i < numbers.Count; i++)
            {
                index1 = numbers[i] >= numbers[index1] ? i : index1;
            }
            int index2 = 0;
            if (index1 == 0)
            {
                index2 = 1;
            }
            for (int i = index2; i < numbers.Count; i++)
            {
                index2 = (numbers[i] >= numbers[index2] && i != index1) ? i : index2;
            }
            return numbers[index1] * numbers[index2];
        }

        public static string Process(string input)
        {
            var inData = input.Split(new char[] { '\n', '\r', ' ' },
                StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s)).ToList();

            return FastMaxPairewiseProduct(inData).ToString();
        }
    }
}
