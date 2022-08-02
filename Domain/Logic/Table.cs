using Domain.Entities;
using Domain.Enums;
using Domain.Extension;

namespace Domain.Logic
{
    public class Table : ITable
    {
        public Table(SuitCard trump)
        {
            moverToBeaterCard = new Dictionary<Card, Card>(6);
            Trump = trump;
        }

        private readonly SuitCard Trump;
        public Dictionary<Card, Card> moverToBeaterCard;
        public bool TryBeatOff(Card cardOnTable, Card cardToBeat)
        {
            if (!moverToBeaterCard.ContainsKey(cardOnTable))
            {
                return false;
            }
            if (moverToBeaterCard.ContainsKey(cardToBeat) || moverToBeaterCard.ContainsValue(cardToBeat))
            {
                return false;
            }

            if (cardOnTable.Suit != cardToBeat.Suit && cardToBeat.Suit != Trump)
            {
                return false;
            }
            var cardOnTableIndex = (byte)cardOnTable.Type + (cardOnTable.IsTrump(Trump) ? (byte)TypeCard.Ace : 0);
            var beaterCardIndex = (byte)cardToBeat.Type + (cardToBeat.IsTrump(Trump) ? (byte)TypeCard.Ace : 0);
            if (cardOnTableIndex < beaterCardIndex)
            {
                moverToBeaterCard[cardOnTable] = cardToBeat;
                return true;
            }

            return false;
        }
        public bool TryPutCardOnTable(Card card)
        {
            if (moverToBeaterCard.IsNullOrEmpty())
            {
                moverToBeaterCard[card] = default;
                return true;
            }
            if (moverToBeaterCard.Keys.Count == 6)
            {
                return false;
            }
            if (moverToBeaterCard.ContainsKey(card) || moverToBeaterCard.ContainsValue(card))
            {
                return false;
            }
            if (moverToBeaterCard.Keys.Any(k => k.Type == card.Type) || moverToBeaterCard.Values.Any(v => v.Type == card.Type))
            {
                moverToBeaterCard[card] = default;
                return true;
            }
            return false;
        }
        public bool AllCardsIsBeaten()
        {
            if (!moverToBeaterCard.IsKeyWithoutValue())
            {
                return true;
            }

            return false;
        }
        public void ClearTable()
        {
            moverToBeaterCard.Clear();
            moverToBeaterCard = new Dictionary<Card, Card>(6);
        }
    }

    public interface ITable
    {
        void ClearTable();
    }
}
