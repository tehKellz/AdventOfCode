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

    Dictionary<Int64,Int64> Mem = new Dictionary<Int64,Int64>();
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

    void SetValue(Int64 addr, Int64 val)
    {
        val = val | Mask1;
        val = val & Mask0;
        Mem[addr] = val;
    }

    public string RunA()
    {
        //mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
        //mem[8] = 11
        //mem[7] = 101
        //mem[8] = 0
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
                SetValue(addr,val);
            }
        }

        Int64 sum = 0;
        foreach(var pair in Mem)
        {
            sum += pair.Value;
        }

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
            int bit = (mask.Length - 1) - index;
            Int64 addr1 = addr;
            Int64 addr0 = addr;
            addr1 |= ((Int64)1) << bit;
            addr0 &= (68719476735 ^ ((Int64)1) << bit);

            //Console.WriteLine($"   {Convert.ToString(addr0, 2)}");
            //Console.WriteLine($"   {Convert.ToString(addr1, 2)}");
            string newMask = mask.Substring(0,index) + '0' + mask.Substring(index + 1);

            //Console.WriteLine($"** {newMask}");
            SetValueB(addr0, val, newMask);
            SetValueB(addr1, val, newMask);
            
            //Thread.Sleep(100);
            //Console.WriteLine($"   {newMask0}");
            //Console.WriteLine($"   {newMask1}");
        }
    }

    public string RunB()
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

                addr = addr | Mask1;
                SetValueB(addr,val, Mask);
            }
        }
        Int64 sum = 0;
        foreach(var pair in Mem)
        {
            sum += pair.Value;
        }

        return $"{sum}";
    }
}
}