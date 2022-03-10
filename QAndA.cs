using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Quizgame
{
    public class QAndA
    {
        public string Question;
        public List<string> AnswersList = new List<string>();
        //public int IndexRightAnswer;
        public string CorrectAnswer;

        //not sure if this method should be here
        public static bool CheckConvertToInt(string answerInString)
        {
            return int.TryParse(answerInString, out _);
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
        public static void SetAndHideCorrectAnswer(QAndA qA)
        {
            var star = "*";
            for (int j = 0; j < qA.AnswersList.Count; j++)
            {
                if (qA.AnswersList[j].Contains(star))
                {
                    qA.AnswersList[j] = qA.AnswersList[j].Replace(star, "");

                    qA.CorrectAnswer = qA.AnswersList[j];
                }
            }
        }

        public override string ToString()
        {
            return $"{Question}";
        }
    }
}
