using Domain.Entities;
using Domain.Entities.Base;
using Domain.Enums;

namespace Domain.Logic
{
    public class FoolSession<TPlayer, TPlayerId> : BaseSession<TPlayer> where TPlayer: BasePlayer, IIdentity<TPlayerId>, IFoolPlayer where TPlayerId : IComparable, IEquatable<TPlayerId>
    {
        public FoolSession(IEnumerable<TPlayer> playersData)
        {
            if (playersData.Count() > 6)
                throw new ArgumentOutOfRangeException("Players in game so more!");
            if (playersData.Count() < 2)
                throw new ArgumentOutOfRangeException("Players in game so less!");

            Players = new List<TPlayer>(playersData.Count());

            // Init players
            foreach (var player in Players)
            {
                //Player<string> p = new Player<string>("", PlayerRole.BeatCards);
                Players.Add(player);
            }

            _deck = new Deck36();
            Tramp = Deck.ChooseTrump();
            Table = new Table(Tramp);
        }

        #region Internal
        internal readonly SuitCard Tramp;
        internal List<TPlayer> Players { get; }
        internal Table Table { get; }
        internal int CountDeck => _deck.Count;

        internal IEnumerable<TPlayerId> ConfirmedList =>
            Players.Where(p => !p.Confirm).Select(p => p.Id);

        internal TPlayer? GetPlayer(TPlayerId id)
        {
            return Players.FirstOrDefault(p => p.Id.Equals(id));
        }

        internal override void Restart()
        {
            foreach (var player in Players)
            {
                player.PlayerRole = PlayerRole.Wait;
                player.Confirm = false;
            }

            _deck.Refresh();
            Start();
        }
        internal override void Start()
        {
            DistributeCards();
            if (Players.All(p => p.PlayerRole == PlayerRole.Wait))
            {
                var random = new Random();
                var index = random.Next(0, Players.Count);
                Players[index].PlayerRole = PlayerRole.PutCards;
                GetNextPlayer(Players[index]).PlayerRole = PlayerRole.PutCards;
            }
        }

        #endregion

        #region Public


        public void PutCardOnTable(TPlayerId playerId, Card card)
        {
            var player = FindPlayer(playerId);
            CheckPlayerState(player, PlayerRole.PutCards);

            if (Table.moverToBeaterCard.Count == 6)
                throw new InvalidOperationException("Table is full");
            if (!Table.TryPutCardOnTable(player.PutCard(card)))
                throw new ArgumentException("You don't put this card");
            //todo
            //Check game is over or player is over
            if (TryExcludePlayer(player))
            {

            }
        }

        public void BeatCard(TPlayerId playerId, Card cardOnTable, Card cardToBeat)
        {
            var player = FindPlayer(playerId);
            CheckPlayerState(player, PlayerRole.BeatCards);

            if (!Table.TryBeatOff(cardOnTable, player.PutCard(cardToBeat)))
                throw new InvalidOperationException("You don't beat this card");
        }

        public void Confirm(TPlayerId playerId)
        {
            if (!Table.AllCardsIsBeaten())
                throw new ArgumentException("All cards are not beaten");
            var player = FindPlayer(playerId);
            player.Confirm = true;

            var defPlayer = Players.First(p => p.PlayerRole == PlayerRole.BeatCards);
            if (Players
                .All(p => p.PlayerRole != PlayerRole.BeatCards && p.Confirm))
            {
                Table.ClearTable();
                MakeNextStep(false, defPlayer);

                //todo
                //Check game is over or player is over
                if (TryExcludePlayer(defPlayer))
                {

                }

                return;
            }

            var nextPlayer = GetNextPlayer(player);
            if (nextPlayer.PlayerRole == PlayerRole.BeatCards)
            {
                nextPlayer = GetNextPlayer(nextPlayer);
            }

            nextPlayer.PlayerRole = PlayerRole.PutCards;
        }

        public void Withdraw(TPlayerId playerId)
        {
            var player = FindPlayer(playerId);
            CheckPlayerState(player, PlayerRole.BeatCards);

            player.WithdrawCards(Table.moverToBeaterCard.Values.ToArray());
            player.WithdrawCards(Table.moverToBeaterCard.Keys.ToArray());

            Table.ClearTable();
            MakeNextStep(true, player);
        }

        #endregion

        #region Private

        private readonly Deck _deck;
        private bool TryExcludePlayer(TPlayer player)
        {
            if (player.CardsCount == 0 && _deck.Count == 0)
            {
                Players.Remove(player);
                return true;
            }

            return false;
        }

        private void MakeNextStep(bool isWithdraw, TPlayer defenderPlayer)
        {
            Table.ClearTable();
            DistributeCards();
            if (isWithdraw)
            {
                var nextPlayer = GetNextPlayer(defenderPlayer);
                nextPlayer.PlayerRole = PlayerRole.PutCards;
                GetNextPlayer(nextPlayer).PlayerRole = PlayerRole.BeatCards;
            }
            else
            {
                defenderPlayer.PlayerRole = PlayerRole.PutCards;
                GetNextPlayer(defenderPlayer);
            }

            foreach (var player in Players)
            {
                player.Confirm = false;
            }
        }

        private TPlayer GetNextPlayer(TPlayer currentPlayer)
        {
            var index = Players.IndexOf(currentPlayer);
            if (index == Players.Count - 1)
            {
                return Players[0];
            }

            return Players[index + 1];
        }

        private void CheckPlayerState(TPlayer player, PlayerRole role)
        {
            if (player.PlayerRole != role)
                throw new InvalidOperationException("You don't put card");
        }

        private TPlayer FindPlayer(TPlayerId playerId)
        {
            var player = Players.FirstOrDefault(p => 
                p.Id.Equals(playerId));
            if (player == null)
                throw new InvalidOperationException("This player don't found");
            return player;
        }

        private void DistributeCards()
        {
            for (int i = 0; i < Players.Count(); i++)
            {
                if (!(Players[i].CardsCount >= 6))
                {
                    for (int j = 0; j < Players[i].CardsCount - 6; j++)
                    {
                        var card = _deck.Pop();
                        Players[i].TakeCard(card);
                    }
                }
            }
        }

        #endregion

    }
}