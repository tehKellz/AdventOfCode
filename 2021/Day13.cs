using System;
using System.Collections.Generic;

namespace AdventOfCode
{

class Day13_21 : CodeTest
{
    public string TestName = "2021/Day13";
    public bool Enabled => true;
    
    public class Point
    {
        public Point(int x, int y) { X = x; Y = y; }
        public int X;
        public int Y;
        public override int GetHashCode() { return (X << 16) | Y; }

        public override bool Equals(object obj)
        {
            Point other = obj as Point;
            if (other != null) return X == other.X && Y == other.Y;
            return false;
        }
    }

    public class Fold
    {
        public char Dir = 'x';
        public int Line = -1;
    }

    private HashSet<Point> Page = new HashSet<Point>();
    private List<Fold> Folds = new List<Fold>();

    public void Init() 
    {
        Utils.Load($"{TestName}.input", (string l, int n) =>
        {
            var coords = l.Split(',');
            Page.Add(new Point(Int32.Parse(coords[0]),Int32.Parse(coords[1])));
        });

        Utils.Load($"{TestName}.folds", (string l, int n) =>
        {
            Fold f = new Fold();
            f.Dir = l[11];
            f.Line = Int32.Parse(l.Split('=')[1]);
            Folds.Add(f);
        });
    }

    void DoFoldX(int x)
    {
        List<Point> toMove = new List<Point>();
        foreach(Point p in Page) if (p.X > x) toMove.Add(p);

        foreach(Point p in toMove)
        {
            Page.Add(new Point(x + x - p.X, p.Y));
            Page.Remove(p);
        }
    }

    void DoFoldY(int y)
    {
        List<Point> toMove = new List<Point>();
        foreach(Point p in Page) if (p.Y > y) toMove.Add(p);

        foreach(Point p in toMove)
        {
            Page.Add(new Point(p.X,y + y - p.Y));
            Page.Remove(p);
        }
    }

    void DoFold(Fold f)
    {
        if (f.Dir == 'y') DoFoldY(f.Line);
        else if (f.Dir == 'x') DoFoldX(f.Line);
    }

    public string RunA()
    {
        Console.WriteLine($"{TestName}: {Page.Count} :: {Folds.Count}");
        DoFold(Folds[0]);

        return $"Count {Page.Count}";
    }

    public string RunB()
    {
        foreach(Fold f in Folds) DoFold(f);

        List<List<char>> display = new List<List<char>>();
        foreach(Point p in Page)
        {
            while (display.Count <= p.Y) display.Add(new List<char>());
            while (display[p.Y].Count <= p.X) display[p.Y].Add(' ');
            display[p.Y][p.X] = 'X';
        }

        foreach(List<char> line in display)
            Console.WriteLine($"{new String(line.ToArray())}");

        return "";
    }
}
}