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
            //   var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();
             
            var outputl = new List<List<string>>();
            var outputin = new List<List<string>>();
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                // Console.WriteLine("\t" + line);
                var x = line.Split(" | ");
                var y = x[1].Split(" ");
                var z = x[0].Split(" ");

                outputl.Add(y.ToList());
                outputin.Add(z.ToList());
            }
            var sum = 0;
            foreach (var y in outputl)
            {
                foreach (var item in y)
                {
                    if (item.Length == 2 | item.Length == 3 || item.Length == 4 || item.Length == 7)
                        sum++;
                }
            }
  
            long summ = 0;
            foreach (var y in outputin)
            {
                var n1 = y.Where(e => e.Length == 2).Single();
                var n4 = y.Where(e => e.Length == 4).Single();
                var dif14 = n4.Replace(n1[0], ' ').Replace(n1[1], ' ').Where(c => !char.IsWhiteSpace(c)).ToArray();

                var index = outputin.IndexOf(y);
                var outp = outputl[index];
                var num = new int[4];
                int ind = 0;
                foreach (var item in outp)
                {
                    var cee = -1;
                    if (item.Length == 2)
                    {
                        cee = 1;
                    }
                    if (item.Length == 3)
                    {
                        cee = 7;
                    }
                    if (item.Length == 4)
                    {
                        cee = 4;
                    }
                    if (item.Length == 5)
                    {
                        if (item.Contains(n1[0]) && item.Contains(n1[1]))
                        {
                            cee = 3;
                        }
                        else if (item.Contains(dif14[0]) && item.Contains(dif14[1]))
                        {
                            cee = 5;
                        }
                        else
                        {
                            cee = 2;
                        }
                    }
                    if (item.Length == 6)
                    {
                        if (item.Contains(n1[0]) && item.Contains(n1[1]))
                        {
                            if (item.Contains(dif14[0]) && item.Contains(dif14[1]))
                            {
                                cee = 9;
                            }
                            else
                            {
                                cee = 0;
                            }
                        }
                        else
                        {
                            cee = 6;
                        }
                    }
                    if (item.Length == 7)
                        cee = 8;
                   
                    num[ind] = cee;
                    ind++;
                }

                var nrc = 1000 * num[0] + 100 * num[1] + 10 * num[2] + num[3];

                summ += nrc;
            }

            Console.WriteLine("raspunsul este: " + summ);
            //Console.WriteLine("Hello World!");
        }
    }
}
