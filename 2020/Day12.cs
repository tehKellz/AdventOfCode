using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day12 : CodeTest
{
    public bool Enabled => false;

    List<string> Orders = new List<string>();
    public void Init() 
    {
        Utils.Load("2020/Day12.input", Orders);
    }

    public string RunA()
    {
        int E = 0;
        int N = 0;
        int D = 0;
        //        N
        //        3
        // <--W 2 x 0 E-->
        //        1
        //        S

        foreach(var o in Orders)
        {
            int v1 = Int32.Parse(o.Substring(1));
            switch(o[0])
            {
                case 'W': E -= v1; break;
                case 'E': E += v1; break;
                case 'S': N -= v1; break;
                case 'N': N += v1; break;
                case 'L':
                    v1 = v1 / 90;
                    D -= v1;
                    while (D < 0) D += 4;
                    break;
                case 'R':
                    v1 = v1 / 90;
                    D += v1;
                    D = D % 4;
                    break;
                case 'F':
                    if (D == 0) E += v1;
                    else if (D == 1) N -= v1;
                    else if (D == 2) E -= v1;
                    else if (D == 3) N += v1;
                    break;
            }
        }
        return $"{N} + {E} = {N + Math.Abs(E)}";
    }

    public string RunB()
    {
        int SE = 0;
        int SN = 0;
        int WE = 10;
        int WN = 1;

        foreach(var o in Orders)
        {
            int v1 = Int32.Parse(o.Substring(1));
            switch(o[0])
            {
                case 'W': WE -= v1; break;
                case 'E': WE += v1; break;
                case 'S': WN -= v1; break;
                case 'N': WN += v1; break;
                case 'L':
                case 'R':
                    if (o == "R90" || o == "L270")
                    {
                        int x = -1 * WE;
                        WE = WN;
                        WN = x;
                    }
                    else if (v1 == 180)
                    {
                        WE = -1 * WE;
                        WN = -1 * WN;
                    }
                    else if (o == "L90" || o == "R270")
                    {
                        int x = -1 * WN;
                        WN = WE;
                        WE = x;
                    }
                    break;
                case 'F':
                    SE += (WE * v1);
                    SN += (WN * v1);
                    break;
            }
        }
        return $"{SN} + {SE} = {Math.Abs(SN) + Math.Abs(SE)}";
    }
}
}