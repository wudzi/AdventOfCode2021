using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{

    public class lanternfish
    {
        public int age;
        public long number;

        public lanternfish(int thisAge, long toAdd)
        {
            age = thisAge;
            number += toAdd;
        }
    }

    public class Day6
    {
        public long Main(string part, string path)
        {
            List<lanternfish> lanternfishes = Populate(path);

            int days = 0;
            if(part == "a") { days = 80; } else { days = 256; }

            for (int i = 0; i < days; i++)
            {
                lanternfishes = AddDay(lanternfishes, 1);
            }

            return lanternfishes.Sum(i => i.number);
        }

        public List<lanternfish> AddDay(List<lanternfish> lanternfishes, int inc)
        {
            List<lanternfish> newLanternfishes = new List<lanternfish>();
            for (int i = 0; i < lanternfishes.Count; i++)
            {
                if (lanternfishes[i].age == 0)
                {
                    newLanternfishes = Append(newLanternfishes, 8, lanternfishes[i].number);
                    newLanternfishes = Append(newLanternfishes, 6, lanternfishes[i].number);
                }
                else
                {
                    newLanternfishes = Append(newLanternfishes, lanternfishes[i].age -1, lanternfishes[i].number);
                }
            }

            return newLanternfishes;
        }

        public List<lanternfish> Append(List<lanternfish> lanternfishes, int age, long inc)
            {

            int index = lanternfishes.FindIndex(i => i.age == age);
            if (index > -1)
            {
                lanternfishes[index].number += inc;
            } else
            {
                lanternfishes.Add(new lanternfish(age, inc));
            }

            return lanternfishes;
        }

        public List<lanternfish> Populate(string path)
        {
            List<lanternfish> lanternfishes = new List<lanternfish>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    string[] parts = line.Split(new char[] { ',' });
                    foreach (string part in parts)
                    {
                        int age = int.Parse(part);
                        lanternfishes = Append(lanternfishes, age, 1);
                    }
                }
            }
            return lanternfishes;
        }
    }
}
