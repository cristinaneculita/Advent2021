using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    public class Parant
    {
        public char car { get; set; }
        public bool inchis { get; set; }
        public Parant(char c, bool inchis)
        {
            car = c;
            this.inchis = inchis;
        }
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
            var paro = new int[4];
          

            int sum = 0;
            List<List<char>> inchideri = new List<List<char>>();
            foreach (string line in lines)
            {
                List<Parant> par = new List<Parant>();
                // Use a tab to indent each line of the file.
                // Console.WriteLine("\t" + line);
                bool found = false;
                foreach (char item in line)
                {
                    if (item == '(')
                    { paro[0]++; par.Add( new Parant(item,false)); }
                    if (item == '[')
                    { paro[1]++; par.Add(new Parant(item, false)); }
                    if (item == '{')
                    { paro[2]++; par.Add(new Parant(item, false)); }
                    if (item == '<')
                    { paro[3]++; par.Add(new Parant(item, false)); }

                    if (item == ')')
                    {
                        if (!isComplete(par, item))
                        { sum += 3; found = true; break; }
                        //else par.Add(item);
                    }
                    if (item == ']')
                    {
                        if (!isComplete(par, item))
                        { sum += 57; found = true; break; }
                       // else par.Add(item);
                    }
                    if (item == '}')
                    {
                        if (!isComplete(par, item))
                        { sum += 1197; found = true; break; }
                       // else par.Add(item);
                    }
                    if (item == '>')
                    {
                        if (!isComplete(par, item))
                        { sum += 25137; found = true; break; }
                        //else par.Add(item);
                    }

                }
                if (!found)
                {
                    inchideri.Add(Complete(par));
                }
            }
            var sums = new List<long>();
            foreach (var inchidere in inchideri)
            {
                long sumc = 0;
                foreach (var item in inchidere)
                {
                    sumc = sumc * 5 + val(item);
                }
                sums.Add(sumc);
            }

            sums = sums.OrderBy(c => c).ToList();
            var sumf = sums[sums.Count / 2];
            Console.WriteLine("raspunsul este: "+sumf);
            //Console.WriteLine("Hello World!");
        }

        private static long val(char item)
        {
            switch (item) {
                case ')': return 1;
                case ']': return 2;
                case '}': return 3;
                case '>': return 4;

            }
            return 0;
        }

        private static List<char> Complete(List<Parant> par)
        {
            var inch = new List<char>();
            par.Reverse();
            foreach (var item in par)
            {
                if (!item.inchis)
                    inch.Add(oposed(item.car));
            }
            return inch;

        }

        private static bool isComplete(List<Parant> par, char c)
        {
            par.Add(new Parant(c,true));
            //if (par.Where(e => e == ')').Count() == par.Where(e => e == '(').Count() && par.Where(e => e == ']').Count() == par.Where(e => e == '[').Count()
            //&& par.Where(e => e == '{').Count() == par.Where(e => e == '}').Count() && par.Where(e => e == '<').Count() == par.Where(e => e == '>').Count())

            var x = par.FindLastIndex(e=>e.car==oposed(c) && !e.inchis );
          
            var newList = new List<char>();
            for (int i = 0; i < par.Count; i++)
            {
                if (i >= x)
                    newList.Add(par[i].car);
            }

            if (newList.Where(e => e == ')').Count() == newList.Where(e => e == '(').Count() && newList.Where(e => e == ']').Count() == newList.Where(e => e == '[').Count()
            && newList.Where(e => e == '{').Count() == newList.Where(e => e == '}').Count() && newList.Where(e => e == '<').Count() == newList.Where(e => e == '>').Count())
            {
                par[x].inchis = true;
                return true;
            }
            return false;
           
        }

        

        private static char oposed(char c)
        {
            if (c == ')') return '(';
            if (c == ']') return '[';
            if (c == '}') return '{';
            if (c == '>') return '<';
            if (c == '(') return ')';
            if (c == '[') return ']';
            if (c == '{') return '}';
            if (c == '<') return '>';
            return '0';
        }
    }
}
