using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    class Content
    {
        public int Days { get; set; }
        public long Count { get; set; }
    }
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
            List<Content> content = new List<Content>(9);
            for (int i = 0; i < 9; i++)
            {
                content.Add(new Content() { Days = i, Count = 0 });
            }

            foreach (var item in timer)
            {
                content[item].Count++;
            }


            List<Content> contentCur = new List<Content>(9);
            for (int i = 0; i < 9; i++)
            {
                contentCur.Add(new Content() { Days = i, Count = 0 });
            }

            var newTimer = new List<int>();
            for (int i = 0; i < 256; i++)
            {
                //foreach (var item in timer)
                //{
                //    if (item == 0)
                //    {
                //        newTimer.Add(6); newTimer.Add(8);
                //    }
                //    else
                //        newTimer.Add(item - 1);
                //}
                //timer = new List<int>();
                //foreach (var item in newTimer)
                //{
                //    timer.Add(item);
                //}

                for (int j = 0; j < content.Count; j++)
                {
                    if (j == 0)
                    {
                        contentCur[6].Count = content[j].Count;
                        contentCur[8].Count = content[j].Count;
                        contentCur[j].Count = content[1].Count;
                    }
                    else if (j < 6 || j == 7)
                    {
                        contentCur[j].Count = content[j + 1].Count;
                    }
                    else if (j == 6)
                    {
                        contentCur[6].Count = contentCur[6].Count + content[7].Count;
                    }
                }

                for (int k = 0; k < content.Count; k++)
                {
                    content[k].Count = contentCur[k].Count;
                }
            }

            long s = 0;
            for (int k = 0; k < content.Count; k++)
            {
                s+= contentCur[k].Count;
            }
            Console.WriteLine("raspunsul este: "+s);
            //Console.WriteLine("Hello World!");
        }
    }
}
