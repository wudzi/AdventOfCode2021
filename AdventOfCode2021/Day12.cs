using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day12
    {
        public long Main(string part, string path)
        {

            long answer = 0;
            if (part == "a")
            {
                answer = Day12a(path);
            }

            if (part == "b")
            {
                answer = Day12b(path);
            }
            return answer;
        }

        public long Day12a(string path)
        {
            IDictionary<string, List<string>> caveMappings = new Dictionary<string, List<string>>();
            caveMappings = Populate(path);
            List<string> collectedPaths = new List<string>();
            CaveMappings(caveMappings, "start", "start", ref collectedPaths);
            return collectedPaths.Count;
        }
        public long Day12b(string path)
        {
            IDictionary<string, List<string>> caveMappings = new Dictionary<string, List<string>>();
            caveMappings = Populate(path);
            List<string> collectedPaths = new List<string>();
            CaveMappings2(caveMappings, "start", "start", ref collectedPaths, false);
            return collectedPaths.Count;
        }

        public void CaveMappings(IDictionary<string, List<string>> caveMappings, string position, string path, ref List<string> collectedPaths)
        {
            if (position == "end")
            {
                collectedPaths.Add(path);
                return;
            }

            List<string> nextNodes = caveMappings[position];

            foreach (string nextNode in nextNodes)
            {
                if ((nextNode == "start") || (!IsUpper(nextNode) && nextNode != "end" && path.Contains(nextNode)))
                {
                    continue;
                }
                else
                {
                    CaveMappings(caveMappings, nextNode, path + ", " + nextNode, ref collectedPaths);
                }
            }
        }

        public void CaveMappings2(IDictionary<string, List<string>> caveMappings, string position, string path, ref List<string> collectedPaths, bool visitedPreviously)
        {
            if (position == "end")
            {
                collectedPaths.Add(path);
                return;
            }

            bool atVisitLimit = false;
            if (visitedPreviously)
            {
                atVisitLimit = true;
            }

            List<string> nextNodes = caveMappings[position];

            foreach (string nextNode in nextNodes)
            {
                if ((nextNode == "start") || (!IsUpper(nextNode) && nextNode != "end" && path.Contains(nextNode) && atVisitLimit))
                {
                    continue;
                }
                else
                {
                    if (!atVisitLimit)
                    {
                        if (!IsUpper(nextNode) && path.Contains(nextNode))
                        {
                            visitedPreviously = true;
                        }
                        else
                        {
                            visitedPreviously = false;
                        }

                    }
                    CaveMappings2(caveMappings, nextNode, path + ", " + nextNode, ref collectedPaths, visitedPreviously);
                }
            }
        }

        public bool IsUpper(string text)
        {
            char input = text.ToCharArray()[0];
            if (char.IsUpper(input))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IDictionary<string, List<string>> Populate(string path)
        {
            IDictionary<string, List<string>> caveMappings = new Dictionary<string, List<string>>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    string[] parts = line.Split("-");
                    string startPoint = parts[0];
                    string endPoint = parts[1];

                    if (!caveMappings.ContainsKey(startPoint))
                    {
                        caveMappings.Add(startPoint, new List<string>() { endPoint });
                    }
                    else
                    {
                        caveMappings[startPoint].Add(endPoint);
                    }
                    if (!caveMappings.ContainsKey(endPoint))
                    {
                        caveMappings.Add(endPoint, new List<string>() { startPoint });
                    }
                    else
                    {
                        caveMappings[endPoint].Add(startPoint);
                    }
                }
            }
            return caveMappings;
        }
    }
}