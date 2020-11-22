using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_app
{
    class Program
    {
        static double[,] SwapRows(double [,] mas, int i)
        {
            double max = mas[i, i];
            int j = i;
            for (int k = i + 1; k < mas.GetLength(0); k++)
            {
                if (Math.Abs(mas[k,i]) > Math.Abs(max))
                {
                    max = Math.Abs(mas[k, i]);
                    j = k;
                }
            }

            for (int k = 0; k < mas.GetLength(1); k++)
            {
                double temp = mas[i, k];
                mas[i, k] = mas[j, k];
                mas[j, k] = temp;
            }

            Console.WriteLine("[Matrix after rows swap]: ");
            for (int m = 0; m < mas.GetLength(0); m++)
            {
                for (int k = 0; k < mas.GetLength(1); k++)
                {
                    Console.Write(Math.Round(mas[m, k], 5) + " ");
                }
                Console.WriteLine();
            }
            return mas;
        }

        static double[] Gauss(double [,] matA, double[] vecB, int n)
        {
            double[] x = new double[n];
            //Console.WriteLine("\n[Extended matrix]");
            double[,] mas = new double[n, n+1];

            for (int i=0; i < mas.GetLength(0); i++)
            {
                for (int j=0; j < mas.GetLength(1); j++)
                {
                    if (j+1 == mas.GetLength(1))
                        mas[i, j] = vecB[i];
                    else
                        mas[i, j] = matA[i, j];
                }
            }

            //ВЫВОД РАСШИРЕННОЙ МАТРИЦЫ
            //for (int i = 0; i < mas.GetLength(0); i++)
            //{
            //    for (int j = 0; j < mas.GetLength(1); j++)
            //    {
            //        Console.Write(Math.Round(mas[i, j], 5) + " ");
            //    }
            //    Console.WriteLine();
            //}

            for (int i=0; i < mas.GetLength(0); i++)
            {
                double div = mas[i, i];
                if (div == 0)
                {
                    mas = SwapRows(mas, i);
                    div = mas[i, i];
                }
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    mas[i, j] = mas[i, j] / div;
                }

                for (int k = i+1; k< mas.GetLength(0); k++)
                {
                    double tmp = mas[k, i];
                    for (int j = i; j < mas.GetLength(1); j++)
                    {
                        mas[k, j] -= mas[i, j] * tmp;
                    }
                }

                //ВЫВОД МАТРИЦ С ПРОМЕЖУТОЧНЫМИ ИЗМЕНЕНИЯМИ
                //Console.WriteLine();
                //for (int m = 0; m < mas.GetLength(0); m++)
                //{
                //    for (int j = 0; j < mas.GetLength(1); j++)
                //    {
                //        Console.Write(Math.Round(mas[m, j], 5) + " ");
                //    }
                //    Console.WriteLine();
                //}
            }

            Console.WriteLine("\n[Changed matrix]");
            PrintMatrix(mas);

            x[n-1] = mas[n-1, n];
            for (int i = n - 2; i >= 0; i--)
            {
                double b = mas[i, n];
                for (int j = i + 1; j < n; j++)
                {
                    b -= mas[i, j] * x[j];
                }
                x[i] = b;
            }

            Console.WriteLine("\n[Vector X]");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(x[i]);
            }
            return x;
        }

        static void Determinant(double[,] mas, int n)
        {
            double det = 1;
            double cnt = 0.0;
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                double div = mas[i, i];
                if (div == 0)
                {
                    mas = SwapRows(mas, i);
                    div = mas[i, i];
                }

                det *= div;

                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    mas[i, j] = mas[i, j] / div;
                }
                for (int k = i + 1; k < mas.GetLength(0); k++)
                {
                    double tmp = mas[k, i];
                    for (int j = i; j < mas.GetLength(1); j++)
                    {
                        mas[k, j] -= mas[i, j] * tmp;
                    }
                }
            }
            det *= Math.Pow(-1.0, cnt);
            Console.WriteLine("Определитель: " + det);
        }

        static double [,] SetMatrixA(double[,]mas, int v, int n)
        {
            Console.WriteLine("[Matrix A]");
            int add = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        mas[i, j] = v + add;
                    else
                        mas[i, j] = (v + add) * 0.001;
                }
                add += 2;
            }
            PrintMatrix(mas);
            return mas;
        }

        static double [,] TridiagMatrixA(double [,] mas, int v, int n)
        {
            Console.WriteLine("[Three-diagonal matrix A]");
            int add = 0;
            for (int i = 0; i < n; i++)
            {
                mas[i, i] = v + add;
                if (i == 0)
                {
                    mas[i, i + 1] = (v + add) * 0.001;
                }
                else if (i == n - 1)
                {
                    mas[i, i - 1] = (v + add) * 0.001;
                }
                else
                {
                    mas[i, i - 1] = (v + add) * 0.001;
                    mas[i, i + 1] = (v + add) * 0.001;
                }
                add += 2;
            }
            PrintMatrix(mas);
            return mas;
        }


        static void TridiagAlg(double [,] masA, double[] vecb, int n)
        {
            double[] masP = new double[n];
            double[] masQ = new double[n];
            double[,] mas = new double[n, n + 1];
            List<double> x_vec = new List<double>();
            for (int i =0; i < n; i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (j == n)
                        mas[i, j] = vecb[i];
                    else
                        mas[i, j] = masA[i, j];
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    masP[i] = mas[i, i + 1] / mas[i, i];
                    masQ[i] = mas[i, mas.GetLength(1) - 1] / mas[i, i];
                }
                else if (i == n - 1)
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

            for (int i=1; i < n; i++)
            {
                if (i == 1)
                    x_vec.Add(masQ[masQ.Length - 1]);
                else
                {
                    double x = masP[masP.Length -1 - i] * x_vec[i - 2] + masQ[masQ.Length - 1 - i];
                    x_vec.Add(x);
                }
            }

            x_vec.Reverse();
            Console.WriteLine("[Vector X]: ");
            foreach(double x in x_vec)
            {
                Console.WriteLine(x);
            }
        }


        static double[,] Inverse(double[,] A, int n)
        {
            double[,] inv = new double[n, n];
            double[] b = new double[n];
            double[] res;
            for (int i = 0; i < n; i++)
            {
                b[i] = 1;
                res = Gauss(A, b, n);
                for (int j = 0; j < n; j++)
                    inv[j, i] = res[j];
                b[i] = 0;
            }
            return inv;
        }

        static double[] SetVectorB(double[,] mas, int v, int n)
        {
            Console.WriteLine("\n[Vector B]");
            int add = 2 * (n - 1);
            double[] vecB = new double[n];
            double[] vec = new double[n];
            for (int i = 0; i < n; i++)
            {
                vec[i] = v + add;
                add -=2;
            }

            for (int i = 0; i<n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double add_ = mas[i, j] * vec[j];
                    vecB[i] = vecB[i] + add_;
                }
            }

            for (int i=0; i<n; i++)
            {
                Console.WriteLine(vecB[i]);
            }
            return vecB;
        }

        static public void PrintMatrix(double[,] mat)
        {
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    Console.Write(Math.Round(mat[i, j], 5) + "\t");
                }
                Console.WriteLine();
            }
        }

        static public double[,] MultMat(double[,] a, double[,] b, int n)
        {
            double[,] mult = new double[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    for (var k = 0; k < n; k++)
                    {
                        mult[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            PrintMatrix(mult);
            return mult;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Enter V: ");
            int v = int.Parse(Console.ReadLine());
            int n = 6;
            double[,] matA = new double[n,n];
            matA = SetMatrixA(matA, v, n);
            //matA = TridiagMatrixA(matA, v, n); //метод прогонки
            double[] vecB = SetVectorB(matA, v, n);
            SimpleIterMethod.AlphaAndBetaMatrices(matA, vecB, n);
            TridiagAlg(matA, vecB, n);  //метод прогонки
            Determinant(matA, n);

            double[,] matA_ = Inverse(matA, n);
            Console.WriteLine("\nОбратная матрица:");
            PrintMatrix(matA_);
            MultMat(matA, matA_, n);
            //Gauss(matA, vecB, n);
            Console.ReadKey();
        }
    }
}
