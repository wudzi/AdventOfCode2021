using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day13
    {
        public long Main(string part, string path)
        {

            long answer = 0;
            if (part == "a")
            {
                answer = Day13a(path);
            }

            if (part == "b")
            {
                Console.WriteLine("Day 13 part b result: See below");
                answer = Day13b(path);
            }
            return answer;
        }

        public long Day13a(string path)
        {
            int[,] grid = Populate(path);
            List<(string s, int i)> folds = Folds(path);
            int[,] finalGrid;
            if (folds[0].s == "y")
            {
                finalGrid = FoldVertical(grid, folds[0].i);
            }
            else
            {
                finalGrid = FoldHorizontal(grid, folds[0].i);
            }
            int[] stars = finalGrid.Cast<int>().ToArray();
            return stars.Sum();
        }

        public long Day13b(string path)
        {
            int[,] grid = Populate(path);
            List<(string s, int i)> folds = Folds(path);
            for (int i = 0; i < folds.Count; i++)
            {
                if (folds[i].s == "y")
                {
                    grid = FoldVertical(grid, folds[i].i);
                }
                else
                {
                    grid = FoldHorizontal(grid, folds[i].i);
                }
            }

            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if(grid[y, x] == 1)
                    {
                        Console.Write("#");
                    } else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }

            return 0;
        }

        public int[,] FoldHorizontal(int[,] inputGrid, int foldFrom)
        {
            int[,] outputGrid = new int[inputGrid.GetLength(0),foldFrom];
            for (int y = 0; y < outputGrid.GetLength(0); y++)
            {
                for (int x = 0; x < outputGrid.GetLength(1); x++)
                {
                    outputGrid[y, x] = inputGrid[y, x];
                    if (outputGrid[y, x] == 0)
                    {
                        outputGrid[y, x] = inputGrid[y, foldFrom + (foldFrom - x)];
                    }
                }
            }
            return outputGrid;
        }
        public int[,] FoldVertical(int[,] inputGrid, int foldFrom)
        {
            int[,] outputGrid = new int[foldFrom, inputGrid.GetLength(1)];
            for(int x = 0; x < outputGrid.GetLength(1); x++)
            {
                for(int y = 0; y < outputGrid.GetLength(0); y++)
                {
                    outputGrid[y, x] = inputGrid[y, x];
                    if (outputGrid[y, x] == 0)
                    {
                        outputGrid[y, x] = inputGrid[foldFrom + (foldFrom - y), x];
                    }
                }
            }
            return outputGrid;
        }

        public int[,] Populate(string path)
        {
            List<(int y, int x)> populatedCordinates = new List<(int y, int x)>();
            int[,] grid;
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != "")
                {
                    string[] parts = line.Split(",");
                    (int x, int y) coordinate = (Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]));
                    populatedCordinates.Add(coordinate);
                }

                grid = new int[populatedCordinates.Max(i => i.x+1), populatedCordinates.Max(i => i.y+1)];
                for(int i=0; i<populatedCordinates.Count; i++)
                {
                    grid[populatedCordinates[i].x, populatedCordinates[i].y] = 1;
                }
            }
            return grid;
        }

        public List<(string s, int i)> Folds(string path)
        {
            List<(string s, int i)> folds = new List<(string s, int i)>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    //int firstChar;
                    //bool isNumeric = int.TryParse(line.Substring(1,1), out firstChar);
                    if (line != "" && line.Contains("fold along"))
                    {
                        string[] parts = line.Split("=");
                        (string s, int i) fold = (parts[0].Replace("fold along ", ""), Convert.ToInt32(parts[1]));
                        folds.Add(fold);
                    }
                }
            }
            return folds;
        }
    }
}
