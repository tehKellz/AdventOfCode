using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day03_21 : CodeTest
{
    public string TestName = "2021/Day03";
    public bool Enabled => true;
    
    private List<string> Data = new List<string>();
    
    public void Init() 
    {
        Utils.Load($"{TestName}.input", Data);
    }

    Int64[] countBits(List<string> codes)
    {
        Int64[] bitCount = new Int64[12];
        foreach(var d in codes)
            for(int i=0;i<12;++i) if (d[i] == '1') bitCount[i]++;
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

        return $"OMEGA:{omega} EPSILON:{epsilon}  POWER:{omega * epsilon}";
    }

    char findMost(List<string> codes, int bit)
    {
        int ones = 0; int zeroes = 0;
        foreach(var d in codes)
        {
            if (d[bit] == '1') ++ones;
            else ++zeroes;
        }
        return ones >= zeroes ? '1' : '0';
    }

    char findLeast(List<string> codes, int bit)
    {
        int ones = 0; int zeroes = 0;
        foreach(var d in codes)
        {
            if (d[bit] == '1') ++ones;
            else ++zeroes;
        }
        return (ones == 0) ? '0' : ((zeroes == 0) ? '1' : ((ones < zeroes) ? '1' : '0'));
    }

    public string RunB()
    {
        List<string> o = new List<string>(Data);
        {
            int bit=0;
            while(o.Count > 1)
            {
                char match = findMost(o,bit);

                List<string> newO = new List<string>();
                foreach(var d in o) if(d[bit] == match) newO.Add(d);
                o = newO;
                ++bit;
            }
        }
        
        List<string> co2 = new List<string>(Data);
        {
            int bit = 0;
            while(co2.Count > 1)
            {
                char match = findLeast(co2,bit);

                List<string> newCo2 = new List<string>();
                foreach(var d in co2) if(d[bit] == match) newCo2.Add(d);
                co2 = newCo2;
                ++bit;
            }
        }

        Int64 oxygen = Convert.ToInt32(o[0], 2);
        Int64 carbon = Convert.ToInt32(co2[0], 2);

        return $"O:{oxygen} CO2:{carbon} LIFE:{oxygen*carbon}";
    }
}
}