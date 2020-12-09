using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day8 : CodeTest
{
    public bool Enabled => false;

    public enum Op
    {
        NOP,
        ACC,
        JMP,
        END
    }

    public class Instruction
    {
        public Instruction(string s)
        {
            string[] split = s.Split(" ");
            switch(split[0])
            {
                case "nop":
                    op = Op.NOP;
                    break;
                case "acc":
                    op = Op.ACC;
                    break;
                case "jmp":
                    op = Op.JMP;
                    break;
                case "end":
                    op = Op.END;

                    break;
                default:
                    break;
            }
            v1 = Int32.Parse(split[1]);
        }
        public Op op = Op.NOP;
        public int v1 = 0;
        public bool visited = false;
    }

    List<Instruction> Code = new List<Instruction>();
    public void Init() 
    {
        Utils.Load<string>("2020/Day8.input", (s, n) =>
        {
            Code.Add(new Instruction(s));
        });
    }

    public int pc = 0;
    public int acc = 0;

    public bool RunProgram()
    {
        while(true)
        {
            Instruction ipc = Code[pc];

            if (ipc.visited) return false;
            ipc.visited = true;

            switch(ipc.op)
            {
                case Op.NOP:
                    pc++;
                    break;
                case Op.ACC:
                    acc += ipc.v1;
                    pc++;
                    break;
                case Op.JMP:
                    pc += ipc.v1;
                    break;
                case Op.END:
                    // Console.WriteLine($" ** END acc={acc}");
                    return true;
                default:
                    break;
            }
        }
    }

    public string RunA()
    {
        RunProgram();
        return acc.ToString();
    }

    public void Reset()
    {
        pc = 0;
        acc = 0;
        foreach(var i in Code)
        {
            i.visited = false;
        }
    }

    bool flip(int c)
    {
        switch (Code[changed].op)
        {
            case Op.NOP:
                Code[changed].op = Op.JMP;
                return true;
            case Op.JMP:
                Code[changed].op = Op.NOP;
                return true;
            default:
                return false;
        }
    }

    int changed = -1;
    void Change()
    {
        // Swapping the last one back
        if (changed > -1) flip(changed);

        for(++changed; changed<Code.Count; ++changed)
        {
            if (flip(changed)) return;
        }
    }

    public string RunB()
    {
        Reset();
        while(!RunProgram())
        {
            Reset();
            Change();
        }

        return acc.ToString();
    }
}
}