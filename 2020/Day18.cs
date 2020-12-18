using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
class Day18 : CodeTest
{
    public bool Enabled => true;

    List<List<string>> Problems = new List<List<string>>();

    public List<string> ParseProblem(string s)
    {
        List<string> x = new List<string>(Regex.Split(s,"([\\*\\+() ])"));
        x.RemoveAll(string.IsNullOrWhiteSpace);
        return x;
    }

    public void Init() 
    {
        Utils.Load<string>("2020/Day18.input",(s,n)=>
        {
            Problems.Add(ParseProblem(s));
        });
    }



    Int64 Solve(List<string> p)
    {
        Int64 a = 0;
        Action<Int64> op = (o) => {a = o;};
        int i= 0;
        while(p.Count > 0 && i < p.Count)
        {
            switch(p[i])
            {
            case "+":
                op = (o) => { a += o;};
                ++i;
                break;
            case "*":
                op = (o) => { a *= o;};
                ++i;
                break;
            case "(":
                {
                    int deep = 0;
                    int j = i;
                    for(j=i; j<p.Count;++j)
                    {
                        if (p[j] == "(") ++deep;
                        else if (p[j] == ")") --deep;

                        if (deep == 0) break;
                    }
                    op(Solve(p.GetRange(i+1,(j-i)-1)));
                    p.RemoveRange(i,(j-i) + 1);
                }
                break;
            case ")": // error
                Console.WriteLine("Parse fail, unexpected )");
                break;
            default: // Num
                op(Int64.Parse(p[i]));
                ++i;
                break;
            }
            
        }

        return a;
    }

    public string RunA()
    {
        Int64 sum = 0;
        foreach(var p in Problems)
        {
            sum += Solve(p);
        }
        
        return $"{sum}";
    }

    public string RunB()
    {
        Console.WriteLine("Hello World B");

        return "Template Output B";
    }
}
}