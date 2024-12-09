using MyML.Functions;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Random;
using System.Text.Json;

namespace MyML
{
    public class NN
    {
        public IActFunction ActFunction { get; private set; }
        public int[] LayerSizes { get; private set; }
        public int InputSize { get; private set; }
        public int OutputSize {  get; private set; }
        public Layer[] Layers { get; private set; }    
        public int EpochCount { get; private set; }
        public List<EpochStats> EpochStats { get; private set; }

        public NN(int[] parameters, IActFunction actFunction, int? randSeed = null)
        {
            // [входных сигналов, нейронов первого слоя, нейронов второго слоя, ...]
            if (parameters.Length < 2)
                throw new ArgumentException();

            ActFunction = actFunction;
            LayerSizes = (int[]) parameters.Clone();
            Layers = new Layer[parameters.Length - 1];
            EpochStats = new List<EpochStats>();

            Random random = (randSeed is null) ? new Random() : new Random(randSeed.Value);

            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i] = new Layer(parameters[i], parameters[i+1]);
                Layers[i].InitializeWeightMatrix(random);
            }
            InputSize = LayerSizes[0];
            OutputSize = LayerSizes[LayerSizes.Length-1];
            EpochCount = 0;
        }
        internal NN(NNStorage data, IActFunction actFunction)
        {
            if (data.LayerSizes.Length < 2)
                throw new ArgumentException();

            ActFunction = actFunction;
            ActFunction = actFunction;
            LayerSizes = (int[]) data.LayerSizes.Clone();
            Layers = new Layer[data.LayerSizes.Length - 1];
            
            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i] = new Layer(data.LayerSizes[i], data.LayerSizes[i + 1]);
                Layers[i].SetWeights(data.Weights[i].ToArray());
            }
            InputSize = LayerSizes[0];
            OutputSize = LayerSizes[LayerSizes.Length - 1];
            EpochCount = data.EpochCount;
            EpochStats = data.EpochStats;
        }

        public static NN BuildFromJson(string path, IActFunction actFunction)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            string serialized = File.ReadAllText(path);
            NNStorage deserialized = JsonSerializer.Deserialize<NNStorage>(serialized) ?? throw new Exception("Unable to deserialize");
            return new NN(deserialized, actFunction);
        }

        public void ExportToJson(string path)
        {
            var storage = new NNStorage(this);
            string serialized = JsonSerializer.Serialize(storage);
            File.WriteAllText(path, serialized);
        }

        public static Matrix<double> Softmax(Matrix<double> input)
        {
            if (input.ColumnCount != 1)
                throw new ArgumentException();

            double sum = 0;
            var result = input.Clone();
            for (int i = 0; i < input.RowCount; i++)
            {
                result[i, 0] = Math.Exp(input[i, 0]);
                sum += result[i, 0];
            }
            return (1 / sum) * result;
        }

        public Matrix<double> Run(Matrix<double> input)
        {
            if (input.ColumnCount != 1 && input.RowCount != LayerSizes[0])
                throw new ArgumentException("Incorrect input dimensions");

            Matrix<double> result = input.Clone();
            for (int i = 0; i < Layers.Length; i++)
            {
                result = Layers[i].Run(result, ActFunction);
            }
            
            return Softmax(result);
        }

        public void Learn(Matrix<double> output, Matrix<double> targetResult, double learningRate, out double logLoss)
        {
            logLoss = LogLoss(output, targetResult);
            Matrix<double> dLdY = output - targetResult;
            for (int i = Layers.Length - 1; i >= 0; i--)
            {
                dLdY = Layers[i].BackPropAndLearn(dLdY, learningRate, ActFunction, out _);
            }
        }

        public static double LogLoss(Matrix<double> real, Matrix<double> ideal)
        {
            if (real.ColumnCount != 1 || ideal.ColumnCount != 1 || real.RowCount != ideal.RowCount)
                throw new ArgumentException("");

            double result = 0;
            for (int i = 0; i < real.RowCount; i++)
            {
                if (real[i, 0] == 0 || ideal[i, 0] == 0)
                    continue;

                result -= ideal[i, 0] * Math.Log(real[i, 0]);
            }

            return result;
        }

        public Matrix<double> RunAndLearn(Matrix<double> input, Matrix<double> targetResult, double learningRate, out double logLoss)
        {
            Matrix<double> output = Run(input);

            Learn(output, targetResult, learningRate, out logLoss);

            return output;
        }

        public Matrix<double> BuildIdeal(int classNumber)
        {
            int outputsNumber = Layers[Layers.Length - 1].OutputsNumber;
            if (classNumber >= outputsNumber)
                throw new ArgumentException();

            var ideal = Matrix<double>.Build.Dense(outputsNumber, 1);
            ideal[classNumber, 0] = 1;

            return ideal;
        }

        public int DecideBySoftmax(Matrix<double> softmaxxedOutput)
        {
            if (softmaxxedOutput.ColumnCount != 1 || softmaxxedOutput.RowCount != OutputSize)
                throw new ArgumentException();

            double max = softmaxxedOutput[0, 0];
            int index = 0;
            for (int i = 1; i < OutputSize; i++) 
            { 
                if (softmaxxedOutput[i, 0] > max)
                {
                    index = i;
                    max = softmaxxedOutput[i, 0];
                }
            }

            return index;
        }

        public Matrix<double> ParseStringCSV(string csv, out int classNumber )
        {
            string[] split = csv.Split(',');

            classNumber = int.Parse(split[1]);

            Matrix<double> result = Matrix<double>.Build.Dense(split[2].Length,1);
            for (int i = 0; i < split[2].Length; i++) {
                result[i, 0] = (split[2][i] == '1') ? 1 : 0;
            }

            return result;
        }

        public void LearnFromFile(string filename, double learningRate) 
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();

            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string? buf = sr.ReadLine();
                    if (buf == null)
                        break;
                    string csvString = buf;
                    var inputVector = ParseStringCSV(csvString, out int classNumber);
                    var ideal = BuildIdeal(classNumber);

                    RunAndLearn(inputVector, ideal, learningRate, out double logLoss);
                }
            }
        }

        public Stats RunFromFile(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();

            var cn = new ConfusionMatrix(OutputSize);
            double logLossSum = 0;
            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string? buf = sr.ReadLine();
                    if (buf == null)
                        break;
                    string csvString = buf;
                    var inputVector = ParseStringCSV(csvString, out int classNumber);
                    var ideal = BuildIdeal(classNumber);

                    var networkOutput = Run(inputVector);
                    double logLoss = LogLoss(networkOutput, ideal);

                    int decision = DecideBySoftmax(networkOutput);
                    cn.AddResult(classNumber, decision);
                    logLossSum += logLoss;
                }
            }

            return new Stats(cn, logLossSum);
        }

        public void EpochFromFiles(string trainingFile, string validationFile, double learningRate)
        {
            LearnFromFile(trainingFile, learningRate);
            var trainingStats = RunFromFile(trainingFile);
            var validationStats = RunFromFile(validationFile);
            EpochCount++;
            EpochStats.Add(new EpochStats(trainingStats, validationStats));
        }

        public override string ToString()
        {
            string result = $"{Layers.Length}:[ ";
            for (int i = 0; i < LayerSizes.Length; i++)
                result += $"{LayerSizes[i]} ";
            result+= "]: ";
            foreach (var layer in Layers)
                result += layer.ToString();

            return result;
        }
    }
}
