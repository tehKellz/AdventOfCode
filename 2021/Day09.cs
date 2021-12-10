using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day09_21 : CodeTest
{
    public string TestName = "2021/Day09";
    public bool Enabled => false;

    private int[,] Map = new int[100,100];
    private static List<Basin> Basins = new List<Basin>();
    private class Basin
    {
        public Basin(int x, int y) { BasinMap[x,y] = true; Basins.Add(this); }
        public bool[,] BasinMap = new bool[100,100];
        
        public bool Contains(int x, int y) { return BasinMap[x,y]; }
        public void Add(int x, int y) { BasinMap[x,y] = true; }
        public void Absorb(Basin other)
        {
            if (other == null || other == this) return;
            for(int x=0;x<100;++x)
                for(int y=0;y<100;++y)
                    BasinMap[x,y] |= other.BasinMap[x,y];
            Basins.Remove(other);
        }

        public int GetSize()
        {
            int size = 0;
            foreach(var p in BasinMap) if (p) ++size;
            return size;
        }
    }    
    
    public void Init() 
    {
        Utils.Load($"{TestName}.input", (string l, int n) =>
        {
            for(int i=0;i<100;++i) Map[i,n] = int.Parse($"{l[i]}");
            return false; // return true would stop parsing lines
        });
    }

    int GetRisk(int x, int y)
    {
        if (x > 0  && Map[x-1,y] <= Map[x,y]) return 0;
        if (x < 99 && Map[x+1,y] <= Map[x,y]) return 0;
        if (y > 0  && Map[x,y-1] <= Map[x,y]) return 0;
        if (y < 99 && Map[x,y+1] <= Map[x,y]) return 0;
        return Map[x,y] + 1;
    }

    public string RunA()
    {
        Int64 risk = 0;
        for(int x=0;x<100;++x)
            for(int y=0;y<100;++y) risk += GetRisk(x,y);

        return $"Risk = {risk}";
    }

    Basin GetBasin(int x, int y)
    {
        if (x<0 || x>99 || y<0 || y> 99 || Map[x,y] == 9) return null;
        foreach(var b in Basins) if (b.Contains(x,y)) return b;
        return new Basin(x,y);
    }

    void SetBasin(int x, int y)
    {
        if (Map[x,y] == 9) return;

        Basin myBasin = GetBasin(x,y);
        myBasin.Absorb(GetBasin(x,y-1));
        myBasin.Absorb(GetBasin(x-1,y));
    }

    public string RunB()
    {
        for(int x=0;x<100;++x)
            for(int y=0;y<100;++y) SetBasin(x,y);

        List<int> sizes = new List<int>();
        foreach(var b in Basins) sizes.Add(b.GetSize());
        sizes.Sort();
        sizes.Reverse();
        return $"{Basins.Count} basins. Largest: {sizes[0]} * {sizes[1]} * {sizes[2]} = {sizes[0] * sizes[1] * sizes[2]}";
    }
}
}