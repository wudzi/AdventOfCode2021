using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day10
    {
        public long Main(string part, string path)
        {
            long answer = 0;
            if (part == "a")
            {
                answer = Day10a(path);
            }

            if (part == "b")
            {
                answer = Day10b(path);
            }

            return answer;
        }

        public long Day10b(string path)
        {
            List<string> chunksList = Populate(path);
            List<long> chunkscores = new List<long>();
            
            foreach (string chunks in chunksList)
            {
                List<string> lastOpener = new List<string>();
                bool valid = true;
                lastOpener.Add(chunks.Substring(0, 1));
                for (int i = 1; i < chunks.Length; i++)
                {
                    if (lastOpener.Count > 0 && !IsValid(lastOpener.Last(), chunks.Substring(i, 1)))
                    {
                        valid = false;
                        break;
                    }
                    if ("{[(<".IndexOf(chunks.Substring(i, 1)) >= 0)
                    {
                        lastOpener.Add(chunks.Substring(i, 1));
                    }
                    else
                    {
                        lastOpener.RemoveAt(lastOpener.Count - 1);
                    }
                }

                if (lastOpener.Count > 0 && valid)
                {
                    long chunkScore = 0;
                    for (int i = lastOpener.Count - 1; i >= 0; i--)
                    {
                        chunkScore = chunkScore * 5 + CloserScore(lastOpener[i]);
                    }
                    chunkscores.Add(chunkScore);
                }
            }

            chunkscores.Sort();
            return chunkscores[chunkscores.Count / 2];
        }

        public int CloserScore(string closer)
        {
            return closer switch
            {
                ("(") => 1,
                ("[") => 2,
                ("{") => 3,
                ("<") => 4,
                _ => 0,
            };
        }

        public long Day10a(string path)
        {
            List<string> chunksList = Populate(path);

            long chunkScore = 0;
            foreach (string chunks in chunksList)
            {
                List<string> lastOpener = new List<string>();
                lastOpener.Add(chunks.Substring(0, 1));
                for (int i = 1; i < chunks.Length; i++)
                {
                    if (lastOpener.Count > 0 && !IsValid(lastOpener.Last(), chunks.Substring(i, 1)))
                    {
                        chunkScore += Value(chunks.Substring(i, 1));
                        break;
                    }
                    if ("{[(<".IndexOf(chunks.Substring(i, 1)) >= 0)
                    {
                        lastOpener.Add(chunks.Substring(i, 1));
                    }
                    else
                    {
                        lastOpener.RemoveAt(lastOpener.Count - 1);
                    }
                }
            }
            return chunkScore;
        }

        public int Value(string chunk)
        {
            return chunk switch
            {
                (")") => 3,
                ("]") => 57,
                ("}") => 1197,
                (">") => 25137,
                _ => 0,
            };
        }

        public bool IsValid(string previous, string next)
        {
            if (@"{[(<".IndexOf(next) >= 0 || previous == "")
            {
                return true;
            }

            return (previous, next) switch
            {
                ("{", "}") => true,
                ("(", ")") => true,
                ("<", ">") => true,
                ("[", "]") => true,
                _ => false
            };
        }

        public List<string> Populate(string path)
        {
            List<string> chunks = new List<string>();

            using (TextReader tr = File.OpenText(path))
            {
                string line;
                int x = 0, y = 0;
                while ((line = tr.ReadLine()) != null)
                {
                    chunks.Add(line);
                }
            }

            return chunks;
        }
    }
}
