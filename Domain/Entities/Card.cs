using Domain.Enums;

namespace Domain.Entities
{
    public struct Card
    {
        public TypeCard Type { get; }
        public SuitCard Suit { get; }
        public override int GetHashCode()
        {
            return Tuple.Create(Type, Suit).GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            return obj is Card && this == (Card)obj;
        }
        public Card(TypeCard type, SuitCard suit)
        {
            Type = type;
            Suit = suit;
        }
        public bool IsTrump(SuitCard tramp)
        {
            return Suit == tramp;
        }

        //public int CardIndex(bool isTrump)
        //{
        //    return (int)Type 
        //}
        public static bool operator ==(Card x, Card y)
        {
            return x.Type == y.Type && x.Suit == y.Suit;
        }

        public static bool operator !=(Card x, Card y)
        {
            return !(x == y);
        }

        public override String ToString()
        {
            return String.Format("({0}, {1})", (int)Type, (int)Suit);
        }
    }
}
