using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Domain
{
    public class Boat
    {
        public int Id { get; set; }
        public BoatType Type { get; set; }
        public List<BoatHit> Hits { get; set; }
        public List<BoatPosition> Positions { get; set; }
        public Player Player { get; set; }

        public int BoatTypeId { get; set; }

        public bool Sink
        {
            get
            {
                return (Hits.Count >= Type.Size);
            }
        }

        public Boat()
        {
            Hits = new List<BoatHit>();
            Positions = new List<BoatPosition>();
        }
    }
}
