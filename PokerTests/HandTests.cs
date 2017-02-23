using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Tests
{
    [TestClass()]
    public class HandTests
    {
        [TestMethod()]
        public void EvaluateTestReturnsProperHandType()
        {
            // Arrange
            Hand hand = new Hand();

            Card[] cards = new Card[5];
            cards[0] = new Card(2, SuitType.Diamonds);
            cards[1] = new Card(3, SuitType.Diamonds);
            cards[2] = new Card(4, SuitType.Diamonds);
            cards[3] = new Card(5, SuitType.Diamonds);
            cards[4] = new Card(6, SuitType.Diamonds);

            hand.cards = cards;

            var type = hand.Evaluate();
            Assert.AreEqual(HandType.StraightFlush, type);
        }

        [TestMethod()]
        public void ToStringTest()
        {
        }

    }
}