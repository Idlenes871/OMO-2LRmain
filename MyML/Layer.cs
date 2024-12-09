using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MyML.Functions;

namespace MyML
{
    public class Layer
    {
        public int InputsNumber { get; private set; }
        public int OutputsNumber { get; private set; }
        public Matrix<double> WeightsMatrix { get; private set; }
        public bool IsInitialised { get; private set; } = false;

        public Matrix<double>? VectorX { get; private set; } = null;
        public Matrix<double>? VectorP {  get; private set; } = null;
        public Matrix<double>? VectorY { get; private set; } = null;
        
        public Layer(int inputs, int outputs)
        {
            if (inputs < 1 || outputs < 1)
                throw new ArgumentException();
            InputsNumber = inputs;
            OutputsNumber = outputs;
            WeightsMatrix = Matrix<double>.Build.Dense(outputs, inputs);
        }

        public void InitializeWeightMatrix(Random? random = null)
        {
            if (random is null)
                random = new Random();

            for (int i = 0; i < WeightsMatrix.RowCount; i++)
            {
                for(int j = 0; j < WeightsMatrix.ColumnCount; j++)
                {
                    WeightsMatrix[i,j] = (random.NextDouble()*2) -1;
                }
            }

            IsInitialised = true;
        }

        public Matrix<double> GetVectorP(Matrix<double> vectorX)
        {
            if (!(vectorX.RowCount == InputsNumber && vectorX.ColumnCount == 1))
                throw new ArgumentException("Wrong vector x dimensions");

            VectorX = vectorX;
            VectorP = WeightsMatrix * vectorX;
            return VectorP.Clone();
        }

        public Matrix<double> GetVectorY(Matrix<double> vectorP, IActFunction actFunction)
        {
            if (!(vectorP.RowCount == OutputsNumber && vectorP.ColumnCount == 1))
                throw new ArgumentException("Wrong vector x dimensions");
            
            VectorY = vectorP.Clone();
            for (int i = 0; i < VectorY.RowCount; i++)
                VectorY[i, 0] = actFunction.ValueAtX(VectorY[i, 0]);
            return VectorY.Clone();
        }

        public Matrix<double> Run(Matrix<double> input, IActFunction actFunction)
        {
            VectorX = null;
            VectorP = null;
            VectorY = null;
            return GetVectorY(GetVectorP(input), actFunction);
        }

        public Matrix<double> GetdYdP(Matrix<double> vectorP, IActFunction actFunction)
        {
            Matrix<double> result = Matrix<double>.Build.Dense(OutputsNumber, OutputsNumber);
            for (int i = 0; i < OutputsNumber; i++)
            {
                result[i,i] = actFunction.DerivativeAtX(vectorP[i,0]);
            }
            return result;
        }

        public Matrix<double> BackPropAndLearn(Matrix<double> dLdY, double learningRate, IActFunction actFunctuin, out Matrix<double> dLdW)
        {
            if (dLdY.RowCount != OutputsNumber || dLdY.ColumnCount != 1)
                throw new ArgumentException();

            Matrix<double> dYdP = GetdYdP(VectorP??throw new NullReferenceException(), actFunctuin);
            Matrix<double> dLdP = dYdP.TransposeThisAndMultiply(dLdY);
            dLdW = dLdP * VectorX.Transpose();
            Matrix<double> dLdX = WeightsMatrix.TransposeThisAndMultiply(dLdP);

            WeightsMatrix = WeightsMatrix - (learningRate * dLdW);
            return dLdX;
        }

        public void SetWeights(double[] weights)
        {
            WeightsMatrix = Matrix<double>.Build.Dense(WeightsMatrix.RowCount, WeightsMatrix.ColumnCount, weights);
        }
        public override string ToString()
        {
            return $"[{InputsNumber} -> {OutputsNumber}]";
        }
    }
}
