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
        public string Q;
        public List<string> AnswersList = new List<string>();
        public int IndexOfCorrectA;
        public string CorrectAString;

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
        public static void SetAndHideCorrectAnswer(List<string> qA, QAndA lA)
        {
            var star = "*";
            for (int j = 1; j < qA.Count; j++)
            {
                if (qA[j].Contains(star))
                {
                    lA.CorrectAString = qA[j];
                    lA.IndexOfCorrectA = j;
                    qA[j] = qA[j].Replace(star, "");
                }
            }
        }

        public override string ToString()
        {
            return $"{Q}";
        }
    }
}
