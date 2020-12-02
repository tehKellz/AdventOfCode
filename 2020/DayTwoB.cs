using System;

namespace AdventOfCode
{

class DayTwoB : CodeTest
{
    public bool Enabled => true;

    public string Run()
    {
        int total = 0;
        Utils.Load<string>("./2020/DayTwo.input", (s, l) => 
        {
            var t = s.Split(new Char [] {'-', ' ', ':'});
            int min = Int32.Parse(t[0]) - 1;
            int max = Int32.Parse(t[1]) - 1;
            char letter = t[2][0];
            string pw = t[4];

            if (pw[min] == letter ^ pw[max] == letter) total++;

            return false;
        });

        return total.ToString();
    }
}
}