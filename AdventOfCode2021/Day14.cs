using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class polymer
    {
        public string pair;
        public long number;

        public polymer(string thisPair, long toAdd)
        {
            pair = thisPair;
            number += toAdd;
        }
    }
    public class Day14
    {
        public long Main(string part, string path)
        {

            long answer = 0;
            if (part == "a")
            {
                answer = Day14a(10,path);
            }

            if (part == "b")
            {
                answer = Day14b(40, path);
            }
            return answer;
        }

        public long Day14b(int steps, string path)
        {

            //Been screwed over by huge recursion. Should have seen it coming :(
            //
            //Lets try and count the numbers pairs instead.

            string template = PopulateTemplate(path);
            IDictionary<string, string> mappings = PopulateMappings(path);
            List<polymer> polymers = new List<polymer>();

            for (int i = 0; i < template.Length - 1; i++)
            {
                polymers.Add(new polymer(template.Substring(i, 2), 1));

            }

            for (int i = 0; i < steps; i++)
            {
                polymers = ProcessPairs(polymers, mappings);
            }

            Dictionary<string, long> counters = new Dictionary<string, long>();

            string[] distinctElements = mappings.Values.Distinct().ToArray();

            foreach (string element in distinctElements)
            {
                if (polymers[0].pair.Substring(0, 1).Contains(element))
                {
                    counters[element] = counters.GetValueOrDefault(element) + polymers[0].number;
                }
                for (int i= 0; i< polymers.Count(); i++)
                {
                    if (polymers[i].pair.Substring(1,1).Contains(element))
                    {
                        counters[element] = counters.GetValueOrDefault(element) + polymers[i].number;
                    }
                }
            }
            long min = counters.OrderBy(s => s.Value).First().Value;
            long max = counters.OrderByDescending(s => s.Value).First().Value;
            return max - min;
        }

        public long Day14a(int steps, string path)
        {
            string template = PopulateTemplate(path);
            IDictionary<string, string> mappings = PopulateMappings(path);
            for (int i = 0; i < steps; i++)
            {
                template = Process(template, mappings);
            }
            Dictionary<char, int> mostCommon = template.Distinct().ToDictionary(c => c, c => template.Count(s => s == c));
            int min = mostCommon.OrderBy(s => s.Value).First().Value;
            int max = mostCommon.OrderByDescending(s => s.Value).First().Value;
            return max - min;
        }

        public List<polymer> ProcessPairs(List<polymer> polymers, IDictionary<string, string> mappings)
        {
            List<polymer> returnedPolymers = new List<polymer>();

            for (int i = 0; i < polymers.Count; i++)
            {
                string polymer = polymers[i].pair;
                long currentCount = polymers[i].number;
                List<string> splitPolymers = new List<string>();
                splitPolymers.Add(polymer.Substring(0,1) + mappings[polymer]);
                splitPolymers.Add(mappings[polymer] + polymer.Substring(1, 1));

                foreach (string thisPolymer in splitPolymers) {
                    int index = returnedPolymers.FindIndex(i => i.pair == thisPolymer);
                    if (index > -1)
                    {
                        returnedPolymers[index].number += currentCount;
                    } else
                    {
                        returnedPolymers.Add(new polymer(thisPolymer, currentCount));
                    }
                }
            }

            return returnedPolymers;
        }

        public string Process(string template, IDictionary<string, string> mappings)
        {
            List<string> chunks = new List<string>();
            
            for (int i = 0; i < template.Length - 1; i++)
            {
                chunks.Add(template.Substring(0 + i, 2));
            }
            string output = chunks[0].Substring(0, 1);
            foreach (string chunk in chunks)
            {
                output +=  mappings[chunk] + chunk.Substring(1, 1);
            }

            return output;
        }

        public string PopulateTemplate(string path)
        {
            string template = "";
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        if (line.Contains("->"))
                        {
                            continue;
                        }
                        else
                        {
                            template = line;
                        }
                    }
                }
            }
            return template;
        }

        public IDictionary<string, string> PopulateMappings(string path)
        {
            IDictionary<string, string> mappings = new Dictionary<string, string>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        if (line.Contains("->"))
                        {
                            string[] parts = line.Split(" -> ");
                            mappings.Add(parts[0], parts[1]);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return mappings;
        }
    }
}
