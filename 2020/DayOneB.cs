using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class DayOneB : CodeTest
{
    public bool Enabled => true;
    private List<int> Expenses = new List<int>();

    public string Run()
    {
        string[] lines = System.IO.File.ReadAllLines(@"./2020/DayOne.input");
        foreach(var l in lines)
        {
            Expenses.Add(Int32.Parse(l));
        }

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