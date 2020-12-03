using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdventOfCode
{
public interface CodeTest
{
    bool Enabled { get; }
    void Init();
    string RunA();
    string RunB();
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
                test.Init();
                watch.Restart();
                string outputA = test.RunA();
                watch.Stop();
                Console.WriteLine($"  [{test.GetType().Name} A]({watch.ElapsedMilliseconds}ms): " + outputA);

                watch.Restart();
                string outputB = test.RunB();
                watch.Stop();
                Console.WriteLine($"  [{test.GetType().Name} B]({watch.ElapsedMilliseconds}ms): " + outputB);
            }
        }
        Console.WriteLine("Run complete.");
    }
}
}