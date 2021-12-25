using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{



    class Program
    {
        public class Operation
        {
            public string Type { get; set; }
            public string Term1 { get; set; }
            public string Term2 { get; set; }
            public Operation(string type, string term1, string term2)
            {
                Type = type;
                Term1 = term1;
                Term2 = term2;
            }

            public bool Apply()
            {
                switch (Type)
                {
                    case "inp":
                        w = numberchar[numberindex] - '0';
                        numberindex++;
                        break;
                    case "add":
                        Sum(Term1, Term2);
                        break;
                    case "mul":
                        Mul(Term1, Term2);
                        break;
                    case "div":
                        return Div(Term1, Term2);
                    case "mod":
                        return Mod(Term1, Term2);
                    case "eql":
                        Eql(Term1, Term2);
                        break;
                }

                return true;
            }

            private void Eql(string term1, string term2)
            {
                double t1, t2;
                t1 = GetTerm1(term1);
                t2 = GetTerm2(term2);
                t1 = (t1 == t2) ? 1 : 0;
                switch (term1)
                {
                    case "x": x = t1; break;
                    case "y": y = t1; break;
                    case "z": z = t1; break;
                    case "w": w = t1; break;
                }
            }

            private bool Mod(string term1, string term2)
            {
                double t1, t2;
                t1 = GetTerm1(term1);
                if (t1 < 0)
                    return false;
                t2 = GetTerm2(term2);
                if (t2 <= 0)
                    return false;
                t1 = t1 % t2;
                switch (term1)
                {
                    case "x": x = t1; break;
                    case "y": y = t1; break;
                    case "z": z = t1; break;
                    case "w": w = t1; break;
                }
                return true;
            }

            private bool Div(string term1, string term2)
            {
                double t1, t2;
                t1 = GetTerm1(term1);

                t2 = GetTerm2(term2);
                if (t2 == 0)
                    return false;
                t1 = Math.Truncate(t1 / t2);
                switch (term1)
                {
                    case "x": x = t1; break;
                    case "y": y = t1; break;
                    case "z": z = t1; break;
                    case "w": w = t1; break;
                }
                return true;
            }

            private void Sum(string term1, string term2)
            {
                double t1, t2;
                t1 = GetTerm1(term1);
                t2 = GetTerm2(term2);
                t1 = t1 + t2;
                switch (term1)
                {
                    case "x": x = t1; break;
                    case "y": y = t1; break;
                    case "z": z = t1; break;
                    case "w": w = t1; break;
                }
            }

            private void Mul(string term1, string term2)
            {
                double t1, t2;
                t1 = GetTerm1(term1);
                t2 = GetTerm2(term2);
                t1 = t1 * t2;
                switch (term1)
                {
                    case "x": x = t1; break;
                    case "y": y = t1; break;
                    case "z": z = t1; break;
                    case "w": w = t1; break;
                }
            }

            private double GetTerm2(string term2)
            {
                switch (term2)
                {
                    case "x": return x;
                    case "y": return y;
                    case "z": return z;
                    case "w": return w;
                    default:
                        return double.Parse(term2);
                }
            }

            private double GetTerm1(string term1)
            {
                switch (term1)
                {
                    case "x": return x;
                    case "y": return y;
                    case "z": return z;
                    case "w": return w;
                    default:
                        break;
                }
                return 0;
            }
        }

        public static double x;
        public static double y;
        public static double z;
        public static double w;
        public static char[] numberchar = new char[14];
        public static int numberindex;
        public static int[] terminations;
        public static int[] hundreds;
        public static double[] hund;
        public static int hundindex;
        public static bool oneatatime;
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            List<Operation> operations = new List<Operation>();

            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var res = 0;
            oneatatime = false;
            foreach (var line in lines)
            {
                var x = line.Split(' ');
                var thirdp = x.Count() == 2 ? "" : x[2];

                var op = new Operation(x[0], x[1], thirdp);
                operations.Add(op);
            }
            //double number = 69914999975370;
            double number = 99999999979512;
            
            terminations = new int[7] { 79, 68,57,46,35,24,13};
            hundreds = new int[7] { 1, 2, 3, 4, 5, 6, 7 };
            hund = new double[4] {11000, 1100, 110, 11 };
            hundindex = 1;
            bool resulttotal = false;
            do
            {
                z = 15376;
                number = nextNumber(number, z);
                numberchar = number.ToString().ToCharArray();
                numberindex = 0;
                if (numberchar.Contains('0'))
                    continue;
                x = 0;
                y = 0;
                z = 0;
                w = 0;
                
                resulttotal = false;
                foreach (var oper in operations)
                {
                    var result = oper.Apply();
                    resulttotal |= result;
                    if (!result)
                        break;
                }
                if (z == 0)
                {
                    resulttotal = true;
                }
                else { resulttotal = false;
                    if (z <=15376)
                    {
                        Console.Write(" " + number);
                        Console.WriteLine(" z:" + z);
                    }
                }

                number = nextNumber(number, z);

              


            } while (!resulttotal);



            Console.WriteLine("response: " + number);

        }

        private static double nextNumber(double number, double z)
        {
            if (z == 15376)
            {
               
                return number - hund[hundindex];
            }
            else if (!oneatatime)
            {
                var anterior = number + hund[hundindex];
                hundindex++;
                oneatatime = true;
                return anterior+1;
            }
            else {
                return number + 1;

            }

            //int s = (int)(numberchar[12]);
            //int z = (int)number % 100;
            //if (hundreds.Contains(s))
            //{
            //    switch (s)
            //    {
            //        case 7: if (z > 91)
            //                return number - 1;
            //            else if (z == 91)
            //                return number-12;
            //            else if (z != 13)
            //                return number - 11;
            //            else return number - 24;
            //        case 6:
            //            if (z > 81)
            //                return number - 1;
            //            else if (z == 81)
            //                return number - 2;
            //            else if (z != 13)
            //                return number - 11;
            //            else return number - 14;
            //        case 5:
            //            if (z > 11)
            //                return number - 1;

            //            else return number - 32;
            //        case 4:
            //            if (z == 79)
            //                return number - 10;
            //            else if (z > 61)
            //                return number - 1;
            //            else if (z == 61)
            //                return number - 4;
            //            else if (z > 13)
            //                return number - 11;
            //            else 
            //                return number - 34;
            //        case 3:
            //            if (z == 79)
            //                return number - 11;
            //            else if (z == 68)
            //                return number - 9;
            //            else if (z >51)
            //                return number - 1;
            //            else if (z ==51)
            //                return number - 11;
            //            else
            //                return number - 34;


            //    }
            return number - 1;
            }

        

        
    }
}