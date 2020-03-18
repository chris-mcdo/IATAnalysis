using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IATAnalysis
{
    class IATBlock
    {
        public const String DataFileName = "IATQuestions.json";

        public String BlockName;
        public String DataDir;
        public List<IATQuestion> QuestionList;

        public double Mean; // mean correct response time in ms

        public IATBlock(String dataDir)
        {
            BlockName = new DirectoryInfo(dataDir).Name;
            DataDir = dataDir + @"\";
            GetQuestions();
        }

        public void EliminateOutliers() // Step 2
        {
            // Get all trials with latencies smaller than 10,000ms
            QuestionList = QuestionList.FindAll(q => q.CompletedTimeStamp - q.TimeStamp < 10000);
        }

        // Calculate mean latencies for correct answers in block (in ms)
        public void CalcMean()
        {
            int count = 0; // number of correct answers
            double sum = 0;
            foreach (IATQuestion q in QuestionList)
            {
                if (q.IsDetectionWrong==false)
                {
                    count += 1;
                    sum += q.CompletedTimeStamp - q.TimeStamp;
                }
            }
            Mean = sum / count;
        } // Step 4

        public void GetQuestions()
        {
            // Loading JSON string from file
            String filePath = DataDir + DataFileName;
            String jsonString = File.ReadAllText(filePath);
            QuestionList = JsonConvert.DeserializeObject<List<IATQuestion>>(jsonString);
        }

    }
}
