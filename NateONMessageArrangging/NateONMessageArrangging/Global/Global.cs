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
            public DivisionNameORTalk FileDivisionUnit;
        }
        /// <summary>
        /// 파일 나누는 기준에 대한 열거자체
        /// </summary>
        public enum DivisionNameORTalk { Name, Talk };
        /// <summary>
        /// 대화날짜가 있는 전체 문자열의 길이
        /// </summary>
        public const int DateTimeStringLength = 21;
        /// <summary>
        /// 대화에서 보낸 메세지마다의 개행
        /// </summary>
        public const string NewLineString = "\r\n";
        /// <summary>
        /// 상대방과의 한 대화가 끝났을 때 이를 구분하기 위한 두 번의 개행
        /// </summary>
        public const string TooNewLineString = "\r\n\r\n";
        /// <summary>
        /// 옵션에 값이 없을 경우 출력할 메세지
        /// </summary>
        public const string EmptyOptionMessage = "config.txt 파일의 {0}에 내용이 없어 작업을 진행할 수 없습니다. 프로그램이 종료됩니다.";
        /// <summary>
        /// 한 대화가 끝날 때 넣을 구분자. 중간에 {0}에는 string.format으로 처리하여 이름 + 날짜를 작성함.
        /// </summary>
        public const string LastLineDelimiter = "//////////////////////////// 대화 끝 : 대화일시 ({0}) //////////////////////////////////";
        public const string SpaceString = " ";
        public const string DateTimeFormat = "yyyy-MM-dd hh:mm:ss";
    }


}
