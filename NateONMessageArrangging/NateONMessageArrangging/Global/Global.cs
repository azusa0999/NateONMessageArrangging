using System;
using System.Collections.Generic;
using System.Text;

namespace NateONMessageArrangging.Class
{
    public static class Global
    {
        /// <summary>
        /// 환경설정 멤버변수명 상수
        /// </summary>
        public static class OptionNames
        {
            public const string TagetFilePath = "TagetFilePath";
            public const string DateTimeFormat = "DateTimeFormat";
            public const string TalkLastLineDelimiter = "TalkLastLineDelimiter";
            public const string TalkerNameFirstString = "TalkerNameFirstString";
            public const string MyName = "MyName";
            public const string FirstNameInputString = "FirstNameInputString";
            public const string DivisionNameORTalk = "DivisionNameORTalk";
        }

        /// <summary>
        /// 환경설정 멤버변수 구조체
        /// </summary>
        public struct OptionValues
        {
            public string TagetFilePath;
            public string DateTimeFormat;
            public string TalkLastLineDelimiter;
            public string TalkerNameFirstString;
            public string MyName;
            public string FirstNameInputString;
            public string DivisionNameORTalk;
        }

        public const int DateTimeStringLength = 21;
        public const string NewLineString = "\r\n";
        public const string TooNewLineString = "\r\n\r\n";
    }


}
