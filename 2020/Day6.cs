using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day6 : CodeTest
{
    public bool Enabled => false;

    public void Init() 
    {
    }

    string alpha = "abcdefghijklmnopqrstuvwxyz";
    public string RunA()
    {
        int total = 0;
        string current = "";
        Utils.Load<string>("2020/Day6.input", (s) => {
            if (s.Length == 0)
            {
                foreach(char c in alpha)
                {
                    if (current.Contains(c)) ++total;
                }
                current = "";
            }
            current += s;
        });

        foreach(char c in alpha)
        {
            if (current.Contains(c)) ++total;
        }
      
        return total.ToString();
    }

    public string RunB()
    {
        List<List<string>> AllGroups = new List<List<string>>();
        Utils.Load("2020/Day6.input", AllGroups);

        int total = 0;
        foreach (List<string> family in AllGroups)
        {
            foreach(char c in family[0])
            {
                bool allYes = true;
                foreach(string member in family)
                {
                    if (!member.Contains(c))
                    {
                        allYes = false;
                        break;
                    }
                }

                if (allYes)  ++total;
            }
        }

        return total.ToString();
    }
}
}