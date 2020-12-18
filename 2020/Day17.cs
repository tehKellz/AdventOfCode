using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day17 : CodeTest
{
    public bool Enabled => false;

    public struct Cell
    {
        public int X,Y,Z,W;
        public Cell(int x, int y, int z, int w)
        {
            X=x;Y=y;Z=z;W=w;
        }
        // From stackoverflow
        override public int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int) 2166136261;
                hash = (hash * 16777619) ^ X.GetHashCode();
                hash = (hash * 16777619) ^ Y.GetHashCode();
                hash = (hash * 16777619) ^ Z.GetHashCode();
                hash = (hash * 16777619) ^ W.GetHashCode();
                return hash;
            }
        }
    }

    public HashSet<Cell> Pocket = new HashSet<Cell>();
    public Cell Min = new Cell(999,999,999,999);
    public Cell Max = new Cell(0,0,0,0);

    public void UpdateBounds(Cell c)
    {
        if (c.X < Min.X) Min.X = c.X;
        if (c.Y < Min.Y) Min.Y = c.Y;
        if (c.Z < Min.Z) Min.Z = c.Z;
        if (c.W < Min.W) Min.W = c.W;

        if (c.X > Max.X) Max.X = c.X;
        if (c.Y > Max.Y) Max.Y = c.Y;
        if (c.Z > Max.Z) Max.Z = c.Z;
        if (c.W > Max.W) Max.W = c.W;
    }

    public void Init() 
    {
        Utils.Load<string>("2020/Day17.input", (s,n) =>
        {
            for(int i=0; i<s.Length;++i)
            {
                if (s[i] == '#')
                {
                    Cell c = new Cell(i,n,0,0);
                    UpdateBounds(c);
                    Pocket.Add(c);
                }
            }
        });
    }

    void SetCell(int x, int y, int z, int w,HashSet<Cell> newPocket, bool hyper)
    {
        Cell c = new Cell(x,y,z,w);

        int neighbors = 0;
        for(int xi = x-1; xi <= x+1; ++xi)
            for(int yi = y-1; yi <= y+1; ++yi)
                for(int zi = z-1; zi <= z+1; ++zi)
                    if (hyper)
                    {
                        for(int wi = w-1; wi <= w+1; ++wi)
                            if ( (z!=zi||y!=yi||x!=xi||w!=wi) && Pocket.Contains(new Cell(xi,yi,zi,wi))) 
                                ++neighbors;
                    }
                    else
                    {
                        if ( (z!=zi||y!=yi||x!=xi) && Pocket.Contains(new Cell(xi,yi,zi,0)))
                            ++neighbors;
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
                            SetCell(x,y,z,0,newPocket,false);
                        }
            Pocket = newPocket;
        }

        return $"Active: {Pocket.Count}";
    }

    public string RunB()
    {
        Pocket = new HashSet<Cell>();
        Init();
        for(int cycle = 0; cycle < 6; ++cycle)
        {
            HashSet<Cell> newPocket = new HashSet<Cell>();
            for(int x = Min.X-1; x<Max.X+2;++x)
                for(int y = Min.Y-1; y<Max.Y+2;++y)
                    for(int z = Min.Z-1; z<Max.Z+2;++z)
                        for(int w = Min.W-1; w<Max.W+2;++w)
                        {
                            SetCell(x,y,z,w,newPocket,true);
                        }
            Pocket = newPocket;
        }

        return $"Active: {Pocket.Count}";
    }
}
}