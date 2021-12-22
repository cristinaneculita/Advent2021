using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    public class Lastpos
    {
        public int pos { get; set; }
        public double howmany { get; set; }
        public Lastpos(int p, double h)
        {
            pos = p;
            howmany = h;
        }
    }

    public class scor
    {
        public double val { get; set; }
        public int deunde { get; set; }
        public scor(double val, int deunde)
        {
            this.val = val;
            this.deunde = deunde;
        }
    }
    class Program
    {
        static double universesw1;
        static double universesw2;
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int res = 0;
            int player1pos = 2;
            int player2pos = 8;
            int p1score = 0;
            int p2score = 0;
            int curdice = 0;
            int player = 1;
            int numberofrols = 0;
            universesw1 = 0;
            universesw2 = 0;

            double[] player1posscores = new double[31];
            double[] player2posscores = new double[31];

            double[] player1posa = new double[11];
            double[] player2posa = new double[11];
            player1posa[player1pos] = 1;
            player2posa[player2pos] = 1;


            int[] curscore = new int[] { 0, 0, 0, 1, 3, 6, 7, 6, 3, 1 };
            double[,,,] lastpos = new double[21, 11, 21, 11];

            double totalwins1 = 0;
            double totalwins2 = 0;


            //calcafterfirstdicep1(player1posscores, curscore, player1pos, player2pos, lastpos);
            //player1posscores = totalp1(lastpos);
            //calcafterfirstdicep2(player2posscores, curscore, player2pos, lastpos);
            //player2posscores = totalp2(lastpos);

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    for (int k = 0; k < 21; k++)
                    {
                        for (int t = 0; t < 11; t++)
                        {
                            if (j == player1pos && t == player2pos && i==0 && k==0)
                                lastpos[i, j, k, t] = 1;
                        }
                    }
                }
            }
            int diceroll = 0;
            do
            {
                if (player == 1)
                {
                    var mat = calcregularp1(player1posscores, curscore, lastpos);
                    player1posscores = totalp1(mat);
                    player2posscores = totalp2(mat);
                    lastpos = copy(mat);

                }
                else
                {
                    var mat = calcregularp2(player2posscores, curscore, lastpos);
                    player1posscores = totalp1(mat);
                    player2posscores = totalp2(mat);
                    lastpos = copy(mat);

                }

                if (player == 1)
                {
                    player = 2;
                }
                else
                {

                    player = 1;
                }

                diceroll++;
            } while ((!everybodywins(player1posscores) && !everybodywins(player2posscores)) ||diceroll<2);

            double resukt = universesw1 > universesw2 ? universesw1 : universesw2;




            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");

            Console.WriteLine("response: " + resukt);

        }

        private static double[,,,] calcregularp2(double[] player2posscores, int[] curscore, double[,,,] lastpos)
        {
            double wins = 0;
            double[,,,] lastposc = new double[21, 11, 21, 11];
            for (int k = 0; k < 21; k++)
                for (int t = 1; t <=10; t++)
                {
                    for (int i = 0; i < 21; i++)
                    {

                        for (int j = 1; j <= 10; j++)
                        {
                            double oameni = lastpos[k, t, i, j];
                            if (oameni > 0)
                            {
                                double[] curentpos = currentpos(j, oameni, curscore);
                                scor[] scorf = getscore(i, curentpos);
                                for (int x = 1; x < 31; x++)
                                {
                                    if (scorf[x] != null && scorf[x].val > 0 && x < 21)
                                        lastposc[k, t, x, scorf[x].deunde] = lastposc[k, t, x, scorf[x].deunde] + scorf[x].val;
                                    if (x >= 21 && scorf[x] != null && scorf[x].val > 0)
                                    {
                                        universesw2 += scorf[x].val;
                                        wins += scorf[x].val;
                                        //lastposc[k, t, i, j] = 0;
                                    }
                                }
                            }



                        }
                    }
                }
            return lastposc;
        }


        private static double[,,,] copy(double[,,,] mat)
        {
            var res = new double[21, 11, 21, 11];
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    for (int k = 0; k < 21; k++)
                    {
                        for (int t = 0; t < 11; t++)
                        {
                            res[i, j, k, t] = mat[i, j, k, t];
                        }
                    }
                }
            }
            return res;
        }

        private static double[] totalp1(double[,,,] mat)
        {
            var res = new double[21];
            for (int k = 0; k < 21; k++)
                for (int t = 1; t < 11; t++)
                {
                    for (int i = 0; i < res.Length; i++)
                    {
                        for (int j = 1; j <= 10; j++)
                        {
                            res[i] += mat[i, j, k, t];
                        }
                    }
                }

            return res;
        }

        private static double[] totalp2(double[,,,] mat)
        {
            var res = new double[21];
            for (int k = 0; k < 21; k++)
                for (int t = 1; t < 11; t++)
                {
                    for (int i = 0; i < res.Length; i++)
                    {
                        for (int j = 1; j <= 10; j++)
                        {
                            res[k] += mat[i, j, k, t];
                        }
                    }
                }

            return res;
        }


        private static double[,,,] calcregularp1(double[] player1posscores, int[] curscore, double[,,,] lastpos)
        {
            double wins = 0;
            double[,,,] lastposc = new double[21, 11, 21, 11];
            for (int k = 0; k < 21; k++)
                for (int t = 1; t <= 10; t++)
                {
                    for (int i = 0; i < 21; i++)
                    {
                        for (int j = 1; j <= 10; j++)
                        {

                            double oameni = lastpos[i, j, k, t];
                            if (oameni > 0)
                            {
                                double[] curentpos = currentpos(j, oameni, curscore);
                                scor[] scorf = getscore(i, curentpos);
                                for (int x = 1; x < 31; x++)
                                {
                                    if (scorf[x] != null && scorf[x].val > 0 && x < 21)
                                        lastposc[x, scorf[x].deunde, k, t] = lastposc[x, scorf[x].deunde, k, t] + scorf[x].val;
                                    if (x >= 21 && scorf[x] != null && scorf[x].val > 0)
                                    {
                                        universesw1 += scorf[x].val;
                                        wins += scorf[x].val;
                                        //lastposc[i, j, k, t] = 0;
                                    }
                                }
                            }
                            
                        }

                    }
                }

            return lastposc;
        }


        private static void calcafterfirstdicep2(double[] player2posscores, int[] curscore, int player2pos, double[,,,] lastpos)
        {
            for (int k = 0; k < 21; k++)
            {
                for (int j = 1; j < 11; j++)
                {
                    //var playerpos = j;
                    for (int i = 1; i < 10; i++)
                    {

                        var ind = i + player2pos > 10 ? (i + player2pos - 10) : i + player2pos;
                        player2posscores[ind] = curscore[i];
                        if (curscore[i] > 0)
                            lastpos[k, j, ind, ind] = curscore[i];
                    }
                }
            }

        }

        private static void mult(double[,] lastpos1, double v)
        {
            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    lastpos1[i, j] += v;
                }
            }
        }

        private static void multiply(double[] player2posscores, double v)
        {
            for (int i = 0; i < player2posscores.Length; i++)
            {
                player2posscores[i] += v;
            }
        }

        private static double checknewwins(double[] player1posscores, double[,] mat)
        {
            double result = 0;
            for (int i = 21; i < 31; i++)
            {
                result += player1posscores[i];
                if (player1posscores[i] > 0)
                {
                    for (int j = 0; j <= 10; j++)
                    {
                        mat[i, j] = 0;
                    }
                    player1posscores[i] = 0;

                }

            }
            return result;
        }

        private static double[,] copy(double[,] mat)
        {
            var res = new double[31, 11];
            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    res[i, j] = mat[i, j];
                }
            }
            return res;
        }

        private static double[] total(double[,] mat)
        {
            var res = new double[31];
            for (int i = 0; i < res.Length; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    res[i] += mat[i, j];
                }
            }
            return res;
        }

        private static Tuple<double[,], double> calcregular(double[] player1posscores, int[] curscore, double[,] lastpos1)
        {
            double[,] lastposcur = new double[31, 11];
            double totalc = 0;
            for (int i = 1; i < 21; i++)
            {
                if (player1posscores[i] > 0)
                {
                    for (int j = 1; j <= 10; j++)
                    {
                        double oameni = lastpos1[i, j];
                        if (oameni > 0)
                        {
                            double[] curentpos = currentpos(j, oameni, curscore);
                            scor[] scorf = getscore(i, curentpos);
                            for (int x = 1; x < 31; x++)
                            {
                                if (scorf[x] != null && scorf[x].val > 0)
                                    lastposcur[x, scorf[x].deunde] = lastposcur[x, scorf[x].deunde] + scorf[x].val;
                                if (x < 21 && scorf[x] != null && scorf[x].val > 0)
                                {
                                    totalc += scorf[x].val;
                                }
                            }
                        }

                    }

                }
            }
            return new Tuple<double[,], double>(lastposcur, totalc);
        }

        private static scor[] getscore(int scori, double[] curentpos)
        {
            var res = new scor[31];
            for (int i = 1; i < 11; i++)
            {
                res[i + scori] = new scor(curentpos[i], i);
            }
            return res;
        }

        private static double[] currentpos(int j, double oameni, int[] curscore)
        {
            var res = new double[11];
            for (int i = 1; i < 10; i++)
            {
                var ind = i + j > 10 ? (i + j - 10) : i + j;
                res[ind] = oameni * curscore[i];
            }
            return res;
        }

        private static void calcafterfirstdicep1(double[] player1posscores, int[] curscore, int player1pos, int playerpos2, double[,,,] lastpos)
        {

            for (int j = 0; j < 11; j++)
            {
                if (j == playerpos2)
                    for (int i = 1; i < 10; i++)
                    {
                        var ind = i + player1pos > 10 ? (i + player1pos - 10) : i + player1pos;
                        //player1posscores[ind] = curscore[i];
                        if (curscore[i] > 0)
                            lastpos[ind, ind, 0, j] = curscore[i];
                    }
            }


        }

        private static bool everybodywins(double[] player1posscores)
        {
            for (int i = 0; i < 21; i++)
                if (player1posscores[i] > 0)
                    return false;
            return true;
        }

        private static bool wins(double[] playerposscores)
        {
            for (int i = 21; i <= 29; i++)
            {
                if (playerposscores[i] > 0)
                    return true;
            }
            return false;
        }

        private static void calcscore(double[] playerposscores, double[] playerposa)
        {
            double[] playernewscores = new double[31];
            for (int i = 0; i < 21; i++)
            {

                for (int j = 1; j <= 10; j++)
                {
                    double catisuntpeposj = playerposa[j];
                    double catiauscori = playerposscores[i];
                    var noulscor = i + j;
                    playernewscores[noulscor] = playernewscores[noulscor] + catiauscori + catisuntpeposj;

                    playerposscores[i] -= catiauscori;
                }
            }

        }


        private static int claclpos(int pos, int curscore)
        {
            while (curscore > 0)
            {
                pos = nextpos(pos);
                curscore--;
            }
            return pos;
        }

        private static int nextpos(int pos, int curscore)
        {
            return (pos + curscore) > 10 ? pos + curscore - 10 : pos + curscore;
        }

        private static int nextpos(int curscore)
        {
            if (curscore + 1 > 10)
                return 1;
            else return curscore + 1;
        }

        private static int nextdice(int curdice)
        {
            if (curdice + 1 > 10)
                return 1;
            else return curdice + 1;
        }
    }
}