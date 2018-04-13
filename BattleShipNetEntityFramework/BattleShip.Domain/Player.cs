using System.Collections.Generic;

namespace BattleShip.Domain
{
    public class Player
    {
        public int Id { get; set; }
        public List<Boat> Boats { get; set; }
        public List<PlayerHit> AlreadyHitPositions { get; set; }
        public bool HaveSeenEndScreen { get; set; }
        public Account Account { get; set; }
        public GameBoard GameBoard { get; set; }

        public int AccountId { get; set; }
        public string GameBoardKey { get; set; }

        public bool Lost
        {
            get
            {
                return (Boats.Find(bp => !bp.Sink) == null);
            }
        }

        public Player()
        {
            Boats = new List<Boat>();
            AlreadyHitPositions = new List<PlayerHit>();
            HaveSeenEndScreen = false;
        }
    }
}