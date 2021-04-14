using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardProcessing
{
    public class CardRecognitionResult
    {
        public IList<Card> cards = null;

        public CardRecognitionResult()
        {
            cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            if (cards == null)
            {
                cards = new List<Card>();
                cards.Add(card);
            }
            else
            {
                cards.Add(card);
            }
        }
    }
    public class Card
    {
        public string RegistrationNumber { get; set; }
        public string TextNumber { get; set; }
        public string Day { get; set; }
        public string Lang { get; set; }
        public IList<QuestionAnswer> questions = null;

        public Card()
        {            
        }

        public void AddQuestionAnswer(QuestionAnswer questionAnswer)
        {
            if (questions == null)
            {
                questions = new List<QuestionAnswer>();
                questions.Add(questionAnswer);
            }
            else
            {
                questions.Add(questionAnswer);
            }
        }
    }
    public class QuestionAnswer
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
