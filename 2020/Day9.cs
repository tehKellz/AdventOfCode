using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
class Day9 : CodeTest
{
    public bool Enabled => true;

    List<Int64> Seq = new List<Int64>();

    public void Init() 
    {
        Utils.Load("2020/Day9.input", Seq);
    }

    public bool Valid(List<Int64> range, Int64 value)
    {

        for(int i = 0; i<range.Count;++i)
        {
            for(int j=i+1; j<range.Count;++j)
            {
                if (range[i] + range[j] == value)
                {
                    return true;
                } 
            }
        }
        return false;
    }

    public string RunA()
    {
        int curr = 25;

        while(Valid(Seq.GetRange(curr - 25, 25), Seq[curr]))
        {
            ++curr;
        }
//        Console.WriteLine($"C:[{curr}] {Seq[curr]} => {string.Join(",", Seq.GetRange(curr - 25, 25).Select(x => x.ToString()).ToArray())}");
        

        return Seq[curr].ToString();
    }

    Int64 FindMinMaxSum(List<Int64> range)
    {
        Int64 min = 375054920;
        Int64 max = 0;
        foreach(Int64 v in range)
        {
            if (v < min) min = v;
            if (v > max) max = v;
        }
        return min + max;
    }

    public string RunB()
    {
        Int64 target = 375054920;
        for(int i=0;i<Seq.Count;++i)
        {
            Int64 sum = 0;
            for(int j=i;j<Seq.Count;++j)
            {
                sum += Seq[j];
                if (sum == target)
                {
                    return $"[{i}] {Seq[i]} + [{j}] {Seq[j]} = {FindMinMaxSum(Seq.GetRange(i, j-i))}";
                }
                else if (sum > target) break;
            }
        }
        return "Template Output B";
    }
}
}