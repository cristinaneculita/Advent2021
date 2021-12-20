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
            var alg = lines[0];

            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var res = 0;
            int pasi = 55;
            int matilinii = lines.Length - 2;
            int maticol = lines[2].Length;
            int[,] matr= new int[matilinii + 2*(pasi+1), maticol + 2* (pasi+1)];

            for (int i = 0; i < pasi+1; i++)
            {
                for (int j = 0; j < maticol+2*(pasi+1); j++)
                {
                    matr[i, j] = 0;
                }
            }
            for (int ind = 2; ind < lines.Length; ind++)
            {
                var l = lines[ind].ToCharArray();
                var lineinmatrix = ind - 2;
                for (int i = 0; i < maticol +2*(pasi+1); i++)
                {
                    if (i < pasi+1 || i >= (maticol + pasi+1))
                    {
                        matr[lineinmatrix+pasi+1, i] = 0;
                    }
                    else
                    {
                        if (l[i - pasi-1] == '.')
                            matr[lineinmatrix+pasi+1, i] = 0;
                        else
                            matr[lineinmatrix+pasi+1, i] = 1;
                    }
                }
            }

            for (int i = 0; i < matilinii + 2 * (pasi + 1); i++)
            {
                for (int j = 0; j < maticol + 2 * (pasi + 1); j++)
                {
                    if (matr[i, j] == 0)
                        Console.Write(".");
                    else
                        Console.Write("#");

                }
                Console.WriteLine();
            }

            
            int[,] matrnew = new int[matilinii + 2 * (pasi+1), maticol + 2 * (pasi+1)];


            for (int pas = 0; pas < 50; pas++)
            {
                Console.WriteLine($"i merge de la {pasi-pas}pana la{matilinii + pasi + 1 + pas}");
                //for (int i = pasi-pas; i <= matilinii+pasi+1+pas; i++)
                for (int i = 0; i < matilinii+2*(pasi+1); i++)
                {
                    if(i==pasi-pas)
                        Console.WriteLine($"j merge de la {pasi-pas}pana la{maticol + pasi + 1 + pas}");

                  //  for (int j = pasi-pas; j <= maticol+pasi+1+pas; j++)
                    for (int j = 0; j < matilinii + 2 * (pasi + 1); j++)
                    {
                        
                        var str = new int[9];
                        str[0] = matriceextisa(i - 1, j - 1, matr, matilinii + 2 * (pasi + 1));
                       
                        str[1] = matriceextisa(i - 1, j, matr, matilinii + 2 * (pasi + 1));
                        str[2] = matriceextisa(i - 1, j + 1, matr, matilinii + 2 * (pasi + 1));
                        str[3] = matriceextisa(i, j - 1, matr, matilinii + 2 * (pasi + 1));
                        str[4] = matriceextisa(i, j, matr, matilinii + 2 * (pasi + 1));
                        str[5] = matriceextisa(i, j + 1, matr, matilinii + 2 * (pasi + 1));
                        str[6] = matriceextisa(i + 1, j - 1, matr, matilinii + 2 * (pasi + 1));
                        str[7] = matriceextisa(i + 1, j, matr, matilinii + 2 * (pasi + 1));
                        str[8] = matriceextisa(i + 1, j + 1, matr, matilinii + 2 * (pasi + 1));
                        int intrez = transform(str);
                        char caracteralg = alg[intrez];
                        if (caracteralg == '#')
                            matrnew[i, j] = 1;
                        else
                            matrnew[i, j] = 0;
                    }
                }
                for (int i = 0; i < matilinii+2*(pasi+1); i++)
                {
                    for (int j = 0; j < maticol +2*(pasi+1); j++)
                    {
                        matr[i, j] = matrnew[i, j];
                    }
                }

                for (int i = 0; i < matilinii + 2 * (pasi + 1); i++)
                {
                    for (int j = 0; j < maticol + 2 * (pasi + 1); j++)
                    {
                        if (matr[i, j] == 0)
                            Console.Write(".");
                        else
                            Console.Write("#");

                    }
                    Console.WriteLine();
                }

            }

            var sum = 0;
            for (int i = 0; i < matilinii + 2 * (pasi + 1); i++)
            {
                for (int j = 0; j < maticol + 2 * (pasi + 1); j++)
                {
                    sum+=matr[i, j];
                }
            }
            Console.WriteLine("response: " + sum);

        }

        private static int matriceextisa(int v1, int v2, int[,] matr, int dim)
        {
            if (v1 < 0 || v1 >= dim || v2 < 0 || v2 >= dim)
                return matr[0, 0];
            return matr[v1, v2];

        }

        private static int transform(int[] str)
        {
            double num = 0;
            for (int i = 0; i < 9; i++)
            {
                num += str[i] * Math.Pow(2, 8 - i);
            }
            return (int)num;
        }
    }
}