using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    public class Command
    {
        public bool On { get; set; }
        public double x1 { get; set; }
        public double x2 { get; set; }
        public double y1 { get; set; }
        public double y2 { get; set; }
        public double z1 { get; set; }
        public double z2 { get; set; }
    }

    public class Cuboid
    {
        public double x1 { get; set; }
        public double x2 { get; set; }
        public double y1 { get; set; }
        public double y2 { get; set; }
        public double z1 { get; set; }
        public double z2 { get; set; }
        public bool Lit { get; set; }
        public Cuboid(double x1, double x2, double y1, double y2, double z1, double z2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            this.z1 = z1;
            this.z2 = z2;
        }
    }

    public class Podouble
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public Podouble(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    public enum spargem
    {
        Nou,
        Vechi,
        Dupadoubleersectie
    }

    class Program
    {

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");

            List<Command> commands = new List<Command>();
            //var coord = lines[0].Split(",").ToList().Select(e => double.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            var res = 0;
            foreach (var line in lines)
            {
                var command = new Command();
                if (line.StartsWith("on"))
                    command.On = true;
                else
                    command.On = false;
                var xx = line.Split("=");
                var x = xx[1].Replace(",y", "");
                var xf = x.Split("..");
                command.x1 = double.Parse(xf[0]);
                command.x2 = double.Parse(xf[1]);

                x = xx[2].Replace(",z", "");
                xf = x.Split("..");
                command.y1 = double.Parse(xf[0]);
                command.y2 = double.Parse(xf[1]);

                x = xx[3];
                xf = x.Split("..");
                command.z1 = double.Parse(xf[0]);
                command.z2 = double.Parse(xf[1]);

                commands.Add(command);
            }

            ShiftAllRight(commands, 200000);

            List<Cuboid> cubs = new List<Cuboid>();

            foreach (var com in commands)
            {
                var cubc = new Cuboid(com.x1, com.x2, com.y1, com.y2, com.z1, com.z2);
              
                List<Cuboid> listac = new List<Cuboid>();
                
                foreach (Cuboid cuburivechi in cubs)
                {
                    var doubleers = doubleersection(cubc, cuburivechi);
                    if (doubleers != null)
                    {
                        if (cuburivechi.Lit)
                            doubleers.Lit = false;
                        else
                            doubleers.Lit = true;
                        listac.Add(doubleers);
                    }
                      
                }
                if (com.On)
                {
                    cubc.Lit = true;
                    listac.Add(cubc);
                }
                cubs.AddRange(listac);
            }
        
            




        double sum = 0;

            foreach (var cub in cubs)
            {
                double volcurent = (cub.x2 - cub.x1 + 1) * (cub.y2 - cub.y1 + 1) * (cub.z2 - cub.z1 + 1);
                if (cub.Lit)
                    sum += volcurent;
                else
                    sum -= volcurent;
            }

        Console.WriteLine("response: " + sum);

        }


      

        private static Cuboid doubleersection(Cuboid cubc, Cuboid cub)
        {
            var dx = Math.Min(cub.x2, cubc.x2) - Math.Max(cub.x1, cubc.x1);
            var dy = Math.Min(cub.y2, cubc.y2) - Math.Max(cub.y1, cubc.y1);
            var dz = Math.Min(cub.z2, cubc.z2) - Math.Max(cub.z1, cubc.z1);
            if (dx < 0 || dy < 0 || dz < 0)
                return null;
            return new Cuboid(Math.Max(cub.x1, cubc.x1), Math.Min(cub.x2, cubc.x2),
                Math.Max(cub.y1, cubc.y1), Math.Min(cub.y2, cubc.y2),
                Math.Max(cub.z1, cubc.z1), Math.Min(cub.z2, cubc.z2));
            
        }

       

        private static void ShiftAllRight(List<Command> commands, double nr)
        {
            foreach (var com in commands)
            {
                com.x1 += nr;
                com.x2 += nr;
                com.y1 += nr;
                com.y2 += nr;
                com.z1 += nr;
                com.z2 += nr;
            }
        }
    }
}