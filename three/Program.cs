using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class Program
    {
        static int minsum = Int32.MaxValue;

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            var mat = new int[lines.Length*5, lines[0].Length*5];

            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var res = 0; int i = 0;
            int j = 0;
            int maxi = lines.Length;
            int maxj = lines[0].Length;
            foreach (var line in lines)
            {
                j = 0;
                var x = line.ToCharArray();

                foreach (var item in x)
                {
                    mat[i, j] = Int32.Parse(item.ToString());
                    j++;
                }
                i++;
            }
            int initsum = 0;
            for (int ii = 1; ii < maxj; ii++)
            {
                initsum += mat[0, ii];
            }
            for (int ii = 0; ii < maxi; ii++)
            {
                initsum += mat[ii, maxj - 1];
            }


            var matrez = new int[maxi*5, maxj*5];
            for (i = 0; i < maxi; i++)
            {
                for (j = 0; j < maxi; j++)
                {
                   
                    for (int k = 1; k <5; k++)
                    {
                        mat[i+ k*maxi, j] = smecherie(mat[i, j] + k);
                    }

                }
            }
            for (i = 0; i < maxi*5; i++)
            {
                for (j = 0; j < maxi; j++)
                {

                    for (int k = 1; k < 5; k++)
                    {
                        mat[i , j + k * maxi] = smecherie(mat[i, j] + k);
                    }

                }
            }


            for (int x = 0; x < maxi * 5; x++)
            {
                for (int y = 0; y < maxi * 5; y++)
                {
                    Console.Write(mat[x, y]);
                }
                Console.WriteLine();
            }



            int rez = Dijkstra(mat, maxi*5, matrez);
            //int sum = 0;
            //for (int x = 0; x < maxi; x++)
            //{
            //    for (int y = 0; y < maxj; y++)
            //    {
            //        if (matrez[x, y] == 1)
            //            sum += mat[x, y];
            //    }
            //}
            //sum -= mat[0, 0];


            
            Console.WriteLine("response: " + rez);

        }

        private static int smecherie(int v)
        {
            if (v >= 10)
                return v-9;
            return v;
        }

        private static int Dijkstra(int[,] mat, int maxi, int[,] matrez)
        {
            int dist = 0;
            //matrez[0, 0] = 1;
            var x = 0; var y = 0;
            var matdist = new int[maxi, maxi];
            for (int i = 0; i < maxi; i++)
            {
                for (int j = 0; j < maxi; j++)
                {
                    matdist[i, j] = Int32.MaxValue;
                }
            }
            matdist[0, 0] = 0;
            while (matrez[maxi - 1, maxi - 1] == 0)
            {
                Point u = minDist(matdist, matrez,maxi);
                matrez[u.x, u.y] = 1;

                x = u.x - 1;
                y = u.y;
                if (x >= 0)
                {
                    if (matrez[x, y] == 0 && matdist[u.x, u.y] + mat[x, y] < matdist[x, y])
                        matdist[x, y] = matdist[u.x, u.y] + mat[x, y];
                }

                x = u.x;
                y = u.y - 1;
                if (y >= 0)
                {
                    if (matrez[x, y] == 0 && matdist[u.x, u.y] + mat[x, y] < matdist[x, y])
                        matdist[x, y] = matdist[u.x, u.y] + mat[x, y];
                }

                x = u.x + 1;
                y = u.y;
                if (x < maxi)
                {
                    if (matrez[x, y] == 0 &&  matdist[u.x, u.y] + mat[x, y] < matdist[x, y])
                        matdist[x, y] = matdist[u.x, u.y] + mat[x, y];
                }
                x = u.x;
                y = u.y + 1;
                if (y < maxi)
                {
                    if (matrez[x, y] == 0 && matdist[u.x, u.y] + mat[x, y] < matdist[x, y])
                        matdist[x, y] = matdist[u.x, u.y] + mat[x, y];
                }
            }
            return matdist[maxi - 1, maxi - 1];
        }
    

        private static Point minDist(int[,] dist, int[,] matrez, int  maxi)
        {
            int min = int.MaxValue;
            Point p = new Point(-1, -1);
            for (int i = 0; i < maxi; i++)
            {
                for (int j = 0; j < maxi; j++)
                {
                    if(matrez[i,j]==0 && dist[i,j]<min)
                    {
                        min = dist[i, j];
                        p.x = i;
                        p.y = j;
                    }
                }
            }
            return p;
        }
    }

    //private static bool SolveMaze(int[,] mat, int[,] matrez, int i, int j, int maxi, int  maxj, int initsum)
    //{


    //    if (isSafe(mat, matrez, i, j, initsum,maxi,maxj))
    //    {
    //        matrez[i, j] = 1;

    //        if (i == maxi - 1 && j == maxj - 1)
    //        {
    //            if (isSafe(mat, matrez, i, j, initsum, maxi, maxj))
    //                return true;
    //        }
    //        if (SolveMaze(mat, matrez, i + 1, j, maxi, maxj, initsum))
    //            return true;
    //        if (SolveMaze(mat, matrez, i, j+1, maxi, maxj, initsum))
    //            return true;
    //        //if (SolveMaze(mat, matrez, i -1, j, maxi, maxj, initsum))
    //        //    return true;
    //        //if (SolveMaze(mat, matrez, i , j-1, maxi, maxj, initsum))
    //        //    return true;
    //        matrez[i, j] = 0;
    //        return false;
    //    }
    //    return false;
    //}

    //private static bool isSafe(int[,] mat, int[,] matrez, int i, int j, int initsum, int maxi, int maxj)
    //{
    //    if (i < 0 || j < 0 || i >= maxi || j >= maxj)
    //        return false;
    //    int sum = 0;
    //    for (int x = 0; x < maxi; x++)
    //    {
    //        for (int y = 0; y < maxj; y++)
    //        {
    //            if (matrez[x, y] == 1)
    //                sum += mat[x, y];
    //        }
    //    }
    //    if (sum > initsum)
    //        return false;

    //    return true;
    //}
}