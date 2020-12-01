using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class UtilTests : CodeTest
{
    public bool Enabled => false;

    public string Run()
    {
        List<List<float>> data = new List<List<float>>();
        Utils.LoadCSV("./Tests/TestDict", data, ",");
        Console.WriteLine($"Loaded {data.Count} elements");
        foreach(var d in data)
        {
            Console.WriteLine($" Count={d.Count}");
            foreach(var dd in d)
                Console.WriteLine($"  {dd}");
        }
        return "Tests Complete";
    }
}
}