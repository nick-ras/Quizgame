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

            //remember to update
            var path = @"C:\Users\nick-\Desktop\List.xml";

            List<QAndA> QAndAList = new List<QAndA>();

            //If QAndA list is already made, and you want to go straight to the game
            // then the follow while loop + Serializer object can be skipped.
            while (typingQandAs)
            {
                List<string> userInputQAndAs = UIMethods.StringQAndAs();
                var quizObject = new QAndA();
                quizObject.Question = userInputQAndAs[0];
                QAndA.SetAndHideCorrectAnswer(userInputQAndAs, quizObject);
                
                //if La.Correct has not changed from starting value, loop will continue
                if (quizObject.IndexOfCorrectA == 0)
                {
                    UIMethods.DidNotMarkAnswer();
                    continue;
                }
                quizObject.AnswersList.Add(userInputQAndAs[1]);
                quizObject.AnswersList.Add(userInputQAndAs[2]);
                quizObject.AnswersList.Add(userInputQAndAs[3]);
                quizObject.AnswersList.Add(userInputQAndAs[4]);

                //Adding object to list of objects
                QAndAList.Add(quizObject);

                if (UIMethods.stopAddingQ() == false)
                {
                    typingQandAs = false;
                }
            }

            QAndA.Serializer(QAndAList, path);

            List<QAndA> AllQAndA = QAndA.Deserialize(path);
            int rounds = 0;
            int rightAnswers = 0;
            while (playGame)
            {
                bool correctAnswer = false;
                var rand = new Random();
                QAndA QuestionForTheRound = AllQAndA[rand.Next(AllQAndA.Count)];
                
                
                while (!correctAnswer)
                {
                    string answerString = UIMethods.ShowQAndAs(QuestionForTheRound);

                    if (!CheckConvertToInt(answerString))
                    {
                        Console.WriteLine("Answer must be a whole number");
                        continue;
                    }
                    else
                    {
                        correctAnswer = true;
                    }
                    if (Convert.ToInt32(answerString) > 4 && Convert.ToInt32(answerString) < 0)
                    {
                        Console.WriteLine("Answer must be between 1-4");
                    }
                    else
                    {
                        correctAnswer = true;
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
                }
                
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
