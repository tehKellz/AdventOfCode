using System;

namespace AdventOfCode
{
class Day2 : CodeTest
{
    public bool Enabled => true;
    public void Init() {}
    public string RunA()
    {
        int total = 0;
        Utils.Load<string>("./2020/Day2.input", (s, l) => 
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

    public string RunB()
    {
        int total = 0;
        Utils.Load<string>("./2020/Day2.input", (s, l) => 
        {
            StringView stv = new StringView(s);
            stv.Split(out int min, "-", out int max, " ", out char letter, ": ", out string pw);
            if (pw[min - 1] == letter ^ pw[max - 1] == letter) total++;

            return false;
        });

        return total.ToString();
    }
}
}