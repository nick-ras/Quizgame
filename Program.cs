// See https://aka.ms/new-console-template for more information
using System;
using System.Xml.Serialization;

namespace Quizgame // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

            List<QAndA> AllQAndA = Deserialize(path);
            int rounds = 0;
            int rightAnswers = 0;
            while (playGame)
            {
                var rand = new Random();
                QAndA QuestionForTheRound = AllQAndA[rand.Next(AllQAndA.Count)];
                
                string answerString = UIMethods.ShowQAndAs(QuestionForTheRound);
                if (!CheckConvertToInt(answerString))
                {
                    continue;
                }
                if (Convert.ToInt32(answerString) > 4)
                {
                    Console.WriteLine("Answer must be between 1-4");
                    continue;
                }
                int answerInt = Convert.ToInt32(answerString);




                if (answerInt == QuestionForTheRound.Correct)
                {
                    Console.WriteLine("You guessed it!");
                    rightAnswers += 1;
                }
                else
                {
                    Console.WriteLine("Wrong answer!");
                }
                rounds += 1;

                //Removed the question, after it has been asked
                AllQAndA.RemoveAt(AllQAndA.IndexOf(QuestionForTheRound));

                if (rounds >= 10 | AllQAndA.Count < 1)
                {
                    playGame = false;
                    Console.WriteLine($"You got {rightAnswers} out of {rounds}!");
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
        public static bool CheckConvertToInt(string answerInString)
        {
            return int.TryParse(answerInString, out _);
        }

    }
}
