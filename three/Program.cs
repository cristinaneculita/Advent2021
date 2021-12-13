using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    class Program
    {
       
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");


            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var response = 0;
            var max = 11;
            var mat = new bool[895, 1311];
            // int i = 1;

            var xy = new List<char>();
            var cooord = new List<int>();
            int maxx = 0;
            int maxy = 0;
            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line) && !line.Contains("fold"))
                {
                    var x = line.Split(",").Select(e => Int32.Parse(e)).ToList();
                    mat[x[1], x[0]] = true;
                    if (maxx < x[0])
                        maxx = x[0];
                    if (maxy < x[1])
                        maxy = x[1];
                }
                if (line.Contains("fold along"))
                {
                    var z = line.Split("fold along ");
                    var t = z[1].Split("=");
                    xy.Add(t[0][0]);
                    cooord.Add(Int32.Parse(t[1]));

                }
            }


            var fold = 'x';


            for (int foldi = 0; foldi < xy.Count(); foldi++)
            {
                fold = xy[foldi];

                if (fold == 'y')
                {
                    maxy = maxy / 2;
                    if (maxy != cooord[foldi])
                    {
                        Console.WriteLine("shity");
                        break;
                    }

                    for (int i = 0; i <= maxy; i++)
                    {
                        for (int j = 0; j <= maxx; j++)
                        {
                            mat[i, j] = mat[i, j] || mat[maxy * 2 - i, j];

                        }
                    }
                }

                if (fold == 'x')
                {
                    maxx = maxx / 2;
                    if (maxx != cooord[foldi])
                    {
                        Console.WriteLine("shitx");
                        break;
                    }
                    for (int i = 0; i <= maxy; i++)
                    {
                        for (int j = 0; j <= maxx; j++)
                        {
                            mat[i, j] = mat[i, j] || mat[i, maxx * 2 - j];

                        }
                    }
                }
            }

            var sum = 0;
            for (int i = 0; i <= maxy; i++)
            {
                for (int j = 0; j < maxx; j++)
                {
                    if (mat[i, j])
                        Console.Write("#");
                    else Console.Write(".");
                }
                Console.WriteLine();
            }

            Console.WriteLine(sum);


        }
    }
}