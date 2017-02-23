using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Hand
    {
        public Card[] cards = new Card[5];

        public override string ToString()
        {
            string s = "";
            foreach (Card c in cards)
                s += c.ToString() + ", ";
            return s;
        }

        public HandType Evaluate()
        {
            // Sort cards for easier evaluation
            Array.Sort(cards);
            // Am I a flush?
            bool flush = IsFlush();
            // Am I a straight?
            bool straight = isStraight();
            // Do I have pairs? Or more of a kind?
            int pairs = CountPairs();
            int mostOfAKind = MostOfAKind();
            Console.WriteLine("Pairs: " + pairs);
            Console.WriteLine("Most of a kind: " + mostOfAKind);
            Console.WriteLine("Is straight?: " + straight);
            Console.WriteLine("Is a flush?: " + flush);

            if (straight && flush)
            {
                return HandType.StraightFlush;
            }
            if (mostOfAKind == 4)
                return HandType.FourOfAKind;
            if (pairs == 2 && mostOfAKind == 3)
                return HandType.FullHouse;
            if (flush)
                return HandType.Flush;
            if (straight)
                return HandType.Straight;
            if (mostOfAKind == 3)
                return HandType.ThreeOfAKind;
            if (pairs == 2)
                return HandType.TwoPair;
            if (pairs == 1)
                return HandType.Pair;
            else return HandType.HighCard;
        }

        private bool isStraight()
        {
            // If the first value is a 2 and final value is an ace, place the ace first and shift the elements of the array over.
            if (cards[0].rank == 2 && cards[cards.Length - 1].rank == 14)
            {
                Card last = cards[cards.Length - 1];
                last.rank = 1;
                for (int i = cards.Length - 1; i > 0; i--)
                    cards[i] = cards[i - 1];
                cards[0] = last;

                for (int i = 0; i < cards.Length; i++)
                    Console.WriteLine("Card in hand: " + cards[i].rank);
            }

            for (int i = 0; i < cards.Length - 1; i++)
            {
                if (cards[i + 1].rank == cards[i].rank + 1)
                    continue;
                return false;
            }
            return true;
        }

        private int CountPairs()
        {
            int pairs = 0;
            bool onPair = false;
            for (int i = 0; i < cards.Length - 1; i++)
            {
                if (cards[i].rank == cards[i + 1].rank && onPair == false)
                {
                    onPair = true;
                    pairs++;
                }
                else if (cards[i].rank != cards[i + 1].rank)
                    onPair = false;
            }
            return pairs;
        }

        private int MostOfAKind()
        {
            int most = 1;
            int currentMost = 1;
            for (int i = 1; i < cards.Length; i++)
            {
                if (cards[i].rank == cards[i - 1].rank)
                {
                    currentMost++;
                    if (currentMost > most)
                        most = currentMost;
                }
                else currentMost = 1;
            }
            return most;
        }

        /// <summary>
        /// Are all cards in the hand the same suit
        /// </summary>
        private bool IsFlush()
        {
            // iterate through card array, 
            // checking each card's suit against 1st card
            SuitType suit = cards[0].suit;
            for (int i = 1; i < cards.Length; i++)
            {
                if (cards[i].suit != suit)
                    return false;
            }
            return true;
        }
    }
}
