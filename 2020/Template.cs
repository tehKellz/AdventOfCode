using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Template : CodeTest
{
    public bool Enabled => false;

    public void Init() 
    {
    }

    public string RunA()
    {
        Console.WriteLine("Hello World A");

        return "Template Output A";
    }

    public string RunB()
    {
        Console.WriteLine("Hello World B");

        return "Template Output B";
    }
}
}