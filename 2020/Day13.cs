using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day13 : CodeTest
{
    public bool Enabled => false;

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

    public struct Bus
    {
        public Bus(int b, int o) { BusId = b; Offset = o; }
        public int BusId;
        public int Offset;
    }

    List<Bus> Busses = new List<Bus>();
    public string RunB()
    {
        for(int i=0; i<Times.Count; ++i)
        {
            if (Times[i] >= 0) Busses.Add(new Bus(Times[i], i));
        }

        Int64 t = 0;
        Int64 inc = Busses[0].BusId;
        for(int i=1; i< Busses.Count; ++i)
        {
            Bus b = Busses[i];
            while ((t + b.Offset) % b.BusId != 0) t += inc;
            inc *= b.BusId;
        }

        return $"{t}";
    }
}
}