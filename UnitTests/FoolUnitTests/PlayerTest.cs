using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Domain.Entities;
using Shouldly;
using Test.Common;

namespace UnitTests.FoolUnitTests
{
    public class PlayerTest : BaseTestPlayer
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PutCard_PlayerHasRemoveThisCard()
        {
            var player = new FoolPlayer(UserId.ToString());
            var card1 = new Card(TypeCard.Eight, SuitCard.Diamond);
            var card2 = new Card(TypeCard.Eight, SuitCard.Diamond);

            player.Cards.ToList().Count.ShouldBe(0);
            player.TakeCard(card1);
            player.Cards.ToList().Count.ShouldBe(1);
            player.Cards.First(c => c.Equals(card1));

            player.PutCard(card1);
            player.Cards.ToList().Count.ShouldBe(0);
        }
        [Test]
        public void TakeCard_PlayerHasRemoveThisCard()
        {
            var player = new FoolPlayer(UserId.ToString());
            var card1 = new Card(TypeCard.Eight, SuitCard.Diamond);
            var card2 = new Card(TypeCard.Eight, SuitCard.Clubs);

            player.Cards.ToList().Count.ShouldBe(0);
            player.TakeCard(card1);
            player.TakeCard(card2);
            player.HasPlayerCard(card1).ShouldBeTrue();
            player.HasPlayerCard(TypeCard.Eight, SuitCard.Clubs).ShouldBeTrue();
            player.Cards.ToList().Count.ShouldBe(2);
        }
        [Test]
        public void TakeCard_IfPlayerHasTheSameCard_WillBeError()
        {
            var player = new FoolPlayer(UserId.ToString());
            var card1 = new Card(TypeCard.Eight, SuitCard.Diamond);
            var card2 = new Card(TypeCard.Eight, SuitCard.Diamond);

            player.WithdrawCards(card1, card2);
            player.CardsCount.ShouldBe(2);
        }
    }
}
