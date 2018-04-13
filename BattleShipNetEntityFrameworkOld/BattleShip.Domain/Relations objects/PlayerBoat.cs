namespace BattleShip.Domain
{
    public class PlayerBoat
    {
        public int PlayerId { get; set; }
        public int BoatId { get; set; }

        public Player Player { get; set; }
        public Boat Boat { get; set; }
    }
}
