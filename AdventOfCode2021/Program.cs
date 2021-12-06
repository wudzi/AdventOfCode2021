using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Reflection;

namespace AdventOfCode2021
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int[] days = { 1, 2, 3, 4, 5, 6 };

            for(int i = 1; i < days.Length; i++)
            {
                string path = Path.Combine(Environment.CurrentDirectory, @"Input\", "Day" + i + ".txt");
                string[] parts = { "a", "b" };

                for (int p = 0; p < parts.Length; p++)
                {
                    string[] parameters = { parts[p], path };

                    if (i == 6)
                    {
                        long result = Caller("AdventOfCode2021.Day" + i, "Main", parameters);
                        Console.WriteLine($"Day {i} part {parts[p]} result: {result}");
                    }
                    else
                    {
                        int result = Caller("AdventOfCode2021.Day" + i, "Main", parameters);
                        Console.WriteLine($"Day {i} part {parts[p]} result: {result}");
                    }
                }
            }

            Console.ReadKey();
        }

        private static int Caller(string classToCall, string methodToCall, string[] parameters)
        {
            Type type = Type.GetType(classToCall);
            Object obj = Activator.CreateInstance(type);
            MethodInfo methodInfo = type.GetMethod(methodToCall);
            return (int)methodInfo.Invoke(obj, parameters);
        }
    }
}