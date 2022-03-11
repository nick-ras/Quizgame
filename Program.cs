// See https://aka.ms/new-console-template for more information
using System;
using System.Xml.Serialization;

namespace Quizgame // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool typingQandAs = true;
            bool playGame = true;

            //remember to update path when needed
            var path = @"C:\Users\nick-\Desktop\List.xml";
            
            if (!File.Exists(path))
            {
                List<QAndA> ListOfObjects = new List<QAndA>();
                while (typingQandAs)
                {
                    QAndA QAndAFromUser = UIMethods.UserInput();

                    QAndAFromUser.ListCorrectAnswers = IndexCorrectAnswer(QAndAFromUser);

                    for (int i = 0; i < QAndAFromUser.ListCorrectAnswers.Count; i++)
                    {
                        QAndAFromUser.AnswersList[QAndAFromUser.ListCorrectAnswers[i]] = QAndAFromUser.AnswersList[QAndAFromUser.ListCorrectAnswers[i]].Replace("*", "");
                    }

                    if (QAndAFromUser.ListCorrectAnswers == null)
                    {
                        UIMethods.DidNotMarkAnswer();
                        continue;
                    }

                    ListOfObjects.Add(QAndAFromUser);

                    if (!UIMethods.continueAddingQ())
                    {
                        typingQandAs = false;
                    }
                }
                Serializer(ListOfObjects, path);
            }
            

            List<QAndA> AllQAndA = Deserialize(path);
            int roundsPlayed = 0;
            int rightAnswers = 0;
            while (playGame)
            {
                bool answerCheckUp = false;
                var rand = new Random();
                QAndA QAndAForTheRound = AllQAndA[rand.Next(AllQAndA.Count)];
                bool won = false;

                while (!answerCheckUp)
                {
                    string answerString = UIMethods.ShowQAndAs(QAndAForTheRound);

                    if (!CheckConvertToInt(answerString))
                    {
                        UIMethods.WrongType();
                        continue;
                    }

                    int answerToIndex = Convert.ToInt32(answerString);

                    if (answerToIndex > 3 | answerToIndex < 0)
                    {
                        UIMethods.TooHighOrLow();
                        continue;
                    }
                    else
                    {
                        answerCheckUp = true;
                    }

                    roundsPlayed += 1;

                    if (AnswerIsCorrect(answerToIndex, QAndAForTheRound))
                    {
                        UIMethods.GuessingRight();
                        rightAnswers += 1;
                        break;
                    }
                    else
                    {
                        UIMethods.WrongGuess();
                    }
                }
                
                //Removing the question, after it has been asked to user
                AllQAndA.RemoveAt(AllQAndA.IndexOf(QAndAForTheRound));

                if (roundsPlayed >= 10 || AllQAndA.Count < 1)
                {                    
                    UIMethods.Score(rightAnswers, roundsPlayed);
                    break;
                }
            }
        }
        public static bool CheckConvertToInt(string answerInString)
        {
            return int.TryParse(answerInString, out _);
        }
        public static void Serializer(List<QAndA> listToXML, string path)
        {
            XmlSerializer x = new XmlSerializer(listToXML.GetType());


            using (FileStream file = File.Create(path))
            {
                x.Serialize(file, listToXML);
            }
        }
        public static List<QAndA> Deserialize(string path)
        {
            XmlSerializer x = new XmlSerializer(typeof(List<QAndA>));
            using (FileStream file = File.OpenRead(path))
            {
                List<QAndA> getXMLList = x.Deserialize(file) as List<QAndA>;
                return getXMLList;
            }

        }

        public static List<int> IndexCorrectAnswer(QAndA QAndAsFromUser)
        {
            List<int> IndexesCorrectAnswers = new List<int>();
            for (int j = 0; j < QAndAsFromUser.AnswersList.Count; j++)
            {
                if (QAndAsFromUser.AnswersList[j].Contains("*"))
                {
                    IndexesCorrectAnswers.Add(j);
                }
            }
            return IndexesCorrectAnswers;
        }
        public static bool AnswerIsCorrect(int answerToIndex, QAndA QAndAForTheRound)
        {
            bool won = false;
            for (int i = 0; i < QAndAForTheRound.ListCorrectAnswers.Count; i++)
            {
                if (QAndAForTheRound.ListCorrectAnswers[i] == answerToIndex)
                {
                    won = true;
                }  
            }
            return won;
        }
    }
}
