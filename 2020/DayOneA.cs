using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class DayOneA : CodeTest
{
    public bool Enabled => true;
    private List<int> Expenses = new List<int>();

    public string Run()
    {
        Utils.Load("./2020/DayOne.input", Expenses);

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
}
}