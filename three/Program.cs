using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace advent
{
    class Move : IEquatable<Move>, IComparable<Move>
    {
        public char[,] puz { get; set; }
        public double Price { get; set; }
        public Move(char[,] puzzle)
        {
            puz = new char[5, 11];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    puz[i, j] = puzzle[i, j];
                }
            }
        }

        public bool Success { get; set; }
        public double FinalPrice { get; set; }

        public bool Equals([AllowNull] Move other)
        {
            if (other.Price != Price)
                return false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (other.puz[i, j] != puz[i, j])
                        return false;
                }
            }
            return true;
        }

        public int CompareTo([AllowNull] Move other)
        {
            if (other.Price > Price)
                return -1;
            if (other.Price == Price)
                return 0;
            if (other.Price < Price)
                return 1;
            return 0;
        }
    }
    static class extensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
    class Program
    {

        public static double minprice;
        public static int movest;
        public static List<Move> moveshist;
        public static List<List<Move>> movessuccess;
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");


            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            double res = 0;
            movest = 0;
            minprice = 44017;
            moveshist = new List<Move>();
            movessuccess = new List<List<Move>>();
            var puzzle = new char[5, 11]
            {{'.', '.','.','.','.','.','.','.','.','.','.' },
            {'#','#','C','#','A','#','B','#','D','#', '#' },
            {'#','#','D','#','C','#','B','#','A','#', '#' },
            {'#','#','D','#','B','#','A','#','C','#', '#' },
            {'#','#','B','#','A','#','D','#','C','#', '#' }};
            var move = new Move(puzzle);
            // Play(move);

            double[] price = new double[100];
            List<Move> chain = new List<Move>();
            var movesfromchain = new List<Move>();
            chain.Add(move);

            while (!IsFinal(move.puz))
            {
                var posm = FindPossibleMoves(move);
                foreach (var pos in posm)
                {
                    if (!movesfromchain.Contains(pos) && !chain.Contains(pos))
                        movesfromchain.Add(pos);
                }
                

                //movesfromchain = movesfromchain.Where(m => !chain.Contains(m)).ToList();
                var movemin = movesfromchain.Min();
                Console.WriteLine(" " + movemin.Price);
                chain.Add(movemin);
                movesfromchain.Remove(movemin);

                //foreach (var chainmove in chain)
                //{
                //    var posm = FindPossibleMoves(chainmove);
                //    foreach (var pos in posm)
                //    {
                //        if (!chain.Contains(pos))
                //            movesfromchain.Add(pos);
                //    }
                //}
                //for (int i = 0; i < 5; i++)
                //{
                //    for (int j = 0; j < 11; j++)
                //    {
                //        Console.Write(movemin.puz[i, j]);
                //    }
                //    Console.WriteLine();
                //}
                //Console.ReadKey();


                move = movemin;

            }





            Console.WriteLine("response: " + move.Price);

        }

        private static bool Play(Move puzzle)
        {
            moveshist.Add(puzzle);
            if (IsFinal(puzzle.puz))
            {
                movessuccess.Add(moveshist);
                puzzle.Success = true;
                puzzle.FinalPrice = puzzle.Price;
                if (minprice > puzzle.Price)
                {
                    Console.WriteLine("pret: " + puzzle.Price);
                    minprice = puzzle.Price;

                }
                return true;
            }
            else
            {

                List<Move> moves;
                moves = FindPossibleMoves(puzzle);
                bool result = false;
                moves.Shuffle();
                var l_CurrentStack = new System.Diagnostics.StackTrace(true);


                foreach (var move in moves)
                {

                    if (l_CurrentStack.FrameCount == 2)
                        Console.WriteLine(moves.IndexOf(move));

                    //for (int i = 0; i < 5; i++)
                    //{
                    //    for (int j = 0; j < 11; j++)
                    //    {
                    //        Console.Write(move.puz[i, j]);
                    //    }
                    //    Console.WriteLine();
                    //}
                    //Console.ReadKey();

                    if (move.Price > minprice)
                    {
                        moveshist.Remove(move);
                        result = false;
                    }
                    else
                    {
                        var res = Play(move);
                        if (!res)
                            moveshist.Remove(move);
                        result |= res;
                    }

                }
                return result;
            }
        }

        private static List<Move> FindPossibleMoves(Move puzzle)
        {
            var posmoves = new List<Move>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (Char.IsLetter(puzzle.puz[i, j]))
                        posmoves.AddRange(FindPosMoves(puzzle, i, j, puzzle.puz[i, j]));
                }
            }
            return posmoves;

        }

        private static List<Move> FindPosMoves(Move puzzle, int i, int j, char v)
        {
            var posmoves = new List<Move>();
            if (i == 0)
                switch (v)
                {
                    case 'A':
                        var move = RoadFree(i, j, 4, 2, puzzle, 'A');
                        if (puzzle.puz[4, 2] == '.' && move != null)
                        {
                            posmoves.Add(move);
                            break;
                        }
                        move = RoadFree(i, j, 3, 2, puzzle, 'A');
                        if (puzzle.puz[3, 2] == '.' && move != null && puzzle.puz[4, 2] == 'A')
                        {
                            posmoves.Add(move);
                            break;
                        }
                        move = RoadFree(i, j, 2, 2, puzzle, 'A');
                        if (puzzle.puz[2, 2] == '.' && move != null && puzzle.puz[4, 2] == 'A' && puzzle.puz[3, 2] == 'A')
                        {
                            posmoves.Add(move);
                            break;
                        }
                        move = RoadFree(i, j, 1, 2, puzzle, 'A');
                        if (puzzle.puz[1, 2] == '.' && move != null && puzzle.puz[4, 2] == 'A' && puzzle.puz[3, 2] == 'A' && puzzle.puz[2, 2] == 'A')
                        {
                            posmoves.Add(move);
                            break;
                        }
                        break;
                    case 'B':
                        move = RoadFree(i, j, 4, 4, puzzle, 'B');
                        if (puzzle.puz[4, 4] == '.' && move != null)
                        {
                            posmoves.Add(move);
                            break;
                        }
                        move = RoadFree(i, j, 3, 4, puzzle, 'B');
                        if (puzzle.puz[3, 4] == '.' && move != null && puzzle.puz[4, 4] == 'B')
                        {
                            posmoves.Add(move);
                            break;
                        }
                        move = RoadFree(i, j, 2, 4, puzzle, 'B');
                        if (puzzle.puz[2, 4] == '.' && move != null && puzzle.puz[4, 4] == 'B' && puzzle.puz[3, 4] == 'B')
                        {
                            posmoves.Add(move);
                            break;
                        }
                        move = RoadFree(i, j, 1, 4, puzzle, 'B');
                        if (puzzle.puz[1, 4] == '.' && move != null && puzzle.puz[4, 4] == 'B' && puzzle.puz[3, 4] == 'B' && puzzle.puz[2, 4] == 'B')
                        {
                            posmoves.Add(move);
                            break;
                        }
                        break;
                    case 'C':
                        move = RoadFree(i, j, 4, 6, puzzle, 'C');
                        if (puzzle.puz[4, 6] == '.' && move != null)
                        {
                            posmoves.Add(move);
                            break;
                        }
                        move = RoadFree(i, j, 3, 6, puzzle, 'C');
                        if (puzzle.puz[3, 6] == '.' && move != null && puzzle.puz[4, 6] == 'C')
                        {
                            posmoves.Add(move);
                            break;
                        }
                        move = RoadFree(i, j, 2, 6, puzzle, 'C');
                        if (puzzle.puz[2, 6] == '.' && move != null && puzzle.puz[4, 6] == 'C' && puzzle.puz[3, 6] == 'C')
                        {
                            posmoves.Add(move);
                            break;
                        }
                        move = RoadFree(i, j, 1, 6, puzzle, 'C');
                        if (puzzle.puz[1, 6] == '.' && move != null && puzzle.puz[4, 6] == 'C' && puzzle.puz[3, 6] == 'C' && puzzle.puz[2, 6] == 'C')
                        {
                            posmoves.Add(move);
                            break;
                        }
                        break;
                    case 'D':
                        move = RoadFree(i, j, 4, 8, puzzle, 'D');
                        if (puzzle.puz[4, 8] == '.' && move != null)
                        {
                            posmoves.Add(move);
                            break;
                        }
                        move = RoadFree(i, j, 3, 8, puzzle, 'D');
                        if (puzzle.puz[3, 8] == '.' && move != null && puzzle.puz[4, 8] == 'D')
                        {
                            posmoves.Add(move);
                            break;
                        }
                        move = RoadFree(i, j, 2, 8, puzzle, 'D');
                        if (puzzle.puz[2, 8] == '.' && move != null && puzzle.puz[4, 8] == 'D' && puzzle.puz[3, 8] == 'D')
                        {
                            posmoves.Add(move);
                            break;
                        }
                        move = RoadFree(i, j, 1, 8, puzzle, 'D');
                        if (puzzle.puz[1, 8] == '.' && move != null && puzzle.puz[4, 8] == 'D' && puzzle.puz[3, 8] == 'D' && puzzle.puz[2, 8] == 'D')
                        {
                            posmoves.Add(move);
                            break;
                        }
                        break;
                }
            else if (i == 1 || i == 2 || i == 3 || i == 4)
            {
                if (RightPosition(i, j, v, puzzle))
                    return posmoves;
                for (int k = 0; k < 11; k++)
                {
                    if (k != 2 && k != 4 && k != 6 && k != 8)
                    {

                        var move = RoadFree(i, j, 0, k, puzzle, v);
                        if (move != null)
                            posmoves.Add(move);
                    }
                }
            }
            return posmoves;
        }

        private static bool RightPosition(int i, int j, char v, Move puzzle)
        {
            switch (v)
            {
                case 'A':
                    if (i == 4 && j == 2)
                        return true;
                    if (i == 3 && j == 2 && puzzle.puz[4, 2] == 'A')
                        return true;
                    if (i == 2 && j == 2 && puzzle.puz[4, 2] == 'A' && puzzle.puz[3, 2] == 'A')
                        return true;
                    if (i == 1 && j == 2 && puzzle.puz[4, 2] == 'A' && puzzle.puz[3, 2] == 'A' && puzzle.puz[2, 2] == 'A')
                        return true;
                    return false;
                case 'B':
                    if (i == 4 && j == 4)
                        return true;
                    if (i == 3 && j == 4 && puzzle.puz[4, 4] == 'B')
                        return true;
                    if (i == 2 && j == 4 && puzzle.puz[3, 4] == 'B' && puzzle.puz[4, 4] == 'B')
                        return true;
                    if (i == 1 && j == 4 && puzzle.puz[3, 4] == 'B' && puzzle.puz[4, 4] == 'B' && puzzle.puz[2, 4] == 'B')
                        return true;
                    return false;
                case 'C':
                    if (i == 4 && j == 6)
                        return true;
                    if (i == 3 && j == 6 && puzzle.puz[4, 6] == 'C')
                        return true;
                    if (i == 2 && j == 6 && puzzle.puz[4, 6] == 'C' && puzzle.puz[3, 6] == 'C')
                        return true;
                    if (i == 1 && j == 6 && puzzle.puz[4, 6] == 'C' && puzzle.puz[3, 6] == 'C' && puzzle.puz[2, 6] == 'C')
                        return true;
                    return false;
                case 'D':
                    if (i == 4 && j == 8)
                        return true;
                    if (i == 3 && j == 8 && puzzle.puz[4, 8] == 'D')
                        return true;
                    if (i == 2 && j == 8 && puzzle.puz[4, 8] == 'D' && puzzle.puz[3, 8] == 'D')
                        return true;
                    if (i == 1 && j == 8 && puzzle.puz[4, 8] == 'D' && puzzle.puz[3, 8] == 'D' && puzzle.puz[2, 8] == 'D')
                        return true;
                    return false;
            }
            return false;
        }

        private static Move RoadFree(int isu, int jsu, int id, int jd, Move puzzle, char c)
        {
            double sum = 0;
            if (id == 0)
            {
                if (isu == 4)
                {
                    if (puzzle.puz[3, jsu] == '.')
                        sum += step(c);
                    else return null;

                    if (puzzle.puz[2, jsu] == '.')
                        sum += step(c);
                    else return null;

                    if (puzzle.puz[1, jsu] == '.')
                        sum += step(c);
                    else return null;

                    if (puzzle.puz[0, jsu] == '.')
                        sum += step(c);
                    else return null;

                    if (jd < jsu)
                    {
                        for (int k = jsu - 1; k >= jd; k--)
                        {
                            if (puzzle.puz[0, k] == '.')
                                sum += step(c);
                            else return null;
                        }
                    }
                    if (jd > jsu)
                    {
                        for (int k = jsu + 1; k <= jd; k++)
                        {
                            if (puzzle.puz[0, k] == '.')
                                sum += step(c);
                            else return null;
                        }
                    }
                }
                if (isu == 3)
                {
                    if (puzzle.puz[2, jsu] == '.')
                        sum += step(c);
                    else return null;

                    if (puzzle.puz[1, jsu] == '.')
                        sum += step(c);
                    else return null;

                    if (puzzle.puz[0, jsu] == '.')
                        sum += step(c);
                    else return null;

                    if (jd < jsu)
                    {
                        for (int k = jsu - 1; k >= jd; k--)
                        {
                            if (puzzle.puz[0, k] == '.')
                                sum += step(c);
                            else return null;
                        }
                    }
                    if (jd > jsu)
                    {
                        for (int k = jsu + 1; k <= jd; k++)
                        {
                            if (puzzle.puz[0, k] == '.')
                                sum += step(c);
                            else return null;
                        }
                    }
                }
                if (isu == 2)
                {
                    if (puzzle.puz[1, jsu] == '.')
                        sum += step(c);
                    else return null;

                    if (puzzle.puz[0, jsu] == '.')
                        sum += step(c);
                    else return null;

                    if (jd < jsu)
                    {
                        for (int k = jsu - 1; k >= jd; k--)
                        {
                            if (puzzle.puz[0, k] == '.')
                                sum += step(c);
                            else return null;
                        }
                    }
                    if (jd > jsu)
                    {
                        for (int k = jsu + 1; k <= jd; k++)
                        {
                            if (puzzle.puz[0, k] == '.')
                                sum += step(c);
                            else return null;
                        }
                    }
                }

                if (isu == 1)
                {
                    if (puzzle.puz[0, jsu] == '.')
                        sum += step(c);
                    else return null;

                    if (jd < jsu)
                    {
                        for (int k = jsu - 1; k >= jd; k--)
                        {
                            if (puzzle.puz[0, k] == '.')
                                sum += step(c);
                            else return null;
                        }
                    }
                    if (jd > jsu)
                    {
                        for (int k = jsu + 1; k <= jd; k++)
                        {
                            if (puzzle.puz[0, k] == '.')
                                sum += step(c);
                            else return null;
                        }
                    }
                }
            }

            if (id == 1)
            {
                if (jd < jsu)
                {
                    for (int k = jsu - 1; k >= jd; k--)
                    {
                        if (puzzle.puz[0, k] == '.')
                            sum += step(c);
                        else return null;
                    }
                }
                if (jd > jsu)
                {
                    for (int k = jsu + 1; k <= jd; k++)
                    {
                        if (puzzle.puz[0, k] == '.')
                            sum += step(c);
                        else return null;
                    }
                }

                if (puzzle.puz[1, jd] == '.')
                    sum += step(c);
                else return null;

            }

            if (id == 2)
            {
                if (jd < jsu)
                {
                    for (int k = jsu - 1; k >= jd; k--)
                    {
                        if (puzzle.puz[0, k] == '.')
                            sum += step(c);
                        else return null;
                    }
                }
                if (jd > jsu)
                {
                    for (int k = jsu + 1; k <= jd; k++)
                    {
                        if (puzzle.puz[0, k] == '.')
                            sum += step(c);
                        else return null;
                    }
                }

                if (puzzle.puz[1, jd] == '.')
                    sum += step(c);
                else return null;

                if (puzzle.puz[2, jd] == '.')
                    sum += step(c);
                else return null;
            }

            if (id == 3)
            {
                if (jd < jsu)
                {
                    for (int k = jsu - 1; k >= jd; k--)
                    {
                        if (puzzle.puz[0, k] == '.')
                            sum += step(c);
                        else return null;
                    }
                }
                if (jd > jsu)
                {
                    for (int k = jsu + 1; k <= jd; k++)
                    {
                        if (puzzle.puz[0, k] == '.')
                            sum += step(c);
                        else return null;
                    }
                }

                if (puzzle.puz[1, jd] == '.')
                    sum += step(c);
                else return null;

                if (puzzle.puz[2, jd] == '.')
                    sum += step(c);
                else return null;

                if (puzzle.puz[3, jd] == '.')
                    sum += step(c);
                else return null;
            }
            if (id == 4)
            {
                if (jd < jsu)
                {
                    for (int k = jsu - 1; k >= jd; k--)
                    {
                        if (puzzle.puz[0, k] == '.')
                            sum += step(c);
                        else return null;
                    }
                }
                if (jd > jsu)
                {
                    for (int k = jsu + 1; k <= jd; k++)
                    {
                        if (puzzle.puz[0, k] == '.')
                            sum += step(c);
                        else return null;
                    }
                }

                if (puzzle.puz[1, jd] == '.')
                    sum += step(c);
                else return null;

                if (puzzle.puz[2, jd] == '.')
                    sum += step(c);
                else return null;

                if (puzzle.puz[3, jd] == '.')
                    sum += step(c);
                else return null;

                if (puzzle.puz[4, jd] == '.')
                    sum += step(c);
                else return null;
            }

            var move = new Move(new char[5, 11]);
            for (int k = 0; k < 5; k++)
            {
                for (int t = 0; t < 11; t++)
                {
                    if (k == isu && t == jsu)
                        move.puz[k, t] = '.';
                    else if (k == id && t == jd)
                        move.puz[k, t] = c;
                    else
                        move.puz[k, t] = puzzle.puz[k, t];
                }
            }
            move.Price = puzzle.Price + sum;
            return move;

        }

        private static double step(char c)
        {
            switch (c)
            {
                case 'A': return 1;
                case 'B': return 10;
                case 'C': return 100;
                case 'D': return 1000;
            }
            return int.MaxValue;
        }

        private static bool IsFinal(char[,] puz)
        {
            for (int i = 0; i < 11; i++)
            {
                if (puz[0, i] != '.')
                    return false;
            }
            if (puz[1, 2] != 'A')
                return false;
            if (puz[2, 2] != 'A')
                return false;
            if (puz[3, 2] != 'A')
                return false;
            if (puz[4, 2] != 'A')
                return false;

            if (puz[1, 4] != 'B')
                return false;
            if (puz[2, 4] != 'B')
                return false;
            if (puz[3, 4] != 'B')
                return false;
            if (puz[4, 4] != 'B')
                return false;

            if (puz[1, 6] != 'C')
                return false;
            if (puz[2, 6] != 'C')
                return false;
            if (puz[3, 6] != 'C')
                return false;
            if (puz[4, 6] != 'C')
                return false;

            if (puz[1, 8] != 'D')
                return false;
            if (puz[2, 8] != 'D')
                return false;
            if (puz[3, 8] != 'D')
                return false;
            if (puz[4, 8] != 'D')
                return false;

            return true;
        }
    }
}