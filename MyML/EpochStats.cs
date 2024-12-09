using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyML
{
    public class EpochStats
    {
        public Stats TrainingStats { get; private set; }
        public Stats ValidationStats { get; private set; }
        [JsonConstructor]
        public EpochStats(Stats trainingStats, Stats validationStats)
        {
            TrainingStats = trainingStats;
            ValidationStats = validationStats;
        }
    }
}
