using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day01_21 : CodeTest
{
    public string TestName = "2021/Day01";
    public bool Enabled => true;
    
    private List<string> Data = new List<string>();
    
    public void Init() 
    {
        Utils.Load($"{TestName}.input", Data);
    }

    public string RunA()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");

        return "Output A";
    }

    public string RunB()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");

        return "Output B";
    }
}
}