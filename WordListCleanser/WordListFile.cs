using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordListCleanser
{
    class WordListFile
    {
        public WordListFile(string filePath, string type)
        {
            this.FilePath = filePath;
            this.Type = type;
            this.LoadContent();
        }

        public WordListFile(List<WordListFile> files)
        {
            WordList = new List<string>();

            foreach (var file in files)
            {
                WordList.AddRange(file.WordList);
            }
            this.FilePath = String.Empty;
            this.Type = "Combined";
        }

        public int LineCount
        {
            get
            {
                return WordList.Count;
            }
        }

        public string GetLine(int lineNumber)
        {
            if (lineNumber > WordList.Count)
            { 
                return "Line does not exist";
            }
            else
            {
                return WordList[lineNumber];
            }
        }

        private void LoadContent()
        {
            WordList = File.ReadAllLines(FilePath).ToList<string>();
        }

        public string Type { get; private set; }

        public List<string> WordList { get; private set; }

        private string FilePath { get; set; }
    }
}
