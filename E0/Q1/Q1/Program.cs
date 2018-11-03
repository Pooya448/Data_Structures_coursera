using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class Program
    {
        private static Dictionary<int, char[]> D =
            new Dictionary<int, char[]>
            {
                [0] = new char[] { '+' },
                [1] = new char[] { '_', ',', '@' },
                [2] = new char[] { 'A', 'B', 'C' },
                [3] = new char[] { 'D', 'E', 'F' },
                [4] = new char[] { 'G', 'H', 'I' },
                [5] = new char[] { 'J', 'K', 'L' },
                [6] = new char[] { 'M', 'N', 'O' },
                [7] = new char[] { 'P', 'Q', 'R', 'S' },
                [8] = new char[] { 'T', 'U', 'V' },
                [9] = new char[] { 'W', 'X', 'Y', 'Z' },
            };


        public static string[] GetNames(int[] phone)
        {
            List<string> FinalResult = new List<string>();
            var res = Recursion(phone, 0, phone.Length - 1); 
            return res.ToArray();
        }

        public static List<string> Recursion(int[] phone,int low,int high)
        {
            if (high <= low)
            {
                List<string> partRes = new List<string>();
                for (int i = 0; i < D[phone[low]].Length; i++)
                {
                    partRes.Add(D[phone[low]][i].ToString());
                }
                return partRes;
            }
            int mid = (low + high) / 2;
            var right = Recursion(phone, low, mid);
            var left = Recursion(phone, mid+1, high);
            List<string> fRes = new List<string>();
            for (int i = 0; i < right.Count; i++)
            {
                for (int j = 0; j < left.Count; j++)
                {
                    fRes.Add($"{right[i]}{left[j]}");
                }
            }
            return fRes;
        }

        static void Main(string[] args)
        {
            int[] phoneNumber = new int[] {0, 9, 1, 2, 2, 2, 4, 2, 5, 2, 5 };

            // چاپ یک رشته حرفی برای شماره تلفن
            for (int i=0; i< phoneNumber.Length; i++)
                Console.Write(D[phoneNumber[i]][0]);
            Console.WriteLine();
        }


    }
}
