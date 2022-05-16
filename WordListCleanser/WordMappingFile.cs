using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordListCleanser
{
    class WordMappingFile
    {
        public WordMappingFile(string filePath)
        {
            this.FilePath = filePath;
            this.LoadContent();
        }

        public int LineCount
        {
            get
            {
                return MappingList.Count;
            }
        }

        public string GetKeyAtLine(int lineNumber)
        {
            if (lineNumber > MappingList.Count)
            {
                return "Line does not exist";
            }
            else
            {
                return MappingList.Keys.ElementAt(lineNumber);
            }
        }

        public string GetValueAtLine(int lineNumber)
        {
            if (lineNumber > MappingList.Count)
            {
                return "Line does not exist";
            }
            else
            {
                return MappingList.Values.ElementAt(lineNumber);
            }
        }

        public string GetValue(string key)
        {
            if (!MappingList.ContainsKey(key))
            {
                return "Key does not exist";
            }
            else
            {
                return MappingList[key];
            }
        }

        public bool ContainsKey(string key)
        {
            return MappingList.ContainsKey(key);
        }

        private void LoadContent()
        {
            MappingList = new Dictionary<string, string>();
            foreach (string line in File.ReadLines(FilePath))
            {
                string[] lineSplit = line.Split(',');
                if (lineSplit.Count() < 2)
                {
                    continue;
                }
                else
                {
                    var key = lineSplit[0].Trim().ToLower();
                    if (!MappingList.ContainsKey(key))
                    {
                        MappingList.Add(key, lineSplit[1].Trim().ToLower());
                    }
                }
            }
        }

        private string FilePath { get; set;}

        private Dictionary<string, string> MappingList { get; set; }

    }
}
