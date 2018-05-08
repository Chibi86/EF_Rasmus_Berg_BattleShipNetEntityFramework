namespace BattleShip.Domain
{
    public class BoatHit
    {
        public Position Position { get; set; }
        public Boat Boat { get; set; }

        public int PositionId { get; set; }
        public int BoatId { get; set; }

        /// <summary>
        /// Deep copy BoatHit object
        /// </summary>
        /// <returns>Deep copy</returns>
        public BoatHit DeepCopy()
        {
            BoatHit newBoatHit = (BoatHit)this.MemberwiseClone();

            if(newBoatHit.Boat != null && newBoatHit.Boat.Player != null && newBoatHit.Boat.Player.Account != null)
            {
                newBoatHit.Boat.Player.Account.Password = null;
            }

            return newBoatHit;
        }
    }
}