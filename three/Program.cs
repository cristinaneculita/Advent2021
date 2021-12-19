using System;
using System.Collections.Generic;
using System.Linq;

namespace advent
{
    class Program
    {
       
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");


            //var coord = lines[0].Split(",").ToList().Select(e => Int32.Parse(e)).ToList();

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of input.txt = ");
            double res = 0;
            var sum = lines[0];
            if (lines.Length == 1)
                sum = Addition(sum, lines[0], false) ;
            double max = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    if (i != j)
                    {

                        sum = Addition(lines[i], lines[j], true);
                        res = Magnitude(sum);
                        if (res > max)
                            max = res;
                    }
                }
                       
            }

            double x = Magnitude(sum);

            Console.WriteLine("response: " + max);

        }

        private static double Magnitude(string sum)
        {
            while (sum.Where(e=>e==']').Count()>=1)
            {
                sum = ReplacePair(sum);
            }

            sum = sum.Replace("[", "");
            sum = sum.Replace("]", "");
            return double.Parse(sum);

        }

        private static string ReplacePair(string sum)
        {
            for (int i = 1; i < sum.Length-1; i++)
            {
                if (sum[i] == ',' && sum[i - 1] != '[' && sum[i + 1] != ']' && sum[i - 1] != ']' && sum[i + 1] != '[')
                {
                    var indexl = i - 1;
                    while (sum[indexl] != '[')
                        indexl--;
                    indexl++;

                    var numl = new char[i - indexl];
                    for (int j = 0; j < numl.Length; j++)
                    {
                        numl[j] = sum[j + indexl];
                    }
                    var stringnl = new string(numl);
                    double numld = Double.Parse(stringnl);

                    var indexr = i + 1;
                    while (sum[indexr] != ']')
                        indexr++;
                    indexr--;

                    var numr = new char[indexr-i];
                    for (int j = 0; j < numr.Length; j++)
                    {
                        numr[j] = sum[j+i+1];
                    }
                    var stringnr = new string(numr);
                    double numrd = Double.Parse(stringnr);

                    double mag = numld * 3 + numrd * 2;

                    //return ReplaceMag(sum, indexl, indexr,mag);
                    sum = sum.Remove(indexl-1, indexr - indexl+3);
                    sum = sum.Insert(indexl-1, mag.ToString());
                    return sum;
                }
            }
            return "";
        }

        private static string ReplaceMag(string sum, int indexl, int indexr, double mag)
        {
            var carv = sum.ToCharArray();
            var oldl = indexr - indexl + 1;
            var newl = mag.ToString().Length;


            var newcarv = new char[carv.Length - oldl + 1];
            for (int i = 0; i < newcarv.Length; i++)
            {
                if (i < indexl)
                    newcarv[i] = carv[i];
                else if (i > indexr)
                    newcarv[i] = carv[i - oldl];
                else newcarv[i] = '0';            
            }
            var str = new string(newcarv);
            return str.Insert(indexl, mag.ToString());
        }

        private static string Addition(string sum, string v, bool reallyadd)
        {
            string sumtemp = "";
            if (reallyadd)
                sumtemp = "[" + sum + "," + v + "]";
            else
                sumtemp = sum;

            bool modif = true;
            while (modif)
            {
                modif = false;
                var x = Explode(sumtemp);
                if (x.Item1)
                {
                    modif = true;
                    sumtemp = x.Item2;
                    continue;
                }
                var y = Split(sumtemp);
                if (y.Item1)
                {
                    modif = true;
                    sumtemp = y.Item2;
                    continue;
                }
            }

            return sumtemp;
        }

        private static Tuple<bool, string> Split(string sumtemp)
        {
            var carv = sumtemp.ToCharArray();
            var index = 0;
            foreach (char c in sumtemp)
            {
                if (isNumber(c) && isNumber(carv[index + 1]))
                {
                    var num = new char[2];
                    num[0] = c;
                    num[1] = carv[index + 1];
                    var nums = new string(num);
                    var numn = Int32.Parse(nums);
                    int left = numn / 2;
                    int right = (numn / 2) + (numn % 2);
                    var strrep = "[" + left.ToString() + "," + right.ToString() + "]";
                    sumtemp = sumtemp.Remove(index, 2);
                    sumtemp = sumtemp.Insert(index, strrep);
                    return new Tuple<bool, string>(true, sumtemp);
                }

                index++;
            }
            return new Tuple<bool, string>(false, "");

        }

        private static string ReplaceS(int v, string strrep, char[] carv)
        {
            var newsstr = new char[carv.Length + 3];
            var cardeintr = strrep.ToCharArray();
            for (int i = 0; i < strrep.Length; i++)
            {
                if (i < v)
                    newsstr[i] = carv[i];

                else if (i > v + 3)
                    newsstr[i] = carv[i - 3];
                else
                    newsstr[i] = cardeintr[i - v];
            }
            return new string(newsstr);          
        }

        private static Tuple<bool,string> Explode(string sumtemp)
        {
            int leftp = 0;
            bool shouldexpl = false;
            int index = 0;
            var carv = new char[sumtemp.Length];
             carv = sumtemp.ToCharArray();
            var toaddlafinal = 3;
            foreach (char c in sumtemp)
            {
                if (shouldexpl && isNumber(c))
                {
                    var indexc = index + 1;
                    while (isNumber(carv[indexc]))
                    {
                        indexc++;
                    }
                    var toadd = new char[indexc - index];
                    for (int i = 0; i < toadd.Length; i++)
                    {
                        toadd[i] = carv[i + index];
                    }
                    //toadd = carv[indexc] - '0';
                    var toaddstr = new string(toadd);
                    toaddlafinal += toadd.Length;
                    int n1 = Int32.Parse(toaddstr);


                    var indexp = index + 1 + toadd.Length;
                    indexc = indexp + 1;
                    if (!isNumber(carv[indexp]))
                    {
                        if (c == '[')
                            leftp++;
                        if (c == ']')
                            leftp--;
                        if (leftp >= 5)
                            shouldexpl = true;

                        index++;
                        continue;
                    }
                    while (isNumber(carv[indexc]))
                    {
                        indexc++;
                    }
                    toadd = new char[indexc - indexp];
                    for (int i = 0; i < toadd.Length; i++)
                    {
                        toadd[i] = carv[i + indexp];
                    }
                    //toadd = carv[indexc] - '0';
                    toaddlafinal += toadd.Length;
                    toaddstr = new string(toadd);
                    int n2= Int32.Parse(toaddstr);



                    
                    indexc = index - 1;
                    var descos = 0;
                    var depus = 0;
                    int newnumbern1 = 0;
                    while (!isNumber(carv[indexc]) && indexc > 0)
                    {
                        indexc--;
                    }
                    if (indexc > 0)
                    {
                        
                        var indexci = indexc-1;
                        while(isNumber(carv[indexci]) && indexci > 0)
                        {
                            indexci--;
                        }
                        toadd = new char[indexc - indexci];
                        for (int i = 0; i < toadd.Length; i++)
                        {
                            toadd[i] = carv[i + indexci+1];
                        }
                        //toadd = carv[indexc] - '0';
                        toaddstr = new string(toadd);
                        descos = toadd.Length;
                        newnumbern1 = n1 + Int32.Parse(toaddstr);
                        depus = newnumbern1.ToString().Length;
                        sumtemp = sumtemp.Remove(indexc-(toadd.Length)+1, toadd.Length);
                        sumtemp = sumtemp.Insert(indexc-(toadd.Length) + 1, newnumbern1.ToString());
                        
                    }

                    index = index +(depus-descos);
                    indexc = index + 2+n2.ToString().Length+n1.ToString().Length-1;
                    
                    carv = sumtemp.ToCharArray();
                    while (!isNumber(carv[indexc]) && indexc < sumtemp.Length - 1)
                    {
                        indexc++;
                    }
                    if (indexc < sumtemp.Length-1)
                    {
                        var indexci = indexc + 1;
                        while (isNumber(carv[indexci]) && indexc < sumtemp.Length - 1)
                        {
                            indexci++;
                        }
                        toadd = new char[indexci - indexc];
                        for (int i = 0; i < toadd.Length; i++)
                        {
                            toadd[i] = carv[i + indexc];
                        }
                        //toadd = carv[indexc] - '0';
                        toaddstr = new string(toadd);
                                           
                        int newnumbern2 = n2 + Int32.Parse(toaddstr);
                        sumtemp = sumtemp.Remove(indexc, toadd.Length);
                        sumtemp = sumtemp.Insert(indexc, newnumbern2.ToString());
                    }

                    // sumtemp = Replace(index - 1, carv,0);
                    sumtemp = sumtemp.Remove(index - 1, +toaddlafinal);
                    sumtemp = sumtemp.Insert(index-1, "0");
                    return new Tuple<bool,string>(true,sumtemp);
                }

                if (c == '[')
                    leftp++;
                if (c == ']')
                    leftp--;
                if (leftp >= 5)
                    shouldexpl = true;
               
                index++;
            }
            return new Tuple<bool, string>(false,"");
        }

        private static string Replace(int v, char[] carv,int n)
        {
            var newsstr = new char[carv.Length-4];
            for (int i = 0; i < carv.Length; i++)
            {
                if (i == v)
                    newsstr[i] = '0';
                if (i < v)
                    newsstr[i] = carv[i];
                if (i > v)
                    newsstr[i] = carv[i - 4];
            }
            return new string(newsstr);
        }

       
        private static bool isNumber(char v)
        {
            return v >= '0' && v <= '9';
        }

        //private static bool NestedIn4(string sumtemp)
        //{
        //    int leftp = 0;
        //    foreach (char c in sumtemp)
        //    {
        //        if (c == '[')
        //            leftp++;
        //        if (c == ']')
        //            leftp--;
        //        if (leftp == 5)
        //            return true;
        //    }
        //    return false;
        //}
    }
}