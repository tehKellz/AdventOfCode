using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day19 : CodeTest
{
    public bool Enabled => false;

    class Rule
    {
        public int[] Left = null;
        public int[] Right = null;
        public string C = "";
    }

    Dictionary<int,Rule> Rules = new Dictionary<int,Rule>();

    List<string> Messages = new List<string>();
    public void Init() 
    {
        Utils.Load("2020/Day19.messages", Messages);

        Utils.Load<string>("2020/Day19.rules", (s,n)=>
        {
            // 100: 18 123 | 47 12
            // 47: "a"
            var ss = s.Split(": ");
            int r = Int32.Parse(ss[0]);
            var rr = ss[1].Split(new Char[]{' ','"','|'});
            Rule nr = new Rule();
            if (rr.Length == 3) // {"","a",""}
            {
                nr.C = rr[1];
            }
            else if (rr.Length == 2) // {8,123}
            {
                nr.Left = new int[2];
                nr.Left[0] = Int32.Parse(rr[0]);
                nr.Left[1] = Int32.Parse(rr[1]);
            }
            else if (rr.Length == 6) // {8,123,"","",47,12}
            {
                nr.Left = new int[2];
                nr.Left[0] = Int32.Parse(rr[0]);
                nr.Left[1] = Int32.Parse(rr[1]);

                nr.Right = new int[2];
                nr.Right[0] = Int32.Parse(rr[4]);
                nr.Right[1] = Int32.Parse(rr[5]);
            }
            Rules[r] = nr;
        });
    }

    bool Matches(string message, Rule rule)
    {
        if (rule.C != "")
        {
            return message == rule.C;
        }
        return false;

    }

    public string RunA()
    {
        return $"";
    }

    public string RunB()
    {
        return "";
    }
}
}