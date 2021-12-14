using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    public class Rule {
        public string Input { get; set; }
        public string Output { get; set; }
        public long Occ { get; set; }
        public long NewOcc { get; set; }
        public Rule(string i, string o)
        {
            Input = i;
            Output = o;
        }
    }

    class Program
    {
       
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            var startstring = "";

            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var res = 0;
            startstring = lines[0];
            var rules = new List<Rule>();
            foreach (var line in lines)
            {
                if (line.Contains("->"))
                {
                    var x = line.Split(" -> ");
                    rules.Add(new Rule(x[0], x[1]));
                }
            }

            //var start = startstring.ToCharArray();

            //for (int step = 0; step < 40; step++)
            //{
            //    List<char> outstart = start.Select(a=>a).ToList();
            //    var j = 0;
            //    for (int i = 0; i < start.Length - 1; i++)
            //    {
            //        var x = start[i].ToString();
            //        char[] y = { start[i], start[i + 1] };
            //        var z = new string(y);
            //        var r = rules.FirstOrDefault(e => e.Input == z);
            //        if (r != null)
            //        {
            //            outstart.Insert(i + 1+j, r.Output[0]);
            //            j++;
            //        }
            //    }
            //    start = outstart.Select(a=>a).ToArray();
            //}

            var start = startstring.ToCharArray();
            for (int i = 0; i < start.Length-1; i++)
            {
                var x = start[i].ToString();
                char[] y = { start[i], start[i + 1] };
                var z = new string(y);
                var r = rules.FirstOrDefault(e => e.Input == z);
                if (r != null)
                {
                    r.Occ++;
                }
            }


            for (int step = 0; step < 40; step++)
            {
                for (int i = 0; i < rules.Count; i++)
                {
                    if (rules[i].Occ != 0)
                    {
                        char[] x1 = { rules[i].Input[0], rules[i].Output.ToCharArray()[0] };
                        var rule1 = rules.FirstOrDefault(e => e.Input[0]==x1[0] && e.Input[1]==x1[1]);
                        if (rule1 != null)
                        {
                            rule1.NewOcc += rules[i].Occ;
                        }

                        char[] x2 = {rules[i].Output.ToCharArray()[0], rules[i].Input[1], };
                        var rule2 = rules.FirstOrDefault(e => e.Input[0] == x2[0] && e.Input[1] == x2[1]);
                        if (rule2 != null)
                        {
                            rule2.NewOcc += rules[i].Occ;
                        }
                    }
                }

                foreach (var item in rules)
                {
                    item.Occ = item.NewOcc;
                    item.NewOcc = 0;
                }
            }





            char[] diff = { 'K', 'H', 'S', 'N', 'F', 'V', 'P', 'C', 'B', 'O' };
            //char[] diff = { 'N', 'B', 'C', 'H' };
            var dif = new List<char>();
            dif = diff.ToList();


            var reslit = new List<long>();
            for (int i = 0; i < dif.Count; i++)
            {
                reslit.Add(0);
            }
            
            for (int i = 0; i < dif.Count(); i++)
            {
                foreach (var rule in rules)
                {
                    if (rule.Input.Contains(dif[i]))
                    {
                        reslit[i] += rule.Occ;
                        if (rule.Input[0] == rule.Input[1])
                            reslit[i] += rule.Occ;
                    }
                }
            }

            for (int i = 0; i < dif.Count; i++)
            {
                if (startstring[0] == dif[i] || startstring[startstring.Length - 1] == dif[i])
                    reslit[i] = (reslit[i] - 1) / 2 + 1;
                else
                    reslit[i] = reslit[i] / 2;
            }
          

            long min = reslit.Min();
            long max = reslit.Max();
            long result = max - min;
            Console.WriteLine("response: " + result);

        }
    }
}