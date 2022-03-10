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
                    QAndA QAndAsFromUser = UIMethods.UserInput();
                    QAndA.SetAndHideCorrectAnswer(QAndAsFromUser);

                    if (QAndAsFromUser.CorrectAnswer == null)
                    {
                        UIMethods.DidNotMarkAnswer();
                        continue;
                    }

                    ListOfObjects.Add(QAndAsFromUser);

                    if (UIMethods.stopAddingQ() == false)
                    {
                        typingQandAs = false;
                    }
                }
                QAndA.Serializer(ListOfObjects, path);
            }
            

            List<QAndA> AllQAndA = QAndA.Deserialize(path);
            int rounds = 0;
            int rightAnswers = 0;
            while (playGame)
            {
                bool answerCheckUp = false;
                var rand = new Random();
                QAndA QAndAForTheRound = AllQAndA[rand.Next(AllQAndA.Count)];


                while (!answerCheckUp)
                {
                    string answerString = UIMethods.ShowQAndAs(QAndAForTheRound);

                    if (!QAndA.CheckConvertToInt(answerString))
                    {
                        UIMethods.WrongType();
                        continue;
                    }

                    if (Convert.ToInt32(answerString) > 3 | Convert.ToInt32(answerString) < 0)
                    {
                        UIMethods.TooHighOrLow();
                        continue;
                    }
                    else
                    {
                        answerCheckUp = true;
                    }

                    int answerToIndex = Convert.ToInt32(answerString);

                    if (QAndAForTheRound.AnswersList[answerToIndex] == QAndAForTheRound.CorrectAnswer)
                    {
                        UIMethods.GuessingRight();
                        rightAnswers += 1;
                    }
                    else
                    {
                        UIMethods.WrongGuess();
                    }
                    rounds += 1;
                }
                
                //Removing the question, after it has been asked to user
                AllQAndA.RemoveAt(AllQAndA.IndexOf(QAndAForTheRound));

                if (rounds >= 10 | AllQAndA.Count < 1)
                {                    
                    UIMethods.Score(rightAnswers, rounds);
                    break;
                }
            }
        }
    }
}
