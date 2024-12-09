using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyML
{
    public class Stats
    {
        public int Size { get; private set; }
        public double[] Accuracy { get; set; }
        public double[] Precision { get; set; }
        public double[] Recall { get; set; }
        public double Loss { get; set; }

        public Stats(ConfusionMatrix cn, double logLossSum)
        {
            Size = cn.Size;
            double N = cn.N();
            if (N < 1)
                throw new Exception("No samples");
            Loss = logLossSum / N;
            Accuracy = new double[Size];
            Precision = new double[Size];
            Recall = new double[Size];

            for (int i = 0; i < Size; i++)
            {
                double TP = cn.TP(i), TN = cn.TN(i), FP = cn.FP(i), FN = cn.FN(i);

                Accuracy[i] = (TP + TN) / N ;
                if (TP != 0)
                {
                    Precision[i] = TP / (TP + FP);
                    Recall[i] = TP / (TP + FN);
                }
                else { 
                    Precision[i] = 0;
                    Recall[i] = 0;
                }
            }
        }
        [JsonConstructor]
        internal Stats(int Size, double[] Accuracy, double[] Precision, double[] Recall, double Loss)
        {
            this.Size = Size;
            this.Accuracy = Accuracy;
            this.Precision = Precision;
            this.Recall = Recall;
            this.Loss = Loss;
        }
    }
}
