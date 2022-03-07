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
                var QAndAList = new List<QAndA>();
                for (int i = 0; i < 10; i++)
                {
                    var star = "*";
                    List<string> qA = UIMethods.QuestionAndAnswers();
                    var lA = new QAndA();
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
                    QAndAList.Add(lA);
                }
                
                Serializer(QAndAList, path);


                if (UIMethods.stopAddingQ() == false)
                {
                    serialize = false;
                }
            
            }

            while (playGame)
            {
                int rounds = 0;
                int counter = 0;
                List<QAndA> AllQAndA = Deserialize(path);
                var rand = new Random();
                QAndA QuestionForTheRound = AllQAndA[rand.Next(AllQAndA.Count)];

                int answer = Convert.ToInt32(UIMethods.ShowQAndAs(QuestionForTheRound));

                if (answer == QuestionForTheRound.Correct)
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
        public static void Serializer(List<QAndA> listToXML, string path)
        {
            XmlSerializer x = new XmlSerializer(listToXML.GetType());

            
            using (FileStream file = File.Create(path))
            {
                x.Serialize(file, listToXML);
            }
        }
        public static List<QAndA> Deserialize(string path)
        {
            XmlSerializer x = new XmlSerializer(typeof(List<QAndA>));
            using (FileStream file = File.OpenRead(path))
            {
                List<QAndA> getXMLList = x.Deserialize(file) as List<QAndA>;
                return getXMLList;
            }
            
        }

    }
}
