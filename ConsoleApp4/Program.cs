using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace AdventOfCode2021
{
    class Day24
    {
        static void Main(string[] args)
        {
            PartOne();
        }
            private static readonly String[] lines = File.ReadAllLines(@"Input.txt");

        
        public static (long result, long time) PartOne()
        {
            var sw = new Stopwatch();
            sw.Start();
            var result = long.Parse(string.Join("", RunData(1).OrderBy(x => x.Key).Select(x => x.Value)));
            sw.Stop();

            return (result, sw.ElapsedMilliseconds);
        }

        public static (long result, long time) PartTwo()
        {
            var sw = new Stopwatch();
            sw.Start();
            var result = long.Parse(String.Join("", RunData(2).OrderBy(x => x.Key).Select(x => x.Value)));
            sw.Stop();

            return (result, sw.ElapsedMilliseconds);
        }

        private static Dictionary<int, int> RunData(int part)
        {
            var pairs = new List<(int, int)>();
            foreach (var i in Enumerable.Range(0, 14))
            {
                pairs.Add((int.Parse(lines[i * 18 + 5][6..]), int.Parse(lines[i * 18 + 15][6..])));
            }
            var stack = new Stack<(int, int)>();
            var keys = new Dictionary<int, (int x, int y)>();

            foreach (var (pair, i) in pairs.Select((pair, i) => (pair, i)))
            {
                if (pair.Item1 > 0)
                {
                    stack.Push((i, pair.Item2));
                }
                else
                {
                    var (j, jj) = stack.Pop();
                    keys[i] = (j, jj + pair.Item1);
                }
            }
            var output = new Dictionary<int, int>();

            foreach (var (key, val) in keys)
            {
                output[key] = part == 1 ? Math.Min(9, 9 + val.y) : Math.Max(1, 1 + val.y);
                output[val.x] = part == 1 ? Math.Min(9, 9 - val.y) : Math.Max(1, 1 - val.y);
            }

            return output;
        }
    }
}