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

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var response=0;
            var coord = new List<int>();
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
               // Console.WriteLine("\t" + line);
            }
            coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();
            var maxc = coord.Max();
            long fuel = 0;
            long minfuel = Int32.MaxValue;
            for (int i = 0; i <= maxc; i++)
            {
                fuel = 0;
                foreach (var item in coord)
                {
                    fuel += ceva(Math.Abs(item - i));
                }
                if (fuel < minfuel)
                    minfuel = fuel;
            }


            Console.WriteLine("raspunsul este: "+minfuel);
            //Console.WriteLine("Hello World!");
        }

        private static int ceva(int v)
        {
            int s = 0;
            for (int i = 1; i <= v; i++)
            {
                s += i;
            }
            return s;
        }
    }
}
