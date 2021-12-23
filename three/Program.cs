using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    public class Command
    {
        public bool On { get; set; }
        public int x1 { get; set; }
        public int x2 { get; set; }
        public int y1 { get; set; }
        public int y2 { get; set; }
        public int z1 { get; set; }
        public int z2 { get; set; }
    }

    public class Cuboid
    {
        public int x1 { get; set; }
        public int x2 { get; set; }
        public int y1 { get; set; }
        public int y2 { get; set; }
        public int z1 { get; set; }
        public int z2 { get; set; }
        public Cuboid(int x1, int x2, int y1, int y2, int z1, int z2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            this.z1 = z1;
            this.z2 = z2;
        }
    }

    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public Point(int x, int y, int z)
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
        DupaIntersectie
    }

    class Program
    {

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");

            List<Command> commands = new List<Command>();
            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

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
                command.x1 = Int32.Parse(xf[0]);
                command.x2 = Int32.Parse(xf[1]);

                x = xx[2].Replace(",z", "");
                xf = x.Split("..");
                command.y1 = Int32.Parse(xf[0]);
                command.y2 = Int32.Parse(xf[1]);

                x = xx[3];
                xf = x.Split("..");
                command.z1 = Int32.Parse(xf[0]);
                command.z2 = Int32.Parse(xf[1]);

                commands.Add(command);
            }

            ShiftAllRight(commands, 200000);

            List<Cuboid> cubs = new List<Cuboid>();

            foreach (var com in commands)
            {
                var cubc = new Cuboid(com.x1, com.x2, com.y1, com.y2, com.z1, com.z2);
                if (!cubs.Any())
                    cubs.Add(cubc);
                else
                {
                    List<Cuboid> cubsint = new List<Cuboid>();
                    foreach (var cub in cubs)
                    {
                        List<Point> pointsnv = Intersectpoint(cubc, cub);
                        List<Point> pointsvn = Intersectpoint(cub, cubc);
                        Cuboid inters = Intersection(cubc, cub);
                        if (inters != null)
                        {
                            var willbreak = pointsnv.Count <= pointsvn.Count ? spargem.Vechi : spargem.Nou;
                            if (pointsvn.Count == 0 && pointsnv.Count == 0)
                                willbreak = spargem.DupaIntersectie;
                            if (willbreak == spargem.Vechi)
                            {
                                if (pointsnv.Count == 1)
                                {
                                    List<Cuboid> cubsmici = Sparge(cub, pointsnv[0]);
                                    foreach (var cubmic in cubsmici)
                                    {
                                        if (!Intersects(cubmic, cubc).Any())
                                            cubsint.Add(cubmic);
                                    }
                                    if (com.On)
                                        cubsint.Add(cub);
                                }
                                else if (pointsnv.Count > 1)
                                {
                                    //spargem de mai multe ori pe cubul vechi
                                    List<Point> despart = new List<Point>();
                                    despart = Undesparg(inters, pointsnv);
                                    List<Cuboid> cubsmici = Sparge(cub, despart[0]);
                                    foreach (var cubmic in cubsmici)
                                    {
                                        if (!Intersects(cubmic, cubc).Any())
                                            cubsint.Add(cubmic);
                                        else
                                        {
                                            List<Cuboid> cubs2 = Sparge(cubmic, despart[1]);
                                            foreach (var cubs2item in cubs2)
                                            {
                                                if (!Intersects(cubs2item, cubc).Any())
                                                    cubsint.Add(cubs2item);
                                            }
                                        }
                                    }
                                    if (com.On)
                                        cubsint.Add(cub);
                                }


                            }
                            else if (pointsvn.Count > 1 && pointsvn.Count < 8)
                            {
                                //spargem de mai multe ori pe cubul nou
                                List<Point> despart = new List<Point>();
                                despart = Undesparg(inters, pointsvn);
                                List<Cuboid> cubsmici = Sparge(cubc, despart[0]);
                                foreach (var cubmic in cubsmici)
                                {

                                    if (!Intersects(cubmic, cub).Any())
                                    {
                                        if (com.On)
                                            cubsint.Add(cubmic);
                                    }
                                    else
                                    {
                                        if (com.On)
                                        {
                                            List<Cuboid> cubs2 = Sparge(cubmic, despart[1]);
                                            foreach (var cubs2item in cubs2)
                                            {
                                                if (!Intersects(cubs2item, cub).Any())
                                                {
                                                    cubsint.Add(cubs2item);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            List<Cuboid> cubs2 = Sparge(cub, despart[1]);
                                            foreach (var cubs2item in cubs2)
                                            {
                                                if (!Intersects(cubs2item, cub).Any())
                                                {
                                                    cubsint.Add(cubs2item);
                                                }
                                            }
                                        }

                                    }
                                }

                            }
                            else if (pointsvn.Count == 8)
                            {
                                if (!com.On)
                                    cubs.Remove(cub);
                                else
                                {
                                    List<Point> despart = new List<Point>();
                                    despart = Undesparg(inters, pointsvn);
                                    List<Cuboid> cubsmici = Sparge(cubc, despart[0]);
                                    foreach (var cubmic in cubsmici)
                                    {

                                        if (!Intersects(cubmic, cub).Any())
                                        {
                                            cubsint.Add(cubmic);
                                        }
                                        else
                                        {
                                            List<Cuboid> cubs2 = Sparge(cubmic, despart[1]);
                                            foreach (var cubs2item in cubs2)
                                            {
                                                if (!Intersects(cubs2item, cub).Any())
                                                {
                                                    cubsint.Add(cubs2item);
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            else if (pointsnv.Count == 0 && pointsvn.Count == 0)
                            {
                                List<Point> despart = new List<Point>();
                                despart = Undesparg(inters);

                                List<Cuboid> cubsmici = Sparge(cub, despart[0]);
                                foreach (var cubmic in cubsmici)
                                {
                                    if (!Intersects(cubmic, cubc).Any())
                                    {
                                        cubsint.Add(cubmic);
                                    }
                                    else
                                    {
                                        List<Cuboid> cubs2 = Sparge(cubmic, despart[1]);
                                        foreach (var cubs2item in cubs2)
                                        {
                                            if (!Intersects(cubs2item, cubc).Any())
                                            {
                                                cubsint.Add(cubs2item);
                                            }
                                        }
                                    }
                                }
                                if (com.On)
                                    cubsint.Add(cubc);

                            }
                        }



                        }
                    cubs.AddRange(cubsint);
                    }
                }



            
            double sum = 0;



            Console.WriteLine("response: " + sum);

        }

        private static List<Cuboid> Sparge(Cuboid cub, Point point)
        {
            var lista = new List<Cuboid>();
            if (point.x > cub.x1)
                if (point.y > cub.y1)
                    if (point.z > cub.z1)
                    {
                        var c = new Cuboid(cub.x1, point.x, cub.y1, point.y, cub.z1, point.z);
                        lista.Add(c);
                    }

        }

        private static Cuboid Intersection(Cuboid cubc, Cuboid cub)
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

        private static List<Point> Intersectpoint(Cuboid cubc, Cuboid cub)
        {
            var lista = new List<Point>();
            if(checkp(cubc.x1,cubc.y1,cubc.z1, cub))
                lista.Add(new Point(cubc.x1, cubc.y1, cubc.z1));
            if (checkp(cubc.x1, cubc.y1, cubc.z2, cub))
                lista.Add(new Point(cubc.x1, cubc.y1, cubc.z2));
            if (checkp(cubc.x1, cubc.y2, cubc.z1, cub))
                lista.Add(new Point(cubc.x1, cubc.y2, cubc.z1));
            if (checkp(cubc.x1, cubc.y2, cubc.z2, cub))
                lista.Add(new Point(cubc.x1, cubc.y2, cubc.z2));
            if (checkp(cubc.x2, cubc.y1, cubc.z1, cub))
                lista.Add(new Point(cubc.x2, cubc.y1, cubc.z1));
            if (checkp(cubc.x2, cubc.y1, cubc.z2, cub))
                lista.Add(new Point(cubc.x2, cubc.y1, cubc.z2));
            if (checkp(cubc.x2, cubc.y2, cubc.z1, cub))
                lista.Add(new Point(cubc.x2, cubc.y2, cubc.z1));
            if (checkp(cubc.x2, cubc.y2, cubc.z2, cub))
                lista.Add(new Point(cubc.x2, cubc.y2, cubc.z2));
            return lista;
        }

        private static bool checkp(int x1, int y1, int z1, Cuboid cub)
        {
            return (x1 > cub.x1 && x1 < cub.x2 &&
                y1 > cub.y1 && y1 < cub.y2 &&
                z1 > cub.z1 && z1 < cub.z2);
        }

        private static List<Point> Intersects(Cuboid cub, Cuboid cubc)
        {
            throw new NotImplementedException();
        }

        private static void ShiftAllRight(List<Command> commands, int nr)
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