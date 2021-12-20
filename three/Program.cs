using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace advent
{
    public class Point : IEquatable<Point>
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public int xtr { get; set; }
        public int ytr { get; set; }
        public int ztr { get; set; }

        public Point(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            xtr = Int32.MinValue;
            ytr = Int32.MinValue;
            ztr = Int32.MinValue;
        }
        public Point(int x, int y, int z, int a, int b, int c)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            xtr = a;
            ytr = b;
            ztr = c;
        }

        public bool Equals([AllowNull] Point other)
        {
            return other != null &&
                   x == other.x &&
                   y == other.y &&
                   z == other.z;
        }
    }

    public class Scanner
    {
        public List<Point> Beacons { get; set; }
        public Scanner()
        {
            Beacons = new List<Point>();
        }
        public Point coordrelto0 { get; set; }
    }


    public class Dif: IEquatable<Dif>
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public int point1index { get; set; }
        public int point2index { get; set; }
        public Dif(int x, int y, int z, int p1, int p2)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            point1index = p1;
            point2index = p2;
        }
        public bool Equals([AllowNull] Dif other)
        {
            return other != null &&
                   x == other.x &&
                   y == other.y &&
                   z == other.z;
        }

        

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y,z);
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
            var res = 0;
            var scanners = new List<Scanner>();
            var index = -1;
            foreach (var line in lines)
            {
                if (line.Contains("--- scanner "))
                {
                    index++;
                    var scanner = new Scanner();
                    scanners.Add(scanner);
                }
                else if (!string.IsNullOrEmpty(line))
                {
                    var p = line.Split(",").Select(e=>Int32.Parse(e)).ToArray();
                    scanners[index].Beacons.Add(new Point(p[0], p[1], p[2]));
                }
            }



            var currentsindex = 0;

            scanners[0].coordrelto0 = new Point(0, 0, 0);
            var knownrels = new List<int>();
            knownrels.Add(0);
            Scanner currentS;

            while (knownrels.Any())
            {
                do
                {
                    currentsindex = knownrels[0];
                    knownrels.RemoveAt(0);
                    currentS = scanners[currentsindex];
                } while (currentS.coordrelto0 == null);

                for (int i = 0; i < scanners.Count; i++)
                {
                    if (i != currentsindex && scanners[i].coordrelto0 == null)
                    {
                        Point overl = Overlapping(currentS, scanners[i]);
                        if (overl != null)
                        {
                            if (currentsindex == 0)
                                scanners[i].coordrelto0 = overl;
                            else
                                scanners[i].coordrelto0 = CalculateRelTo0(currentS, overl);
                            knownrels.Add(i);
                        }
                    }

                }
            }

            var resultB = new List<Point>();
            foreach (var item in scanners[0].Beacons)
            {
                resultB.Add(item);
            }

            foreach (var scanner in scanners)
            {
                if (scanner != scanners[0])
                {
                    foreach (var beacon in scanner.Beacons)
                    {
                        Point xx = Transformed(beacon, scanner.coordrelto0);
                        if (!resultB.Contains(xx))
                        {
                            resultB.Add(xx);
                        }
                    }
                }
            }
            var maxd = 0;
            foreach (var scan1 in scanners)
            {
                foreach (var scan2 in scanners)
                {
                    int d = distance(scan1, scan2);
                    if (d > maxd)
                        maxd = d;
                }
            }

            Console.WriteLine("response: " + maxd);

        }

        private static int distance(Scanner scan1, Scanner scan2)
        {
            return Math.Abs(scan1.coordrelto0.x - scan2.coordrelto0.x) +
                 Math.Abs(scan1.coordrelto0.y - scan2.coordrelto0.y) +
                 Math.Abs(scan1.coordrelto0.z - scan2.coordrelto0.z);
        }

        private static Point Transformed(Point beacon, Point coordrelto0)
        {
            return new Point(beacon.xtr + coordrelto0.x, beacon.ytr + coordrelto0.y, beacon.ztr + coordrelto0.z);
        }

        private static Point CalculateRelTo0(Scanner currentS, Point overl)
        {
            var p = currentS.coordrelto0;

            return new Point(p.x + overl.x, p.y +overl.y, p.z + overl.z);
        }

        private static Point Overlapping(Scanner currentS, Scanner scanner2)
        {
            var listdif = new List<Dif>();
            for (int i = 0; i < currentS.Beacons.Count; i++)
            {
                for (int j = i + 1; j < currentS.Beacons.Count; j++)
                {

                    listdif.Add(new Dif(currentS.Beacons[i].x - currentS.Beacons[j].x,
                       currentS.Beacons[i].y - currentS.Beacons[j].y,
                       currentS.Beacons[i].z - currentS.Beacons[j].z, i, j));

                }
            }
            var listdif2 = new List<Dif>();

            for (int perm = 1; perm <= 24; perm++)
            {
                CalculatePerm(scanner2, perm);
                for (int i = 0; i < scanner2.Beacons.Count; i++)
                {
                    for (int j = i + 1; j < scanner2.Beacons.Count; j++)
                    {

                        listdif2.Add(new Dif(scanner2.Beacons[i].xtr - scanner2.Beacons[j].xtr,
                           scanner2.Beacons[i].ytr - scanner2.Beacons[j].ytr,
                           scanner2.Beacons[i].ztr - scanner2.Beacons[j].ztr, i, j));

                    }
                }
                List<Dif> inters = new List<Dif>();
                inters =   Intersection(listdif, listdif2);
                if (inters.Count>=24)
                {
                    Dif dif = inters[0];
                    Dif difs2 = inters[1];
                    var p1 = currentS.Beacons[dif.point1index];
                    //var p2 = currentS.Beacons[dif.point2index];

                    var p3 = scanner2.Beacons[difs2.point1index];
                   // var p4 = scanner2.Beacons[difs2.point2index];

                    var difx = p1.x - p3.xtr;
                    var dify = p1.y - p3.ytr;
                    var difz = p1.z - p3.ztr;
                    foreach (var beam in scanner2.Beacons)
                    {
                        beam.x = beam.xtr;
                        beam.y = beam.ytr;
                        beam.z = beam.ztr;
                    }
                    return new Point(difx, dify, difz);
                    
                }

            }
            return null; 
        }

        private static List<Dif> Intersection(List<Dif> listdif, List<Dif> listdif2)
        {
            var difint = new List<Dif>();
            foreach (var dif in listdif)
            {
                if (listdif2.Contains(dif))
                {
                    difint.Add(dif);
                    difint.Add(listdif2.SingleOrDefault(l => l.Equals(dif)));
                }
            }
            return difint;
        }

        private static void CalculatePerm(Scanner scanner2, int perm)
        {
            foreach (var beam in scanner2.Beacons)
            {
                Trans(beam, perm);
            }
        }

        private static void Trans(Point beam, int perm)
        {
            switch (perm)
            {
                case 1:
                    beam.xtr = beam.x;
                    beam.ytr = beam.y;
                    beam.ztr = beam.z;
                    break;
                case 2:
                    beam.xtr = beam.x;
                    beam.ytr = beam.z;
                    beam.ztr = -beam.y;
                    break;
                case 3:
                    beam.xtr = beam.x;
                    beam.ytr = -beam.y;
                    beam.ztr = -beam.z;
                    break;
                case 4:
                    beam.xtr = beam.x;
                    beam.ytr = -beam.z;
                    beam.ztr = beam.y;
                    break;
                case 5:
                    beam.xtr = -beam.x;
                    beam.ytr = -beam.y;
                    beam.ztr = beam.z;
                    break;
                case 6:
                    beam.xtr = -beam.x;
                    beam.ytr = beam.z;
                    beam.ztr = beam.y;
                    break;
                case 7:
                    beam.xtr = -beam.x;
                    beam.ytr = beam.y;
                    beam.ztr = -beam.z;
                    break;
                case 8:
                    beam.xtr = -beam.x;
                    beam.ytr = -beam.z;
                    beam.ztr = -beam.y;
                    break;

                case 9:
                    beam.xtr = -beam.y;
                    beam.ytr = beam.x;
                    beam.ztr = beam.z;
                    break;
                case 10:
                    beam.xtr = -beam.z;
                    beam.ytr = beam.x;
                    beam.ztr = -beam.y;
                    break;
                case 11:
                    beam.xtr = beam.y;
                    beam.ytr = beam.x;
                    beam.ztr = -beam.z;
                    break;
                case 12:
                    beam.xtr = beam.z;
                    beam.ytr = beam.x;
                    beam.ztr = beam.y;
                    break;
                case 13:
                    beam.xtr = -beam.z;
                    beam.ytr = -beam.x;
                    beam.ztr = beam.y;
                    break;
                case 14:
                    beam.xtr = -beam.y;
                    beam.ytr = -beam.x;
                    beam.ztr = -beam.z;
                    break;
                case 15:
                    beam.xtr = beam.z;
                    beam.ytr = -beam.x;
                    beam.ztr = -beam.y;
                    break;
                case 16:
                    beam.xtr = beam.y;
                    beam.ytr = -beam.x;
                    beam.ztr = beam.z;
                    break;


                case 17:
                    beam.xtr = beam.y;
                    beam.ytr = -beam.z;
                    beam.ztr = -beam.x;
                    break;
                case 18:
                    beam.xtr = -beam.z;
                    beam.ytr = -beam.y;
                    beam.ztr = -beam.x;
                    break;
                case 19:
                    beam.xtr = -beam.y;
                    beam.ytr = beam.z;
                    beam.ztr = -beam.x;
                    break;
                case 20:
                    beam.xtr = beam.z;
                    beam.ytr = beam.y;
                    beam.ztr = -beam.x;
                    break;
                case 21:
                    beam.xtr = -beam.z;
                    beam.ytr = beam.y;
                    beam.ztr = beam.x;
                    break;
                case 22:
                    beam.xtr = beam.y;
                    beam.ytr = beam.z;
                    beam.ztr = beam.x;
                    break;
                case 23:
                    beam.xtr = beam.z;
                    beam.ytr = -beam.y;
                    beam.ztr = beam.x;
                    break;
                case 24:
                    beam.xtr = -beam.y;
                    beam.ytr = -beam.z;
                    beam.ztr = beam.x;
                    break;
                default:
                    break;
            }
        }
    }
}