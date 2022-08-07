namespace UnitTests.FoolUnitTests
{
    public class FoolSessionTest : PlayerTest
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
    }
}
