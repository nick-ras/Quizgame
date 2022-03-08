using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizgame
{
    internal class UIMethods
    {
        public static List<string> StringQAndAs()
        {
            List<string> qA = new List<string>();
            Console.WriteLine("Type your question");
            qA.Add (Console.ReadLine());
            Console.WriteLine("Type an answer, if its the correct answer, then put \"*\" at the end");
            qA.Add(Console.ReadLine());
            Console.WriteLine("Type an answer, if its the correct answer, then put \"*\" at the end");
            qA.Add(Console.ReadLine());
            Console.WriteLine("Type an answer, if its the correct answer, then put \"*\" at the end");
            qA.Add(Console.ReadLine());
            Console.WriteLine("Type an answer, if its the correct answer, then put \"*\" at the end");
            qA.Add(Console.ReadLine());

            return qA;
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

            Console.WriteLine($"\nQuestion=>  {AllQAndA.Q}. If {AllQAndA.AnswersList[0]} press 1. If {AllQAndA.AnswersList[1]} press 2. If {AllQAndA.AnswersList[1]} press 3. If {AllQAndA.AnswersList[3]} press 4");
            return Console.ReadLine();
        }
    }
}
