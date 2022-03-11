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
        public List<int> ListCorrectAnswers = new List<int>();

        public override string ToString()
        {
            return $"{Question}";
        }
    }
}
