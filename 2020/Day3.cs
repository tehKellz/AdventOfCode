using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day3 : CodeTest
{
    public bool Enabled => false;

    List<string> Trees = new List<string>();
    public void Init() 
    {
        Utils.Load<string>("./2020/Day3.input", Trees);
    }

    private Int64 Slide(int dx, int dy)
    {
        Int64 hit = 0;
        int x = 0;
        int y = 0;
        while (x < Trees.Count)
        {
            if (y >= Trees[x].Length) y -= Trees[x].Length;
            if (Trees[x][y] == '#') hit++;
            x += dx;
            y += dy;
        }
        return hit;
    }

    public string RunA()
    {
        return Slide(1,3).ToString();
    }

    public string RunB()
    {
        return (Slide(1,1) * Slide(1,3) * Slide(1,5) * Slide(1,7) * Slide(2,1)).ToString();
    }
}
}