using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace IATAnalysis
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("IAT D-score calculator. See README for required data format and folder organisation.");

            int programOption = 0;

            String dataDir = GetDataDirectory();

            while (programOption != 3)
            {

                Console.WriteLine("\r\nSelect: 1 - Calculate a D-score; 2 - change experiment folder, 3 - exit");
                programOption = int.Parse(Console.ReadLine());

                if (programOption == 1)
                {
                    // Selecting test.
                    List<double> score = CalculateDScores(dataDir);
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

        static List<double> CalculateDScores(String dataDir)
        {
            Console.WriteLine("Calculating D-scores...");

            IATDataFormatter iatDF = new IATDataFormatter(dataDir);

            return iatDF.GetDScores();
        }
    }
}
