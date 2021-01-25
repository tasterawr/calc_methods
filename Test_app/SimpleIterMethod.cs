using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_app
{
    static class SimpleIterMethod
    {
        private static double e = 0.00001;
        public static void AlphaAndBetaMatrices(double[,] matA, double[] vecB, int n)
        {
            double[,] alpha = new double[n, n];
            double[] beta = new double[n];

            for (int i = 0; i < n; i++) //получение матрицы alpha и вектора beta
            {
                beta[i] = vecB[i] / matA[i, i];
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                        alpha[i, j] = -1 * matA[i, j] / matA[i, i];
                    else
                        alpha[i, j] = 0;
                }
            }

            double max = double.MinValue; //проверка на диаг. преобладание
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (alpha[i, j] > max)
                        max = alpha[i, j];
                }
            }
            if (max >= 1)
            {
                Console.WriteLine("Нарушение условия диагонального преобладания.");
                return;
            }

            GetVectorX(alpha, beta, beta);
        }

        public static double[] MultMatrixVec(double[,] matr, double[] vec) //умножение матрицы на вектор
        {
            double[] result = new double[vec.Length];

            for (int i=0; i<matr.GetLength(0); i++)
            {
                double sum = 0;
                for (int j = 0; j<matr.GetLength(1); j++)
                {
                    sum += matr[i, j] * vec[i];
                }
                result[i] = sum;
            }

            return result;
        }

        public static double[] VectorsSum(double[] vec1, double[] vec2) //сложение векторов
        {
            double[] result = new double[vec1.Length];

            for (int i =0; i< vec1.Length; i++)
            {
                result[i] = vec1[i] + vec2[i];
            }

            return result;
        }

        public static double[] VectorsDiff(double[] vec1, double[] vec2) //разность векторов
        {
            double[] result = new double[vec1.Length];

            for (int i = 0; i < vec1.Length; i++)
            {
                result[i] = vec1[i] - vec2[i];
            }

            return result;
        }

        public static double GetVectorNorm(double[] vec) //получить норму вектора
        {
            double result = 0;
            for (int i = 0; i < vec.Length; i++)
                result += vec[i] * vec[i];
            result = Math.Sqrt(result);
            return result;
        }

        public static void GetVectorX(double[,] alpha, double[]beta, double[] k)
        {
            double[] k_ = MultMatrixVec(alpha, k); //вычисляем вектор Xk+1 = Beta + Alpha*Xk
            k_ = VectorsSum(beta, k_);
            double[] diff = VectorsDiff(k_, k);
            if (GetVectorNorm(diff) < e)
            {
                Console.WriteLine("[Vector X]: ");
                for (int i = 0; i < k.Length; i++)
                    Console.WriteLine(k[i]);
            }
            else
                GetVectorX(alpha, beta, k_);
        }
    }
}
