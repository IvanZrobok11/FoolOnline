using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Base
{
    public class Deck36 : Deck
    {
        public Deck36() : base(36)
        {
            
        }
        protected override void FillDeck()
        {
            for (int i = 1; i <= 36; i++)
            {
                var type = (i % 9 == 0 ? 9 : i % 9);
                var shuit = (i % 4 == 0 ? 4 : i % 4);
                var card = new Card((TypeCard)type, (SuitCard)shuit);
                Push(card);
            }
        }
    }
}
