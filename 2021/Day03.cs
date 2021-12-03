using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day03_21 : CodeTest
{
    public string TestName = "2021/Day03";
    public bool Enabled => true;
    
    class Command : Utils.IValue
    {
        public string Op;
        public void FromString(string[] line) 
        {
            Op = line[0];
        }
    }
    
    private List<Command> Data = new List<Command>();
    
    public void Init() 
    {
        Utils.LoadValues($"{TestName}.input", Data);
    }

    Int64[] countBits(List<Command> commands)
    {
        Int64[] bitCount = new Int64[12];

        foreach(var d in commands)
        {
            for(int i=0;i<12;++i)
            {
                if (d.Op[i] == '1') bitCount[i]++;
            }
        }
        return bitCount;
    }

    public string RunA()
    {
        Int64[] bitCount = countBits(Data);

        Int64 omega = 0;
        Int64 epsilon = 0;
        for(int i=0;i<12;++i)
        {
            if (i != 0)
            {
                omega = omega << 1; 
                epsilon = epsilon << 1;
            }

            if (bitCount[i] > (Data.Count / 2)) omega |= 1;
            else epsilon |= 1;
        }

        Int64 output = (omega * epsilon);
        return $"{omega}({Convert.ToString(omega, 2)}) * {epsilon} = {output}";
    }

    public string RunB()
    {
        Console.WriteLine($"{TestName}: {Data.Count}");

        List<Command> o = new List<Command>(Data);
        {
            int bit=0;
            while(o.Count > 1)
            {
                Int64[] bitCounts = countBits(o);
                
                char match = '0';
                if(bitCounts[bit] >= (o.Count / 2) ) // 1 is most common
                {
                    match = '1';
                }

                List<Command> newO = new List<Command>();
                foreach(var d in o)
                {
                    if(d.Op[bit] == match) newO.Add(d);
                }

                o = newO;
                
                Console.WriteLine($"Finished bit {bit}, count is {o.Count}");
                ++bit;
            }
        }
        Console.WriteLine($"Only {o.Count} results: {o[0].Op} = {Convert.ToInt32(o[0].Op, 2)}");
        
        List<Command> co2 = new List<Command>(Data);
        {
            int bit = 0;
            while(co2.Count > 1)
            {
                Int64[] bitCounts = countBits(co2);
                
                char match = '1';
                if (bitCounts[bit] == co2.Count) // If ALL have 1 then 1 is still the "least" common
                {
                    match = '1';
                }
                else if (bitCounts[bit] == 0) // If NONE have 1 (all are 0) then 0 is the "least" common
                {
                    match = '0';
                }
                else if(bitCounts[bit] >= (co2.Count / 2) ) // 1 is most common
                {
                    match = '0'; // look for the "least" common.
                }

                List<Command> newCo2 = new List<Command>();
                foreach(var d in co2)
                {
                    if(d.Op[bit] == match) newCo2.Add(d);
                }

                co2 = newCo2;
                
                Console.WriteLine($"Finished bit {bit}, count is {co2.Count}");
                ++bit;
            }
        }
        Console.WriteLine($"Only {co2.Count} results: {co2[0].Op} = {Convert.ToInt32(co2[0].Op, 2)}");

        Int64 oxygen = Convert.ToInt32(o[0].Op, 2);
        Int64 carbon = Convert.ToInt32(co2[0].Op, 2);

        // Wrong: 2567786
        // Wrong: 6873932
        // Wrong: 184652
        // Wrong: 2540096
        // Wrong: 1799376
        // Wrong: 433992
        return $"O:{oxygen} CO2:{carbon} LIFE:{oxygen*carbon}";
    }
}
}