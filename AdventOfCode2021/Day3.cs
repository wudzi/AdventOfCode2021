using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day3
    {

        public int Main(string part, string path)
        {
            int answer =0;
            if (part == "a")
            {
                answer = Day3a(path);
            }

            if (part == "b")
            {
                answer = Day3b(path);
            }

            return answer;
        }
        public int Day3b(string path)
        {
            List<string> diags = new List<string>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    diags.Add(line);
                }
            }
            string o2 = Scrubber(diags, "1");
            string co2 = Scrubber(diags, "0");
            return Convert.ToInt32(o2, 2) * Convert.ToInt32(co2, 2);
        }
        public string Scrubber(List<string> diags, string div)
        {
            for (int i = 0; i < 12; i++)
            {
                if (diags.Count == 1) break;
                int a = diags.Count(item => item.Substring(i, 1) == "1");
                int b = diags.Count(item => item.Substring(i, 1) == "0");

                if (a >= b && div == "1")
                {
                    diags = diags.Where(item => item.Substring(i, 1) == "1").ToList();
                }
                else if (a < b && div == "1")
                {
                    diags = diags.Where(item => item.Substring(i, 1) == "0").ToList();
                }
                else if (b <= a && div == "0")
                {
                    diags = diags.Where(item => item.Substring(i, 1) == "0").ToList();
                }
                else if (b > a && div == "0")
                {
                    diags = diags.Where(item => item.Substring(i, 1) == "1").ToList();
                }
            }
            return diags.First();
        }

        public int Day3a(string path)
        {
            string gamma = "";
            string epsilon = "";
            for (int i = 0; i < 12; i++)
            {
                int pos = 0;
                int neg = 0;
                using (TextReader tr = File.OpenText(path))
                {
                    string line;
                    while ((line = tr.ReadLine()) != null)
                    {
                        int thisBit = int.Parse(line.Substring(i, 1));
                        if (thisBit == 0) { neg++; } else { pos++; }
                    }
                }

                if (pos > neg) { gamma += "1"; epsilon += "0"; } else { gamma += "0"; epsilon += "1"; }
            }

            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);

        }
    }
}
