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

            for (int i = 1; i <= days.Length; i++)
            {
                string seperator = @"Input\";
                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    seperator = @"Input/";
                }
                string path = Path.Combine(Environment.CurrentDirectory, seperator, "Day" + i + ".txt");
                string[] parts = { "a", "b" };

                for (int p = 0; p < parts.Length; p++)
                {
                    var watch = new System.Diagnostics.Stopwatch();
                    string[] parameters = { parts[p], path };
                    watch.Start();
                    long result = Caller("AdventOfCode2021.Day" + i, "Main", parameters);
                    watch.Stop();
                    Console.WriteLine($"Day {i} part {parts[p]} result: {result} Executed in: {watch.ElapsedMilliseconds} ms");
                }
            }

            Console.ReadKey();
        }

        private static long Caller(string classToCall, string methodToCall, string[] parameters)
        {
            Type type = Type.GetType(classToCall);
            Object obj = Activator.CreateInstance(type);
            MethodInfo methodInfo = type.GetMethod(methodToCall);
            return (long)methodInfo.Invoke(obj, parameters);
        }
    }
}