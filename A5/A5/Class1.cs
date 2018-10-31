using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5
{
    class Class1
    {
        public static void sort(long[] input)
        {
            sort(input, 0, input.Length - 1);
        }

        private static void sort(long[] input, int lowIndex, int highIndex)
        {
            if (highIndex <= lowIndex)
                return;

            int partIndex = partition(input, lowIndex, highIndex);

            sort(input, lowIndex, partIndex - 1);

            sort(input, partIndex + 1, highIndex);
        }



        private static int partition(long[] input, int lowIndex, int highIndex)
        {
            int i = lowIndex;

            int pivotIndex = lowIndex;

            int j = highIndex + 1;



            while (true)
            {
                while (input[++i] < input[pivotIndex])
                    if (i == highIndex)
                        break;




                while (input[pivotIndex] < input[--j])
                    if (j == lowIndex)
                        break;

                if (i >= j)
                    break;

                (input[i], input[j]) = (input[j], input[i]);

            }

            (input[pivotIndex], input[j]) = (input[j], input[pivotIndex]);

            return j;

        }


        public static long[] ImprovingQuickSort3(long n, long[] a)
        {
            sort(a);
            return a;
        }
    }
}
