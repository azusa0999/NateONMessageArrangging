using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NateONMessageArrangging.Class
{
    public class TXTRead : TXT
    {
        public TXTRead(string filePath)
        {
            FilePath = filePath;
        }

        public string[] GetStringLines()
        {
            return File.ReadAllLines(FilePath);
        }
    }
}
