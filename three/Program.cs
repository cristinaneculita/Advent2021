using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent
{

    class Program
    {



        private static double sum;
        private static double result;
        private static List<string> operations;
        private static List<string> operationslist;

        static void Main(string[] args)
        {
            sum = 0;
            string[] lines = System.IO.File.ReadAllLines("input.txt");


            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var res = 0;
            operations = new List<string>();
            operationslist = new List<string>();
            var binary = HexStringToBinary(lines[0].ToString());
            Process(binary);
            operationslist.Add("+");
            operationslist.Add("*");
            operationslist.Add("min");
            operationslist.Add("max");
            operationslist.Add(">");
            operationslist.Add("<");
            operationslist.Add("==");

            var rezut = Calc(operations);

            Console.WriteLine("response: " + rezut);
        
        }

        private static string Calc(List<string> operations)
        {
            while (operations.Count() > 1)
            {
                var indstop = operations.FindIndex(e => e == "stop");
                var indop = indstop - 1;
                while (!operationslist.Contains(operations[indop]))
                {
                    indop--;
                }
                var result = doOperation(operations, indop, indstop);
                operations.RemoveRange(indop, indstop - indop + 1);
                operations.Insert(indop, result.ToString());
            }
            return operations[0];
        }

        private static double doOperation(List<string> operations, int indop, int indstop)
        {
            
            var listmic = new List<double>();

            for (int i = indop+1; i < indstop; i++)
            {
                listmic.Add(double.Parse(operations[i]));
            }
            if (listmic.Count() > 2 && (operations[indop] == ">" || operations[indop] == "<" || operations[indop] == "=="))
                Console.WriteLine("buba");
            switch (operations[indop])
            {
                case "+": return suma(listmic);
                case "*": return produs(listmic);
                case "min": return listmic.Min();
                case "max": return listmic.Max();
                case ">":  return listmic[0] > listmic[1] ? (double)1 :(double) 0;
                case "<": return listmic[0] < listmic[1] ? (double)1 : (double)0;
                case "==": return listmic[0] == listmic[1] ? (double)1 : (double)0;
            }
            return 0;
        }

        private static double produs(List<double> listmic)
        {
            double s = 1;
            foreach (var item in listmic)
            {
                s *= item;
            }
            return s;
        }

        private static double suma(List<double> listmic)
        {
            double s = 0;
            foreach (double item in listmic)
            {
                s += item;
            }
            return s;
        }

        private static double Process(string binary, int lenght = 0)
        {
            double processed = 0;
            var v = binary.Take(3).ToList();
            sum += (double)number(v);
            processed += 3;
            var left = binary.Substring(3);
            var t = left.Take(3).ToList();
            var nt = number(t);

            switch (nt) {
                case 0: operations.Add("+"); break;
                case 1: operations.Add("*"); break;
                case 2: operations.Add("min"); break;
                case 3: operations.Add("max"); break;
                case 5: operations.Add(">"); break;
                case 6: operations.Add("<"); break;
                case 7: operations.Add("=="); break;
            }


            left = left.Substring(3);
            processed += 3;
          
            if (nt == 4)//number
            {
                var nrrez = new List<char>();
                var nr = new List<char>();
                do
                {
                    nr = left.Take(5).ToList();
                    left = left.Substring(5);
                    processed += 5;
                    nrrez.Add(nr[1]);
                    nrrez.Add(nr[2]);
                    nrrez.Add(nr[3]);
                    nrrez.Add(nr[4]);
                      
                } while (nr[0] == '1');
                operations.Add(number(nrrez).ToString());
            }
            else //operator
            {
                var ii = left.Take(1).ToList();
                left = left.Substring(1);
                processed++;
                if (ii[0] == '0')//15 next nr bits
                {
                    var bits = number(left.Take(15).ToList());
                    left = left.Substring(15);
                    processed += 15;
                    double p = 0;
                    p += Process(left);
                    processed += p;
                    left = left.Substring((int)p);
                    while (p < bits)
                    {
                        if (left.Contains('1'))
                        {
                            double pc= Process(left);
                            p += pc;
                            processed += pc;
                            left = left.Substring((int)pc);
                        }
                    }
                }
                else//11 bits
                {
                    var packets = number(left.Take(11).ToList());
                    left = left.Substring(11);
                    processed += 11;
                    for (double i = 0; i < packets; i++)
                    {
                        double pc = Process(left);
                        processed += pc;
                        left = left.Substring((int)pc);
                    }
                }
                operations.Add("stop");
            }
            return (double)processed;
        }

        private static double number(List<char> t)
        {
            var ordi = t.Count();
            double s = 0;
            for (int i = 0; i < ordi; i++)
            {
                s += (double)Math.Pow(2, ordi - i - 1) * (t[i] - '0');
            }
            return (double)s;
        }

        public static string HexStringToBinary(string hex)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in hex)
            {
                Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
    { '0', "0000" },
    { '1', "0001" },
    { '2', "0010" },
    { '3', "0011" },
    { '4', "0100" },
    { '5', "0101" },
    { '6', "0110" },
    { '7', "0111" },
    { '8', "1000" },
    { '9', "1001" },
    { 'a', "1010" },
    { 'b', "1011" },
    { 'c', "1100" },
    { 'd', "1101" },
    { 'e', "1110" },
    { 'f', "1111" }
};
                // This will crash for non-hex characters. You might want to handle that differently.
                result.Append(hexCharacterToBinary[char.ToLower(c)]);
            }
            return result.ToString();
        }
    }
}