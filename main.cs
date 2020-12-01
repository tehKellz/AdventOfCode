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
    private static List<CodeTest> Challenges = new List<CodeTest>();

    static void Main(string[] args)
    {
        Assembly a = Assembly.LoadFrom("aoc_test.exe");
        foreach (Type t in a.GetTypes())
        {
            if (t.GetInterface("AdventOfCode.CodeTest") != null)
            {
                CodeTest instance = (CodeTest)Activator.CreateInstance(t);
                Challenges.Add(instance);
            }
        }

        var watch = new System.Diagnostics.Stopwatch();
        Console.WriteLine("Running Challenges.");
        foreach(var test in Challenges)
        {
            if (test.Enabled)
            {
                watch.Restart();
                string output = test.Run();
                watch.Stop();
                Console.WriteLine($"  [{test.GetType().Name}]({watch.ElapsedMilliseconds}ms): " + output);
            }
        }
        Console.WriteLine("Run complete.");
    }
}
}