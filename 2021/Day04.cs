using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day04_21 : CodeTest
{
    public string TestName = "2021/Day04";
    public bool Enabled => false;
    
    class Board
    {
        public int[,] Cells = new int[5, 5];
        public bool[,] Marked = new bool[5, 5];
        public bool Won = false;

        public bool BoardWon()
        {
            if (Won) return false;

            for(int r = 0; r<5; ++r)
            {
                if (Marked[r,0] == true 
                    && Marked[r,1] == true 
                    && Marked[r,2] == true 
                    && Marked[r,3] == true 
                    && Marked[r,4] == true) 
                {
                    Won = true;
                    return true;
                }

                if (Marked[0,r] == true 
                    && Marked[1,r] == true 
                    && Marked[2,r] == true 
                    && Marked[3,r] == true 
                    && Marked[4,r] == true)
                {
                    Won = true;
                    return true;
                }
            }
            return false;
        }

        public bool Mark(int v)
        {
            for(int r=0;r<5;++r)
                for(int c=0;c<5;++c)
                {
                    if (Cells[r,c] == v) Marked[r,c] = true;
                }
            return BoardWon();
        }

        public int GetScore()
        {
            int sum = 0;
            for(int r=0;r<5;++r)
                for(int c=0;c<5;++c)
                {
                    if (!Marked[r,c]) sum += Cells[r,c];
                }
            return sum;
        }
    }
    
    private List<Board> Boards = new List<Board>();
    private List<int> Turns = new List<int>();
    public void Init() 
    {
        int currBoard = 0;
        int currRow = 0;
        Boards.Add(new Board());
        Utils.Load($"{TestName}.input", (string s,int n) =>
        {
            if (String.IsNullOrEmpty(s))
            {
                ++currBoard;
                Boards.Add(new Board());
                currRow = 0;
                return false;
            }

            string[] row = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for(int i=0;i<5;++i)
                Boards[currBoard].Cells[currRow, i] = Int32.Parse(row[i]);
            
            ++currRow;
            return false;
        });

        Utils.LoadCSV($"{TestName}.turns",Turns);

        Console.WriteLine($"Loaded Boards:{Boards.Count} Turns:{Turns.Count}");
    }

    public string RunA()
    {
        foreach(int t in Turns)
            foreach(var b in Boards)
            {
                if (b.Mark(t))
                {
                    return $"Turn:{t} BoardScore:{b.GetScore()} Final:{b.GetScore() * t}";
                }
            }
        return "No winner found.";
    }

    public string RunB()
    {
        int wonBoards = 0;
        foreach(int t in Turns)
            foreach(var b in Boards)
            {
                if (b.Mark(t))
                {
                    ++wonBoards;
                    if (wonBoards == (Boards.Count - 1))
                        return $"Turn:{t} BoardScore:{b.GetScore()} Final:{b.GetScore() * t}";
                }
            }
        return "No last winner";
    }
}
}