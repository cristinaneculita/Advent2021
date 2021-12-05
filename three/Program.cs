using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    public class Element
    {
        public int Val { get; set; }
        public bool Marked { get; set; }
        public Element()
        {

        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {

            string[] lines = System.IO.File.ReadAllLines("input.txt");
            List<List<List<Element>>> boards = new List<List<List<Element>>>();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var response = 0;
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                // Console.WriteLine("\t" + line);
            }
            var scoase = lines[0].Split(",").Select(e => Int32.Parse(e)).ToArray();
            for (int i = 2; i < lines.Length;)
            {
                var b = new List<List<Element>>();
                for (int j = 0; j < 5; j++)
                {
                    var l = lines[i].Split(" ");
                    var ll = new List<Element>();
                    for (int k = 0; k < l.Length; k++)
                        if (l[k] != "")
                            ll.Add(new Element() { Val = Int32.Parse(l[k]), Marked = false });
                    b.Add(ll);
                    i++;
                }
                boards.Add(b);
                i++;
            }




           
            //Console.WriteLine("Hello World!");
            var lastNumber = -1;
            var nemarcateSum = 0;
            int markedforline = 0;
            int markedforcolumn = 0;
            List<int> boardswon = new List<int>();
            for (int i = 0; i < scoase.Length; i++)
            {
                //check and mark
                foreach (var board in boards)
                {
                    foreach (var line in board)
                    {
                        foreach (var item in line)
                        {
                            if (item.Val == scoase[i])
                                item.Marked = true;
                        }
                    }
                }

                //check lines 

                for (int b = 0; b < boards.Count; b++)
                {
                    if (!boardswon.Contains(b))
                    {
                        for (int l = 0; l < 5; l++)
                        {
                            markedforline = 0;
                            // int markedforcolumn = 0;
                            for (int c = 0; c < 5; c++)
                            {
                                if (boards[b][l][c].Marked == true)
                                    markedforline++;
                            }
                            if (markedforline == 5) break;
                        }

                        for (int c = 0; c < 5; c++)
                        {
                            markedforcolumn = 0;
                            for (int l = 0; l < 5; l++)
                            {
                                if (boards[b][l][c].Marked == true)
                                    markedforcolumn++;
                            }
                            if (markedforcolumn == 5) break;
                        }

                        if (markedforline == 5 || markedforcolumn == 5)
                        {
                            boardswon.Add(b);
                           
                            if (boardswon.Count == boards.Count || i == scoase.Length - 1)
                            {
                                lastNumber = scoase[i];
                                nemarcateSum = 0;
                                for (int l = 0; l < 5; l++)
                                {
                                    for (int c = 0; c < 5; c++)
                                    {
                                        if (boards[b][l][c].Marked == false)
                                            nemarcateSum += boards[b][l][c].Val;
                                    }
                                }
                            }                         
                        }
                    }
                   
                }
                if (lastNumber != -1)
                    break;
            }

            Console.WriteLine(nemarcateSum * lastNumber);
        }
    }

}
