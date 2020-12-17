using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day16 : CodeTest
{
    public bool Enabled => false;

    public List<int> MyTicket = new List<int>()
    {151,103,173,199,211,107,167,59,113,179,53,197,83,163,101,149,109,79,181,73};

    public List<List<int>> Tickets = new List<List<int>>();
    public List<List<int>> ValidTickets = new List<List<int>>();

    public struct Rule
    {
        public Rule(string name, int a, int b, int c, int d) 
        {
            Name=name; A=a; B=b; C=c; D=d;
            PossibleIndex = new List<int>();
        }

        public string Name;
        public int A,B,C,D;
        public List<int> PossibleIndex;

        public bool IsValid(int num)
        {
            return ((num>=A)&&(num<=B))||((num>=C)&&(num<=D));
        }
    }
    public List<Rule> Rules = new List<Rule>();

    public void Init() 
    {
        Utils.LoadCSV("2020/Day16.tickets", Tickets);
        Utils.Load<string>("2020/Day16.rules", (s,n)=>
        {
            string[] first = s.Split(": ");
            string[] parts = first[1].Split(new char[]{' ','-'});
            Rules.Add(new Rule(first[0],
                Int32.Parse(parts[0]),Int32.Parse(parts[1]),
                Int32.Parse(parts[3]),Int32.Parse(parts[4])));
        });
    }

    public string RunA()
    {
        Int64 errorSum = 0;
        foreach(var t in Tickets)
        {
            bool validTicket = true; // each ticket starts valid until any invalid field is found
            foreach(var n in t)
            {
                bool valid = false; // each field starts as invalid until any valid Rule is found
                foreach(var r in Rules)
                    if (r.IsValid(n)) valid = true;

                if (!valid)
                {
                    errorSum += n;
                    validTicket = false;
                }
            }

            if (validTicket) ValidTickets.Add(t);
        }

        return $"{errorSum}";
    }

    public string RunB()
    {
        // seed from first nearby
        for(int i=0;i<Tickets[0].Count;++i)
            foreach(var r in Rules)
                if (r.IsValid(Tickets[0][i])) r.PossibleIndex.Add(i);

        // eliminate based on invalid values
        foreach(var t in ValidTickets)
            for(int i=0;i<t.Count;++i)
                foreach(var r in Rules)
                    if (!r.IsValid(t[i])) 
                         r.PossibleIndex.Remove(i);

        // eliminate based on narrowed fields
        for(int k=0; k<Rules.Count;++k)
            for(int i=0; i<Rules.Count; ++i)
                if (Rules[i].PossibleIndex.Count == 1)
                    for(int j=0; j<Rules.Count; ++j) 
                        if (j != i)
                            Rules[j].PossibleIndex.Remove(Rules[i].PossibleIndex[0]);

        Int64 magic = 1;
        foreach(var r in Rules)
            if (r.Name.StartsWith("departure"))
                magic *= MyTicket[r.PossibleIndex[0]];

        return $"{magic}";
    }
}
}