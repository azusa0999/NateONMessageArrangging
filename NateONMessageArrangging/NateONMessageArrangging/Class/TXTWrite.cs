using System.Text;
using System.IO;

namespace NateONMessageArrangging.Class
{
    public class TXTWrite : TXT
    {
        public TXTWrite(string fileDirectory, string fileName)
        {
            FileDirectory = fileDirectory;
            FileName = fileName;
            FilePath = FileDirectory + FileName;
        }

        public void Write(NateONTalk talk)
        {
            if (!Directory.Exists(FileDirectory))  // if it doesn't exist, create
                Directory.CreateDirectory(FileDirectory);

            File.WriteAllText(FilePath, talk.TalkContent, Encoding.Default);
        }
    }
}
