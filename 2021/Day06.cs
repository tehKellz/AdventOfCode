using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day06_21 : CodeTest
{
    public string TestName = "2021/Day06";
    public bool Enabled => true;
    
    // Array of [Days-to-spawn] to #-of-fish.
    // ie School[0] is the # of fish that will spawn tomorrow.
    private UInt64[] School = new UInt64[9];
    
    public void Init() 
    {
        for(int i=0;i<9;++i) School[i] = 0;
        Utils.Load($"{TestName}.input", (string l) =>
        {
            var ls = l.Split(',');
            foreach(var entry in ls) School[Int32.Parse(entry)]++;
            return false;
        });
    }

    public UInt64 Simulate(int days)
    {
        for(int day =0; day < days; ++day)
        {
            UInt64 tmp = School[0];
            for(int i=1;i<9;++i) School[i-1] = School[i];
            School[6] += tmp;
            School[8] =  tmp;
        }

        UInt64 totalFish = 0;
        foreach(var f in School) totalFish += f;
        return totalFish;
    }

    public string RunA()
    {
        return $"Total fish: {Simulate(80)}";
    }

    public string RunB()
    {
        Init(); // Get a clean School
        return $"Total fish: {Simulate(256)}";
    }
}
}