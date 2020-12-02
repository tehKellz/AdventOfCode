using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class DayOneBSorted : CodeTest
{
    public bool Enabled => true;
    private List<int> E = new List<int>();

    public string Run()
    {
        Utils.Load("./2020/DayOne.input", E);
        E.Sort();
        int l = 0;
        int m = 1;
        int h = E.Count - 1;
        while(true)
        {
            int sum = E[l] + E[m] + E[h];
            if (sum == 2020) return (E[l] * E[m] * E[h]).ToString();
            
            // Too large
            if (sum > 2020)
            {
                // If low and mid index are already adjacent (can't be smaller) decrement high index
                if (m == l + 1)
                {
                    --h;
                }
                // Not adjacent low and mid, looking for a higher number
                // increment the lower and recet mid to next up.
                else
                {
                    l++;
                    m = l + 1;
                }
            }

            // Too small
            else // if (sum < 2020)
            {
                // too small but can't make mid any larger, bump low up one and reset mid to next above low.
                if (m == h - 1)
                {
                    l++;
                    m = l + 1;
                }
                // too small but mid could be larger
                else
                {
                    m++;
                }
            }
        }

        return "None found";
    }
}
}