using System;

namespace AdventOfCode
{

public static class MyExt
{

}

class DayTwoB : CodeTest
{
    public bool Enabled => true;

    public string Run()
    {
        int total = 0;
        Utils.Load<string>("./2020/DayTwo.input", (s, l) => 
        {
            Utils.StringView stv = new Utils.StringView(s);
            stv.Split(out int min, "-", out int max, " ", out char letter, ": ", out string pw);
            if (pw[min - 1] == letter ^ pw[max - 1] == letter) total++;

            return false;
        });

        return total.ToString();
    }
}
}