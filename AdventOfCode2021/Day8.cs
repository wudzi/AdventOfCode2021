using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Display
    {
        public List<string> signals { get; set; }
        public List<string> outputs { get; set;}

        public Display(List<string> theseSignals, List<string> theseOutputs)
        {
            signals = theseSignals;
            outputs = theseOutputs;
        }
    }

    public class Segments
    {
        public int number { get; set; }
        public string pattern { get; set; }

        public Segments(int thisNumber, string thisPattern)
        {
            number = thisNumber;
            pattern = thisPattern;
        }
    }

    public class Day8
    {
        public long Main(string part, string path)
        {
            long answer = 0;
            if (part == "a")
            {
                answer = Day8a(path);
            }

            if (part == "b")
            {
                answer = Day8b(path);
            }
            return answer;
        }

        public long Day8a(string path)
        {
            List<Display> displays = Populate(path);
            List<int> segmentsToCount = new List<int> { 2, 4, 3, 7 };
            return displays
                .SelectMany(x => x.outputs)
                .Where(x => segmentsToCount.Contains(x.Length))
                .Count();
        }
        public long Day8b(string path)
        {
            List<Display> displays = Populate(path);
            
            long answer = 0;
            foreach(Display display in displays)
            {
                Dictionary<string, int> mapper = Decode(display.signals);
                string decodedOutput = "";
                foreach (string output in display.outputs)
                {
                    decodedOutput += mapper[Alpha(output)].ToString();
                }
                answer += long.Parse(decodedOutput);
            }

            return answer;
        }

        public string Alpha(string inStr){

            char[] input = inStr.ToCharArray();
            Array.Sort(input);
            return new string(input);

        }
        public Dictionary<string, int> Decode(List<string> signals)
        {
            Dictionary<string, int> mapper = new Dictionary<string, int>();

            //Not sure if there is a better way, but you can work out numbers by a process of elimination referencing known segments
            string map1 = signals
                            .Where(i => i.Length == 2)
                            .Select(i => i)
                            .First();
            mapper.Add(Alpha(map1), 1);
            char[] map1Letters = map1.ToCharArray();

            string map4  = signals
                            .Where(i => i.Length == 4)
                            .Select(i => i)
                            .First();
            mapper.Add(Alpha(map4), 4);
            char[] map4Letters = map4.ToCharArray();
            
            string map7 = signals
                            .Where(i => i.Length == 3)
                            .Select(i => i)
                            .First();

            char[] map7Letters = map7.ToCharArray();
            mapper.Add(Alpha(map7), 7);

            string map8 = signals
                            .Where(i => i.Length == 7)
                            .Select(i => i)
                            .First();
            mapper.Add(Alpha(map8), 8);

            string map9 = signals
                            .Where(i => i.Length == 6 && map4Letters.Count(s => i.Contains(s)) == 4)
                            .Select(i => i)
                            .First();
            mapper.Add(Alpha(map9), 9);
            char[] map9Letters = map9.ToCharArray();

            string map6 = signals
                            .Where(i => i.Length == 6 && map1Letters.Count(s => i.Contains(s)) == 1)
                            .Select(i => i)
                            .First();
            mapper.Add(Alpha(map6), 6);

            string map0 = signals
                            .Where(i => i.Length == 6 && map1Letters.Count(s => i.Contains(s)) == 2 && map4Letters.Count(s => i.Contains(s)) == 3)
                            .Select(i => i)
                            .First();
            mapper.Add(Alpha(map0), 0);

            string map2 = signals
                            .Where(i => i.Length == 5 && map4Letters.Count(s => i.Contains(s)) == 2 && map9Letters.Count(s => i.Contains(s)) == 4)
                            .Select(i => i)
                            .First();
            mapper.Add(Alpha(map2), 2);

            string map3 = signals
                            .Where(i => i.Length == 5 && map7Letters.Count(s => i.Contains(s)) == 3)
                            .Select(i => i)
                            .First();
            mapper.Add(Alpha(map3), 3);

            string map5 = signals
                            .Where(i => i.Length == 5 && map4Letters.Count(s => i.Contains(s)) == 3 && map1Letters.Count(s => i.Contains(s)) == 1)
                            .Select(i => i)
                            .First();
            mapper.Add(Alpha(map5), 5);

            return mapper;
        }
        
        public List<Display> Populate(string path)
        {
            List<Display> displays = new List<Display>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    List<string> signals = new List<string>();
                    List<string> outputs = new List<string>();
                    string[] lineSignals = line.Substring(0,line.IndexOf(" | ")).Split(new char[] { ' ' });
                    foreach (string signal in lineSignals)
                    {
                        signals.Add(signal);
                    }
                    string[] lineOutputs = line.Substring(line.IndexOf(" | ") + 3).Split(new char[] { ' ' });
                    foreach (string output in lineOutputs)
                    {
                        outputs.Add(output);
                    }

                    displays.Add(new Display(signals,outputs));
                }
            }
            return displays;
        }

    }
}
