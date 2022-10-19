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
            var lins = lines.Length;
            var cols = lines[0].Length;
            var sea = new char[lins, cols];

            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var res = 0;
            var index = 0;
            foreach (var line in lines)
            {
                var c = line.ToCharArray();
                for (int i = 0; i < c.Length; i++)
                {
                    sea[index, i] = c[i];
                }
                index++;

            }


            int moved = 0;
            double step = 0;
            var seacur = new char[lins, cols];
            do
            {
                moved = 0;
                for (int i = 0; i < lins; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (sea[i, j] == '>')
                        {
                            int nextj = nextjm(j, cols);
                            if (sea[i, nextj] == '.')
                            {
                                seacur[i, nextj] = '>';
                                moved++;
                                seacur[i, j] = '.';
                            }
                            else
                                seacur[i, j] = '>';
                        }
                        else if (sea[i, j] == 'v')
                            seacur[i, j] = 'v';
                        else if (sea[i, j] == '.' && seacur[i,j]!='>')
                            seacur[i, j] = '.';

                    }
                }

                for (int i = 0; i < lins; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        sea[i, j] = seacur[i, j];
                    }
                }

                for (int i = 0; i < lins; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (sea[i, j] == 'v')
                        {
                            int nexti = nextim(i, lins);
                            if (sea[nexti, j] == '.')
                            {
                                seacur[nexti, j] = 'v';
                                moved++;
                                seacur[i, j] = '.';
                            }
                            else
                                seacur[i, j] = 'v';
                        }
                        else if (sea[i, j] == '>')
                            seacur[i, j] = '>';
                        else if (sea[i, j] == '.' && seacur[i, j] != 'v')
                            seacur[i, j] = '.';
                    }
                }

                for (int i = 0; i < lins; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        sea[i, j] = seacur[i, j];
                    }
                }

                step++;
                //for (int i = 0; i < lins; i++)
                //{
                //    for (int j = 0; j < cols; j++)
                //    {
                //        Console.Write(sea[i, j]);
                //    }
                //    Console.WriteLine();
                //}


            } while (moved > 0);


            Console.WriteLine("response: " + step);

        }

        private static int nextim(int i, int lins)
        {
            if (i < lins - 1)
                return i+1;
            return 0;
        }

        private static int nextjm(int j, int cols)
        {
            if (j < cols - 1)
                return j+1;
            return 0;
        }
    }
}