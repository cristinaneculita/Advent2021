using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    class Point
    {
        public int x1 { get; set; }
        public int x2 { get; set; }
        public int y1 { get; set; }
        public int y2 { get; set; }
        public Point(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var response = 0;
            List<Point> coord = new List<Point>();
            int maxx = 0, maxy = 0;
            foreach (string line in lines)
            {
                var l = line.Split(" -> ");
                var l1 = l[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToArray();
                var l2 = l[1].Split(",").ToList().Select(e => Int32.Parse(e)).ToArray(); ;
                var p = new Point(l1[0], l1[1], l2[0], l2[1]);
                if (p.x1 > maxx)
                    maxx = p.x1;
                if (p.x2 > maxx)
                    maxx = p.x2;
                if (p.y1 > maxy)
                    maxy = p.y1;
                if (p.y2 > maxy)
                    maxy = p.y2;
                // Use a tab to indent each line of the file.
                // Console.WriteLine("\t" + line);
                coord.Add(p);
            }
            int[,] mat = new int[990, 991];


            foreach (var p in coord)
            {
                if (p.x1 == p.x2)
                {
                    for (int i = p.y1; i <= p.y2; i++)
                    {
                        mat[p.x1, i]++;
                    }
                    for (int i = p.y2; i <= p.y1; i++)
                    {
                        mat[p.x1, i]++;
                    }
                }
                else if (p.y1 == p.y2)
                {
                    for (int i = p.x1; i <= p.x2; i++)
                    {
                        mat[i, p.y1]++;
                    }
                    for (int i = p.x2; i <= p.x1; i++)
                    {
                        mat[i, p.y1]++;
                    }
                }
                else
                {
                    maxx = p.x1 > p.x2 ? p.x1 : p.x2;
                    int minx = p.x1 < p.x2 ? p.x1 : p.x2;
                    //maxy = p.y1 > p.y2 ? p.y1 : p.y2;
                    //int miny = p.y1 < p.y2 ? p.y1 : p.y2;
                    bool ycresc = p.y1 < p.y2;
                    bool xcredc = p.x1 < p.x2;
                    int cy = p.y1;
                    int cx = p.x1;


                    while (cx <= maxx && cx>=minx)
                    {
                        mat[cx, cy]++;
                        if (ycresc) cy++;
                        else cy--;
                        if (xcredc) cx++;
                        else cx--;
                    }
                   
                }


            }

            int s = 0;
            for (int i = 0; i < 990; i++)
                for (int j = 0; j < 991; j++)
                {
                    if (mat[i, j] >= 2)
                        s++;
                }
            Console.WriteLine("raspunsul este: " + s);
            //Console.WriteLine("Hello World!");
        }
    }
}
