using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day04_22: CodeTest
{
    public string TestName = "2022/Day04";
    public bool Enabled => false;

    public class RangePair
    {
        public int[] first;
        public int[] second;
    }
    private List<RangePair> Data = new List<RangePair>();
    
    public void Init() 
    {
        var splitOn = new char[]{'-',','};
        Utils.Load<string>($"{TestName}.input", (string l, int n) =>
        {
            var l2 = l.Split(splitOn);
            RangePair rp = new RangePair();
            rp.first = new int[2];
            rp.second = new int[2];
            rp.first[0] = Int32.Parse(l2[0]);
            rp.first[0] = Int32.Parse(l2[0]);
            rp.first[1] = Int32.Parse(l2[1]);
            rp.second[0] = Int32.Parse(l2[2]);
            rp.second[1] = Int32.Parse(l2[3]);
            Data.Add(rp);
        });
    }

    public string RunA()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");
        int count = 0;
        foreach(var rp in Data)
        {
            if (rp.first[0] >= rp.second[0] && rp.first[1] <= rp.second[1])
            {
                count++;
            }
            else if (rp.second[0] >= rp.first[0] && rp.second[1] <= rp.first[1])
            {
                count++;
            }
        }
        return $"Output A {count}";
    }

    public string RunB()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");
        int count = 0;
        foreach(var rp in Data)
        {
            if (
                   (rp.first[0] >= rp.second[0] && rp.first[0] <= rp.second[1])
                || (rp.first[1] <= rp.second[1] && rp.first[1] >= rp.second[0])
                || (rp.second[0] >= rp.first[0] && rp.second[0] <= rp.first[1])
                || (rp.second[1] <= rp.first[1] && rp.second[1] >= rp.first[0])
                
                )
            {
                count++;
            }
        }
        return $"Output B {count}";
    }
}
}