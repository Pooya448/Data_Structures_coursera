using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class CheckBrackets : Processor
    {
        public CheckBrackets(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, long>)Solve);

        public long Solve(string str)
        {   
            Dictionary<char, char> braces = new Dictionary<char, char>()
            {
                {'(',')'},
                {'[',']'},
                {'{','}'}
            };
            Stack<(char,int)> bracesStack = new Stack<(char, int)>();
            for (int i = 0; i < str.Length; i++)
                if (braces.Keys.ToList().Contains(str[i]))
                    bracesStack.Push((str[i], i));
                else if (braces.Values.ToList().Contains(str[i]))
                    if (bracesStack.Count == 0)
                        return i + 1;
                    else if (str[i] != braces[bracesStack.Pop().Item1])                        
                        return i + 1;
            if (bracesStack.Count > 0)
                return bracesStack.Pop().Item2 + 1;
            else
                return -1;
        }
    }
}
