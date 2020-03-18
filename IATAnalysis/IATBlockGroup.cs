using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IATAnalysis
{
    class IATBlockGroup
    {
        public List<IATBlock> BlockList;
        public double SD;
        public double GroupDScore;

        public IATBlockGroup(List<IATBlock> blockList)
        {
            BlockList = blockList;
        } // Step 5

        public void CalculateSD()
        {
            // Pooled SD from all questions in block group

            List<double> latencies = new List<double>();
            foreach (IATBlock block in BlockList)
            {
                foreach (IATQuestion q in block.QuestionList)
                {
                    latencies.Add(q.Latency);
                }
            }

            // Standard deviation of all latencies
            SD = StandardDeviation(latencies);
        } // Step 5

        public void ReplaceLatencies()
        {
            // Replace error latencies with (block mean) + 2 * (block sd)

            foreach (IATBlock block in BlockList)
            {
                foreach (IATQuestion q in block.QuestionList)
                {
                    if (!q.CorrectResponse)
                    {
                        q.Latency = block.CorrectMean + 2 * SD;
                    }
                }
            }
        } // Step 6

        public void CalculateGroupDScore()
        {
            // Calculate D-score for this block group

            // Average latency of each block
            foreach (IATBlock block in BlockList)
            {
                block.CalculateTotalMean();
            }

            GroupDScore = (BlockList[1].TotalMean - BlockList[0].TotalMean) / SD;
        } // Step 7



        private double StandardDeviation(IEnumerable<double> values)
        {
            // Calculate sample standard deviation of list of double values

            // Mean
            double mean = values.Average();

            // Sum of squared deviations from mean       
            double sum = values.Sum(x => Math.Pow(x - mean, 2));

            // Sample standard deviation
            return Math.Sqrt((sum) / (values.Count() - 1));
        }

    }
}
