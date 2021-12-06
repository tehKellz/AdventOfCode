using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day05_21 : CodeTest
{
    public string TestName = "2021/Day05";
    public bool Enabled => false;
    
    class Point
    {
        public int X = 0;
        public int Y = 0;
    }

    class Line
    {
        public Point Start = new Point();
        public Point End = new Point();
    }
    
    private List<Line> Data = new List<Line>();
    int[,] Map = new int[1000,1000];

    public void Init() 
    {
        string[] separatingStrings = { ",", " -> " };
        Utils.Load<string>($"{TestName}.input", (string l, int n) =>
        {
            string[] ls = l.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
            Line newLine = new Line();
            newLine.Start.X = Int32.Parse(ls[0]);
            newLine.Start.Y = Int32.Parse(ls[1]);
            newLine.End.X   = Int32.Parse(ls[2]);
            newLine.End.Y   = Int32.Parse(ls[3]);

            Data.Add(newLine);
        });
    }
    
    public string RunA()
    {
        foreach(var l in Data)
        {
            if (l.Start.X == l.End.X)
            {
                int start = l.Start.Y < l.End.Y ? l.Start.Y : l.End.Y;
                int end = l.End.Y > l.Start.Y ? l.End.Y : l.Start.Y;
                for(int i=start; i <= end; ++i)
                    Map[l.Start.X,i]++;
            }
            else if (l.Start.Y == l.End.Y)
            {
                int start = l.Start.X < l.End.X ? l.Start.X : l.End.X;
                int end = l.End.X > l.Start.X ? l.End.X : l.Start.X;
                for(int i=start; i<=end;++i)
                    Map[i,l.Start.Y]++;
            }
        }

        int overlap = 0;
        for(int x=0;x<1000;++x)
            for(int y=0;y<1000;++y)
                if (Map[x,y] > 1) ++overlap;

        return $"Overlap = {overlap}";
    }

    public string RunB()
    {
        foreach(var l in Data)
        {
            if ((l.Start.X != l.End.X) && (l.Start.Y != l.End.Y))
            {
                int incX = l.Start.X < l.End.X ? 1 : -1;
                int incY = l.Start.Y < l.End.Y ? 1 : -1;
                int x = l.Start.X;
                int y = l.Start.Y;

                while(x != l.End.X)
                {
                    Map[x,y]++;
                    x += incX;
                    y += incY;
                }

                Map[x,y]++;
            }
        }

        int overlap = 0;
        for(int x=0;x<1000;++x)
            for(int y=0;y<1000;++y)
                if (Map[x,y] > 1) ++overlap;

        return $"Overlap = {overlap}";
    }
}
}