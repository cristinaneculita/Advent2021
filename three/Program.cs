using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
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
            var response=0;
            var mat = new int[12, 12];
            int i = 1;
            foreach (string line in lines)
            {
                int j = 1;
                foreach (var car in line)
                {
                    mat[i, j] = car - '0';
                    j++;
                }
                i++;
                // Use a tab to indent each line of the file.
               // Console.WriteLine("\t" + line);
            }

            for (int ii = 0; ii < 12; ii++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (ii == 0 || j == 0 || ii == 11 || j == 11)
                        mat[ii, j] = int.MaxValue;
                }
            }
            var matr = new Octopus[12, 12];

            for (int x = 1; x <= 10; x++)
            {
                for (int y = 1; y <= 10; y++)
                {
                    matr[x, y] = new Octopus(mat[x, y], false);                 
                }
            }

            for (int ii = 0; ii < 12; ii++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (ii == 0 || j == 0 || ii == 11 || j == 11)
                        matr[ii, j] = new Octopus(Int32.MaxValue, true);
                }
            }

            var totallitted = 0;
            var lista = new List<PointO>();
            var all0 = 0;
            for (int step = 0; step < 1000; step++)
            {
               

                for (int x = 1; x <= 10; x++)
                {
                    for (int y = 1; y <= 10; y++)
                    {
                        matr[x, y].val = matr[x, y].val+1;
                        matr[x, y].lit = false;
                        if (matr[x, y].val == 10)
                        {
                            lista.Add(new PointO(x,y));
                            matr[x, y].lit = true;
                            matr[x, y].val = 0;                           
                        }
                    }
                }

                for (int ii = 0; ii < 12; ii++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (ii == 0 || j == 0 || ii == 11 || j == 11)
                            matr[ii, j] = new Octopus(Int32.MaxValue, true);
                    }
                }
                int litted = 0;
                var somthinglit = false;

                while(lista.Any())
                {
                    var p = lista[0];
                    var l = p.x;
                    var c = p.y;
                    Mark(l - 1, c - 1, matr, lista);
                    Mark(l - 1, c, matr, lista);
                    Mark(l - 1, c + 1, matr, lista);
                    Mark(l, c - 1, matr, lista);
                    Mark(l, c + 1, matr, lista);
                    Mark(l + 1, c - 1, matr, lista);
                    Mark(l + 1, c, matr, lista);
                    Mark(l + 1, c + 1, matr, lista);
                    lista.Remove(p);
                    totallitted++;
                }

                var allflash = true;
                for (int x = 1; x <= 10; x++)
                {
                    for (int y = 1; y <= 10; y++)
                    {
                        if (matr[x, y].val != 0)
                            allflash = false;
                    }
                }
                if (allflash)
                {
                    all0 = step + 1;
                    break;
                }



                //  do


                //  {
                //     for (int l = 1; l <= 10; l++)
                //     {
                //         for (int c = 1; c <= 10; c++)
                //         {
                //         if (matr[l, c].val == 0 && !matr[l, c].lit)
                //         {



                //             litted += Mark(l - 1, c - 1, matr);
                //             litted += Mark(l - 1, c, matr);
                //             litted += Mark(l - 1, c + 1, matr);
                //             litted += Mark(l, c - 1, matr);
                //             litted += Mark(l, c + 1, matr);
                //             litted += Mark(l + 1, c - 1, matr);
                //             litted += Mark(l + 1, c, matr);
                //             litted += Mark(l + 1, c + 1, matr);
                //             totallitted += litted;

                //         }


                //                 matr[l, c].lit = true;
                //             }
                //         }
                //     }
                //// } while (somthinglit);


                //do
                //{
                //    litted = 0;
                //    for (int x = 1; x <= 10; x++)
                //    {
                //        for (int y = 1; y <= 10; y++)
                //        {
                //            if (matr[x, y].val==0)
                //            {
                //                litted += Mark(x - 1, y - 1, matr);
                //                litted += Mark(x - 1, y, matr);
                //                litted += Mark(x - 1, y + 1, matr);
                //                litted += Mark(x, y - 1, matr);
                //                litted += Mark(x, y + 1, matr);
                //                litted += Mark(x + 1, y - 1, matr);
                //                litted += Mark(x + 1, y, matr);
                //                litted += Mark(x + 1, y + 1, matr);
                //            }
                //        }
                //    }
                //    totallitted += litted;
                //} while (litted > 0);
            }

            Console.WriteLine("raspunsul este: "+all0);
            //Console.WriteLine("Hello World!");
        }

        private static int Mark(int v1, int v2, Octopus[,] matr, List<PointO> lista)
        {
            if (!matr[v1, v2].lit)
            {
                matr[v1, v2].val++;
                if (matr[v1, v2].val == 10)
                {
                    matr[v1, v2].val = 0;
                    matr[v1, v2].lit = true;
                    lista.Add(new PointO(v1,v2));
                    return 1;
                }
            }
            return 0;
        }
    }
}
