using System;
using System.IO;
using System.Windows.Forms;

namespace IATAnalysis
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("IAT D-score calculator. See README for required data format and folder organisation.\r\n");

            int programOption = 0;

            String dataDir = GetDataDirectory();

            while (programOption != 3)
            {

                Console.WriteLine("Select: 1 - Calculate a D-score; 2 - change experiment folder, 3 - exit");
                programOption = int.Parse(Console.ReadLine());

                if (programOption == 1)
                {
                    // Selecting test.
                    double[] score = CalculateDScore(dataDir);
                    Console.WriteLine("D-score for first (null) IAT is: " + score[0] + "\r\n" +
                        "D-score for second (real) IAT is: " + score[1]);
                }
                else if (programOption == 2)
                {
                    dataDir = GetDataDirectory();
                }
                else if (programOption == 3)
                {
                    Console.WriteLine("Exiting...");
                }
            }

        }

        static String GetDataDirectory()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            do
            {
                Console.WriteLine("Navigate to the 'IAT' folder of the experiment. Your screen should show two folders, IATTest0 and IATTest1. (Press enter to show dialog...)");
                Console.ReadLine();
            } while (!fbd.ShowDialog().Equals(DialogResult.OK));

            return fbd.SelectedPath + @"\";
        }

        static double[] CalculateDScore(String dataDir)
        {
            Console.WriteLine("Calculating D-score...");
            double[] dScore = new double[]{ 0, 0 };

            IATDataFormatter iatDF = new IATDataFormatter(dataDir);

            return dScore;
        }
    }
}
