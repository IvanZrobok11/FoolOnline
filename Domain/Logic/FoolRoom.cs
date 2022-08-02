using Domain.DTO;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Enums;
using Domain.Extension;

namespace Domain.Logic
{
    public class FoolRoom : Room<FoolPlayer>
    {
        public FoolRoom(int places, int roomId)
        {
            CountPlaces = places;
            ReadyForPlayingList = new List<FoolPlayer>(places);
            _roomId = roomId;
        }

        private readonly int _roomId;
        public int RoomId => _roomId;

        public readonly int CountPlaces;
        private readonly List<FoolPlayer> ReadyForPlayingList;
        public FoolSession<FoolPlayer<string>, string> Session;

        private void StartSession()
        {
            GameIsProcess = true;
            Session = new FoolSession<FoolPlayer<string>, string>(ReadyForPlayingList);
            Session.Start();
        }

        public void SetReadyForPlaying(FoolPlayer playerId)
        {
            ReadyForPlayingList.Add(playerId);
            if (ReadyForPlayingList.Count == CountPlaces)
            {
                StartSession();
                ReadyForPlayingList.Clear();
            }
        }
        public FoolRoomDTO GetGameData(long playerId)
        {
            if (ReadyForPlayingList.Count != CountPlaces)
                throw new ArgumentException("Session has not yet begin");
            var session = new FoolSessionDTO
            {
                Trump = Session.Tramp,
                CardsOnTable = Session.Table.moverToBeaterCard.AsDictionary(kv => 
                    new KeyValuePair<CardDTO, CardDTO>(
                        new CardDTO(kv.Key.Type, kv.Key.Suit), 
                        new CardDTO(kv.Value.Type, kv.Value.Suit))
                ),
                CountCardsInDeck = Session.CountDeck,
                ConfirmedIfCardsIsBeatenIds = Session.ConfirmedList,
                BeaterId = Session.Players.First(p => p.PlayerRole == PlayerRole.BeatCards).Id,
                MoversId = Session.Players.Where(p => p.PlayerRole == PlayerRole.PutCards).Select(_ => _.Id),
            };
            var gameDto = new FoolRoomDTO
            {
                GameIsProcess = this.GameIsProcess && !Session.IsGameOver(),
                PlayersInRoom = ReadyForPlayingList.Select(p => new PlayerDTO
                {
                    Id = p.Id, Name = ""
                }),
                Session = session
            };

            return gameDto;
        }
    }
}
