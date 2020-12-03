using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day1 : CodeTest
{
    public bool Enabled => true;
    private List<int> Expenses = new List<int>();

    public void Init() 
    {
        Utils.Load("./2020/Day1.input", Expenses);
    }

    public string RunA()
    {
        for(int i=0; i< Expenses.Count - 1; ++i)
        {
            for(int j=i + 1; j < Expenses.Count; ++j)
            {
                if (Expenses[i] + Expenses[j] == 2020) 
                    return (Expenses[i] * Expenses[j]).ToString();
            }
        }
        return "None found";
    }

    public string RunB()
    {
        for(int i=0; i< Expenses.Count - 2; ++i)
        {
            for(int j=i + 1; j < Expenses.Count - 1; ++j)
            {
                for(int k =j + 1; k < Expenses.Count; ++k)
                {
                    if (Expenses[i] + Expenses[j] + Expenses[k] == 2020) 
                        return (Expenses[i] * Expenses[j] * Expenses[k]).ToString();
                }
            }
        }
        return "None found";
    }
}
}