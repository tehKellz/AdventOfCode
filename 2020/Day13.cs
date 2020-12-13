using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day13 : CodeTest
{
    public bool Enabled => true;

    int now = 0;
    List<int> Times = new List<int>();
    public void Init() 
    {
        Utils.Load<string>("2020/Day13.input", (s,n) =>
        {
            if (n==0) now = Int32.Parse(s);
            else
            {
                var split = s.Split(",");
                foreach(var e in split)
                {
                    if (e == "x") Times.Add(-1);
                    else Times.Add(Int32.Parse(e));
                }
            }
        });
    }

    public Int64 NextBus(Int64 now, Int64 busId)
    {
        return busId * ((now / busId) + 1);
    }

    public string RunA()
    {
        int bus = 0;
        int depart = Int32.MaxValue;
        foreach(int b in Times)
        {
            if (b == -1) continue;
            
            Int64 d = b * ((now / b) + 1);
            if (d < depart)
            {
                depart = (Int32)d;
                bus = b;
            }
        }

        return $"It is {now}, next bus is {bus} at {depart}, magic: {(depart - now)*bus}";
    }

    public string RunB()
    {
        return $"Learn Chinese Remainder Therem";
    }
}
}