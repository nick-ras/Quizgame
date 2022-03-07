// See https://aka.ms/new-console-template for more information
using System;
using System.Xml.Serialization;

namespace Quizgame // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            bool serialize = true;
            bool deserialize = true;
            
            var path = @"C:\Users\nick-\Desktop\List.xml";
            while (serialize)
            {
                List<string> qA = UIMethods.QuestionAndAnswers();
                QAndA lA = new QAndA();
                lA.Q = qA[0];
                lA.Answer1 = qA[1];
                lA.Answer2 = qA[2];



                Serializer(lA, path);


                if (UIMethods.stopAddingQ() == false)
                {
                    serialize = false;
                }
            }
            while (deserialize)
            {
                QAndA AllQAndA = Deserialize(path);
                UIMethods.ShowQAndAs();
                deserialize = false;
                /*if (UIMethods.AskQ() == "0")
                {
                    count += 1;
                    Console.WriteLine("You guessed correct");
                }
                else
                {
                    Console.WriteLine("Wrong answer");
                }*/
            }
        }
        public static void Serializer(QAndA listOut, string path)
        {
            XmlSerializer x = new XmlSerializer(listOut.GetType());

            
            using (FileStream file = File.Create(path))
            {
                x.Serialize(file, listOut);
            }
        }
        public static QAndA Deserialize(string path)
        {
            QAndA listIn = new QAndA();
            XmlSerializer x = new XmlSerializer(typeof(QAndA));
            using (FileStream file = File.OpenRead(path))
            {
                listIn = x.Deserialize(file) as QAndA;
            }
            return listIn;
        }

    }
}
