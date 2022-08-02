using Domain.Enums;

namespace Domain.Entities.Base
{
    public abstract class Deck : Stack<Card>
    {
        protected Deck(int capacity) : base(capacity)
        {
            _cardsCount = capacity;
            FillDeck();
        }

        protected abstract void FillDeck();
        private readonly int _cardsCount;
        public static SuitCard ChooseTrump()
        {
            var random = new Random();
            return (SuitCard)random.Next(1, 4);
        }
        public void ShuffleCards()
        {
            var random = new Random();
            var tempList = this.ToList();

            for (int i = 0; i < tempList.Count; i++)
            {
                var index1 = random.Next(1, tempList.Count);
                var index2 = random.Next(1, tempList.Count);

                (tempList[index1], tempList[index2]) = (tempList[index2], tempList[index1]);
            }

            Clear();
            foreach (var card in tempList)
            {
                Push(card);
            }
        }

        public void Refresh()
        {
            if (Count != _cardsCount)
            {
                Clear();
                FillDeck();
            }
            ShuffleCards();
        }
    }
}
