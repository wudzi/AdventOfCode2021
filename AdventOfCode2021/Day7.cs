using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{

    public class Day7
    {
        public long Main(string part, string path)
        {
            long answer = 0;
            if (part == "a")
            {
                answer = Day7a(path);
            }

            if (part == "b")
            {
                answer = Day7b(path);
            }
            return answer;
        }

        public long Day7b(string path)
        {
            List<int> crabs = Populate(path);

            int best = -1;
            for (int j = crabs.Min(); j <= crabs.Max(); j++)
            {
                int run = 0;
                for (int i = 0; i < crabs.Count; i++)
                {
                    run += FuelBurned(Math.Abs(j - crabs[i]));
                }

                if (run < best || best < 0)
                {
                    best = run;
                }
            }
            return best;
        }

        public int FuelBurned(int fuel)
        {
            return (fuel * (fuel + 1)) / 2;
        }

        public long Day7a(string path)
        {
            List<int> crabs = Populate(path);

            int best = -1;
            for (int j = crabs.Min(); j <= crabs.Max(); j++)
            {
                int run = 0;
                for (int i = 0; i < crabs.Count; i++)
                {
                    run += Math.Abs(j - crabs[i]);
                }

                if (run < best || best < 0)
                {
                    best = run;
                }
            }
            return best;
        }

        public List<int> Populate(string path)
        {
            List<int> crabs = new List<int>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    string[] parts = line.Split(new char[] { ',' });
                    foreach (string part in parts)
                    {
                        int pos = int.Parse(part);
                        crabs.Add(pos);
                    }
                }
            }
            return crabs;
        }
    }
}