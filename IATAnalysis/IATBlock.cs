using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IATAnalysis
{
    class IATBlock
    {
        public const String DataFileName = "IATQuestions.json";

        public String BlockName;
        public String DataDir;
        public List<IATQuestion> QuestionList;

        public double CorrectMean; // mean correct response time in ms
        public double TotalMean; // mean response time after adjustments in ms

        public IATBlock(String dataDir)
        {
            BlockName = new DirectoryInfo(dataDir).Name;
            DataDir = dataDir + @"\";
            GetQuestions();
        }

        public void GetQuestions()
        {
            // Loading JSON string from file
            String filePath = DataDir + DataFileName;
            String jsonString = File.ReadAllText(filePath);
            List<IATQuestionModel> questionModelList = JsonConvert.DeserializeObject<List<IATQuestionModel>>(jsonString);

            QuestionList = questionModelList
                .Select(q => new IATQuestion()
                {
                    CorrectResponse = !q.IsDetectionWrong,
                    Latency = (double)(q.CompletedTimeStamp - q.TimeStamp)
                })
                .ToList();
        }

        public void EliminateOutliers() 
        {
            // Get all trials with latencies smaller than 10,000ms
            QuestionList = QuestionList.FindAll(q => q.Latency > 0 && q.Latency < 10000);
        } // Step 2

        public void CalculateCorrectMean()
        {
            // Calculate mean latencies for correct answers in block (in ms)

            int count = 0; // number of correct answers
            double sum = 0; // sum of correct latencies
            foreach (IATQuestion q in QuestionList)
            {
                if (q.CorrectResponse)
                {
                    count += 1;
                    sum += q.Latency;
                }
            }
            Console.WriteLine("Calculating correct response mean: sum = " + sum + ", count = " + count);
            CorrectMean = sum / count;
            Console.WriteLine("Calculated mean correct response = " + CorrectMean);
        } // Step 4

        public void CalculateTotalMean()
        {
            // Calculate mean latencies for all answers in block (in ms)
            // Assuming steps 1-6 have been performed

            int count = QuestionList.Count; // number of questions
            double sum = QuestionList.Sum(q => q.Latency); // sum of latencies

            TotalMean = sum / count;
        } // Step 7

    }
}
