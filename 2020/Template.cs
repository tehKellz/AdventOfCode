using System;

namespace AdventOfCode
{
class Template : CodeTest
{
    public bool Enabled => false;

    public string Run()
    {
        Console.WriteLine("Hello World Template");

        return "Template Output";
    }
}
}