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

    AdventOfCodeMain() {}
    static void Main(string[] args)
    {
      Assembly a = Assembly.LoadFrom("aoc_test.exe");
      // Gets the type names from the assembly.
      Type[] types2 = a.GetTypes();
      foreach (Type t in types2)
      {
          if (t.GetInterface("AdventOfCode.CodeTest") != null)
          {
            Console.WriteLine("Found test " + t.FullName);
            CodeTest instance = (CodeTest)Activator.CreateInstance(t);
            Tests.Add(instance);
          }
      }

      Console.WriteLine("Running Tests.");
      foreach(var test in Tests)
      {
        string output = test.Enabled ? test.Run() : "<disabled>";
        Console.WriteLine("  [" + test.GetType().ToString() + "]: " + output);
      }
      Console.WriteLine("Run complete.");
    }
  }
}