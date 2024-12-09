using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyML
{
    internal class NNStorage
    {
        public int[] LayerSizes {  get; private set; }
        public List<double>[] Weights { get; private set; }
        public int EpochCount { get; private set; }
        public List<EpochStats> EpochStats { get; private set; }
        
        public NNStorage(NN nn) {
            LayerSizes = nn.LayerSizes;
            Weights = new List<double>[nn.Layers.Length];
            for (int i = 0; i < nn.Layers.Length; i++)
            {
                Layer layer = nn.Layers[i];
                Weights[i] = new List<double>(layer.WeightsMatrix.AsColumnMajorArray());
            }
            EpochCount = nn.EpochCount;
            EpochStats = nn.EpochStats;
        }

        [JsonConstructor]
        public NNStorage(int[] LayerSizes, List<double>[] Weights, int EpochCount, List<EpochStats> EpochStats)
        {
            this.LayerSizes = LayerSizes;
            this.Weights = Weights;
            this.EpochCount = EpochCount;
            this.EpochStats = EpochStats;
        }
    }
}
