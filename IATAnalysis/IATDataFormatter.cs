using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IATAnalysis
{
    class IATDataFormatter
    {
        public List<IATTest> TestList;

        public String DataDir;

        public IATDataFormatter(String dataDir)
        {
            DataDir = dataDir;
            Console.WriteLine("New IATDataFormatter object from dir " + DataDir);
            GetTestList();
        }

        public void GetTestList()
        {
            Console.WriteLine("Making a list of tests...");

            // Making a new list of tests
            TestList = new List<IATTest>();

            // Getting subdirectories of root data folder
            string[] subDirs = Directory.GetDirectories(DataDir); // no trailing slash

            // Creating test for each directory in the root folder
            foreach (String dir in subDirs)
            {
                TestList.Add(new IATTest(dir));
            }
        }

        public List<double> GetDScores()
        {
            List<double> scores = new List<double>();
            foreach (IATTest test in TestList)
            {
                scores.Add(DScore(test));
            }
            return scores;
        }

        public double DScore(IATTest test)
        {
            // Performing steps 1-7, returning D-score result

            test.RemoveBlocks();
            test.EliminateQuestions();
            if (!test.IsSubjectValid()) return -1000; // if subject is invalid, test score -1000 is returned
            test.CalculateMeanLatencies();
            test.CalculateSDs();
            test.ReplaceErrorLatencies();
            return test.GetDScore();
        }
    }
}
