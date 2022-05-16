using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordListCleanser
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> feedbackTypeList = new List<string>() { "Coach","Parent","Player" };
            List<WordListFile> wordListFiles = new List<WordListFile>();

            foreach (string type in feedbackTypeList)
            {
                WordListFile wordList = new WordListFile(String.Format("c:\\temp\\WordList_{0}.txt", type), type);
                wordListFiles.Add(wordList);
            }
            
            
            //Console.WriteLine("Word Count: " + wordList.LineCount);
            //Console.WriteLine("Enter desired Line!");
            //int desiredLineNumber = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Word at line {0} is: {1}", desiredLineNumber, wordList.GetLine(desiredLineNumber));
            //Console.WriteLine("Done. Press Enter to exit.");
            //Console.ReadLine();

            WordMappingFile mappingList = new WordMappingFile(@"c:\temp\MappingList.csv");

            //Console.WriteLine("Word Count: " + mappingList.LineCount);
            //Console.WriteLine("Enter desired Line!");
            //int desiredLineNumber = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Key at line {0} is: {1}", desiredLineNumber, mappingList.GetKeyAtLine(desiredLineNumber));
            //Console.WriteLine("Value at line {0} is: {1}", desiredLineNumber, mappingList.GetValueAtLine(desiredLineNumber));
            //Console.WriteLine("Done. Press Enter to exit.");
            //Console.ReadLine();

            WordListInsights insights = new WordListInsights(wordListFiles, mappingList);

            insights.WriteAllFileContent();

        }


    }
}
