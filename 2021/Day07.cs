using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
class Day07_21 : CodeTest
{
    public string TestName = "2021/Day07";
    public bool Enabled => false;
    
    private List<int> Data = new List<int>();
    
    public void Init() 
    {
        Utils.Load($"{TestName}.input", (string l) =>
        {
            var ls = l.Split(',');
            foreach(var entry in ls) Data.Add(int.Parse(entry));
            Data.Sort();
            return false;
        });
    }

    public string RunA()
    {
        int median = Data[Data.Count / 2];

        int fuel = 0;
        foreach (var d in Data)
        {
            int dist = Math.Abs(d - median);
            fuel += dist;
        }
        return $"Median: {median}, Fuel: {fuel}";
    }

    public string RunB()
    {
        double mean = Math.Floor(Data.Average());

        int fuel = 0;
        foreach (var d in Data)
        {
            int dist = Math.Abs(d - (int)mean);
            for(int i=1; i <= dist; ++i) fuel += i;
        }

        return $"Mean: {mean}, Fuel: {fuel}";
    }
}
}