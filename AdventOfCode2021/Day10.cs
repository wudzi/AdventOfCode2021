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
                //answer = Day1b(path);
            }

            return answer;
        }

        public long Day10a(string path)
        {
            List<string> chunksList = Populate(path);

            long chunkScore = 0;
            foreach (string chunks in chunksList)
            {
                string lastOpener = "";
                for (int i = 0; i < chunks.Length; i++)
                {
                    if ("{[(<".IndexOf(chunks.Substring(i - 1, 1)) > 0)
                    { 
                        lastOpener = chunks.Substring(i - 1, 1);
                    }
                    if (!IsValid(lastOpener, chunks.Substring(i, 1)))
                    {
                        chunkScore += Value(chunks.Substring(i, 1));
                        break;
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
            if ("{[(<".IndexOf(next) > 0 || previous == "")
            {
                return true;
            }

            switch (previous, next)
            {
                case ("{", "}"):
                    return true;
                case ("[", "]"):
                    return true;
                case ("<", ">"):
                    return true;
                case ("(", "])"):
                    return true;
                default:
                    return false;
            }
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
