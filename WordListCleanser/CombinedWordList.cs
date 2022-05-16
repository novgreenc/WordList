using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordListCleanser
{
    class CombinedWordList
    {
        public CombinedWordList(List<NormalizedWordList> wordListFiles)
        {
            this.NormalizedWordLists = wordListFiles;
            CombineWordLists();
        }

        private void CombineWordLists()
        {
            CombinedSortedNormalizedWordList = new List<string>();
            CombinedWordCounts = new Dictionary<string, Dictionary<string, int>>();
            types = new List<string>();

            foreach (var wordList in this.NormalizedWordLists)
            {
                CombinedSortedNormalizedWordList.AddRange(wordList.SortedNormalizedWordList);
                string type = wordList.OriginalWordList.Type;
                types.Add(type);

                foreach (KeyValuePair<string,int> kvp in wordList.WordCounts)
                {
                    string key = kvp.Key;
                    int value = kvp.Value;

                    if (CombinedWordCounts.ContainsKey(key))
                    {
                        if (CombinedWordCounts[key].ContainsKey(type))
                        {
                            CombinedWordCounts[key][type] = CombinedWordCounts[key][type] + value;
                        }
                        else
                        {
                            CombinedWordCounts[key].Add(type, value);
                        }
                    }
                    else
                    {
                        Dictionary<string, int> typeCount = new Dictionary<string, int>();
                        typeCount.Add(type, value);
                        CombinedWordCounts.Add(kvp.Key, typeCount);
                    }
                }
                
            }

            CombinedSortedNormalizedWordList.Sort();
        }

        public void WriteWordListContentsToFile()
        {
            WriteNormalizedWordListToFile();
            WriteWordCountsToFile();
        }

        public void WriteNormalizedWordListToFile()
        {
            var normalizedWordListOutput = new StringBuilder();

            string normalizedWordListPath = String.Format("c:\\temp\\WordListNormalized_{0}.txt", "Combined");

            foreach (string word in CombinedSortedNormalizedWordList)
            {
                normalizedWordListOutput.AppendLine(word);
            }

            File.WriteAllText(normalizedWordListPath, normalizedWordListOutput.ToString());
        }

        public void WriteWordCountsToFile()
        {
            var wordCountOutput = new StringBuilder();

            string wordCountPath = String.Format("c:\\temp\\WordListCount_{0}.csv", "Combined");

            string headerLine = "word";
            foreach (string type in types)
            {
                headerLine = headerLine + String.Format(",{0}", type);
            }

            wordCountOutput.AppendLine(headerLine);

            foreach (KeyValuePair<string, Dictionary<string, int>> wordCountMapping in CombinedWordCounts)
            {
                string values = String.Empty;
                foreach (string type in types)
                {
                    int value = 0;
                    if (wordCountMapping.Value.ContainsKey(type))
                    {
                        value = wordCountMapping.Value[type];
                    }

                    values = values + String.Format(",{0}", value);
                }
                string line = String.Format("{0}{1}", wordCountMapping.Key, values);
                wordCountOutput.AppendLine(line);
            }

            File.WriteAllText(wordCountPath, wordCountOutput.ToString());
        }

        private List<NormalizedWordList> NormalizedWordLists { get; set; }

        private List<string> CombinedSortedNormalizedWordList { get; set; }

        private Dictionary<string, Dictionary<string, int>> CombinedWordCounts { get; set; }

        private List<string> types { get; set; }
    }
}
