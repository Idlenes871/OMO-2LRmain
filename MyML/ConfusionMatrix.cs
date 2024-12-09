using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace MyML
{
    public class ConfusionMatrix
    {
        public int Size { get; private set; }
        private Matrix<double> matrix { get; set; }

        public ConfusionMatrix(int size)
        {
            if (size < 2)
                throw new ArgumentException();

            matrix = Matrix<double>.Build.Dense(size, size);
            Size = size;
        }

        public void AddResult(int trueClass, int predictedClass)
        {
            if (trueClass < 0 || trueClass >= Size || predictedClass < 0 || predictedClass >= Size)
                throw new ArgumentException();

            matrix[trueClass, predictedClass]++;
        }

        public double TP(int classNumber) {
            if (classNumber < 0 || classNumber >= Size)
                throw new ArgumentException();

            return matrix[classNumber, classNumber];
        }

        public double TN(int classNumber)
        {
            if (classNumber < 0 || classNumber >= Size)
                throw new ArgumentException();

            double result = 0;
            var colSums = matrix.ColumnSums();
            var rowSums = matrix.RowSums();
            for (int i = 0; i < Size; i++)
            {
                if (i == classNumber)
                    continue;
                result += colSums[i];
            }
            result -= rowSums[classNumber];
            result += matrix[classNumber, classNumber];

            return result;
        }

        public double FP(int classNumber)
        {
            if (classNumber < 0 || classNumber >= Size)
                throw new ArgumentException();

            double result = 0;
            result = matrix.ColumnSums()[classNumber];
            result -= matrix[classNumber, classNumber];

            return result;
        }

        public double FN(int classNumber)
        {
            if (classNumber < 0 || classNumber >= Size)
                throw new ArgumentException();

            double result = 0;
            result = matrix.RowSums()[classNumber];
            result -= matrix[classNumber, classNumber];

            return result;
        }

        public double N()
        {
            return matrix.ColumnSums().Sum();
        }
    }
}
