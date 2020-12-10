using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
class Day10 : CodeTest
{
    public bool Enabled => true;

    List<int> Adapters = new List<int>();
    public void Init() 
    {
        Utils.Load("2020/Day10.input", Adapters);
        Adapters.Sort();
    }

    public string RunA()
    {
        int prev = 0;
        int ones = 0;
        int threes = 0;
        foreach(int a in Adapters)
        {
            if (a - prev == 1) ++ones;
            else if (a - prev == 3) ++threes;
            else Console.WriteLine("Non one or three!!");
            prev = a;
        }
        threes++;
        return $"{ones * threes}";
    }

    Int64[] Paths;
    public string RunB()
    {
        Adapters.Insert(0,0);
        Paths = new Int64[Adapters.Count];
        Paths[0] = 1;
        for(int i=0; i<Adapters.Count;++i)
        {
            if (i+1 < Adapters.Count && Adapters[i] + 3 >= Adapters[i+1]) Paths[i+1] += Paths[i];
            if (i+2 < Adapters.Count && Adapters[i] + 3 >= Adapters[i+2]) Paths[i+2] += Paths[i];
            if (i+3 < Adapters.Count && Adapters[i] + 3 >= Adapters[i+3]) Paths[i+3] += Paths[i];
        }
        
        Int64 total = Paths.Last();
        return $"{total}";
    }
}
}