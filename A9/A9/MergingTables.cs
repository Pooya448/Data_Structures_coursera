using System;
using TestCommon;
using System.Collections.Generic;
using System.Linq;

namespace A9
{
    class Table
    {
        public int Link { get; set; }
        public int Size { get; set; }
        public int Id { get; set; }
        public Table(long link, long size, long id)
        {
            Link = (int)link;
            Size = (int)size;
            Id = (int)id;
        }
    }
    public class MergingTables : Processor
    {
        public MergingTables(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long[], long[]>)Solve);

        public long[] Solve(long[] tableSizes, long[] sourceTables, long[] targetTables)
        {
            long n = sourceTables.Length;
            long[] results = new long[n];
            List<Table> tables = new List<Table>();
            tables.Add(null);

            for (int i = 1; i <= tableSizes.Length; i++)
                tables.Add(new Table(i,tableSizes[i-1],i));

            long maxSize = tableSizes.Max();
            for (int j = 0; j < n; j++)
            {
                var t = (int)targetTables[j];
                var s = (int)sourceTables[j];

                while (tables[t].Link != tables[t].Id)
                    t = tables[t].Link;
                while (tables[s].Link != tables[s].Id)
                    s = tables[s].Link;
                if (s != t)
                {
                    tables[t].Size += tables[s].Size;
                    tables[s].Link = t;
                    tables[s].Size = 0;
                }
                maxSize = Math.Max(tables[t].Size, maxSize);
                results[j] = maxSize;
            }
            return results.ToArray();
        }
    }
}