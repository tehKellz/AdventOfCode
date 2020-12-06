using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdventOfCode
{

public static class Utils
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

    // void perLine(T1 line) // always processes all lines
    static public void Load<T1>(string fileName, Action<T1> perLine)
    {
        Load<T1>(fileName, (l,n) => { perLine(l); return false; });
    }

    // void perLine(T1 line, int lineNum) // always processes all lines
    static public void Load<T1>(string fileName, Action<T1,int> perLine)
    {
        Load<T1>(fileName, (l,n) => { perLine(l,n); return false; });
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

    // Puts grouped lines (separated by a blank line) into a list together.
    static public void Load<T>(string fileName, List<List<T>> output)
    {
        List<T> currentGroup = new List<T>();
        Utils.Load<string>(fileName, (s, n) => {
            if (s.Length == 0)
            {
                output.Add(currentGroup);
                currentGroup = new List<T>();
            }
            else
            {
                currentGroup.Add((T)Convert.ChangeType(s, typeof(T)));
            }
            return false;
        });
        output.Add(currentGroup);
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

//
// Formatted splitting/parsing
//
public class StringView
{
    public StringView(String str)
    {
        _base = str;
        _length = str.Length;
    }

    private String _base;
    private int _start = 0;
    private int _length = 0;
    public int Length 
    {
        get
        {
            return _length;
        }
    }

    public override string ToString()
    {
        return _base.Substring(_start,_length);
    }

    public StringView Substring(int start, int length = Int32.MaxValue)
    {
        StringView stv = new StringView(_base);
        stv._start = _start + start;
        stv._length = length;
        if (stv._start >= _base.Length) stv._start = _base.Length - 1;
        if (stv._length > _base.Length) stv._length = _base.Length - stv._start;

        return stv;
    }

    public int IndexOf(string token)
    {
        return _base.IndexOf(token,_start,_length) - _start;
    }

    private int SplitInternal<T1>(out T1 v1, string D1 = null)
    {
        try
        {
            if (D1 != null)
            {
                int i1 = IndexOf(D1);
                if (i1 >= 0)
                {
                    v1 = (T1)Convert.ChangeType(Substring(0,i1).ToString(), typeof(T1));
                    return i1;
                }
            }
            else
            {
                v1 = (T1)Convert.ChangeType(ToString(), typeof(T1));
                return Length - 1;
            }
        }
        catch (Exception)
        {
            v1 = default(T1);
            return -1;
        }

        v1 = default(T1);
        return -1;
    }

    public bool Split<T1>(out T1 v1, string D1 = null)
    {
        return SplitInternal<T1>(out v1, D1) != -1;
    }

    public bool Split<T1,T2>(out T1 v1, string D1, out T2 v2, string D2 = null)
    {
        int index = SplitInternal(out v1, D1);
        if (index != -1)
        {
            return Substring(index + D1.Length).Split(out v2, D2);
        }
        v2 = default(T2);
        return false;
    }

    public bool Split<T1,T2,T3>(out T1 v1, string D1, out T2 v2, string D2, out T3 v3, string D3 = null)
    {
        int index = SplitInternal(out v1, D1);
        if (index != -1)
        {
            return Substring(index + D1.Length).Split(out v2, D2, out v3, D3);
        }
        v2 = default(T2);
        v3 = default(T3);
        return false;
    }

    public bool Split<T1,T2,T3,T4>(out T1 v1, string D1, out T2 v2, string D2, out T3 v3, string D3, out T4 v4, string D4 = null)
    {
        int index = SplitInternal(out v1, D1);
        if (index != -1)
        {
            return Substring(index + D1.Length).Split(out v2, D2, out v3, D3, out v4, D4);
        }
        v2 = default(T2);
        v3 = default(T3);
        v4 = default(T4);
        return false;
    }

    public bool Split<T1,T2,T3,T4,T5>(out T1 v1, string D1, out T2 v2, string D2, out T3 v3, string D3, out T4 v4, string D4, out T5 v5, string D5 = null)
    {
        int index = SplitInternal(out v1, D1);
        if (index != -1)
        {
            return Substring(index + D1.Length).Split(out v2, D2, out v3, D3, out v4, D4, out v5, D5);
        }
        v2 = default(T2);
        v3 = default(T3);
        v4 = default(T4);
        v5 = default(T5);
        return false;
    }
}
}