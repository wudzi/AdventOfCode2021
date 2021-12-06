using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace AdventOfCode2021
{
    public class BingoBoard
    {
        public int[,] coord;
    }
    public class Day4
    {
        public int Main(string part, string path)
        {
            int answer = 0;
            if (part == "a")
            {
                answer = Day4a(path);
            }

            if (part == "b")
            {
                answer = Day4b(path);
            }

            return answer;
        }

        public int[] GetDraws(string path)
        {
            int[] draws;
            using (TextReader tr = File.OpenText(path))
            {
                string line = tr.ReadLine();
                draws = line.Split(',').Select(Int32.Parse).ToArray();
            }

            return draws;
        }
        public int Day4a(string path)
        {
            int[] draws = GetDraws(path);
            List<BingoBoard> bingoBoards = PopulateBoards(path);

            foreach (int draw in draws)
            {
                bingoBoards = DrawBall(bingoBoards, draw);
                int result = CheckForWin(bingoBoards, draw);
                if (result > 0)
                {
                    return result;
                }
            }
            return 0;
        }

        public int Day4b(string path)
        {
            int[] draws = GetDraws(path);
            List<BingoBoard> bingoBoards = PopulateBoards(path);
            foreach (int draw in draws)
            {
                if (bingoBoards.Count == 1)
                {
                    bingoBoards = DrawBall(bingoBoards, draw);
                    int result = CheckForWin(bingoBoards, draw);
                    if (result > 0)
                    {
                        return result;
                    }
                }
                else
                {
                    bingoBoards = DrawBall(bingoBoards, draw);
                    bingoBoards = RemoveIfWin(bingoBoards, draw);
                }
            }
            return 0;
        }
        public List<BingoBoard> DrawBall(List<BingoBoard> boards, int ball)
        {
            foreach (BingoBoard board in boards)
            {
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (board.coord[y, x] == ball) board.coord[y, x] = 0;
                    }
                }
            }
            return boards;
        }
        public List<BingoBoard> RemoveIfWin(List<BingoBoard> boards, int ball)
        {
            List<BingoBoard> returnBoards = new List<BingoBoard>();
            foreach (BingoBoard board in boards)
            {
                bool remove = false;
                int ySum = 0; int xSum = 0;
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        xSum += board.coord[y, x];
                    }
                    if (xSum == 0)
                    {
                        remove = true;
                    }
                    xSum = 0;
                }
                if (board != null)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        for (int y = 0; y < 5; y++)
                        {
                            ySum += board.coord[y, x];
                        }
                        if (ySum == 0)
                        {
                            remove = true;
                        }
                        ySum = 0;
                    }
                }
                if (!remove) returnBoards.Add(board);
            }
            return returnBoards;
        }
        public int CheckForWin(List<BingoBoard> boards, int ball)
        {
            foreach (BingoBoard board in boards)
            {
                int ySum = 0; int xSum = 0;
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        xSum += board.coord[y, x];
                    }
                    if (xSum == 0)
                    {
                        return ball * board.coord.Cast<int>().Sum();
                    }
                    xSum = 0;
                }
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        ySum += board.coord[y, x];
                    }
                    if (ySum == 0)
                    {
                        return ball * board.coord.Cast<int>().Sum();
                    }
                    ySum = 0;
                }
            }
            return 0;
        }
        public List<BingoBoard> PopulateBoards(string path)
        {
            List<BingoBoard> bingoBoards = new List<BingoBoard>();
            using (TextReader tr = File.OpenText(path))
            {
                string line;
                int[,] thisArray = new int[5, 5];
                int x = 0, y = 0;
                while ((line = tr.ReadLine()) != null)
                {
                    if (line == "" || line.Contains(",")) continue;
                    if (line.Substring(0, 1) == " ") line = line.Remove(0, 1);
                    string[] parts = line.Replace("  ", " ").Split(" ");
                    foreach (string part in parts)
                    {
                        thisArray[y, x] = Convert.ToInt32(part);
                        x++;
                    }
                    x = 0;
                    y++;
                    if (y == 5)
                    {
                        bingoBoards.Add(PopulateBoard(thisArray));
                        y = 0; x = 0;
                    }
                }
            }
            return bingoBoards;
        }
        public BingoBoard PopulateBoard(int[,] numbers)
        {
            BingoBoard bingoBoard = new BingoBoard();
            bingoBoard.coord = new int[5, 5];
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    bingoBoard.coord[y, x] = numbers[y, x];
                }
            }
            return bingoBoard;
        }
    }
}