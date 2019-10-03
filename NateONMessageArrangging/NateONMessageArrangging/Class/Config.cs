using System;
using System.Collections.Generic;
using System.Text;

namespace NateONMessageArrangging.Class
{
    public class Config
    {
        readonly string FilePath;
        public Global.OptionValues OptionValues;
        public Config()
        {
            FilePath = AppDomain.CurrentDomain.BaseDirectory + "config.txt";
        }

        public void Include()
        {
            TXT txt = new TXT(FilePath);
            string[] lines = txt.GetStringLines();
            foreach(string line in lines)
            {
                if (line.Contains(Global.OptionNames.TagetFilePath))
                {
                    this.OptionValues.TagetFilePath = getOptionText(Global.OptionNames.TagetFilePath, line);
                }
                else if (line.Contains(Global.OptionNames.DateTimeFormat))
                {
                    this.OptionValues.DateTimeFormat = getOptionText(Global.OptionNames.DateTimeFormat, line);
                }
                else if (line.Contains(Global.OptionNames.TalkLastLineDelimiter))
                {
                    this.OptionValues.TalkLastLineDelimiter = getOptionText(Global.OptionNames.TalkLastLineDelimiter, line);
                }
                else if (line.Contains(Global.OptionNames.TalkerNameFirstString))
                {
                    this.OptionValues.TalkerNameFirstString = getOptionText(Global.OptionNames.TalkerNameFirstString, line);
                }
                else if (line.Contains(Global.OptionNames.MyName))
                {
                    this.OptionValues.MyName = getOptionText(Global.OptionNames.MyName, line);
                }
                else if (line.Contains(Global.OptionNames.FirstNameInputString))
                {
                    this.OptionValues.FirstNameInputString = getOptionText(Global.OptionNames.FirstNameInputString, line);
                }
                else if (line.Contains(Global.OptionNames.DivisionNameORTalk))
                {
                    this.OptionValues.DivisionNameORTalk = getOptionText(Global.OptionNames.DivisionNameORTalk, line);
                }
            }
        }

        /// <summary>
        /// 옵션과 값이 합쳐진 문자열에 대해 옵션에 대한 값을 리턴
        /// </summary>
        /// <param name="PropertName"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private string getOptionText(string OptionName, string line)
        {
            return line.Replace(OptionName + "=", string.Empty);
        }
    }

}
