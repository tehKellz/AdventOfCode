using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day11 : CodeTest
{
    public bool Enabled => true;

    public enum Pos
    {
        Floor,
        Empty,
        Occupied,
    }
    List<List<Pos>> Data = new List<List<Pos>>();
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

    public int IsFull(int i, int s)
    {
        try
        {
            if (Data[i][s] == Pos.Occupied) return 1;
        } catch(Exception) {}
        return 0;
    }
    public int CountNeighbors(int i, int s)
    {
        return IsFull(i-1,s-1)
        + IsFull(i-1,s)
        + IsFull(i-1,s+1)
        + IsFull(i,s-1)
        + IsFull(i,s+1)
        + IsFull(i+1,s-1)
        + IsFull(i+1,s)
        + IsFull(i+1,s+1);
    }

    public bool Step(out int occupied, Func<int,int,int> neighborCheck, int crowd)
    {

    //If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
    //If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
    //Otherwise, the seat's state does not change.
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
                        row.Add(Pos.Empty);
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
        int q = i;
        int v = s;
        while(true)
        {
            q += di;
            v += ds;
            switch(GetType(q,v))
            {
                case Pos.Floor:
                    continue;
                case Pos.Occupied:
                    return 1;
                case Pos.Empty:
                    return 0; 
            }
        }
    }

    public int CountVisibleNeighbors(int i, int s)
    {
        return 
          Look(i,s,-1,-1)
        + Look(i,s,-1, 0)
        + Look(i,s,-1,+1)
        + Look(i,s, 0,-1)
        + Look(i,s, 0,+1)
        + Look(i,s,+1,-1)
        + Look(i,s,+1, 0)
        + Look(i,s,+1,+1);
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