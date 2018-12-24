﻿using System;
using System.Collections.Generic;
using TestCommon;

namespace A11
{
    
    public class SetWithRageSums : Processor
    {
        public SetWithRageSums(string testDataName) : base(testDataName)
        {
            CommandDict =
                        new Dictionary<char, Func<string, SplayTree, string>>()
                        {
                            ['+'] = Add,
                            ['-'] = Del,
                            ['?'] = Find,
                            ['s'] = Sum
                        };
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string[]>)Solve);

        public readonly Dictionary<char, Func<string, SplayTree, string>> CommandDict;

        protected const long M = 1_000_000_001;

        protected long X = 0;

        protected List<long> Data;

        public string[] Solve(string[] lines)
        {
            SplayTree tree = new SplayTree();
            X = 0;
            Data = new List<long>();
            List<string> result = new List<string>();
            foreach(var line in lines)
            {
                char cmd = line[0];
                string args = line.Substring(1).Trim();
                var output = CommandDict[cmd](args,tree);
                if (null != output)
                    result.Add(output);
            }
            return result.ToArray();
        }

        private long Convert(long i)
            => i = (i + X) % M;       

        private string Add(string arg, SplayTree tree)
        {
            long i = Convert(long.Parse(arg));
            tree.Insert(i);
            return null;
        }

        private string Del(string arg, SplayTree tree)
        {
            long i = Convert(long.Parse(arg));
            tree.Delete(i);
            //int idx = Data.BinarySearch(i);
            //if (idx >= 0)
            //    Data.RemoveAt(idx);

            return null;
        }

        private string Find(string arg, SplayTree tree)
        {
            long i = Convert(int.Parse(arg));
            var temp = tree.Find(i);
            if (temp == null)
            {
                return "Not found";
            }
            if (temp.Key == i)
            {
                return "Found";
            }
            else
                return "Not found";
            //int idx = Data.BinarySearch(i);
            //return idx < 0 ?
            //    "Not found" : "Found";
        }

        private string Sum(string arg, SplayTree tree)
        {
            var toks = arg.Split();
            long l = Convert(long.Parse(toks[0]));
            long r = Convert(long.Parse(toks[1]));

            l = Data.BinarySearch(l);
            if (l < 0)
                l = ~l;

            r = Data.BinarySearch(r);
            if (r < 0)
                r = (~r -1); 
            // If not ~r will point to a position with
            // a larger number. So we should not include 
            // that position in our search.

            long sum = 0;
            for (int i = (int)l; i <= r && i < Data.Count; i++)
                sum += Data[i];

            X = sum;

            return sum.ToString();
        }
    }
}
