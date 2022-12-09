using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day02: CodeTest
{
    public string TestName = "2022/Day02";
    public bool Enabled => true;

    public class RPSRound
    {
        public int theirs = 0;
        public int mine = 0;
        public int outcome = 0;
        public int score = 0;
    }
    private List<RPSRound> Data = new List<RPSRound>();
    
    public void Init() 
    {
        Utils.Load<string>($"{TestName}.input", (string l, int n) =>
        {
            RPSRound r = new RPSRound();
            string debug = "";
            switch(l[0])
            {
            case 'A': r.theirs = 1; debug = "ROCK"; break;
            case 'B': r.theirs = 2; debug = "PAPER"; break;
            case 'C': r.theirs = 3; debug = "SCISSORS"; break;
            }

            switch(l[2])
            {
            case 'X': r.mine = 1; debug += " vs. ROCK"; break;
            case 'Y': r.mine = 2; debug += " vs. PAPER"; break;
            case 'Z': r.mine = 3; debug += " vs. SCISSORS"; break;
            }
            
            if (r.theirs == r.mine)
            {
                r.outcome = 3;
                debug += " DRAW";
            }
            else if ((r.theirs == 3 && r.mine == 1) || (r.theirs + 1 == r.mine))
            {
                r.outcome = 6;
                debug += " WIN";
            }
            else
            {
                debug += " LOSS";
            }
            r.score = r.outcome + r.mine;
            //Console.WriteLine(debug);
            Data.Add(r);
        });
    }

    public string RunA()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");
        Int64 total = 0;
        foreach(var round in Data)
        {
            total += round.score;
        }
        return $"Output A {total}";
    }

    public string RunB()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");
        Data = new List<RPSRound>();
        Int64 total = 0;
        Utils.Load<string>($"{TestName}.input", (string l, int n) =>
        {
            RPSRound r = new RPSRound();
            string debug = "";
            switch(l[0])
            {
            case 'A': r.theirs = 1; debug = "ROCK"; break;
            case 'B': r.theirs = 2; debug = "PAPER"; break;
            case 'C': r.theirs = 3; debug = "SCISSORS"; break;
            }

            switch(l[2])
            {
            case 'X': 
            {
                r.outcome = 0;
                r.mine = r.theirs - 1;
                if (r.mine == 0) r.mine = 3;
                break;
            }
            case 'Y': r.mine = r.theirs; r.outcome = 3; break;
            case 'Z': 
            {
                r.outcome = 6;
                r.mine = r.theirs + 1;
                if (r.mine == 4) r.mine = 1;
                break;
            }
            }
            
            r.score = r.outcome + r.mine;
            total += r.score;
            //Console.WriteLine(debug);
            Data.Add(r);
        });
        return $"Output B {total}";
    }
}
}