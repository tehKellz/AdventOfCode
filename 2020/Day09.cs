 using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
class Day9 : CodeTest
{
    public bool Enabled => true;

    Int64 weakness = -1;
    List<Int64> Seq = new List<Int64>();
    public void Init() { Utils.Load("2020/Day09.input", Seq); }

    public bool Valid(int start, int end, Int64 value)
    {
        for(int i=start; i<end;++i)
            for(int j=i+1; j<end;++j)
                if (Seq[i] + Seq[j] == value) return true;
        return false;
    }

    public string RunA()
    {
        int curr = 25;
        while(Valid(curr-25, curr, Seq[curr])) ++curr;
        return (weakness = Seq[curr]).ToString();
    }

    public string RunB()
    {
        for(int i=0;i<Seq.Count;++i)
        {
            Int64 sum = 0;
            for(int j=i;sum<weakness;++j)
            {
                sum += Seq[j];
                if (sum == weakness) return (Seq.GetRange(i, j-i).Min() + Seq.GetRange(i, j-i).Max()).ToString();
            }
        }
        return "Unbreakable code";
    }
}
}