using System;
using System.Threading.Tasks;

namespace Epam.Multithreading.Task3
{
    static class MatrixExt
    {
        public static int RowsCount(this int[,] matrix)
        {
            return matrix.GetUpperBound(0) + 1;
        }

        public static int ColumnsCount(this int[,] matrix)
        {
            return matrix.GetUpperBound(1) + 1;
        }

        public static int[,] MatrixMultiplication(int[,] matrixA, int[,] matrixB)
        {
            if (matrixA.ColumnsCount() != matrixB.RowsCount())
            {
                throw new Exception("Multiplication is not possible! The number of columns in the first matrix is ​​not equal to the number of rows in the second matrix.");
            }

            var matrixC = new int[matrixA.RowsCount(), matrixB.ColumnsCount()];

            Parallel.For(0, matrixA.RowsCount(), i =>
                Parallel.For(0, matrixB.ColumnsCount(), j =>
                {
                    matrixC[i, j] = 0;
                    Parallel.For(0, matrixA.ColumnsCount(), k =>
                    {
                        matrixC[i, j] += matrixA[i, k] * matrixB[k, j];
                    });
                }));

            return matrixC;
        }
    }

}
