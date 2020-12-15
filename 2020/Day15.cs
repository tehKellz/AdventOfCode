using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
class Day15 : CodeTest
{
    public bool Enabled => true;

    public void Init() 
    {
    }

    public string RunA()
    {
        List<int> input = new List<int>()
            {13,0,10,12,1,5,8};
            //{2,1,3}; // should be 10?

        while (input.Count < 2020)
        {
            int index = input.LastIndexOf(input.Last(),input.Count - 2);
            if (index < 0)
            {
                input.Add(0);
            }
            else
            {
                //Console.WriteLine($"Found it at index {index}");
                input.Add((input.Count - 1) - index);
            }
        }

        return $"{input.Last()}";
    }
// Given 0,3,6, the 30000000th number spoken is 175594.
// Given 1,3,2, the 30000000th number spoken is 2578.
//
    public string RunB()
    {
        Dictionary<int,int> input = new Dictionary<int,int>()
            { {13,0}, {0,1}, {10,2}, {12,3}, {1,4}, {5,5}}; //, {8,6} };
            //{ {1,0}, {3,1} }; // , {6,2} }
            //{ {2,0} , {1,1}}; // 3
        int curr = input.Count;
        int last = 8;
        //string output = "2,1";
        while (curr < 29999999)
        {
            int lastIndex;
            if (input.TryGetValue(last, out lastIndex))
            {
                input[last] = curr;
                //output += "," + last;
                //Console.WriteLine($"{last} {curr}");
                last = (curr ) - lastIndex;
            }
            else
            {
                input[last] = curr;
                //output += "," + last;
                //Console.WriteLine($"{last} {curr}");
                last = 0;
            }
            ++curr;
        }

        //Console.WriteLine($"Last {input.Last()}");
        //Console.WriteLine($"{output}");

        return $"{last}";

    }
}
}