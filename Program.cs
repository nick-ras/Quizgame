// See https://aka.ms/new-console-template for more information
using System;
using System.Xml.Serialization;

namespace Quizgame // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool makingQuestions = true;
            bool playGame = true;
            
            var path = @"C:\Users\nick-\Desktop\List.xml";
            var QAndAList = new List<QAndA>();

            //If QAndA list is already made, and you want to go straight to the game
            // then the follow while loop + Serializer object can be skipped.
            while (makingQuestions)
            {
                List<string> qA = UIMethods.StringQAndAs();
                var lA = new QAndA();
                lA.Q = qA[0];
                QAndA.SetAndHideCorrectAnswer(qA, lA);
                
                //if La.Correct has not changed from starting value, loop will continue
                if (lA.IndexOfCorrectA == 0)
                {
                    Console.WriteLine("You didnt mark the correct answer with \"*\"");
                    continue;
                }
                lA.AnswersList[0] = qA[1];
                lA.AnswersList[1] = qA[2];
                lA.AnswersList[2] = qA[3];
                lA.AnswersList[3] = qA[4];
                QAndAList.Add(lA);

                if (UIMethods.stopAddingQ() == false)
                {
                    makingQuestions = false;
                }
            }

            QAndA.Serializer(QAndAList, path);

            List<QAndA> AllQAndA = QAndA.Deserialize(path);
            int rounds = 0;
            int rightAnswers = 0;
            while (playGame)
            {
                var rand = new Random();
                QAndA QuestionForTheRound = AllQAndA[rand.Next(AllQAndA.Count)];
                
                string answerString = UIMethods.ShowQAndAs(QuestionForTheRound);
                if (!CheckConvertToInt(answerString))
                {
                    continue;
                }
                if (Convert.ToInt32(answerString) > 4)
                {
                    Console.WriteLine("Answer must be between 1-4");
                    continue;
                }
                int answerInt = Convert.ToInt32(answerString);

                if (answerInt == QuestionForTheRound.IndexOfCorrectA)
                {
                    Console.WriteLine("You guessed it!");
                    rightAnswers += 1;
                }
                else
                {
                    Console.WriteLine("Wrong answer!");
                }
                rounds += 1;

                //Removing the question, after it has been asked to user
                AllQAndA.RemoveAt(AllQAndA.IndexOf(QuestionForTheRound));

                if (rounds >= 10 | AllQAndA.Count < 1)
                {
                    playGame = false;
                    Console.WriteLine($"You got {rightAnswers} out of {rounds}!");
                }
            }
        }
        
        public static bool CheckConvertToInt(string answerInString)
        {
            return int.TryParse(answerInString, out _);
        }
    }
}
