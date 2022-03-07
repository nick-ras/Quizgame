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
            var QAndAList = new List<QAndA>();

            while (serialize)
            {
                var star = "*";
                List<string> qA = UIMethods.QuestionAndAnswers();
                var lA = new QAndA();
                lA.Q = qA[0];
                for (int j = 1; j < qA.Count; j++)
                {

                    if (qA[j].Contains(star))
                    {
                        lA.Correct = j;
                        qA[j] = qA[j].Replace(star, "");
                    }
                }
                lA.Answer1 = qA[1];
                lA.Answer2 = qA[2];
                lA.Answer3 = qA[3];
                lA.Answer4 = qA[4];
                QAndAList.Add(lA);

                if (UIMethods.stopAddingQ() == false)
                {
                    serialize = false;
                }
            }

            Serializer(QAndAList, path);

            while (playGame)
            {
                int rounds = 0;
                int rightAnswers = 0;
                List<QAndA> alreadyAnswered = new List<QAndA>();
                List<QAndA> AllQAndA = Deserialize(path);
                var rand = new Random();
                QAndA QuestionForTheRound = AllQAndA[rand.Next(AllQAndA.Count)];
                if (alreadyAnswered.Contains(QuestionForTheRound))
                {
                    continue;
                }
                int answer = Convert.ToInt32(UIMethods.ShowQAndAs(QuestionForTheRound));

                if (answer == QuestionForTheRound.Correct)
                {
                    Console.WriteLine("You guessed it!");
                    rightAnswers += 1;
                    continue;
                }
                else
                {
                    Console.WriteLine("Wrong answer!");
                    continue;
                }
                rounds += 1;
                alreadyAnswered.Add(QuestionForTheRound);
                if (rounds >= 10)
                {
                    playGame = false;
                    Console.WriteLine($"You got {rightAnswers} out of 10!");
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
