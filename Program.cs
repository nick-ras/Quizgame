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
            bool playGame = true;
            
            var path = @"C:\Users\nick-\Desktop\List.xml";
            while (serialize)
            {
                var star = "*";
                List<string> qA = UIMethods.QuestionAndAnswers();
                QAndA lA = new QAndA();
                lA.Q = qA[0];
                for (int i = 1; i < qA.Count; i++)
                {
                    
                    if (qA[i].Contains(star))
                    {
                        lA.Correct = i;
                        qA[i] = qA[i].Replace(star, "");
                    }
                }
                lA.Answer1 = qA[1];
                lA.Answer2 = qA[2];
                lA.Answer3 = qA[3];
                lA.Answer4 = qA[4];
                Serializer(lA, path);


                if (UIMethods.stopAddingQ() == false)
                {
                    serialize = false;
                }
            }

            while (playGame)
            {
                int rounds = 0;
                int counter = 0;
                QAndA AllQAndA = Deserialize(path);
                
                int answer = Convert.ToInt32(UIMethods.ShowQAndAs(AllQAndA));

                if (answer == AllQAndA.Correct)
                {
                    Console.WriteLine("You guessed it!");
                    counter += 1;
                }
                rounds += 1;
                if (rounds >= 10)
                {
                    playGame = false;
                }
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
