using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Card : IComparable<Card>, IEquatable<Card>
    {

        public int rank;  // 2-14 (ace high)
        public SuitType suit;  // c, s, h, or d

        public Card() { }

        public Card(int rank, SuitType suit)
        {
            this.rank = rank;
            this.suit = suit;
        }

        public override string ToString()
        {
            return rank + " of " + suit;
        }

        public bool IsValid()
        {
            return rank != 0 && 
                (suit == SuitType.Clubs 
                || suit == SuitType.Hearts 
                || suit == SuitType.Spades 
                || suit == SuitType.Diamonds);
        }

        public void SetFromChar(char ch)
        {
            // case sensitive sucks
            ch = char.ToUpper(ch);
            // 2-9 are easy
            if (ch >= '2' && ch <= '9')
                rank = ch - '0';
            // t,j,q,k,a are random, so special case
            else if (ch == 'T')
                rank = 10;
            else if (ch == 'J')
                rank = 11;
            else if (ch == 'Q')
                rank = 12;
            else if (ch == 'K')
                rank = 13;
            else if (ch == 'A')
                rank = 14;
            // TODO Q, K, A
            // everything else is a suit
            else if (ch == 'S')
                suit = SuitType.Spades;
            else if (ch == 'C')
                suit = SuitType.Clubs;
            else if (ch == 'D')
                suit = SuitType.Diamonds;
            else if (ch == 'H')
                suit = SuitType.Hearts;
        }

        public int CompareTo(Card other)
        {
            return this.rank - other.rank;
        }

        public bool Equals(Card other)
        {
            if (this.rank == other.rank && this.suit == other.suit)
                return true;
            else
                return false;
        }
    }


}
