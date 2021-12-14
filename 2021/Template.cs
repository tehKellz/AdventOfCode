using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Template_21 : CodeTest
{
    public string TestName = "2021/Template";
    public bool Enabled => false;
    
    private List<string> Data = new List<string>();
    
    public void Init() 
    {
        Utils.Load($"{TestName}.input", (string s, int n) =>
        {

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