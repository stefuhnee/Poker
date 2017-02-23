using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get 5 cards
            Hand hand = Get5Cards();
            // Evaluate what kind of hand
            HandType ht1 = hand.Evaluate();
            Console.WriteLine("Hand 1 is a: " + ht1);
    
            // Get 5 other cards
            Hand hand2 = Get5Cards();
            // Evaluate type
            HandType ht2 = hand2.Evaluate();
            Console.WriteLine("Hand 2 is a: " + ht2);

            Console.ReadLine();

            // If types are different, better one wins
            if (ht1 > ht2)
                Console.WriteLine("Hand 1 is better: " + ht1);
            else if (ht1 < ht2)
                Console.WriteLine("Hand 2 is better: " + ht2);
            // If types are same, evaluate with a tie breaker
            else
            {
                var winner = TieBreak(ht1, hand, hand2);
                if (winner == null) Console.WriteLine("Tie!");
                else
                    Console.WriteLine("Winner is: " + winner);
            }

        }

        static string TieBreak(HandType type, Hand hand1, Hand hand2)
        {
            switch(type)
            {
                case HandType.StraightFlush:
                case HandType.Flush:
                case HandType.Straight:
                case HandType.HighCard:
                    return GetHighest(hand1.cards.Last().rank, hand2.cards.Last().rank);
                case HandType.FourOfAKind:
                    return CompareNOfAKind(4, hand1, hand2);
                case HandType.FullHouse:
                    var winner = CompareNOfAKind(3, hand1, hand2);
                    if (winner == null)
                        winner = CompareNOfAKind(2, hand1, hand2);
                    return winner;
                case HandType.ThreeOfAKind:
                    winner = CompareNOfAKind(3, hand1, hand2);
                    if (winner == null)
                        winner = GetHighest(hand1.cards.Last().rank, hand2.cards.Last().rank);
                    return winner;
                case HandType.TwoPair:
                    int handOneLowPair;
                    int handOneHighPair;
                    int handTwoLowPair;
                    int handTwoHighPair;
                    FindPairs(hand1.cards, out handOneLowPair, out handOneHighPair);
                    FindPairs(hand2.cards, out handTwoLowPair, out handTwoHighPair);
                    winner = GetHighest(handOneHighPair, handTwoHighPair);
                    if (winner == null)
                        winner = GetHighest(handOneLowPair, handTwoLowPair);
                    return winner;
                case HandType.Pair:
                    winner = CompareNOfAKind(2, hand1, hand2);
                    if (winner == null)
                        winner = GetHighest(hand1.cards.Last().rank, hand2.cards.Last().rank);
                    return winner;
                
                default:
                    return null;
            }
        }

        static void FindPairs(Card[] cards, out int lowPair, out int highPair)
        {
            lowPair = findRankOfSet(2, cards);
            cards = (Card[])cards.Reverse();
            highPair = findRankOfSet(2, cards);
        }

        static string CompareNOfAKind(int n, Hand hand1, Hand hand2)
        {
            int handOneSetRank = findRankOfSet(n, hand1.cards);
            int handTwoSetRank = findRankOfSet(n, hand2.cards);

            return GetHighest(handOneSetRank, handTwoSetRank);
        }

        static int findRankOfSet(int n, Card[] cards)
        {
            int counter = 1;
            int rank = cards[0].rank;
            for (int i = 1; i < cards.Length; i++)
            {
                if (cards[i].rank == cards[i - 1].rank)
                    counter++;
                else
                    counter = 1;
                if (counter == n)
                {
                    rank = cards[i].rank;
                    break;
                }
            }
            return rank;
        }

        static string GetHighest(int rank1, int rank2)
        {
            Console.WriteLine("Comparing " + rank1 + " and " + rank2);
            if (rank1 > rank2)
                return "Hand1";
            else if (rank1 < rank2)
                return "Hand2";
            else return null;
        }

        static Hand Get5Cards()
        {
            Hand hand = new Hand();
            for (int i = 0; i < 5; i++)
                hand.cards[i] = Deck.DrawCard();
            Console.WriteLine("Confirming, your hand is: " + hand);
            return hand;
        }

        static Card ReadCardFromConsole()
        {
            Card c = new Card();
            // two characters, one is suit, one is rank
            // fill in card, and return it
            // blank line returns null, meaning done
            while (!c.IsValid())
            {
                string s = Console.ReadLine();
                if (s.Length != 2)
                    return null;
                c.SetFromChar(s[0]);
                c.SetFromChar(s[1]);
                if (!c.IsValid())
                {
                    Console.WriteLine("Invalid card, try again");
                    c = new Card();
                }
            }
            return c;
        }
    }
}
