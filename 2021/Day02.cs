using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day02_21 : CodeTest
{
    public string TestName = "2021/Day02";
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
        int depth = 0; int dist = 0;
        foreach(var d in Data)
        {
            switch(d.Op)
            {
                case "up": depth -= d.Val; break;
                case "down": depth += d.Val; break;
                case "forward": dist += d.Val; break;
                default: break;
            }
        }

        return (depth * dist).ToString();
    }

//    down X increases your aim by X units.
//    up X decreases your aim by X units.
//    forward X does two things:
//        It increases your horizontal position by X units.
//        It increases your depth by your aim multiplied by X.
    public string RunB()
    {
        int depth = 0; int dist = 0; int aim = 0;
        foreach(var d in Data)
        {
            switch(d.Op)
            {
                case "up": aim -= d.Val; break;
                case "down": aim += d.Val; break;
                case "forward": dist += d.Val; depth += aim * d.Val; break;
                default: break;
            }
        }
        return (depth * dist).ToString();
    } 
}
}