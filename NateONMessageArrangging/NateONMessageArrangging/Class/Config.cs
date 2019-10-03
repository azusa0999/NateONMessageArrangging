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
            TXTRead read = new TXTRead(FilePath);
            string[] lines = read.GetStringLines();
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
                    //첫글자만 가져온다.
                    string str = getOptionText(Global.OptionNames.DivisionNameORTalk, line).Substring(0, 1);
                    int i;
                    if (!int.TryParse(str, out i))
                        throw new Exception("config.txt 파일의 DivisionNameORTalk 항목의 값이 0 혹은 1이 아닙니다. 프로그램이 종료됩니다.");
                    this.OptionValues.FileDivisionUnit = i == 0 ? Global.DivisionNameORTalk.Name : Global.DivisionNameORTalk.Talk;
                }
            }

            OptionValuesAvailable();
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

        /// <summary>
        /// 옵션 항들의 값이 모두 있는 것인지 확인. 없으면 오류를 스로우한다.
        /// </summary>
        private void OptionValuesAvailable()
        {
            string EmptyOptionname = string.Empty;
            if (this.OptionValues.TagetFilePath == string.Empty)
                EmptyOptionname = Global.OptionNames.TagetFilePath;
            else if (this.OptionValues.DateTimeFormat == string.Empty)
                EmptyOptionname = Global.OptionNames.DateTimeFormat;
            else if (this.OptionValues.TalkLastLineDelimiter == string.Empty)
                EmptyOptionname = Global.OptionNames.TalkLastLineDelimiter;
            else if (this.OptionValues.TalkerNameFirstString == string.Empty)
                EmptyOptionname = Global.OptionNames.TalkerNameFirstString;
            else if (this.OptionValues.MyName == string.Empty)
                EmptyOptionname = Global.OptionNames.MyName;
            else if (this.OptionValues.FirstNameInputString == string.Empty)
                EmptyOptionname = Global.OptionNames.FirstNameInputString;

            if (EmptyOptionname != string.Empty)
                throw new Exception(string.Format(Global.EmptyOptionMessage, EmptyOptionname));
        }
    }

}
