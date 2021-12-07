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
            var timer = new List<int>();
            timer = lines[0].Split(",").ToList().Select(e=>Int32.Parse(e)).ToList();
            List<long> content = new List<long>(9);
            for (int i = 0; i < 9; i++)
            {
                content.Add(0);
            }
            foreach (var item in timer)
            {
                content[item]++;
            }
            List<long> currentContent = new List<long>(9);
            for (int i = 0; i < 9; i++)
            {
                currentContent.Add(0);
            }

            var newTimer = new List<long>();
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < content.Count; j++)
                {
                    if (j == 0)
                    {
                        currentContent[6] = content[j];
                        currentContent[8] = content[j];
                        currentContent[j] = content[1];
                    }
                    else if (j < 6 || j == 7)
                    {
                        currentContent[j] = content[j + 1];
                    }
                    else if (j == 6)
                    {
                        currentContent[6] = currentContent[6] + content[7];
                    }
                }

                for (int k = 0; k < content.Count; k++)
                {
                    content[k] = currentContent[k];
                }
            }

            long s = 0;
            for (int k = 0; k < content.Count; k++)
            {
                s+= currentContent[k];
            }
            Console.WriteLine("raspunsul este: "+s);
            //Console.WriteLine("Hello World!");
        }
    }
}
