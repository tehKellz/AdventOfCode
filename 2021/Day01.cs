using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day01_21 : CodeTest
{
    public string TestName = "2021/Day01";
    public bool Enabled => false;
    
    private List<int> Data = new List<int>();
    
    public void Init() 
    {
        Utils.Load($"{TestName}.input", Data);
    }

    public string RunA()
    {
        int prev = 9999999;
        int incs = 0;
        foreach(int d in Data)
        {
            if (d > prev)
            {
                incs++;
            }
            prev = d;
        }

        return incs.ToString();
    }

    public string RunB()
    {
        int prev = 9999999;
        int incs = 0;
        for(int i=2;i<Data.Count;++i)
        {
            int cur = Data[i-2]+Data[i-1]+Data[i];
            if (cur > prev)
            {
                incs++;
            }
            prev = cur;
        }
        return incs.ToString();
    }
}
}