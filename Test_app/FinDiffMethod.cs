using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_app
{
    static class FinDiffMethod
    {
        static List<double> xs = new List<double>();
        static List<double> fs = new List<double>();
        static List<double> ys_actual = new List<double>();
        static List<double> ys_counted = new List<double>();
        static List<double> diff = new List<double>();

        public static void Execute()
        {
            int n = 11;
            double [,] mas = new double[n,n+1];
            double[] masP = new double[n];
            double[] masQ = new double[n];
            Console.Write("Введите вариант V: ");
            int v = int.Parse(Console.ReadLine());

            double h = (double)v / 10;

            for (double i = 0; i <= v + 0.2; i += h)
            {
                xs.Add(i);
                fs.Add(4 * v * Math.Pow(i, 4) - 3 * v * v * Math.Pow(i, 3) + 6 * v * i - 2 * v * v);
                ys_actual.Add(v * i * i * (i - v));
            }
            ys_actual[10] = 0;

            //заполнение матрицы коэффициентов и одновременное расширение n+1 столбца
            for (int i = 0; i < n; i++)
            {
                mas[i, n] = fs[i];

                if (i == 0)
                {
                    mas[i, i] = -1 * (2 / (h * h) + (xs[i] * xs[i]) / (2 * h) + xs[i]);
                    mas[i, i + 1] = 1 / (h * h) + (xs[i] * xs[i]) / (2 * h);
                }
                else if (i == 10)
                {
                    mas[i, i] = -1 * (2 / (h * h) + (xs[i] * xs[i]) / (2 * h) + xs[i]);
                    mas[i, i - 1] = 1/(h*h) - (xs[i] * xs[i]) / (2 * h);
                }
                else
                {
                    mas[i, i - 1] = 1 / (h * h) - (xs[i] * xs[i]) / (2 * h);
                    mas[i, i] = -1 * (2 / (h * h) + (xs[i] * xs[i]) / (2 * h) + xs[i]);
                    mas[i, i + 1] = 1 / (h * h) + (xs[i] * xs[i]) / (2 * h);
                }
            }
            PrintMatrix(mas);

            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    masP[i] = mas[i, i + 1] / mas[i, i];
                    masQ[i] = mas[i, mas.GetLength(1) - 1] / mas[i, i];
                }
                else if (i == n-1)
                {
                    masP[i] = 0;
                    masQ[i] = (mas[i, i - 1] * masQ[i - 1]) / (mas[i, i] - mas[i, i - 1] * masP[i - 1]);
                }
                else
                {
                    masP[i] = mas[i, i + 1] / (mas[i, i] - mas[i, i - 1] * masP[i - 1]);
                    masQ[i] = (mas[i, i - 1] * masQ[i - 1]) / (mas[i, i] - mas[i, i - 1] * masP[i - 1]);
                }
            }

            for (int i = 1; i < n; i++)
            {
                if (i == 1)
                    ys_counted.Add(masQ[masQ.Length - 1]);
                else
                {
                    double y = masP[masP.Length - 1 - i] * ys_counted[i - 2] + masQ[masQ.Length - 1 - i];
                    ys_counted.Add(y);
                }
            }

            ys_counted.Reverse();
            ys_counted.Add(0);

            for(int i = 0; i < n; i++)
            {
                diff.Add(Math.Abs(ys_actual[i] - ys_counted[i]));
            }

            Print();
        }

        static public void PrintMatrix(double[,] mat)
        {
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    Console.Write(Math.Round(mat[i, j], 2) + "\t");
                }
                Console.WriteLine();
            }
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
