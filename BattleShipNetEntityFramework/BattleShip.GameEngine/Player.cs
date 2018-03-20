using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip.GameEngine
{
    public class Player
    {
        public int Id { get; set; }
        public List<PlayerBoat> Boats { get; set; }
        public List<PlayerHit> AlreadyHitPositions { get; set; }
        public bool HaveSeenEndScreen { get; set; }
        public Account Account { get; set; }
        public GameBoard GameBoard { get; set; }

        public int AccountId { get; set; }
        public int GameBoardId { get; set; }
    }
}
