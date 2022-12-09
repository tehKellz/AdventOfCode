using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class GameOfLife_22: CodeTest
{
    public string TestName = "2022/GoL";
    public bool Enabled => false;
    
    class Command : Utils.IValue
    {
        public string Op;
        public int Val;
        public void FromString(string[] line) 
        {
            Op = line[0];
            Val = Int32.Parse(line[1]);
        }
    }
    
    private List<Command> Data = new List<Command>();
    
    public void Init() 
    {
        Utils.LoadValues($"{TestName}.input", Data);
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