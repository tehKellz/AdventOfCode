using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdventOfCode
{
public interface CodeTest
{
    bool Enabled { get; }
    string Run();
}

public class AdventOfCodeMain
{
    private static List<CodeTest> Tests = new List<CodeTest>();

    static void Main(string[] args)
    {
        Assembly a = Assembly.LoadFrom("aoc_test.exe");
        foreach (Type t in a.GetTypes())
        {
            if (t.GetInterface("AdventOfCode.CodeTest") != null)
            {
                CodeTest instance = (CodeTest)Activator.CreateInstance(t);
                Tests.Add(instance);
            }
        }

        Console.WriteLine("Running Tests.");
        foreach(var test in Tests)
        {
            if (test.Enabled)
                Console.WriteLine("  [" + test.GetType().ToString() + "]: " + test.Run());
        }
        Console.WriteLine("Run complete.");
    }
}
}