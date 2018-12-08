using System;
using System.Linq;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Contact
    {
        public string Name;
        public int Number;

        public Contact(string name, int number)
        {
            Name = name;
            Number = number;
        }
    }

    public class PhoneBook : Processor
    {
        public PhoneBook(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string[]>)Solve);

        protected List<Contact>[] PhoneBookList;

        public string[] Solve(string [] commands)
        {
            PhoneBookList = new List<Contact>[1000];
            for (int i = 0; i < PhoneBookList.Length; i++)
                PhoneBookList[i] = new List<Contact>();
            List<string> result = new List<string>();
            foreach(var cmd in commands)
            {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var args = toks.Skip(1).ToArray();
                int number = int.Parse(args[0]);
                int hashed = (int)Hash(number);
                switch (cmdType)
                {
                    case "add":
                        Add(args[1], number,hashed);
                        break;
                    case "del":
                        Delete(number, hashed);
                        break;
                    case "find":
                        result.Add(Find(number, hashed));
                        break;
                }
            }
            return result.ToArray();
        }

        public long Hash (int number)
        {
            long a = 27, b = 3;
            long p = 10000019;
            return (a * number + b) % p % 1000;
        }

        public void Add(string name, int number, int hash)
        {
            for (int i = 0; i < PhoneBookList[hash].Count; i++)
                if (PhoneBookList[hash][i].Number == number)
                {
                    PhoneBookList[hash][i].Name = name;
                    return;
                }
            PhoneBookList[hash].Add(new Contact(name,number));
        }

        public string Find(int number, int hash)
        {
            for (int i = 0; i < PhoneBookList[hash].Count; i++)
                if (PhoneBookList[hash][i].Number == number)
                    return PhoneBookList[hash][i].Name;
            return "not found";
        }

        public void Delete(int number, int hash)
        {
            for (int i = 0; i < PhoneBookList[hash].Count; i++)
                if (PhoneBookList[hash][i].Number == number)
                {
                    PhoneBookList[hash].RemoveAt(i);
                    return;
                }
            return;
        }
    }
}
