using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day06_21 : CodeTest
{
    public string TestName = "2021/Day06";
    public bool Enabled => true;
    
    // Dictionary of [Days-to-spawn] to #-of-fish.
    // ie School[0] is the # of fish that will spawn tomorrow.
    private Dictionary<int,UInt64> School = new Dictionary<int,UInt64>();
    
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
            Dictionary<int,UInt64> newSchool = new Dictionary<int,UInt64>();
            for(int i=0;i<8;++i) newSchool[i] = School[i+1];
            newSchool[6] += School[0];
            newSchool[8] =  School[0];
            School = newSchool;
        }

        UInt64 totalFish = 0;
        foreach(var f in School) totalFish += f.Value;
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