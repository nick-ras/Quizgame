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

            List<QAndA> ListOfObjects = new List<QAndA>();

            //If QAndA list is already made, and you want to go straight to the game
            // then the follow while loop + Serializer object can be skipped.
            while (typingQandAs)
            {
                List<string> userInputQAndAs = UIMethods.UserInput();
                var quizObject = new QAndA();
                quizObject.Question = userInputQAndAs[0];
                QAndA.SetAndHideCorrectAnswer(userInputQAndAs, quizObject);
                
                //if La.Correct has not changed from starting value, loop will continue
                if (quizObject.IndexRightAnswer == 0)
                {
                    UIMethods.DidNotMarkAnswer();
                    continue;
                }
                quizObject.AnswersList.Add(userInputQAndAs[1]);
                quizObject.AnswersList.Add(userInputQAndAs[2]);
                quizObject.AnswersList.Add(userInputQAndAs[3]);
                quizObject.AnswersList.Add(userInputQAndAs[4]);

                //Adding object to list of objects
                ListOfObjects.Add(quizObject);

                if (UIMethods.stopAddingQ() == false)
                {
                    typingQandAs = false;
                }
            }

            QAndA.Serializer(ListOfObjects, path);

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

                    if (!QAndA.CheckConvertToInt(answerString))
                    {
                        UIMethods.WrongType();
                        continue;
                    }
                    else
                    {
                        correctAnswer = true;
                    }
                    if (Convert.ToInt32(answerString) > 4 && Convert.ToInt32(answerString) < 0)
                    {
                        UIMethods.TooHighOrLow();
                        continue;
                    }
                    else
                    {
                        correctAnswer = true;
                    }

                    int answerInt = Convert.ToInt32(answerString);

                    if (answerInt == QuestionForTheRound.IndexRightAnswer)
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
                AllQAndA.RemoveAt(AllQAndA.IndexOf(QuestionForTheRound));

                if (rounds >= 10 | AllQAndA.Count < 1)
                {                    
                    UIMethods.Score(rightAnswers, rounds);
                    break;
                }
            }
        }
    }
}
