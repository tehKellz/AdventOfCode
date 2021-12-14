using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day14_21 : CodeTest
{
    public string TestName = "2021/Day14";
    public bool Enabled => true;
    
    private string Poly = "BCHCKFFHSKPBSNVVKVSK";

    private Dictionary<string,string> Rules = new Dictionary<string,string>();
    
    public void Init() 
    {
        Utils.Load($"{TestName}.input", (string s, int n) =>
        {
            var sl = s.Split(" -> ");
            Rules[sl[0]] = sl[1];
        });
    }

    public string ApplyRules(string poly)
    {
        string newString = "";
        for(int i=0; i<poly.Length - 1; ++i)
        {
            newString += poly[i];
            string pair = poly.Substring(i,2);
            if (Rules.ContainsKey(pair))
            {
                newString += Rules[pair];
            }
        }
        newString += poly[poly.Length - 1];
        return newString;
    }

    
    public int Score(string poly)
    {
        Dictionary<char,int> counts = new Dictionary<char,int>();
        foreach(char c in poly)
        {
            if (!counts.ContainsKey(c)) counts[c] = 0;
            counts[c]++;
        }

        char maxKey = ' ';
        char minKey = ' ';
        int max = 0;
        int min = 99999999;
        foreach(var kp in counts)
        {
            if(kp.Value > max) { max = kp.Value; maxKey = kp.Key; }
            if(kp.Value < min) { min = kp.Value; minKey = kp.Key; }
        }
        Console.WriteLine($"Min [{minKey},{min}], Max [{maxKey},{max}]");
        return max - min;
    }

    public string RunA()
    {
        Console.WriteLine($"{TestName}: {Rules.Count}");
        string result = Poly;
        for(int i=0;i<10;++i)
        {
            result = ApplyRules(result);
            Console.Write($"{i} ");
            Score(result);
        }



        return $"score: {Score(result)}";
    }

    public string RunB()
    {
        Console.WriteLine($"{TestName}: {Rules.Count}");

        string result = Poly;
        for(int i=0;i<40;++i)
        {
            result = ApplyRules(result);
        }
        return $"score: {Score(result)}";
    }
}
}