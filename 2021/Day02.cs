using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day02_21 : CodeTest
{
    public string TestName = "2021/Day02";
    public bool Enabled => true;
    
    private List<string> Data = new List<string>();
    
    public void Init() 
    {
        Utils.Load($"{TestName}.input", Data);
    }

    public string RunA()
    {
        int depth = 0;
        int dist = 0;
        foreach(var d in Data)
        {
            var cmd = d.Split(' ');
            int val = Int32.Parse(cmd[1]);
            switch(cmd[0])
            {
                case "up": depth -= val; break;
                case "down": depth += val; break;
                case "forward": dist += val; break;
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
            var cmd = d.Split(' ');
            int val = Int32.Parse(cmd[1]);
            switch(cmd[0])
            {
                case "up": aim -= val; break;
                case "down": aim += val; break;
                case "forward": dist += val; depth += aim * val; break;
                default: break;
            }
        }
        return (depth * dist).ToString();
    }
}
}