using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdventOfCode
{

public class Utils
{
    // bool perLine(T1 line, int lineNum) : return true if processing of the file should stop.
    static public bool Load<T1>(string fileName, Func<T1,int,bool> perLine)
    {
        bool done = false;
        int lineNum = 0;
        string[] lines = System.IO.File.ReadAllLines(fileName);
        foreach(var l in lines)
        {
            try
            {
                done = perLine((T1)Convert.ChangeType(l, typeof(T1)), lineNum);
                if (done) break;
            }
            catch(Exception)
            {
                Console.WriteLine($"Failed to parse line: {l} to {typeof(T1).ToString()}>");
            }
            lineNum++;
        }

        return done;
    }

    // bool perLine(T1 line) : return true if processing of the file should stop.
    static public bool Load<T1>(string fileName, Func<T1,bool> perLine)
    {
        return Load<T1>(fileName, (l,n) => { return perLine(l); });
    }

    static public bool Load<T1, T2>(string fileName, Func<T1, T2, bool> perLine, string seperator = ",")
    {
        return Load<string>(fileName, (s) => {
            string[] split = s.Split(seperator);
            if (split.Length == 2)
            {
                try
                {
                    return perLine(
                        (T1)Convert.ChangeType(split[0], typeof(T1)),
                        (T2)Convert.ChangeType(split[1], typeof(T2)));
                }
                catch
                {
                    Console.WriteLine($"Failed to parse line: {s} to <{typeof(T1).ToString()},{typeof(T2).ToString()}>");
                }
            }
            else
            {
                Console.WriteLine($"Failed to parse line: {s} to <{typeof(T1).ToString()},{typeof(T2).ToString()}>");
            }
            return false;
        });
    }

    static public void Load<T>(string fileName, List<T> output)
    {
        LoadCSV<T>(fileName, (v) => {
            output.Add(v);
            return false;
        });
    }

    static public void Load<K,V>(string fileName, Dictionary<K,V> output, string seperator = ",")
    {
        Load<K,V>(fileName, (k,v) => {
            output[k] = v;
            return false;
        }, seperator);
    }

    // bool perElement(T1 element, int lineNum, int index) : return true if processing of the file should stop.
    static public bool LoadCSV<T1>(string fileName, Func<T1, int, int, bool> perElement, string seperator = ",")
    {
        bool done = false;
        int lineNum = 0;
        return Load<string>(fileName, (line) =>
        {
            string[] split = line.Split(seperator);
            int index = 0;
            foreach(var s in split)
            {
                try
                {
                    done = perElement((T1)Convert.ChangeType(s, typeof(T1)), lineNum, index);
                    if (done) break;
                }
                catch(Exception)
                {
                    Console.WriteLine($"Failed to parse element: {s} to {typeof(T1).ToString()}>");
                }
                ++index;
            }
            ++lineNum;
            return done;
        });
    }

    // bool perLine(T1 element) : return true if processing of the file should stop.
    static public bool LoadCSV<T1>(string fileName, Func<T1,bool> perElement, string seperator = ",")
    {
        return LoadCSV<T1>(fileName, (e, n, i) => { return perElement(e); }, seperator);
    }

    static public void LoadCSV<T1>(string fileName, List<T1> output, string seperator = ",")
    {
        LoadCSV<T1>(fileName, (e, n, i) => { output.Add(e); return false; }, seperator);
    }

    static public void LoadCSV<T1>(string fileName, List<List<T1>> output, string seperator = ",")
    {
        List<T1> interim = null;
        int curLine = -1;
        LoadCSV<T1>(fileName, (e, n, i) => {
            if (curLine != n)
            {
                if (interim != null)
                {
                    output.Add(interim);
                }
                interim = new List<T1>();
                curLine = n;
            }
            interim.Add(e);
            return false;
        }, seperator);
        if (interim != null) output.Add(interim);
    }
}
}