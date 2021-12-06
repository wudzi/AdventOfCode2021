using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day2
    {
        struct directions
        {
            public string heading;
            public int unit;
        }

        public int Main(string part, string path)
        {
            int answer = 0;
            if (part == "a")
            {
                answer = Day2a(path);
            }

            if (part == "b")
            {
                answer = Day2b(path);
            }

            return answer;
        }

        public int Day2b(string path)
        {
            List<directions> directions = new List<directions>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    string[] parts = line.Split(" ");
                    directions thisDirection = new directions();
                    thisDirection.heading = parts[0];
                    thisDirection.unit = int.Parse(parts[1]);
                    directions.Add(thisDirection);

                }

                int heading = 0;
                int depth = 0;
                int aim = 0;
                foreach (directions direction in directions)
                {
                    if (direction.heading == "forward") { heading += direction.unit; depth += direction.unit * aim; }
                    if (direction.heading == "up") aim -= direction.unit;
                    if (direction.heading == "down") aim += direction.unit;
                }

                int final = heading * depth;

                return final;
            }
        }

        public int Day2a(string path)
        {

            List<directions> directions = new List<directions>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    string[] parts = line.Split(" ");
                    directions thisDirection = new directions();
                    thisDirection.heading = parts[0];
                    thisDirection.unit = int.Parse(parts[1]);
                    directions.Add(thisDirection);

                }
            }

            int heading = 0;
            int depth = 0;
            foreach (directions direction in directions)
            {
                if (direction.heading == "forward") heading += direction.unit;
                if (direction.heading == "up") depth -= direction.unit;
                if (direction.heading == "down") depth += direction.unit;
            }

            int final = heading * depth;

            return final;
        }
    }
}
