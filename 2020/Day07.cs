using System;
using System.Collections.Generic;

namespace AdventOfCode
{
class Day7 : CodeTest
{
    public bool Enabled => false;

    class Bag
    {
        public Bag(string name) { Name = name; }
        public string Name = "";
        public Dictionary<Bag, int> Contents = new Dictionary<Bag, int>();

        public override int GetHashCode() { return Name.GetHashCode(); }
        public override bool Equals(object obj) { return Equals(obj as Bag); }
        public bool Equals(Bag obj) { return obj != null && obj.Name == this.Name; }
    }

    HashSet<Bag> Rules = new HashSet<Bag>();
    Bag ShinyBag = null;

    Bag GetBag(string name)
    {
        Bag b;
        if (!Rules.TryGetValue(new Bag(name), out b))
        {
            b = new Bag(name);
            Rules.Add(b);
            if (name == "shiny gold bag") ShinyBag = b;
        }
        return b;
    }

    public void Init() 
    {
        Utils.Load<string>("2020/Day07.input", (s, l) =>
        {
            s = s.Replace("bags", "bag").Replace("bag.", "bag");

            int i = s.IndexOf("contain");
            Bag b = GetBag(s.Substring(0, i - 1));

            s = s.Substring(i + 8);
            if (!s.Contains("no other"))
            {
                string[] contents = s.Split(", ");
                foreach(string c in contents)
                {
                    int d = c.IndexOf(" ");
                    Bag inB = GetBag(c.Substring(d+1));
                    b.Contents[inB] = Int32.Parse(c.Substring(0,d));
                }
            }

            return false;
        });
    }

    bool CanHoldGold(Bag bag)
    {
        if (bag.Contents.ContainsKey(ShinyBag)) return true;

        foreach(var contents in bag.Contents)
        {
            if (CanHoldGold(contents.Key)) return true;
        }
        return false;
    }

    public string RunA()
    {
        int total = 0;
        foreach(var kvp in Rules)
        {
            if (CanHoldGold(kvp)) ++total;
        }

        return total.ToString();
    }

    int NumInside(Bag b)
    {
        int total = 0;
        foreach(var kvp in b.Contents)
        {
            int inside1 = NumInside(kvp.Key);
            total += (inside1 * kvp.Value);
        }
        return total + 1;
    }

    public string RunB()
    {
        return (NumInside(ShinyBag) - 1).ToString();
    }
}
}