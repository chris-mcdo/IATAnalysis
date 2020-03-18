using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IATAnalysis
{
    class IATTest
    {
        public int[] UsedBlocksIndices { get; set; } = { 2, 3, 5, 6 };

        public String TestName;
        public String DataDir;

        public List<IATBlock> BlockList;
        public List<IATBlockGroup> BlockGroupList;


        public IATTest(String dataDir)
        {
            TestName = new DirectoryInfo(dataDir).Name;
            DataDir = dataDir + @"\";
            GetAllBlocks(); // note: blocks are labelled "questions" in data folder
        }

        public void GetAllBlocks()
        {
            // Get all blocks from file

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

        public void RemoveBlocks()
        {
            // Keep only relevant blocks: 3, 4, 6 and 7

            List<IATBlock> newBlockList = new List<IATBlock>();
            foreach (int ind in UsedBlocksIndices)
            {
                newBlockList.Add(BlockList[ind]);
            }
            BlockList = newBlockList;
        } // Step 1

        public void EliminateQuestions()
        {
            // Eliminate questions answered in over 10,000ms

            foreach (IATBlock block in BlockList)
            {
                block.EliminateOutliers();
            }
        } // Step 2

        public bool IsSubjectValid()
        {
            // Reject subject if they answer too quickly

            int fastCount = 0; // Number of questions answered in less than 300ms
            int totalCount = 0; // Total number of questions answered
            foreach (IATBlock block in BlockList)
            {
                foreach (IATQuestion q in block.QuestionList)
                {
                    if (q.Latency < 300) fastCount += 1;
                }
                totalCount += block.QuestionList.Count;
            }

            // Require: less than 10% of answers to be faster than 300ms
            return ((double)fastCount / totalCount) < 0.1;
        } // Step 3

        public void CalculateMeanLatencies()
        {
            // Calculate mean latencies for correct answers in each block (in ms)

            foreach (IATBlock block in BlockList)
            {
                block.CalculateCorrectMean();
            }
        } // Step 4

        public void CalculateSDs() 
        {
            // Calculate standard deviation of response latency for all questions within list of blocks

            // Make two "block groups"
            BlockGroupList = new List<IATBlockGroup>();
            BlockGroupList.Add(new IATBlockGroup(
                new List<IATBlock> { BlockList[0], BlockList[2] }
                ));
            BlockGroupList.Add(new IATBlockGroup(
                new List<IATBlock> { BlockList[1], BlockList[3] }
                ));

            foreach (IATBlockGroup group in BlockGroupList)
            {
                group.CalculateSD();
            }

            // Work with data in 2 blocks (B3 & B6, B4 & B7) from now on

        } // Step 5

        public void ReplaceErrorLatencies()
        {
            // Replace error latencies with (block mean) + 2 * (block sd)

            foreach (IATBlockGroup group in BlockGroupList)
            {
                group.ReplaceLatencies();
            }

        } // Step 6

        public double GetDScore() 
        {
            // Calculate overall D-score

            foreach (IATBlockGroup group in BlockGroupList)
            {
                group.CalculateGroupDScore();
            }

            // Return average of D-scores
            return BlockGroupList.Average(group => group.GroupDScore);
        } // Step 7

    }

}
