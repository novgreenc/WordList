using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordListCleanser
{
    class NormalizedWordList
    {
        public NormalizedWordList(WordListFile wordListFile, WordMappingFile wordMappingFile)
        {
            OriginalWordList = wordListFile;
            GenerateNormalizedWordList(wordListFile, wordMappingFile);
        }

        public void WriteWordListContentsToFile()
        {
            WriteNormalizedWordListToFile();
            WriteWordCountsToFile();
        }

        public void WriteNormalizedWordListToFile()
        {
            var normalizedWordListOutput = new StringBuilder();

            string normalizedWordListPath = String.Format("c:\\temp\\WordListNormalized_{0}.txt", OriginalWordList.Type);

            foreach (string word in SortedNormalizedWordList)
            {
                normalizedWordListOutput.AppendLine(word);
            }

            File.WriteAllText(normalizedWordListPath, normalizedWordListOutput.ToString());
        }

        public void WriteWordCountsToFile()
        {
            var wordCountOutput = new StringBuilder();

            string wordCountPath = String.Format("c:\\temp\\WordListCount_{0}.csv", OriginalWordList.Type);

            string headerLine = String.Format("word,count");
            wordCountOutput.AppendLine(headerLine);

            foreach (KeyValuePair<string, int> wordCountMapping in WordCounts)
            {
                string line = String.Format("{0},{1}", wordCountMapping.Key, wordCountMapping.Value);
                wordCountOutput.AppendLine(line);
            }

            File.WriteAllText(wordCountPath, wordCountOutput.ToString());
        }

        private void CountWord(string word)
        {

            if (WordCounts.ContainsKey(word))
            {
                WordCounts[word]++;
            }
            else
            {
                WordCounts.Add(word, 1);
            }
        }
        private void GenerateNormalizedWordList(WordListFile wordListFile, WordMappingFile wordMappingFile)
        {
            SortedNormalizedWordList = new List<string>();
            WordCounts = new Dictionary<string, int>();

            string outputWord = String.Empty;

            for (int i = 0; i < wordListFile.WordList.Count; i++)
            {
                string currentWord = wordListFile.GetLine(i).Trim().ToLower();

                if (wordMappingFile.ContainsKey(currentWord))
                {
                    outputWord = wordMappingFile.GetValue(currentWord);
                }
                else
                {
                    outputWord = currentWord;
                }

                var outputWordLower = outputWord.ToLower();

                SortedNormalizedWordList.Add(outputWordLower);
                CountWord(outputWordLower);
            }

            SortedNormalizedWordList.Sort();
        }

        public WordListFile OriginalWordList { get; private set; }

        public List<string> SortedNormalizedWordList { get; private set; }

        public Dictionary<string, int> WordCounts { get; private set; }
    }
}
