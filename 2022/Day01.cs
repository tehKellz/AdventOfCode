using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day01_22 : CodeTest
{
    public string TestName = "2022/Day01";
    public bool Enabled => true;
    
    private List<List<int>> Data = new List<List<int>>();
    
    public void Init() 
    {
        int elfNum = 0;
        Data.Add(new List<int>());
        Utils.Load<string>($"{TestName}.input", (string l, int n) =>
        {
            //Console.WriteLine($"{l}");
            if (string.IsNullOrEmpty(l))
            {
                ++elfNum;
                Data.Add(new List<int>());
                return;
            }
            Data[elfNum].Add(Int32.Parse(l));
        });
    }

    public string RunA()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");
        int max = 0;
        foreach(var elfBag in Data)
        {
            int total = 0;
            foreach(var cal in elfBag)
            {
                total += cal;
            }
            if (total > max) max = total;
        }
        return $"Output A:{max}";
    }

    public string RunB()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");

        List<int> elfTotals = new List<int>();
        foreach(var elfBag in Data)
        {
            int total = 0;
            foreach(var cal in elfBag)
            {
                total += cal;
            }
            elfTotals.Add(total);
        }
        elfTotals.Sort();
        elfTotals.Reverse();
        return $"Output B: {elfTotals[0]+elfTotals[1]+elfTotals[2]} ";
    }
}
}