using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day03: CodeTest
{
    public string TestName = "2022/Day03";
    public bool Enabled => false;

    public struct Pack
    {
        public string all;
        public string[] compartments;
    }
    private List<Pack> Data = new List<Pack>();
    private string codex = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public void Init() 
    {
        Console.WriteLine($"Length of codex is {codex.Length} :: z = {codex.IndexOf('Z')}");
        Utils.Load<string>($"{TestName}.input", (string l, int n) =>
        {
            Pack p;
            p.all = l;
            p.compartments = new string[2];
            p.compartments[0] = l.Substring(0,l.Length / 2);
            p.compartments[1] = l.Substring(l.Length/2);

            Data.Add(p);
        });
    }

    public int FindMispacked(Pack pack)
    {
        foreach(var c in pack.compartments[0])
        {
            if (pack.compartments[1].IndexOf(c) != -1)
                return codex.IndexOf(c);
        }
        Console.WriteLine("No common item.");
        return 0;
    }

    public string RunA()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");
        Int64 sum = 0;
        foreach (var pack in Data)
        {
            sum += FindMispacked(pack);
        }

        return $"Output A {sum}";
    }

    public int FindBadge(int first)
    {
        foreach(var c in Data[first].all)
        {
            if (Data[first+1].all.IndexOf(c) != -1
                && Data[first+2].all.IndexOf(c) != -1)
            {
                return codex.IndexOf(c);
            }
        }
        Console.WriteLine("No badgecommon item.");
        return 0;
    }
    
    public string RunB()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");
        Int64 sum = 0;
        for(int i=0; i<Data.Count; i += 3)
        {
            sum += FindBadge(i);
        }

        return $"Output B {sum}";
    }
}
}