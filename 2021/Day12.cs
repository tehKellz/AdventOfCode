using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day12_21 : CodeTest
{
    public string TestName = "2021/Day12";
    public bool Enabled => true;
    
    class Cave
    {
        public Cave(string name) { Name = name; }
        public string Name = "";
        public List<Cave> NextCaves = new List<Cave>();
        public bool Big { get { return Char.IsUpper(Name[0]); }}
        public bool Visited = false;
    }
    
    private List<Cave> Caves = new List<Cave>();
    private Cave Start = new Cave("start");

    public void Init() 
    {
        Caves.Add(Start);

        Utils.Load($"{TestName}.input", (string l, int n) =>
        {
            var ls = l.Split('-');
            Cave fromCave = null;
            Cave toCave = null;
            foreach(var c in Caves)
            {
                if(c.Name == ls[0]) fromCave = c;
                if(c.Name == ls[1]) toCave = c;
            }

            if (fromCave == null)
            {
                fromCave = new Cave(ls[0]);
                Caves.Add(fromCave);
            }

            if (toCave == null)
            {
                toCave = new Cave(ls[1]);
                Caves.Add(toCave);
            }

            fromCave.NextCaves.Add(toCave);
            toCave.NextCaves.Add(fromCave);
        });
    }

    int Visit(Cave c)
    {
        if (c.Name == "end") return 1;

        int paths = 0;
        if (!c.Big) c.Visited = true;
        foreach(Cave next in c.NextCaves)
        {
            if (next.Visited) continue;
            paths += Visit(next);
        }
        if (!c.Big) c.Visited = false;
        return paths;
    }

    public string RunA()
    {
        return $"{Visit(Start)}";
    }

    HashSet<string> Paths = new HashSet<string>();
    bool Leisured = false;
    void VisitB(Cave c, string path)
    {
        if (c.Name == "end") 
        {
            Paths.Add($"{path}-end");
            return;
        }

        if (!c.Big) c.Visited = true;

        foreach(Cave next in c.NextCaves)
        {
            if (next.Visited) continue;
            VisitB(next,$"{path}-{c.Name}");
        }

        if (!c.Big && c != Start)
        {
            c.Visited = false;
            if (!Leisured)
            {
                Leisured = true;
                foreach(Cave next in c.NextCaves)
                {
                    if (next.Visited) continue;
                    VisitB(next,$"{path}-{c.Name}");
                }
                Leisured = false;
            }
        }
    }

    public string RunB()
    {
        VisitB(Start,"");
        return $"{Paths.Count}";
    }

    // Wrong 5012
    // Wrong 158208
    // Wrong 153501
}
}