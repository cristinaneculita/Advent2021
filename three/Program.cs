using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    public class Basin
    {
        public int Size { get; set; }
        public int Sum { get; set; }
    }

    public class PointM
    {
      
        public PointM(int i, int k)
        {
            this.x = i;
            this.y = k;
        }

        public int x { get; set; }
        public int y { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
          
            
            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var response=0;
            int[,] mat = new int[lines.Length, lines[0].Length];
            int j = 0;
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                // Console.WriteLine("\t" + line);
                int[] l = new int[line.Length];
                char[] x = line.ToCharArray();
                for (int i = 0; i < x.Length; i++)
                {
                    l[i] = x[i]-'0';
                    mat[j, i] = l[i];
                }
                j++;
             }
            var isLow= false;
            int sum = 0;
            var bas = new List<Basin>();
                
            for (int i = 0; i < lines.Length; i++)
            {
                for (int k = 0; k < lines[0].Length; k++)
                {
                    if(isLowest(mat, i,k, lines.Length, lines[0].Length, bas))
                    {
                        sum += mat[i, k] + 1;
                    }
                }
            }
            var ord = bas.OrderByDescending(x => x.Size).ToList();

            var s = ord[0].Size * ord[1].Size * ord[2].Size;
            Console.WriteLine("raspunsul este: "+s);
            //Console.WriteLine("Hello World!");
        }

        private static bool isLowest(int[,] mat, int i, int k, int l1, int l2, List<Basin> bas)
        {
            int[,] matt = new int[l1+2, l2+2];
            for (int ii = 0; ii < l1+2; ii++)
            {
                for (int jj = 0; jj < l2+2; jj++)
                {
                    if (ii == 0 || jj == 0 || ii == l1+1 || jj == l2+1)
                        matt[ii, jj] = int.MaxValue;
                    else
                        matt[ii, jj] = mat[ii - 1, jj - 1];
                }
            }
            i++;
            k++;

            bool[,] matluate = new bool[l1 + 2, l2 + 2];
            for (int ii = 0; ii < l1 + 2; ii++)
            {
                for (int jj = 0; jj < l2 + 2; jj++)
                {
                    matluate[ii, jj] = false;
                }
            }
            if (matt[i, k] < matt[i - 1, k] && matt[i, k] < matt[i, k - 1] && matt[i, k] < matt[i, k + 1] && matt[i, k] < matt[i + 1, k])
            {
                var basinl = 1;
                var basinsum = matt[i,k];
                var vecini = new List<PointM>();
                vecini.Add(new PointM(i, k));
                matluate[i, k] = true;
                do
                { 
                    var v = vecini[0];
                    vecini.Remove(v);
                    var xv = v.x;
                    var yv = v.y;
                    var x = isLowerTanANeighAndNot9(matt, xv, yv, l1, l2,matluate);
                    if (x.Any())
                    {
                        foreach (var item in x)
                        {
                            basinl++;
                            basinsum += matt[item.x, item.y];
                            matluate[item.x, item.y] = true;
                            vecini.Add(item);
                        }
                    }

                } while (vecini.Any());
                bas.Add(new Basin() { Size = basinl, Sum = basinsum });
               return true;
            }
            return false;
        }

        private static List<PointM> isLowerTanANeighAndNot9(int[,] matt, int i, int k, int l1, int l2, bool[,]matluate)
        {
            var listres = new List<PointM>();
            int[,] matt2 = new int[l1 + 2, l2 + 2];
            for (int ii = 0; ii < l1 + 2; ii++)
            {
                for (int jj = 0; jj < l2 + 2; jj++)
                {
                    if (ii == 0 || jj == 0 || ii == l1 + 1 || jj == l2 + 1)
                        matt2[ii, jj] = int.MinValue;
                    else
                        matt2[ii, jj] = matt[ii, jj];
                }
            }
            if (matt2[i, k] < matt2[i - 1, k] && matt2[i-1, k] != 9 && !matluate[i - 1, k])
                listres.Add(new PointM(i - 1, k));
            if( matt2[i, k] < matt2[i, k + 1] && matt2[i, k+1] != 9 && !matluate[i,k+1])
                listres.Add(new PointM(i,k+1));
            if (matt2[i, k] < matt2[i + 1, k] && matt2[i+1, k] != 9 && !matluate[i+1, k])
                listres.Add(new PointM(i+1, k));
            if(matt[i,k] < matt2 [i,k-1] && matt2[i,k-1]!=9 && !matluate[i,k-1]) 
                listres.Add(new PointM(i, k-1));
            return listres;

        }
    }
}
