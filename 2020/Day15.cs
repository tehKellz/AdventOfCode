using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
class Day15 : CodeTest
{
    public bool Enabled => true;

    public void Init() {}

    public string RunA()
    {
        List<int> input = new List<int>(){13,0,10,12,1,5,8};

        while (input.Count < 2020)
        {
            int index = input.LastIndexOf(input.Last(),input.Count - 2);
            if (index < 0) input.Add(0);
            else input.Add((input.Count - 1) - index);
        }

        return $"{input.Last()}";
    }

    public string RunB()
    {
        Dictionary<int,int> input = new Dictionary<int,int>()
            { {13,0}, {0,1}, {10,2}, {12,3}, {1,4}, {5,5}}; //, {8,6} };

        int currTurn = input.Count;
        int currNumber = 8;

        while (currTurn < 29999999) // 1 less than 30mil since we want our value in last.
        {
            int nextNum = 0;
            if (input.TryGetValue(currNumber, out int lastIndex))
                nextNum = currTurn - lastIndex;

            input[currNumber] = currTurn++;
            currNumber = nextNum;
        }

        return $"{currNumber} size:{input.Count}";
    }
}
}