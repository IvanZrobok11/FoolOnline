using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Domain.Entities;
using Domain.Logic;
using Shouldly;

namespace UnitTests.FoolUnitTests
{
    public class TableTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TryPutCardTheSameTypeOnTable()
        {
            var table = new Table(SuitCard.Clubs);

            table.TryPutCardOnTable(new Card(TypeCard.Six, SuitCard.Clubs)).ShouldBe(true);
            table.TryPutCardOnTable(new Card(TypeCard.Six, SuitCard.Diamond)).ShouldBe(true);
        }
        [Test]
        public void TryPutCardTheDifferenceTypeOnTable()
        {
            var table = new Table(SuitCard.Clubs);

            table.TryPutCardOnTable(new Card(TypeCard.Six, SuitCard.Clubs)).ShouldBe(true);
            table.TryPutCardOnTable(new Card(TypeCard.Seven, SuitCard.Diamond)).ShouldBe(false);
        }
        [Test]
        public void TryPutTheSameCardOnTable_ReturnsFalse()
        {
            var table = new Table(SuitCard.Clubs);

            table.TryPutCardOnTable(new Card(TypeCard.Six, SuitCard.Diamond)).ShouldBe(true);
            table.TryPutCardOnTable(new Card(TypeCard.Six, SuitCard.Diamond)).ShouldBe(false);
        }
        [Test]
        public void TryPutCardIfCountCardsOnTableEquelsSix()
        {
            var table = new Table(SuitCard.Clubs);

            var cards = new List<Card>
            {
                new Card(TypeCard.Six, SuitCard.Diamond),
                new Card(TypeCard.Six, SuitCard.Spades),
                new Card(TypeCard.Six, SuitCard.Clubs),
                new Card(TypeCard.Six, SuitCard.Hearts)
            };
            table.TryPutCardOnTable(cards[0]).ShouldBe(true);
            table.TryPutCardOnTable(cards[1]).ShouldBe(true);
            table.TryPutCardOnTable(cards[2]).ShouldBe(true);
            table.TryPutCardOnTable(cards[3]).ShouldBe(true);

            table.TryBeatOff(cards[0], new Card(TypeCard.Seven, SuitCard.Diamond)).ShouldBe(true);
            table.TryBeatOff(cards[1], new Card(TypeCard.Seven, SuitCard.Spades)).ShouldBe(true);
            table.TryBeatOff(cards[2], new Card(TypeCard.Seven, SuitCard.Clubs)).ShouldBe(true);
            table.TryBeatOff(cards[3], new Card(TypeCard.Eight, SuitCard.Hearts)).ShouldBe(true);

            table.TryPutCardOnTable(new Card(TypeCard.Eight, SuitCard.Diamond)).ShouldBe(true);
            table.TryPutCardOnTable(new Card(TypeCard.Eight, SuitCard.Spades)).ShouldBe(true);
        }

        [Test]
        public void TryBeatCardOnTable_IfBeaterCardHigher_ResultTrue()
        {
            var trump = SuitCard.Clubs;
            var table = new Table(trump);
            var card = new Card(TypeCard.Six, SuitCard.Diamond);

            table.TryPutCardOnTable(card).ShouldBe(true);
            table.TryBeatOff(card, new Card(TypeCard.Ace, SuitCard.Diamond)).ShouldBe(true);

            table.ClearTable();
            table.TryPutCardOnTable(card).ShouldBe(true);
            table.TryBeatOff(card, new Card(TypeCard.Ace, trump)).ShouldBe(true);
        }
        [Test]
        public void TryBeatCardOnTable_IfBeaterDifferentSuite_ReturnsFalse()
        {
            var table = new Table(SuitCard.Clubs);
            var card = new Card(TypeCard.Six, SuitCard.Diamond);

            table.ClearTable();
            table.TryPutCardOnTable(card).ShouldBe(true);
            table.TryBeatOff(card, new Card(TypeCard.Ace, SuitCard.Hearts)).ShouldBe(false);
        }
        [Test]
        public void TryBeatCardOnTable_IfMoverCardNotHasOnTable_ReturnFalse()
        {
            var table = new Table(SuitCard.Clubs);

            table.ClearTable();
            table.TryPutCardOnTable(new Card(TypeCard.Six, SuitCard.Diamond)).ShouldBe(true);
            table.TryBeatOff(new Card(TypeCard.Seven, SuitCard.Diamond), new Card(TypeCard.Ace, SuitCard.Hearts)).ShouldBe(false);
        }
        [Test]
        public void AllCardsIsBeaten_IfSuccess_ReturnTrue()
        {
            var table = new Table(SuitCard.Clubs);
            var card = new Card(TypeCard.Six, SuitCard.Diamond);

            table.ClearTable();
            table.TryPutCardOnTable(card).ShouldBe(true);
            table.TryBeatOff(card, new Card(TypeCard.Ace, SuitCard.Clubs)).ShouldBe(true);
            table.AllCardsIsBeaten().ShouldBeTrue();
        }
        [Test]
        public void AllCardsIsBeaten_IfUnsuccess_ReturnFalse()
        {
            var table = new Table(SuitCard.Clubs);

            table.ClearTable();
            table.TryPutCardOnTable(new Card(TypeCard.Six, SuitCard.Diamond)).ShouldBe(true);
            table.AllCardsIsBeaten().ShouldBeFalse();
        }

    }
}
