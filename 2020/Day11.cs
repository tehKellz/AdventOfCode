using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day11 : CodeTest
{
    public bool Enabled => false;

    public enum Pos
    {
        Floor,
        Empty,
        Occupied,
    }
    
    List<List<Pos>> Data = null;
    public void Init() 
    {
        Data = new List<List<Pos>>();
        Utils.Load<string>("2020/Day11.input", (s,n) =>
        {
            List<Pos> row = new List<Pos>();
            foreach(char c in s)
            {
                switch(c)
                {
                case 'L':
                    row.Add(Pos.Empty);
                    break;
                case '.':
                    row.Add(Pos.Floor);
                    break;
                default:
                    Console.WriteLine($"Unexpected input {c}");
                    break;
                }
            }
            Data.Add(row);
        });
    }

    public int IsOccupied(int i, int s)
    {
        try { if (Data[i][s] == Pos.Occupied) return 1; } catch(Exception) {}
        return 0;
    }

    public int CountNeighbors(int i, int s)
    {
        return IsOccupied(i-1,s-1) + IsOccupied(i-1,s) + IsOccupied(i-1,s+1)
            + IsOccupied(i,s-1) + IsOccupied(i,s+1)
            + IsOccupied(i+1,s-1) + IsOccupied(i+1,s) + IsOccupied(i+1,s+1);
    }

    public bool Step(out int occupied, Func<int,int,int> neighborCheck, int crowd)
    {
        bool changed = false;
        occupied = 0;
        List<List<Pos>> NewData = new List<List<Pos>>();
        for(int i=0;i<Data.Count;++i)
        {
            List<Pos> row = new List<Pos>();
            for(int s=0;s<Data[i].Count;++s)
            {
                int neighbors = neighborCheck(i,s);
                switch(Data[i][s])
                {
                case Pos.Floor:
                    row.Add(Pos.Floor);
                    break;
                case Pos.Occupied:
                    if (neighbors >= crowd)
                    {
                        row.Add(Pos.Empty);
                        changed = true;
                    }
                    else
                    {
                        row.Add(Pos.Occupied);
                        ++occupied;
                    }
                    break;
                case Pos.Empty:
                    if (neighbors == 0)
                    {
                        row.Add(Pos.Occupied);
                        changed = true;
                        ++occupied;
                    }
                    else
                    {
                        row.Add(Pos.Empty);
                    }
                    break;
                }
            }
            NewData.Add(row);
        }
        Data = NewData;
        return changed;
    }

    public string RunA()
    {
        int occupied = 0;
        while (Step(out occupied, CountNeighbors, 4)) ;
        return $"{occupied}";
    }

    public Pos GetType(int i, int s)
    {
        try
        {
            Pos type = Data[i][s];
            return type;
        } catch(Exception) {}
        return Pos.Empty;
    }

    public int Look(int i, int s, int di, int ds)
    {
        while(true)
        {
            i += di;
            s += ds;
            switch(GetType(i,s))
            {
                case Pos.Floor: continue;
                case Pos.Occupied: return 1;
                case Pos.Empty: return 0; 
            }
        }
    }

    public int CountVisibleNeighbors(int i, int s)
    {
        return Look(i,s,-1,-1) + Look(i,s,-1, 0) + Look(i,s,-1,+1)
             + Look(i,s, 0,-1) + Look(i,s, 0,+1)
             + Look(i,s,+1,-1) + Look(i,s,+1, 0) + Look(i,s,+1,+1);
    }

    public string RunB()
    {
        Init();
        int occupied = 0;
        while (Step(out occupied, CountVisibleNeighbors, 5)) ;
        return $"{occupied}";
    }
}
}