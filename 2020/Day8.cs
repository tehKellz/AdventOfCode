using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day8 : CodeTest
{
    public bool Enabled => true;

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

            if (ipc.visited)
            {
                return false;
            }

            ipc.visited = true;
            switch(ipc.op)
            {
                case Op.nop:
                    pc++;
                    break;
                case Op.acc:
                    acc += ipc.v1;
                    pc++;
                    break;
                case Op.jmp:
                    pc += ipc.v1;
                    break;
                case Op.end:
                    Console.WriteLine($" ** END acc={acc}");
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

    int changed = -1;
    void Change()
    {
        if (changed > -1)
        {
            // Swapping this one back first.
            if (Code[changed].op == Op.nop)
            {
                Code[changed].op = Op.jmp;
            }
            else if (Code[changed].op == Op.jmp)
            {
                Code[changed].op = Op.nop;
            }
        }
        for(++changed; changed<Code.Count; ++changed)
        {
            if (Code[changed].op == Op.nop)
            {
                Code[changed].op = Op.jmp;
                return;
            }
            else if (Code[changed].op == Op.jmp)
            {
                Code[changed].op = Op.nop;
                return;
            }
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