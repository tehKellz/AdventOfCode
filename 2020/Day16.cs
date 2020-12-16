using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day16 : CodeTest
{
    public bool Enabled => true;

    public List<int> MyTicket = new List<int>()
    {151,103,173,199,211,107,167,59,113,179,53,197,83,163,101,149,109,79,181,73};

    public List<List<int>> Tickets = new List<List<int>>();

    public struct Rule
    {
        public Rule(string name, int a, int b, int c, int d) {
            Name=name; A=a; B=b; C=c; D=d;
            PossibleIndex = new List<int>();
            Completed = false;
        }
        public string Name;
        public int A,B,C,D;
        public override string ToString()
        {
            return $"'{Name}' = {A}:{B} {C}:{D} [[{string.Join(",",PossibleIndex)}]]";
        }

        public bool IsValid(int num)
        {
            return ( (num <= B) && (num >= A)  ) || ( (num <= D) && (num >= C)  );
        }

        public List<int> PossibleIndex;
        public bool Completed;
    }

    public List<Rule> Rules = new List<Rule>();
    public void Init() 
    {
        Utils.LoadCSV("2020/Day16.tickets", Tickets);
        Console.WriteLine($"{Tickets[24][5]}");

        Utils.Load<string>("2020/Day16.rules", (s,n)=>
        {
            //zone: 44-451 or 464-955
            string[] first = s.Split(": ");

            string[] parts = first[1].Split(new char[]{' ','-'});
            Rule rule = new Rule(first[0],
                Int32.Parse(parts[0]),Int32.Parse(parts[1]),
                Int32.Parse(parts[3]),Int32.Parse(parts[4]));
            //Console.WriteLine($"Rule {rule}");
            Rules.Add(rule);
        });
    }

    public List<List<int>> ValidTickets = new List<List<int>>();

    public string RunA()
    {
        Int64 errorSum = 0;
        foreach(var t in Tickets)
        {
            bool validTicket = true;
            foreach(var n in t)
            {
                bool valid = false;
                foreach(var r in Rules)
                {
                    if (r.IsValid(n))
                    {
                        valid = true;
                        break;
                    }
                }

                if (!valid)
                {
                    errorSum += n;
                    validTicket = false;
                }
            }

            if (validTicket)
            {
                ValidTickets.Add(t);
            }
        }

        return $"{errorSum}";
    }

    public string RunB()
    {
        Console.WriteLine($"There are {ValidTickets.Count} valid tickets");

        // Seed the possible positions on each rule from my ticket
        //for(int i=0;i<MyTicket.Count;++i)
        for(int i=0;i<Tickets[0].Count;++i)
        {
            foreach(var r in Rules)
            {
                if (r.IsValid(Tickets[0][i])) r.PossibleIndex.Add(i);
            }
        }

foreach(var r in Rules) Console.WriteLine($"{r}");
Console.WriteLine("=======");
        // now eliminate
        foreach(var t in ValidTickets)
        {
            for(int i=0;i<t.Count;++i)
            {
                foreach(var r in Rules)
                {
                    if (!r.IsValid(t[i])) 
                    {
                        r.PossibleIndex.Remove(i);
                    }
                }
            }
        }

foreach(var r in Rules) Console.WriteLine($"{r}");
Console.WriteLine("=======");

        List<Rule> CRS = new List<Rule>();
        for(int k=0; k<20;++k)
        // while(CRS.Count != Rules.Count)
        {
            for(int i=0; i<Rules.Count; ++i)
            {
                if (Rules[i].Completed != true && Rules[i].PossibleIndex.Count == 1)
                {
                    //Console.WriteLine($"  Rule Complete: {Rules[i]} !! {CRS.Count} != {Rules.Count}");
                    CRS.Add(Rules[i]);
                    for(int j=0; j<Rules.Count; ++j) 
                    {
                        if (j != i && !Rules[j].Completed)
                        {
                            //Console.WriteLine($"    Cleaning up [{i}] from {Rules[j]}");
                            Rules[j].PossibleIndex.Remove(Rules[i].PossibleIndex[0]);
                            //Console.WriteLine($"      Cleaned up {Rules[j]}");
                        }
                    }
                    //Rules[i].Completed = true;
                }
            }

            //foreach(var r in Rules) Console.WriteLine($"{r}");
            //Console.WriteLine("=======");
        }

        foreach(var r in Rules) Console.WriteLine($"{r}");

        string eq = "";
        Int64 magic = 1;
        foreach(var r in Rules)
        {
            if (r.Name.StartsWith("departure"))
            {
                eq += $"* {MyTicket[r.PossibleIndex[0]]}";
                magic *= MyTicket[r.PossibleIndex[0]];
            }
        }
        return $"<{magic}> = {eq}";
    }
}
}