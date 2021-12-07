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
            var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var response=0;
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
               // Console.WriteLine("\t" + line);
            }
            
            Console.WriteLine("raspunsul este: "+response);
            //Console.WriteLine("Hello World!");
        }
    }
}
