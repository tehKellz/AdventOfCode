using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
class Day08_21 : CodeTest
{
    public string TestName = "2021/Day08";
    public bool Enabled => true;
    
    class Command : Utils.IValue
    {
        public Dictionary<string,int> Codes = new Dictionary<string,int>();
        public string[] ValueToCode = new string[10]; // 

        public string[] Display = new string[4];
        public int Value = -1;

        public override string ToString()
        {
            return $"'{String.Join(", ", Display)}' = {Value} [Key: {String.Join(", ", ValueToCode)}]";
        }

        public void FromString(string[] line) 
        {
            for(int i=0;i<line.Length;++i)
                line[i] = String.Concat(line[i].OrderBy(c => c));

            // Pick the easy ones
            for(int i=0;i<10;++i)
            {
                string code = line[i];
                switch(code.Length)
                {
                    case 2:
                        Codes[code] = 1; ValueToCode[1] = code;
                        break;
                    case 3:
                        Codes[code] = 7; ValueToCode[7] = code;
                        break;
                    case 4:
                        Codes[code] = 4; ValueToCode[4] = code;
                        break;
                    case 7:
                        Codes[code] = 8; ValueToCode[8] = code;
                        break;
                    default:
                        Codes[code] = -1; 
                        break;
                }
            }

            // First Pass
            // 5 seg and contains 1 is a 3
            // 6 seg and no 1 is a 6, contains 4 then it is 9 else 0
            for(int i=0;i<10;++i)
            {
                string code = line[i];
                if (Codes[code] != -1) continue;

                if (code.Length == 5 && Contains(code,ValueToCode[1]))
                {
                    Codes[code] = 3; ValueToCode[3] = code;
                }
                if (code.Length == 6)
                {
                    if (!Contains(code,ValueToCode[1]))
                    {
                        Codes[code] = 6; ValueToCode[6] = code;
                    }
                    else if (Contains(code,ValueToCode[4]))
                    {
                        Codes[code] = 9; ValueToCode[9] = code;
                    }
                    else
                    {
                        Codes[code] = 0; ValueToCode[0] = code;
                    }
                }
            }

            // 5 seg and 6 contains it, it is a 5 else 2
            for(int i=0;i<10;++i)
            {
                string code = line[i];
                if (Codes[code] != -1) continue;

                if(code.Length == 5)
                {
                    if(Contains(ValueToCode[6],code))
                    {
                        Codes[code] = 5; ValueToCode[5] = code;
                    }
                    else
                    {
                        Codes[code] = 2; ValueToCode[2] = code;
                    }
                }
            }

            Value = 0;
            // The display values
            for(int i=0;i<4;++i)
            {
                Display[i] = line[i+11];
                Value = Value * 10;
                Value += Codes[Display[i]];
            }

            foreach(var c in Codes)
            {
                if(c.Value == -1) Console.WriteLine($"Failed to find value for {c.Key}");
            }
        }

        bool Contains(string big, string small)
        {
            foreach(char c in small)
                if (big.IndexOf(c) == -1) return false;
            return true;
        }
    }

    private List<Command> Data = new List<Command>();
    
    public void Init() 
    {
        Utils.LoadValues($"{TestName}.input", Data);
    }

    public string RunA()
    {
        int count = 0;
        foreach(var c in Data)
        {
            foreach(var d in c.Display)
            {
                switch(d.Length)
                {
                    case 2:
                    case 3:
                    case 4:
                    case 7:
                        count++;
                        break;
                    default: break;
                }

            }
        }

        return $"Total 1,4,7,8: {count}";
    }

    public string RunB()
    {
        Int64 sum = 0;
        foreach(var d in Data) sum += d.Value;

        return $"Sum is {sum}";
    }
}
}