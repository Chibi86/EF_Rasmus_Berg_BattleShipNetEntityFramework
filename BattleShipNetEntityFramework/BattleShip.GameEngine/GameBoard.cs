using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.GameEngine
{
    public class GameBoard
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public string GameKey { get; set; }
        public bool Private { get; set; }
        public int Turn { get; set; }
    }
}
