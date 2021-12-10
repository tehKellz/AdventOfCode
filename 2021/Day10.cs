using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day10_21 : CodeTest
{
    public string TestName = "2021/Day10";
    public bool Enabled => true;
    private List<string> Data = new List<string>();
    
    public void Init() 
    {
        Utils.Load($"{TestName}.input", Data);
    }

    public int CheckLine(string s)
    {
        Stack<char> stack = new Stack<char>();
        foreach(char c in s)
        {
            switch (c)
            {
                case '(':
                case '{':
                case '[':
                case '<':
                    stack.Push(c);
                    break;
                case ')': if (stack.Pop() != '(') return 3;     else break;
                case ']': if (stack.Pop() != '[') return 57;    else break;
                case '}': if (stack.Pop() != '{') return 1197;  else break;
                case '>': if (stack.Pop() != '<') return 25137; else break;
                default: Console.WriteLine($"Unexpected char {c}");  break;
            }
        }
        return 0;
    }

    public string RunA()
    {
        Int64 score = 0;
        foreach(var s in Data) score += CheckLine(s);
        
        return $"Score {score}";
    }

    public Int64 CompleteLine(string s)
    {
        Stack<char> stack = new Stack<char>();
        for(int i=0; i<s.Length;++i)
        {
            char c = s[i];
            switch (c)
            {
                case '(':
                case '{':
                case '[':
                case '<':
                    stack.Push(c);
                    break;
                case ')': if (stack.Pop() != '(') return 0; else break;
                case ']': if (stack.Pop() != '[') return 0; else break;
                case '}': if (stack.Pop() != '{') return 0; else break;
                case '>': if (stack.Pop() != '<') return 0; else break;
                default: Console.WriteLine($"Unexpected char {c}"); break;
            }
        }

        Int64 score = 0;
        while(stack.Count > 0)
        {
            score = score*5;
            char sc = stack.Pop();
            switch(sc)
            {
                case '(': score += 1; break;
                case '[': score += 2; break;
                case '{': score += 3; break;
                case '<': score += 4; break;
            }
        }
        return score;
    }

    public string RunB()
    {
        List<Int64> scores = new List<Int64>();
        foreach(var s in Data)
        {
            Int64 score = CompleteLine(s);
            if (score > 0) scores.Add(score);
        }
        
        scores.Sort();
        return $"Score {(scores.Count / 2)}/{scores.Count} = {scores[(scores.Count / 2)]}";
    }
}
}