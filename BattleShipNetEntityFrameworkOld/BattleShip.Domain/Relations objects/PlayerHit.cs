using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Domain
{
    public class PlayerHit
    {
        public Position Position { get; set; }
        public Player Player { get; set; }

        public int PositionId { get; set; }
        public int PlayerId { get; set; }
    }
}
