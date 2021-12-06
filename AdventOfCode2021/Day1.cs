using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day1
    {
        public int Main(string part, string path)
        {
            int answer = 0;
            if (part == "a")
            {
                answer = Day1a(path);
            }

            if (part == "b")
            {
                answer = Day1b(path);
            }

            return answer;
        }

        public int Day1a(string path)
        {
            int increased = 0;
            int decreased = 0;
            List<int> measurements = new List<int>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    measurements.Add(int.Parse(line));
                }
            }

            for(int i = 1; i < measurements.Count; i++)
            {
                if(measurements[i] < measurements[i-1]) { decreased++; } else { increased++; }
            }

            return increased;
        }

        public int Day1b(string path)
        {
            int increased = 0;
            List<int> measurements = new List<int>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    measurements.Add(int.Parse(line));
                }
            }

            for (int i = 1; i < measurements.Count - 2; i++)
            {
                if (measurements[0 + i] + measurements[1 + i] + measurements[2 + i] > measurements[-1 + i] + measurements[0 + i] + measurements[1 + i])
                {
                    increased++;
                }
            }

            return increased;
        }
    }
}
