using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizgame
{
    internal class UIMethods
    {
        /// <summary>
        /// Prompt user for a question, and saves it to an new objects. aftewards it  prompts user for potential answers in a loop that loop 4 times.
        /// </summary>
        /// <returns>A QAndaA object with a Question var and a AnswersList with 4 answers</returns>
        public static QAndA UserInput()
        {
            QAndA qAndAObject = new QAndA();
            Console.WriteLine("Type your question");
            qAndAObject.Question = Console.ReadLine();
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Type an answer, if its a correct one, then put \"*\" at the end");
                string answer = Console.ReadLine();
                if (answer.Contains('*'))
                {
                    qAndAObject.ListCorrectAnswers.Add(i);
                }
                qAndAObject.AnswersList.Add(answer.Replace("*", ""));
            }
            return qAndAObject;
        }

        public static void DidNotMarkCorrectAnswer()
        {
            Console.WriteLine("You didnt mark the correct answer with \"*\"");
        }
        public static bool ContinueAddingQ()
        {
            Console.WriteLine("Press <Enter> to continue, if you wish to add more questions");
            var playAgain = Console.ReadKey();
            if (playAgain.Key != ConsoleKey.Enter)
            {
                return false;
            }
            return true;
        }
        public static string ShowQAndAs(QAndA AllQAndA)
        {

            Console.WriteLine($"\nQUESTIONTIME******************************\nQuestion=>  {AllQAndA.Question}. If {AllQAndA.AnswersList[0]} press 0. If {AllQAndA.AnswersList[1]} press 1. If {AllQAndA.AnswersList[1]} press 2. If {AllQAndA.AnswersList[3]} press 3");
            return Console.ReadLine();
        }
        public static void WrongType()
        {
            Console.WriteLine("Answer must be a whole number");
        }
        public static void TooHighOrLow()
        {
            Console.WriteLine("Answer must be between 1-4");
        }
        public static void Score(int rightAnswers, int rounds)
        {
            Console.WriteLine($"You got {rightAnswers} out of {rounds}!");
        }
        public static void GuessingRight()
        {
            Console.WriteLine("You guessed it!");
        }
        public static void WrongGuess()
        {
            Console.WriteLine("Wrong answer!");
        }
    }
}
