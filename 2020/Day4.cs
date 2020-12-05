using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day4 : CodeTest
{
    public bool Enabled => false;

    public class Record
    {
        public Dictionary<string,string> Fields = new Dictionary<string,string>();
            
        public void Add(string s)
        {
            var parsed = s.Split(new Char[]{':',' '});
            for(int i=0; i < parsed.Length; i += 2)
            {
                Fields[parsed[i]] = parsed[i+1];
            }
        }

        public bool IsValidA()
        {
            return Fields.ContainsKey("byr")
                    && Fields.ContainsKey("iyr")
                    && Fields.ContainsKey("eyr")
                    && Fields.ContainsKey("hgt")
                    && Fields.ContainsKey("hcl")
                    && Fields.ContainsKey("ecl")
                    && Fields.ContainsKey("pid");
        }

        public bool IsValidB()
        {
            return IsValidA()
                && IsValidByr() 
                && IsValidIyr()
                && IsValidEyr()
                && IsValidHgt()
                && IsValidHcl()
                && IsValidEcl()
                && IsValidPid();
        }

        // pid (Passport ID) - a nine-digit number, including leading zeroes.
        bool IsValidPid()
        {
            if (Fields["pid"].Length != 9) return false;
            return true;
        }

        // ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
        List<string> EyeColors = new List<string>(){"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
        bool IsValidEcl()
        {
            return EyeColors.Contains(Fields["ecl"]);
        }

        // hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
        string Hex = "0123456789abcdef#";
        bool IsValidHcl()
        {
            string s = Fields["hcl"];
            if (s[0] != '#' || s.Length != 7) return false;

            foreach (char c in s) if (!Hex.Contains(c)) return false;

            return true;
        }

        bool IsValidYear(string s, int min, int max)
        {
            if (s.Length != 4) return false;
            int year = Int32.Parse(s);
            return year >= min && year <= max;
        }
        // byr (Birth Year) - four digits; at least 1920 and at most 2002.
        bool IsValidByr() { return IsValidYear(Fields["byr"],1920,2002); }
        // iyr (Issue Year) - four digits; at least 2010 and at most 2020.
        bool IsValidIyr() { return IsValidYear(Fields["iyr"],2010,2020); }
        // eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
        bool IsValidEyr() { return IsValidYear(Fields["eyr"],2020,2030); }

        bool IsValidHgt()
        {
            String s = Fields["hgt"];
            string m = s.Substring(s.Length - 2);
            string i = s.Substring(0, s.Length - 2);
            if (m == "cm")
            {
                int hgt = Int32.Parse(i); 
                return hgt >= 150 && hgt <= 193;
            }
            else if (m == "in")
            {
                int hgt = Int32.Parse(i);
                return hgt >= 59 && hgt <= 76;
            }
            return false;
        }
    }

    public List<Record> Records = new List<Record>();

    public void Init() 
    {
        Record curRecord = new Record();
        Utils.Load<string>("./2020/Day4.input", (s, l) => 
        {
            if (s != "")
            {
                curRecord.Add(s);
            }
            else
            {
                Records.Add(curRecord);
                curRecord = new Record();
            }
            return false;
        });

        Records.Add(curRecord);
    }

    public string RunA()
    {
        int validCount = 0;
        foreach (var r in Records)
        {
            if (r.IsValidA()) validCount++;
        }

        return validCount.ToString();
    }

    public string RunB()
    {
        int validCount = 0;
        foreach (var r in Records)
        {
            if (r.IsValidB()) validCount++;
        }

        return validCount.ToString();
    }
}
}