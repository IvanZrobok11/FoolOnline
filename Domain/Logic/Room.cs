using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public class Room<TPlayer> //where Player: IEquatable<Player>
    {
        public Room()
        {
            GameIsProcess = false;
            Players = new List<TPlayer>();
        }

        public List<TPlayer> Players { get; }
        public bool GameIsProcess { get; protected set; }
        public void Connect(TPlayer player)
        {
            Players.Add(player);
        }
        public void Disconnect(TPlayer player)
        {
            if (GameIsProcess)
            {
                GameIsProcess = false;
            }

            var current = Players.FirstOrDefault(p => p.Equals(player));
            if (current == null)
                throw new ArgumentException("PlayerNotFound");

            Players.Remove(current);
        }
    }
}
