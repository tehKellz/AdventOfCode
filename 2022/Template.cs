using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Template_22: CodeTest
{
    public string TestName = "2022/Template";
    public bool Enabled => false;
    
    private List<string> Data = new List<string>();
    
    public void Init() 
    {
        Utils.Load<string>($"{TestName}.input", (string l, int n) =>
        {
            Data.Add(l);
        });
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