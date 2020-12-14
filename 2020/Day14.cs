using System;
using System.Collections.Generic;
using System.Threading;

namespace AdventOfCode
{
class Day14 : CodeTest
{
    public bool Enabled => true;

    List<string> Prog = new List<string>();
    public void Init() 
    {
        Utils.Load("2020/Day14.input", Prog);
    }

    Dictionary<Int64,Int64> Mem = null;
    string Mask = "";
    Int64 Mask0 = 68719476735;
    Int64 Mask1 = 0;

    void SetMask(string m)
    {
        Mask = m;
        Mask0 = 68719476735;
        Mask1 = 0;
        for(int i=0;i<m.Length;++i)
        {
            char c = m[m.Length - (i+1)];
            if (c == '0') Mask0 ^= (((Int64)1) << i); 
            if (c == '1') Mask1 = Mask1 | (((Int64)1) << i);
        }
    }

    void RunProg(Action<Int64,Int64,String> setAction)
    {
        Mem = new Dictionary<Int64,Int64>();
        foreach(string s in Prog)
        {
            if (s.StartsWith("mask"))
            {
                SetMask(s.Substring(7));
            }
            else
            {
                string[] parts = s.Split(" = ");
                Int64 val = Int64.Parse(parts[1]);
                string[] cmdParts = parts[0].Split(new Char[]{'[',']'});
                Int64 addr = Int64.Parse(cmdParts[1]);
                setAction(addr,val,Mask);
            }
        } 
    }

    void SetValueA(Int64 addr, Int64 val, string mask)
    {
        val = val | Mask1;
        val = val & Mask0;
        Mem[addr] = val;
    }

    public string RunA()
    {
        RunProg(SetValueA);

        Int64 sum = 0;
        foreach(var pair in Mem) sum += pair.Value;
        return $"{sum}";
    }

    void SetValueB(Int64 addr, Int64 val, string mask)
    {
        int index = mask.LastIndexOf('X');
        if (index == -1)
        {
            Mem[addr] = val;
            return;
        }

        char c = mask[index];
        if (c == 'X') 
        {
            Int64 bit = ((Int64)1) << ((mask.Length - 1) - index);
            string newMask = mask.Substring(0,index) + '0' + mask.Substring(index + 1);
            SetValueB(addr & (((Int64)68719476735) ^ bit), val, newMask);
            SetValueB(addr | bit, val, newMask);
        }
    }

    public string RunB()
    {
        RunProg((a, v, m) => {
            SetValueB(a | Mask1,v,m);
        });

        Int64 sum = 0;
        foreach(var pair in Mem) sum += pair.Value;
        return $"{sum}";
    }
}
}