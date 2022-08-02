using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;

namespace UnitTests.DeckTests
{
    public class Deck36Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DeckCreations_AssertThatDeckIsBeFill()
        {
           var deck = new Deck36();
           deck.Count.ShouldBe(36);
        }
        [Test]
        public void DeckCreations_AssertThatDeckNotContainsTheSameCards()
        {
            var deck = new Deck36();
            var card1 = new Card(TypeCard.Six, SuitCard.Diamond);
            var card2 = new Card(TypeCard.Six, SuitCard.Clubs);
            var card3 = new Card(TypeCard.Six, SuitCard.Hearts);
            var card4 = new Card(TypeCard.Six, SuitCard.Spades);
            var card5 = new Card(TypeCard.Ace, SuitCard.Diamond);
            var card6 = new Card(TypeCard.Ace, SuitCard.Clubs);
            var card7 = new Card(TypeCard.Ace, SuitCard.Spades);
            var card8 = new Card(TypeCard.Ace, SuitCard.Hearts);

            deck.Count.ShouldBe(36);

            deck.Count(c => c == card1).ShouldBe(1);
            deck.Count(c => c == card2).ShouldBe(1);
            deck.Count(c => c == card3).ShouldBe(1);
            deck.Count(c => c == card4).ShouldBe(1);
            deck.Count(c => c == card5).ShouldBe(1);
            deck.Count(c => c == card6).ShouldBe(1);
            deck.Count(c => c == card7).ShouldBe(1);
            deck.Count(c => c == card8).ShouldBe(1);
        }
        [Test]
        public void DeckCreations_AssertThatDeckShuffleCorrectly()
        {
            var deck = new Deck36();
            var card1 = new Card(TypeCard.Six, SuitCard.Diamond);
            var card2 = new Card(TypeCard.Six, SuitCard.Clubs);
            var card3 = new Card(TypeCard.Six, SuitCard.Hearts);
            var card4 = new Card(TypeCard.Six, SuitCard.Spades);
            var card5 = new Card(TypeCard.Ace, SuitCard.Diamond);
            var card6 = new Card(TypeCard.Ace, SuitCard.Clubs);
            var card7 = new Card(TypeCard.Ace, SuitCard.Spades);
            var card8 = new Card(TypeCard.Ace, SuitCard.Hearts);

            deck.ShuffleCards();
            deck.Count.ShouldBe(36);

            deck.Count(c => c == card1).ShouldBe(1);
            deck.Count(c => c == card2).ShouldBe(1);
            deck.Count(c => c == card3).ShouldBe(1);
            deck.Count(c => c == card4).ShouldBe(1);
            deck.Count(c => c == card5).ShouldBe(1);
            deck.Count(c => c == card6).ShouldBe(1);
            deck.Count(c => c == card7).ShouldBe(1);
            deck.Count(c => c == card8).ShouldBe(1);
        }
    }
}
