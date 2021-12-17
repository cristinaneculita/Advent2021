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
            var res = 0;
            var x1 = 150;
            var x2 = 193;
            var y1 = -136;
            var y2 = -86;
            var maxyall = int.MinValue;
            var homany = 0;
            for (var yv = 1000; yv > -1000; yv--)
            {
                for (var xv = -1000; xv < 1000; xv++)
                {
                    int maxy = int.MinValue;
                    int yvc = yv;
                    int xvc = xv;
                    int xc = 0;
                    int yc = 0;
                    while (!IntargetArea(xc, yc, x1, x2, y1, y2) && !(WayOverTarget(xc, yc, x1, x2, y1, y2)))
                    {
                        xc += xvc;
                        yc += yvc;
                        if (yc > maxy)
                            maxy = yc;
                        if (xvc > 0)
                            xvc--;
                        else if (xvc < 0)
                            xvc++;
                        yvc--;
                    }
                    if (IntargetArea(xc, yc, x1, x2, y1, y2))
                    {
                        //maxyall = maxy;
                        homany++;
                    }
               }
            }

            Console.WriteLine("response: " + homany);

        }

        private static bool WayOverTarget(int xc, int yc, int x1, int x2, int y1, int y2)
        {
            return yc < y1;
        }

        private static bool IntargetArea(int xc, int yc, int x1, int x2, int y1, int y2)
        {
            return (xc >= x1 && xc <= x2 && yc >= y1 && yc <= y2);
        }
    }
}