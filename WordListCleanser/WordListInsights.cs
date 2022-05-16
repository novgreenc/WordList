using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordListCleanser
{
    internal class WordListInsights
    {
        public WordListInsights(List<WordListFile> wordListFiles, WordMappingFile wordMappingFile)
        {
            WordListFiles = new List<NormalizedWordList>();
            foreach (var file in wordListFiles)
            {
                NormalizedWordList currentWorldList = new NormalizedWordList(file, wordMappingFile);
                WordListFiles.Add(currentWorldList);
            }

            CombinedNormalizedWordList = new CombinedWordList(WordListFiles);
        }

        public void WriteAllFileContent()
        {
            foreach(var file in WordListFiles)
            {
                file.WriteWordListContentsToFile();
            }

            CombinedNormalizedWordList.WriteWordListContentsToFile();
        }

        private CombinedWordList CombinedNormalizedWordList { get; set; }

        private List<NormalizedWordList> WordListFiles { get; set; }
    }
}
