using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day9
    {
        public long Main(string part, string path)
        {

            long answer = 0;
            if (part == "a")
            {
                answer = Day9a(path);
            }

            if (part == "b")
            {
                answer = Day9b(path);
            }
            return answer;
        }

        public long Day9a(string path)
        {
            int[,] grid = Populate(path);
            long sumOfMins = 0;
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    int higherNeighbours = 0;
                    IEnumerable<(int y, int x)> neighbours = CaveNeighbours(grid, y, x);
                    foreach (var coordinate in neighbours)
                    {
                        higherNeighbours += (grid[y, x] >= grid[coordinate.y, coordinate.x]) ? 1 : 0;
                    }

                    if (higherNeighbours ==0)
                    {
                        sumOfMins += grid[y,x]+ 1;
                    }
                }
            }
            return sumOfMins;
        }

        public long Day9b(string path)
        {
            int[,] grid = Populate(path);
            List<int> basins = new List<int>();
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {

                    int higherNeighbours = 0;
                    IEnumerable<(int y, int x)> neighbours = CaveNeighbours(grid, y, x);
                    foreach (var coordinate in neighbours)
                    {
                        higherNeighbours += (grid[y, x] >= grid[coordinate.y, coordinate.x]) ? 1 : 0;
                    }

                    if (higherNeighbours == 0)
                    {
                        basins.Add(BasinsFrom((y, x), grid, new HashSet<(int y, int x)>()));
                    }
                }
            }

            basins.Sort();
            return basins[basins.Count - 1] * basins[basins.Count - 2] * basins[basins.Count - 3];
        }

        public int BasinsFrom((int y, int x) coordinate, int[,] grid, HashSet<(int y, int x)> basins) { 

            basins.Add(coordinate);
            foreach((int y, int x) neighbour in CaveNeighbours(grid, coordinate.y, coordinate.x))
            {
                if(grid[neighbour.y,neighbour.x] < 9 && grid[coordinate.y, coordinate.x] < grid[neighbour.y,neighbour.x])
                {
                    if (!basins.Contains(neighbour))
                    {
                        BasinsFrom(neighbour, grid, basins);
                    }
                }
            }

            return basins.Count;

        }

        public IEnumerable<(int y, int x)> CaveNeighbours(int[,] grid, int yPos, int xPos)
        {
            (int y, int x)[] offsets = new (int y, int x)[4] { (1, 0), (0, -1), (-1, 0), (0, 1) };

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
            int[,] grid = new int[rows,cols];

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