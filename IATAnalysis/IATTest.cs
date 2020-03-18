using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IATAnalysis
{
    class IATTest
    {
        public int[] UsedBlocksIndices { get; set; } = { 2, 3, 5, 6 };
        public String TestName;
        public String DataDir;

        public List<IATBlock> BlockList;

        public IATTest(String dataDir)
        {
            TestName = new DirectoryInfo(dataDir).Name;
            DataDir = dataDir + @"\";
            GetAllBlocks(); // note: blocks are labelled "questions" in folder
        }

        // Keep only relevant blocks: 3, 4, 6 and 7
        public void RemoveBlocks()
        {
            List<IATBlock> newBlockList = new List<IATBlock>();
            foreach (int ind in UsedBlocksIndices)
            {
                newBlockList.Add(BlockList[ind]);
            }
            BlockList = newBlockList;
        } // Step 1

        // Eliminate questions answered in over 10,000ms 
        public void EliminateQuestions() 
        {
            foreach (IATBlock block in BlockList)
            {
                block.EliminateOutliers();
            }
        } // Step 2

        // Reject subject if they answer too quickly
        public bool IsSubjectValid()
        {
            int fastCount = 0; // Number of questions answered in less than 300ms
            int totalCount = 0; // Total number of questions answered
            foreach (IATBlock block in BlockList)
            {
                foreach (IATQuestion q in block.QuestionList)
                {
                    if (q.CompletedTimeStamp - q.TimeStamp < 300) fastCount += 1;
                }
                totalCount += block.QuestionList.Count;
            }

            // Require: less than 10% of answers to be faster than 300ms
            return ((double)fastCount / totalCount) < 0.1;
        } // Step 3

        // Calculate mean latencies for correct answers in each block (in ms)
        public void CalculateMeanLatencies()
        {
            foreach (IATBlock block in BlockList)
            {

            }
        } // Step 4

        public void GetAllBlocks()
        {
            // Making a new list of blocks
            BlockList = new List<IATBlock>();

            // Getting subdirectories of test folder
            string[] subDirs = Directory.GetDirectories(DataDir); // no trailing slash

            // Creating IAT block for each directory in the test folder
            foreach (String dir in subDirs)
            {
                BlockList.Add(new IATBlock(dir));
            }
        }

    }
}
