using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day8 : CodeTest
{
    public bool Enabled => true;

    public enum Op
    {
        nop,
        acc,
        jmp,
        end
    }

    public class Instruction
    {
        public Instruction(string s)
        {
            string[] split = s.Split(" ");
            switch(split[0])
            {
                case "nop":
                    op = Op.nop;
                    break;
                case "acc":
                    op = Op.acc;
                    break;
                case "jmp":
                    op = Op.jmp;
                    break;
                case "end":
                    op = Op.end;

                    break;
                default:
                    break;
            }
            v1 = Int32.Parse(split[1]);
        }
        public Op op = Op.nop;
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
        Code.Add(new Instruction("end +0"));
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
                    Console.WriteLine(" ***** END *****");
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
        if (changed >= Code.Count)
        {
            Console.WriteLine(" ** ERROR: End of program??");
            //return;
        }

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
        Console.WriteLine($"Last line {Code[Code.Count - 1].op}");
        while(!RunProgram())
        {
            Reset();
            Change();
            Console.WriteLine($" Deadlock from changing {changed}");
        }

        return acc.ToString();
    }
}
}