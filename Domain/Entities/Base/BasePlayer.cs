using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities.Base
{
    public abstract class BasePlayer
    {
        public BasePlayer()
        {
            _cards = new List<Card>();
        }

        public IEnumerable<Card> Cards => _cards;
        protected List<Card> _cards;
        public int CardsCount => _cards.Count;
        public void TakeCard(Card card)
        {
            if (_cards.Contains(card))
            {
                throw new Exception();
            }
            _cards.Add(card);
        }
        public Card PutCard(Card card)
        {
            if (!HasPlayerCard(card))
                throw new ArgumentException("Card is not found");
            _cards.Remove(card);
            return card;
        }
        public bool HasPlayerCard(TypeCard type, SuitCard suit)
        {
            return _cards.Any(c => c == new Card(type, suit));
        }
        public bool HasPlayerCard(Card card)
        {
            return _cards.Any(c => c == card);
        }
    }
}
