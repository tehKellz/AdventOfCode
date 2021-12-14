using System;
using System.Collections.Generic;

namespace AdventOfCode
{
static class Extensions
{
    public static void AddValue<T>(this Dictionary<T,Int64> self, T key, Int64 value)
    {
        if (!self.ContainsKey(key)) self[key] = value;
        else self[key] += value;
    }
}

class Day14_21 : CodeTest
{
    public string TestName = "2021/Day14";
    public bool Enabled => true;
    
    private string Poly = "BCHCKFFHSKPBSNVVKVSK";
    private Dictionary<string,string> Rules = new Dictionary<string,string>();
    
    public void Init() 
    {
        Utils.Load($"{TestName}.input", (string s, int n) =>
        {
            var sl = s.Split(" -> ");
            Rules[sl[0]] = sl[1];
        });
    }

    public Dictionary<string,Int64> ApplyRules(Dictionary<string,Int64> pairs)
    {
        Dictionary<string,Int64> newPairs = new Dictionary<string,Int64>();
        foreach(var key in pairs.Keys)
        {
            if (Rules.ContainsKey(key))
            {
                newPairs.AddValue($"{key[0]}{Rules[key]}", pairs[key]);
                newPairs.AddValue($"{Rules[key]}{key[1]}", pairs[key]);
            }
            else newPairs[key] = pairs[key];
        }
        return newPairs;
    }

    public Int64 Score(Dictionary<string,Int64> pairs)
    {
        Dictionary<char,Int64> counts = new Dictionary<char,Int64>();
        foreach(string key in pairs.Keys)
        {
            counts.AddValue(key[0], pairs[key]);
            counts.AddValue(key[1], pairs[key]);
        }
        counts[Poly[0]]++;
        counts[Poly[Poly.Length - 1]]++;

        Int64 max = 0;
        Int64 min = Int64.MaxValue;
        foreach(var kp in counts)
        {
            if(kp.Value > max) max = kp.Value;
            if(kp.Value < min) min = kp.Value;
        }

        return (max / 2) - (min / 2);
    }

    Dictionary<string,Int64> pairs = new Dictionary<string,Int64>();
    public string RunA()
    {
        for(int i=0; i<Poly.Length - 1; ++i)
            pairs.AddValue(Poly.Substring(i,2),1);

        for(int i=0;i<10;++i) pairs = ApplyRules(pairs);
        return $"score: {Score(pairs)}";
    }

    public string RunB()
    {
        for(int i=0;i<30;++i) pairs = ApplyRules(pairs);
        return $"score: {Score(pairs)}";
    }
}
}