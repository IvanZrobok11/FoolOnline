using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities.Base
{
    public class FoolPlayer<TUserId> : BasePlayer, IFoolPlayer, IIdentity<TUserId>
    {
        public FoolPlayer(TUserId id, PlayerRole role)
        {
            _id = id;
            PlayerRole = role;
        }
        public FoolPlayer(TUserId id)
        {
            _id = id;
            PlayerRole = PlayerRole.Wait;
        }
        private readonly TUserId _id;
        public TUserId Id => _id;
        public PlayerRole PlayerRole { get; set; }
        public bool Confirm { get; set; } = false;
        public void WithdrawCards(params Card[] cards)
        {
            _cards.AddRange(cards);
        }
        public bool Equals(TUserId? other)
        {
            if (_id != null && other.Equals(_id))
            {
                return true;
            }

            return false;
        }
    }
}
