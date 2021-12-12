using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day11_21 : CodeTest
{
    public string TestName = "2021/Day11";
    public bool Enabled => false;
    
    private int[,] Data = new int[10,10];
    public void Init() 
    {
        Utils.Load($"{TestName}.input", (string l, int n) =>
        {
            for (int i=0;i<10;++i) Data[i,n] = Int32.Parse($"{l[i]}");
        });
    }

    private void Inc(int x, int y)
    {
        if (x < 0 || x > 9 || y < 0 || y > 9) return;
        if (Data[x,y] == -1) return;
        Data[x,y]++;
    }

    private bool Flash(int x, int y)
    {
        if (Data[x,y] == -1) return false; // already flashed
        else if (Data[x,y] > 9)
        {   // flash
            Data[x,y] = -1;
            for(int dx = -1; dx < 2; ++dx)
                for (int dy = -1; dy < 2; ++dy) Inc(x+dx,y+dy);
            return true;
        }
        return false;
    }

    private int Step()
    {
        // inc
        for(int x=0;x<10;++x)
            for(int y=0;y<10;++y)
                Data[x,y]++;

        // flash
        bool changed = true;
        while(changed)
        {
            changed = false;
            for(int x=0;x<10;++x)
                for(int y=0;y<10;++y)
                    changed |= Flash(x,y);
        }

        // reset+count
        int count = 0;
        for(int x=0;x<10;++x)
            for(int y=0;y<10;++y)
            {
                if (Data[x,y] == -1)
                {
                    Data[x,y] = 0;
                    count++;
                }
            }

        return count;
    }

    public string RunA()
    {
        int score = 0;
        for(int s = 0; s<100;++s) score += Step();
        return $"Score {score}";
    }

    public string RunB()
    {
        Init();
        int score = 1;
        while(Step() != 100) ++score;
        return $"Score {score}";
    }
}
}