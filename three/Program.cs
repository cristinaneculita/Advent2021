using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    class Point {
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
            var response=0;
            List<Point> coord = new List<Point>();
            foreach (string line in lines)
            {
                var l = line.Split(" -> ");
                var l1 = l[0].Split(",").ToList()
                var l2 = l[1].Split(",");
                var p = new Point()
                // Use a tab to indent each line of the file.
               // Console.WriteLine("\t" + line);
            }
            
            Console.WriteLine("raspunsul este: "+response);
            //Console.WriteLine("Hello World!");
        }
    }
}
