using System;
using System.Collections.Generic;
using NateONMessageArrangging.Class;

namespace NateONMessageArrangging
{
    class Program
    {
        static void Main(string[] args)
        {
            //설정파일 세팅
            Config config = new Config();
            config.Include();

            //백업된 대화텍스트파일 열기
            TXT txt = new TXT(config.OptionValues.TagetFilePath);
            string[] lines = txt.GetStringLines();

            List<NateONTalk> talks = new List<NateONTalk>();
            string currentTalkerName = string.Empty;
            DateTime currentTalkerDatetime = new DateTime();
            NateONTalk talk = null;

            //대화 데이터를 NateONTalk 형태로 담아놓기
            foreach (string line in lines)
            {
                //상대방 이름이 나오는 라인 (날짜도 포함되어있음)
                if (talk == null && line.Contains(config.OptionValues.TalkerNameFirstString))
                {
                    //먼저 앞에 불필요한 대화명 앞 구분 문자열을 지운다.
                    string str = line.Replace(config.OptionValues.TalkerNameFirstString, string.Empty);
                    //대화명
                    string talkName = str.Replace(str.Substring(str.Length - Global.DateTimeStringLength), string.Empty).TrimEnd();
                    //대화날짜
                    DateTime talkDate = DateTime.Parse(str.Substring(str.Length - Global.DateTimeStringLength, config.OptionValues.DateTimeFormat.Length));

                    currentTalkerName = talkName;
                    currentTalkerDatetime = talkDate;
                    talk = new NateONTalk();
                }
                //마지막 구분자라면
                else if (talk != null && line.Contains(config.OptionValues.TalkLastLineDelimiter))
                {
                    talk.TalkerName = currentTalkerName;
                    talk.TalkDateTime = currentTalkerDatetime;
                    talk.TalkContent = talk.TalkContent + config.OptionValues.TalkLastLineDelimiter + Global.TooNewLineString;

                    //추가한 후 상대방 이름과 날짜로 정렬시킨다
                    talks.Add(talk);
                    talks.Sort((t1, t2) => t1.TalkerName.CompareTo(t2.TalkerName));
                    talks.Sort((t1, t2) => t1.TalkDateTime.CompareTo(t2.TalkDateTime));

                    //다음 등록을 위한 초기화
                    talk = null;
                    currentTalkerDatetime = new DateTime();
                    currentTalkerName = string.Empty;
                }
                //그 외에 내용이 있다면 문자열만 추가한다.
                else if(talk != null && line.TrimEnd().Length > 0)
                {
                    //대화명이나 내 이름이 나오면 앞에 구분자를 넣어준다.
                    if(config.OptionValues.FirstNameInputString.Length > 0)
                    {
                        if (line.Contains(config.OptionValues.MyName) || line.Contains(currentTalkerName))
                        {
                            talk.TalkContent = config.OptionValues.FirstNameInputString;
                        }
                    }
                    //내용을 추가한다.
                    talk.TalkContent = talk.TalkContent + line + Global.NewLineString;
                }
            }

        }
    }
}
