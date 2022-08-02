using System.Drawing;
using Domain.Entities.Base;
using Domain.Enums;

namespace Domain.Entities
{
    public class FoolPlayer : FoolPlayer<string>
    {
        public FoolPlayer(string id, PlayerRole role) : base(id, role)
        {
            
        }

        public FoolPlayer(string id) : base(id)
        {
            
        }
    }
}
