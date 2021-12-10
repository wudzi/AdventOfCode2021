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
                        higherNeighbours += (grid[y, x] >= grid[coordinate.x, coordinate.y]) ? 1 : 0;
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
                    basins.Add(BasinsFrom((y,x),grid,new HashSet<(int y, int x)>()));
                }
            }

            basins.Sort();
            return basins[basins.Count - 1] * basins[basins.Count - 2] * basins[basins.Count - 3];
        }

        public int BasinsFrom((int y, int x) coordinate, int[,] grid, HashSet<(int y, int x)> basins) { 

            basins.Add(coordinate);
            foreach((int x, int y) neighbour in CaveNeighbours(grid, coordinate.y, coordinate.x))
            {
                if(grid[neighbour.y,neighbour.x] < 9 && Maths.Abs(grid[coordinate.y, coordinate.x] - grid[neighbour.y,neighbour.x]) ==1)
                {
                    if (!basins.Contains(neighbour))
                    {
                        BasinsFrom(neighbour, grid, basins);
                    }
                }
            }

            return basins.Count;

        }

        //public int BasinsFrom(int[,] grid, int yPos, int xPos)
        //{
        //    int basins = 1;

        //    for (int y = yPos + 1; y < grid.GetLength(0); y++)
        //    {
        //        if (grid[yPos + y, xPos] == grid[y, xPos] +1 && grid[yPos + y, xPos] < 9) { basins++; } else { break; }
        //    }

        //    for (int y = yPos - 1; y >=0 ; y--)
        //    {
        //        if (grid[yPos + y, xPos] == grid[y, xPos] -1) { basins++; } else { break; }
        //    }

        //    for (int x = xPos + 1; x < grid.GetLength(1); x++)
        //    {
        //        if (grid[yPos, xPos + x] == grid[yPos, x] + 1) { basins++; } else { break; }
        //    }

        //    for (int x = xPos - 1; x >=0; x--)
        //    {
        //        if (grid[yPos, xPos + x] == grid[yPos, x] -1) { basins++; } else { break; }
        //    }

        //    return basins;
        //}

        public IEnumerable<(int y, int x)> CaveNeighbours(int[,] grid, int yPos, int xPos)
        {
            (int x, int y)[] offsets = new (int x, int y)[4] { (1, 0), (0, -1), (-1, 0), (0, 1) };

            for (int i = 0; i < offsets.Length; i++)
            {
                (int x, int y) coordinate = (x: xPos + offsets[i].x, y: yPos + offsets[i].y);
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