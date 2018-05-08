namespace BattleShip.Domain
{
    public class BoatPosition
    {
        public Position Position { get; set; }
        public Boat Boat { get; set; }

        public int PositionId { get; set; }
        public int BoatId { get; set; }

        /// <summary>
        /// Deep copy newBoatPosition object
        /// </summary>
        /// <returns>Deep copy</returns>
        public BoatPosition DeepCopy()
        {
            BoatPosition newBoatPosition = (BoatPosition)this.MemberwiseClone();

            if (newBoatPosition.Boat != null && newBoatPosition.Boat.Player != null && newBoatPosition.Boat.Player.Account != null)
            {
                newBoatPosition.Boat.Player.Account.Password = null;
            }

            return newBoatPosition;
        }
    }
}