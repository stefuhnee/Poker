using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Deck
    {
        public static List<Card> drawnCards = new List<Card>();
        public static Random randomGen = new Random();

        public static Card DrawCard()
        {
            int rank =  randomGen.Next(2, 15);
            SuitType suit = (SuitType)randomGen.Next(0, 4);

            Card newCard = new Card();
            newCard.rank = rank;
            newCard.suit = suit;

            if (drawnCards.Contains(newCard))
            {
                Console.WriteLine("Found duplicate!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                DrawCard();
            }
            else 
                drawnCards.Add(newCard);

            return newCard;
        }
    }
}
