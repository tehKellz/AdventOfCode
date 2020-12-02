using System;

namespace AdventOfCode
{
class DayTwoA : CodeTest
{
    public bool Enabled => true;

    public string Run()
    {
        int total = 0;
        Utils.Load<string>("./2020/DayTwo.input", (s, l) => 
        {
            var t = s.Split(new Char [] {'-', ' ', ':'});
            int min = Int32.Parse(t[0]);
            int max = Int32.Parse(t[1]);
            char letter = t[2][0];
            string pw = t[4];

            int count = 0;
            foreach(char c in pw)
            {
                if (c == letter) ++count;
                if (count > max) break;
            }

            if (count >= min && count <= max) total++;
            //Console.WriteLine($"Got line {l}: {s} {t.Length} {min} {max} {letter} '{pw}'");
            return false;
        });

        return total.ToString();
    }
}
}