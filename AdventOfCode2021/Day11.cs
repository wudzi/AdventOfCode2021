using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day11
    {
        public int flashes { get; set; }

        public long Main(string part, string path)
        {

            long answer = 0;
            if (part == "a")
            {
                answer = Day11a(path);
            }

            if (part == "b")
            {
                answer = Day11b(path);
            }
            return answer;
        }

        public long Day11a(string path)
        {
            int[,] grid = Populate(path);
            flashes = 0;
            for(int i = 0; i < 100; i++)
            {
                grid = Stage1(grid);
                grid = Stage2(grid);
            }
            return flashes;
        }

        public long Day11b(string path)
        {
            int[,] grid = Populate(path);
            flashes = 0;
            int gridsize = grid.GetLength(0) * grid.GetLength(1);
            for (int i = 0; i<10000000; i++)
            {
                grid = Stage1(grid);
                grid = Stage2(grid);
                List<int> elements = grid.Cast<int>().ToList();
                if (elements.Distinct().Count() == 1)
                {
                    flashes = i + 1;
                    break;
                }
            }
            return flashes;
        }

        public int[,] Stage2(int[,] grid)
        {
            bool recordsToProcess = true;
            while (recordsToProcess)
            {
                recordsToProcess = false;
                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    for (int x = 0; x < grid.GetLength(1); x++)
                    {
                        if (grid[y, x] > 9)
                        {
                            recordsToProcess = true;
                            flashes++;
                            grid[y, x] = 0;
                            Increase(grid, y, x);
                        }
                    }
                }
            }
            return grid;
        }

        public int[,] Increase(int[,] grid, int y, int x)
        {
            IEnumerable<(int y, int x)> neighbours = CoordinateNeighbours(grid, y, x);
            foreach (var coordinate in neighbours)
            {
                if (grid[coordinate.y, coordinate.x] > 0)
                {
                    grid[coordinate.y, coordinate.x]++;
                }
            }

            return grid;
        }

        public int[,] Stage1(int[,] grid)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    grid[y, x]++;
                }
            }
            return grid;
        }
        public IEnumerable<(int y, int x)> CoordinateNeighbours(int[,] grid, int yPos, int xPos)
        {
            (int y, int x)[] offsets = new (int y, int x)[8] { (1, 0), (0, -1), (-1, 0), (0, 1), (1, 1), (1, -1), (-1, 1), (-1, -1) };

            for (int i = 0; i < offsets.Length; i++)
            {
                (int y, int x) coordinate = (y: yPos + offsets[i].y, x: xPos + offsets[i].x);
                if (coordinate.y >= 0 && coordinate.y < grid.GetLength(0) && coordinate.x >= 0 && coordinate.x < grid.GetLength(1))
                {
                    yield return coordinate;
                }

            }
        }

        public int[,] Populate(string path)
        {
            int rows = File.ReadLines(path).Count();
            int cols = File.ReadLines(path).First().Length;
            int[,] grid = new int[rows, cols];

            using (TextReader tr = File.OpenText(path))
            {
                string line;
                int x = 0, y = 0;
                while ((line = tr.ReadLine()) != null)
                {
                    char[] parts = line.ToCharArray();
                    foreach (char part in parts)
                    {
                        grid[y, x] = Convert.ToInt32(part.ToString());
                        x++;
                    }
                    x = 0;
                    y++;
                }
                return grid;
            }
        }
    }
}
