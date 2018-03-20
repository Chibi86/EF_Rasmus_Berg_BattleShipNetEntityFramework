using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.GameEngine
{
    public class BoatHit
    {
        public Position Position { get; set; }
        public Boat Boat { get; set; }

        public int PositionID { get; set; }
        public int BoatID { get; set; }
    }
}
