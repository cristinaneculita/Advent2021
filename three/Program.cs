using System;
using System.Collections.Generic;

namespace advent
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int coll = 12;
            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var response = 0;
            char[][] matr = new char[lines.Length][];
            for (int ii = 0; ii < lines.Length; ii++)
            {
                matr[ii] = new char[coll];
            }
            int i = 0;
            foreach (string line in lines)
            {

                // Use a tab to indent each line of the file.
                matr[i] = line.ToCharArray();
                i++;
                //Console.WriteLine("\t" + line);
            }
            var res1 = new char[coll];
           
            double dec = 0;
            for (int ix = coll; ix > 0;)
            {
                --ix;
                if (res1[ix] == '0')
                    continue;

                else if (res1[ix] == '1')
                    dec = Math.Pow(2, ix) + dec;


            }

            int zeross=0;int unuss=0;
            double dec2 = 0;
            for (int ix = coll; ix > 0;)
            {
                --ix;
                if (res1[ix] == '1')
                    continue;

                else if (res1[ix] == '0')
                    dec2 = Math.Pow(2, ix) + dec2;


            }

            char[][] matr2 = new char[lines.Length][];
            for (int ii = 0; ii < lines.Length; ii++)
            {
                matr2[ii] = new char[coll];
            }
            

            int oldind = lines.Length;
            for (int k = 0; k < coll; k++)
            {
                int ind = k;
                int indmat = 0;
                int keep = -1;
                zeross = 0; unuss = 0;
                for (int j = 0; j < oldind; j++)
                {
                    if (matr[j][k] == '0')
                        zeross++;
                    else unuss++;
                }
                if (zeross > unuss)
                        keep = 0;
                else keep = 1;

                for (int j = 0; j < oldind; j++)
                {
                    if (keep == 0)
                        if (matr[j][ind] == '0')
                        {
                            matr2[indmat] = matr[j];
                            indmat++;
                        }
                    if (keep == 1)
                        if (matr[j][ind] == '1')
                        {
                            matr2[indmat] = matr[j];
                            indmat++;
                        };
                }

                for (int a = 0; a < indmat; a++)
                    for (int b = 0; b < coll; b++)
                    {
                        matr[a][b] = matr2[a][b];
                    }
                oldind = indmat;
                if (indmat == 1)
                    break;
                    
            }

            dec = 0;
            for (int ix = coll; ix > 0;)
            {
                --ix;
                if (matr[0][coll-ix-1] == '0')
                    continue;

                else if (matr[0][coll-ix-1] == '1')
                    dec = Math.Pow(2, ix) + dec;

            }


             i = 0;
            foreach (string line in lines)
            {

                // Use a tab to indent each line of the file.
                matr[i] = line.ToCharArray();
                i++;
                //Console.WriteLine("\t" + line);
            }

            oldind = lines.Length;
            for (int k = 0; k < coll; k++)
            {
                int ind = k;
                int indmat = 0;
                int keep = -1;
                zeross = 0; unuss = 0;
                for (int j = 0; j < oldind; j++)
                {
                    if (matr[j][k] == '0')
                        zeross++;
                    else unuss++;
                }
                if (zeross <= unuss)
                    keep = 0;
                else keep = 1;

                for (int j = 0; j < oldind; j++)
                {
                    if (keep == 0)
                        if (matr[j][ind] == '0')
                        {
                            matr2[indmat] = matr[j];
                            indmat++;
                        }
                    if (keep == 1)
                        if (matr[j][ind] == '1')
                        {
                            matr2[indmat] = matr[j];
                            indmat++;
                        };
                }

                for (int a = 0; a < indmat; a++)
                    for (int b = 0; b < coll; b++)
                    {
                        matr[a][b] = matr2[a][b];
                    }
                oldind = indmat;
                if (indmat == 1)
                    break;

            }


            dec2 = 0;
            for (int ix = coll; ix > 0;)
            {
                --ix;
                if (matr[0][coll - ix - 1] == '0')
                    continue;

                else if (matr[0][coll - ix - 1] == '1')
                    dec2 = Math.Pow(2, ix) + dec2;


            }







            Console.WriteLine("raspunsul este: " + (dec * dec2));
            //Console.WriteLine("Hello World!");
        }
    }
}
