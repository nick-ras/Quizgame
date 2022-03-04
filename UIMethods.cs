using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizgame
{
    internal class UIMethods
    {
        public static List<string> QuestionAndAnswers()
        {
            List<string> qA = new List<string>();
            Console.WriteLine("Type your question");
            qA.Add (Console.ReadLine());
            Console.WriteLine("Type the correct answer");
            qA.Add ($"{Console.ReadLine()}*");
            Console.WriteLine("Type a possible that is not correct");
            qA.Add(Console.ReadLine());

            return qA;
        }
        public static string AskQ(string q, string c, string iC)
        {
            Console.WriteLine($"{q}. If {c} press 0. If {iC} press 1");

            return Console.ReadLine();
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
    }
}
