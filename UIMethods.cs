using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizgame
{
    internal class UIMethods
    {
        public static List<string> UserInput()
        {
            List<string> qA = new List<string>();
            Console.WriteLine("Type your question");
            qA.Add (Console.ReadLine());
            Console.WriteLine("Type an answer, if its a correct one, then put \"*\" at the end");
            qA.Add(Console.ReadLine());
            Console.WriteLine("Type an answer, if its a correct one, then put \"*\" at the end");
            qA.Add(Console.ReadLine());
            Console.WriteLine("Type an answer, if its a correct one, then put \"*\" at the end");
            qA.Add(Console.ReadLine());
            Console.WriteLine("Type an answer, if its a correct one, then put \"*\" at the end");
            qA.Add(Console.ReadLine());

            return qA;
        }
        public static void DidNotMarkAnswer()
        {
            Console.WriteLine("You didnt mark the correct answer with \"*\"");
        }
        public static bool stopAddingQ()
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

            Console.WriteLine($"\nQUESTIONTIME******************************\nQuestion=>  {AllQAndA.Question}. If {AllQAndA.AnswersList[0]} press 1. If {AllQAndA.AnswersList[1]} press 2. If {AllQAndA.AnswersList[1]} press 3. If {AllQAndA.AnswersList[3]} press 4");
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
