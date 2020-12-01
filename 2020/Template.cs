using System;
using AdventOfCode;

namespace AdventOfCode
{
  class Template : CodeTest
  {
    public Template() {}
    public bool Enabled => false;

    public string Run()
    {
      Console.WriteLine("Hello World Template");

      return "Template Output";
    }
  }
}