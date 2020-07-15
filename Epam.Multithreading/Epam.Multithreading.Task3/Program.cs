using System;

namespace Epam.Multithreading.Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Программа для умножения матриц");

            var a = GetMatrixFromConsole("A");
            var b = GetMatrixFromConsole("B");

            Console.WriteLine("Матрица A:");
            PrintMatrix(a);

            Console.WriteLine("Матрица B:");
            PrintMatrix(b);

            var result = MatrixExt.MatrixMultiplication(a, b);
            Console.WriteLine("Произведение матриц:");
            PrintMatrix(result);

            Console.ReadLine();
        }
        static int[,] GetMatrixFromConsole(string name)
        {
            Console.Write("Количество строк матрицы {0}:    ", name);
            var n = int.Parse(Console.ReadLine());
            Console.Write("Количество столбцов матрицы {0}: ", name);
            var m = int.Parse(Console.ReadLine());

            var matrix = new int[n, m];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    Console.Write("{0}[{1},{2}] = ", name, i, j);
                    matrix[i, j] = int.Parse(Console.ReadLine());
                }
            }

            return matrix;
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (var i = 0; i < matrix.RowsCount(); i++)
            {
                for (var j = 0; j < matrix.ColumnsCount(); j++)
                {
                    Console.Write(matrix[i, j].ToString().PadLeft(4));
                }

                Console.WriteLine();
            }
        }
    }

}
