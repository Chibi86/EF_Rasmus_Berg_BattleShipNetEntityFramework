using System.Collections.Generic;

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
        public int PlayerId { get; set; }

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

        /// <summary>
        /// Deep copy Boat object
        /// </summary>
        /// <returns>Deep copy</returns>
        public Boat DeepCopy()
        {
            Boat newBoat = (Boat)this.MemberwiseClone();

            if(newBoat.Player != null && newBoat.Player.Account != null)
            {
                newBoat.Player.Account.Password = null;
            }

            return newBoat;
        }
    }
}