using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    public class Lvl {
        public int lvl { get; set; }
        public bool ch { get; set; }

        public Lvl(int lvl, bool ch)
        {
            this.lvl = lvl;
            this.ch = ch;
        }
    }
    class Program
    {
        public class Octopus {
            public int val { get; set; }
            public bool lit { get; set; }
            public Octopus(int v, bool lit)
            {
                this.val = v;
                this.lit = lit;
            }
        }

        public class PointO
        {
            public int x { get; set; }
            public int y { get; set; }
            public PointO(int i,int j)
            {
                x = i;y = j;
            }
        }
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");


            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var response = 0;
            var points = new List<String>();


            foreach (string line in lines)
            {
                var x = line.Split("-");
                if (!points.Contains(x[0]))
                    points.Add(x[0]);
                if (!points.Contains(x[1]))
                    points.Add(x[1]);
                // Use a tab to indent each line of the file.
                // Console.WriteLine("\t" + line);
            }

            var graf = new bool[13, 13];
            foreach (var item in lines)
            {
                    var x = item.Split("-");
                    Addway(graf, x[1], x[0], points);                 
                
            }

            var start = points.IndexOf("start");
            var end = points.IndexOf("end");
            var solutions = new List<List<int>>();
            var found = 0;



            var passed = new List<bool>();
            var current = start;
            var traseu = new List<int>();
            for (int i = 0; i < 13; i++)
            {
                passed.Add(false);
            }
            passed[current] = true;
            var nodes = new Stack<int>();
            nodes.Push(start);
            var depth = new Stack<int>();
            depth.Push(start);
            var nivel = new Stack<int>();
            nivel.Push(0);

            var goback = false;
            while (nodes.Count > 0)
            {
                var cur = nodes.Pop();
                var lvl = depth.Pop();
                var level = nivel.Pop();

                while (traseu.Count()!= level)
                {
                    traseu.RemoveAt(traseu.Count - 1);


                }
              
                //if (goback && traseu.Any())
                //{
                //    traseu.RemoveAt(traseu.Count - 1);                   
                //}
                // if(traseu.Contains(lvl))
                traseu.Add(cur);
                var somethingadded = false;


                for (int i = 0; i < 13; i++)
                {
                    if (graf[i, cur])
                    {
                        if (!isCapital(points, i) && traseu.Contains(i))
                        {
                            

                            continue;
                        }


                        if (i == end)
                        {
                            

                            if (isStraseuInSolution(solutions,traseu))
                            {
                                goback = true;
                            }
                            else
                            {
                                found++;
                                solutions.Add(new List<int>(traseu));
                            }
                            continue;

                        }
                        else
                        {
                            somethingadded = true;
                            nodes.Push(i);
                            depth.Push(cur);
                            nivel.Push(traseu.Count);
                        }
                    }
                }
            

                if (!somethingadded)
                {
                    traseu.RemoveAt(traseu.Count - 1);
                }

                

            }
     



            Console.WriteLine("raspunsul este: "+     found    );
            //Console.WriteLine("Hello World!");
        }

        private static bool isStraseuInSolution(List<List<int>> solutions, List<int> traseu)
        {
            foreach (var sol in solutions)
            {
                if (sol.Count == traseu.Count)
                {
                    int id = 0;
                    for (int i = 0; i < traseu.Count; i++)
                    {
                        if (sol[i] == traseu[i])
                            id++;                           
                    }
                    if (id == traseu.Count)
                        return true;
                }
            }
            return false;
        }

        private static bool isCapital(List<string> points, int i)
        {
            var p = points[i];
            foreach (var letter in p)
            {
                if (char.IsUpper(letter))
                    return true;
            }
            return false;
        }

        private static void Addway(bool[,] graf, string v, string item, List<string> points)
        {
            var indexi = points.IndexOf(item);
            var indexd = points.IndexOf(v);

            graf[indexi, indexd] = true;
            graf[indexd, indexi] = true;
        }
    }
}
