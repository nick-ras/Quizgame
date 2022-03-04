// See https://aka.ms/new-console-template for more information
using System;

namespace Quizgame // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            while (true)
            {
                List<string> list = UIMethods.QuestionAndAnswers();

                Serializer(list);


                if (UIMethods.stopAddingQ() == false)
                {
                    break;
                }
            }
            /*while (true)
            {

                if (UIMethods.AskQ() == "0")
                {
                    count += 1;
                    Console.WriteLine("You guessed correct");
                }
                else
                {
                    Console.WriteLine("Wrong answer");
                }
            }*/
        }
        public static void Serializer(List<string> list)
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(list.GetType());

            var path = @"C:\Users\nick-\Desktop\List.xml";
            using (FileStream file = File.Create(path))
            {
                x.Serialize(file, list);
            }
        }
                
    }
}
