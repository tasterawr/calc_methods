﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_app
{
    static class EulerMethodImproved
    {
        static List<double> xs = new List<double>();
        static List<double> ys_actual = new List<double>();
        static List<double> ys_counted = new List<double>();
        static List<double> hf_betw = new List<double>();
        static List<double> hf = new List<double>();
        static List<double> diff = new List<double>();

        public static void Execute()
        {
            Console.Write("Введите вариант V: ");
            int v = int.Parse(Console.ReadLine());
            Console.Write("Введите шаг h: ");
            double h = double.Parse(Console.ReadLine());

            double x = 1;
            int ind = 0;

            xs.Add(x);
            ys_actual.Add(v);
            ys_counted.Add(v);
            hf_betw.Add(ys_counted[ind] + h / 2 * (2 * v * x + v*x*x - ys_counted[ind]));
            hf.Add(h * (2 * v * (x + h/2) + v * (x + h / 2)* (x + h / 2) - hf_betw[ind]));
            diff.Add(0);

            while (x <= 2)
            {
                x += h;
                xs.Add(x);
                ys_actual.Add(v * x * x);
                ys_counted.Add(ys_counted[ind] + hf[ind]);
                hf_betw.Add(ys_counted[ind+1] + h/2 * (2 * v * x + v * x * x - ys_counted[ind + 1]));
                hf.Add(h * (2 * v * (x + h / 2) + v * (x + h / 2) * (x + h / 2) - hf_betw[ind+1]));
                ind++;
                diff.Add(Math.Abs(ys_actual[ind] - ys_counted[ind]));
            }

            Print();
        }

        public static void Print()
        {
            Console.WriteLine("[x]\t[Y подсч.]\t[Y точн.]\t[Погрешность]");
            for (int i = 0; i < xs.Count; i++)
            {
                Console.WriteLine(xs[i] + "\t" + ys_counted[i].ToString("#.###") + "\t\t" + ys_actual[i].ToString("#.###") + "\t\t" + diff[i]);
            }
        }
    }
}
