using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NateONMessageArrangging.Class
{
    public class TXT
    {
        public string FilePath = string.Empty;

        public TXT(string filePath)
        {
            FilePath = filePath;
        }

        public string[] GetStringLines()
        {
            return File.ReadAllLines(FilePath);
        }

        public void Save(List<NateONTalk> talks)
        {
            
        }
    }
}
