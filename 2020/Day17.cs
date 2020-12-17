using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day17 : CodeTest
{
    public bool Enabled => true;

    public struct Cell
    {
        public int X,Y,Z;
        public Cell(int x, int y, int z)
        {
            X=x;Y=y;Z=z;
        }
        // From stackoverflow
        override public int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int) 2166136261;
                // Suitable nullity checks etc, of course :)
                hash = (hash * 16777619) ^ X.GetHashCode();
                hash = (hash * 16777619) ^ Y.GetHashCode();
                hash = (hash * 16777619) ^ Z.GetHashCode();
                return hash;
            }
        }
    }

    public HashSet<Cell> Pocket = new HashSet<Cell>();
    public Cell Min = new Cell(999,999,999);
    public Cell Max = new Cell(0,0,0);

    public void UpdateBounds(Cell c)
    {
        if (c.X < Min.X) Min.X = c.X;
        if (c.Y < Min.Y) Min.Y = c.Y;
        if (c.Z < Min.Z) Min.Z = c.Z;
        if (c.X > Max.X) Max.X = c.X;
        if (c.Y > Max.Y) Max.Y = c.Y;
        if (c.Z > Max.Z) Max.Z = c.Z;
    }

    public void Init() 
    {
        Utils.Load<string>("2020/Day17.input", (s,n) =>
        {
            for(int i=0; i<s.Length;++i)
            {
                if (s[i] == '#')
                {
                    Cell c = new Cell(i,n,0);
                    UpdateBounds(c);
                    Pocket.Add(c);
                }
            }
        });

        Console.WriteLine($"Active in Pocket: {Pocket.Count}");
    }

    void SetCell(int x, int y, int z, HashSet<Cell> newPocket)
    {
        Cell c = new Cell(x,y,z);

        int neighbors = 0;
        for(int xi = x-1; xi <= x+1; ++xi)
        {
            for(int yi = y-1; yi <= y+1; ++yi)
            {
                for(int zi = z-1; zi <= z+1; ++zi)
                {
                    if ( (z!=zi||y!=yi||x!=xi) && Pocket.Contains(new Cell(xi,yi,zi)))
                    {
                        ++neighbors;
                    }
                }
            }
        }

        bool active = Pocket.Contains(c);
        if (active)
        {
            //If a cube is active and exactly 2 or 3 of its neighbors are also active, the cube remains active. Otherwise, the cube becomes inactive.
            if (neighbors == 2 || neighbors == 3)
            {
                newPocket.Add(c);
                UpdateBounds(c);
            }
        }
        else
        {
            //If a cube is inactive but exactly 3 of its neighbors are active, the cube becomes active. Otherwise, the cube remains inactive.
            if (neighbors == 3)
            {
                newPocket.Add(c);
                UpdateBounds(c);
            }
        }
    }

    public string RunA()
    {
        for(int cycle = 0; cycle < 6; ++cycle)
        {
            HashSet<Cell> newPocket = new HashSet<Cell>();
            for(int x = Min.X-1; x<Max.X+2;++x)
                for(int y = Min.Y-1; y<Max.Y+2;++y)
                    for(int z = Min.Z-1; z<Max.Z+2;++z)
                    {
                        SetCell(x,y,z,newPocket);
                    }
            Pocket = newPocket;
            Console.WriteLine($" End day {cycle}, active in Pocket: {Pocket.Count}");
        }

        return $"Active: {Pocket.Count}";
    }

    public string RunB()
    {
        Console.WriteLine("Hello World B");

        return "Template Output B";
    }
}
}