using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsHelper
{
    public class Matrix
    {
        double[,] matr;
        int rowCount;
        int colCount;

        public Matrix(int rows, int cols)
        {
            rowCount = rows;
            colCount = cols;
            matr = new double[rows, cols];
        }
        /// <summary>
        /// Заполнение матрицы слева направо, сверху вниз
        /// </summary>
        /// <param name="elems">Элементы для заполнения</param>
        /// <returns></returns>
        public Matrix fill(params double[] elems)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    matr[i, j] = elems[i * colCount + j];
                }
            }
            return this;
        }
        /// <summary>
        /// Заполняет матрицу для преобразований в 2D
        /// </summary>
        /// <param name="elems"></param>
        /// <returns></returns>
        public Matrix fillAffine(params double[] elems)
        {
            return fill(elems[0], elems[1], 0, elems[2], elems[3], 0, elems[4], elems[5], 1);
        }

        public double this[int x, int y]
        {
            get
            {
                return matr[x, y];
            }
            set
            {
                matr[x, y] = value;
            }
        }
        /// <summary>
        /// Умножение матрицы на число
        /// </summary>
        /// <param name="matr">Матрица</param>
        /// <param name="value">Вещественное число</param>
        /// <returns></returns>
        public static Matrix operator *(Matrix matr, double value)
        {
            var res = new Matrix(matr.rowCount, matr.colCount);
            for (int i = 0; i < matr.rowCount; i++)
            {
                for (int j = 0; j < matr.colCount; j++)
                {
                    res[i, j] = matr[i, j] * value;
                }
            }
            return res;
        }
        /// <summary>
        /// Умножение матриц
        /// </summary>
        /// <param name="matrix1">Матрица вещественных</param>
        /// <param name="matrix2">Матрица вещественных</param>
        /// <returns></returns>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.colCount != matrix2.rowCount)
            {
                throw new Exception("Так умножать нельзя...");
            }
            var res = new Matrix(matrix1.rowCount, matrix2.colCount);
            for (int i = 0; i < res.rowCount; i++)
            {
                for (int j = 0; j < res.colCount; j++)
                {
                    for (var k = 0; k < matrix1.colCount; k++)
                    {
                        res[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return res;
        }
    }
}
