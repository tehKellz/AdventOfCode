using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day5 : CodeTest
{
    public bool Enabled => true;
    List<int> Seats = new List<int>();
    public void Init() 
    {
        Utils.Load<string>("2020/Day5.input", (s, n) => {
            Seats.Add(FindSeat(s));
            return false;
        });
        Seats.Sort();
    }

    int FindSeat(string pass)
    {
        int seat = 0;
        for(int j=0; j<pass.Length;++j)
        {
            if (pass[j] == 'B' || pass[j] == 'R')
            {
                seat |= (1 << (pass.Length - j - 1));
            }
        }
        return seat;
    }

    public string RunA()
    {
        return Seats[Seats.Count - 1].ToString();
    }

    public string RunB()
    {
        for(int i=0; i< Seats.Count - 2; i++)
        {
            int next = Seats[i]+1;
            if (next != Seats[i+1])
            {
                return next.ToString();
            }
        }
        return "Output B";
    }
}
}