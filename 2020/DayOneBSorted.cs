using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class DayOneBSorted : CodeTest
{
    public bool Enabled => false;
    private List<int> E = new List<int>();

    public string Run()
    {
        Utils.Load("./2020/DayOne.input", E);
        E.Sort();

        int kMax = E.Count;
        for(int i=0; i< E.Count - 2; ++i)
        {
            kMax = E.Count;
            for (int j=i+1; j < E.Count - 1; ++j)
            {
                if (E[i] + E[j] > 2020) break;
                for (int k=j+1; k < kMax; ++k)
                {
                    if (E[i] + E[j] + E[k] == 2020) return (E[i] * E[j] * E[k]).ToString();
                    if (E[i] + E[j] + E[k] > 2020) 
                    {
                        kMax = k;
                        break;
                    }
                }
            }
        }
        
        return "None found";
    }
}
}