using System;
using System.Linq;
using System.Collections.Generic;
using NateONMessageArrangging.Class;

namespace NateONMessageArrangging
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("작업이 시작됩니다.");

            try
            {
                //설정파일 세팅
                Config config = new Config();
                config.Include();

                //백업된 대화텍스트파일 열기
                TXTRead read = new TXTRead(config.OptionValues.TagetFilePath);
                string[] lines = read.GetStringLines();

                List<NateONTalk> talks = new List<NateONTalk>();
                string currentTalkerName = string.Empty;
                DateTime currentTalkerDatetime = new DateTime();
                NateONTalk talk = null;

                Console.WriteLine("읽은 대화정보들을 정리 중입니다 ...... ");
                Console.WriteLine();
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
                        talk.TalkContent = line + Global.TooNewLineString;//대화상대 및 날짜에 대한 헤더값의 성격으로 내용에 추가해준다.
                    }
                    //마지막 구분자라면
                    else if (talk != null && line.Contains(config.OptionValues.TalkLastLineDelimiter))
                    {
                        talk.TalkerName = currentTalkerName;
                        talk.TalkDateTime = currentTalkerDatetime;
                        talk.TalkContent = talk.TalkContent + string.Format(Global.LastLineDelimiter, talk.TalkDateTime.ToString(Global.DateTimeFormat)) + Global.TooNewLineString + Global.TooNewLineString;

                        //추가
                        talks.Add(talk);

                        //다음 등록을 위한 초기화
                        talk = null;
                        currentTalkerDatetime = new DateTime();
                        currentTalkerName = string.Empty;
                    }
                    //그 외에 내용이 있다면 문자열만 추가한다.
                    else if (talk != null && line.TrimEnd().Length > 0)
                    {
                        //대화명이나 내 이름이 나오면 앞에 구분자를 넣어준다.
                        if (config.OptionValues.FirstNameInputString.Length > 0)
                        {
                            if (line.Contains(config.OptionValues.MyName) || line.Contains(currentTalkerName))
                            {
                                talk.TalkContent = talk.TalkContent + config.OptionValues.FirstNameInputString;
                            }
                        }
                        //내용을 추가한다.
                        talk.TalkContent = talk.TalkContent + line + Global.TooNewLineString;
                    }
                }

                Console.WriteLine("대화 정리가 완료되었습니다.");
                Console.WriteLine();
                Console.WriteLine("파일을 내보낼 준비를 하고 있습니다.");
                Console.WriteLine();

                //상대방 이름과 날짜로 정렬시킨다
                talks.Sort((t1, t2) => t1.TalkerName.CompareTo(t2.TalkerName));
                talks.Sort((t1, t2) => t1.TalkDateTime.CompareTo(t2.TalkDateTime));

                Console.WriteLine("파일 내보내는 중.....");
                Console.WriteLine();

                string SaveFileDirectory = string.Empty;
                //파일 내보내기
                //파일 나뉘는 기준에 따른 저장 처리
                switch (config.OptionValues.FileDivisionUnit)
                {
                    case Global.DivisionNameORTalk.Name:
                        SaveFileDirectory = AppDomain.CurrentDomain.BaseDirectory + "Name\\";

                        //상대방 이름으로 그룹by한 데이터를 얻는다.
                        var names = from NateONTalk t in talks
                                    group t by t.TalkerName into d
                                    select new { Name = d.Key };


                        foreach (var name in names)
                        {
                            NateONTalk talker = new NateONTalk();
                            talker.TalkerName = name.Name;

                            foreach (NateONTalk tk in talks)
                            {
                                //이름이 같으면 내용 추가
                                if (name.Name == tk.TalkerName)
                                {
                                    talker.TalkContent = talker.TalkContent + tk.TalkContent;
                                }
                            }
                            //이름별로 txt 내보내기
                            TXTWrite write = new TXTWrite(SaveFileDirectory, talker.TalkerName + ".txt");
                            write.Write(talker);

                            Console.WriteLine(">>>>>> " + talker.TalkerName + "님의 파일을 저장했습니다.");
                        }
                        break;
                    case Global.DivisionNameORTalk.Talk:
                        SaveFileDirectory = AppDomain.CurrentDomain.BaseDirectory + "Talk\\";

                        foreach (NateONTalk tk in talks)
                        {
                            NateONTalk talker = new NateONTalk();
                            talker.TalkerName = tk.TalkerName;
                            talker.TalkDateTime = tk.TalkDateTime;
                            talker.TalkContent = tk.TalkContent;

                            //이름별로 txt 내보내기
                            TXTWrite write = new TXTWrite(SaveFileDirectory, talker.TalkerName + talker.TalkDateTime.ToString("yyyyMMdd") + ".txt");
                            write.Write(talker);
                            Console.WriteLine(">>>>>> " + talker.TalkerName + talker.TalkDateTime.ToString("yyyyMMdd") + ".txt 파일을 저장했습니다.");
                        }
                        break;
                }

                Console.WriteLine();
                Console.WriteLine(SaveFileDirectory + "에 정리된 파일을 저장했습니다");
                Console.WriteLine();
                Console.WriteLine("작업이 완료되었습니다.");
                Console.WriteLine("아무 키나 누르면 프로그램이 종료됩니다.");
                Console.WriteLine();
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("오류가 일어났습니다.");
                Console.WriteLine("오류가 일어났습니다. 아무 키나 누르면 프로그램이 종료됩니다. 다시 시도해주세요. " + ex.ToString());
                Console.WriteLine();
                Console.ReadKey();
            }

        }
    }
}
