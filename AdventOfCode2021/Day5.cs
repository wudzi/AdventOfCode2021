using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class point
    {
        public int xPos;
        public int yPos;
    }

    public class line
    {
        public int xFrom;
        public int yFrom;
        public int xTo;
        public int yTo;
    }

    public class Day5
    {

        public int Main(string part, string path)
        {
            List<line> lines = PopulateData(path);
            List<point> points = CreatePoints(lines, part);
            List<point> count = points.GroupBy(p => new { p.xPos, p.yPos })
                      .Where(p => p.Count() > 1)
                      .Select(p => p.First())
                      .ToList();

            return count.Count();
        }

        public List<line> PopulateData(string path)
        {
            List<line> lines = new List<line>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    string[] parts = line.Replace(" -> ", ",").Split(",");

                    line thisLine = new line
                    {
                        xFrom = Convert.ToInt32(parts[0]),
                        yFrom = Convert.ToInt32(parts[1]),
                        xTo = Convert.ToInt32(parts[2]),
                        yTo = Convert.ToInt32(parts[3]),
                    };

                    lines.Add(thisLine);
                }
            }
            return lines;
        }

        public List<point> CreatePoints(List<line> lines, string day)
        {
            List<point> points = new List<point>();
            foreach (line line in lines)
            {
                if (line.xFrom == line.xTo || line.yFrom == line.yTo)
                {
                    int xVar = Math.Max(line.xFrom, line.xTo) - Math.Min(line.xFrom, line.xTo);
                    int yVar = Math.Max(line.yFrom, line.yTo) - Math.Min(line.yFrom, line.yTo);

                    int xInc = 0; int yInc = 0;

                    if (xVar == yVar) { xInc = 1; yInc = 1; }
                    if (xVar == 0) yInc = 1;
                    if (yVar == 0) xInc = 1;
                    {
                        for (int i = 0; i <= Math.Max(xVar, yVar); i++)
                        {
                            point point = new point
                            {
                                xPos = Math.Min(line.xFrom, line.xTo) + (i * xInc),
                                yPos = Math.Min(line.yFrom, line.yTo) + (i * yInc)
                            };

                            points.Add(point);
                        }
                    }
                }
                else
                {
                    if (day == "b")
                    {
                        int zVar = Math.Max(line.xFrom, line.xTo) - Math.Min(line.xFrom, line.xTo);
                        int xInc = 0; int yInc = 0;
                        if (line.xFrom < line.xTo) { xInc = 1; } else { xInc = -1; }
                        if (line.yFrom < line.yTo) { yInc = 1; } else { yInc = -1; }

                        for (int i = 0; i <= zVar; i++)
                        {
                            point point = new point
                            {
                                xPos = line.xFrom + (i * xInc),
                                yPos = line.yFrom + (i * yInc)
                            };

                            points.Add(point);
                        }
                    }
                }
            }

            return points;
        }
    }
}