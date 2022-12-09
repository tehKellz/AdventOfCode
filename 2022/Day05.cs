using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day05_22: CodeTest
{
    public string TestName = "2022/Day05";
    public bool Enabled => true;

    struct Command
    {
        public int count;
        public int source;
        public int dest;
    }
    private List<Command> Data = new List<Command>();
    private List<List<char>> Stacks = new List<List<char>>();
    
    public void Init() 
    {
        Stacks.Add(new List<char>()); // dummy 0th stack
        Utils.Load<string>($"{TestName}.stacks", (string l, int n) =>
        {
            var stack = new List<char>();
            foreach(var c in l)
            {
                stack.Add(c);
            }
            Stacks.Add(stack);
        });

        
        Utils.Load<string>($"{TestName}.input", (string l, int n) =>
        {
            Command command = new Command();
            var l2 = l.Split(' ');
            command.count = Int32.Parse(l2[1]);
            command.source = Int32.Parse(l2[3]);
            command.dest = Int32.Parse(l2[5]);
            Data.Add(command);
        });
    }

    public string RunA()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");

        foreach(var com in Data)
        {
            for(int i=0;i<com.count; ++i)
            {
                int lastIndex = Stacks[com.source].Count - 1;
                char item = Stacks[com.source][lastIndex];
                Stacks[com.source].RemoveAt(lastIndex);
                Stacks[com.dest].Add(item);
            }
        }

        string output = "";
        foreach(var s in Stacks)
        {
            if (s.Count > 0)
                output += $"{s[s.Count - 1]}";
        }
        return $"Output A {output}";
    }

    public string RunB()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");
        Data = new List<Command>();
        Stacks = new List<List<char>>();
        Init();
        foreach(var com in Data)
        {
            for(int i=0;i<com.count; ++i)
            {
                int lastIndex = Stacks[com.source].Count - 1;
                char item = Stacks[com.source][lastIndex];
                Stacks[com.source].RemoveAt(lastIndex);
                Stacks[0].Add(item);
            }
            
            for(int i=0;i<com.count; ++i)
            {
                int lastIndex = Stacks[0].Count - 1;
                char item = Stacks[0][lastIndex];
                Stacks[0].RemoveAt(lastIndex);
                Stacks[com.dest].Add(item);
            }
        }

        string output = "";
        foreach(var s in Stacks)
        {
            if (s.Count > 0)
                output += $"{s[s.Count - 1]}";
        }
        return $"Output B {output}";
    }
}
}