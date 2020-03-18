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

        public bool CheckedValid = false;

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

        public double[] GetDScore()
        {
            double[] score = new double[TestList.Count];
            for (int i = 0; i<TestList.Count; i++)
            {
                // perform steps; see whether it is valid; get score for each test.
            }
            return score;
        }
    }
}
