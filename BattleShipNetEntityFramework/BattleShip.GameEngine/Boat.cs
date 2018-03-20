using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.GameEngine
{
    public class Boat
    {
        public int ID { get; set; }
        public BoatType Type { get; set; }
        public List<Position> Hits { get; set; }
    }
}
