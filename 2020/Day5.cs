using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day5 : CodeTest
{
    public bool Enabled => false;
    List<int> Seats = new List<int>();
    public void Init() 
    {
        Utils.Load<string>("2020/Day5.input", (s) => {
            Seats.Add(FindSeat(s));
        });
        Seats.Sort();
    }

    int FindSeat(string pass)
    {
        int seat = 0;
        for(int j=0; j<10;++j)
        {
            if (pass[j] == 'B' || pass[j] == 'R')
                seat |= (1 << (9 - j));
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
            if (Seats[i]+1 != Seats[i+1])
                return (Seats[i]+1).ToString();
        }
        return "No open seats";
    }
}
}